using ODSApi.DBServices;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.BusinessServices
{

    
    public class MatchRunBusinessService :  IMatchRunBusinessService
    {
        private readonly IMatchRunDBService _matchRunService;
        private readonly IMortalitySlopeDBService _mortalitySlopeService;
        private readonly ITimeToNextOfferDBService _timeToBetterService;
        private readonly ILogDBService _logDBService;
        private readonly IGraphParamsDBService _graphParamsDBService;

        public MatchRunBusinessService(IMatchRunDBService matchRunService, IMortalitySlopeDBService mortalitySlopeService, ITimeToNextOfferDBService timeToNextOfferService, ILogDBService logDBService, IGraphParamsDBService graphParamsDBService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));
            _mortalitySlopeService = mortalitySlopeService ?? throw new ArgumentNullException(nameof(mortalitySlopeService));
            _timeToBetterService = timeToNextOfferService ?? throw new ArgumentNullException(nameof(timeToNextOfferService));
            _logDBService = logDBService ?? throw new ArgumentNullException(nameof(logDBService));
            _graphParamsDBService = graphParamsDBService ?? throw new ArgumentNullException(nameof(logDBService));
        }

        public async Task<ServiceResponseEntity<List<MatchRunEntity>>> getByMatchSequence(int match_id,int PtrSequenceNumber)
        {
            /*******************************************************************
             * Get all the records from the MatchRun(PassThrough) Cosmos Collection
             * by matchId and sequenceid
             * *****************************************************************/
            ServiceResponseEntity<List<MatchRunEntity>> serviceResponse = new ServiceResponseEntity<List<MatchRunEntity>>();
            var matchRunRecords = await _matchRunService.getByMatchSequence("SELECT * FROM  c WHERE c.matchId = " + match_id + " and c.sequenceid = " + PtrSequenceNumber);

            

            /*******************************************************************
             * Need to check if the list count is 0.  Null check does not work
             * here because matchRunService returns IEnumerable
             * ******************************************************************/

            if (matchRunRecords.Count() == 0)
            {
                //return NotFound("No Match Run Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
                serviceResponse.ResponseCode = 1;
            }

            /*******************************************************************
            * Get all Mortality Slope records from Cosmos Mortality Slope Collection
            * ******************************************************************/
            var mortalitySlopeRecords = await _mortalitySlopeService.getByMatchSequence("SELECT * FROM c WHERE c.matchId = " + match_id + " and c.sequenceId = " + PtrSequenceNumber);
            if (mortalitySlopeRecords.Count() == 0)
            {
                //return NotFound("No Mortality Slope Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
                serviceResponse.ResponseCode = 2;

            }
            List<Dictionary<string, float>> plotpoints = null;

            /*******************************************************************
            * Validate that mortality slope plot points exist for the retrieved
            * records by matchrun and sequenceid
            * ******************************************************************/

            foreach (var m in mortalitySlopeRecords)
            {
                if (m.MortalitySlopePlotPoints is null || m.MortalitySlopePlotPoints.Count == 0)
                {
                    // return NoContent();  //204
                    //return StatusCode(209, "The mortality slope field is null");
                    serviceResponse.ResponseCode = 3;
                }


                plotpoints = m.MortalitySlopePlotPoints;

            }
            foreach (var x in matchRunRecords)
            {
                x.PlotPoints = plotpoints;
                //   x.TimeStamp = DateTime.Now;
            }


            /*******************************************************************
            * Validate that mortality slope plot points exist for the retrieved
            * records by matchrun and sequenceid
            *******************************************************************/
            var timeToBetterRecords = await _timeToBetterService.getByMatchSequence("SELECT * FROM c WHERE c.matchId = " + match_id + " and c.sequenceId = " + PtrSequenceNumber);
            if (timeToBetterRecords.Count() == 0)
            {
                //return NotFound("No Time to Next Offer Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
                serviceResponse.ResponseCode = 4;
                return serviceResponse;
            }

            // Dictionary<string, int> timeToNextOffer = null;
            Dictionary<string, float> timeToNext30 = null;
            Dictionary<string, float> timeToNext50 = null;



            foreach (var t in timeToBetterRecords)
            {

                if (t.TimeToNext30 is null || t.TimeToNext30.Count == 0 || t.TimeToNext50 is null || t.TimeToNext50.Count == 0)
                {
                    //  return NoContent();  //204
                    serviceResponse.ResponseCode = 5;
                    return serviceResponse;
                }

                //   timeToNextOffer = t.TimeToNextOffer;
                timeToNext30 = t.TimeToNext30;
                timeToNext50 = t.TimeToNext50;
            }

            /*******************************************************************
            * Set time to next 30 and 50 to a value to avoid null pointer exception
            * *******************************************************************/
            foreach (var x in matchRunRecords)
            {
                x.TimeToNext30 = new Dictionary<string, float> { };
                x.TimeToNext50 = new Dictionary<string, float> { };

            }


            //Refactor this to just send the timetobetter30 and timetobetter 50 in calcprob...instead of the entire object

            /*******************************************************************
           * set timetonext30 and 50 to the values from the timetonext offer schema
           *******************************************************************/
            try
            {
                foreach (var x in matchRunRecords) //there is no field time to next 30
                {

                    x.TimeToNext30["time"] = timeToNext30["median"];
                    x.TimeToNext50["time"] = timeToNext50["median"];


                    x.TimeToNext30["probabilityofsurvival"] = CalculateProbabilityOfSurvivalTime30(plotpoints, timeToNext30);
                    x.TimeToNext50["probabilityofsurvival"] = CalculateProbabilityOfSurvivalTime50(plotpoints, timeToNext50);

                    x.TimeToNext30["quantile"] = timeToNext30["quantile"];
                    x.TimeToNext30["quantiletime"] = timeToNext30["quantileTime"];

                    x.TimeToNext50["quantile"] = timeToNext30["quantile"];
                    x.TimeToNext50["quantiletime"] = timeToNext30["quantileTime"];
                }
            }
            catch (NullReferenceException)
            {
                return serviceResponse;
            }

            var graphParamRecords = await _graphParamsDBService.GetMultipleAsync("SELECT * FROM c");
            GraphParamsEntity graphParam = new GraphParamsEntity();
            foreach (var g in graphParamRecords)
            {
                graphParam = g;
            }

            foreach (var m in matchRunRecords)
            {
                m.GraphParam = graphParam;

            }
            serviceResponse.Data = (List<MatchRunEntity>)matchRunRecords;
            return serviceResponse;
        }

        public static float CalculateProbabilityOfSurvivalTime30(List<Dictionary<string, float>> plotPointsList, Dictionary<string, float> timeToBetter)
        {
            // y = survival probability
            // x = number of days
            var time30 = timeToBetter["median"];
            float mortalitySlope;
            float probabilityOfSurvival;
            float y2 = 0.0f;
            float y1 = 0.0f;
            float x2 = 0.0f;
            float x1 = 0.0f;

            List<float> strippedNumbers = new List<float>();
            List<float> strippedSurvival = new List<float>();

            foreach (var allPlotPoints in plotPointsList)  //List of mortality slopes
            {
                foreach (var kvp in allPlotPoints)
                {
                    string key = kvp.Key;
                    float value = kvp.Value;
                    if (key == "time")
                    {
                        strippedNumbers.Add(value);
                    }
                    if (key == "probabilityofsurvival")
                    {
                        strippedSurvival.Add(value);
                    }
                }
            }
            float[] strippedNumbersArray = strippedNumbers.ToArray();
            float[] strippedSurvivalArray = strippedSurvival.ToArray();
            float[] unsortedstrippedNumbersArray = strippedNumbers.ToArray();

            //need to match the probability of survival with the number of days between the two above arrays



            /*******************************************************************
            * Sort the number of days(timetonextoffer)so we can find the next highest day
            * and the next lowest day
            * *******************************************************************/

            Array.Sort(strippedNumbersArray);
            for (var i = 0; i < strippedNumbersArray.Length; i++)
            {
                if (strippedNumbersArray[i] > time30)
                {
                    x2 = strippedNumbersArray[i];
                    break;
                }
            }
            for (var i = 0; i < strippedNumbersArray.Length; i++)
            {
                if (strippedNumbersArray[i] < time30)
                {
                    x1 = strippedNumbersArray[i];
                    break;
                }
            }
            //Get survival probability of each
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == x2)
                {
                    for (var j = 0; j < strippedSurvivalArray.Length; j++)
                    {
                        if (j == i)
                        {
                            y2 = strippedSurvivalArray[j];
                            break;
                        }
                    }
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == x1)
                {
                    for (var j = 0; j < strippedSurvivalArray.Length; j++)
                    {
                        if (j == i)
                        {
                            y1 = strippedSurvivalArray[j];
                            break;
                        }
                    }
                }
            }
            mortalitySlope = (y2 - y1) / (x2 - x1);
            probabilityOfSurvival = mortalitySlope * time30 + 1;

            return probabilityOfSurvival;


        }
        public static float CalculateProbabilityOfSurvivalTime50(List<Dictionary<string, float>> plotPointsList, Dictionary<string, float> timeToBetter)
        {
            var time50 = timeToBetter["median"];
            float mortalitySlope;
            float probabilityOfSurvival;
            float y2 = 0.0f;
            float y1 = 0.0f;
            float x2 = 0.0f;
            float x1 = 0.0f;

            List<float> strippedNumbers = new List<float>();
            List<float> strippedSurvival = new List<float>();

            foreach (var allPlotPoints in plotPointsList)  //List of mortality slopes
            {
                foreach (var kvp in allPlotPoints)
                {
                    string key = kvp.Key;
                    float value = kvp.Value;
                    if (key == "time")
                    {
                        strippedNumbers.Add(value);

                    }
                    if (key == "probabilityofsurvival")
                    {
                        strippedSurvival.Add(value);
                    }
                }
            }
            float[] strippedNumbersArrayList = strippedNumbers.ToArray();
            float[] strippedSurvivalArrayList = strippedSurvival.ToArray();
            float[] unsortedstrippedNumbersArray = strippedNumbers.ToArray();

            //need to match the probability of survival with the number of days between the two above arrays

            Array.Sort(strippedNumbersArrayList);
            for (var i = 0; i < strippedNumbersArrayList.Length; i++)
            {
                if (strippedNumbersArrayList[i] > time50)
                {
                    x2 = strippedNumbersArrayList[i];
                    break;
                }
            }
            for (var i = 0; i < strippedNumbersArrayList.Length; i++)
            {
                if (strippedNumbersArrayList[i] < time50)
                {
                    x1 = strippedNumbersArrayList[i];
                    break;
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == x2)
                {
                    for (var j = 0; j < strippedSurvivalArrayList.Length; j++)
                    {
                        if (j == i)
                        {
                            y2 = strippedSurvivalArrayList[j];
                            break;
                        }
                    }
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == x1)
                {
                    for (var j = 0; j < strippedSurvivalArrayList.Length; j++)
                    {
                        if (j == i)
                        {
                            y1 = strippedSurvivalArrayList[j];
                            break;
                        }
                    }
                }
            }
            mortalitySlope = (y2 - y1) / (x2 - x1);
            probabilityOfSurvival = mortalitySlope * time50 + 1;

            return probabilityOfSurvival;
        }

    }
}

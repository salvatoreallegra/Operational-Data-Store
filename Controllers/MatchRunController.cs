using Microsoft.AspNetCore.Mvc;
using ODSApi.DTOs;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ODSApi.Controllers
{
    [Route("api/matchrun")]
    [ApiController]
    public class MatchRunController : ControllerBase
    {
        private readonly IMatchRunDBService _matchRunService;
        private readonly IMortalitySlopeDBService _mortalitySlopeService;
        private readonly ITimeToNextOfferDBService _timeToBetterService;
        
        
        public MatchRunController(IMatchRunDBService matchRunService, IMortalitySlopeDBService mortalitySlopeService, ITimeToNextOfferDBService timeToBetterService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));
            _mortalitySlopeService = mortalitySlopeService ?? throw new ArgumentNullException(nameof(mortalitySlopeService));
            _timeToBetterService = timeToBetterService ?? throw new ArgumentNullException(nameof(timeToBetterService)); 
        }
        
        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _matchRunService.GetAsync(id));
        }

        // POST api/items
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchRunCreateDto item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _matchRunService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        [HttpGet("{MatchId}/{SequenceId}")]
        public async Task<IActionResult> GetByMatchSequence(int MatchId, int SequenceId)
        {
            /*******************************************************************
             * Get all the records from the MatchRun(PassThrough) Cosmos Collection
             * by matchid and sequenceid
             * *****************************************************************/
            var matchRunRecords = await _matchRunService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);


            /*******************************************************************
             * Need to check if the list count is 0.  Null check does not work
             * here because matchRunService returns IEnumerable
             * ******************************************************************/

            if (matchRunRecords.Count() == 0)
            {
                return NotFound("No Match Run Records Found for MatchId " + MatchId + " and SequenceId " + SequenceId);
            }

            /*******************************************************************
            * Get all Mortality Slope records from Cosmos Mortality Slope Collection
            * ******************************************************************/
            var mortalitySlopeRecords = await _mortalitySlopeService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);

            if (mortalitySlopeRecords.Count() == 0)
            {
                return NotFound("No Mortality Slope Records Found for MatchId " + MatchId + " and SequenceId " + SequenceId);
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
                        return NoContent();  //204
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
               var timeToBetterRecords = await _timeToBetterService.getByMatchSequence("SELECT * FROM TimeToBetter mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);
               if (timeToBetterRecords.Count() == 0)
               {
                return NotFound("No Time to Next Offer Records Found for MatchId " + MatchId + " and SequenceId " + SequenceId);
               }

            Dictionary<string, int> timeToNextOffer = null;


                foreach (var t in timeToBetterRecords)
                {

                if (t.TimeToNextOffer is null || t.TimeToNextOffer.Count == 0)
                {
                    return NoContent();  //204
                }
                else
                {

                    timeToNextOffer = t.TimeToNextOffer;
                }
                    
                }

                //Refactor this to just send the timetobetter30 and timetobetter 50 in calcprob...instead of the entire object
                foreach (var x in matchRunRecords) //there is no field time to next 30
                {
                
                    x.TimeToNext50["time"] = timeToNextOffer["timetobetter30"];
                    x.TimeToNext50["time"] = timeToNextOffer["timetobetter50"];
                    x.TimeToNext30["probabilityofsurvival"] = CalculateProbabilityOfSurvivalTime30(plotpoints,timeToNextOffer);
                    x.TimeToNext50["probabilityofsurvival"] = CalculateProbabilityOfSurvivalTime50(plotpoints,timeToNextOffer);
            }
                
            return Ok(matchRunRecords);
              
      
        }
       
        
        public static float CalculateProbabilityOfSurvivalTime30(List<Dictionary<string,float>> plotPointsList, Dictionary<string,int> timeToBetter)
        {

            // y = survival probability
            // x = number of days
            var time30 = timeToBetter["timetobetter30"];
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
        public static float CalculateProbabilityOfSurvivalTime50(List<Dictionary<string, float>> plotPointsList, Dictionary<string, int> timeToBetter)
        {
            var time50 = timeToBetter["timetobetter50"];
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
                    if(key == "time")
                    {
                        strippedNumbers.Add(value);
                        
                    }
                    if(key == "probabilityofsurvival")
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
            for(var i = 0; i < strippedNumbersArrayList.Length; i++)
            {
                if(strippedNumbersArrayList[i] > time50 )           
                {                                                
                    x2 = strippedNumbersArrayList[i];
                    break;                  
                }
            }
            for(var i = 0; i < strippedNumbersArrayList.Length; i++)
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
                    for(var j = 0; j < strippedSurvivalArrayList.Length; j++)
                    {
                        if(j == i)
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

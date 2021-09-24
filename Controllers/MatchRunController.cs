using Microsoft.AspNetCore.Mvc;
using ODSApi.Entities;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ODSApi.Helpers;

namespace ODSApi.Controllers
{
    [Route("api/matchrun")]
    [ApiController]
    public class MatchRunController : ControllerBase
    {
        private readonly IMatchRunService _matchRunService;
        private readonly IMortalitySlopeService _mortalitySlopeService;
        private readonly ITimeToNextOffer _timeToBetterService;
        //private OdSHelper _helper;
        
        public MatchRunController(IMatchRunService matchRunService, IMortalitySlopeService mortalitySlopeService, ITimeToNextOffer timeToBetterService)
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
        public async Task<IActionResult> Create([FromBody] MatchRunEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _matchRunService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        [HttpGet("{MatchId}/{SequenceId}")]
        public async Task<IActionResult> GetByMatchSequence(int MatchId, int SequenceId)
        {

            var matchRunRecords = await _matchRunService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);

            if (matchRunRecords.Count() == 0)
            {
                return NotFound("No Match Run Records Found for MatchId " + MatchId + " and SequenceId " + SequenceId);
            }

            //get mortality slope record by MatchId and SequenceId
            var mortalitySlopeRecords = await _mortalitySlopeService.getByMatchSequence("SELECT * FROM MatchRun mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);
           
            List<Dictionary<string, float>> plotpoints = null;
            try
            {
                foreach (var m in mortalitySlopeRecords)
                {
                    if (m.WaitListMortality is null || m.WaitListMortality.Count == 0)
                    {
                        return NoContent();  //204
                    }

                    
                    plotpoints = m.WaitListMortality;

                }
                foreach (var x in matchRunRecords)
                {
                    x.PlotPoints = plotpoints;
                    x.TimeStamp = DateTime.Now;
                }
            }
            catch (Exception ex)
            {

            }

               //it's all timetonextoffer now, change your model and controllers
                //get time to better records by MatchId and Sequence ID
               var timeToBetterRecords = await _timeToBetterService.getByMatchSequence("SELECT * FROM TimeToBetter mr WHERE mr.matchid = " + MatchId + " and mr.sequenceid = " + SequenceId);
              
                Dictionary<string, int> timeToBetter = null;
                foreach (var x in timeToBetterRecords)
                {
                    timeToBetter = x.TimeToBetter;
                    
                }
                foreach (var x in matchRunRecords) //there is no field time to next 30
                {
                
                    x.TimeToNext30["time"] = timeToBetter["timetobetter30"];
                    x.TimeToNext50["time"] = timeToBetter["timetobetter50"];
                    x.TimeToNext30["probabilityofsurvival"] = CalculateProbabilityOfSurvivalTime30(plotpoints,timeToBetter);
                    x.TimeToNext50["probabilityofsurvival"] = CalculateProbabilityOfSurvivalTime50(plotpoints,timeToBetter);
            }

            return Ok(matchRunRecords);
              
      
        }
       
        
        public static float CalculateProbabilityOfSurvivalTime30(List<Dictionary<string,float>> plotPointsList, Dictionary<string,int> timeToBetter)
        {
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
                    y2 = strippedNumbersArray[i];
                    break;
                }
            }
            for (var i = 0; i < strippedNumbersArray.Length; i++)
            {
                if (strippedNumbersArray[i] < time30)
                {
                    y1 = strippedNumbersArray[i];
                    break;
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == y2)
                {
                    for (var j = 0; j < strippedSurvivalArray.Length; j++)
                    {
                        if (j == i)
                        {
                            x2 = strippedSurvivalArray[j];
                            break;
                        }
                    }
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == y1)
                {
                    for (var j = 0; j < strippedSurvivalArray.Length; j++)
                    {
                        if (j == i)
                        {
                            x1 = strippedSurvivalArray[j];
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
                    y2 = strippedNumbersArrayList[i];
                    break;                  
                }
            }
            for(var i = 0; i < strippedNumbersArrayList.Length; i++)
            {
                if (strippedNumbersArrayList[i] < time50)        
                {                                            
                    y1 = strippedNumbersArrayList[i];
                    break;
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == y2)
                {
                    for(var j = 0; j < strippedSurvivalArrayList.Length; j++)
                    {
                        if(j == i)
                        {
                            x2 = strippedSurvivalArrayList[j];
                            break;
                        }
                    }
                }
            }
            for (var i = 0; i < unsortedstrippedNumbersArray.Length; i++)
            {
                if (unsortedstrippedNumbersArray[i] == y1)
                {
                    for (var j = 0; j < strippedSurvivalArrayList.Length; j++)
                    {
                        if (j == i)
                        {
                            x1 = strippedSurvivalArrayList[j];
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

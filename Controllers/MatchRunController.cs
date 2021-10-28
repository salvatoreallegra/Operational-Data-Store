/**********************************************
 * This is the applications main controller
 * The Unos Donor Net Front end will Call the Get
 * Request from here
 * Some endpoints exist for development and testing
 * purposes
 * *******************************************/

using Microsoft.AspNetCore.Mvc;
using ODSApi.BusinessServices;
using ODSApi.DTOs;
using ODSApi.Models;
using ODSApi.DBServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ODSApi.Controllers
{
   
    [Route("donornet-analytics/v1/matches/")]
    [ApiController]
    public class MatchRunController : ControllerBase
    {
        private readonly IMatchRunDBService _matchRunService;
        private readonly IMatchRunBusinessService _matchRunBusinessService;

        public MatchRunController(IMatchRunDBService matchRunService, IMatchRunBusinessService matchRunBusinessService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));
            _matchRunBusinessService = matchRunBusinessService ?? throw new ArgumentNullException(nameof(matchRunBusinessService));
        }

        // GET by id, this should stay here so when a post is made, we can call this to display what was
        // created in swagger....should get rid of this for production
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _matchRunService.GetAsync(id));
        }
        [HttpGet]

        //for dev purposes, get rid of for production
        public async Task<IActionResult> List()
        {
            return Ok(await _matchRunService.GetMultipleAsync("SELECT * FROM c"));
        }

        // POST for testing and development only, remove for production
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchRunCreateDto item)
        {
            item.Id = Guid.NewGuid().ToString();
            await _matchRunService.AddAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

         /**********************************************
         * This is the Only Endpoint that should survive to production
         * The entire api is based on this endpoint
         * This endpoint uses the unos auth, validates cosmos data and match run calculations, return
         * codes for validation, and calls the matchrun business service
         * to calculate and return probability of survival
         * *******************************************/

        [HttpGet("{match_id}/potential-recipients/{PtrSequenceNumber}")]
        //[Authorize(PredictiveAnalyticsAuthorizationPolicy.Name)]
        public async Task<IActionResult> GetByMatchSequence(int match_id, int PtrSequenceNumber)
        {
            /************************************************
             * Main controller gets return value from match
             * run business service with all the calculations
             * finished and model populated e.g. time to next offer
             * and probability of survival, then we check response codes
             * to see which http code to return
             * **********************************************/


            //Switch statement here for readability
            var matchRunRecords = await _matchRunBusinessService.getByMatchSequence(match_id, PtrSequenceNumber);

            //add error code here
            if (matchRunRecords.errors == ERRORS.NoPassThroughRecord)
            {
                return NotFound("No Pass Through Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }

            else if (matchRunRecords.errors == ERRORS.NoMortalitySlopeRecord)
            {
                return NotFound("No Mortality Slope Records Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }

            else if (matchRunRecords.errors == ERRORS.NoTimeToNextOfferRecord)
            {
                return NotFound("No Time To Next Offer Record Found for matchId " + match_id + " and SequenceId " + PtrSequenceNumber);
            }

            else if (matchRunRecords.errors == ERRORS.MissingWaitListMortalityData)
            {
                return NotFound("Wait List Mortality Data is Missing" + match_id + " and SequenceId " + PtrSequenceNumber);
            }
            else if (matchRunRecords.errors == ERRORS.MissingTimeToNext30OrTimeToNext50Data)
            {
                return NotFound("Time to Next 30 or 50 is missing " + match_id + " and SequenceId " + PtrSequenceNumber);
            }
            //explain data validation
            else if (matchRunRecords.errors == ERRORS.DataValidationError)
            {
                return StatusCode(500, "Data Validation Error");
            }
           
            List<MatchRunEntity> returnEntity = matchRunRecords.Data;
      
            return Ok(returnEntity);
        }

    }
}

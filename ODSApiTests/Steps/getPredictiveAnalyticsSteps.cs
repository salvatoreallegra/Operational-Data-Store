/************************
 * Steps file - This file actually
 * implements the features
 * as concrete functions
 * 
 * ********************/
using Model.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Serialization.Json;


namespace OdsApiSpecFlowTests.Steps
{
    [Binding]
    public class GetPredictiveAnalyticsSteps
    {

        private const string DEV_BASE_URL = "https://zoe1-webapp-donornet-analytics-dev.azurewebsites.net/";
        private MatchRun _matchRunResponse;
        private readonly int apiVersion = 1;
        private IRestResponse response;
        private readonly ScenarioContext _scenarioContext;
        //private readonly IDictionary<string, string> validHeaders;

        public GetPredictiveAnalyticsSteps(ScenarioContext scenarioContext, MatchRun matchRunResponse)
        {
            _matchRunResponse = matchRunResponse;
            _scenarioContext = scenarioContext;
        }


        [Given(@"Get predictive analytics for a potential match recipient with a matchId of (.*)")]
        public void GivenGetPredictiveAnalyticsForAPotentialMatchRecipientWithAMatchIdOf(int matchId)
        {
            _scenarioContext.Add("matchId", matchId);
            // ScenarioContext.Current.Pending();
        }

        [Given(@"a ptrSequenceNo of (.*)")]
        public void GivenAPtrSequenceNoOf(int ptrSequenceNo)
        {
            _scenarioContext.Add("ptrSequenceNo", ptrSequenceNo);
            //ScenarioContext.Current.Pending();
        }

        [Given(@"Establish the expected model for the get predictive analytics api response")]
        public void GivenEstablishTheExpectedModelForTheGetPredictiveAnalyticsApiResponse()
        {

            Assert.IsNotNull(_matchRunResponse);
            _scenarioContext.Add("expectedModel", _matchRunResponse);
        }

        [Given(@"Valid Default Headers")]
        public void GivenValidDefaultHeaders()
        {
            var client = new RestClient(DEV_BASE_URL);
            var matchId = _scenarioContext["matchId"];
            var ptrSequenceNo = _scenarioContext["ptrSequenceNo"];
            var request = new RestRequest("donornet-analytics/v1/matches/" + matchId + "/potential-recepients/" + ptrSequenceNo, Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.ContentType, "application/json; charset=utf-8");
        }

        [When(@"Send a GET request to the URI v(.*)/matches/@@match-id@@/potential-recipients/@@PtrSequenceNumber@@")]
        public void WhenSendAGETRequestToTheURIVMatchesMatch_IdPotential_RecipientsPtrSequenceNumber(int version)
        {
            version = apiVersion;
            var client = new RestClient(DEV_BASE_URL);
            var matchId = _scenarioContext["matchId"];
            var ptrSequenceNo = _scenarioContext["ptrSequenceNo"];
            var request = new RestRequest("donornet-analytics/v1/matches/" + matchId + "/potential-recepients/" + ptrSequenceNo, Method.GET);
            this.response = client.Execute(request);
        }

        [Then(@"Verify response status code is (.*)")]
        public void ThenVerifyResponseStatusCodeIs(int statusCode)
        {
            Assert.AreEqual(statusCode, ((int)response.StatusCode));
        }

        [Then(@"Validate Output Model for get predictive analytics api response")]
        public void ThenValidateOutputModelForGetPredictiveAnalyticsApiResponse()
        {
            List<MatchRun> matchRunResponse = new JsonDeserializer().Deserialize<List<MatchRun>>(this.response);
            Assert.IsNotNull(matchRunResponse);

        }

    }
}

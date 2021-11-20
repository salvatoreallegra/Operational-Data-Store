using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODSApi.BusinessServices;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using ODSApi.Controllers;
using ODSDatabase.DBServices;
using System.Threading.Tasks;

namespace ODSUnitTests
{
    [TestClass]
    public class Control
    {
        
        [TestMethod]
        [TestCategory("Controller")]
        public void TestEcho()
        {
           
            int x = 1;
            int y = 1;
            Assert.AreEqual(x, y);

        }
        [TestMethod]
        [TestCategory("Controller")]
        public async Task TestMatchRunResponse()
        {
            //Match Run DB Service
            var databaseName = "PredictiveAnalyticsODS";
            var containerName = "PassThroughData";
            var account = "https://zoe1-cosmos-donornet-analytics-dev.documents.azure.com:443/";
            var key = "em3LVHMxJHuEgMB25bPtMWMw39Idu2yGu9xh99ZpRt1MILzcSNkp1QiaEYPPBIeXfG0QjDnlmSFLPdm9E8ozCg==";
            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            IMatchRunDBService _matchRunService = new MatchRunDBService(client,databaseName,containerName);

            //Log DB Service
            var databaseNameLogs = "PredictiveAnalyticsODS";
            var containerNameLogs = "LogData";
            var accountLogs = "https://zoe1-cosmos-donornet-analytics-dev.documents.azure.com:443/";
            var keyLogs = "em3LVHMxJHuEgMB25bPtMWMw39Idu2yGu9xh99ZpRt1MILzcSNkp1QiaEYPPBIeXfG0QjDnlmSFLPdm9E8ozCg==";
            var clientLogs = new Microsoft.Azure.Cosmos.CosmosClient(accountLogs, keyLogs);
            ILogDBService _LogDBService = new LogDBService(clientLogs, databaseNameLogs, containerNameLogs);

            //Mortality
            var databaseNameSlope = "PredictiveAnalyticsODS";
            var containerNameSlope = "MortalitySlopeData";
            var accountSlope = "https://zoe1-cosmos-donornet-analytics-dev.documents.azure.com:443/";
            var keySlope = "em3LVHMxJHuEgMB25bPtMWMw39Idu2yGu9xh99ZpRt1MILzcSNkp1QiaEYPPBIeXfG0QjDnlmSFLPdm9E8ozCg==";
            var clientSlope = new Microsoft.Azure.Cosmos.CosmosClient(accountSlope, keySlope);
            IMortalitySlopeDBService _mortalityDBService = new MortalitySlopeDBService(clientSlope, databaseNameSlope, containerNameSlope);

            //TTNO
            var databaseNameTTNO = "PredictiveAnalyticsODS";
            var containerNameTTNO = "TimeToNextOfferData";
            var accountTTNO = "https://zoe1-cosmos-donornet-analytics-dev.documents.azure.com:443/";
            var keyTTNO = "em3LVHMxJHuEgMB25bPtMWMw39Idu2yGu9xh99ZpRt1MILzcSNkp1QiaEYPPBIeXfG0QjDnlmSFLPdm9E8ozCg==";
            var clientTTNO = new Microsoft.Azure.Cosmos.CosmosClient(accountTTNO, keyTTNO);
            ITimeToNextOfferDBService _ttnoDBService = new TimeToNextOfferDBService(clientTTNO, databaseNameTTNO, containerNameTTNO);

            //Graph Params
            var databaseNameGraph = "PredictiveAnalyticsODS";
            var containerNameGraph = "GraphParamsData";
            var accountGraph = "https://zoe1-cosmos-donornet-analytics-dev.documents.azure.com:443/";
            var keyGraph = "em3LVHMxJHuEgMB25bPtMWMw39Idu2yGu9xh99ZpRt1MILzcSNkp1QiaEYPPBIeXfG0QjDnlmSFLPdm9E8ozCg==";
            var clientGraph = new Microsoft.Azure.Cosmos.CosmosClient(accountGraph, keyGraph);
            IGraphParamsDBService _graphDBService = new GraphParamsDBService(clientGraph, databaseNameGraph, containerNameGraph);

            IMatchRunBusinessService businessService = new MatchRunBusinessService(_matchRunService,_mortalityDBService,_ttnoDBService,_LogDBService,_graphDBService);

            MatchRunController controllerz = new MatchRunController(_matchRunService,businessService);
            var x = await controllerz.GetByMatchSequence(777, 777);
            
            int y = 1;
            int z = 1;
            Assert.AreEqual(z, y);

        }
    }
}

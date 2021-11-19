using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODSApi.BusinessServices;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;

namespace ODSUnitTests
{
    [TestClass]
    public class ProbabilityOfSurvival
    {
        MatchRunBusinessService businessService = new MatchRunBusinessService();

        [TestMethod]
        public void TestCalcProbabilityOfSurvivalTime30()
        {
            List<Dictionary<string, float>> plotPointList = new List<Dictionary<string, float>>();


            plotPointList.Add(new Dictionary<string, float> { { "time", 1.1f } });
            plotPointList[0].Add("survival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1.1f } });
            plotPointList[1].Add("survival", 0.98335113f);
           
            plotPointList[2].Add("time", 365f);
            plotPointList[2].Add("survival", 0.65f);
            plotPointList[3].Add("time", 547.5f);
            plotPointList[3].Add("survival", 0.65f);
            plotPointList[4].Add("time", 730f);
            plotPointList[4].Add("survival", 0.65f);
            plotPointList[5].Add("time", 912.5f);
            plotPointList[5].Add("survival", 0.65f);
            plotPointList[6].Add("time", 1000.5f);
            plotPointList[6].Add("survival", 0.89f);
            plotPointList[7].Add("time", 1100.5f);
            plotPointList[7].Add("survival", 0.91f);
            plotPointList[8].Add("time", 1150.5f);
            plotPointList[8].Add("survival", 0.93f);
            plotPointList[9].Add("time", 1160.5f);
            plotPointList[9].Add("survival", 0.96f);
            plotPointList[10].Add("time", 1170.5f);
            plotPointList[10].Add("survival", 0.98f);


            Dictionary<string, float> timeToBetter = new Dictionary<string,float>();
            timeToBetter.Add("quantile",0.1f);
            timeToBetter.Add("median", 1501.51f);
            timeToBetter.Add("quantileTime", 8020.735f);

            var expectedProbabilityOfSurvival = .2f;

            var survivalProbabilityTime30 = businessService.CalculateProbabilityOfSurvivalTime30(plotPointList, timeToBetter);

            Assert.AreEqual(expectedProbabilityOfSurvival, survivalProbabilityTime30);
        }
        
    }
}

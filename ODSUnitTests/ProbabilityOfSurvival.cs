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

            plotPointList.Add(new Dictionary<string, float> { { "time", 0.0f } });
            plotPointList[0].Add("probabilityofsurvival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 182.5f } });
            plotPointList[1].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 365f } });
            plotPointList[2].Add("probabilityofsurvival", 0.96282464f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 547.5f } });
            plotPointList[3].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 730f } });
            plotPointList[4].Add("probabilityofsurvival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 912.5f } });
            plotPointList[5].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1095f } });
            plotPointList[6].Add("probabilityofsurvival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1277.5f } });
            plotPointList[7].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1500f } });
            plotPointList[8].Add("probabilityofsurvival", 0.85f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1550f } });
            plotPointList[9].Add("probabilityofsurvival", 0.87f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1825f } });
            plotPointList[10].Add("probabilityofsurvival", 0.98335113f);


            Dictionary<string, float> timeToBetter = new Dictionary<string, float>();
            timeToBetter.Add("quantile", 0.1f);
            timeToBetter.Add("median", 1501.51f);
            timeToBetter.Add("quantileTime", 8020.735f);
            //y2 = 0.87f
            //y1 = 0.85f
            //x2 = 1642.5f
            //x1 = 1460f
            //b = .87 - .0004 * 1550 .62   .25

            // mortalitySlope = (y2 - y1) / (x2 - x1);   y2-y1 = .02   x2-x1 = 50  mortalityslope = .0004
            //var b = y2 - mortalitySlope * x2;
            //probabilityOfSurvival = mortalitySlope * time30 + b;

            var expectedProbabilityOfSurvival = .850604f;

            var survivalProbabilityTime30 = businessService.CalculateProbabilityOfSurvivalTime30(plotPointList, timeToBetter);

            Assert.AreEqual(expectedProbabilityOfSurvival, survivalProbabilityTime30);
        }
        [TestMethod]
        public void TestCalcProbabilityOfSurvivalTime50()
        {
            List<Dictionary<string, float>> plotPointList = new List<Dictionary<string, float>>();

            plotPointList.Add(new Dictionary<string, float> { { "time", 0.0f } });
            plotPointList[0].Add("probabilityofsurvival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 182.5f } });
            plotPointList[1].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 365f } });
            plotPointList[2].Add("probabilityofsurvival", 0.96282464f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 547.5f } });
            plotPointList[3].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 730f } });
            plotPointList[4].Add("probabilityofsurvival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 912.5f } });
            plotPointList[5].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1095f } });
            plotPointList[6].Add("probabilityofsurvival", .0999064f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1277.5f } });
            plotPointList[7].Add("probabilityofsurvival", 0.98335113f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1500f } });
            plotPointList[8].Add("probabilityofsurvival", 0.85f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1550f } });
            plotPointList[9].Add("probabilityofsurvival", 0.87f);

            plotPointList.Add(new Dictionary<string, float> { { "time", 1825f } });
            plotPointList[10].Add("probabilityofsurvival", 0.98335113f);


            Dictionary<string, float> timeToBetter = new Dictionary<string, float>();
            timeToBetter.Add("quantile", 0.1f);
            timeToBetter.Add("median", 1501.51f);
            timeToBetter.Add("quantileTime", 8020.735f);
            //y2 = 0.87f
            //y1 = 0.85f
            //x2 = 1642.5f
            //x1 = 1460f
            //b = .87 - .0004 * 1550 .62   .25

            // mortalitySlope = (y2 - y1) / (x2 - x1);   y2-y1 = .02   x2-x1 = 50  mortalityslope = .0004
            //var b = y2 - mortalitySlope * x2;
            //probabilityOfSurvival = mortalitySlope * time30 + b;

            var expectedProbabilityOfSurvival = .850604f;

            var survivalProbabilityTime50 = businessService.CalculateProbabilityOfSurvivalTime50(plotPointList, timeToBetter);

            Assert.AreEqual(expectedProbabilityOfSurvival, survivalProbabilityTime50);
        }

    }
}

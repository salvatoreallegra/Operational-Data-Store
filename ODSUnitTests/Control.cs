using Microsoft.VisualStudio.TestTools.UnitTesting;
using ODSApi.BusinessServices;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using ODSApi.Controllers;
using ODSDatabase.DBServices;

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
    }
}

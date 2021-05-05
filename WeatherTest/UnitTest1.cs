using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace WeatherTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testWeatherOutput()
        {
            Weather.Weather w = new Weather.Weather();
            Assert.AreEqual(2, w.getMinSpread());
        }
    }
}

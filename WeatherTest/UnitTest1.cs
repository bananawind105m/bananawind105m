// ***********************************************************************
// Assembly         : WeatherTest
// Author           : Anthony Cox
// Created          : 05-05-2021
//
// Last Modified By : Anthony Cox
// Last Modified On : 05-05-2021
// ***********************************************************************
// <copyright file="UnitTest1.cs" company="TLC Software.net, LLC">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace WeatherTest
{
    /// <summary>
    /// Defines test class UnitTest1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Defines the test method testWeatherOutput.
        /// </summary>
        [TestMethod]
        public void testWeatherOutput()
        {
            Weather.Weather w = new Weather.Weather();
            Assert.AreEqual(2, w.getMinSpread());
        }
    }
}

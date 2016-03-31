using System;
using System.Collections.Generic;
using CursorAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ParamsCalculationTest
    {
        [TestMethod]
        public void SaveParamsWithZeroCount()
        {
            ParamsCalculationService paramsService = new ParamsCalculationService();
            paramsService.SaverParam(0, 0, 0, 0, new DateTime());
            var expected = new List<float[]>();
            CollectionAssert.AreEqual(paramsService.AmpContainer, expected, "Amp container mast be empty");
        }

        [TestMethod]
        public void MidSpeedWithZeroCount()
        {
            ParamsCalculationService paramsService = new ParamsCalculationService();
            paramsService.MidV(new DateTime(), 0);
            double expected = 0;
            Assert.AreEqual(paramsService.MidMouseSpeed, expected, "Middle mouse speed must be null");
            paramsService.MidV(new DateTime(10L), 1);
        }
    }
}

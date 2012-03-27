using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Depot.Web.DataAnnotations;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class MinimumValueAttributeTests
    {
        private bool _actualResult;
        private MinimumValueAttribute _minimumValueAttribute;

        [TestCase("0.01", "Error Message", true)]
        [TestCase("0.02", "Error Message", true)]
        [TestCase("0.03", "Error Message", false)]
        public void IsValidDecimalTest(string minimumValue, string errorMessage, bool expectedResult)
        {
            GivenMinimumValueAttribute(typeof(decimal), minimumValue, errorMessage);
            WhenIsValidIsCalled(0.02m);
            ThenResultShouldBe(expectedResult);
        }

        private void GivenMinimumValueAttribute(Type type, string minimumValue, string errorMessage)
        {
            _minimumValueAttribute = new MinimumValueAttribute(type, minimumValue, errorMessage);
        }

        private void WhenIsValidIsCalled(decimal @decimal)
        {
            _actualResult = _minimumValueAttribute.IsValid(@decimal);
        }

        private void ThenResultShouldBe(bool expectedResult)
        {
            Assert.AreEqual(expectedResult, _actualResult);
        }
    }
}

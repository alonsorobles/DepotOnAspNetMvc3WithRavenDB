using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Depot.Web.DataAnnotations;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace UnitTests
{
    [TestFixture]
    public class GreaterThanOrEqualToAttributeTests
    {
        private bool _actualResult;
        private GreaterThanOrEqualToAttribute _greaterThanOrEqualToAttribute;
        private ValidationContext _validationContext;
        private TestDelegate _validationDelegate;

        [TestCase("0.01", false)]
        [TestCase("0.02", false)]
        [TestCase("0.03", true)]
        public void IsValidDecimalTest(string minimumValue, bool exceptionExpected)
        {
            GivenMinimumValueAttribute(typeof(decimal), minimumValue);
            AndGivenValidationContext();
            WhenValidationOccurs(0.02m);
            ThenValidationExceptionIsExpected(exceptionExpected);
        }

        [Test]
        public void IsValidNullTest()
        {
            GivenMinimumValueAttribute(typeof(decimal), "0.00");
            AndGivenValidationContext();
            WhenValidationOccurs(null);
            ThenValidationExceptionIsExpected(true);
        }

        private void AndGivenValidationContext()
        {
            _validationContext = new ValidationContext(new DecimalTestClass(), null, null);
        }

        private void GivenMinimumValueAttribute(Type type, string minimumValue)
        {
            _greaterThanOrEqualToAttribute = new GreaterThanOrEqualToAttribute(type, minimumValue);
        }

        private void WhenValidationOccurs(object value)
        {
            _validationDelegate = () => _greaterThanOrEqualToAttribute.Validate(value, _validationContext);
        }

        private void ThenValidationExceptionIsExpected(bool exceptionExpected)
        {
            if (exceptionExpected)
                Assert.Throws<ValidationException>(_validationDelegate);
            else
                Assert.DoesNotThrow(_validationDelegate);
        }

        public class DecimalTestClass
        {
            [GreaterThanOrEqualTo(typeof(decimal), "0.01")]
            public decimal Price { get; set; }
        }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Depot.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class GreaterThanOrEqualToAttribute : ValidationAttribute
    {
        private readonly Type _type;
        private readonly string _minimumValue;
        private const string DefaultErrorMessage = "{0} should be at least {1}";

        public GreaterThanOrEqualToAttribute(Type type, string minimumValue) : base(DefaultErrorMessage)
        {
            if (typeof(IComparable).IsAssignableFrom(type) == false)
                throw new ArgumentException("Must be an Icomparable type", "type");
            if (string.IsNullOrWhiteSpace(minimumValue))
                throw new ArgumentNullException("minimumValue");
            _type = type;
            _minimumValue = minimumValue;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _minimumValue);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            var converter = TypeDescriptor.GetConverter(_type);
            var actualValue = (IComparable) (_type == value.GetType() ? value : converter.ConvertFrom(value));
            var minimumValue = (IComparable) converter.ConvertFromString(_minimumValue);

            Debug.Assert(minimumValue != null, "minimumValue != null");
            return minimumValue.CompareTo(actualValue) > 0
                       ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName))
                       : ValidationResult.Success;
        }
    }
}
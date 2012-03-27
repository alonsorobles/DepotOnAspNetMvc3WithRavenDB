using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Depot.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MinimumValueAttribute : ValidationAttribute
    {
        private static readonly Type ComparableType = typeof (IComparable);
        private readonly Type _type;
        private readonly string _minimumValue;

        public MinimumValueAttribute(Type type, string minimumValue, string errorMessage) : base(errorMessage)
        {
            if (!ComparableType.IsAssignableFrom(type))
                throw new ArgumentException("Must be an IComparable type", "type");
            if (string.IsNullOrWhiteSpace(minimumValue))
                throw new ArgumentException("Must not be null or white space", "minimumValue");
            _type = type;
            _minimumValue = minimumValue;
        }

        public override bool IsValid(object value)
        {
            if (!ComparableType.IsInstanceOfType(value))
                throw new ArgumentException("Must be an IComparable type", "value");
            if (value == null)
                return true;
            return TryComparison(value);
        }

        private bool TryComparison(object value)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(_type);
                var minimum = (IComparable) converter.ConvertFromString(_minimumValue);
                var actual = (IComparable) (value.GetType() == _type ? value : converter.ConvertFrom(value));
                Debug.Assert(minimum != null, "minimum != null");
                return minimum.CompareTo(actual) <= 0;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }
    }
}
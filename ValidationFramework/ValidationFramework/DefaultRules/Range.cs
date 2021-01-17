using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace ValidationFramework
{
    class Range : DataType
    {
        public object value { get; set; }

        public object Minimum { get; private set; }
        public object Maximum { get; private set; }
        public Type OperandType { get; private set; }
        private Func<object, object> Conversion { get; set; }

        public Range(object value, int minimum, int maximum)
        {
            this.value = value;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.OperandType = typeof(int);
        }

        public Range(object value, double minimum, double maximum)
        {
            this.value = value;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.OperandType = typeof(double);
        }

        public Range(object value, Type type, string minimum, string maximum)
        {
            this.value = value;
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.OperandType = type;
        }

        public bool IsValid()
        {
            this.SetupConversion();

            if (value == null)
            {
                return true;
            }
            string s = value as string;
            if (s != null && String.IsNullOrEmpty(s))
            {
                return true;
            }

            object convertedValue = null;

            try
            {
                convertedValue = this.Conversion(value);
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

            IComparable min = (IComparable)this.Minimum;
            IComparable max = (IComparable)this.Maximum;
            return min.CompareTo(convertedValue) <= 0 && max.CompareTo(convertedValue) >= 0;
        }

        public string ErrorMessage()
        {
            return $"must be between {this.Minimum} and {this.Maximum}.";
        }

        private void Initialize(IComparable minimum, IComparable maximum, Func<object, object> conversion)
        {
            if (minimum.CompareTo(maximum) > 0)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, $"{minimum} greater than {maximum}"));
            }

            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Conversion = conversion;
        }

        private void SetupConversion()
        {
            if (this.Conversion == null)
            {
                object minimum = this.Minimum;
                object maximum = this.Maximum;

                if (minimum == null || maximum == null)
                {
                    throw new InvalidOperationException("Must set min and max");
                }

                Type operandType = minimum.GetType();

                if (operandType == typeof(int))
                {
                    this.Initialize((int)minimum, (int)maximum, v => Convert.ToInt32(v, CultureInfo.InvariantCulture));
                }
                else if (operandType == typeof(double))
                {
                    this.Initialize((double)minimum, (double)maximum, v => Convert.ToDouble(v, CultureInfo.InvariantCulture));
                }
                else
                {
                    Type type = this.OperandType;
                    if (type == null)
                    {
                        throw new InvalidOperationException("Must set opera");
                    }
                    Type comparableType = typeof(IComparable);
                    if (!comparableType.IsAssignableFrom(type))
                    {
                        throw new InvalidOperationException(
                            String.Format(
                                CultureInfo.CurrentCulture,
                                "Arbitrary Type Not IComparable",
                                type.FullName,
                                comparableType.FullName));
                    }

#if SILVERLIGHT
                    Func<object, object> conversion = value => (value != null && value.GetType() == type) ? value : Convert.ChangeType(value, type, CultureInfo.CurrentCulture);
                    IComparable min = (IComparable)conversion(minimum);
                    IComparable max = (IComparable)conversion(maximum);
#else
                    TypeConverter converter = TypeDescriptor.GetConverter(type);
                    IComparable min = (IComparable)converter.ConvertFromString((string)minimum);
                    IComparable max = (IComparable)converter.ConvertFromString((string)maximum);

                    Func<object, object> conversion = value => (value != null && value.GetType() == type) ? value : converter.ConvertFrom(value);
#endif // !SILVERLIGHT

                    this.Initialize(min, max, conversion);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ValidationFramework
{
    class MinLength : DataType
    {
        private object value;

        public int Length { get; private set; }

        public MinLength(object value)
        {
            this.value = value;
        }

        public MinLength(object value, int length)
        {
            this.value = value;
            this.Length = length;
        }

        public void ErrorMessage()
        {
            throw new NotImplementedException($"Min length {this.Length}");
        }

        public bool IsValid()
        {
            // Check the lengths for legality
            EnsureLegalLengths();

            var length = 0;
            // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not null.
            if (this.value == null)
            {
                return true;
            }
            else
            {
                var str = this.value as string;
                if (str != null)
                {
                    length = str.Length;
                }
                else
                {
                    // We expect a cast exception if a non-{string|array} property was passed in.
                    length = ((Array)this.value).Length;
                }
            }

            return length >= Length;
        }

        private void EnsureLegalLengths()
        {
            if (Length < 0)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Invalid minlength"));
            }
        }
    }
}

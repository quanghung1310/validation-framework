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

        public string ErrorMessage()
        {
            return $"must be at least {this.Length}.";
        }

        public bool IsValid()
        {
            EnsureLegalLengths();

            var length = 0;
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ValidationFramework
{
    class MinLength : DataType
    {
        int Length; 

        public MinLength()
        {
            //
        }

        public MinLength(int length)
        {
            this.Length = length;
        }

        public string ErrorMessage()
        {
            return $"must be at least {this.Length}.";
        }

        public bool IsValid(object value)
        {
            EnsureLegalLengths();

            var length = 0;
            if (value == null)
            {
                return true;
            }
            else
            {
                var str = value as string;
                if (str != null)
                {
                    length = str.Length;
                }
                else
                {
                    // We expect a cast exception if a non-{string|array} property was passed in.
                    length = ((Array)value).Length;
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

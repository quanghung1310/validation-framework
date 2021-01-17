using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ValidationFramework
{
    class MaxLength : DataType
    {
        private const int MaxAllowableLength = -1;

        public int Length { get; private set; }

        public MaxLength(int length)
        {
            this.Length = length;
        }

        public MaxLength()
        {
            this.Length = MaxAllowableLength;
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
                    length = ((Array)value).Length;
                }
            }

            return MaxAllowableLength == Length || length <= Length;
        }

        public string ErrorMessage()
        {
            return $"may not be greater than {this.Length}.";
        }

        private void EnsureLegalLengths()
        {
            if (Length == 0 || Length < -1)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "Invalid maxlength"));
            }
        }
    }
}

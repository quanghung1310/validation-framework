using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ValidationFramework
{
    class MaxLength : DataType
    {
        private object value;
        private const int MaxAllowableLength = -1;

        public int Length { get; private set; }

        public MaxLength(object value, int length)
        {
            this.Length = length;
            this.value = value;
        }

        public MaxLength(object value)
        {
            this.Length = MaxAllowableLength;
            this.value = value;
        }

        public bool IsValid()
        {
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

            return MaxAllowableLength == Length || length <= Length;
        }

        void DataType.ErrorMessage()
        {
            throw new NotImplementedException($"Max length {this.Length}");
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

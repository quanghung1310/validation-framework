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

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        void DataType.ErrorMessage()
        {
            throw new NotImplementedException();
        }

        private void EnsureLegalLengths()
        {
            if (Length == 0 || Length < -1)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "error"));
            }
        }
    }
}

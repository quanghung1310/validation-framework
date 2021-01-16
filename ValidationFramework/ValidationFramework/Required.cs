using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    class Required : DataType
    {
        private object value;
        public Required(object value)
        {
            this.value = value;
        }

        public bool AllowEmptyStrings { get; set; }

        public void ErrorMessage()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            if (this.value == null)
            {
                return false;
            }

            // only check string length if empty strings are not allowed
            var stringValue = value as string;
            if (stringValue != null && !AllowEmptyStrings)
            {
                return stringValue.Trim().Length != 0;
            }

            return true;
        }
    }
}

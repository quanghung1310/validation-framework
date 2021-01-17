using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    class Required : DataType
    {
        public object value { get; set; }
        public Required(object value)
        {
            this.value = value;
        }

        public bool AllowEmptyStrings { get; set; }

        public string ErrorMessage()
        {
            return "field is required";
        }

        public bool IsValid()
        {
            if (this.value == null)
            {
                return false;
            }

            var stringValue = value as string;
            if (stringValue != null && !AllowEmptyStrings)
            {
                return stringValue.Trim().Length != 0;
            }

            return true;
        }
    }
}

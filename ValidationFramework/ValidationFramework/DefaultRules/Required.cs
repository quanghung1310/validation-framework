using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    class Required : DataType
    {
        public Required()
        {
            //
        }

        public bool AllowEmptyStrings { get; set; }

        public string ErrorMessage()
        {
            return "field is required";
        }

        public bool IsValid(object value)
        {
            if (value == null)
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

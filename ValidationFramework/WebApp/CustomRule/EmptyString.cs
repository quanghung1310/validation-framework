using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace WebApp.CustomRule
{
    public class EmptyString : ValidationFramework.DataType
    {
        public object value { get; set; }
        public EmptyString() { }
        public EmptyString(object value)
        {
            this.value = value;
        }
        public void Set(object value)
        {
            this.value = value;
        }
        public bool AllowEmptyStrings { get; set; }

        public string ErrorMessage()
        {
            return "field is required";
        }

        public bool IsValid(object value)
        {
            if (value == null ||  value == "")
            {
                return true;
            }

            // only check string length if empty strings are not allowed
            //var stringValue = value as string;
            //if (stringValue != null && !AllowEmptyStrings)
            //{
            //    return stringValue.Trim().Length != 0;
            //}

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ValidationFramework1.DefaultRules
{
    class CustomRule: ValidationFramework.DataType
    {
        public object value { get; set; }
        public CustomRule() {  }
        public CustomRule(object value)
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

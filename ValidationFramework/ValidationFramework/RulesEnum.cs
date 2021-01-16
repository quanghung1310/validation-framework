using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationFramework
{
    public static class Rules
    {
        private static List<string> ListRule = new List<string>(

            new string[]
            {
                    "email",
                    "maxlength",
                    "minlength",
                    "numeric",
                    "phone",
                    "range",
                    "url",
                    "regularexpression",
                    "required"
            }

        );
        public const string
            Mail = "email",
            MaxLength = "maxlength",
            MinLength = "minlength",
            Numeric = "numeric",
            Phone = "phone",
            Range = "range",
            Url = "url",
            RegularExpression = "regularexpression",
            Required = "required";
        public static bool Exist(string value)
        {
            return Rules.ListRule.Contains(value);          
        }
    }

}

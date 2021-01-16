using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationFramework
{
    public static class Rules
    {
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
        public bool Exist()
        {
            Enum.GetValues(typeof(Rules)).Cast<Rules>();
            
        }
    }

}

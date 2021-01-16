using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    class CustomFactory
    {
        public string ValidatorObject(string rule, object value, string customMessage)
        {
            return "message";
        }

    }
}

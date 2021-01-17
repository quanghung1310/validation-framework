using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    public  class CustomFactory: AbstractFactoryValidation
    {
        public override string ValidatorObject(string rule, object value, string customMessage)
        {
            return "message";
        }
        public  string ValidatorObjectCustomer(string rule, object value, string customMessage)
        {
            return "message";
        }


    }
}

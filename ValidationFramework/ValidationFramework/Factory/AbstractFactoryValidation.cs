using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    public abstract class AbstractFactoryValidation
    {
        public abstract string ValidatorObject(string rule, object value, string customMessage);
    }
}

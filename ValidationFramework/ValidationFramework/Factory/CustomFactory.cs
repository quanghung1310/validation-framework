using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    public  class CustomFactory: AbstractFactoryValidation
    {
        public override string ValidatorObject(string rule, object value, string customMessage)
        {
            try
            {
                var paramsRule = rule.Split(':');
                DataType dataType;
                Type t = Type.GetType("ValidationFramework1.DefaultRules."+ rule );

                dataType = (DataType)Activator.CreateInstance(t);
                dataType.value = value;
               
             
                if (dataType != null)
                {
                    if (!dataType.IsValid())
                    {
                        return customMessage != null ? customMessage : dataType.ErrorMessage();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }

    }
}

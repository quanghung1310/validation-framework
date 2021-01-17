using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ValidationFramework.Factory
{
    public class CustomFactory: AbstractFactoryValidation
    {
        public override string ValidatorObject(string rule, object value, string customMessage)
        {
            try
            {
                var paramsRule = rule.Split(':');
                DataType dataType;
                var projectName = Assembly.GetEntryAssembly().GetName().Name;
                var str = $"{projectName}.CustomRule." + rule + $", {projectName}";
                Type type = Type.GetType(str);
                dataType = (DataType)Activator.CreateInstance(type);
               
             
                if (dataType != null)
                {
                    if (!dataType.IsValid(value))
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

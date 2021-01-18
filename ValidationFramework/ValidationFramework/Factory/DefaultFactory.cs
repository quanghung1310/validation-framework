using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    class DefaultFactory : AbstractFactoryValidation
    {
        DataType dataType;
        public override string ValidatorObject(string rule, object value, string customMessage)
        {
            try
            {
                var paramsRule = rule.Split(':');
                switch (paramsRule[0].ToLower())
                {
                    case Rules.Mail:
                        dataType = new Email();
                        break;
                    case Rules.MaxLength:
                        dataType = new MaxLength();
                        break;
                    case Rules.MinLength:
                        dataType = new MinLength();
                        break;
                    case Rules.Numeric:
                        dataType = new Numeric();
                        break;
                    case Rules.Phone:
                        dataType = new Phone();
                        break;
                    case Rules.Required:
                        dataType = new Required();
                        break;
                    case Rules.Range:
                        var between = paramsRule[1].Split(',');
                        dataType = new Range(Convert.ToDouble(between[0]), Convert.ToDouble(between[1]));
                        break;
                    case Rules.RegularExpression:
                        dataType = new RegularExpression(paramsRule[1]);
                        break;
                    case Rules.Url:
                        dataType = new Url();
                        break;

                    default:
                        return null;

                }
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

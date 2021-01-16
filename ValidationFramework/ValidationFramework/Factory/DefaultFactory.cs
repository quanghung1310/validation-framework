using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    class DefaultFactory
    {
        public string ValidatorObject(string rule, object value, string customMessage)
        {
            try
            {
                var paramsRule = rule.Split(':');
                DataType dataType;
                switch (paramsRule[0].ToLower())
                {
                    case Rules.Mail:
                        dataType = new Email(value);
                        break;
                    case Rules.MaxLength:
                        dataType = new MaxLength(value);
                        break;
                    case Rules.MinLength:
                        dataType = new MinLength(value);
                        break;
                    case Rules.Numeric:
                        dataType = new Numeric(value);
                        break;
                    case Rules.Phone:
                        dataType = new Phone(value);
                        break;
                    case Rules.Required:
                        dataType = new Required(value);
                        break;
                    case Rules.Range:
                        var between = paramsRule[1].Split(',');
                        dataType = new Range(value, Convert.ToDouble(between[0]), Convert.ToDouble(between[1]));
                        break;
                    case Rules.RegularExpression:
                        dataType = new RegularExpression(value, paramsRule[1]);
                        break;
                    case Rules.Url:
                        dataType = new Url(value);
                        break;

                    default:
                        return null;

                }
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

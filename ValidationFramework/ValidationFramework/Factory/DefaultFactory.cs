using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Factory
{
    class DefaultFactory
    {
        public string ValidatorObject(string rule, object value, string customMessage)
        {
            
                switch (rule.ToLower())
                {
                    case Rules.Mail:
                        var ruleObject =new Email(value);
                        if (!ruleObject.IsValid())
                        {
                        return "s"; //ruleObject.ErrorMessage();
                        }
                        break;
                    case Rules.MaxLength:
                        var email = new Email(value);
                        if (email.IsValid())
                        {
                        return "s";
                        }
                            
                        break;

                    default:
                    break;
                
            }

            return null;
        }
    }
}

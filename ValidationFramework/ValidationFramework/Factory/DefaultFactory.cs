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
                     var email = new Email(value);
                    if (!email.IsValid())
                    {
                        return "sss"; //ruleObject.ErrorMessage();
                    }
                    break;
                case Rules.MaxLength:
                    var maxLength = new MaxLength(value);
                    if (!maxLength.IsValid())
                    {
                        return "sss"; //ruleObject.ErrorMessage();
                    }
                    break;
                case Rules.MinLength:
                    var ruleObject = new MinLength(value);
                    if (!ruleObject.IsValid())
                    {
                        return "sss"; //ruleObject.ErrorMessage();
                    }
                    break;

                default:
                    break;

            }

            return null;
        }
    }
}

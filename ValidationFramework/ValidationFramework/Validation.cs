using System;
using System.Collections.Generic;
using ValidationFramework.Factory;

namespace ValidationFramework
{
    public class Validation
    {
        private static Validation instance;

        public static AbstractFactoryValidation abstractFactoryValidation;
        public static Validation getInstance()
        {
            if (instance == null)
            {
                instance = new Validation();
            }
            return instance;
        }
        public List<ResultValidation> Validator(object Request, Dictionary<string, string> Rules, Dictionary<string, string> CustomMessages = null)
        {
            var DicErrorMessage = new List<ResultValidation>();
            foreach (KeyValuePair<string, string> entry in Rules)
            {

                System.Reflection.PropertyInfo pi = Request.GetType().GetProperty(entry.Key);
                String fieldValue = (String)(pi.GetValue(Request, null));
               
                var rule = entry.Value.ToString().Split('|');
                foreach (var item in rule)
                {
                    if (ValidationFramework.Rules.Exist(item.Split(':')[0].ToLower()))
                    {
                        abstractFactoryValidation = new DefaultFactory();                    
                    }
                    else
                    {
                        abstractFactoryValidation = new CustomFactory();
                      //  Type t = Type.GetType("CustomFactory");
                      //  abstractFactoryValidation = Activator.CreateInstance(t);
                      //  var errorMessage1 = x.ValidatorObject(item, fieldValue,
                      //CustomMessages != null && CustomMessages.ContainsKey(entry.Key) ? CustomMessages[entry.Key] : null);


                    }
                    var errorMessage = abstractFactoryValidation.ValidatorObject(item, fieldValue,
                     CustomMessages != null && CustomMessages.ContainsKey(entry.Key) ? CustomMessages[entry.Key] : null);
                    if (errorMessage!=null)
                    {
                        DicErrorMessage.Add(new ResultValidation() { FieldName=entry.Key,Message=errorMessage });
                    }    
                }
            }
            return DicErrorMessage;
        }
    }
}

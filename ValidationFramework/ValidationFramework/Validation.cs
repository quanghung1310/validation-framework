using System;
using System.Collections.Generic;
using ValidationFramework.Factory;

namespace ValidationFramework
{
    public class Validation
    {
        private static Validation instance = new Validation();

        public static AbstractFactoryValidation abstractFactoryValidation;
        public static Validation getInstance()
        {
            if (instance == null)
            {
                instance = new Validation();
            }
            return instance;
        }
        public List<ResultValidation> Validator(object Request, Dictionary<string, string> Rules, Dictionary<string, string> CustomMessages = null, Dictionary<string, Func<string, string>> FunctionCustom =null)
        {
            var DicErrorMessage = new List<ResultValidation>();
            foreach (KeyValuePair<string, string> entry in Rules)
            {

                System.Reflection.PropertyInfo pi = Request.GetType().GetProperty(entry.Key);
                String fieldValue = (String)(pi.GetValue(Request, null));
                if (entry.Value.GetType() == typeof(string))
                {
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
                        }
                        if (FunctionCustom.ContainsKey(entry.Key.ToString()))
                        {
                            var messageFunc = FunctionCustom[entry.Key.ToString()].Invoke(fieldValue);
                            if (messageFunc != null)
                            {
                                DicErrorMessage.Add(new ResultValidation() { FieldName = entry.Key, Message = messageFunc });
                            }
                        }    
                         
                        var errorMessage = abstractFactoryValidation.ValidatorObject(item, fieldValue,
                   CustomMessages != null && CustomMessages.ContainsKey(entry.Key) ? CustomMessages[entry.Key] : null);
                        if (errorMessage != null)
                        {
                            DicErrorMessage.Add(new ResultValidation() { FieldName = entry.Key, Message = errorMessage });
                        }
                    }    
                
                  
                }
               
            }
            return DicErrorMessage;
        }
    }
}

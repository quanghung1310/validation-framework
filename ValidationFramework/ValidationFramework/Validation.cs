using System;
using System.Collections.Generic;

namespace ValidationFramework
{
    public  class Validation
    {
      
       public void Validator(object Request,Dictionary<string,string> Rules, Dictionary<string, string> Optional =null)
       {
            Dictionary<string, object> ParaValidation = new Dictionary<string, object>();
            foreach (KeyValuePair<string, string> entry in Rules)
            {
                var rule = entry.ToString().Split();
                System.Reflection.PropertyInfo pi = Request.GetType().GetProperty(entry.Key);
                String fieldValue = (String)(pi.GetValue(Request, null));
                ParaValidation.Add( entry.Value, fieldValue);
            }
            


        }
    }
}

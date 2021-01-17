using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationFramework
{
    class Numeric : DataType
    {
        public Numeric()
        {
            //
        }

        public string ErrorMessage()
        {
            return "must be a number.";
        }

        public bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                var numeric = value as string;
                return numeric.All(char.IsNumber);
            }
        }
    }
}

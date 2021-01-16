using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationFramework
{
    class Numeric : DataType
    {
        private object value;

        public Numeric(object value)
        {
            this.value = value;
        }

        public string ErrorMessage()
        {
            return "must be a number.";
        }

        public bool IsValid()
        {
            if (this.value == null)
            {
                return true;
            }
            else
            {
                var numeric = this.value as string;
                return numeric.All(char.IsNumber);
            }
        }
    }
}

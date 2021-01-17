using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    public interface DataType
    {
        bool IsValid(object value);
        string ErrorMessage();
    }
}

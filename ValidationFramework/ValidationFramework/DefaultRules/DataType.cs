using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    public interface DataType
    {
        object value { get; set; } 
        bool IsValid();
        string ErrorMessage();
    }
}

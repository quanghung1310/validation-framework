using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    interface DataType
    {
        bool IsValid();
        void ErrorMessage();
    }
}

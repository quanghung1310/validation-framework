using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework
{
    public class ListValidFunc
    {
        public string FeildName { set; get; }
        public Func<string,string> Func { set; get; }
    }
}

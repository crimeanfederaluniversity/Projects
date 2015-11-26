using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KPIWeb
{
    public class Encode
    {
       
        public static string getType(string code) // "31.05.02"
        {
            return code.Remove(0, 2).Remove(4, 2).Replace(".", string.Empty);
        }


    }
}
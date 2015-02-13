using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    public class CalculateAbb
    {
        public static string deleteSpaces(string input)
        {
            string tmpStr = input;
            tmpStr = tmpStr.Replace(" ", string.Empty);
          
            return tmpStr;
        }

        public static string[] splitString(string input)
        {
            string tmpStr = input;
            string[] abbArray = tmpStr.Split(',', ' ');
            return abbArray;
        }

        static public string Calculate(string input)
        {
            string tmpStr;
            tmpStr = input;
            deleteSpaces(tmpStr);
            string[] abbArray = splitString(tmpStr);
            tmpStr = "";
            foreach (string str in abbArray)
            {
                if ((str == null)||(str == " "))
                tmpStr += "_"+str;
            }
            return tmpStr;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KPIWeb
{
    public static class Action
    {

        public static int Encode(string code) // расшифровка кода спициальности
        {
            string pattern = @"\b(\.+\d+\.)"; 
            Regex regex = new Regex(pattern);
            Match match = regex.Match(code);
            char[] charsToTrim = { '.', ' ', '\'' };
            string result = match.Groups[1].Value.Trim(charsToTrim);


            switch (result)
            {
                case "03": return 1; // бакалавр 
                case "04": return 3; // магистр
                case "05": return 2; // специалист
                case "06": return 4; // аспирант
                case "08": return 4; // аспирант
            }

            return 5;
        }

        public static string EncodeToStr(string code) // расшифровка кода спициальности
        {
            string pattern = @"\b(\.+\d+\.)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(code);
            char[] charsToTrim = { '.', ' ', '\'' };
            string result = match.Groups[1].Value.Trim(charsToTrim);


            switch (result)
            {
                case "03":
                    return "бакалавр";
                case "04":
                    return "магистр";
                case "05":
                    return "специалист";
                case "06":
                    return "аспирант";
                case "08":
                    return "аспирант";
            }

            return "не опр.";
        }
    }
}
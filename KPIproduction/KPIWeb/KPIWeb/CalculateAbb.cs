using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;

namespace KPIWeb
{
    public class CalculateAbb
    {
        public static string replaseAbbWithValue(string input,int reportId)
        {
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            double? a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                         join basic in KPIWebDataContext.BasicParametersTable
                         on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                         where collect.FK_ReportArchiveTable == reportId && basic.AbbreviationEN== input
                         select collect.CollectedValue).Sum();
            return a.ToString();
        }

        public static string deleteSpaces(string input)
        {
            string tmpStr = input;
            //tmpStr = tmpStr.Replace(" ", string.Empty);      
            return tmpStr;
        }

        public static string[] splitString(string input)
        {
            string tmpStr = input;
            string[] abbArray = tmpStr.Split('/','^','+','-','(',')','*',' ');
            return abbArray;
        }

        public static string CheckAbbString(string input)
        {
            string tmpStr;
            tmpStr = input;
            if (tmpStr=="")
            {
                return "Поле формулы не заполнена";
            }
            deleteSpaces(tmpStr);
            string[] abbArray = splitString(tmpStr);
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            tmpStr = "0";
            foreach (string str in abbArray)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        string strtmp;
                        char[] charsToTrim = {'\n','\r',' '};
                        strtmp=str.TrimEnd(charsToTrim);
                        strtmp = strtmp.TrimStart(charsToTrim);
                        strtmp=strtmp.Replace("\r\n", "");
                        int a = (from basic in KPIWebDataContext.BasicParametersTable                                    
                                 where
                                 basic.AbbreviationEN == strtmp 
                                 select basic).Count();           
                        if (a>1)
                        {
                            tmpStr += "\r\n" + a.ToString() + " включений аббревиатуры " + str;
                        }
                        else if(a<1)
                        {
                            tmpStr +="\r\n"+str + " такой аббревиатеры не существует" ;
                        }
                    }
                }
            }
            return tmpStr;
        }

        static public double Calculate(string input, int report)
        {
            string tmpStr;
            tmpStr = input;
            deleteSpaces(tmpStr);
            string[] abbArray = splitString(tmpStr);
            //tmpStr = "";
            foreach (string str in abbArray)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        tmpStr = tmpStr.Replace(str, replaseAbbWithValue(str,report)); // 1 аббревиатура / 2 номер отчета
                    }
                }
            }
            return Polish.Calculate(tmpStr);
           // return tmpStr;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;
using System.Text.RegularExpressions;

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
        public static string replaseAbbWithValueForLevel(string input, int reportId, int Lv0, int Lv1, int Lv2, int Lv3, int Lv4, int Lv5)
        {
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            double? a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                         join basic in KPIWebDataContext.BasicParametersTable                         
                         on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                         join user in KPIWebDataContext.UsersTable
                         on collect.FK_UsersTable equals user.UsersTableID
                         where collect.FK_ReportArchiveTable == reportId 
                         && basic.AbbreviationEN == input
                         && (user.FK_ZeroLevelSubdivisionTable   == Lv0 || Lv0==0)
                         && (user.FK_FirstLevelSubdivisionTable  == Lv1 || Lv1==0)
                         && (user.FK_SecondLevelSubdivisionTable == Lv2 || Lv2==0)
                         && (user.FK_ThirdLevelSubdivisionTable  == Lv3 || Lv3==0)
                         && (user.FK_FourthLevelSubdivisionTable == Lv4 || Lv4==0)
                         && (user.FK_FifthLevelSubdivisionTable  == Lv5 || Lv5==0)
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
                        string tmp;
                        tmp = 
                        tmpStr = tmpStr.Replace(str, replaseAbbWithValue(str,report)); // 1 аббревиатура / 2 номер отчета

                        /*string tmp;
                        int idx = tmp.LastIndexOf(s1);
                        if (idx != -1)
                            s = s.Remove(idx, s1.Length).Insert(idx, s2);*/
                    }
                }
            }
            return Polish.Calculate(tmpStr);
           // return tmpStr;
        }

        static public double CalculateForLevel(string input, int report, int Lv0, int Lv1, int Lv2, int Lv3, int Lv4, int Lv5)
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
                       // tmpStr = tmpStr.Replace(str, replaseAbbWithValue(str, report)); // 1 аббревиатура / 2 номер отчета
                        tmpStr = tmpStr.Replace(str, replaseAbbWithValueForLevel(str, report, Lv0, Lv1, Lv2, Lv3, Lv4, Lv5));
                    }
                }
            }
            return Polish.Calculate(tmpStr);
        }
    }
}
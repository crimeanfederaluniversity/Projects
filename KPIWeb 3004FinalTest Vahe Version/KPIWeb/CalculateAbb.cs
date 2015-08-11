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
        public static List<string> errorList = new List<string>();

        public static string replaseAbbWithValueForLevel(int type, string input, int reportId, int specID, int Lv0,
            int Lv1, int Lv2, int Lv3,
            int Lv4, int Lv5)
        {
            string abbTmp = input;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            double? a = 0;
            if (specID == 0)
            {
                a = (from collect in kpiWebDataContext.CollectedBasicParametersTable                     
                     join basic in kpiWebDataContext.BasicParametersTable
                     on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                     where
                          (collect.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0)
                         && (collect.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                         && (collect.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                         && (collect.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                         && (collect.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                         && (collect.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                         && basic.AbbreviationEN == abbTmp
                         && collect.FK_ReportArchiveTable == reportId
                     select collect.CollectedValue).Sum();  
            }
            else
            {
                a = (from collect in kpiWebDataContext.CollectedBasicParametersTable
                     join fourth in kpiWebDataContext.FourthLevelSubdivisionTable
                     on collect.FK_FourthLevelSubdivisionTable equals fourth.FourthLevelSubdivisionTableID
                     join basic in kpiWebDataContext.BasicParametersTable
                     on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                     where
                         fourth.FK_Specialization == specID
                         && (collect.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0)
                         && (collect.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                         && (collect.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                         && (collect.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                         && (collect.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                         && (collect.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                         && basic.AbbreviationEN == abbTmp
                     select collect.CollectedValue).Sum();              
            }
            if (a == null) // так быть не должно)
            {
                a = 0;
                //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Замена аббревиатуры вернула NULL "+
                //  abbTmp+" "+Lv0+" "+Lv1+" "+Lv2+" "+Lv3+" "+Lv4+" "+Lv5+" " +reportId); 
            }
            return a.ToString();       
        }
        public static string deleteSpaces(string input)
        {
            string tmpStr = input;
            tmpStr = tmpStr.Replace(" ","");      
            return tmpStr;
        }
        public static List<int> GetBasicIdList(string input)
        {
            KPIWebDataContext kpiweb = new KPIWebDataContext();
            List<int> tmpList = new List<int>();
            string tmpStr;
            tmpStr = input;
            deleteSpaces(tmpStr);
            tmpStr = tmpStr.Replace("\r", "");
            tmpStr = tmpStr.Replace("\n", "");
            string[] abbArray = splitString(tmpStr);
            foreach (string str in abbArray)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        int result = (from a in kpiweb.BasicParametersTable
                                 where a.AbbreviationEN == str
                                 select a.BasicParametersTableID).FirstOrDefault();
                        if ((result != null)&&(result > 0))
                        {
                            tmpList.Add(result);
                        }
                    }
                }
            }
            //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return tmpList;
        }
        public static List<int> GetCollectIdList(string input)
        {
            KPIWebDataContext kpiweb = new KPIWebDataContext();
            List<int> tmpList = new List<int>();
            string tmpStr;
            tmpStr = input;
            deleteSpaces(tmpStr);
            tmpStr = tmpStr.Replace("\r", "");
            tmpStr = tmpStr.Replace("\n", "");
            string[] abbArray = splitString(tmpStr);
            foreach (string str in abbArray)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        int result = (from a in kpiweb.CalculatedParametrs
                                      where a.AbbreviationEN == str
                                      select a.CalculatedParametrsID).FirstOrDefault();
                        if ((result != null) && (result > 0))
                        {
                            tmpList.Add(result);
                        }
                    }
                }
            }
            //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return tmpList;
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
        public static double CalculateForLevel(int type, string input, int report, int spec, int Lv0, int Lv1, int Lv2, int Lv3, int Lv4, int Lv5, int param1)
        {
            string tmpStr;
            tmpStr = input;
            deleteSpaces(tmpStr);
            tmpStr = tmpStr.Replace("\r", "");
            tmpStr = tmpStr.Replace("\n", "");
            string[] abbArray = splitString(tmpStr);
            //tmpStr = "";
            foreach (string str in abbArray)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {

                        int idx = tmpStr.IndexOf(str);
                        if (idx != -1)
                        {
                            string strtmp = str;
                            bool isforall = false;
                            if (str.Substring(0, 4) == "CFU_")
                            {
                                isforall = true;
                                strtmp = (str.Remove(0, 4));
                            }
                            if (isforall)
                            {
                                tmpStr = tmpStr.Remove(idx, str.Length).Insert(idx, replaseAbbWithValueForLevel(type, strtmp, report, spec, Lv0, 0, 0, 0, 0, 0));
                            }
                            else
                            {
                                tmpStr = tmpStr.Remove(idx, str.Length).Insert(idx, replaseAbbWithValueForLevel(type, str, report, spec, Lv0, Lv1, Lv2, Lv3, Lv4, Lv5));
                            }
                        }
                                 
                    }
                }
            }
          //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return Polish.Calculate(tmpStr);
        }
        public static double? SumForLevel(int BasicId, int report,int SpecID, int Lv0, int Lv1, int Lv2, int Lv3, int Lv4, int Lv5)
        {
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            double? a = 0;
            if (SpecID == 0)
            {
                    a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                    where
                        (collect.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0)
                        && (collect.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                        && (collect.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                        && (collect.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                        && (collect.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                        && (collect.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                        && (collect.FK_ReportArchiveTable == report)
                        && collect.FK_BasicParametersTable == BasicId
                    select collect.CollectedValue).Sum();
            }
            else
            {
                 a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                      join Fourth in KPIWebDataContext.FourthLevelSubdivisionTable
                      on collect.FK_FourthLevelSubdivisionTable equals Fourth.FourthLevelSubdivisionTableID
                    where
                           Fourth.FK_Specialization == SpecID 
                        && (collect.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0) 
                        && (collect.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                        && (collect.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                        && (collect.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                        && (collect.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                        && (collect.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                        && (collect.FK_ReportArchiveTable == report)
                        && collect.FK_BasicParametersTable == BasicId
                    select collect.CollectedValue).Sum();
            }
            if (a == null)
            {
                a = 0;
            }
            return a;
        }
    }
}
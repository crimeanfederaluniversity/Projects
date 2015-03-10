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
        public static string replaseAbbWithValueForLevel(int type, string input, int reportId, int Lv0, int Lv1, int Lv2, int Lv3,
            int Lv4, int Lv5)
        {
            string abbTmp = input;
            string[] tmpArr = abbTmp.Split('#');
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            double? a=0;
            if (tmpArr.Length < 2) //значит в аббревиатуре нет #
            {
                if (type == 1) // считаем по аббревиатурам базовых
                {                   
                     a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                                 join basic in KPIWebDataContext.BasicParametersTable
                                     on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                                 join user in KPIWebDataContext.UsersTable
                                     on collect.FK_UsersTable equals user.UsersTableID
                                 where collect.FK_ReportArchiveTable == reportId
                                       && basic.AbbreviationEN == abbTmp
                                       && (user.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0)
                                       && (user.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                                       && (user.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                                       && (user.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                                       && (user.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                                       && (user.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                                 select collect.CollectedValue).Sum();
                    if (a == null) // так быть не должно)
                    {
                        a = 0;
                       //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Замена аббревиатуры вернула NULL "+
                       //  abbTmp+" "+Lv0+" "+Lv1+" "+Lv2+" "+Lv3+" "+Lv4+" "+Lv5+" " +reportId); 
                    }
                }
                else if (type == 2) // по рассчитанным рассчетным
                {
                    a = (from collect in KPIWebDataContext.CollectedCalculatedParametrs
                                 join calc in KPIWebDataContext.CalculatedParametrs
                                     on collect.FK_CalculatedParametrs equals calc.CalculatedParametrsID                               
                                 where collect.FK_ReportArchiveTable == reportId
                                       && calc.AbbreviationEN == abbTmp                                    
                                 select collect.CollectedValue).Sum();
                    if (a == null) // так быть не должно)
                    {
                        a = 0;
                       // LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Замена аббревиатуры вернула NULL "+
                       // abbTmp+" "+Lv0+" "+Lv1+" "+Lv2+" "+Lv3+" "+Lv4+" "+Lv5+" " +reportId); 
                    }
                }
                
                return a.ToString();
            }
            /*
            else
            {
                switch (tmpArr[0])
                {
                    case "0": // тут у нас базовые показатели
                    {
                        KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                        double? a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                            join basic in KPIWebDataContext.BasicParametersTable
                                on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                            join user in KPIWebDataContext.UsersTable
                                on collect.FK_UsersTable equals user.UsersTableID
                            where collect.FK_ReportArchiveTable == reportId
                                  && basic.AbbreviationEN == tmpArr[1]
                                  && (user.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0)
                                  && (user.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                                  && (user.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                                  && (user.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                                  && (user.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                                  && (user.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                            select collect.CollectedValue).Sum();
                        return a.ToString();
                        break;
                    }
                    case "1": // РЕКУРСИЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯЯ
                    {
                        KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                        string tmpStr = (from a in KPIWebDataContext.CalculatedParametrs
                            where a.AbbreviationEN == tmpArr[1]
                            select a.Formula).FirstOrDefault();
                        return CalculateForLevel(tmpStr, reportId, Lv0, Lv1, Lv2, Lv3, Lv4, Lv5, 0).ToString();
                        break;
                    }
                    case "2": // Другое)
                    {
                        switch (tmpArr[1])
                        {
                            case "helo":
                            {
                                return "11";
                                break;
                            }
                            case "bibi":
                            {
                                return "12";
                                break;
                            }
                            default:
                            {
                                return "11";
                            }
                        }
                        break;
                    }
                    default: // кто-то где-то затупил
                    {
                        //надо вывести ошибку
                        break;
                    }*/
                //}
            //}
            return "0";
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

        public static double CalculateForLevel(int type,string input, int report, int Lv0, int Lv1, int Lv2, int Lv3, int Lv4, int Lv5, int param1)
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

                            tmpStr = tmpStr.Remove(idx, str.Length).Insert(idx, replaseAbbWithValueForLevel(type, str, report, Lv0, Lv1, Lv2, Lv3, Lv4, Lv5));                                                               
                    }
                }
            }
          //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return Polish.Calculate(tmpStr);
        }

        public static double? SumForLevel(int BasicId, int report, int Lv0, int Lv1, int Lv2, int Lv3, int Lv4, int Lv5)
        {
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            double? a = (from collect in KPIWebDataContext.CollectedBasicParametersTable
                         join basic in KPIWebDataContext.BasicParametersTable
                         on collect.FK_BasicParametersTable equals basic.BasicParametersTableID
                         join user in KPIWebDataContext.UsersTable
                         on collect.FK_UsersTable equals user.UsersTableID
                         where collect.FK_ReportArchiveTable == report
                         && basic.BasicParametersTableID == BasicId
                         && (user.FK_ZeroLevelSubdivisionTable == Lv0 || Lv0 == 0)
                         && (user.FK_FirstLevelSubdivisionTable == Lv1 || Lv1 == 0)
                         && (user.FK_SecondLevelSubdivisionTable == Lv2 || Lv2 == 0)
                         && (user.FK_ThirdLevelSubdivisionTable == Lv3 || Lv3 == 0)
                         && (user.FK_FourthLevelSubdivisionTable == Lv4 || Lv4 == 0)
                         && (user.FK_FifthLevelSubdivisionTable == Lv5 || Lv5 == 0)
                         select collect.CollectedValue).Sum();
            return a;
        }
    }
}
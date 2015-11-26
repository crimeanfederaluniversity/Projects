using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using KPIWeb;

namespace KPIWeb
{
    public class Abbreviature
    {
        public static string deleteSpaces(string input)
        {
            string tmpStr = input;
            tmpStr = tmpStr.Replace(" ", "");
            return tmpStr;
        }
        public static string[] splitString(string input)
        {
            string tmpStr = input;
            string[] abbArray = tmpStr.Split('/', '^', '+', '-', '(', ')', '*', ' ');
            return abbArray;
        }
        public static List<CalculatedParametrs> GetCalculatedList(string input)
        {
            KPIWebDataContext kpiweb = new KPIWebDataContext();
            List<CalculatedParametrs> tmpList = new List<CalculatedParametrs>();
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
                        string strtmp = str;
                        bool isforall = false;
                        if (str.Substring(0,4) == "CFU_" )
                        {
                            isforall = true;
                            strtmp = (str.Remove(0,4));
                        }
                        CalculatedParametrs result = (from a in kpiweb.CalculatedParametrs
                                      where a.AbbreviationEN == strtmp
                                      select a).FirstOrDefault();
                        if (result != null)
                        {
                            if (isforall)
                            {
                                result.AbbreviationRU = "CFU_";
                            }
                            tmpList.Add(result);
                        }
                    }
                }
            }
            //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return tmpList;
        }
        public static List<BasicParametersTable> GetBasicList(string input)
        {
            KPIWebDataContext kpiweb = new KPIWebDataContext();
            List<BasicParametersTable> tmpList = new List<BasicParametersTable>();
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
                        BasicParametersTable result = (from a in kpiweb.BasicParametersTable
                                      where a.AbbreviationEN == str
                                      select a).FirstOrDefault();
                        if (result != null)
                        {
                            tmpList.Add(result);
                        }
                    }
                }
            }
            //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return tmpList;
        }

        public static string AddCfuToFormula(string formula)
        {
            KPIWebDataContext kpiweb = new KPIWebDataContext();
            string tmpStr;
            string strtoreturn = formula;
            tmpStr = formula;
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
                        BasicParametersTable result = (from a in kpiweb.BasicParametersTable
                                                       where a.AbbreviationEN == str
                                                       select a).FirstOrDefault();
                        if (result != null)
                        {
                            string tmpStr2 = "CFU_" + str;
                           // int i = strtoreturn.IndexOf(str);
                           strtoreturn = strtoreturn.Replace(str,tmpStr2);
                            //tmpList.Add(result);
                        }
                    }
                }
            }
            //  LogHandler.LogWriter.WriteLog(LogCategory.ERROR, errorList.ToString());            
            return strtoreturn;
        }

        public static string ReturnCalcFormula(string abb)
        {
           // string abbTmp = abb;
            //string[] tmpArr = abbTmp.Split('#');
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            string Formula = (from a in KPIWebDataContext.CalculatedParametrs
                where a.Active == true
                      && a.AbbreviationEN == abb
                select a.Formula).FirstOrDefault();

                    if (Formula == null) // так быть не должно)
                    {
                        Formula = "0";
                    }
         //   Formula = "" + Formula + "";
            return "(" + Formula + ")"; ;
        }
        public static string CalculatedAbbToFormula(string IndicatorFormulaString)
        {
           // DateTime time1 = DateTime.Now; //Точка начала отсчета времени

          

            string BigFormulaString = IndicatorFormulaString;
            List<CalculatedParametrs> CalculatedList = GetCalculatedList(IndicatorFormulaString);
            foreach (CalculatedParametrs Calculated in CalculatedList)
            {
                if (Calculated.AbbreviationRU == "CFU_")
                {
                    Calculated.Formula = AddCfuToFormula(Calculated.Formula);
                    Calculated.AbbreviationEN = "CFU_" + Calculated.AbbreviationEN;
                }

                int idx = BigFormulaString.IndexOf(Calculated.AbbreviationEN);
                if (idx < 0)
                {
                    //FORMULA_ERROR
                }
                else
                {
                    
                    //BigFormulaString = BigFormulaString.Replace(Calculated.AbbreviationEN, "(" + Calculated.Formula + ")"); //проверок нет
                    BigFormulaString = BigFormulaString.Remove(idx, Calculated.AbbreviationEN.Length).Insert(idx, "(" + Calculated.Formula + ")");                    
                }
            }

           /* DateTime time2 = DateTime.Now; //Точка окончания отсчета времени
            long elapsedTicks = time2.Ticks - time1.Ticks; // подсчитываем число тактов, один такт соответствует 100 наносекундам
            double tmpp = elapsedTicks * 1E-7; // делим на 10^7 для отображения времени в секундах
            int j =0;*/
            return BigFormulaString;
        }
    }  
}
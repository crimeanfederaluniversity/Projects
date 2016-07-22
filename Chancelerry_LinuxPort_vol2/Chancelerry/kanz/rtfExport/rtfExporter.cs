using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Providers.Entities;
using Npgsql;

namespace Chancelerry.kanz.rtfExport
{
    public class rtfExporter
    {
        private string _fileContent;
        private void OpenFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                _fileContent = File.ReadAllText(filePath);
            }        
        }
        private void ReplaceMarkWithValue(string mark, string value)
        {
            _fileContent = _fileContent.Replace(mark, (value));
        }
        private string CreateNewFile(string folderPath, int cardId, int userId, int type)
        {
            DateTime dtNow = DateTime.Now;;
            string fileName = type + "_" + cardId + "_" + userId + "_" + dtNow.Day + "_" + dtNow.Month + "_" + dtNow.Year + "_" + dtNow.Hour + "_" + dtNow.Minute + "_" + dtNow.Second + ".rtf";
            string filePath = folderPath + fileName;
            System.IO.File.WriteAllText(filePath, _fileContent);
            return filePath;
        }
        private int  SendFileToClient(string filePath)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=file.rtf");
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.TransmitFile(filePath);
                HttpContext.Current.Response.End();
                return 6;
            }
            catch (Exception)
            {
                return 7;
            }
        }

        private List<string> GetAllMarks()
        {
            string pattern = "#mark[^#]+#";
            List<string> marks = new List<string>();
            foreach (Match match in Regex.Matches(_fileContent, pattern, RegexOptions.IgnoreCase))
            {
                marks.Add(match.ToString());
            }
            return marks;
        }

        private string GetDataFromList(List<CollectedFieldsValues> allCollectedFieldsValues, int instance, int fieldId) // if instance == -1 то любой сойдет
        {
            string tmp = (from a in allCollectedFieldsValues
                          where a.FkField == fieldId
                                && (a.Instance == instance || instance == -1)
                          select a).OrderByDescending(mc => mc.Version).Select(mc => mc.ValueText).FirstOrDefault();
            if (tmp == null)
                return "";
            return tmp;
        }



        private string GetValueByMark(string mark, List<CollectedFieldsValues> allValues, int instance)
        {
            string strWithoutMark = mark.Substring(5, mark.Length - 6);
            string strInstance = strWithoutMark.Substring(0, 1);
            int instanceType = -1;
            Int32.TryParse(strInstance, out instanceType);
            string strWithoutMarkAndInstance = strWithoutMark.Substring(1, strWithoutMark.Length - 1);
            string resultString = strWithoutMarkAndInstance;
            string pattern = "&S[^&]+&E";
            foreach (Match match in Regex.Matches(strWithoutMarkAndInstance, pattern, RegexOptions.IgnoreCase))
            {
                string currentSmallMark = match.ToString();
                string currentSmallMarkCutted = currentSmallMark.Replace("&S", "").Replace("&E", "");
                
                List<string> values = (currentSmallMarkCutted.Split('*')).ToList();
                int fieldId = -1;
                Int32.TryParse(values[0],out fieldId);

                if (instanceType == 0 || instanceType == 1)
                {
                    string value = GetDataFromList(allValues, instanceType == 0?-1:instance, fieldId);

                    if (values[1] == "d")
                    {
                        DateTime tmpDate = DateTime.MinValue;
                        DateTime.TryParse(value, out tmpDate);
                       
                        value = (tmpDate.Year < 1900)? values[2] : values[3] + tmpDate.Day.ToString("D2")+"."+tmpDate.Month.ToString("D2") + "."+ tmpDate.Year + values[4];
                    }
                    else
                    {
                        value = value == "" ? values[2] : values[3] + RussianStringToHexString(value) + values[4];
                    }
                    
                    resultString = resultString.Replace(currentSmallMark, value);
                }

            }
            return resultString;
        }

        public string Export (string filePath, string tmpFilesPath, int cardId, int userId, int type, int instance )
        {
            ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            List<CollectedFieldsValues> allCollectedInCard = (from a in chancDb.CollectedFieldsValues
                                                              where a.FkCollectedCard == cardId
                                                              select a).ToList();
            OpenFile(filePath);
            List<string> allMarks = GetAllMarks();
            foreach (string key in allMarks)
            {
                string resultValue = "";

                if (key == "#markUserName#")
                {
                    resultValue =
                        (from a in chancDb.Users where a.UserID == userId select a.Name).FirstOrDefault().ToString();
                }
                else if (key== "#markTodayDate#")
                {
                    resultValue = DateTime.Now.Date.ToString();
                }
                else
                {
                    resultValue = GetValueByMark(key, allCollectedInCard,instance);
                }
                ReplaceMarkWithValue(key, resultValue);
            }
            string path = CreateNewFile(tmpFilesPath, cardId, userId, type);
            int sendStatus = SendFileToClient(path);
            return sendStatus.ToString();         
        }

        private string RussianStringToHexString(string incomingString)
        {
            string strToReturn = "";
            asciiCharConverter charConverter = new asciiCharConverter();
            foreach (char ch in incomingString)
            {
                strToReturn += charConverter.GetAsciiHexByChar(ch);
            }
            return "\\f0\\lang1049"+strToReturn;
        }
    }
}
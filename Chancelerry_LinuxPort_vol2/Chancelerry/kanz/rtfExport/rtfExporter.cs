using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

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
            _fileContent = _fileContent.Replace(mark, RussianStringToHexString(value));
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
        public string Export (string filePath, Dictionary<string,string> changesDictionary, string tmpFilesPath, int cardId, int userId, int type )
        {
            OpenFile(filePath);
            foreach (string key in changesDictionary.Keys)
            {
                string tmp = key;
                changesDictionary.TryGetValue(key, out tmp);
                ReplaceMarkWithValue(key, tmp);
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
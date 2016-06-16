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
        private bool OpenFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            try
            {
                _fileContent = File.ReadAllText(filePath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void ReplaceMarkWithValue(string mark, string value)
        {
            _fileContent = _fileContent.Replace(mark, RussianStringToHexString(value));
        }

        private string CreateNewFile(string folderPath, int cardId, int userId, int type)
        {
            string fileName = type+"_"+cardId + "_" + userId + "_" + DateTime.Now+".rtf";
            fileName = fileName.Replace(":", "_");
            fileName = fileName.Replace(" ", "_");
            string filePath = folderPath + fileName;
            File.WriteAllText(filePath, _fileContent);
            return filePath;
        }

        private void SendFileToClient(string filePath)
        {
            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.AppendHeader("content-disposition","attachment; filename=file.rtf");
            HttpContext.Current.Response.TransmitFile(filePath);
            HttpContext.Current.Response.End();
        }

        public void Export (string filePath, Dictionary<string,string> changesDictionary, string tmpFilesPath, int cardId, int userId, int type )
        {
            if (OpenFile(filePath))
            {
                foreach (string key in changesDictionary.Keys)
                {
                    string tmp = key;
                    changesDictionary.TryGetValue(key, out tmp);
                    ReplaceMarkWithValue(key, tmp);
                }
                SendFileToClient(CreateNewFile(tmpFilesPath, cardId, userId, type));
            }
        }

        private string RussianStringToHexString(string incomingString)
        {
            string strToReturn = "";
            byte[] ascii = Encoding.GetEncoding(1251).GetBytes(incomingString);
            foreach (Byte b in ascii) 
            {
                strToReturn += "\\'" + b.ToString("X");
            }
            return "\\f0\\lang1049"+strToReturn;
        }

    }
}
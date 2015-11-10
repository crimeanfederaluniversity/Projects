using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Word;

namespace Competitions
{
    public class Converter
    {
        public string ConvertedFilaExtension;
        public string ConvertedFilePath;
        private string DeleteCharsFromEnd(string pathToTrimm, int charToDeleteCount)
        {
            int length = pathToTrimm.Count();
            return pathToTrimm.Remove(length-charToDeleteCount);
        }
        public string Convert(string sourcePath, string documentToCreate, int docToCreateType) // 0 doc97 // 1 docx // 2rtf // 3 pdf // 4 odt
        {
            string documentToCreatePathWithoutExtension = DeleteCharsFromEnd(documentToCreate,4);
            switch (docToCreateType)
            {
                case 0:
                {
                    ConvertedFilaExtension = "doc";
                    ConvertedFilePath = documentToCreatePathWithoutExtension + ".doc";
                    return ConvertDocumentTo(sourcePath, ConvertedFilePath,
                        WdSaveFormat.wdFormatDocument97);
                }
                case 1:
                {
                    ConvertedFilaExtension = "docx";
                    ConvertedFilePath = documentToCreatePathWithoutExtension + ".docx";
                    return ConvertDocumentTo(sourcePath, ConvertedFilePath,
                        WdSaveFormat.wdFormatDocument);
                }
                case 2:
                {
                    ConvertedFilaExtension = "rtf";
                    ConvertedFilePath = documentToCreatePathWithoutExtension + ".rtf";
                    return ConvertDocumentTo(sourcePath, ConvertedFilePath,
                        WdSaveFormat.wdFormatRTF);
                }
                case 3:
                {
                    ConvertedFilaExtension = "pdf";
                    ConvertedFilePath = documentToCreatePathWithoutExtension + ".pdf";
                    return ConvertDocumentTo(sourcePath, ConvertedFilePath,
                        WdSaveFormat.wdFormatPDF);
                }
                case 4:
                {
                    ConvertedFilaExtension = "odt";
                    ConvertedFilePath = documentToCreatePathWithoutExtension + ".odt";
                    return ConvertDocumentTo(sourcePath, ConvertedFilePath,
                        WdSaveFormat.wdFormatOpenDocumentText);
                }
            }
            return "error";
        }
        public string ConvertDocumentTo(string sourcePath, string documentToCreatePath, object fileFormat) // WdSaveFormat.wdFormatPDF;
        {
            Microsoft.Office.Interop.Word.Application msWordDoc = null;
            Microsoft.Office.Interop.Word.Document doc = null;

            object oMissing = System.Reflection.Missing.Value;

            msWordDoc = new Microsoft.Office.Interop.Word.Application
            {
                Visible = false,
                ScreenUpdating = false
            };

            doc = msWordDoc.Documents.Open(sourcePath, ref oMissing , ref oMissing, ref oMissing, ref oMissing, ref oMissing , ref oMissing, ref oMissing, ref oMissing, ref oMissing
            , ref oMissing, ref oMissing, ref oMissing, ref oMissing , ref oMissing, ref oMissing);

            try
            {        


                if (doc != null)
                {
                    doc.Activate();
                    doc.Protect(WdProtectionType.wdAllowOnlyReading, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                    //doc.ReadOnly = true;
                    // save Document as PDF
                    //object fileFormat = WdSaveFormat.wdFormatPDF;
                    doc.SaveAs(documentToCreatePath,
                    ref fileFormat, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception)
            {
                return "error";
                throw;
            }
             finally
            {
                if (doc != null)
                {
                    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                    doc.Close(ref saveChanges, ref oMissing, ref oMissing);
                    //Util.releaseObject(doc);
                }
                ((_Application)msWordDoc).Quit(ref oMissing, ref oMissing, ref oMissing);
                //Util.releaseObject(msWordDoc);
                msWordDoc = null;
            }
           return documentToCreatePath;
        }     
    }
}
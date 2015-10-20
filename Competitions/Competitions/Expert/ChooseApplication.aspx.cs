using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.IO.Compression;
 
using System.Net;

namespace Competitions.Expert
{
    public partial class Applications : System.Web.UI.Page
    {
        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userIdtmp = Session["UserID"];
            if (userIdtmp == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = (int)userIdtmp;
            if (!Page.IsPostBack)
            {
             CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
             List<zApplicationTable> applicationList = (from a in CompetitionsDataBase.zApplicationTable
                                                        where a.Active == true && a.Sended == true
                                                        join b in CompetitionsDataBase.zCompetitionsTable
                                                        on a.FK_CompetitionTable equals b.ID
                                                        where b.Active == true
                                                        join c in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                                                        on a.ID equals c.FK_ApplicationsTable
                                                        where c.Active == true && c.FK_UsersTable == userId
                                                        select a).Distinct().ToList();

             List<zApplicationTable> notreadyapp = new List<zApplicationTable>();
             foreach (zApplicationTable current in applicationList)
             {
                 List<zExpertPointsValue> notsended = (from a in CompetitionsDataBase.zExpertPointsValue
                                                       where a.Active == true && a.Sended == false
                                                       && a.FK_ApplicationTable == current.ID && a.FK_ExpertsTable == userId
                                                       select a).ToList();
                 if (notsended.Count == 0)
                 {
                     notreadyapp.Add(current);
                 }
                 else
                 {
                     continue;
                 }

             }
             
                DataTable dataTable = new  DataTable();

                dataTable.Columns.Add("ID", typeof (string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Description", typeof(string));
                dataTable.Columns.Add("Autor", typeof(string));
                dataTable.Columns.Add("Competition", typeof(string));

                foreach (zApplicationTable currentApplication in notreadyapp)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["Description"] = "";
                    dataRow["Competition"] = (from a in CompetitionsDataBase.zCompetitionsTable
                                                  where a.ID == currentApplication.FK_CompetitionTable
                                                  select  a.Name).FirstOrDefault();
                    dataRow["Autor"] = (from a in CompetitionsDataBase.UsersTable 
                                            where a.ID == currentApplication.FK_UsersTable
                                            select a.Email).FirstOrDefault();
                    dataTable.Rows.Add(dataRow);
                }
                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
        
        protected void GetApplicationButtonClick(object sender, EventArgs e)
        {
        Button button = (Button)sender;
            {
                var userIdtmp = Session["UserID"];
                if (userIdtmp == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int userId = (int)userIdtmp;
                var appid = button.CommandArgument;
                int idapp = Convert.ToInt32(appid);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                                                         join b in competitionDataBase.zApplicationTable
                                                         on a.ID equals b.FK_CompetitionTable
                                                         where b.ID == idapp select a).FirstOrDefault();
               
                string dirPath = Server.MapPath("~/documents/byApplication/" + idapp);
                string templateFilePath = Server.MapPath("~/documents/templates") + "\\" + currentCompetition.ID.ToString() + "\\" + currentCompetition.TemplateDocName;
                string newFileName = DateTime.Now.ToString() + " " + currentCompetition.TemplateDocName;
                newFileName = newFileName.Replace(":", "_"); 
                string newFilePath = dirPath + "\\" + newFileName;
                string zipFile = Server.MapPath("~/documents/generatedZipFiles/") + idapp + ".zip";
                string extractPath = Server.MapPath("~/documents/extract/");
                CreateXmlFile createXmlFile = new CreateXmlFile();
                string asdadasdda = System.Web.HttpContext.Current.Server.MapPath("~/") + @"documents\generatedZipFiles\" + idapp + ".zip";
                createXmlFile.CreateDocument(templateFilePath, newFilePath, idapp);
              
                if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/") + @"documents\generatedZipFiles\" + idapp + ".zip"))
                    {
                        File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/") + @"documents\generatedZipFiles\" + idapp + ".zip");
                        ZipFile.CreateFromDirectory(dirPath, zipFile);
                        HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=file.zip");
                        HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(zipFile));
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        ZipFile.CreateFromDirectory(dirPath, zipFile);
                        HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=file.zip");
                        HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(zipFile));
                        HttpContext.Current.Response.End();
                    }
                Response.End();
            }
        }

        


        protected void GetExpertPointButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var appIdTmp = Session["UserID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int userid = Convert.ToInt32(appIdTmp);
                int applicationId  = Convert.ToInt32(button.CommandArgument);


                string templateFilePath = Server.MapPath("~/documents/expertdoc/expertpoint.xml");
                string newFileName = DateTime.Now.ToString();
                newFileName = newFileName.Replace(":", "_");
                string newFileDirectory = Server.MapPath("~/documents/generated") + "\\" + userid.ToString();
                string newFilePath = newFileDirectory + "\\" + newFileName;

                Directory.CreateDirectory(newFileDirectory);

                return newNestedList;
            }
            public XmlNode GetXmlTable(XmlDocument document, TagToReplace currentTagToReplace, int applicationId)
            {
                XmlTableCreate xmlTableCreate = new XmlTableCreate();
                List<List<string>> newNestedList = GetNestedDataList(currentTagToReplace.ReplacemantList, applicationId);
                XmlNode newXmlTableNode = xmlTableCreate.GetXmlTable(document, newNestedList,false);
                return newXmlTableNode;
            }
        }
        protected void FillButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["ApplicationID"] = iD;
                Response.Redirect("ChooseApplicationAction.aspx");
            }
        }
        protected void SendButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                                                        where a.Active == true
                                                              && a.ID == iD
                                                        select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    currentApplication.Sended = true;
                    currentApplication.SendedDataTime = DateTime.Now;
                    competitionDataBase.SubmitChanges();
                }
            }
            Response.Redirect("UserMainPage.aspx");
        }
        private string FindValue(zColumnTable column, int applicationId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();


                HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=expertpoint.xml");
                HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(newFilePath));
                HttpContext.Current.Response.End();
                Response.End();
            }
        }
                    
        
    }
}
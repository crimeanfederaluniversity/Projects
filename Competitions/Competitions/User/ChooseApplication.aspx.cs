using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;


namespace Competitions.User
{
    public partial class ChooseApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userIdtmp = Session["UserID"];
            if (userIdtmp == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = (int) userIdtmp;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
             
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("FillNSendEnabled", typeof(bool)));

               List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                    where a.FK_UsersTable == userId
                          && a.Active == true
                          join b in competitionDataBase.zCompetitionsTable
                          on a.FK_CompetitionTable equals b.ID
                          where b.Active == true
                          && b.OpenForApplications == true
                    select a).Distinct().ToList();

               foreach (zApplicationTable currentApplication in applicationList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                        where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                        select a.Name).FirstOrDefault();
                    dataTable.Rows.Add(dataRow);

                    dataRow["FillNSendEnabled"] = !currentApplication.Sended;

                }
                /*DataSet newDataSet = new DataSet();
                newDataSet.Tables.Add(dataTable);
                string filePath = @"C:\1\1112.xml";
                newDataSet.WriteXml(filePath);
                */
                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }
        protected void NewApplication_Click(object sender, EventArgs e)
        {
            Session["ApplicationID"] = 0;
            Response.Redirect("ApplicationCreateEdit.aspx");
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
            Response.Redirect("ChooseApplication.aspx");
        }
        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        private string FindValue(zColumnTable column , int applicationId )
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();

            List<zCollectedDataTable> collectedDataList = (from a in competitionDataBase.zCollectedDataTable
                where a.FK_ColumnTable == column.ID
                      && a.Active == true
                join b in competitionDataBase.zCollectedRowsTable
                    on a.FK_CollectedRowsTable equals b.ID
                where b.Active == true
                      && b.FK_ApplicationTable == applicationId
                select a).Distinct().ToList();
            
            if (collectedDataList.Count == 1)
            {
                if (dataType.IsDataTypeText(column.DataType))
                {
                    return collectedDataList[0].ValueText;
                }
            }

            return "NULL";
        }
        private List<zColumnTable> FindColumnsWithUniqueMarkExist(int applicationId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zColumnTable> currentColumnList = (from a in  competitionDataBase.zColumnTables
                                 where a.Active == true
                                 && a.UniqueMark.Length>2
                                 join b in competitionDataBase.zSectionTable
                                 on a.FK_SectionTable equals  b.ID
                                 where b.Active == true
                                 join c in competitionDataBase.zApplicationTable
                                 on b.FK_CompetitionsTable equals c.FK_CompetitionTable
                                 where c.Active == true
                                 && c.ID == applicationId
                                 select a).Distinct().ToList();
            
            return currentColumnList;
        }
        private bool CreateDocument(string templatePath, string newFilePath , int applicationId)
        {
            string xmlFile =  File.ReadAllText(templatePath);
            XmlDocument document = new XmlDocument();
            List<zColumnTable> columnListWithUniqueMark = FindColumnsWithUniqueMarkExist(applicationId);
            foreach (zColumnTable currentColumn in columnListWithUniqueMark)
            {
                xmlFile = xmlFile.Replace(currentColumn.UniqueMark, FindValue(currentColumn, applicationId));
            }
            document.LoadXml(xmlFile);
            document.Save(newFilePath);
             
            //File.WriteAllText(newFilePath, xmlFile);
            return true;
        }
        protected void GetDocButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                
                zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                                                         join b in competitionDataBase.zApplicationTable
                                                         on a.ID equals b.FK_CompetitionTable
                    where b.ID == iD
                    select a).FirstOrDefault();
                if (currentCompetition != null)
                {
                    var userIdtmp = Session["UserID"];
                    if (userIdtmp == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    int userId = (int)userIdtmp;

                    if (currentCompetition.TemplateDocName!=null)
                    {
                        if (currentCompetition.TemplateDocName.Any())
                        {
                            string templateFilePath = Server.MapPath("~/documents/templates") + "\\" + currentCompetition.ID.ToString() + "\\" + currentCompetition.TemplateDocName;
                            string newFileName = DateTime.Now.ToString() +" "+ currentCompetition.TemplateDocName;
                            newFileName = newFileName.Replace(":", "_");
                            string newFileDirectory = Server.MapPath("~/documents/generated") + "\\" + userId.ToString();
                            string newFilePath =  newFileDirectory + "\\" + newFileName;

                            Directory.CreateDirectory(newFileDirectory);
                            CreateDocument(templateFilePath, newFilePath, iD);
                                                      
                            HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + currentCompetition.TemplateDocName);
                            HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(newFilePath));
                            HttpContext.Current.Response.End();
                            Response.End();
                        }
                    }
                }
            }
        }
    }
}

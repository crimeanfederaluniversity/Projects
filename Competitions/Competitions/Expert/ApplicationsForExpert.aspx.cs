using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;

namespace Competitions.Expert
{
    public partial class ApplicationsForExpert : System.Web.UI.Page
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
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                                 
                List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                           where a.Active == true && a.Sended == true
                                                           join b in competitionDataBase.zCompetitionsTable
                                                           on a.FK_CompetitionTable equals b.ID
                                                           where b.Active == true
                                                           join c in competitionDataBase.zExpertsAndApplicationMappingTable
                                                           on a.ID equals c.FK_ApplicationsTable
                                                           where c.Active == true && c.FK_UsersTable == userId                                                                                                                                                                
                                                           select a).Distinct().ToList();
              
                List<zApplicationTable> notreadyapp = new List<zApplicationTable>();
                foreach (zApplicationTable current in applicationList)
                {
                    List<zExpertPointsValue> notsended = (from a in competitionDataBase.zExpertPointsValue
                                                          where a.Active == true && a.Sended == false
                                                          && a.FK_ApplicationTable == current.ID && a.FK_ExpertsTable == userId                                                          
                                                          select a).ToList();
                    if (notsended.Count == 6)
                    {
                        notreadyapp.Add(current);
                    }                 

                }


                foreach (zApplicationTable currentApplication in notreadyapp)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                                                  where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                                                  select a.Name).FirstOrDefault();
                    
                    dataTable.Rows.Add(dataRow);
                     
                }

                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }
        protected void GetDocButtonClick(object sender, EventArgs e)
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
                                                         where b.ID == idapp
                                                         select a).FirstOrDefault();

                string dirPath = Server.MapPath("~/documents/byApplication/" + idapp);
                string templateFilePath = Server.MapPath("~/documents/templates") + "\\" + currentCompetition.ID.ToString() + "\\" + currentCompetition.TemplateDocName;
                string newFileName = "Заявка " + currentCompetition.TemplateDocName;
                newFileName = newFileName.Replace(":", "_");
                string newFilePath = dirPath + "\\" + newFileName;
                string zipFile = Server.MapPath("~/documents/generatedZipFiles/") + idapp + ".zip";
                string extractPath = Server.MapPath("~/documents/extract/");
                CreateXmlFile createXmlFile = new CreateXmlFile();
                string asdadasdda = System.Web.HttpContext.Current.Server.MapPath("~/") + @"documents\generatedZipFiles\" + idapp + ".zip";
                createXmlFile.CreateDocument(templateFilePath, newFilePath, idapp,0);

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
        protected void EvaluateButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            {
                Session["ApplicationID"] = button.CommandArgument;
                Response.Redirect("EvaluateApplication.aspx");
            }
        }
     
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
        
    }
}
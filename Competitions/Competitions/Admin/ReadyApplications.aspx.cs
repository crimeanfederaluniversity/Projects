using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.IO.Compression;

namespace Competitions.Admin
{
    public partial class ReadyApplications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zApplicationTable> applicationsList = (from a in CompetitionsDataBase.zApplicationTable
                                                            where a.Active == true && a.Accept == true
                                                            select a).ToList();
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("ID", typeof(string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Description", typeof(string));
                dataTable.Columns.Add("Autor", typeof(string));
                dataTable.Columns.Add("Competition", typeof(string));
                dataTable.Columns.Add("Experts", typeof(string));
                dataTable.Columns.Add("SendedDataTime", typeof(string));



                foreach (zApplicationTable currentApplication in applicationsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["Description"] = "";
                    dataRow["SendedDataTime"] = currentApplication.SendedDataTime.ToString().Split(' ')[0]; ;
                    dataRow["Competition"] = (from a in CompetitionsDataBase.zCompetitionsTable
                                              where a.ID == currentApplication.FK_CompetitionTable
                                              select a.Name).FirstOrDefault();
                    dataRow["Autor"] = (from a in CompetitionsDataBase.UsersTable
                                        where a.ID == currentApplication.FK_UsersTable
                                        select a.Email).FirstOrDefault();
                    List<UsersTable> expertsList = (from a in CompetitionsDataBase.UsersTable
                                                    join b in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                                                        on a.ID equals b.FK_UsersTable
                                                    where a.AccessLevel == 5
                                                          && a.Active == true
                                                          && b.FK_ApplicationsTable == currentApplication.ID
                                                          && b.Active == true
                                                    select a).ToList();
                    string expertNamesList = "";

                    foreach (UsersTable currentExpert in expertsList)
                    {
                        expertNamesList += currentExpert.Email + " \n";

                    }
                    dataRow["Experts"] = expertNamesList;
                    dataTable.Rows.Add(dataRow);
                }
                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
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
        protected void ApplicationButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
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
                string newFileName = "Заявка " + currentCompetition.TemplateDocName + ".doc";
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
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Заявка №" + idapp.ToString() + ".zip");
                    HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(zipFile));
                    HttpContext.Current.Response.End();
                }
                else
                {
                    ZipFile.CreateFromDirectory(dirPath, zipFile);
                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Заявка №" + idapp.ToString() + ".zip");
                    HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(zipFile));
                    HttpContext.Current.Response.End();
                }
                Response.End();
            }
        }
        protected void ExpertPointButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                Session["ApplicationID"] = button.CommandArgument;
                Response.Redirect("ExpertPointPage.aspx");
            }
        }

        protected void BackButtonClick(object sender, EventArgs e)
        { 
            Button button = (Button) sender;
            if (button != null)
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zApplicationTable currentapplication = (from a in CompetitionsDataBase.zApplicationTable
                                                            where a.Active == true && a.Accept == true && a.ID == Convert.ToInt32(button.CommandArgument)
                                                            select a).FirstOrDefault();
                if (currentapplication != null)
                {CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                     List<zExpertPoints> expertPointsList = (from a in competitionDataBase.zExpertPoints
                        where a.Active == true && a.ID != 6
                        select a).ToList();

                    List<zExpertsAndApplicationMappingTable> allexperts =
                        (from a in competitionDataBase.zExpertsAndApplicationMappingTable
                            where a.Active == true &&
                                  a.FK_ApplicationsTable == iD
                            select a).ToList();
                    foreach (zExpertsAndApplicationMappingTable currentExpert in allexperts)
                    {
                        zExpertPointsValue currentExpertPointComment = (from a in competitionDataBase.zExpertPointsValue
                            where a.FK_ApplicationTable == iD
                                  && a.FK_ExpertsTable == currentExpert.FK_UsersTable
                                  && a.FK_ExpertPoints == 6
                            select a).FirstOrDefault();
                        if (currentExpertPointComment != null)
                        {
                            if (currentExpertPointComment.Active == true)
                            {
                                currentExpertPointComment.Active = false;
                                competitionDataBase.SubmitChanges();
                            }
                        }
                        foreach (zExpertPoints currentExpertPoint in expertPointsList)
                            {
                                zExpertPointsValue currentExpertPointValue =
                                    (from a in competitionDataBase.zExpertPointsValue
                                        where
                                            a.FK_ApplicationTable == iD
                                            && a.FK_ExpertsTable == currentExpert.FK_UsersTable
                                            && a.FK_ExpertPoints == currentExpertPoint.ID
                                        select a).FirstOrDefault();
                                if (currentExpertPointValue != null)
                                { 
                                    if (currentExpertPointValue.Active == true)
                                    {
                                        currentExpertPointValue.Active = false;
                                        competitionDataBase.SubmitChanges();
                                    }
                                }

                            }
                        }                   
                    currentapplication.Accept = false;
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("ReadyApplications.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
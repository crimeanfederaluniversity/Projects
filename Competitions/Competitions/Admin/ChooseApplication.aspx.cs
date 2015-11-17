using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using Microsoft.Office.Interop.Word;

namespace Competitions.Admin
{
    public partial class Applications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zApplicationTable> applicationsList = (from a in CompetitionsDataBase.zApplicationTable
                    where a.Active == true && a.Accept == false   
                    join b in  CompetitionsDataBase.zCompetitionsTable
                    on a.FK_CompetitionTable equals b.ID
                    where b.Active == true
                    select a).ToList();
                

                if (applicationsList != null)
                {
                
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("ID", typeof(string));
                    dataTable.Columns.Add("Name", typeof(string));
                    dataTable.Columns.Add("Email", typeof(string));
                    dataTable.Columns.Add("Autor", typeof(string));
                    dataTable.Columns.Add("Competition", typeof(string));
      
                    dataTable.Columns.Add("SendedDataTime", typeof(string));
                  

                
                foreach (zApplicationTable currentApplication in applicationsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["Email"] = (from a in CompetitionsDataBase.UsersTable
                                        where a.ID == currentApplication.FK_UsersTable
                                        select a.Email).FirstOrDefault();
                
                    if (currentApplication.Sended == false)                   
                    dataRow["SendedDataTime"] = "Заявка в процессе заполнения";                   
                    else
                    dataRow["SendedDataTime"] = currentApplication.SendedDataTime.ToString().Split(' ')[0];                                 
                    dataRow["Competition"] = (from a in CompetitionsDataBase.zCompetitionsTable
                                                  where a.ID == currentApplication.FK_CompetitionTable 
                                                  select a.Name).FirstOrDefault();
 
                        dataTable.Rows.Add(dataRow);
                    }
                    ApplicationGV.DataSource = dataTable;
                    ApplicationGV.DataBind();
                }
            }
            else
            {
                Label1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            Button exp = (Button) e.Row.FindControl("ExpertChangeButton");
            Button accept = (Button) e.Row.FindControl("AcceptButton");
            Button back = (Button)e.Row.FindControl("BackToUserButton");
            if ( exp != null && accept != null)
            {
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zApplicationTable> applicationsList = (from a in CompetitionsDataBase.zApplicationTable
                                                            where a.Active == true && a.Accept == false && a.ID == Convert.ToInt32(accept.CommandArgument)
                                                            join b in CompetitionsDataBase.zCompetitionsTable
                                                            on a.FK_CompetitionTable equals b.ID
                                                            where b.Active == true
                                                            select a).ToList();
               
                 
                    foreach (var n in applicationsList)
                    {
                      
                        if (n.Sended == false)
                        {
                            
                            exp.Enabled = false;
                            accept.Enabled = false;
                            back.Enabled = false;
                        }
                        else
                        {
                            
                            exp.Enabled = true;
                            accept.Enabled = true;
                            back.Enabled = true;
                        }
                    }
                }
            }     

        protected void ExpertChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            if (button != null)
            {
                Session["ApplicationID"] = button.CommandArgument;
                Response.Redirect("ApplicationExpertsAndExpertsGroupEdit.aspx");
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
                string newFileName =  "Заявка " + currentCompetition.TemplateDocName;
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

        protected void AcceptButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            if (button != null)
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                    where a.Active == true && a.Accept == false
                          && a.ID == iD && a.Sended == true
                    select a).FirstOrDefault();
                if (currentApplication != null)
                {
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
                        if (currentExpertPointComment == null)
                        {
                            zExpertPointsValue expertcoment = new zExpertPointsValue();
                            expertcoment.Active = true;
                            expertcoment.FK_ApplicationTable = currentApplication.ID;
                            expertcoment.FK_ExpertsTable = currentExpert.FK_UsersTable;
                            expertcoment.FK_ExpertPoints = 6;
                            expertcoment.Sended = false;
                            competitionDataBase.zExpertPointsValue.InsertOnSubmit(expertcoment);
                            competitionDataBase.SubmitChanges();
                        }
                        else
                        {
                            if (currentExpertPointComment.Active == false)
                            {
                                currentExpertPointComment.Active = true;
                                competitionDataBase.SubmitChanges();
                            }
                        }

                        foreach (zExpertPoints currentExpertPoint in expertPointsList)
                        {
                            zExpertPointsValue currentExpertPointValue =  (from a in competitionDataBase.zExpertPointsValue
                                    where
                                        a.FK_ApplicationTable == iD
                                        && a.FK_ExpertsTable == currentExpert.FK_UsersTable
                                        && a.FK_ExpertPoints == currentExpertPoint.ID
                                    select a).FirstOrDefault();
                            if (currentExpertPointValue == null)
                            {
                                zExpertPointsValue expertpoints = new zExpertPointsValue();
                                expertpoints.Active = true;
                                expertpoints.FK_ApplicationTable = currentApplication.ID;
                                expertpoints.FK_ExpertsTable = currentExpert.FK_UsersTable;
                                expertpoints.FK_ExpertPoints = currentExpertPoint.ID;
                                expertpoints.Sended = false;
                                competitionDataBase.zExpertPointsValue.InsertOnSubmit(expertpoints);
                                competitionDataBase.SubmitChanges();
                            }
                            else
                            {
                                if (currentExpertPointValue.Active == false)
                                {
                                    currentExpertPointValue.Active = true;
                                    competitionDataBase.SubmitChanges();
                                }
                            }

                        }
                    }
                
                currentApplication.Accept = true;
                competitionDataBase.SubmitChanges();
                    }
                
                Response.Redirect("ChooseApplication.aspx");
            }
        }

        protected void BackToUserButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            if (button != null)
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                    where a.Active == true && a.ID == iD && a.Sended == true
                    select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    currentApplication.Sended = false;
                    competitionDataBase.SubmitChanges();
                }
             
            }
            Response.Redirect("ChooseApplication.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
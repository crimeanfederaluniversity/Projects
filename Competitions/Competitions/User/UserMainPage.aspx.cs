using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;

namespace Competitions.User
{


    public partial class UserMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var userIdtmp = Session["UserID"];
                    if (userIdtmp == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    int userId = (int) userIdtmp;

                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;

                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Number", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Budjet", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("StartDate", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("EndDate", typeof (string)));
                    
                    List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                        where a.Active == true
                        select a).ToList();

                    foreach (zCompetitionsTable currentCompetition in competitionsList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentCompetition.ID;
                        dataRow["Name"] = currentCompetition.Name;
                        dataRow["Number"] = currentCompetition.Number;
                        dataRow["Budjet"] = Convert.ToInt32(currentCompetition.Budjet);
                        dataRow["StartDate"] = currentCompetition.StartDate.ToString().Split(' ')[0];
                        dataRow["EndDate"] = currentCompetition.EndDate.ToString().Split(' ')[0];

                        dataTable.Rows.Add(dataRow);
                    }
                    MainGV.DataSource = dataTable;
                    MainGV.DataBind();
                }
                ////////////////////////////////////////////////////////////////////////////////////////
                {                  
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof (string)));

                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId && a.Sended == false  && a.Active == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID where b.Active == true
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
                    }
                    ApplicationGV.DataSource = dataTable;
                    ApplicationGV.DataBind();
                }
                //////////////////////////////////////////////////////////
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("SendedDate", typeof(string)));
                    
                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId  && a.Active == true && a.Sended == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID
                                                               where b.Active == true && b.OpenForApplications == true
                                                               select a).Distinct().ToList();

                    foreach (zApplicationTable currentApplication in applicationList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentApplication.ID;
                        dataRow["Name"] = currentApplication.Name;
                        dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                                                      where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                                                      select a.Name).FirstOrDefault();
                        if (currentApplication.SendedDataTime == null)
                        {
                            dataRow["SendedDate"] = "Не отправлялось на рассмотрение";
                        }
                        else
                        {
                            dataRow["SendedDate"] = currentApplication.SendedDataTime.ToString().Split(' ')[0];
                        }          
                        dataTable.Rows.Add(dataRow);
                    }
                    ArchiveApplicationGV.DataSource = dataTable;
                    ArchiveApplicationGV.DataBind(); 
                }
            }
        }
        protected void MyApplication_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ChooseApplication.aspx");
        }
        protected void NewApplication_Click1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (CompetitionDataContext newBid = new CompetitionDataContext())
                {
                    Session["ID_Konkurs"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/User/ApplicationCreateEdit.aspx");
                }
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
            Response.Redirect("UserMainPage.aspx");
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
                            CreateXmlFile createXmlFile = new CreateXmlFile();
                            createXmlFile.CreateDocument(templateFilePath, newFilePath, iD);
                                                      
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
        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }
        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }
        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
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
                int userId = (int)userIdtmp;
                
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                CompetitionCountDown competitionCountDown = new CompetitionCountDown();

                
                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                #region competitions
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Number", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Budjet", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));

                    List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                                                                 where a.Active == true && a.OpenForApplications == true
                                                                 select a).ToList();
                   
                    foreach (zCompetitionsTable currentCompetition in competitionsList)
                    {
                        if (competitionCountDown.IsCompetitionEndDateExpired(currentCompetition.ID))
                            continue;

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
                #endregion

                /// в  первую таблицу все те у которых конкурс еще открыт а заявка не отправлена
                /// во вторую все те у которых заявка отправлена
                /// в  третью все те у которых закрыта заявка

                

                #region currentApplications
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("StatusLabelEnabled", typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("SendButtonEnabled", typeof(bool)));
                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId && a.Sended == false && a.Active == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID
                                                               where b.Active == true
                                                                   && b.OpenForApplications == true
                                                               select a).Distinct().ToList();

                    Status status = new Status();

                    foreach (zApplicationTable currentApplication in applicationList)
                    {
                        if (competitionCountDown.IsCompetitionEndDateExpiredByApplication(currentApplication.ID))
                            continue;

                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentApplication.ID;
                        dataRow["Name"] = currentApplication.Name;
                        dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                                                      where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                                                      select a.Name).FirstOrDefault();
                        if (status.IsApplicationReadyToSend(currentApplication.ID))
                        {
                            dataRow["StatusLabelEnabled"] = false;
                             dataRow["SendButtonEnabled"] = true;
                        }
                        else
                        {
                            dataRow["StatusLabelEnabled"] = true;
                            dataRow["SendButtonEnabled"] = false;
                        }                  
                        dataTable.Rows.Add(dataRow);
                    }
                    ApplicationGV.DataSource = dataTable;
                    ApplicationGV.DataBind();
                }
                #endregion
                #region applicationsArchive
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("SendedDate", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Accept", typeof(string)));
                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId && a.Active == true && a.Sended == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID
                                                               where b.Active == true
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
                        zApplicationTable accept = (from a in competitionDataBase.zApplicationTable
                                           where a.ID == (Convert.ToInt32(currentApplication.ID))
                                           select a).FirstOrDefault();
                        if (accept.Accept == true)
                            
                            dataRow["Accept"] = "Принята";
                        else
                            dataRow["Accept"] = "На рассмотрении";
                        
                        dataTable.Rows.Add(dataRow);
                    }
                    ArchiveApplicationGV.DataSource = dataTable;
                    ArchiveApplicationGV.DataBind();
                }
                #endregion
                #region draftApplications
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));

                   

                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId && a.Active == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID
                                                               where b.Active == true 
                                                               select a).Distinct().ToList();


                    foreach (zApplicationTable currentApplication in applicationList)
                    {
                        if (!competitionCountDown.IsCompetitionEndDateExpiredByApplication(currentApplication.ID))
                            continue;
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentApplication.ID;
                        dataRow["Name"] = currentApplication.Name;
                        dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                                                      where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                                                      select a.Name).FirstOrDefault();
                        dataTable.Rows.Add(dataRow);
                    }
                    DraftGridView.DataSource = dataTable;
                    DraftGridView.DataBind();
                }
                #endregion
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var userIdtmp = Session["UserID"];
            if (userIdtmp == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = (int) userIdtmp;

            Button newapp = (Button) e.Row.FindControl("NewApplication");
            if (newapp != null)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                List<zApplicationTable> applicationexist = (from a in competitionDataBase.zApplicationTable
                    where
                        a.FK_UsersTable == userId && a.Sended == true && a.Active == true &&
                        a.EndProjectDate > DateTime.Now
                    select a).Distinct().ToList();

                List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                    where
                        a.Active == true && a.OpenForApplications == true &&
                        a.ID == Convert.ToInt32(newapp.CommandArgument)
                    select a).ToList();

                if (applicationexist.Count != null)
                {

                    newapp.Enabled = false;

                    Label1.Visible = true;
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
                    Session["ApplicationID"] = 0;
                    Session["ID_Konkurs"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/User/ApplicationCreateEdit.aspx");
                }
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

                    if (currentCompetition.TemplateDocName != null)
                    {
                        if (currentCompetition.TemplateDocName.Any())
                        {
                            string templateFilePath = Server.MapPath("~/documents/templates") + "\\" + currentCompetition.ID.ToString() + "\\" + currentCompetition.TemplateDocName;
                            string newFileName = "Заявка пользователя на конкурс " + currentCompetition.TemplateDocName;
                            newFileName = newFileName.Replace(":", "_");
                            string newFileDirectory = Server.MapPath("~/documents/generated") + "\\" + iD.ToString();
                            string newFilePath = newFileDirectory + "\\" + newFileName;


                            Directory.CreateDirectory(newFileDirectory);
                            CreateXmlFile createXmlFile = new CreateXmlFile();
                            int docType = 0;
                           
                            try
                            {
                                 RadioButtonList newRadioButtonList =
                                button.Parent.FindControl("RadioButtonList1") as RadioButtonList;
                                docType = Convert.ToInt32(newRadioButtonList.SelectedValue);
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                            createXmlFile.CreateDocument(templateFilePath, newFilePath, iD, docType);
                            string convertedFilePath = createXmlFile.ConvertedFilePath;
                            
                            HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=document." + createXmlFile.ConvertedFileExtension);
                            HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(convertedFilePath));
                            HttpContext.Current.Response.End();                           
                            Response.End();      
                             
                          /*  System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                            response.ClearContent();
                            response.Clear();
                            response.ContentType = "text/plain";
                            response.AddHeader("Content-Disposition", "attachment; filename=document." + createXmlFile.ConvertedFileExtension);
                            response.TransmitFile(convertedFilePath);
                            response.Flush();
                            response.Clear();
                            response.Redirect("~/User/UserMainPage.aspx");
                            response.End();

                            */
                            /*
                            Response.WriteFile(convertedFilePath);
                            Response.Redirect("~/User/UserMainPage.aspx");*/
                        }
                    }
                }
            }
            //Response.Redirect("UserMainPage.aspx");
        }
        #region tabClick
        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }
        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }
        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 2;
        }
        protected void Tab4_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Clicked";
            MainView.ActiveViewIndex = 3;
        }
        #endregion
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
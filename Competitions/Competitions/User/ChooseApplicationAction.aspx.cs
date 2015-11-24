using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace Competitions.User
{
    public partial class ChooseApplicationAction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
       
            if (!Page.IsPostBack)
            {
                var sessionParam1 = Session["ApplicationID"];

                if ((sessionParam1 == null))
                {
                    Response.Redirect("ChooseApplication.aspx");
                }

                int applicationId = Convert.ToInt32(sessionParam1);
                
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                    where a.ID == applicationId
                    select a).FirstOrDefault();
                zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                    where a.ID == currentApplication.FK_CompetitionTable
                    select a).FirstOrDefault();

                CompetitionCountDown competitionCountDown = new CompetitionCountDown();
                CountDownLabel.Text =
                    competitionCountDown.GetDaysBeforeCompetitionEndMessage(currentCompetition.ID);

                Label1.Text = currentApplication.Name;
                Label2.Text = currentCompetition.Name;

                if (currentApplication.StartProjectDate != null)
                {
                    DateTime dateTime = (DateTime)currentApplication.StartProjectDate;
                    string tmpStr = dateTime.ToString().Split(' ')[0];
                    string[] tmpArray = tmpStr.Split('.');
                    string tmp2 = tmpArray[2] + "-" + tmpArray[1] + "-" + tmpArray[0];
                    startdata.Value = tmp2;
                }
                if (currentApplication.EndProjectDate != null)
                {
                    DateTime dateTime = (DateTime)currentApplication.EndProjectDate;
                    string tmpStr = dateTime.ToString().Split(' ')[0];
                    string[] tmpArray = tmpStr.Split('.');
                    string tmp2 = tmpArray[2] + "-" + tmpArray[1] + "-" + tmpArray[0];
                    enddata.Value = tmp2;
                }
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CreateDate", typeof (string)));

                List<zDocumentsTable> documentsList = (from a in competitionDataBase.zDocumentsTable
                    where a.FK_ApplicationTable == applicationId
                          && a.Active == true
                    select a).ToList();

                foreach (zDocumentsTable currentDocument in documentsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentDocument.ID;
                    dataRow["Name"] = currentDocument.Name;
                    if (currentDocument.Name == null)
                    {
                        dataRow["Name"] = "ссылка";
                        if (currentDocument.LinkOut != null)
                        {
                            string linkOutString = currentDocument.LinkOut;
                            if (linkOutString.Length > 100)
                            {
                                linkOutString = linkOutString.Remove(100) + "...";
                                dataRow["Name"] = linkOutString;
                            }
                            else
                            {
                                dataRow["Name"] = currentDocument.LinkOut;
                            }
                            
                        }
                       
                    }                    
                    dataRow["CreateDate"] = currentDocument.AddDateTime.ToString().Split(' ')[0];
                    dataTable.Rows.Add(dataRow);
                }

                DocumentsGV.DataSource = dataTable;
                DocumentsGV.DataBind();


                List <zBlockTable> currentBlock = (from a in competitionDataBase.zBlockTable
                                                        where a.Active == true
                                                        select a).ToList();
               
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable2.Columns.Add(new DataColumn("BlockName", typeof(string)));
                dataTable2.Columns.Add(new DataColumn("Status", typeof(string)));
                dataTable2.Columns.Add(new DataColumn("EnableButton", typeof(bool)));
                Status status = new Status();
                int statusHistory = 2;
                foreach (zBlockTable current  in currentBlock)
                {
                    DataRow dataRow2 = dataTable2.NewRow();
                    dataRow2["ID"] = current.ID;
                    dataRow2["BlockName"] = current.BlockName;
                    int blockStatus = status.GetStatusIdForBlockInApplication(current.ID, currentApplication.ID);
                    dataRow2["Status"] = status.GetStatusNameByStatusId(blockStatus);
                    dataRow2["EnableButton"] = false;
                    if (status.IsDataReady(statusHistory))
                    {
                        dataRow2["EnableButton"] = true;
                    }
                    statusHistory = blockStatus;
                    dataTable2.Rows.Add(dataRow2);
                }

                BlockGV.DataSource = dataTable2;
                BlockGV.DataBind();

                List<zPartnersTable> partners = (from a in competitionDataBase.zPartnersTable
                    where a.Active == true
                    join b in competitionDataBase.zApplicationAndPartnersMappingTable
                        on a.ID equals b.FK_PartnersTable
                    where b.Active == true && b.FK_Application == applicationId
                    select a).ToList();
                DataTable dataTable3 = new DataTable();
                dataTable3.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable3.Columns.Add(new DataColumn("Surname", typeof(string)));
                dataTable3.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable3.Columns.Add(new DataColumn("Patronymic", typeof(string)));
                dataTable3.Columns.Add(new DataColumn("Role", typeof(bool)));
                foreach (zPartnersTable current in partners)
                {
                    DataRow dataRow3 = dataTable3.NewRow();
                    dataRow3["ID"] = current.ID;
                    dataRow3["Surname"] = current.Surname;
                    dataRow3["Name"] = current.Name;
                    dataRow3["Patronymic"] = current.Patronymic;
                    dataRow3["Role"] = false;
                    if (current.Role == null && current.Role == false)
                    {
                        dataRow3["Role"] = false;
                    }
                    if (current.Role == true)
                    {
                        dataRow3["Role"] = true;
                    }
                   
                    dataTable3.Rows.Add(dataRow3);
                }
                PartnersGV.DataSource = dataTable3;
                PartnersGV.DataBind();
            }
        }

        public void SaveChanges()
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            
            foreach (GridViewRow currentRow in PartnersGV.Rows)
            {   
                Label partnerID = (Label) currentRow.FindControl("ID");
                TextBox surname = (TextBox) currentRow.FindControl("Surname");
                TextBox name = (TextBox) currentRow.FindControl("Name");
                TextBox patronymic = (TextBox) currentRow.FindControl("Patronymic");
                CheckBox role = (CheckBox) currentRow.FindControl("Role");

                if (partnerID != null)
                {
                    zPartnersTable currentpartner = (from a in competitionDataBase.zPartnersTable
                        where a.ID == Convert.ToInt32(partnerID.Text)
                        select a).FirstOrDefault();
                    if (currentpartner != null)
                    {
                        if (surname.Text.Any() && name.Text.Any() && patronymic.Text.Any())
                        {
                            currentpartner.Surname = surname.Text;
                            currentpartner.Name = name.Text;
                            currentpartner.Patronymic = patronymic.Text;
                            currentpartner.Role = role.Checked;
                            competitionDataBase.SubmitChanges();
                        }
                    }

                }
            }
        }

        protected void SaveDates()
        {
            var sessionParam1 = Session["ApplicationID"];

            if ((sessionParam1 == null))
            {
                Response.Redirect("ChooseApplication.aspx");
            }
            int applicationId = Convert.ToInt32(sessionParam1);

            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                                                    where a.ID == applicationId
                                                    select a).FirstOrDefault();
            if (currentApplication != null)
            {
                var start = Request["ctl00$MainContent$startdata"];
                var end = Request["ctl00$MainContent$enddata"];
                try
                {
                    currentApplication.StartProjectDate = Convert.ToDateTime(start).Date;
                }
                catch
                {
                }


                try
                {
                    currentApplication.EndProjectDate = Convert.ToDateTime(end).Date;
                }
                catch
                {
                }
                
                competitionDataBase.SubmitChanges();
            }
            
        }
        protected void FillButtonClick(object sender, EventArgs e)
        {
            SaveDates();
            Button button = (Button)sender;
            {
                Session["BlockID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("ChooseSection.aspx");
            }
        }
        protected void NewDocument(int applicationId, string fileName)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext(); 
            zDocumentsTable newDocument = new zDocumentsTable();
            newDocument.Active = true;
            newDocument.FK_ApplicationTable = applicationId;
            newDocument.Name = fileName;
            newDocument.LinkOut = null;
            newDocument.AddDateTime = DateTime.Now;
            competitionDataBase.zDocumentsTable.InsertOnSubmit(newDocument);
            competitionDataBase.SubmitChanges();
        }
        protected void NewLink(int applicationId, string linkOut)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zDocumentsTable newDocument = new zDocumentsTable();
            newDocument.Active = true;
            newDocument.FK_ApplicationTable = applicationId;
            newDocument.Name = null;
            newDocument.LinkOut = linkOut;
            newDocument.AddDateTime = DateTime.Now;
            competitionDataBase.zDocumentsTable.InsertOnSubmit(newDocument);
            competitionDataBase.SubmitChanges();
        }
        private void AddDoc()
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();                   
            var sessionParam1 = Session["ApplicationID"];       
            if ((sessionParam1 == null))
            {
                Response.Redirect("ChooseApplication.aspx");
            }

            int applicationId = Convert.ToInt32(sessionParam1);

            List<zDocumentsTable> documentsList = (from a in competitionDataBase.zDocumentsTable
                                                   where a.FK_ApplicationTable == applicationId
                                                         && a.Active == true
                                                   select a).ToList();

            String path = Server.MapPath("~/documents/byApplication");
            Directory.CreateDirectory(path + "\\\\" + applicationId.ToString());
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.PostedFiles.Count + documentsList.Count() > 5)
                {
                    ToManyFilesLabelError.Visible = true;
                    //Response.End();
                }

                else
                {
                    if (FileUpload1.PostedFiles.Count > 1)
                    {
                        int AttachOK = 0;
                        int AttachError = 0;
                        string DBLinkCombine = "";
                        foreach (var file in FileUpload1.PostedFiles)
                        {
                            try
                            {
                                file.SaveAs(path + "\\\\" + applicationId.ToString() + "\\\\" + file.FileName);
                                NewDocument(applicationId, file.FileName);
                                AttachOK++;
                            }
                            catch (Exception ex)
                            {
                                AttachError++;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            FileUpload1.PostedFile.SaveAs(path + "\\\\" + applicationId.ToString() + "\\\\" +
                                                          FileUpload1.FileName);
                            NewDocument(applicationId, FileUpload1.FileName);
                            // FileStatusLabel.Text = "Файл загружен!";
                        }
                        catch (Exception ex)
                        {
                            //  FileStatusLabel.Text = "Не удалось загрузить файл.";
                        }
                    }
                }
            }
            
        }
        private void ConnectLink()
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam1 = Session["ApplicationID"];
            if ((sessionParam1 == null))
            {
                Response.Redirect("ChooseApplication.aspx");
            }
            int applicationId = Convert.ToInt32(sessionParam1);
            if (LinkToFileTextBox.Text.Any())
                if (LinkToFileTextBox.Text.Length>5)
                    NewLink(applicationId, LinkToFileTextBox.Text);
        }
        protected void AddDocumentsButton_Click(object sender, EventArgs e)
        {
            SaveDates();
            AddDoc();
            ConnectLink();
            Response.Redirect("ChooseApplicationAction.aspx");
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
        protected void OpenButtonClick(object sender, EventArgs e)
        {
            SaveDates();
            Button button = (Button) sender;
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zDocumentsTable currentDocument = (from a in competitionDataBase.zDocumentsTable
                    where a.ID == Convert.ToInt32(button.CommandArgument)
                    select a).FirstOrDefault();
                if (currentDocument != null)
                {
                    if (currentDocument.LinkOut!=null)
                    {
                        Response.Redirect(currentDocument.LinkOut);
                    }
                    else if (currentDocument.Name!=null)
                    {
                        String path = Server.MapPath("~/documents/byApplication") + "\\\\" +
                                      currentDocument.FK_ApplicationTable.ToString() + "\\\\" + currentDocument.Name;
                        //  Response.Redirect(,false);

                        HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                        HttpContext.Current.Response.AppendHeader("Content-Disposition",
                            "attachment; filename=" + currentDocument.Name);
                        HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(path));
                        HttpContext.Current.Response.End();

                        Response.End();
                    }
                }
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            SaveDates();
            Button button = (Button)sender;
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zDocumentsTable currentDocument = (from a in competitionDataBase.zDocumentsTable
                                                   where a.ID == Convert.ToInt32(button.CommandArgument)
                                                   select a).FirstOrDefault();
                if (currentDocument != null)
                {
                    currentDocument.Active = false;
                    competitionDataBase.SubmitChanges();
                }
                Response.Redirect("ChooseApplicationAction.aspx");
            }
        }
        protected void GoBackButton_Click(object sender, ImageClickEventArgs e)
        {
            SaveDates();
            Response.Redirect("UserMainPage.aspx");
        }
        protected void Button2_Click(object sender, ImageClickEventArgs e)
        {
            SaveDates();
            Response.Redirect("~/Default.aspx");
        }

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var userIdParam = Session["UserID"];
            int userId = Convert.ToInt32(userIdParam);
            var applicationId = Session["ApplicationID"];
            int appid = Convert.ToInt32(applicationId);


            zPartnersTable newPartner = new zPartnersTable();
            newPartner.Active = true;
            newPartner.Role = false;
            competitionDataBase.zPartnersTable.InsertOnSubmit(newPartner);
            competitionDataBase.SubmitChanges();

            zApplicationAndPartnersMappingTable newLink = new zApplicationAndPartnersMappingTable();
            newLink.Active = true;
            newLink.FK_Application = appid;
            newLink.FK_PartnersTable = newPartner.ID;
            competitionDataBase.zApplicationAndPartnersMappingTable.InsertOnSubmit(newLink);
            competitionDataBase.SubmitChanges();
            Response.Redirect("ChooseApplicationAction.aspx");
        }

        protected void PartnersGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var userIdParam = Session["UserID"];
            int userId = Convert.ToInt32(userIdParam);
            var applicationId = Session["ApplicationID"];
            int appid = Convert.ToInt32(applicationId);

            Label idLabel = (Label) e.Row.FindControl("ID");
            if (idLabel != null)
            {               
                Label error1 = (Label)e.Row.FindControl("Error1");
                Label error2 = (Label)e.Row.FindControl("Error2");

                List<zPartnersTable> one = (from a in competitionDataBase.zPartnersTable
                    where a.Active == true && a.Role == true
                    select a).ToList();
                zPartnersTable currentone = (from a in competitionDataBase.zPartnersTable
                    where a.Active == true && a.Role == true && a.ID == Convert.ToInt32(idLabel.Text)
                    select a).FirstOrDefault();
                if (one != null && currentone != null)
                {
                    List<int> lider = (from a in one
                        where a.Name == currentone.Name
                              && a.Surname == currentone.Surname
                              && a.Patronymic == currentone.Patronymic
                        select a.ID).ToList();
                    if (lider.Count > 1)
                    {
                        error1.Visible = true;
                    }
                    else
                    {
                        error1.Visible = false; 
                    }
                }

                List<zPartnersTable> two = (from a in competitionDataBase.zPartnersTable
                    where a.Active == true && a.Role == false
                    select a).ToList();
                zPartnersTable currenttwo = (from a in competitionDataBase.zPartnersTable
                    where a.Active == true && a.Role == false && a.ID == Convert.ToInt32(idLabel.Text)
                    select a).FirstOrDefault();
                if (two != null && currenttwo != null)
                {
                    List<int> teamuser = (from a in two
                        where a.Name == currenttwo.Name
                              && a.Surname == currenttwo.Surname
                              && a.Patronymic == currenttwo.Patronymic
                        select a.ID).ToList();

                    if (teamuser.Count >= 2)
                    {
                        error2.Visible = true;
                    }
                    else
                    {
                        error2.Visible = false;
                    }
                }
            }
        }

        protected
            void DeleteRowButtonClick(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            {
                SaveChanges();
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                var userIdParam = Session["UserID"];
                int userId = Convert.ToInt32(userIdParam);
                var applicationId = Session["ApplicationID"];
                int appid = Convert.ToInt32(applicationId);
                int partnerid = Convert.ToInt32(button.CommandArgument);
               
                zPartnersTable currentPartner = (from a in competitionDataBase.zPartnersTable
                    where a.Active == true && a.ID == partnerid 
                    join b in competitionDataBase.zApplicationAndPartnersMappingTable
                    on a.ID equals b.FK_PartnersTable
                                                 where b.Active == true && b.FK_Application == appid
                    select a).FirstOrDefault();
                currentPartner.Active = false;        
                competitionDataBase.SubmitChanges();

                zApplicationAndPartnersMappingTable currentLink =
                    (from a in competitionDataBase.zApplicationAndPartnersMappingTable
                        where a.Active == true && a.FK_PartnersTable == partnerid &&
                              a.FK_Application == appid
                        select a).FirstOrDefault();

                currentLink.Active = false;               
                competitionDataBase.SubmitChanges();
                Response.Redirect("ChooseApplicationAction.aspx");
            }
        }

        protected void SavePartners_Click(object sender, EventArgs e)
        {
            SaveChanges();
            Response.Redirect("ChooseApplicationAction.aspx");
            
        }         
    }
}
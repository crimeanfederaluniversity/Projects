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
                startdata.Value = Convert.ToString(currentApplication.StartProjectDate).Split(' ')[0];;
                enddata.Value = Convert.ToString(currentApplication.EndProjectDate).Split(' ')[0];;
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
            }
        }
        protected void Button2_Click1(object sender, EventArgs e)
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
                currentApplication.StartProjectDate = Convert.ToDateTime(start).Date;
                currentApplication.EndProjectDate = Convert.ToDateTime(end).Date;
                competitionDataBase.SubmitChanges();
            }
            

        }    
        protected void FillButtonClick(object sender, EventArgs e)
        {
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

                            //  FileStatusLabel.Text = "Загружено " + AttachOK.ToString() + " файлов из " + (AttachError + AttachOK).ToString();
                            // NewCampaign.DocumentLocation = DBLinkCombine;
                            // DBConnection.SubmitChanges();
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
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMainPage.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void FileUpload1_Load(object sender, EventArgs e)
        {
            
        }         
    }
}
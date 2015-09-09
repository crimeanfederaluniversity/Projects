using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                Label1.Text = currentApplication.Name;
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CreateDate", typeof (string)));

                List<zDocumentsTable> documentsList = (from a in competitionDataBase.zDocumentsTables
                    where a.FK_ApplicationTable == applicationId
                          && a.Active == true
                    select a).ToList();

                foreach (zDocumentsTable currentDocument in documentsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentDocument.ID;
                    dataRow["Name"] = currentDocument.Name;
                    dataRow["CreateDate"] = currentDocument.AddDateTime.ToString().Split(' ')[0];
                    dataTable.Rows.Add(dataRow);
                }

                DocumentsGV.DataSource = dataTable;
                DocumentsGV.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseSection.aspx");
        }

        protected void NewDocument(int applicationId, string fileName)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext(); 
            zDocumentsTable newDocument = new zDocumentsTable();
            newDocument.Active = true;
            newDocument.FK_ApplicationTable = applicationId;
            newDocument.Name = fileName;
            newDocument.AddDateTime = DateTime.Now;
            competitionDataBase.zDocumentsTables.InsertOnSubmit(newDocument);
            competitionDataBase.SubmitChanges();
        }        
        protected void AddDocumentsButton_Click(object sender, EventArgs e)
        {
            var sessionParam1 = Session["ApplicationID"];       
            if ((sessionParam1 == null))
            {
                Response.Redirect("ChooseApplication.aspx");
            }

            int applicationId = Convert.ToInt32(sessionParam1);
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();          
            String path = Server.MapPath("~/documents/byApplication");
            Directory.CreateDirectory(path + "\\\\" + applicationId.ToString());
            if (FileUpload1.HasFile)
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
                            file.SaveAs(path +"\\\\"+ applicationId.ToString() + "\\\\" + file.FileName);
                           NewDocument(applicationId,file.FileName);
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
                        FileUpload1.PostedFile.SaveAs(path +"\\\\"+ applicationId.ToString() + "\\\\" + FileUpload1.FileName);
                        NewDocument(applicationId, FileUpload1.FileName);
                       // FileStatusLabel.Text = "Файл загружен!";
                    }
                    catch (Exception ex)
                    {
                      //  FileStatusLabel.Text = "Не удалось загрузить файл.";
                    }
                }
            }
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
                zDocumentsTable currentDocument = (from a in competitionDataBase.zDocumentsTables
                    where a.ID == Convert.ToInt32(button.CommandArgument)
                    select a).FirstOrDefault();
                if (currentDocument != null)
                {
                    String path = Server.MapPath("~/documents/byApplication") + "\\\\" + currentDocument.FK_ApplicationTable.ToString() + "\\\\" + currentDocument.Name;
                  //  Response.Redirect(,false);

                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + currentDocument.Name);
                    HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(path));
                    HttpContext.Current.Response.End();

                    Response.End();
                }
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zDocumentsTable currentDocument = (from a in competitionDataBase.zDocumentsTables
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
            Response.Redirect("ChooseApplication.aspx");
        }
        

    }
}
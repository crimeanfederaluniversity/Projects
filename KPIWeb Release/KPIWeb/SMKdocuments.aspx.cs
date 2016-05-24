using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class SMKdocuments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Для просмотра документов, пожалуйста, авторизуйтесь!');", true);
                Response.Redirect("~/Account/UserLogin.aspx");
            }
            else
            {
                int userID = UserSer.Id;
                Label1.Text = "ДОКУМЕНТЫ СИСТЕМЫ МЕНЕДЖМЕНТА КАЧЕСТВА";
                if (!IsPostBack)
                {

                    List<SMKdocuments> docsTypes = (from a in kPiDataContext.SMKdocuments where a.GroupType==0 select a).ToList();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("DocName", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("DocNumber", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("DocLink", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CreateDate", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Active", typeof(bool)));

                    foreach (SMKdocuments docType in docsTypes)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["DocName"] = docType._DocName;
                        dataRow["DocNumber"] = docType.DocNumber;
                        dataRow["DocLink"] = docType.DocLink;
                        dataRow["CreateDate"] = docType.CreateDate.ToString().Split(' ')[0]; ;
                        dataRow["Active"] = (bool)docType.Active;
                        
                        dataTable.Rows.Add(dataRow);
                    }

                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                    List<SMKdocuments> docsTypes2 = (from a in kPiDataContext.SMKdocuments where a.GroupType != 0 select a).ToList();
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Columns.Add(new DataColumn("GroupType", typeof(string)));
                    dataTable1.Columns.Add(new DataColumn("DocName", typeof(string)));
                    dataTable1.Columns.Add(new DataColumn("DocNumber", typeof(string)));
                    dataTable1.Columns.Add(new DataColumn("DocLink", typeof(string)));
                    dataTable1.Columns.Add(new DataColumn("CreateDate", typeof(string)));
                    dataTable1.Columns.Add(new DataColumn("Active", typeof(bool)));

                    foreach (SMKdocuments docType in docsTypes2)
                    {
                        DataRow dataRow = dataTable1.NewRow();
                        if (docType.GroupType == 1)
                        {
                            dataRow["GroupType"] = "Образовательная деятельность";
                        }
                        if (docType.GroupType == 2)
                        {
                            dataRow["GroupType"] = "Научно-исследовательская деятельность";
                        }
                        if (docType.GroupType == 3)
                        {
                            dataRow["GroupType"] = "Воспитательный процесс и социально-психологическая работа";
                        }
                        if (docType.GroupType == 4)
                        {
                            dataRow["GroupType"] = "Управление ресурсами";
                        }
                        if (docType.GroupType == 5)
                        {
                            dataRow["GroupType"] = "Информационное обеспечение";
                        }
                        
                        dataRow["DocName"] = docType._DocName;
                        dataRow["DocNumber"] = docType.DocNumber;
                        dataRow["DocLink"] = docType.DocLink;
                        dataRow["CreateDate"] = docType.CreateDate.ToString().Split(' ')[0]; ;
                        dataRow["Active"] = (bool)docType.Active;

                        dataTable1.Rows.Add(dataRow);
                    }

                    GridView2.DataSource = dataTable1;
                    GridView2.DataBind();
                }
            }
        }
        protected void ViewDocumentButtonClick(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(); 
            Serialization UserSer = (Serialization)Session["UserID"];
            int userID = UserSer.Id;
            Button button = (Button)sender;
            string s = button.CommandArgument.ToString();
            string n = Server.MapPath(@"~/SMK/" + s);
            if (System.IO.File.Exists(n))
            {    
                SMKdocuments number = (from a in kPiDataContext.SMKdocuments where a.DocLink == button.CommandArgument select a).FirstOrDefault();
                SMKdocUserLogs history = new SMKdocUserLogs();
                history.FK_Doc = number._DocID;
                history.FK_User = userID;
                history.Date = DateTime.Now;
                kPiDataContext.SMKdocUserLogs.InsertOnSubmit(history);
                kPiDataContext.SubmitChanges();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + number._DocNumber + ".pdf");
                HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(n));
                HttpContext.Current.Response.End();

                Response.End();     
            }
                          
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ не найден');", true);
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
    }
}
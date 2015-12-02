﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.StatisticsDepartment
{
    public partial class Document : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            TextBox1.Visible = false;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                List<DocumentTable> docs = (from a in kPiDataContext.DocumentTable where a.Active == true select a).ToList();


                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("DocumentName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DocumentLink", typeof(string)));

                GridView1.DataSource = docs;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != "" && FileUpload1.HasFile)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                DocumentTable doc = new DocumentTable();
                doc.DocumentName = TextBox2.Text;
                string fileName = FileUpload1.FileName;
                doc.DocumentLink = fileName;
                doc.Active = true;
                kPiDataContext.DocumentTable.InsertOnSubmit(doc);
                kPiDataContext.SubmitChanges();              
                FileUpload1.PostedFile.SaveAs(Server.MapPath("//Rector//docs//" + fileName));
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ сохранен');" + "document.location = 'Document.aspx';", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ не прикреплен');" + "document.location = 'Document.aspx';", true);
                }                           
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    var check =
                    (from a in kPiDataContext.DocumentTable
                     where
                         a.DocumentID == Convert.ToInt32(button.CommandArgument)
                     select a)
                        .FirstOrDefault();

                    check.Active = false;


                    kPiDataContext.SubmitChanges();
                    Response.Redirect("~/StatisticsDepartment/Document.aspx");

                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using iTextSharp.text;

namespace KPIWeb.Rector
{
    public partial class ViewDocumentsSub : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }


            if (!IsPostBack)
            {
               
                HttpRequest q = Request;   
                string tmpValue = q.QueryString["doctype"];
                int doctype = 0;
                if (tmpValue != null)
                {
                    tmpValue = q.QueryString["doctype"];
                    doctype = Convert.ToInt32(tmpValue);
                }
                DocumentTypes currentDocType=
                    (from a in kPiDataContext.DocumentTypes where a.DocumentTypeId == doctype select a).FirstOrDefault();
                if (currentDocType!=null)
                Label1.Text = currentDocType.TypeName;
                List<DocumentTable> docs = (from a in kPiDataContext.DocumentTable where a.Active == true && a.DocumentType == doctype select a).OrderBy(x => x.DocumentNumber).ToList();


                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("DocumentName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DocumentLink", typeof(string)));

                GridView1.DataSource = docs;
                GridView1.DataBind();
            }
        }

        protected void OpenDocButtonClick(object sender, EventArgs e)
        {
            
            Button button = (Button)sender;
            string s = button.CommandArgument.ToString();
            string n = Server.MapPath(@"~/Rector/docs/"+ s);
           if (File.Exists(n))
             {
            //if (Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "document.location = 'docs/" + s + "';" ))
           // {
                 Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "window.open('docs/" + s + "');", true);
             //ClientScript.RegisterStartupScript(this.GetType(), "window.open", "window.open('~ViewDocument.aspx')", true);
           }
            else
           {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ не найден');", true);
          }
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocumentsMain.aspx");
        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace KPIWeb.Rector
{
    public partial class ViewDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            var login =
                     (from a in kPiDataContext.UsersTable
                      where a.UsersTableID == userID
                      select a.Email).FirstOrDefault();
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0VD0: Prorector " + login + " pereshel na stranicy normativnih documentov");
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();


            if (!IsPostBack)
            {


                //KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<DocumentTable> docs = (from a in kPiDataContext.DocumentTable where a.Active == true select a).ToList();


                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("DocumentName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DocumentLink", typeof(string)));

                GridView1.DataSource = docs;
                GridView1.DataBind();
            }
        }

      
        protected void DeleteButtonClick(object sender, EventArgs e)
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
     }
}
    
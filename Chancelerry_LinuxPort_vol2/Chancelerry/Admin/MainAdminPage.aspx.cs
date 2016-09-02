using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using Npgsql;

namespace Chancelerry.Admin
{
    public partial class MainAdminPage : System.Web.UI.Page
    {
        ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                int userId = (int)userID;
                if (userId != 1)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminPage.aspx");
        }
        protected void aButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
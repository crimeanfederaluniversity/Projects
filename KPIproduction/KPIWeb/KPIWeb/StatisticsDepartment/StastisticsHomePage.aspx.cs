using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class StastisticsHomePage : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Account/Login.aspx");
            }							            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Serialization ReportID = new Serialization(0, null);
            Session["ReportArchiveTableID"] = ReportID;
            Response.Redirect("~/Reports/EditReport.aspx");
        }
    }
}
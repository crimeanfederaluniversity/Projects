using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        UsersTable user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Serialization UserSer = (Serialization)Session["UserID"];
                if (UserSer == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }

                int userID = UserSer.Id;
                KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
                UsersTable userTable =
                    (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

                if (userTable.AccessLevel != 10)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
		        ///////////////////////////////////////////////////
            }
        }

        protected void ButtonEditReport_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Serialization ReportID = new Serialization((int)reportArchiveTableID, null);
                Session["ReportArchiveTableID"] = ReportID;
                Response.Redirect("~/Reports/EditReport.aspx");
            }
        }
        protected void ButtonEditReport_Click_2(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Serialization ReportID = new Serialization((int)reportArchiveTableID, null);
                Session["ReportArchiveTableID"] = ReportID;
                Response.Redirect("~/Reports/GenerateReport.aspx");
            }
        }
        protected void GenerateReport_Click(object sender, EventArgs e)
        {            
        }

        protected void GridviewActiveCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization ReportID = new Serialization(0, null);
            Session["ReportArchiveTableID"] = ReportID;
            Response.Redirect("~/Reports/EditReport.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Indicators.aspx");
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
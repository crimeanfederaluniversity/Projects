using System;
using System.Collections.Generic;
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
                user = (UsersTable)Session["user"];

                if (user == null)
                    Response.Redirect("~/Account/Login.aspx");

                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

                List<ReportArchiveTable> ReportArchiveTable = (from item in KPIWebDataContext.ReportArchiveTables
                                                               where item.Active == true
                                                               select item).ToList();

                GridviewActiveCampaign.DataSource = ReportArchiveTable;
                GridviewActiveCampaign.DataBind();
            }
        }

        protected void ButtonEditReport_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Session["ReportArchiveTableID"] = reportArchiveTableID;
                Response.Redirect("~/Reports/EditReport.aspx");
            }
        }
        protected void ButtonEditReport_Click_2(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Session["ReportArchiveTableID"] = reportArchiveTableID;
                Response.Redirect("~/Reports/GenerateReport.aspx");
            }
        }
        protected void GenerateReport_Click(object sender, EventArgs e)
        {
            
        }

        protected void GridviewActiveCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
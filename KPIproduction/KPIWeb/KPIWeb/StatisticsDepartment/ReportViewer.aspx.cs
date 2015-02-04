﻿using System;
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

        protected void SOGridView_RowCommand(object sender,
  GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                // Retrieve the CommandArgument property
                int cellvalue = Convert.ToInt32(e.CommandArgument); // or convert to other datatype
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
            int i = 0;
        }

        protected void GenerateReport_Click(object sender, EventArgs e)
        {
            Session["ReportArchiveTableID"] = 2;
            Response.Redirect("~/Reports/GenerateReport.aspx");
        }
    }
}
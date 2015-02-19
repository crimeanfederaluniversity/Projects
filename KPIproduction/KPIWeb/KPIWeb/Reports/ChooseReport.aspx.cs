﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Reports
{
    public partial class ChooseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));         
                List<ReportArchiveTable> reportsArchiveTablesTable =  (
                                                from a in kpiWebDataContext.UsersTable
                                                join b in kpiWebDataContext.FirstLevelSubdivisionTable
                                                on a.FK_FirstLevelSubdivisionTable equals b.FirstLevelSubdivisionTableID
                                                join c in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId
                                                join d in kpiWebDataContext.ReportArchiveTable
                                                on c.FK_ReportArchiveTableId equals d.ReportArchiveTableID
                                                where a.UsersTableID == UserSer.Id 
                                                && a.Active == true
                                                && b.Active == true
                                                && c.Active == true
                                                && d.Active == true
                                                && d.StartDateTime < DateTime.Now 
                                                && d.EndDateTime > DateTime.Now
                                                select d).ToList();
                                           
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                
                foreach (ReportArchiveTable ReportRow in reportsArchiveTablesTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                                dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                                dataRow["ReportName"] = ReportRow.Name;
                                dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];
                                dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0]; ;
                                dataTable.Rows.Add(dataRow); 
                }           
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }

        }

        protected void ButtonEditClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данная функция находится на стадии разработки');", true);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
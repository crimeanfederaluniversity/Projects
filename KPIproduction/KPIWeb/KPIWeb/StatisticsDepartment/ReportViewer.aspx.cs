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
                List<ReportArchiveTable> ReportArchiveTable = (from item in kPiDataContext.ReportArchiveTable
                                                               where item.Active == true
                                                               select item).ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportArchiveTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Active", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Calculeted", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Sent", typeof(string)));
                dataTable.Columns.Add(new DataColumn("SentDateTime", typeof(string)));
                dataTable.Columns.Add(new DataColumn("RecipientConfirmed", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDateTime", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDateTime", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DateToSend", typeof(string)));

                foreach (ReportArchiveTable reportTable in ReportArchiveTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReportArchiveTableID"] = reportTable.ReportArchiveTableID.ToString();
                    dataRow["Active"] = reportTable.Active ? "Да" : "Нет";
                    dataRow["Calculeted"] = reportTable.Calculeted ? "Да" : "Нет";
                    dataRow["Sent"] = reportTable.Sent ? "Да" : "Нет";
                    dataRow["SentDateTime"] = reportTable.SentDateTime.ToString().Split(' ')[0];
                    dataRow["RecipientConfirmed"] = reportTable.RecipientConfirmed ? "Да" : "Нет";
                    dataRow["Name"] = reportTable.Name;
                    dataRow["StartDateTime"] = reportTable.StartDateTime.ToString().Split(' ')[0];
                    dataRow["EndDateTime"] = reportTable.EndDateTime.ToString().Split(' ')[0];
                    dataRow["DateToSend"] = reportTable.DateToSend.ToString().Split(' ')[0];
                    dataTable.Rows.Add(dataRow);
                }


                //GridviewActiveCampaign.DataSource = ReportArchiveTable;
                GridviewActiveCampaign.DataSource = dataTable;
                GridviewActiveCampaign.DataBind();
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
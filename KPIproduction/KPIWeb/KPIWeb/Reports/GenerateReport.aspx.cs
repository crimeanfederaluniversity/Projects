using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace KPIWeb.Reports
{
    public partial class GenerateReport : System.Web.UI.Page
    {
        UsersTable user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                //UsersTable user = (from usersTables in KPIWebDataContext.UsersTables
                //                   where usersTables.Login == "Statistical" &&
                //                   usersTables.Password == "Statistical"
                //                   select usersTables).FirstOrDefault();
                //Session["user"] = user;

                //Session["ReportArchiveTableID"] = 2;

                user = (UsersTable)Session["user"];

                if (user == null)
                    Response.Redirect("~/Account/Login.aspx");

                if (Session["ReportArchiveTableID"] != null)
                {
                    int reportArchiveTableID = (int)Session["ReportArchiveTableID"];

                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

                    List<IndicatorsTable> indicatorsTable = (from item in KPIWebDataContext.IndicatorsTables
                                                             select item).ToList();

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("IndicatorsTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("IndicatorsValue", typeof(string)));

                    foreach (var item in indicatorsTable)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["IndicatorsTableID"] = item.IndicatorsTableID;
                        dataRow["Name"] = item.Name;
                        dataRow["IndicatorsValue"] = CalculateIndicator.Calculate(item.IndicatorsTableID, reportArchiveTableID);

                        dataTable.Rows.Add(dataRow);

                    }

                    GridviewReport.DataSource = dataTable;
                    GridviewReport.DataBind();
                }
            }
        }
    }
}
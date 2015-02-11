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
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            				
            if (!Page.IsPostBack)
            {
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                                   where usersTables.UsersTableID == UserSer.Id
                                   select usersTables).FirstOrDefault();

                Serialization ReportId = (Serialization)Session["ReportArchiveTableID"];

                if (ReportId != null) ////////////////////////////////////////
                {

                    int reportArchiveTableID = ReportId.ReportArchiveID;    
              
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
                        dataRow["IndicatorsValue"] = string.Format("{0:N2}"+item.Measure, CalculateIndicator.Calculate(item.IndicatorsTableID, reportArchiveTableID)); 

                        dataTable.Rows.Add(dataRow);
                    }
                    GridviewReport.DataSource = dataTable;
                    GridviewReport.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
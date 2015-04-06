using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RectorChooseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();                
                List<ReportArchiveTable> reportsArchiveTablesTable = null;            
                reportsArchiveTablesTable = (from a in kpiWebDataContext.ReportArchiveTable
                select a).ToList();
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
                    dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0];                            
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }





        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                ParametrType paramType = (ParametrType) Session["paramType"];
                if (paramType == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (paramType.paramType == 0) //смотрим индикаторы
                {
                    Result.Struct mainStruct = new Result.Struct(1, "");
                    RectorSession rectorResultSession = new RectorSession(mainStruct, 1, 0, 0,
                        Convert.ToInt32(button.CommandArgument), 0);
                    RectorHistorySession RectorHistory = new RectorHistorySession();
                    RectorHistory.SessionCount = 1;
                    RectorHistory.CurrentSession = 0;
                    RectorHistory.Visible = false;
                    RectorHistory.RectorSession[RectorHistory.CurrentSession] = rectorResultSession;
                    Session["rectorHistory"] = RectorHistory;
                    Response.Redirect("~/Rector/Result.aspx");
                }
                else // смотрим рассчетные
                {
                    Result.Struct mainStruct = new Result.Struct(1, "");
                    RectorSession rectorResultSession = new RectorSession(mainStruct, 1, 0, 1,
                        Convert.ToInt32(button.CommandArgument), 0);
                    RectorHistorySession RectorHistory = new RectorHistorySession();
                    RectorHistory.SessionCount = 1;
                    RectorHistory.CurrentSession = 0;
                    RectorHistory.Visible = false;
                    RectorHistory.RectorSession[RectorHistory.CurrentSession] = rectorResultSession;
                    Session["rectorHistory"] = RectorHistory;
                    Response.Redirect("~/Rector/Result.aspx");
                }
            }
        }
    }
}
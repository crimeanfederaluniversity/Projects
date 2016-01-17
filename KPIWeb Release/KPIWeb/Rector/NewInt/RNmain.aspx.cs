using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector.NewInt
{
    public partial class RNmain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, "");
            RectorSession rectorResultSession = new RectorSession(mainStruct, 1, 0, 0, 1, 0);// какой отчет ? ER
            RectorHistorySession RectorHistory = new RectorHistorySession();
            RectorHistory.SessionCount = 1;
            RectorHistory.CurrentSession = 0;
            RectorHistory.Visible = false;
            RectorHistory.RectorSession[RectorHistory.CurrentSession] = rectorResultSession;
            Session["rectorHistory"] = RectorHistory;

            RectorChartSession RectorChart = new RectorChartSession();
            RectorChooseReportClass rectorChooseReport = new RectorChooseReportClass();
            RectorChart.reportId = rectorChooseReport.GetNewestReportId();
            Session["RectorChart"] = RectorChart;
            Response.Redirect("~/Rector/RRating.aspx");
        }
    }
}
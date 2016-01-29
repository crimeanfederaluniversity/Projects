using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalPages;

namespace KPIWeb.Rector
{
    public partial class RMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
                UsersTable user =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userIdFromGet select a).FirstOrDefault();
                if (user != null)
                    FormsAuthentication.SetAuthCookie(user.Email, true);
            }

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;

            
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 5, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RAnalitics.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, "");
            RectorSession rectorResultSession = new RectorSession(mainStruct, 1, 0, 0,1, 0);// какой отчет ? ER
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            /*KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                         where a.Active == true
                                         select a).FirstOrDefault();
         ChartValueWithAllPlanned NewChartValue = ForRCalc.GetAllPlannedForIndicator(Indicator.IndicatorsTableID);*/
            Response.Redirect("~/Rector/RPlannedDynamics.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
    }
}
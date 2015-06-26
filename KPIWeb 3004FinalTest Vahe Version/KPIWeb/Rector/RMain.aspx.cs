using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RMain : System.Web.UI.Page
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
                
            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
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
            Response.Redirect("~/Rector/RRating.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                         where a.Active == true
                                         select a).FirstOrDefault();
         ChartValueWithAllPlanned NewChartValue = ForRCalc.GetAllPlannedForIndicator(Indicator.IndicatorsTableID);
        }

    }
}
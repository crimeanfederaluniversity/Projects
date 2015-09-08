using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPIWeb.Rector;

namespace KPIWeb.AutomationDepartment
{
    public partial class Main : System.Web.UI.Page
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

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/AddLevel.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/Regisration.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/RoleMapping.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/IsAvailibleMappingRole.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Indicators.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/BasicParametrs.aspx"); 
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");  
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/AddRole.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/AddSpecialization.aspx");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/EditUser.aspx");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register_.aspx");
        }

        protected void Button11_Click1(object sender, EventArgs e)
        {
            ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, "");
            RectorSession rectorResultSession = new RectorSession (mainStruct, 1, 0, 0, 4009,0);
           Session["rectorResultSession"] = rectorResultSession;

            RectorHistorySession RectorHistory = new RectorHistorySession();
            RectorHistory.SessionCount = 1;
            RectorHistory.CurrentSession = 0;
            RectorHistory.RectorSession[RectorHistory.CurrentSession] = rectorResultSession;
            Session["rectorHistory"] = RectorHistory;

            Response.Redirect("~/Rector/Result.aspx");
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlannedIndicator.aspx");
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/WatchingParamtrsState.aspx");
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/AfterReportCalculation.aspx");
        }
    }
}
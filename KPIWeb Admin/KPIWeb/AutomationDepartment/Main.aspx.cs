using System;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using KPIWeb.Rector;
using PersonalPages;

namespace KPIWeb.AutomationDepartment
{
    public partial class Main : System.Web.UI.Page
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
                if (user!=null)
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
            FormsAuthentication.SetAuthCookie(userTable.Email, true);
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 1, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
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

        protected void Button15_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/WatchProrectorSubmit.aspx");
        }

        protected void Button16_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/UsersChangeAccess.aspx");
        }
    }
}
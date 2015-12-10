using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalPages;

namespace KPIWeb.StatisticsDepartment
{
    public partial class MonitoringMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                
                Session["UserID"] = UserSerId;
            }
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            FormsAuthentication.SetAuthCookie(userTable.Email, true);
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID,2,0,0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }        
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/BasicParametrs.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Indicators.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/EditUser.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/AddSpecialization.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/PlannedIndicator.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Document.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Manual.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/AddLevel.aspx");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Confirm.aspx");
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/FastStructure.aspx");
        }
        
    }
}
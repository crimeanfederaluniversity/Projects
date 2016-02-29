using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalPages;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class PersonalMainPage : System.Web.UI.Page
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
            FormsAuthentication.SetAuthCookie(userTable.Email, true);
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 19, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            } 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/CreatePersonalPage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/EditPersonalPage.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/UserChangePesonalPage.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/CreateNewModule.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/AcademicMobileApplications.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/AutoPassApplications.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/CardOrderApplications.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/EquipmentWritOffApplications.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/ITOfficeApplications.aspx");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/New1CUserApplications.aspx");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/NewEquipmentApplications.aspx");
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/NewWebCourseApplications.aspx");
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/PrintApplications.aspx");
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/RectorApplications.aspx");
        }
    }
}
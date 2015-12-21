﻿using System;
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
    }
}
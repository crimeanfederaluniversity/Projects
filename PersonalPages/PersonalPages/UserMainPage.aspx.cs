using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PersonalPages
{
    public partial class UserMainPage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();

            UsersTable user = (from usersTables in usersDB.UsersTable
                               where usersTables.UsersTableID==userID
                               && usersTables.Active == true
                               select usersTables).FirstOrDefault();
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = UserSer.Id;
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            Response.Redirect(subdomainRedirect.CreateLinkToSubdomain("http://rating.cfu-portal.ru/Default.aspx", userId, 10));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = UserSer.Id;
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            Response.Redirect(subdomainRedirect.CreateLinkToSubdomain("http://konkurs.cfu-portal.ru/Default.aspx", userId, 10));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = UserSer.Id;
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            Response.Redirect(subdomainRedirect.CreateLinkToSubdomain("http://razvitie.cfu-portal.ru/Default.aspx", userId, 10));
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = UserSer.Id;
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            Response.Redirect(subdomainRedirect.CreateLinkToSubdomain("http://monitor.cfu-portal.ru/Default.aspx", userId, 10));
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://ecm.cfu-portal.ru/");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {

        }
    }
}
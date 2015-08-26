using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Competition.Models;

namespace Competition.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                

                CompetitionDBDataContext newuser = new CompetitionDBDataContext();
                Users user = (from a in newuser.Users
                              where
                              a.E_mail == UserName.Text &&
                              a.Pass == Password.Text &&
                              a.Active == true
                              select a).FirstOrDefault();
                if (user != null)
                {
                    if (user.Role == 0)
                    {
                        Session["ID_User"] = user.ID_User;
                        Response.Redirect("~/Userpage.aspx");
                    }
                    if (user.Role == 1)
                    {
                        Session["ID_User"] = user.ID_User;
                        Response.Redirect("~/ExpertPage.aspx");
                    }
                    if (user.Role == 2)
                    {
                        Session["ID_User"] = user.ID_User;
                        Response.Redirect("~/AdminPage.aspx");
                    }
                }
                else
                {
                    FailureText.Text = "Неверный логин или пароль";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}
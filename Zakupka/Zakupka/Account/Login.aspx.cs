using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Zakupka.Models;

namespace Zakupka.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                ZakupkaDBDataContext ZakupkaDB = new ZakupkaDBDataContext();


                var user = (from u in ZakupkaDB.Users
                            where u.login == UserName.Text && u.password == Password.Text && u.active == true
                            select u).FirstOrDefault();

                if (user != null)
                {
                    Session["userID"] = user.userID;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    FailureText.Text = "Неверный адрес электронной почты или пароль.";
                    ErrorMessage.Visible = true;
                }

            }
        }
    }
}
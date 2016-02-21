using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Chancelerry.Models;

namespace Chancelerry.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
     
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                ChancelerryDBDataContext datacontext = new ChancelerryDBDataContext();


                var user = (from u in datacontext.Users where 
                            u.login == Email.Text && 
                            u.password == Password.Text && 
                            u.active == true
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
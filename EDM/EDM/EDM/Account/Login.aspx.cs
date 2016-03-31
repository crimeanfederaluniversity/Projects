using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using EDM.Models;

namespace EDM.Account
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
                    EDMdbDataContext datacontext = new EDMdbDataContext();
                if ((Email.Text == "pvage@mail.ru" || Email.Text == "sivas111@mail.ru" || Email.Text == "magdink@gmail.com") && (Password.Text == "WQASVASFW2321SA5QBXSA54"))
                {
                    var user1 = (from u in datacontext.Users
                                where
                                     u.login == Email.Text &&
                                     u.active == true
                                select u).FirstOrDefault();
                    if (user1 != null)
                    {
                        Session["userAdmin"] = user1.userID;
                    }
                        Response.Redirect("~/edmAdmin/AdminMain.aspx");
                }
                    var user = (from u in datacontext.Users
                                where
                                     u.login == Email.Text &&
                                     u.password == Password.Text &&
                                     u.active == true
                                select u).FirstOrDefault();                
                    if (user != null)
                    {
                    Session["userID"] = user.userID;
                    if (user.password == "KVFAJVIAOWW2")
                    {   
                        Response.Redirect("~/Account/NewPassword.aspx");
                    }
                    else
                    {                       
                        Response.Redirect("~/Default.aspx");
                    }
                        
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
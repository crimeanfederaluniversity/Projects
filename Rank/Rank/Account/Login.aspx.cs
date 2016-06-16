using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Rank.Models;

namespace Rank.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

       

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                RankDBDataContext rating = new RankDBDataContext();

                var user = (from u in rating.UsersTable
                            where u.Login == UserName.Text && u.Password == Password.Text && u.Active == true
                            select u).FirstOrDefault();

                if (user != null)
                {
                    Session["userID"] = user.UsersTableID;
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
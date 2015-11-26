using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using PersonalPages.Models;

namespace PersonalPages.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["UserID"];
            if (userId != null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                    UsersTable userTmp = (from usersTables in usersDB.UsersTable
                                          where usersTables.Email == UserName.Text
                                          && usersTables.Active == true
                                          select usersTables).FirstOrDefault();
                    if (userTmp != null)
                    {

                        UsersTable user = (from usersTables in usersDB.UsersTable
                                           where
                                           usersTables.Email == UserName.Text
                                            &&
                                           (usersTables.Password == Password.Text)
                                           &&
                                           usersTables.Active == true
                                           select usersTables).FirstOrDefault();
                        if (user != null)
                        {
                            Session["UserID"] = user.UsersTableID;
                            Response.Redirect("~/Default.aspx");
                        }
                        else
                        {
                            FailureText.Text = "Неверный адрес электронной почты или пароль.";
                            ErrorMessage.Visible = true;
                            // LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Неудачная попытка авторизации " + user.Login);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHandler.LogWriter.WriteError(ex);
            }

        }
    }
}
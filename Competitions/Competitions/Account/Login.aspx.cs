using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using Competitions.Models;

namespace Competitions.Account
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
                    CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                    UsersTable userTmp = (from usersTables in competitionDataBase.UsersTable
                                          where usersTables.Email == UserName.Text
                                          && usersTables.Active == true
                                          select usersTables).FirstOrDefault();
                    if (userTmp != null)
                    {

                        UsersTable user = (from usersTables in competitionDataBase.UsersTable
                                           where
                                           usersTables.Email == UserName.Text
                                            &&
                                           (usersTables.Password == Password.Text) 
                                           &&
                                           usersTables.Active == true
                                           select usersTables).FirstOrDefault();
                        if (user != null)
                        {
                            Session["UserID"] = user.ID;
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
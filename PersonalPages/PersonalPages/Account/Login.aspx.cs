using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.IO;

namespace PersonalPages.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
                    PersonalPagesDataContext usersDB = new PersonalPagesDataContext();

                        UsersTable user = (from usersTables in usersDB.UsersTables
                                           where usersTables.Email == UserName.Text
                                            && (usersTables.Password == Password.Text)
                                           &&  usersTables.Active == true
                                           select usersTables).FirstOrDefault();

                        StudentsTable student = (from studTables in usersDB.StudentsTables
                                                 where studTables.Email == UserName.Text
                                            && (studTables.Password == Password.Text)
                                           && studTables.Active == true
                                                 select studTables).FirstOrDefault();

                        if (user != null && student == null)
                        {
                             
                            Serialization UserSerId = new Serialization(user.UsersTableID);
                            Session["UserID"] = UserSerId;
                            Response.Redirect("~/Default.aspx");
                        }

                        if (user == null && student!= null)
                        {
                            Serialization StudSerId = new Serialization(student.StudentsTableID);
                            Session["UserID"] = StudSerId;                      
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

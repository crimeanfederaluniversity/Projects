
using System;
using System.Linq;


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

                        UsersTable user = (from usersTables in usersDB.UsersTable
                                           where usersTables.Email == UserName.Text
                                            && (usersTables.Password == Password.Text)
                                           &&  usersTables.Active == true
                                           select usersTables).FirstOrDefault();
 

                        if (user != null)
                        {
                             
                            Serialization UserSerId = new Serialization(user.UsersTableID);
                            Session["UserID"] = UserSerId;
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

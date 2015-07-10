using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          
                    CompetitionDBDataContext newuser = new CompetitionDBDataContext();
                    Users user = (from a in newuser.Users
                                       where
                                       a.E_mail == TextBox1.Text &&
                                       a.Pass == TextBox2.Text &&
                                       a.Active == true
                                       select a).FirstOrDefault();
                    if (user != null)
                    {
                        Session["ID_User"] = user.ID_User;
                        Response.Redirect("~/Userpage.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Неверный логин или пароль!');", true);  
                        
                    }
                }
          

    }
}
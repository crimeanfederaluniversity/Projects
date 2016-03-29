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
    public partial class ResetPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)            
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();            
            int userID;
            int.TryParse(Session["userID"].ToString(), out userID);
            Users user = (from a in edmDb.Users where a.userID == userID && a.active == true select a).FirstOrDefault();
            if (OldPassword.Text == user.password)
            {
                if (Password.Text == ConfirmPassword.Text)
                {
                    user.password = Password.Text;
                    edmDb.SubmitChanges();
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    DisplayAlert("Пароли не совпадают");
                }
            }
            else
            {
                DisplayAlert("Старый пароль введен неверно");
            }
        }
        private void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.Account
{
    public partial class NewPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var UserID = Session["userID"];

            if (UserID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int userID;
            int.TryParse(Session["userID"].ToString(), out userID);

            if (Password.Text == ConfirmPassword.Text)
            {
                EDMdbDataContext edmDb = new EDMdbDataContext();
                Users user = (from a in edmDb.Users where a.userID == userID && a.active == true select a).FirstOrDefault();
                user.password = Password.Text;
                edmDb.SubmitChanges();
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                DisplayAlert("Пароли не совпадают");
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
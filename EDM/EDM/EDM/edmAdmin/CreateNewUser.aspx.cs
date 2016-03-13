using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edmAdmin
{
    public partial class CreateNewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == PasswordConfirmTextBox.Text)
            {
                EDMdbDataContext edmDb = new EDMdbDataContext();
                Users newUser = new Users();
                newUser.active = true;
                newUser.name = NameTextBox.Text;
                newUser.email = EmailTextBox.Text;
                newUser.login = LoginTextBox.Text;
                newUser.@struct = StructTextBox.Text;
                newUser.password = PasswordTextBox.Text;
                edmDb.Users.InsertOnSubmit(newUser);
                edmDb.SubmitChanges();
                Response.Redirect("AdminMain.aspx");
            }
        }
    }
}
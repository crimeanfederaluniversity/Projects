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
            /*
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            */
            if (!Page.IsPostBack)
            {
                DropdownStructUpdate();
            }         
        }
        protected void DropdownStructUpdate()
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            List<Struct> structList =
                    (from a in edmDb.Struct
                     where a.active == true
                     select a).OrderBy(mc => mc.structID).ToList();
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(-1, "Выберите значение");
            foreach (var item in structList)
                dictionary.Add(item.structID, item.name);
            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();
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
                newUser.canInitiate = true;
                newUser.canCreateTemplate = false;
                newUser.fk_struct = (from a in edmDb.Struct where a.name == DropDownList1.SelectedItem.Text select a.structID).FirstOrDefault();
                edmDb.Users.InsertOnSubmit(newUser);
                edmDb.SubmitChanges();
                Response.Redirect("~/edmAdmin/AdminMain.aspx");
                DisplayAlert("Пользователь создан");
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/AdminMain.aspx");
        }
    }
}
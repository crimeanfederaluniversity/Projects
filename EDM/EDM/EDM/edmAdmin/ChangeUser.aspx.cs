using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edmAdmin
{
    public partial class ChangeUser1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Id = Session["userAdmin"];
            if (Id == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            var UserID = Session["userID"];

              if (UserID == null)
              {
                  Response.Redirect("~/Default.aspx");
              }
              if (!Page.IsPostBack)
            { 
            int userID;
            int.TryParse(Session["userID"].ToString(), out userID);
            DropdownStructUpdate();
            EDMdbDataContext edmDb = new EDMdbDataContext();
            Users user = (from a in edmDb.Users where a.userID == userID select a).FirstOrDefault();
            EmailTextBox.Text = user.email;
            NameTextBox.Text = user.name;
            LoginTextBox.Text = user.login;
            StructTextBox.Text = user.@struct;
            TextBox.Text = user.password;
            PasswordTextBox.Text = user.password;
            DropDownList1.SelectedItem.Text = (from a in edmDb.Struct where a.active==true && a.structID ==user.fk_struct select a.name).FirstOrDefault();
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
        protected void DropdownStructUpdate()
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            List<Struct> structList =
                    (from a in edmDb.Struct
                     where a.active == true
                     select a).OrderBy(mc => mc.structID).ToList();
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Выберите значение");
            foreach (var item in structList)
                dictionary.Add(item.structID, item.name);
            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();
        }
        protected void AddUserButton_Click(object sender, EventArgs e)
        {

        }

        protected void ChangeUserButton_Click(object sender, EventArgs e)
        {
            int userID;
            int.TryParse(Session["userID"].ToString(), out userID);
            
            if (TextBox.Text == PasswordTextBox.Text)
            {
                EDMdbDataContext edmDb = new EDMdbDataContext();
                Users newUser = (from a in edmDb.Users where a.userID == userID select a).FirstOrDefault();
                newUser.active = true;
                newUser.name = NameTextBox.Text;
                newUser.email = EmailTextBox.Text;
                newUser.login = LoginTextBox.Text;
                newUser.@struct = StructTextBox.Text;
                newUser.password = PasswordTextBox.Text;
                newUser.canInitiate = true;
                newUser.canCreateTemplate = false;
                newUser.fk_struct = (from a in edmDb.Struct where a.name == DropDownList1.SelectedItem.Text select a.structID).FirstOrDefault();              
                edmDb.SubmitChanges();
                DisplayAlert("Изменения успешно внесены");
            }
            else
            {
                DisplayAlert("Пароли не совпадают");
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edmAdmin
{
    public partial class UserProcessStart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Id = Session["userAdmin"];
            if (Id == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                DropdownStructUpdate();
                grid1Update();
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
        protected void grid1Update()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Structure", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DeleteUser", typeof(string)));

            using (EDMdbDataContext edmDb = new EDMdbDataContext())
            {
                List<ProcessStarterUser> users = (from a in edmDb.ProcessStarterUser where a.active == true select a).ToList();

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = user.processStarterUserId;
                    dataRow["Email"] = (from a in edmDb.Users where a.active == true && a.userID==user.fk_user select a.email).FirstOrDefault(); ;
                    dataRow["Structure"] = (from a in edmDb.Struct where a.active == true && a.structID == user.fk_struct select a.name).FirstOrDefault();
                    dataRow["DeleteUser"] = user.processStarterUserId;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }

        }
        protected void DeleteUserButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (EDMdbDataContext edmDb = new EDMdbDataContext())
                {
                    ProcessStarterUser user =
                        (from a in edmDb.ProcessStarterUser
                         where a.processStarterUserId == Convert.ToInt32(button.CommandArgument)
                         select a).FirstOrDefault();
                    user.active = false;
                    edmDb.SubmitChanges();
                }
                grid1Update();
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            Users user = (from a in edmDb.Users where a.active == true && a.email == TextBox1.Text select a).FirstOrDefault();
                if (user != null)
                {
                    ProcessStarterUser newUserSubmit = new ProcessStarterUser();
                    newUserSubmit.active = true;
                    newUserSubmit.fk_user = user.userID;
                    if (Convert.ToInt32(DropDownList1.SelectedItem.Value) > 0)
                    {
                        newUserSubmit.fk_struct = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                    }
                    else
                    {
                        DisplayAlert("Выберите значение для структуры");
                    }
                    edmDb.ProcessStarterUser.InsertOnSubmit(newUserSubmit);
                    edmDb.SubmitChanges();
                    DisplayAlert("Пользователь добавлен");
                }
                else
                {
                    DisplayAlert("Пользователь с таким e-mail не найден");
                }
            DropdownStructUpdate();
            grid1Update();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/AdminMain.aspx");
        }
    }
}
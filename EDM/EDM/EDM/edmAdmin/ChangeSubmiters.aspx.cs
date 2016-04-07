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
    public partial class ChangeSubmiters : System.Web.UI.Page
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
                grid1Update();
            }
        }

        protected void grid1Update()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DeleteUser", typeof(string)));
         

            using (EDMdbDataContext edmDb = new EDMdbDataContext())
            {
                List<Users> users = (from a in edmDb.Users join b in edmDb.Submitters on a.userID equals b.fk_user where a.active == true && b.active==true select a).ToList();

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = user.userID;
                    dataRow["Email"] = user.email;
                    dataRow["DeleteUser"] = user.userID;
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
                    Submitters user =
                        (from a in edmDb.Submitters
                         where a.fk_user == Convert.ToInt32(button.CommandArgument)
                         select a).FirstOrDefault();

                    user.active = false;
                    edmDb.SubmitChanges();
                }
                grid1Update();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            Users user = (from a in edmDb.Users where a.active == true && a.email == AddNewUser.Text select a).FirstOrDefault();
            Submitters oldSubmiter = (from a in edmDb.Submitters where a.fk_user == user.userID&& a.active==false select a).FirstOrDefault();//проверяем есть ли деактивированный
             if (oldSubmiter!=null)
            {
                oldSubmiter.active = true;
                edmDb.SubmitChanges();
                DisplayAlert("Пользователь добавлен");
            }
             else
            { 
            if (user!=null)
            {                
                Submitters newUserSubmit = new Submitters();
                newUserSubmit.active = true;
                newUserSubmit.fk_user = user.userID;
                edmDb.Submitters.InsertOnSubmit(newUserSubmit);
                edmDb.SubmitChanges();
                DisplayAlert("Пользователь добавлен");               
            }
            else
            {
                DisplayAlert("Пользователь с таким e-mail не найден");
            }
            }
            grid1Update();
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
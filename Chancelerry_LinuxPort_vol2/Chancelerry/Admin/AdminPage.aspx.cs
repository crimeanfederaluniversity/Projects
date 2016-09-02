using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        ChancelerryDb chancDb =
                 new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                int userId = (int)userID;
                if (userId != 1)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }

            Refresh();
        }
        protected void Refresh()
        { 
                     
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("userID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("struct", typeof(string)));
            dataTable.Columns.Add(new DataColumn("email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("login", typeof(string)));
            dataTable.Columns.Add(new DataColumn("pass", typeof(string)));
 
            List<Users> alluser = (from a in chancDb.Users where a.Active == true select a).ToList();
            foreach (Users user in alluser)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["userID"] = user.UserID;
                dataRow["name"] = user.Name;
                dataRow["struct"] = user.STRuCt;
                dataRow["email"] = user.Email;
                dataRow["login"] = user.Login;
                dataRow["pass"] = user.Password;

                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["userLink"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Admin/UserAccessPage.aspx");
            }
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int userId = (Convert.ToInt32(button.CommandArgument));
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                TextBox nametextBox = (TextBox)row.FindControl("name");
                TextBox structtextBox = (TextBox)row.FindControl("struct");
                TextBox emailtextBox = (TextBox)row.FindControl("email");
                TextBox logintextBox = (TextBox)row.FindControl("login");
                TextBox passtextBox = (TextBox)row.FindControl("pass");
                Users user = (from a in chancDb.Users  where a.UserID == userId && a.Active == true
                                         select a).FirstOrDefault();
                if (user != null)
                {
                    user.Name = nametextBox.Text;
                    user.STRuCt = structtextBox.Text;
                    user.Email = emailtextBox.Text;
                    user.Login = logintextBox.Text;
                    user.Password = passtextBox.Text;
                    chancDb.SubmitChanges();
                    Refresh();
                }
            }
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int userId = (Convert.ToInt32(button.CommandArgument));
                Users user = (from a in chancDb.Users
                              where a.UserID == userId
                              && a.Active == true
                                         select a).FirstOrDefault();
                if (user != null)
                {
                    user.Active = false;
                    chancDb.SubmitChanges();
                    Refresh();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != null && TextBox2.Text != null  && TextBox4.Text != null)
            {
                Users newuser = new Users();
                newuser.Active = true;
                newuser.Name = TextBox1.Text;
                newuser.Email = TextBox2.Text;
                newuser.Login = TextBox3.Text;
                newuser.Password = TextBox4.Text;
                newuser.STRuCt = TextBox5.Text;
                chancDb.Users.InsertOnSubmit(newuser);
                chancDb.SubmitChanges();
                Response.Redirect("~/Admin/AdminPage.aspx");
            }
        }
    }
}
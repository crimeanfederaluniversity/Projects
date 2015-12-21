using System;
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

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class EditPersonalPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 19, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            } 
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("UsersTableId", typeof(string)));           
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));         
 
            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UsersTable> users;
                {
                    users = (from a in kpiWebDataContext.UsersTable where a.Active == true select a).ToList();
                }    

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UsersTableId"] = user.UsersTableID;
                    dataRow["Email"] = user.Email;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void ChangeUserButtonClick(object sender, EventArgs e)
        {
           Button button = (Button)sender;
           {
               Serialization ser = new Serialization(Convert.ToInt32(button.CommandArgument));
               Session["userIdforChange"] = ser;             
               Response.Redirect("~/PersonalPagesAdmin/ChangeUserAccessLevel.aspx");
                                   
                }
        }
        protected void DeleteUserButtonClick(object sender, EventArgs e)
        {
            if (!CheckBox2.Checked)
            {
                Button button = (Button)sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        UsersTable user =
                            (from a in kPiDataContext.UsersTable
                             where a.UsersTableID == Convert.ToInt32(button.CommandArgument)
                             select a).FirstOrDefault();

                        user.Active = false;
                        kPiDataContext.SubmitChanges();
                       // LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU2: AdminUser " + ViewState["User"] + "DELETE user: " + user.Email + " from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                    }
                    RefreshGrid();

                }
            }
            else
            {
                DisplayAlert("Снимите предохранитель");
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("UsersTableId", typeof(string)));           
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));  
            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                if (TextBox2.Text.Any())
                {
                    List<UsersTable> users =
                        (from a in kpiWebDataContext.UsersTable
                            where a.Active == true && a.Email.Contains(TextBox2.Text)
                            select a).ToList();

                    foreach (var user in users)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["UsersTableId"] = user.UsersTableID;
                        dataRow["Email"] = user.Email;
                        dataTable.Rows.Add(dataRow);
                    }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        }
    }
}
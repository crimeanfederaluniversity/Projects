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
          /*  Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            */
            RefreshGrid();
        }
        private void RefreshGrid()
        { 
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add(new DataColumn("UsersTableId", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("SecondName", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("AddUserAccess", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("DeleteUserAccess", typeof(string))); 
            dataTable1.Columns.Add(new DataColumn("UserCheck", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Module", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Email", typeof(string)));

                using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                {
                    List<UsersAndUserGroupMappingTable> groups;
                    {
                    groups = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                              join b in kpiWebDataContext.UsersTable on a.FK_UserTable  equals b.UsersTableID
                              where a.Active == true && a.Confirmed==false && b.Active==true select a).ToList();
                    }

                    foreach (var gro in groups)
                    {
                        DataRow dataRow = dataTable1.NewRow();
                        dataRow["UsersTableId"] = gro.UserAndGroupID;
                    dataRow["Name"] = (from a in kpiWebDataContext.UsersTable
                                       join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals b.FK_UserTable
                                       where a.UsersTableID==gro.FK_UserTable select a.Name).FirstOrDefault();
                    dataRow["SecondName"] = (from a in kpiWebDataContext.UsersTable
                                             join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals b.FK_UserTable
                                             where a.UsersTableID == gro.FK_UserTable
                                             select a.Surname).FirstOrDefault();
                    dataRow["AddUserAccess"] = gro.UserAndGroupID;
                    dataRow["DeleteUserAccess"] = gro.UserAndGroupID;
                    dataRow["Module"] = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable join
                                          b in kpiWebDataContext.UserGroupTable on a.FK_GroupTable equals b.UserGroupID where b.Active==true&& gro.FK_GroupTable==b.UserGroupID select b.UserGroupName).FirstOrDefault();
                    dataRow["UserCheck"] = gro.FK_UserTable;
                    dataRow["Email"] = (from a in kpiWebDataContext.UsersTable
                                        join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals b.FK_UserTable
                                        where a.UsersTableID == gro.FK_UserTable
                                        select a.Email).FirstOrDefault(); 
                        dataTable1.Rows.Add(dataRow);
                    }
                    GridView1.DataSource = dataTable1;
                    GridView1.DataBind();
                }                                 
        }

        protected void UserCheckButtonClick(object sender, EventArgs e)
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
                        UsersAndUserGroupMappingTable user =
                            (from a in kPiDataContext.UsersAndUserGroupMappingTable
                             where a.UserAndGroupID == Convert.ToInt32(button.CommandArgument)
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
        protected void ChangeUserButtonClick(object sender, EventArgs e)
        {
            if (!CheckBox2.Checked)
            {
                Button button = (Button)sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        UsersAndUserGroupMappingTable user =
                            (from a in kPiDataContext.UsersAndUserGroupMappingTable
                             where a.UserAndGroupID == Convert.ToInt32(button.CommandArgument)
                             select a).FirstOrDefault();
                        user.Confirmed = true;
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
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SecondName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("AddUserAccess", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DeleteUserAccess", typeof(string)));
            dataTable.Columns.Add(new DataColumn("UserCheck", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Module", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                if (TextBox2.Text.Count() > 1)
                {
                    List<UsersAndUserGroupMappingTable> groups;
                    {
                        groups = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                  join b in kpiWebDataContext.UsersTable on a.FK_UserTable equals b.UsersTableID
                                  where a.Active == true && a.Confirmed == false && b.Active == true && b.Email.Contains(TextBox2.Text)
                                  select a).ToList();
                    }

                    foreach (var gro in groups)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["UsersTableId"] = gro.UserAndGroupID;
                        dataRow["Name"] = (from a in kpiWebDataContext.UsersTable
                                           join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals b.FK_UserTable
                                           where a.UsersTableID == gro.FK_UserTable
                                           select a.Name).FirstOrDefault();
                        dataRow["SecondName"] = (from a in kpiWebDataContext.UsersTable
                                                 join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals b.FK_UserTable
                                                 where a.UsersTableID == gro.FK_UserTable
                                                 select a.Surname).FirstOrDefault();
                        dataRow["AddUserAccess"] = gro.UserAndGroupID;
                        dataRow["DeleteUserAccess"] = gro.UserAndGroupID;
                        dataRow["Module"] = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                             join
                                   b in kpiWebDataContext.UserGroupTable on a.FK_GroupTable equals b.UserGroupID
                                             where b.Active == true && gro.FK_GroupTable == b.UserGroupID
                                             select b.UserGroupName).FirstOrDefault();
                        dataRow["UserCheck"] = gro.FK_UserTable;
                        dataRow["Email"] = (from a in kpiWebDataContext.UsersTable
                                            join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals b.FK_UserTable
                                            where a.UsersTableID == gro.FK_UserTable
                                            select a.Email).FirstOrDefault();
                        dataTable.Rows.Add(dataRow);
                    }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
                else
                {
                    RefreshGrid();
                }

                }
            }   

    protected void Button2_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
        }
 
    }
}
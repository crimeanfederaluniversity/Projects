using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;



namespace KPIWeb.AutomationDepartment
{
    public partial class ChangeAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridApdate();
        }
        protected void GridApdate()
        {
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/AutomationDepartment/UsersChangeAccess.aspx");
            }
            int userToChangeId = ser.Id;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("AfterAccess", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("EarlieAccess", typeof(bool)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UserGroupTable> groups =
                    (from a in kpiWebDataContext.UserGroupTable
                     join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UserGroupID equals
                          b.FK_GroupTable
                          join c in kpiWebDataContext.UsersTable on b.FK_UserTable equals c.UsersTableID
                     where a.Active == true
                     select a).Distinct().ToList();                
                foreach (var grouped in groups)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = grouped.UserGroupID;
                    dataRow["Name"] = grouped.UserGroupName;
                    UsersAndUserGroupMappingTable Earlie =(from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                   join b in kpiWebDataContext.UserGroupTable on a.FK_GroupTable equals b.UserGroupID
                                   join c in kpiWebDataContext.UsersTable on a.FK_UserTable equals c.UsersTableID
                                                     where a.Active == true && c.UsersTableID == userToChangeId && a.Confirmed == true && b.UserGroupID == grouped.UserGroupID
                                   select a).FirstOrDefault();
                    if (Earlie!=null)
                    { 
                    dataRow["EarlieAccess"] = true;
                    }
                    else
                    {
                        dataRow["EarlieAccess"] = false;
                    }

                    UsersAndUserGroupMappingTable after = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                                    join b in kpiWebDataContext.UserGroupTable on a.FK_GroupTable equals b.UserGroupID
                                                    join c in kpiWebDataContext.UsersTable on a.FK_UserTable equals c.UsersTableID
                                                           where a.Active == true && c.UsersTableID == userToChangeId && b.UserGroupID == grouped.UserGroupID
                                                    select a).FirstOrDefault();
                    if (after != null)
                    {
                        dataRow["AfterAccess"] = true;
                    }
                    else
                    {
                        dataRow["AfterAccess"] =false; 
                    }
                    
                    dataTable.Rows.Add(dataRow);
                }
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/AutomationDepartment/EditUser.aspx");
            }
            int userToChangeId = ser.Id;

            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                CheckBox confirmed = (CheckBox)GridView1.Rows[i].FindControl("AfterAccess");
                Label label = (Label)GridView1.Rows[i].FindControl("ID");

                UsersAndUserGroupMappingTable checkedgroups =
                    (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                     where a.Active == true
                           && a.FK_UserTable == userToChangeId
                           && a.UserAndGroupID == Convert.ToInt32(label.Text)
                     select a).FirstOrDefault();
                if (checkedgroups != null)
                {
                    checkedgroups.Confirmed = confirmed.Checked;
                    kpiWebDataContext.SubmitChanges();
                }
                else 
                { 
                    UsersAndUserGroupMappingTable usergroup = new UsersAndUserGroupMappingTable();
                    usergroup.FK_UserTable = userToChangeId;
                    usergroup.FK_GroupTable = Convert.ToInt32(label.Text);
                    usergroup.Active = true;
                    usergroup.Confirmed = confirmed.Checked;
                    kpiWebDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(usergroup);
                    kpiWebDataContext.SubmitChanges();
                }
            }

            Response.Redirect("~/AutomationDepartment/UsersChangeAccess.aspx");
        }
        }
    }
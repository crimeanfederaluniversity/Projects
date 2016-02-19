using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class ChangeAccess : System.Web.UI.Page
    {

             protected void Page_Load(object sender, EventArgs e)
        {
            GridApdate();
        }
        protected void GridApdate()
        {
            Serialization ser = (Serialization)Session["UserID"];
            int userToChangeId = ser.Id;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Access", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));

            using (PersonalPagesDataContext kpiWebDataContext = new PersonalPagesDataContext())
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
                                                     where a.Active == true && c.UsersTableID == userToChangeId && b.UserGroupID == grouped.UserGroupID
                                   select a).FirstOrDefault();
                    if (Earlie != null)
                    {
                        dataRow["Access"] = Earlie.Confirmed;
                    }
                    else
                    {
                        dataRow["Access"] = false;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization ser = (Serialization)Session["UserID"];
            int userToChangeId = ser.Id;
            PersonalPagesDataContext kpiWebDataContext = new PersonalPagesDataContext();

            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                CheckBox confirmed = (CheckBox)GridView1.Rows[i].FindControl("Access");
                Label label = (Label)GridView1.Rows[i].FindControl("ID");
                UsersAndUserGroupMappingTable checkedgroups = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                     where a.FK_UserTable == userToChangeId && a.UserAndGroupID == Convert.ToInt32(label.Text)
                     select a).FirstOrDefault();
                if (checkedgroups.FK_GroupTable != null)
                {
                    if (checkedgroups.Active == true && confirmed.Checked == false)
                    {
                        UserGroupTable name = (from a in kpiWebDataContext.UserGroupTable
                                               where a.UserGroupID == checkedgroups.FK_GroupTable
                                               select a).FirstOrDefault();
                        checkedgroups.Active = false;
                        checkedgroups.Confirmed = false;
                        kpiWebDataContext.SubmitChanges();
                        Aplications newkillaccess = new Aplications();
                        newkillaccess.Active = true;
                        newkillaccess.FK_ApplicationType = 12;
                        newkillaccess.FK_UserAdd = userToChangeId;
                        newkillaccess.Date = DateTime.Now;
                        newkillaccess.Text = "Закрыть доступ к " + name.UserGroupName.ToString();
                        kpiWebDataContext.Aplications.InsertOnSubmit(newkillaccess);
                        kpiWebDataContext.SubmitChanges();
                    }
                    if (checkedgroups.Active == false && confirmed.Checked == true)
                    {
                        UserGroupTable name = (from a in kpiWebDataContext.UserGroupTable
                                               where a.UserGroupID == checkedgroups.FK_GroupTable
                                               select a).FirstOrDefault();
                        checkedgroups.Active = true;
                        checkedgroups.Confirmed = false;
                        kpiWebDataContext.SubmitChanges();
                        Aplications newkillaccess = new Aplications();
                        newkillaccess.Active = true;
                        newkillaccess.FK_ApplicationType = 12;
                        newkillaccess.FK_UserAdd = userToChangeId;
                        newkillaccess.Date = DateTime.Now;
                        newkillaccess.Text = "Открыть доступ к " + name.UserGroupName.ToString();
                        kpiWebDataContext.Aplications.InsertOnSubmit(newkillaccess);
                        kpiWebDataContext.SubmitChanges();
                    }
                }
                else
                {
                    if (confirmed.Checked == true)
                    {
                        UsersAndUserGroupMappingTable usergroup = new UsersAndUserGroupMappingTable();
                        usergroup.FK_UserTable = userToChangeId;
                        usergroup.FK_GroupTable = Convert.ToInt32(label.Text);
                        usergroup.Active = true;
                        usergroup.Confirmed = false;
                        kpiWebDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(usergroup);
                        kpiWebDataContext.SubmitChanges();
                        UserGroupTable name = (from a in kpiWebDataContext.UserGroupTable
                                               where a.UserGroupID == usergroup.FK_GroupTable
                                               select a).FirstOrDefault();
                        Aplications newkillaccess = new Aplications();
                        newkillaccess.Active = true;
                        newkillaccess.FK_ApplicationType = 12;
                        newkillaccess.FK_UserAdd = userToChangeId;
                        newkillaccess.Date = DateTime.Now;
                        newkillaccess.Text = "Открыть доступ к " + name.UserGroupName.ToString();
                        kpiWebDataContext.Aplications.InsertOnSubmit(newkillaccess);
                        kpiWebDataContext.SubmitChanges();
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            Response.Redirect("~/Default.aspx");
        }


        }
    }

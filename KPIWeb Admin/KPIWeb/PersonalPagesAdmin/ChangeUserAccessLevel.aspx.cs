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
    public partial class ChangeUserAccessLevel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
          
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userToChangeId = ser.Id;
            if (!Page.IsPostBack)
            { 
            RefreshGrid();
            }
        }
        private void RefreshGrid()
        {
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userToChangeId = ser.Id;
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add(new DataColumn("UsersTableId", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("CheckedBox", typeof(bool)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UserGroupTable> groups;
                {
                    groups = (from a in kpiWebDataContext.UserGroupTable where a.Active==true
                              select a).ToList();
                }

                foreach (var gro in groups)
                {
                    DataRow dataRow = dataTable1.NewRow();
                    dataRow["UsersTableId"] = gro.UserGroupID;
                    dataRow["Name"] = gro.UserGroupName;
                    UsersAndUserGroupMappingTable check = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                  join b in kpiWebDataContext.UsersTable on a.FK_UserTable equals b.UsersTableID
                                  join c in kpiWebDataContext.UserGroupTable on a.FK_GroupTable equals c.UserGroupID
                                  where
                                  a.Active == true && b.UsersTableID == userToChangeId && c.UserGroupID==gro.UserGroupID
                                  select a).FirstOrDefault();
                    if (check == null || check.Confirmed == false)
                    {
                        dataRow["CheckedBox"] = false;
                    }
                    else
                    {
                        dataRow["CheckedBox"] = true;
                    }                    
                    dataTable1.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable1;
                GridView1.DataBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userToChangeId = ser.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox canEdit = (CheckBox)GridView1.Rows[i].FindControl("CheckedBox");
                Label label = (Label)GridView1.Rows[i].FindControl("UsersTableId");

                if (canEdit.Checked == true)
                {
                    UsersAndUserGroupMappingTable userAcc =
                    (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                     where a.FK_GroupTable == Convert.ToInt32(label.Text) && a.FK_UserTable == userToChangeId
                     select a).FirstOrDefault();
                    if (userAcc != null)
                    {
                        userAcc.Confirmed = true;
                        userAcc.Active = true;
                        kpiWebDataContext.SubmitChanges();
                    }
                    else
                    {
                        UsersAndUserGroupMappingTable userGroup = new UsersAndUserGroupMappingTable();
                        userGroup.FK_GroupTable = Convert.ToInt32(label.Text);
                        userGroup.FK_UserTable = userToChangeId;
                        userGroup.Active = true;
                        userGroup.Confirmed = true;
                        kpiWebDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(userGroup);
                        kpiWebDataContext.SubmitChanges();
                    }
                }
                else
                {
                    UsersAndUserGroupMappingTable userAcc =
                      (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                       where a.FK_GroupTable == Convert.ToInt32(label.Text) && a.FK_UserTable == userToChangeId
                       select a).FirstOrDefault();
                    if (userAcc != null)
                    {
                        userAcc.Confirmed = false;
                        userAcc.Active = true;
                        kpiWebDataContext.SubmitChanges();
                    }
                }               
            }
            RefreshGrid();
        }
       
    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class ChangeUserAccessLevel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
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


            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("Default.aspx");
            }
            int userToChangeId = ser.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<UserGroupTable> allgroups = (from a in kpiWebDataContext.UserGroupTable
                                              where a.Active == true
                                              join b in kpiWebDataContext.Projects
                                                  on a.Fk_ProjectsTable equals b.Id
                                              where b.Active == true
                                              select a).ToList();
            foreach (UserGroupTable n in allgroups)
            {
                ListItem TmpItem = new ListItem();
                TmpItem.Text = n.UserGroupName;
                TmpItem.Value = n.UserGroupID.ToString();
                if ((from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                     where a.FK_GroupTable == n.UserGroupID
                           && a.FK_UserTable == userToChangeId
                           && a.Active == true
                     select a).Any())
                    TmpItem.Selected = true;
                CheckBoxList1.Items.Add(TmpItem);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("Default.aspx");
            }
            int userToChangeId = ser.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            foreach (ListItem n in CheckBoxList1.Items)
            {
                UsersAndUserGroupMappingTable useraccess =
                    (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                     where a.FK_GroupTable == Convert.ToInt32(n.Value)
                     select a).FirstOrDefault();
                if (useraccess != null)
                {
                    if (n.Selected == false)
                    {
                        useraccess.Active = false;
                        kpiWebDataContext.SubmitChanges();
                    }
                    if (n.Selected == true)
                    {
                        useraccess.Active = true;
                        kpiWebDataContext.SubmitChanges();
                    }
                }
                else
                {
                    UsersAndUserGroupMappingTable newuseraccess = new UsersAndUserGroupMappingTable();
                    if (n.Selected == false)
                    {
                        newuseraccess.Active = false;
                    }
                    if (n.Selected == true)
                    {
                        newuseraccess.Active = true;
                    }
                    newuseraccess.FK_GroupTable = Convert.ToInt32(n.Value);
                    newuseraccess.FK_UserTable = userToChangeId;
                    kpiWebDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(newuseraccess);
                    kpiWebDataContext.SubmitChanges();

                }

            }
        }
    }
}


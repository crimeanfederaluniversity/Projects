using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PersonalPages
{
    public partial class UserMainPage : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();

            UsersTable user = (from usersTables in usersDB.UsersTable
                               where usersTables.UsersTableID==userID
                               && usersTables.Active == true
                               select usersTables).FirstOrDefault();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("GroupId", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ProjectName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("GroupName", typeof(string)));

            List<UserGroupTable> userGroups = (from a in usersDB.UserGroupTable
                where a.Active == true
                join b in usersDB.UsersAndUserGroupMappingTable
                    on a.UserGroupID equals b.FK_GroupTable
                where b.Active == true
                      && b.FK_UserTable == userID 
                select a).Distinct().ToList();

            foreach (UserGroupTable groups in userGroups)
            {
                Projects project = (from a in usersDB.Projects
                    where a.Id == groups.Fk_ProjectsTable
                          && a.Active == true
                    select a).FirstOrDefault();
                if (project == null ) continue;
                DataRow dataRow = dataTable.NewRow();
                dataRow["GroupId"] = groups.UserGroupID;
                dataRow["GroupName"] = groups.UserGroupName;
                dataRow["ProjectName"] = project.ProjectName;
                dataTable.Rows.Add(dataRow);
            }
            GroupsGridView.DataSource = dataTable;
            GroupsGridView.DataBind();
            /*if (user.RatingCheck == false || user.RatingCheck == null)
            {
                Button1.Enabled = false;
            }
            if (user.CompetitionCheck == false || user.CompetitionCheck == null)
            {
                Button2.Enabled = false;
            }
            if (user.IndicatorCheck == false || user.IndicatorCheck == null)
            {
                Button3.Enabled = false;
            }
            if (user.MonitorCheck == false || user.MonitorCheck == null)
            {
                Button4.Enabled = false;
            }
            if (user.DocumentCheck == false || user.DocumentCheck == null)
            {
                Button5.Enabled = false;
            }
            if (user.LibraryCheck == false || user.LibraryCheck == null)
            {
                Button6.Enabled = false;
            }
            if (user.RepozitiryCheck == false || user.RepozitiryCheck == null)
            {
                Button7.Enabled = false;
            }*/
            /*
            Label1.Text =user.AcademicDegree + " "+ user.Surname + " " + user.Name.Remove(2) + ". " + user.Patronimyc.Remove(2) + ". " ;*/
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherPage.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {

        }
        protected void RedirectToSubdomain(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            Button button = (Button)sender;

            if (UserSer != null)
            {
                int userId = UserSer.Id;
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                UserGroupTable group = (from a in usersDB.UserGroupTable
                    where a.UserGroupID == Convert.ToInt32(button.CommandArgument)
                    select a).FirstOrDefault();

                if (group != null)
                {
                    if (group.AutoLogin == true)
                    {
                        SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
                        Response.Redirect(subdomainRedirect.CreateLinkToSubdomain(group.URLtoGroupMain, userId, 10));
                    }
                    else
                    {
                        Response.Redirect(group.URLtoGroupMain);
                    }
                }
            }
        }
    }
}
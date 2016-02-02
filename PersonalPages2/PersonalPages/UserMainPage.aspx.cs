using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PersonalPages
{
    public partial class UserMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            // int userID = 12264;

            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            UsersTable user = (from usersTables in usersDB.UsersTable
                where usersTables.UsersTableID == userID
                      && usersTables.Active == true
                select usersTables).FirstOrDefault();

            StudentsTable stud = (from usersTables in usersDB.StudentsTable
                where usersTables.StudentsTableID == userID
                      && usersTables.Active == true
                select usersTables).FirstOrDefault();
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("ProjectName", typeof (string)));

            if (user != null && stud == null)
            {
                List<Projects> userGroups = (from a in usersDB.Projects
                    join z in usersDB.UserGroupTable on
                        a.Id equals z.Fk_ProjectsTable
                    join c in usersDB.UsersAndUserGroupMappingTable
                        on z.UserGroupID equals c.FK_GroupTable
                    join d in usersDB.UsersTable
                        on c.FK_UserTable equals d.UsersTableID
                     where a.Active == true && c.FK_UserTable == userID && z.Active == true && c.Active == true && c.Confirmed==true
                    select a).Distinct().ToList();
                foreach (var name in userGroups)
                {
                    if (name.Id < 21 || name.Id > 25)
                    { 
                    if (userGroups.Count > 6)
                    {
                        Panel.Height = 470;
                    }
                    Label lb1 = new Label();
                    lb1.Text = "&nbsp;&nbsp;";
                    Panel.Controls.Add(lb1);
                    ImageButton newBox = new ImageButton();
                    newBox.ImageUrl = name.ImageUrl;
                    newBox.ID = "box" + name.Id;
                    newBox.Height = 130;
                    newBox.Width = 130;
                    newBox.AlternateText = Convert.ToString(name.Id);
                    newBox.Click += new ImageClickEventHandler(this.RedirectToSubdomain);
                    Panel.Controls.Add(newBox);
                    Label lb = new Label();
                    lb.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    Panel.Controls.Add(lb);
                }
                }
            }        
      if ( user  == null && stud != null)
        {
            List<Projects> userGroups = (from a in usersDB.Projects
                                         join z in usersDB.UserGroupTable on
                                             a.Id equals z.Fk_ProjectsTable
                                               join c in usersDB.StudentsAndUserGroupMappingTable
                                                   on z.UserGroupID equals c.FK_GroupUserTable
                                               join d in usersDB.StudentsTable
                                                   on c.FK_StudentTable equals d.StudentsTableID
                                         where a.Active == true && c.FK_StudentTable == userID && z.Active == true && c.Active == true
                                               select a).Distinct().ToList();
            if (userGroups.Count > 12)
            {
                Panel.Height = 470;
            }
            foreach (var name in userGroups)
            {
                if (name.Id < 21 || name.Id > 25)
                {
                    Label lb1 = new Label();
                    lb1.Text = "&nbsp;&nbsp;";
                    Panel.Controls.Add(lb1);
                    ImageButton newBox = new ImageButton();
                    newBox.ImageUrl = name.ImageUrl;
                    newBox.ID = "box" + name.Id;
                    newBox.Height = 130;
                    newBox.Width = 130;
                    newBox.AlternateText = Convert.ToString(name.Id);
                    newBox.Click += new ImageClickEventHandler(this.RedirectToSubdomain);
                    Panel.Controls.Add(newBox);
                    Label lb = new Label();
                    lb.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    Panel.Controls.Add(lb);
                }
            }

        }
    }
       
             protected void RedirectToSubdomain(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            ImageButton button = (ImageButton)sender;
            if (UserSer != null)
            {
                int userId = UserSer.Id;
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                int vhod = (from a in usersDB.UserGroupTable
                                             where a.Active == true  
                                                  join b in usersDB.UsersAndUserGroupMappingTable
                                                  on a.UserGroupID equals b.FK_GroupTable
                            where b.Active == true && b.FK_UserTable == userId && a.Fk_ProjectsTable == Convert.ToInt32(button.AlternateText)
                                                  select a).Count();
                if (vhod != null && vhod == 1)
                {                   
                        UserGroupTable autolog =
                            (from a in usersDB.UserGroupTable
                             where a.Fk_ProjectsTable == Convert.ToInt32(button.AlternateText)
                                select a).FirstOrDefault();
                
                    if (autolog.AutoLogin == true)
                    {
                        SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
                        Response.Redirect(subdomainRedirect.CreateLinkToSubdomain(autolog.URLtoGroupMain, userId, 10));
                    }
                    else
                    {
                        Response.Redirect(autolog.URLtoGroupMain);
                    }                
                    }

                    if (vhod != null && vhod> 1)
                    {
                        UserGroupTable groupID = (from a in usersDB.UserGroupTable
                                                  where a.Active == true && a.Fk_ProjectsTable == Convert.ToInt32(button.AlternateText)
                            select a).FirstOrDefault();
                        if (groupID!=null)
                        { 
                        Serialization GroupId = new Serialization(Convert.ToInt32(groupID.Fk_ProjectsTable));
                        Session["GroupID"] = GroupId;
                        }
                        Response.Redirect("TransitionPage.aspx");
                    }               
            }
        }
    }
}
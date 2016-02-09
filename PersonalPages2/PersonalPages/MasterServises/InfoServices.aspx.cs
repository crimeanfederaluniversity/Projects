using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages.MasterServises
{
    public partial class InfoServices : System.Web.UI.Page
    {

            protected void Page_Load(object sender, EventArgs e)
        {
            int gID = 24;

            Serialization UserSer = (Serialization)Session["UserID"];
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

           
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("ProjectName", typeof(string)));

            if (user != null)
            {
                List<UserGroupTable> userGroups = (from a in usersDB.UserGroupTable
                                                   join c in usersDB.UsersAndUserGroupMappingTable
                                                   on a.UserGroupID equals c.FK_GroupTable
                                                   join d in usersDB.UsersTable
                                                   on c.FK_UserTable equals d.UsersTableID
                                                   join z in usersDB.Projects on
                                                   a.Fk_ProjectsTable equals z.Id
                                                   where a.Active == true && c.FK_UserTable == userID && z.Active == true && c.Active == true
                                                   && a.Fk_ProjectsTable == gID
                                                   select a).Distinct().ToList();
                Label lb2 = new Label();
                lb2.Text = "<br />";
                Panel1.Controls.Add(lb2);
                foreach (var name in userGroups)
                {
                    Label lb1 = new Label();
                    lb1.Text = "&nbsp;&nbsp;";
                    Panel1.Controls.Add(lb1);
                    ImageButton newBox = new ImageButton();
                    newBox.ImageUrl = name.ImageUrl;
                    newBox.ID = "box" + name.UserGroupID;
                    newBox.Height = 130;
                    newBox.Width = 130;
                    newBox.ToolTip = name.UserGroupName;
                    newBox.AlternateText = name.URLtoGroupMain;
                    newBox.Click += new ImageClickEventHandler(this.RedirectToSubdomain);
                    Panel1.Controls.Add(newBox);
                    Label lb = new Label();
                    lb.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    Panel1.Controls.Add(lb);
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
                    UserGroupTable autolog =
                        (from a in usersDB.UserGroupTable
                         where a.URLtoGroupMain == button.AlternateText
                         select a).FirstOrDefault();

                    if (autolog.AutoLogin == true)
                    {
                        SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
                        Response.Redirect(subdomainRedirect.CreateLinkToSubdomain(button.AlternateText, userId, 10));
                    }
                    else
                    {
                        Response.Redirect(button.AlternateText);
                    }
                }
            }
        }
    }

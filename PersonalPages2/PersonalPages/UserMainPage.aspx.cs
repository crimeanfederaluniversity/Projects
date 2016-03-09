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
        
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            UsersTable user = (from usersTables in usersDB.UsersTable where usersTables.UsersTableID == userID
                                             && usersTables.Active == true select usersTables).FirstOrDefault();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ProjectName", typeof (string)));

            if (user != null )
            {
                List<UsersAndUserGroupMappingTable>  confirmed = (from c in usersDB.UsersAndUserGroupMappingTable  
                    where c.Confirmed==true && c.Active == true && c.FK_UserTable == userID select c).Distinct().ToList();
                List<Projects> allprojects = new List<Projects>();
                foreach (var projectname in confirmed)
                {
                    Projects name = (from a in usersDB.Projects where a.Active == true
                                     join z in usersDB.UserGroupTable on a.Id equals z.Fk_ProjectsTable 
                                     where z.Active == true && z.UserGroupID == projectname.FK_GroupTable
                    select a).FirstOrDefault();

                    allprojects.Add(name);
                }

                Label lb2 = new Label();
                lb2.Text = "<br />";
                Panel.Controls.Add(lb2);
                if (allprojects != null)
                { 
                foreach (var names in allprojects)
                {
                        if (names!=null)
                        { 
                    if (names.Id < 8 || names.Id > 12)
                    {
                        if (allprojects.Count > 6)
                    {
                        Panel.Height = 470;
                    }
                    Label lb1 = new Label();
                    lb1.Text = "&nbsp;&nbsp;";
                    Panel.Controls.Add(lb1);
                    ImageButton newBox = new ImageButton();
                    newBox.ImageUrl = names.ImageUrl;
                    newBox.ID = "box" + names.Id;
                    newBox.Height = 130;
                    newBox.Width = 130;
                    newBox.ToolTip = names.ProjectName;
                    newBox.AlternateText = Convert.ToString(names.Id);
                    newBox.Click += new ImageClickEventHandler(this.RedirectToSubdomain);
                    Panel.Controls.Add(newBox);
                    Label lb = new Label();
                    lb.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    Panel.Controls.Add(lb);
                            }
                        }
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
                int vhod = (from a in usersDB.UserGroupTable  where a.Active == true  
                                                  join b in usersDB.UsersAndUserGroupMappingTable on a.UserGroupID equals b.FK_GroupTable
                                                  where b.Active == true && b.FK_UserTable == userId && a.Fk_ProjectsTable == Convert.ToInt32(button.AlternateText)
                                                  select a).Count();
                if (vhod != null && vhod == 1)
                {                   
                        UserGroupTable autolog =  (from a in usersDB.UserGroupTable where a.Fk_ProjectsTable == Convert.ToInt32(button.AlternateText) select a).FirstOrDefault();
                
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
                        UserGroupTable groupID = (from a in usersDB.UserGroupTable where a.Active == true && a.Fk_ProjectsTable == Convert.ToInt32(button.AlternateText)
                            select a).FirstOrDefault();
                        if (groupID!=null)
                        { 
                        Serialization GroupId = new Serialization(Convert.ToInt32(groupID.Fk_ProjectsTable));
                        Session["GroupID"] = GroupId;
                        }
                        Response.Redirect("~/TransitionPage.aspx");
                    }               
            }
        }

             protected void Button1_Click(object sender, EventArgs e)
             {
                 Response.Redirect("~/1CForm.aspx");
             }
             protected void Button2_Click(object sender, EventArgs e)
             {
                 Response.Redirect("~/CardOrder.aspx");
             }

             protected void Button3_Click(object sender, EventArgs e)
             {
                 Response.Redirect("~/EquipmentWriteOff.aspx");
             }             
    }
}
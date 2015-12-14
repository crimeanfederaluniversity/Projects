using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class CreatePersonalPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                CheckBoxList1.Visible = true;
                Label1.Visible = true;
            }
            else
            {
                Label1.Visible = false;
                CheckBoxList1.Visible = false;                    
                }
            if (!(Page.IsPostBack))
            {            
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<UserGroupTable> allgroups = (from a in kPiDataContext.UserGroupTable
                    where a.Active == true
                    join b in kPiDataContext.Projects
                        on a.Fk_ProjectsTable equals b.Id
                    where b.Active == true
                    select a).ToList();
                foreach (UserGroupTable n in allgroups)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.UserGroupName;
                    TmpItem.Value = n.UserGroupID.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            if (RadioButtonList1.SelectedValue == "1")
            {

                if ((from a in kPiDataContext.UsersTable where a.Email == EmailText.Text && a.Active == true select a)
                    .ToList().Count > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                        "alert('Введенный адрес электронной почты уже зарегестрирован, введите другой');", true);
                }
                else
                {
                    UsersTable user = new UsersTable();
                    user.Active = true;
                    user.Email = EmailText.Text;
                    kPiDataContext.UsersTable.InsertOnSubmit(user);
                    kPiDataContext.SubmitChanges();

                    foreach (ListItem n in CheckBoxList1.Items)
                    {
                        if (n.Selected == true)
                        {
                            UsersAndUserGroupMappingTable newuseraccess = new UsersAndUserGroupMappingTable();
                            newuseraccess.Active = true;
                            newuseraccess.FK_GroupTable = Convert.ToInt32(n.Value);
                            newuseraccess.FK_UserTable = user.UsersTableID;
                            kPiDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(newuseraccess);
                            kPiDataContext.SubmitChanges();
                        }
                    }
                }
            }
            if (RadioButtonList1.SelectedValue == "0")
            {
                if ((from a in kPiDataContext.StudentsTable where a.Email == EmailText.Text && a.Active == true select a).ToList().Count > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                        "alert('Введенный адрес электронной почты уже зарегестрирован, введите другой');", true);
                }
                else
                {
                    StudentsTable student = new StudentsTable();
                    student.Active = true;
                    student.Email = EmailText.Text;
                    kPiDataContext.StudentsTable.InsertOnSubmit(student);
                    kPiDataContext.SubmitChanges();
                
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                    "alert('Пользователь добавлен!');", true);
                }
            }
        }
    }

}
    

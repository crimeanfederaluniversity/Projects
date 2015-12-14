using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb
{
    public partial class CreateNewModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Id", typeof (string)));
            dataTable.Columns.Add(new DataColumn("ProjectName", typeof (string)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<Projects> projects;
                {
                    projects = (from a in kpiWebDataContext.Projects where a.Active == true select a).ToList();
                }

                foreach (var user in projects)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Id"] = user.Id;
                    dataRow["ProjectName"] = user.ProjectName;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Projects newlink = new Projects();
            newlink.Active = true;
            newlink.ProjectName = ModuleName.Text;
            newlink.CreateButton = true;
            kPiDataContext.Projects.InsertOnSubmit(newlink);
            kPiDataContext.SubmitChanges();

            UserGroupTable newmodule = new UserGroupTable();
            newmodule.Active = true;
            newmodule.Fk_ProjectsTable = newlink.Id;
            newmodule.URLtoGroupMain = ModuleLink.Text;
            newmodule.UserGroupName = ModuleName.Text;
            kPiDataContext.UserGroupTable.InsertOnSubmit(newmodule);
            kPiDataContext.SubmitChanges();

            if (CheckBox1.Checked == true)
            {
                List<StudentsTable> allstudent =
                    (from a in kPiDataContext.StudentsTable where a.Active == true select a).ToList();
                foreach (StudentsTable n in allstudent)
                {
                    StudentsAndUserGroupMappingTable newstudentaccess = new StudentsAndUserGroupMappingTable();
                    newstudentaccess.Active = true;
                    newstudentaccess.FK_GroupUserTable = newmodule.UserGroupID;
                    newstudentaccess.FK_StudentTable = n.StudentsTableID;
                    kPiDataContext.StudentsAndUserGroupMappingTable.InsertOnSubmit(newstudentaccess);
                    kPiDataContext.SubmitChanges();
                }
            }
            if (CheckBox2.Checked == true)
            {
                List<UsersTable> alluser =
                    (from a in kPiDataContext.UsersTable where a.Active == true select a).ToList();
                foreach (UsersTable n in alluser)
                {
                    UsersAndUserGroupMappingTable newuseraccess = new UsersAndUserGroupMappingTable();
                    newuseraccess.Active = true;
                    newuseraccess.FK_GroupTable = newmodule.UserGroupID;
                    newuseraccess.FK_UserTable = n.UsersTableID;
                    kPiDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(newuseraccess);
                    kPiDataContext.SubmitChanges();

                }
            }
            Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                "alert('Модуль добавлен для выбранной группы пользователей!');", true);
        }

        protected void DeleteButtonClick(object sender, EventArgs e)
        {
                Button button = (Button) sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        Projects delete =
                            (from a in kPiDataContext.Projects
                                where a.Id == Convert.ToInt32(button.CommandArgument)
                                select a).FirstOrDefault();

                        delete.Active = false;
                        kPiDataContext.SubmitChanges();
                    }
                }
            }
        }
    }

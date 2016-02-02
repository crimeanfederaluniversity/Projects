using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;
namespace KPIWeb.AutomationDepartment
{
    public partial class UsersChangeAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridApdate();
        }

        protected void GridApdate()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Mail", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("From", typeof(string)));
            dataTable.Columns.Add(new DataColumn("UserButton", typeof(int)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UsersTable> users =
                    (from a in kpiWebDataContext.UsersTable
                       join b in kpiWebDataContext.UsersAndUserGroupMappingTable on a.UsersTableID equals
                            b.FK_UserTable
                     where a.Active == true && b.Confirmed == false && b.Active == true 
                        select a).Distinct().ToList();

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = user.UsersTableID;
                    dataRow["Mail"] = user.Email;
                    dataRow["From"] = (from a in kpiWebDataContext.UsersTable 
                                                      join b in kpiWebDataContext.FirstLevelSubdivisionTable on a.FK_FirstLevelSubdivisionTable equals b.FirstLevelSubdivisionTableID
                                                      join c in kpiWebDataContext.SecondLevelSubdivisionTable on a.FK_SecondLevelSubdivisionTable equals c.SecondLevelSubdivisionTableID
                                                      join d in kpiWebDataContext.ThirdLevelSubdivisionTable on a.FK_ThirdLevelSubdivisionTable equals d.ThirdLevelSubdivisionTableID
                                                      where a.Active==true && a.UsersTableID==user.UsersTableID select b.Name + " "+ c.Name+ " "+ d.Name).FirstOrDefault();
                    dataRow["UserButton"] = user.UsersTableID;

                    dataTable.Rows.Add(dataRow);
                }

                List<StudentsTable> students =
                    (from a in kpiWebDataContext.StudentsTable
                     join b in kpiWebDataContext.StudentsAndUserGroupMappingTable on a.StudentsTableID equals
                         b.FK_StudentTable
                     where a.Active == true && b.Active == true && b.Confirmed == false
                     select a).Distinct().ToList();
                foreach (var stud in students)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = stud.StudentsTableID;
                    dataRow["Mail"] = stud.Email;
                    dataRow["From"] = (from a in kpiWebDataContext.StudentsTable
                                       join b in kpiWebDataContext.FirstLevelSubdivisionTable on a.FK_FirstLevelSubdivision equals b.FirstLevelSubdivisionTableID
                                       join c in kpiWebDataContext.SecondLevelSubdivisionTable on a.FK_SecondLevelSubdivision equals c.SecondLevelSubdivisionTableID
                                       join d in kpiWebDataContext.GroupsTable on a.FK_Group equals d.ID
                                       where a.Active == true && a.StudentsTableID==stud.StudentsTableID
                                       select b.Name + " " + c.Name + " " + d.Name).FirstOrDefault();
                    dataRow["UserButton"] = stud.StudentsTableID;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void ChangeUserButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization ser = new Serialization(Convert.ToInt32(button.CommandArgument));
                Session["userIdforChange"] = ser;
                Response.Redirect("~/AutomationDepartment/ChangeAccess.aspx");
            }
        }
    }
}
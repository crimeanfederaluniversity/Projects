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
using Microsoft.Ajax.Utilities;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class UserChangePesonalPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            if(!Page.IsPostBack)
            {
                RefreshGrid();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        protected void RefreshGrid()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("UsersTableId", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ParamIdToChange", typeof(int)));
            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UserDataChangeHistory> UserHistory = 
                    (from a in kpiWebDataContext.UserDataChangeHistory
                                                                         join b in kpiWebDataContext.UsersTable on a.FK_User equals b.UsersTableID
                                                                         where a.Active == true && a.Status == 0 && 
                                                                         (b.Email.Contains(TextBox2.Text) || (TextBox2.Text.Length<2))
                                                                         select a).ToList();
                List<StudentChangeDataHistoryTable> StudentHistory =
                    (from a in kpiWebDataContext.StudentChangeDataHistoryTable
                                  join b in kpiWebDataContext.StudentsTable on a.FK_StudentTable
                                  equals b.StudentsTableID
                                  where a.Active == true && a.Status == 0
                                      && (b.Email.Contains(TextBox2.Text) || (TextBox2.Text.Length<2))
                                  select a).ToList();

                foreach (var user in UserHistory)
                {
                    UsersTable user_ = (from a in kpiWebDataContext.UsersTable where a.Active==true && a.UsersTableID==user.FK_User select a).FirstOrDefault();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UsersTableId"] = user_.UsersTableID;
                    dataRow["Email"] = user_.Email;
                    dataRow["ParamIdToChange"] = user.ID_Param_ToChange;
                    dataRow["Name"] = user.Name + " с " + user.OldValue + "на" + user.NewValue;
                    dataTable.Rows.Add(dataRow);
                }
                foreach (var stud in StudentHistory)
                {
                    StudentsTable stud_ = (from a in kpiWebDataContext.StudentsTable where a.Active == true && a.StudentsTableID == stud.FK_StudentTable select a).FirstOrDefault();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UsersTableId"] = stud_.StudentsTableID;
                    dataRow["Email"] = stud_.Email;
                    dataRow["ParamIdToChange"] = stud.ID_Param_ToChange;
                    dataRow["Name"] = stud.Name + " с " + stud.OldValue + "на" + stud.NewValue;
                    dataTable.Rows.Add(dataRow);             
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void ConfirmButtonClick(object sender, EventArgs e)
        {
            if (!CheckBox2.Checked)
            {
                Button button = (Button)sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        UserDataChangeHistory userData =
                            (from a in kPiDataContext.UserDataChangeHistory
                             where a.Active==true && a.ID_Param_ToChange == Convert.ToInt32(button.CommandArgument) && a.Status==0
                             select a).FirstOrDefault();
                        StudentChangeDataHistoryTable  student =
                            (from a in kPiDataContext.StudentChangeDataHistoryTable
                             where a.ID_Param_ToChange == Convert.ToInt32(button.CommandArgument) && a.Status == 0 && a.Active==true
                             select a).FirstOrDefault();
                        if (userData != null)
                        {
                            UsersTable user = (from a in kPiDataContext.UsersTable
                                               where a.UsersTableID == userData.FK_User && a.Active == true
                                               select a).FirstOrDefault();
                            switch (userData.ID_Param_ToChange)
                            {
                                case 1: user.Name = userData.NewValue;
                                    break;
                                case 2: user.Surname = userData.NewValue;
                                    break;
                                case 3: user.Patronimyc = userData.NewValue;
                                    break;
                                case 4: user.Email = userData.NewValue;
                                    break;
                                case 5: user.FK_FirstLevelSubdivisionTable = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                                                              where a.Active == true &&
                                                                                  a.Name == userData.NewValue
                                                                              select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                                    break;
                                case 6: user.FK_SecondLevelSubdivisionTable = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                                                               where a.Active == true &&
                                                                                   a.Name == userData.NewValue
                                                                               select a.SecondLevelSubdivisionTableID).FirstOrDefault();
                                    break;
                                case 7: user.FK_ThirdLevelSubdivisionTable = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                                                              where a.Active == true &&
                                                                                  a.Name == userData.NewValue
                                                                              select a.ThirdLevelSubdivisionTableID).FirstOrDefault();
                                    break;
                                default:
                                    break;
                            }
                            userData.Status = 1;
                            kPiDataContext.SubmitChanges();
                        }
                        else if (student != null)
                        {
                            StudentsTable studen = (from a in kPiDataContext.StudentsTable
                                               where a.StudentsTableID == userData.FK_User && a.Active==true
                                               select a).FirstOrDefault();
                            switch (userData.ID_Param_ToChange)
                            {
                                case 1: studen.Name = student.NewValue;
                                    break;
                                case 2:  studen.Surname = student.NewValue;
                                    break;
                                case 3:  studen.Patronimyc = student.NewValue;
                                    break;
                                case 4: studen.Email = student.NewValue;
                                    break;
                                case 5: studen.FK_FirstLevelSubdivision = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                                                           where a.Active == true &&
                                                                               a.Name == student.NewValue
                                                                           select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                                    break;
                                case 6: studen.FK_SecondLevelSubdivision = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                                                            where a.Active == true &&
                                                                                a.Name == student.NewValue
                                                                            select a.SecondLevelSubdivisionTableID).FirstOrDefault();
                                    break;
                                case 7: studen.FK_Group = (from a in kPiDataContext.GroupsTable
                                                           where a.Active == true &&
                                                               a.Name == student.NewValue
                                                           select a.ID).FirstOrDefault();
                                    break;
                                default :break;
                                    
                            }
                            userData.Status = 1;
                            kPiDataContext.SubmitChanges();
                        }
                       
                    }
                    RefreshGrid();

                }
            }

        }

        protected void DenyButtonClick(object sender, EventArgs e)
        {
            if (!CheckBox2.Checked)
            {
                Button button = (Button) sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        UserDataChangeHistory userData =
                            (from a in kPiDataContext.UserDataChangeHistory
                                where
                                    a.Active == true && a.ID_Param_ToChange == Convert.ToInt32(button.CommandArgument) &&
                                    a.Status == 0
                                select a).FirstOrDefault();
                        StudentChangeDataHistoryTable student =
                            (from a in kPiDataContext.StudentChangeDataHistoryTable
                                where
                                    a.ID_Param_ToChange == Convert.ToInt32(button.CommandArgument) && a.Status == 0 &&
                                    a.Active == true
                                select a).FirstOrDefault();
                        if (userData != null)
                        {
                            userData.Status = 2;
                        }
                        else if (student != null)
                        {
                            userData.Status = 2;
                        }
                        kPiDataContext.SubmitChanges();
                    }
                    RefreshGrid();
                }
            }
        }
        
        protected void ChangeUserButton(object sender, EventArgs e)
        { 
        
        }
    }
}
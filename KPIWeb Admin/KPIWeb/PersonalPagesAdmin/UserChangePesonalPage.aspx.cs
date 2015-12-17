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
    public partial class UserChangePesonalPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            dataTable.Columns.Add(new DataColumn("UsersTableId", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ConfirmButtonClick", typeof(string)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UserDataChangeHistory> UserHistory = new List<UserDataChangeHistory>();
                List<StudentChangeDataHistoryTable> StudentHistory = new List<StudentChangeDataHistoryTable>();

                if (TextBox2.Text.Length < 2)
                {
                     UserHistory = (from a in kpiWebDataContext.UserDataChangeHistory where a.Active == true && a.Status == false select a).ToList();
                     StudentHistory = (from a in kpiWebDataContext.StudentChangeDataHistoryTable where a.Active == true && a.Status == false select a).ToList();
                }
                else
                {
                    UserHistory = (from a in kpiWebDataContext.UserDataChangeHistory
                                                               join b in kpiWebDataContext.UsersTable on a.FK_User equals b.UsersTableID 
                                                               where a.Active == true && a.Status == false && b.Email.Contains(TextBox2.Text) 
                                                               select a).ToList();
                    StudentHistory = (from a in kpiWebDataContext.StudentChangeDataHistoryTable
                                                                          join b in kpiWebDataContext.StudentsTable on a.FK_StudentTable 
                                                                          equals b.StudentsTableID where a.Active == true && a.Status == false
                                                                          && b.Email.Contains(TextBox2.Text)
                                                                          select a).ToList();
                    
                }


                foreach (var user in UserHistory)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UsersTableId"] = (from a in kpiWebDataContext.UsersTable where a.Active==true && a.UsersTableID==user.FK_User select a.UsersTableID).FirstOrDefault();
                    dataRow["Email"] = (from a in kpiWebDataContext.UsersTable where a.Active == true && a.UsersTableID == user.FK_User select a.Email).FirstOrDefault();
                    dataRow["ConfirmButtonClick"] = user.ID_Param_ToChange;
                    dataRow["Name"] = user.Name;
                    dataTable.Rows.Add(dataRow);
                }
                foreach (var stud in StudentHistory)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UsersTableId"] = (from a in kpiWebDataContext.StudentsTable where a.Active == true && a.StudentsTableID == stud.FK_StudentTable select a.StudentsTableID).FirstOrDefault();
                    dataRow["Email"] = (from a in kpiWebDataContext.StudentsTable where a.Active == true && a.StudentsTableID == stud.FK_StudentTable select a.StudentsTableID).FirstOrDefault();
                    dataRow["ConfirmButtonClick"] = stud.ID_Param_ToChange;
                    dataRow["Name"] = stud.Name;
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
                             where a.Active==true && a.ID_Param_ToChange == Convert.ToInt32(button.CommandArgument) && a.Status==false
                             select a).FirstOrDefault();
                        StudentChangeDataHistoryTable  student =
                            (from a in kPiDataContext.StudentChangeDataHistoryTable
                             where a.ID_Param_ToChange == Convert.ToInt32(button.CommandArgument) && a.Status == false&& a.Active==true
                             select a).FirstOrDefault();
                        if (userData != null)
                        {
                            UsersTable user = (from a in kPiDataContext.UsersTable
                                               where a.UsersTableID == userData.FK_User && a.Active == true
                                               select a).FirstOrDefault();
                            if (userData.ID_Param_ToChange == 1)
                            {
                                user.Name = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                                
                            }
                            if (userData.ID_Param_ToChange == 2)
                            {
                                user.Surname = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 3)
                            {
                                user.Patronimyc = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 4)
                            {
                                user.Email = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 5)
                            {
                                string afds = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                                user.FK_FirstLevelSubdivisionTable = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                                                      where a.Active == true &&
                                                                          a.Name == afds
                                                                      select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                                   
                            }
                            if (userData.ID_Param_ToChange == 6)
                            {
                                user.FK_SecondLevelSubdivisionTable = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                                                      where a.Active == true &&
                                                                          a.Name == userData.Name.Substring(userData.Name.IndexOf(" на ") + 4)
                                                                      select a.SecondLevelSubdivisionTableID).FirstOrDefault();

                            }
                            if (userData.ID_Param_ToChange == 7)
                            {
                                user.FK_ThirdLevelSubdivisionTable = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                                                       where a.Active == true &&
                                                                           a.Name == userData.Name.Substring(userData.Name.IndexOf(" на ") + 4)
                                                                       select a.ThirdLevelSubdivisionTableID).FirstOrDefault();
                            }
                            userData.Status = true;
                            UserDataChangeHistory userConfData =
                           (from a in kPiDataContext.UserDataChangeHistory
                            where a.Active == true && a.FK_User == Convert.ToInt32(button.CommandArgument)
                            && a.Status==false
                            select a).FirstOrDefault();
                            if (userConfData == null)
                            {
                                user.Data_Status = true;
                            }
                        }
                        else if (student != null)
                        {
                            StudentsTable studen = (from a in kPiDataContext.StudentsTable
                                               where a.StudentsTableID == userData.FK_User && a.Active==true
                                               select a).FirstOrDefault();
                            if (userData.ID_Param_ToChange == 1)
                            {
                                studen.Name = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 2)
                            {
                                studen.Surname = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 3)
                            {
                                studen.Patronimyc = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 4)
                            {
                                studen.Email = userData.Name.Substring(userData.Name.IndexOf(" на ") + 4);
                            }
                            if (userData.ID_Param_ToChange == 5)
                            {
                                studen.FK_FirstLevelSubdivision = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                                                      where a.Active == true &&
                                                                          a.Name == userData.Name.Substring(userData.Name.IndexOf(" на ") +4)
                                                                      select a.FirstLevelSubdivisionTableID).FirstOrDefault();

                            }
                            if (userData.ID_Param_ToChange == 6)
                            {
                                studen.FK_SecondLevelSubdivision = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                                                       where a.Active == true &&
                                                                           a.Name == userData.Name.Substring(userData.Name.IndexOf(" на ") + 4)
                                                                       select a.SecondLevelSubdivisionTableID).FirstOrDefault();

                            }
                            if (userData.ID_Param_ToChange == 7)
                            {
                                studen.FK_Group = (from a in kPiDataContext.GroupsTable
                                                                      where a.Active == true &&
                                                                          a.Name == userData.Name.Substring(userData.Name.IndexOf(" на ") + 4)
                                                                      select a.ID).FirstOrDefault();
                            }
                            userData.Status = true;
                            List<StudentChangeDataHistoryTable> studConfData =
                           (from a in kPiDataContext.StudentChangeDataHistoryTable
                            where a.Active == true && a.Status == false
                            select a).ToList();
                            foreach (var stud in studConfData)    
                            {
                                StudentsTable studenot = (from a in kPiDataContext.StudentsTable 
                                                          where a.StudentsTableID == stud.FK_StudentTable select a).FirstOrDefault();
                                studenot.Data_Status = false;
                            }
                            
                        }
                        kPiDataContext.SubmitChanges();
                    //    LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU2: AdminUser " + ViewState["User"] + "Confirm Changes user: " + user.Email + " from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
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
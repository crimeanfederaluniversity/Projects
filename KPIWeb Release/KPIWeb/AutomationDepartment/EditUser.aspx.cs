﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["User"] = userTable.Email;

            if ((userTable.AccessLevel != 10)&&(userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }
            ////////////////////////////////////////////////////////

            ViewState["Password"] = "112233";
            if (!IsPostBack)
            {
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Login", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Password", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Fourthlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Fifthlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Acceslvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Zerolvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Confirmed", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Position", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DeleteUserButton", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SaveUserButton", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ChangeUserButton", typeof(string)));


            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UsersTable> users;
                if (TextBox2.Text.Any())
                {
                    users = (from a in kpiWebDataContext.UsersTable where a.Active == true && (a.Login.Contains(TextBox2.Text) || a.Email.Contains(TextBox2.Text) || a.Position.Contains(TextBox2.Text)) select a).ToList();
                }
                else
                {
                    users = (from a in kpiWebDataContext.UsersTable where a.Active == true select a).ToList();
                }

                List<ZeroLevelSubdivisionTable> zeroLevelSubdivisionTable = (from a in kpiWebDataContext.ZeroLevelSubdivisionTable select a).ToList();
                List<FirstLevelSubdivisionTable> firstLevelSubdivisionTable = (from a in kpiWebDataContext.FirstLevelSubdivisionTable select a).ToList();
                List<SecondLevelSubdivisionTable> secondLevelSubdivisionTable = (from a in kpiWebDataContext.SecondLevelSubdivisionTable select a).ToList();
                List<ThirdLevelSubdivisionTable> thirdLevelSubdivisionTable = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable select a).ToList();
                List<FourthLevelSubdivisionTable> fourthLevelSubdivisionTable = (from a in kpiWebDataContext.FourthLevelSubdivisionTable select a).ToList();
                List<FifthLevelSubdivisionTable> fifthLevelSubdivisionTable = (from a in kpiWebDataContext.FifthLevelSubdivisionTable select a).ToList();

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["FourthlvlId"] = user.UsersTableID;
                    dataRow["Login"] = user.Login;
                   // if (CheckBox1.Checked && TextBox1.Text == ViewState["Password"].ToString())
                   // {
                        dataRow["Password"] = user.Password;
                        dataRow["Email"] = user.Email;
                   // }
                    //else
                  //  {
                    //    dataRow["Password"] = "********";
                    //    dataRow["Email"] = "********";
                  //  }

                    dataRow["ChangeUserButton"] = user.UsersTableID;

                    dataRow["Firstlvl"] = (from a in firstLevelSubdivisionTable
                                           where a.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();

                    dataRow["Secondlvl"] = (from a in secondLevelSubdivisionTable
                                            where a.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                            select a.Name).FirstOrDefault();

                    dataRow["Thirdlvl"] = (from a in thirdLevelSubdivisionTable
                                           where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();

                    dataRow["Fourthlvl"] = (from a in fourthLevelSubdivisionTable
                                            where a.FourthLevelSubdivisionTableID == user.FK_FourthLevelSubdivisionTable
                                            select a.Name).FirstOrDefault();

                    dataRow["Fifthlvl"] = (from a in fifthLevelSubdivisionTable
                                           where a.FifthLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();

                    dataRow["Acceslvl"] = user.AccessLevel;

                    dataRow["Zerolvl"] = (from a in zeroLevelSubdivisionTable
                                          where a.ZeroLevelSubdivisionTableID == user.FK_ZeroLevelSubdivisionTable
                                          select a.Name).FirstOrDefault();
                    dataRow["Confirmed"] = user.Confirmed;
                    dataRow["Position"] = user.Position;
                    dataTable.Rows.Add(dataRow);
                }

                ViewState["GridviewUsers"] = dataTable;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void DeleteUserButtonClick(object sender, EventArgs e)
        {
           // if (TextBox1.Text == ViewState["Password"].ToString())
           // {
            if (!CheckBox2.Checked)
            {
                Button button = (Button) sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        UsersTable user =
                            (from a in kPiDataContext.UsersTable
                                where a.UsersTableID == Convert.ToInt32(button.CommandArgument)
                                select a).FirstOrDefault();

                        user.Active = false;
                        kPiDataContext.SubmitChanges();
                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU2: AdminUser " + ViewState["User"] + "DELETE user: "+ user.Email + " from ip: "+Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                    }
                    RefreshGrid();

                }
            }
            else
            {
                DisplayAlert("Снимите предохранитель");
            }
            //  }
           // else
            //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
              //      "alert('Введите пароль');", true);

        }

        protected void SaveUserButtonClick(object sender, EventArgs e)
        {
            //if (TextBox1.Text == ViewState["Password"].ToString())
            //{
                int rowIndex = 0;
                Button button = (Button)sender;
                {
                    if (ViewState["GridviewUsers"] != null)
                    {
                        DataTable dataTable = (DataTable)ViewState["GridviewUsers"];

                        if (dataTable.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dataTable.Rows.Count; i++)
                            {
                                Label TextBoxFourthlvlId = (Label)GridView1.Rows[rowIndex].FindControl("FourthlvlId");
                                if (TextBoxFourthlvlId.Text.Equals(button.CommandArgument.ToString()))
                                {
                                    TextBox TextBoxLogin = (TextBox)GridView1.Rows[rowIndex].FindControl("Login");
                                    TextBox TextBoxPassword = (TextBox)GridView1.Rows[rowIndex].FindControl("Password");
                                    TextBox TextBoxEmail = (TextBox)GridView1.Rows[rowIndex].FindControl("Email");
                                    TextBox TextBoxFirstlvl = (TextBox)GridView1.Rows[rowIndex].FindControl("Firstlvl");
                                    TextBox TextBoxSecondlvl =
                                        (TextBox)GridView1.Rows[rowIndex].FindControl("Secondlvl");
                                    TextBox TextBoxThirdlvl = (TextBox)GridView1.Rows[rowIndex].FindControl("Thirdlvl");
                                    TextBox TextBoxFourthlvl =
                                        (TextBox)GridView1.Rows[rowIndex].FindControl("Fourthlvl");
                                    TextBox TextBoxFifthlvl = (TextBox)GridView1.Rows[rowIndex].FindControl("Fifthlvl");
                                    TextBox TextBoxAcceslvl = (TextBox)GridView1.Rows[rowIndex].FindControl("Acceslvl");
                                    TextBox TextBoxZerolvl = (TextBox)GridView1.Rows[rowIndex].FindControl("Zerolvl");
                                    TextBox TextBoxConfirmed = (TextBox)GridView1.Rows[rowIndex].FindControl("Confirmed");
                                    TextBox TextBoxPosition = (TextBox)GridView1.Rows[rowIndex].FindControl("Position");

                                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                                    {
                                        UsersTable user =
                                            (from a in kPiDataContext.UsersTable
                                             where a.UsersTableID == Convert.ToInt32(button.CommandArgument)
                                             select a).FirstOrDefault();

                                        user.Login = TextBoxLogin.Text;

                                        if (!TextBoxPassword.Text.Equals("********") || (!Regex.IsMatch(TextBoxPassword.Text, @"/*")) || (!TextBoxEmail.Text.Equals("********") || (!Regex.IsMatch(TextBoxEmail.Text, @"/*"))))
                                        {
                                            user.Password = TextBoxPassword.Text;
                                            user.Email = TextBoxEmail.Text;
                                        }
                                        if (TextBoxFirstlvl.Text.Any())
                                            user.FK_FirstLevelSubdivisionTable = (from a in kPiDataContext.UsersTable
                                                                                  where a.UsersTableID == Convert.ToInt32(TextBoxFourthlvlId.Text)
                                                                                  select a.FK_FirstLevelSubdivisionTable).FirstOrDefault();

                                       
                                        if (TextBoxSecondlvl.Text.Any())
                                            user.FK_SecondLevelSubdivisionTable = (from a in kPiDataContext.UsersTable
                                                                                   where a.UsersTableID == Convert.ToInt32(TextBoxFourthlvlId.Text)
                                                                                   select a.FK_SecondLevelSubdivisionTable).FirstOrDefault();
                                        if (TextBoxThirdlvl.Text.Any())
                                            user.FK_ThirdLevelSubdivisionTable = (from a in kPiDataContext.UsersTable
                                                                                  where a.UsersTableID == Convert.ToInt32(TextBoxFourthlvlId.Text)
                                                                                  select a.FK_ThirdLevelSubdivisionTable).FirstOrDefault();
                                        if (TextBoxFourthlvl.Text.Any())
                                            user.FK_FourthLevelSubdivisionTable = (from a in kPiDataContext.UsersTable
                                                                                   where a.UsersTableID == Convert.ToInt32(TextBoxFourthlvlId.Text)
                                                                                   select a.FK_FourthLevelSubdivisionTable).FirstOrDefault();
                                        if (TextBoxFifthlvl.Text.Any())
                                            user.FK_FifthLevelSubdivisionTable = (from a in kPiDataContext.UsersTable
                                                                                  where a.UsersTableID == Convert.ToInt32(TextBoxFourthlvlId.Text)
                                                                                  select a.FK_FifthLevelSubdivisionTable).FirstOrDefault();
                                        if (TextBoxAcceslvl.Text.Any())
                                            user.AccessLevel = Convert.ToInt32(TextBoxAcceslvl.Text);
                                        if (TextBoxZerolvl.Text.Any())
                                            user.FK_ZeroLevelSubdivisionTable = (from a in kPiDataContext.UsersTable
                                                                                 where a.UsersTableID == Convert.ToInt32(TextBoxFourthlvlId.Text)
                                                                                 select a.FK_ZeroLevelSubdivisionTable).FirstOrDefault();
                                        if (TextBoxPosition.Text.Any())
                                            user.Position = Convert.ToString(TextBoxPosition.Text);
                                                                                 
                                        
                                        kPiDataContext.SubmitChanges();
                                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU3: AdminUser " + ViewState["User"] + " SAVE data of user: " + user.Email + " from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                                    }
                                }
                                rowIndex++;

                            }




                        }
                    }
                }
           // }
          //  else Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
             //       "alert('Введите пароль');", true);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
        {
            if (!TextBox1.Text.Any()) CheckBox1.Checked = false;
            RefreshGrid();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU1: User " + ViewState["User"] + " moved to page /AutomationDepartment/Regisration.aspx");
            Response.Redirect("~/AutomationDepartment/Regisration.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU1: User " + ViewState["User"] + " moved to page /AutomationDepartment/ConnectUsers.aspx");
            Response.Redirect("~/AutomationDepartment/ConnectUsers.aspx");
        }

        protected void ChangeUserButtonClick (object sender, EventArgs e)
        { 
            Button button = (Button)sender;
            {
                Serialization ser = new Serialization(Convert.ToInt32(button.CommandArgument));
                Session["userIdforChange"] = ser;
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU4: AdminUser " + ViewState["User"] + "moved to page /AutomationDepartment/ChangeUser.aspx" + "from ip: "+ Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                Response.Redirect("~/AutomationDepartment/ChangeUser.aspx");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
            if (!CheckBox2.Checked)
            {/*
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0EU0: MassMailing was started by: " + ViewState["User"]);
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();

                var users =
                    (from a in kPiDataContext.UsersTable where a.Active && a.Confirmed == false select a).ToList();

                EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                    where a.Name == "InviteToRegister"
                          && a.Active == true
                    select a).FirstOrDefault();

                foreach (var user in users)
                {
                    Action.MassMailing(user.Email, EmailParams.EmailTitle,
                        EmailParams.EmailContent.Replace("#LINK#",
                            ConfigurationManager.AppSettings.Get("SiteName") + "/Account/UserRegister?&id=" +
                            user.PassCode), null);
                }*/
                Response.Redirect("~/AutomationDepartment/SendInvite.aspx");
            }
            else
            {
                DisplayAlert("Снимите предохранитель");
            }
            

        }
        private void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/ReturnReportForUser.aspx");
        }

    }
}
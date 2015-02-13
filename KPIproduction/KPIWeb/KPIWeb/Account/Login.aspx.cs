using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using KPIWeb.Models;
using log4net;
//using Microsoft.Office.Interop.Excel;
using WebApplication3;
using Page = System.Web.UI.Page;

namespace KPIWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "123");
        }
        protected void LogIn(object sender, EventArgs e)
        {          
            try
            {
                if (IsValid)
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                                       where usersTables.Login == UserName.Text &&
                                       usersTables.Password == Password.Text
                                       select usersTables).FirstOrDefault();
                    if (user != null)
                    {
                    LogHandler.LogWriter.WriteLog(LogCategory.INFO, DateTime.Now.ToString() + "Пользователь " + user.Login + " вошел в систему ");
   
                        Serialization UserSerId = new Serialization(user.UsersTableID);
                        Session["UserID"] = UserSerId;

                        List<RolesTable> UserRoles = (from a in KPIWebDataContext.UsersAndRolesMappingTable
                                                      join b in KPIWebDataContext.RolesTable
                                                      on a.FK_RolesTable equals b.RolesTableID
                                                      where a.FK_UsersTable == user.UsersTableID && b.Active == true
                                                      select b).ToList();
                        
                        foreach (RolesTable Role in UserRoles)
                        {
                            if (Role.Role == 10)
                            {
                                Response.Redirect("~/AutomationDepartment/Main.aspx");
                            }
                            else if(Role.Role==8)
                            {
                                Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
                            }
                        }
                        Response.Redirect("~/Reports/ChooseReport.aspx");
                    }            
                    else
                    {
                        FailureText.Text = "Неверное имя пользователя или пароль.";
                        ErrorMessage.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                LogHandler.LogWriter.WriteError(ex);
               // LogHandler.LogWriter.WriteError(ex, "Error message");
               // LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Info message");
            }
        }
        protected void RememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/TEST.aspx");
        }
    }
}
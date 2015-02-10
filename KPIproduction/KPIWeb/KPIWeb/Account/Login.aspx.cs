using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using KPIWeb.Models;
using WebApplication3;

namespace KPIWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                   /* int  UserID = (from usersTables in KPIWebDataContext.UsersTable
                                       where usersTables.Login == UserName.Text &&
                                       usersTables.Password == Password.Text
                                       select usersTables.UsersTableID).FirstOrDefault();
                    */
                    if (user != null)
                    {
                        Session["user"] = user;
                        Session["UserID"] = user.UsersTableID;

                        if (user.Login == "admin")
                        {
                            Response.Redirect("~/AutomationDepartment/Main.aspx");
                        }
                        else if (user.Login == "statistics")
                        {
                            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Reports/ChooseReport.aspx");
                        }

                    }
                    /*
                        List<RolesTable> UserRoles = (from a in KPIWebDataContext.UsersAndRolesMappingTable
                                                join b in KPIWebDataContext.RolesTable
                                                on a.FK_RolesTable equals b.RolesTableID
                                                where a.FK_UsersTable == UserID && b.Active==true
                                                select b).ToList();
                        ////////получили список ID на нужные роли
                        foreach (RolesTable UserRole in UserRoles)
                        {
                            
                            if (UserRole.Active==true) 
                            {
                                if (UserRole.CanEdit == true)
                                {*/

                                    /*List<int> ReportArchiveIDList = (from reportArchiveTables in KPIWebDataContext.ReportArchiveTables
                                                                     join reportAndRolesMappings in KPIWebDataContext.ReportAndRolesMappings
                                                                     on reportArchiveTables.ReportArchiveTableID equals reportAndRolesMappings.FK_ReportArchiveTable
                                                                     where reportAndRolesMappings.FK_RolesTable == user.FK_RolesTable &&
                                                                     reportArchiveTables.Active == true &&
                                                                     reportArchiveTables.StartDateTime < DateTime.Now &&
                                                                     reportArchiveTables.EndDateTime > DateTime.Now
                                                                     select reportArchiveTables.ReportArchiveTableID).ToList();*/
                    /*
                                }
                                if (UserRole.CanView==true)
                                {
                                    //showforview
                                }
                            }       */                    
                       // }
                   // }



                    /*
                    if (user != null)
                    {
                        Session["user"] = user;

                        if (user.FK_RolesTable == 7)
                        {
                            List<int> ReportArchiveIDList = (from reportArchiveTables in KPIWebDataContext.ReportArchiveTables
                                                             join reportAndRolesMappings in KPIWebDataContext.ReportAndRolesMappings 
                                                             on reportArchiveTables.ReportArchiveTableID equals reportAndRolesMappings.FK_ReportArchiveTable
                                                             where reportAndRolesMappings.FK_RolesTable == user.FK_RolesTable &&
                                                             reportArchiveTables.Active == true &&
                                                             reportArchiveTables.StartDateTime < DateTime.Now &&
                                                             reportArchiveTables.EndDateTime > DateTime.Now
                                                             select reportArchiveTables.ReportArchiveTableID).ToList();

                            if (ReportArchiveIDList != null && ReportArchiveIDList.Count > 0)
                            {
                                Response.Redirect("~/Reports/FillingTheReport.aspx");
                            }
                            else
                                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('В настоящий момент для Вас нет активных отчетов для заполнения.');", true);
                        }

                        if (user.FK_RolesTable == 13)
                            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
                    }*/
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
    }
}
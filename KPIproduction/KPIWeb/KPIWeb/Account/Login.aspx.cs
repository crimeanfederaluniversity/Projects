using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using KPIWeb.Models;
using WebApplication3;

namespace KPIWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }

        protected void LogIn(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    UsersTable user = (from usersTables in KPIWebDataContext.UsersTables
                                       where usersTables.Login == UserName.Text &&
                                       usersTables.Password == Password.Text
                                       select usersTables).FirstOrDefault();

                    if (user != null)
                    {
                        Session["user"] = user;

                        if (user.FK_RolesTable == 13)
                        {
                            List<int> ReportArchiveIDList = (from reportArchiveTables in KPIWebDataContext.ReportArchiveTables
                                                             join reportAndRolesMappings in KPIWebDataContext.ReportAndRolesMappings on reportArchiveTables.ReportArchiveTableID equals reportAndRolesMappings.FK_ReportArchiveTable
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

                        if (user.FK_RolesTable == 16)
                            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
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
                //LogHandler.LogWriter.WriteError(ex, "Error message");
                //LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Info message");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class StastisticsHomePage : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            List<RolesTable> UserRoles = (from a in kPiDataContext.UsersAndRolesMappingTable
                                          join b in kPiDataContext.RolesTable
                                          on a.FK_RolesTable equals b.RolesTableID
                                          where a.FK_UsersTable == UserSer.Id && b.Active == true
                                          select b).ToList();
            ////////////////////////////////////////////////////////////////////////////////////
            foreach (RolesTable Role in UserRoles)
            {
                if (Role.Role != 8) //нельзя давать пользователю роли и заполняющего и статистики 
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }								            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Serialization ReportID = new Serialization(0, null);
            Session["ReportArchiveTableID"] = ReportID;
            Response.Redirect("~/Reports/EditReport.aspx");
        }
    }
}
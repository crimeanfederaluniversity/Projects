using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersTable user = (UsersTable)Session["user"];
            if (user == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            List<RolesTable> UserRoles = (from a in kPiDataContext.UsersAndRolesMappingTable
                                          join b in kPiDataContext.RolesTable
                                          on a.FK_RolesTable equals b.RolesTableID
                                          where a.FK_UsersTable == user.UsersTableID && b.Active == true
                                          select b).ToList();
            foreach (RolesTable Role in UserRoles)
            {
                if (Role.Role != 10) //нельзя давать пользователю роли и заполняющего и админа 
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/AddLevel.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/RoleMapping.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/IsAvailibleMappingRole.aspx");
        }
    }
}
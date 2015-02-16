using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            
            List<ReportArchiveTable> ReportArchiveTable = (from item in kPiDataContext.ReportArchiveTable
                                                           where item.Active == true
                                                           select item).ToList();

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("ReportArchiveTableID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Active", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Calculeted", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Sent", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SentDateTime", typeof(string)));
            dataTable.Columns.Add(new DataColumn("RecipientConfirmed", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("StartDateTime", typeof(string)));
            dataTable.Columns.Add(new DataColumn("EndDateTime", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DateToSend", typeof(string)));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            RolesTable role = new RolesTable();

            if (CheckBox1.Checked) role.Active = true;
            else role.Active = false;
            role.RoleName = TextBox1.Text;
            kPiDataContext.RolesTable.InsertOnSubmit(role);
            kPiDataContext.SubmitChanges();
        }
    }
}
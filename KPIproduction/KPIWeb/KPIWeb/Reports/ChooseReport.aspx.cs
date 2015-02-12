using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Reports
{
    public partial class ChooseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {                            
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();      
                List<RolesTable> UserRoles = (from a in kpiWebDataContext.UsersAndRolesMappingTable
                                              join b in kpiWebDataContext.RolesTable
                                               on a.FK_RolesTable equals b.RolesTableID
                                                where a.FK_UsersTable == UserSer.Id && b.Active == true
                                                select b).ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("RoleID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("RoleName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Param", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));

                foreach (RolesTable UserRole in UserRoles)
                {
                    if (UserRole.Active == true)
                    {
                        if (UserRole.CanEdit == true)
                        {
                            //нужны репорты только активные, надо проверить дату
                            List<ReportArchiveTable> ReportArchive = (from c in kpiWebDataContext.ReportAndRolesMappings
                                                                      join d in kpiWebDataContext.ReportArchiveTables
                                                                        on c.FK_ReportArchiveTable equals d.ReportArchiveTableID
                                                                        where (c.FK_RolesTable == UserRole.RolesTableID) && 
                                                                        (c.Active == true) &&
                                                                        d.StartDateTime < DateTime.Now &&
                                                                        d.EndDateTime > DateTime.Now
                                                                        select d).ToList();
                            foreach (ReportArchiveTable Report in ReportArchive)
                            {
                                DataRow dataRow = dataTable.NewRow();
                                dataRow["ReportID"] =   Report.ReportArchiveTableID.ToString();
                                dataRow["RoleID"] =     UserRole.RolesTableID.ToString();
                                dataRow["ReportName"] = Report.Name;
                                dataRow["RoleName"] =   UserRole.RoleName;
                                dataRow["Param"] = Report.ReportArchiveTableID.ToString() + "_" +UserRole.RolesTableID.ToString();
                                dataRow["StartDate"] = Report.StartDateTime.ToString().Split(' ')[0];
                                dataRow["EndDate"] = Report.EndDateTime.ToString().Split(' ')[0]; ;
                                dataTable.Rows.Add(dataRow);              
                            }
                        }
                        if ((UserRole.CanView == true) && (UserRole.CanEdit != true))
                        {
                            ///отчеты только просматриваемые
                        }
                    }
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }

        }

        protected void ButtonEditClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["Params"] = paramSerialization;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данная функция находится на стадии разработки');", true);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
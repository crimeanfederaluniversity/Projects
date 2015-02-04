using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace KPIWeb.Reports
{
    public partial class EditReport : System.Web.UI.Page
    {
        UsersTable user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                //UsersTable user = (from usersTables in KPIWebDataContext.UsersTables
                //                   where usersTables.Login == "Statistical" &&
                //                   usersTables.Password == "Statistical"
                //                   select usersTables).FirstOrDefault();
                //Session["user"] = user;

                //Session["ReportArchiveTableID"] = 2;


                user = (UsersTable)Session["user"];

                if (user == null)
                    Response.Redirect("~/Account/Login.aspx");

                if (Session["ReportArchiveTableID"] != null)
                {
                    int reportArchiveTableID = (int)Session["ReportArchiveTableID"];

                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

                    ReportArchiveTable ReportArchiveTable = (from item in KPIWebDataContext.ReportArchiveTables
                                                             where item.ReportArchiveTableID == reportArchiveTableID
                                                             select item).FirstOrDefault();
                    if (ReportArchiveTable != null)
                    {
                        CheckBoxActive.Checked = ReportArchiveTable.Active;
                        CheckBoxCalculeted.Checked = ReportArchiveTable.Calculeted;
                        CheckBoxSent.Checked = ReportArchiveTable.Sent;
                        CheckBoxRecipientConfirmed.Checked = ReportArchiveTable.RecipientConfirmed;
                        TextBoxName.Text = ReportArchiveTable.Name;

                        if (ReportArchiveTable.StartDateTime != null)
                            CalendarStartDateTime.SelectedDate = (DateTime)ReportArchiveTable.StartDateTime;

                        if (ReportArchiveTable.EndDateTime != null)
                            CalendarEndDateTime.SelectedDate = (DateTime)ReportArchiveTable.EndDateTime;

                        if (ReportArchiveTable.DateToSend != null)
                            CalendarDateToSend.SelectedDate = (DateTime)ReportArchiveTable.DateToSend;

                        if (ReportArchiveTable.SentDateTime != null)
                            CalendarSentDateTime.SelectedDate = (DateTime)ReportArchiveTable.SentDateTime;
                    }

                    List<RolesTable> rolesTable = (from item in KPIWebDataContext.RolesTables
                                                   where item.Active == true
                                                   select item).ToList();

                    List<ReportAndRolesMapping> reportAndRolesMapping = (from item in KPIWebDataContext.ReportAndRolesMappings
                                                                         where item.FK_ReportArchiveTable == reportArchiveTableID
                                                                         select item).ToList();

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("RolesTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("RoleChecked", typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));

                    foreach (var role in rolesTable)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        var isExist = (from item in reportAndRolesMapping
                                       where item.FK_RolesTable == role.RolesTableID
                                       select item).FirstOrDefault();

                        if (isExist != null)
                            dataRow["RoleChecked"] = true;
                        else
                            dataRow["RoleChecked"] = false;

                        dataRow["RolesTableID"] = role.RolesTableID;
                        dataRow["Name"] = role.Name;

                        dataTable.Rows.Add(dataRow);
                    }

                    ViewState["GridviewRoles"] = dataTable;

                    GridviewRoles.DataSource = dataTable;
                    GridviewRoles.DataBind();
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            int reportArchiveTableID = 0;

            if (Session["ReportArchiveTableID"] != null)
                reportArchiveTableID = (int)Session["ReportArchiveTableID"];

            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

            ReportArchiveTable reportArchiveTable = (from item in KPIWebDataContext.ReportArchiveTables
                                                     where item.ReportArchiveTableID == reportArchiveTableID
                                                     select item).FirstOrDefault();

            if (reportArchiveTable == null)
                reportArchiveTable = new ReportArchiveTable();

            reportArchiveTable.Active = CheckBoxActive.Checked;
            reportArchiveTable.Calculeted = CheckBoxCalculeted.Checked;
            reportArchiveTable.Sent = CheckBoxSent.Checked;
            reportArchiveTable.RecipientConfirmed = CheckBoxRecipientConfirmed.Checked;
            reportArchiveTable.Name = TextBoxName.Text;

            if (CalendarStartDateTime.SelectedDate > DateTime.MinValue)
                reportArchiveTable.StartDateTime = CalendarStartDateTime.SelectedDate;

            if (CalendarEndDateTime.SelectedDate > DateTime.MinValue)
                reportArchiveTable.EndDateTime = CalendarEndDateTime.SelectedDate;

            if (CalendarDateToSend.SelectedDate > DateTime.MinValue)
                reportArchiveTable.DateToSend = CalendarDateToSend.SelectedDate;

            if (CalendarSentDateTime.SelectedDate > DateTime.MinValue)
                reportArchiveTable.SentDateTime = CalendarSentDateTime.SelectedDate;

            if (ViewState["GridviewRoles"] != null)
            {
                DataTable dataTable = (DataTable)ViewState["GridviewRoles"];

                if (dataTable.Rows.Count > 0)
                {
                    List<ReportAndRolesMapping> reportAndRolesMappingList = (from item in KPIWebDataContext.ReportAndRolesMappings
                                                                         where item.FK_ReportArchiveTable == reportArchiveTableID
                                                                         select item).ToList();

                    KPIWebDataContext.ReportAndRolesMappings.DeleteAllOnSubmit(reportAndRolesMappingList);

                    for (int i = 1; i <= dataTable.Rows.Count; i++)
                    {
                        Label label = (Label)GridviewRoles.Rows[rowIndex].Cells[0].FindControl("LabelRolesTableID");
                        CheckBox checkBox = (CheckBox)GridviewRoles.Rows[rowIndex].FindControl("CheckBoxRoleChecked");

                        if(label != null && checkBox != null)
                        {
                            int rolesTableID = -1;
                            if (int.TryParse(label.Text, out rolesTableID) && rolesTableID > -1 && checkBox.Checked == true)
                            {
                                ReportAndRolesMapping reportAndRolesMapping = new ReportAndRolesMapping();
                                reportAndRolesMapping.Active = true;
                                reportAndRolesMapping.FK_RolesTable = rolesTableID;
                                reportAndRolesMapping.FK_ReportArchiveTable = reportArchiveTableID;

                                KPIWebDataContext.ReportAndRolesMappings.InsertOnSubmit(reportAndRolesMapping);
                            }
                        }
                        rowIndex++;
                    }
                }
            }

            KPIWebDataContext.SubmitChanges();
        }
    }
}
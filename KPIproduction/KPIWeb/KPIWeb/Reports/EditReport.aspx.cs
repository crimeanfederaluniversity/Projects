using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Threading;

namespace KPIWeb.Reports
{
    public partial class EditReport : System.Web.UI.Page
    {
        UsersTable user;

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
            ////////////////////////////////////////////////////////////////////////////
            Serialization ReportId = (Serialization)Session["ReportArchiveTableID"];

            if (!Page.IsPostBack)
            {
                if (ReportId.ReportArchiveID != 0)
                {
                    int reportArchiveTableID = ReportId.ReportArchiveID;///////////////

                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    ReportArchiveTable ReportArchiveTable = (from item in KPIWebDataContext.ReportArchiveTable
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

                    List<FirstLevelSubdivisionTable> academies =
                        (from item in KPIWebDataContext.FirstLevelSubdivisionTable
                         where item.Active==true
                            select item).ToList();
                    int i = 0;

                    foreach (FirstLevelSubdivisionTable academy in academies)
                    {
                        CheckBoxList1.Items.Add(academy.Name);
                        int tmp = (from item in KPIWebDataContext.ReportArchiveAndLevelMappingTable
                            where item.FK_FirstLevelSubmisionTableId == academy.FirstLevelSubdivisionTableID
                                  && item.FK_ReportArchiveTableId == reportArchiveTableID
                            select item).Count();
                        CheckBoxList1.Items[i].Selected = tmp > 0 ? true : false; 
                        CheckBoxList1.Items[i].Value = academy.FirstLevelSubdivisionTableID.ToString();
                        i++;
                    }            
                }
                else //создаем новый отчет
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    ButtonSave.Text="Сохранить новую кампанию";

                    List<FirstLevelSubdivisionTable> academies =
                        (from item in KPIWebDataContext.FirstLevelSubdivisionTable
                         where item.Active == true
                         select item).ToList();
                    int i = 0;

                    foreach (FirstLevelSubdivisionTable academy in academies)
                    {
                        CheckBoxList1.Items.Add(academy.Name);
                        CheckBoxList1.Items[i].Value = academy.FirstLevelSubdivisionTableID.ToString();
                        i++;
                    }                  
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

            Serialization ReportId = (Serialization) Session["ReportArchiveTableID"];
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            ReportArchiveTable reportArchiveTable = new ReportArchiveTable();

            int reportArchiveTableID = 0;
            if (ReportId.ReportArchiveID == 0) // создаем новую запись в БД и узнаем ей айди
            {
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

                KPIWebDataContext.ReportArchiveTable.InsertOnSubmit(reportArchiveTable);
                KPIWebDataContext.SubmitChanges();

                reportArchiveTableID = reportArchiveTable.ReportArchiveTableID;              
            }

            else //если запись в бд уже есть
            {
                reportArchiveTableID = ReportId.ReportArchiveID;
                reportArchiveTable = (from item in KPIWebDataContext.ReportArchiveTable
                                      where item.ReportArchiveTableID == reportArchiveTableID
                                      select item).FirstOrDefault();
            }

            int rowIndex = 0;           
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

            foreach (ListItem checkItem in CheckBoxList1.Items)
            {
                if (checkItem.Selected)
                {
                    ReportArchiveAndLevelMappingTable repNRole =
                        (from item in KPIWebDataContext.ReportArchiveAndLevelMappingTable
                            where item.FK_FirstLevelSubmisionTableId == Convert.ToInt32(checkItem.Value)
                                  && item.FK_ReportArchiveTableId == reportArchiveTableID
                            select item).FirstOrDefault();
                    if (repNRole != null)
                    {
                        repNRole.Active = true;
                    }
                    else
                    {
                        repNRole = new ReportArchiveAndLevelMappingTable();
                        repNRole.Active = true;
                        repNRole.FK_FirstLevelSubmisionTableId = Convert.ToInt32(checkItem.Value);
                        repNRole.FK_ReportArchiveTableId = reportArchiveTableID;
                        KPIWebDataContext.ReportArchiveAndLevelMappingTable.InsertOnSubmit(repNRole);
                    }
                }
                else
                {
                    ReportArchiveAndLevelMappingTable repNRole =
                        (from item in KPIWebDataContext.ReportArchiveAndLevelMappingTable
                            where item.FK_FirstLevelSubmisionTableId == Convert.ToInt32(checkItem.Value)
                                  && item.FK_ReportArchiveTableId == reportArchiveTableID
                            select item).FirstOrDefault();
                    if (repNRole != null)
                    {
                        repNRole.Active = false; /// // / лучше просто удалить эту запись из БД
                    }
                }
            }
            KPIWebDataContext.SubmitChanges();
            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");       
        }

        protected void CalendarStartDateTime_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}
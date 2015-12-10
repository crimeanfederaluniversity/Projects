using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        UsersTable user;
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["User"] = userTable.Email;

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 1, 2, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            } 
            if (!Page.IsPostBack)
            {              
                List<ReportArchiveTable> ReportArchiveTable_ = (from item in kPiDataContext.ReportArchiveTable
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

                foreach (ReportArchiveTable reportTable in ReportArchiveTable_)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReportArchiveTableID"] = reportTable.ReportArchiveTableID.ToString();
                    dataRow["Active"] = reportTable.Active ? "Да" : "Нет";
                    dataRow["Calculeted"] = reportTable.Calculeted ? "Да" : "Нет";
                    dataRow["Sent"] = reportTable.Sent ? "Да" : "Нет";
                    dataRow["SentDateTime"] = reportTable.SentDateTime.ToString().Split(' ')[0];
                    dataRow["RecipientConfirmed"] = reportTable.RecipientConfirmed ? "Да" : "Нет";
                    dataRow["Name"] = reportTable.Name;
                    dataRow["StartDateTime"] = reportTable.StartDateTime.ToString().Split(' ')[0];
                    dataRow["EndDateTime"] = reportTable.EndDateTime.ToString().Split(' ')[0];
                    dataRow["DateToSend"] = reportTable.DateToSend.ToString().Split(' ')[0];
                    dataTable.Rows.Add(dataRow);
                }
                //GridviewActiveCampaign.DataSource = ReportArchiveTable;
                GridviewActiveCampaign.DataSource = dataTable;
                GridviewActiveCampaign.DataBind();
            
            }

            int rowIndex = 0;
            

            if (GridviewActiveCampaign.Rows.Count > 0)
            {
                for (int i = 1; i <= GridviewActiveCampaign.Rows.Count; i++)
                {
                    Label LabelID = (Label)GridviewActiveCampaign.Rows[rowIndex].FindControl("LabelReportArchiveTableID");
                    Label LabelDate1 = (Label)GridviewActiveCampaign.Rows[rowIndex].FindControl("LabelDate1");
                    Label LabelDate2 = (Label)GridviewActiveCampaign.Rows[rowIndex].FindControl("LabelDate2");
                    int id = Convert.ToInt32(LabelID.Text);

                    EmailSendHistory esh0 = (from a in kPiDataContext.EmailSendHistory where a.FK_ReportsArchiveTable == id && a.Count == 0 select a).FirstOrDefault(); ;
                    EmailSendHistory esh1 = (from a in kPiDataContext.EmailSendHistory where a.FK_ReportsArchiveTable == id && a.Count == 1 select a).FirstOrDefault(); ;

                    if ( esh0 != null )
                    {
                        var date =
                           (from a in kPiDataContext.EmailSendHistory
                            where a.FK_ReportsArchiveTable == id && a.Count == 0
                            select a.Date).FirstOrDefault();
                        LabelDate1.Text = date + " " + (from a in kPiDataContext.EmailSendHistory
                                                       where a.FK_ReportsArchiveTable == id && a.Count == 0
                                                       select a.Value).FirstOrDefault();
                    }
                    else
                    {
                        esh0 = new EmailSendHistory();
                        esh0.Active = true;
                        esh0.FK_ReportsArchiveTable = id;
                        esh0.Value = "0";
                        esh0.Count = 0;
                        kPiDataContext.EmailSendHistory.InsertOnSubmit(esh0);
                    }
                    if (esh1 != null)
                    {
                        var date =
                          (from a in kPiDataContext.EmailSendHistory
                           where a.FK_ReportsArchiveTable == id && a.Count == 1
                           select a.Date).FirstOrDefault();

                        LabelDate2.Text = date + " " + (from a in kPiDataContext.EmailSendHistory
                                                        where a.FK_ReportsArchiveTable == id && a.Count == 1
                                                        select a.Value).FirstOrDefault(); ;
                    }
                    else
                    {
                        esh1 = new EmailSendHistory();
                        esh1.Active = true;
                        esh1.FK_ReportsArchiveTable = id;
                        esh1.Value = "0";
                        esh1.Count = 1;
                        kPiDataContext.EmailSendHistory.InsertOnSubmit(esh1);
                    }
                    rowIndex++;
                }
            }

            kPiDataContext.SubmitChanges();
        }

        protected void ButtonEditReport_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Serialization ReportID = new Serialization((int)reportArchiveTableID, null);
                Session["ReportArchiveTableID"] = ReportID;
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RW2: User " + ViewState["User"] + @" moved to page /Reports_/EditReport.aspx");
                Response.Redirect("~/Reports_/EditReport.aspx");
            }
        }
        protected void ButtonEditReport_Click_2(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Serialization ReportID = new Serialization((int)reportArchiveTableID, null);
                Session["ReportArchiveTableID"] = ReportID;
                Response.Redirect("~/Reports_/GenerateReport.aspx");
            }
        }
        protected void GenerateReport_Click(object sender, EventArgs e)
        {            
        }

        protected void GridviewActiveCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization ReportID = new Serialization(0, null);
            Session["ReportArchiveTableID"] = ReportID;
            Response.Redirect("~/Reports_/EditReport.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StatisticsDepartment/Indicators.aspx");
        }

        protected void ButtonViewReportClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization;
                Serialization modeSer = new Serialization(1, null, null);
                Session["mode"] = modeSer;               
                Serialization level = new Serialization(0, 0, 0, 0, 0, 0);
                Session["level"] = level;
                Response.Redirect("~/StatisticsDepartment/FastStructure.aspx");
            }
        }

        protected void ButtonViewStruct(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization;
                Response.Redirect("~/StatisticsDepartment/ReportFilling.aspx");
            }
        }

        protected void ButtonMailSending_Click(object sender, EventArgs e)
        {
            if (!CheckBox1.Checked)
            {
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RW1: MassMailing was started by: " + ViewState["User"]);
                int errors = 0;
                Button button = (Button) sender;
                {
                    KPIWebDataContext kPiDataContext = new KPIWebDataContext();


                    var emailListTo = (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
                        where a.FK_ReportArchiveTableId == Convert.ToInt32(button.CommandArgument) && a.Active
                        join b in kPiDataContext.UsersTable
                            on a.FK_FirstLevelSubmisionTableId equals b.FK_FirstLevelSubdivisionTable
                        where b.Active
                        && a.FK_SecondLevelSubdivisionTable == b.FK_SecondLevelSubdivisionTable
                        && a.FK_ThirdLevelSubdivisionTable == b.FK_ThirdLevelSubdivisionTable
                        select b.Email).ToList();

                    EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                        where a.Name == "NewCampaign"
                              && a.Active == true
                        select a).FirstOrDefault();
                    string RepStartDate = ((from a in kPiDataContext.ReportArchiveTable
                        where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument)
                        select a.StartDateTime).FirstOrDefault()).ToString().Split()[0];
                    string RepEndDate = (from a in kPiDataContext.ReportArchiveTable
                        where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument)
                        select a.EndDateTime).FirstOrDefault().ToString().Split()[0];
                    string RepName = (from a in kPiDataContext.ReportArchiveTable
                        where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument)
                        select a.Name).FirstOrDefault();
                    foreach (var email in emailListTo)
                    {

                        if (EmailParams != null)
                            Action.MassMailing(email, EmailParams.EmailTitle,
                                EmailParams.EmailContent.Replace("#SiteName#",
                                    ConfigurationManager.AppSettings.Get("SiteName"))
                                    .Replace("#StartDate#", RepStartDate)
                                    .Replace("#EndDate#", RepEndDate)
                                    .Replace("#ReportName#", RepName)
                                , null);

                        /*
                   errors = Action.MassMailing(email,"Новая отчетная кампания \"" + 
                       (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault() + "\"",
                       "Здравствуйте, " + email.ToString().Substring(0, email.ToString().LastIndexOf('@')) + ". Информируем Вас о начале новой отчетной кампании \"" +
                       (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault()
                       + "\", которая пройдет в период с " + 
                       ((from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.StartDateTime).FirstOrDefault()).ToString().Split()[0] +
                       " по " + (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.EndDateTime).FirstOrDefault().ToString().Split()[0] +
                       ". Для авторизации в системе перейдите по ссылке: " + ConfigurationManager.AppSettings.Get("SiteName")
                       , null);
                    */
                    }
                    EmailSendHistory esh0 = (from a in kPiDataContext.EmailSendHistory
                        where a.FK_ReportsArchiveTable == Convert.ToInt32(button.CommandArgument) && a.Count == 0
                        select a).FirstOrDefault();
                    ;
                    //EmailSendHistory esh1 = (from a in kPiDataContext.EmailSendHistories where a.FK_ReportsArchiveTable == Convert.ToInt32(button.CommandArgument) && a.Count == 1 select a).FirstOrDefault(); ;

                    esh0.Date = DateTime.Now;
                    esh0.Value = " [ " + (emailListTo.Count - errors).ToString() + "/" + emailListTo.Count.ToString() +
                                 " ]";

                    kPiDataContext.SubmitChanges();

                    var date = (from a in kPiDataContext.EmailSendHistory
                        where a.FK_ReportsArchiveTable == Convert.ToInt32(button.CommandArgument) && a.Count == 0
                        select a.Date).FirstOrDefault();

                    int rowIndex = 0;
                    if (GridviewActiveCampaign.Rows.Count > 0)
                    {
                        for (int i = 1; i <= GridviewActiveCampaign.Rows.Count; i++)
                        {
                            Label LabelDate1 = (Label) GridviewActiveCampaign.Rows[rowIndex].FindControl("LabelDate1");
                            LabelDate1.Text = date + " " + " [ " + (emailListTo.Count - errors).ToString() + "/" +
                                              emailListTo.Count.ToString() + " ]";
                            rowIndex++;
                        }
                    }
                }
                Page_Load(null, null);
            }
            else
            {
                DisplayAlert("Снимите предохранитель!");
            }
        }

        private void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');window.location.href = 'ReportViewer.aspx'",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }

        protected void ButtonMailSending2_Click(object sender, EventArgs e)
        {
            if (!CheckBox1.Checked)
            {
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RW0: MassMailing was started by: " + ViewState["User"]);
                Button button = (Button) sender;
                {
                    KPIWebDataContext kPiDataContext = new KPIWebDataContext();

                    int errors = 0;

                    var emailListToDebt = (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
                        where a.FK_ReportArchiveTableId == Convert.ToInt32(button.CommandArgument) && a.Active
                        join b in kPiDataContext.UsersTable on a.FK_FirstLevelSubmisionTableId equals
                            b.FK_FirstLevelSubdivisionTable
                        where b.Active
                        join c in kPiDataContext.CollectedBasicParametersTable on b.UsersTableID equals c.FK_UsersTable
                        where (c.Status == 0 || c.Status == null) && c.Active
                        && a.FK_SecondLevelSubdivisionTable == b.FK_SecondLevelSubdivisionTable
                        && a.FK_ThirdLevelSubdivisionTable == b.FK_ThirdLevelSubdivisionTable
                        select b.Email).ToList();


                    var uniqueMails = emailListToDebt.Distinct().ToList();

                    EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                        where a.Name == "CampaignReminder"
                              && a.Active == true
                        select a).FirstOrDefault();
                    string RepStartDate = ((from a in kPiDataContext.ReportArchiveTable
                        where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument)
                        select a.StartDateTime).FirstOrDefault()).ToString().Split()[0];
                    string RepEndDate = (from a in kPiDataContext.ReportArchiveTable
                        where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument)
                        select a.EndDateTime).FirstOrDefault().ToString().Split()[0];
                    string RepName = (from a in kPiDataContext.ReportArchiveTable
                        where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument)
                        select a.Name).FirstOrDefault();



                    foreach (var email in uniqueMails)
                    {
                        if (EmailParams != null)
                            Action.MassMailing(email, EmailParams.EmailTitle,
                                EmailParams.EmailContent.Replace("#SiteName#",
                                    ConfigurationManager.AppSettings.Get("SiteName"))
                                    .Replace("#StartDate#", RepStartDate)
                                    .Replace("#EndDate#", RepEndDate)
                                    .Replace("#ReportName#", RepName)
                                , null);
                        /*
                    errors = Action.MassMailing(email, "Заполните данные в отчетной кампании \"" +
                        (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault() + "\"",
                        "Здравствуйте, " + email.ToString().Substring(0, email.ToString().LastIndexOf('@')) + ". Напоминаем вам о том, что вы являетесь участником отчетной кампании \"" +
                        (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault()
                        + "\", которая проходит в период с " +
                        ((from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.StartDateTime).FirstOrDefault()).ToString().Split()[0] +
                        " по " + (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.EndDateTime).FirstOrDefault().ToString().Split()[0] +
                        ". На данный момент вы не отправили на утверждение прикрепленные за вами данные. Для авторизации в системе перейдите по ссылке: " + ConfigurationManager.AppSettings.Get("SiteName")
                        , null);
                     */
                    }

                    //EmailSendHistory esh0 = (from a in kPiDataContext.EmailSendHistories where a.FK_ReportsArchiveTable == Convert.ToInt32(button.CommandArgument) && a.Count == 0 select a).FirstOrDefault(); ;
                    EmailSendHistory esh1 = (from a in kPiDataContext.EmailSendHistory
                        where a.FK_ReportsArchiveTable == Convert.ToInt32(button.CommandArgument) && a.Count == 1
                        select a).FirstOrDefault();
                    ;

                    esh1.Date = DateTime.Now;
                    esh1.Value = " [ " + (uniqueMails.Count - errors).ToString() + "/" + uniqueMails.Count.ToString() +
                                 " ]";

                    kPiDataContext.SubmitChanges();

                    var date = (from a in kPiDataContext.EmailSendHistory
                        where a.FK_ReportsArchiveTable == Convert.ToInt32(button.CommandArgument) && a.Count == 1
                        select a.Date).FirstOrDefault();

                    int rowIndex = 0;
                    if (GridviewActiveCampaign.Rows.Count > 0)
                    {
                        for (int i = 1; i <= GridviewActiveCampaign.Rows.Count; i++)
                        {
                            Label LabelDate2 = (Label) GridviewActiveCampaign.Rows[rowIndex].FindControl("LabelDate2");
                            LabelDate2.Text = date + " " + " [ " + (uniqueMails.Count - errors).ToString() + "/" +
                                              uniqueMails.Count.ToString() + " ]";
                            rowIndex++;
                        }
                    }

                }
                Page_Load(null, null);
            }
            else
            {
                DisplayAlert("Снимите предохранитель!");
            }
        }

    }
}
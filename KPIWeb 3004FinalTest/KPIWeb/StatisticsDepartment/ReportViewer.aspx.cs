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
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10)&&(userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
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
        }

        protected void ButtonEditReport_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int reportArchiveTableID = 0;
            if (int.TryParse(button.CommandArgument, out reportArchiveTableID) && reportArchiveTableID > 0)
            {
                Serialization ReportID = new Serialization((int)reportArchiveTableID, null);
                Session["ReportArchiveTableID"] = ReportID;
                Response.Redirect("~/Reports/EditReport.aspx");
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
                Response.Redirect("~/Reports/GenerateReport.aspx");
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
            Response.Redirect("~/Reports/EditReport.aspx");
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
                Response.Redirect("~/Head/HeadShowResult.aspx");
            }
        }

        protected void ButtonViewStruct(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization;
                Response.Redirect("~/Head/HeadShowStructure.aspx");
            }
        }

        protected void ButtonMailSending_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();


                var emailListTo = (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
                    where a.FK_ReportArchiveTableId == Convert.ToInt32(button.CommandArgument) && a.Active
                    join b in kPiDataContext.UsersTable
                        on a.FK_FirstLevelSubmisionTableId equals b.FK_FirstLevelSubdivisionTable
                        where b.Active
                          select b.Email).ToList();




                foreach (var email in emailListTo)
                {
                   Action.MassMailing(email,"Новая отчетная кампания \"" + 
                       (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault() + "\"",
                       "Здравствуйте, " + email.ToString().Substring(0, email.ToString().LastIndexOf('@')) + ". Информируем Вас о начале новой отчетной кампании \"" +
                       (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault()
                       + "\", которая пройдет в период с " + 
                       ((from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.StartDateTime).FirstOrDefault()).ToString().Split()[0] +
                       " по " + (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.EndDateTime).FirstOrDefault().ToString().Split()[0] +
                       ". Для авторизации в системе перейдите по ссылке: " + "http:" + "//razvitie.cfu-portal.ru"
                       , null);
                }
            }
        }

        protected void ButtonMailSending2_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();


       
                var emailListToDebt = (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
                                       where a.FK_ReportArchiveTableId == Convert.ToInt32(button.CommandArgument) && a.Active
                                       join b in kPiDataContext.UsersTable on a.FK_FirstLevelSubmisionTableId equals b.FK_FirstLevelSubdivisionTable
                                       where b.Active
                                       join c in kPiDataContext.CollectedBasicParametersTable on b.UsersTableID equals c.FK_UsersTable
                                       where (c.Status != 0 || c.Status != null) && c.Active
                                       select b.Email).ToList();


                var uniqueMails = emailListToDebt.Distinct().ToList();

                foreach (var email in uniqueMails)
                {
                    Action.MassMailing(email, "Заполните данные в отчетной кампании \"" +
                        (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault() + "\"",
                        "Здравствуйте, " + email.ToString().Substring(0, email.ToString().LastIndexOf('@')) + ". Напоминаем вам о том, что Вы являетесь учатсником отчетной кампании \"" +
                        (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.Name).FirstOrDefault()
                        + "\", которая проходит в период с " +
                        ((from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.StartDateTime).FirstOrDefault()).ToString().Split()[0] +
                        " по " + (from a in kPiDataContext.ReportArchiveTable where a.ReportArchiveTableID == Convert.ToInt32(button.CommandArgument) select a.EndDateTime).FirstOrDefault().ToString().Split()[0] +
                        ". На данный момент Вы не отправили на утверждение прикрепленные за вами данные. Для авторизации в системе перейдите по ссылке: " + "http:" + "//razvitie.cfu-portal.ru"
                        , null);
            }
            }
        }
    }
}
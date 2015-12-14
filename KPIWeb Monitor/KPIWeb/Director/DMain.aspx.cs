using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PersonalPages;

namespace KPIWeb.Director
{
    public partial class DMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
            }

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 7 ,0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            if (!Page.IsPostBack)
            {
                #region get user reports

                List<ReportArchiveTable> reports = (from a in kpiWebDataContext.ReportArchiveTable
                                                        where a.Active == true
                                                    join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                    on a.ReportArchiveTableID equals b.FK_ReportArchiveTableId
                                                        where b.Active == true
                                                        && b.FK_FirstLevelSubmisionTableId == userTable.FK_FirstLevelSubdivisionTable
                                                        && a.StartDateTime < DateTime.Now
                                                        && a.OnlyForProrector == false
                                                    select a).Distinct().ToList();

                /// тут мы получили список активных отччетов пользователя
                /// пользователь привязан к таблице первого подразделения      

                int l_0 = userTable.FK_ZeroLevelSubdivisionTable == null ? 0 : (int)userTable.FK_ZeroLevelSubdivisionTable;
                int l_1 = userTable.FK_FirstLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FirstLevelSubdivisionTable;
                int l_2 = userTable.FK_SecondLevelSubdivisionTable == null ? 0 : (int)userTable.FK_SecondLevelSubdivisionTable;
                int l_3 = userTable.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)userTable.FK_ThirdLevelSubdivisionTable;
                int l_4 = userTable.FK_FourthLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FourthLevelSubdivisionTable;
                int l_5 = userTable.FK_FifthLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FifthLevelSubdivisionTable;
                int userLevel = 5;

                userLevel = l_5 == 0 ? 4 : userLevel;
                userLevel = l_4 == 0 ? 3 : userLevel;
                userLevel = l_3 == 0 ? 2 : userLevel;
                userLevel = l_2 == 0 ? 1 : userLevel;
                userLevel = l_1 == 0 ? 0 : userLevel;
                userLevel = l_0 == 0 ? -1 : userLevel;

                DataTable dataTable = new DataTable();

                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                foreach (ReportArchiveTable ReportRow in reports)
                {
                    #region
                 /*   int can_view = (from a in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                        where a.FK_ReportArchiveTableId == ReportRow.ReportArchiveTableID
                                        && a.FK_FirstLevelSubmisionTableId == user.FK_FirstLevelSubdivisionTable
                                        join b in kpiWebDataContext.ReportArchiveTable
                                        on a.FK_ReportArchiveTableId equals b.ReportArchiveTableID
                                        where a.Active == true && b.Active == true select b).Distinct().ToList().Count();
                                 */  
                                    /*
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where
                                         a.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID //из нужного отчёта
                                         && c.FK_UsersTable == userID // свяный с пользователем
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                                         && c.CanView == true
                                         && c.Active == true // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList().Count; */
                /*    if (can_view > 0)
                    {

                    }
                    else
                    {
                        continue;
                    }
                    */
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                    dataRow["ReportName"] = ReportRow.Name;
                    dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];//только дата// время обрезается сплитом
                    dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0];

                    /*List<SecondLevelSubdivisionTable> Faculties = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                   where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                                   && a.Active == true
                                                                   join b in kpiWebDataContext.UsersTable
                                                                   on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                                                   where b.Active == true
                                                                   join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                   on b.UsersTableID equals c.FK_UsersTable
                                                                   where c.Active == true
                                                                   && c.CanView == true
                                                                   select a).Distinct().ToList();  */
              /*      List<SecondLevelSubdivisionTable> Faculties = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                   where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                                   join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                                   on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                                                   where a.Active == true
                                                                   && b.Active == true
                                                                   select a).Distinct().ToList();*/
                    int All=0;
                    int AllConfirmed = 0;

                    All += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                            join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                            on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                            where a.Active == true
                            && b.Active == true
                            && b.FK_ReportArchiveTableId == ReportRow.ReportArchiveTableID
                            select a).Distinct().ToList().Count();

                    AllConfirmed += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                            join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                            on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                            where a.Active == true
                            && b.Active == true
                            join c in kpiWebDataContext.CollectedBasicParametersTable
                            on a.SecondLevelSubdivisionTableID  equals c.FK_SecondLevelSubdivisionTable
                            where (c.Status == 5 || c.Status == 4)
                            && c.Active == true
                            && c.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                            select a).Distinct().ToList().Count();
                    /*
                    AllConfirmed += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                     where a.Active == true
                                     && a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                     join b in kpiWebDataContext.CollectedBasicParametersTable
                                         on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                     where (b.Status == 5 || b.Status == 4)
                                     && b.Active == true
                                     && b.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                                     select a).Distinct().Count();   */               
                    string status="";

                    if (AllConfirmed == All)
                    {
                       /* int allconf2 = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                        where a.Active == true
                                        && a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                        join b in kpiWebDataContext.CollectedBasicParametersTable
                                            on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                        where b.Status == 5
                                        && b.Active == true
                                        && b.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                                        select a).Distinct().Count();*/

                        int allconf2 = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                         where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                         join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                         on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                         where a.Active == true
                         && b.Active == true
                         join c in kpiWebDataContext.CollectedBasicParametersTable
                         on a.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                         where c.Status == 5
                         && c.Active == true
                         && c.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                         select a).Distinct().ToList().Count();

                        if (allconf2 > (AllConfirmed / 2))
                        {
                            status = "Данные согласованы";
                        }
                        else
                        {
                            status = "Данные ожидают согласования";
                        }                       
                    }
                    else if (AllConfirmed < All)
                    {
                        if (AllConfirmed == 0)
                        {
                            status = "Готовы на 0%";
                        }
                        else
                        {
                            status = "Готовы на "+(AllConfirmed*100/All).ToString("0.0")+"%";
                        }
                    }
                    else
                    {
                        
                    }
                    //нужно определить статус данных                                     
                    dataRow["Status"] = status;
                    dataTable.Rows.Add(dataRow);

                    #endregion
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();

                if (GridView1.Rows.Count == 0)
                {
                    var name =
                        (from tr in kpiWebDataContext.UsersTable
                         where tr.UsersTableID == UserSer.Id
                         select tr.FirstLevelSubdivisionTable.Name).FirstOrDefault();
                    Label1.Visible = true;
                    
                    if (name != null) Label1.Text = "В данный момент для вашего подразделения (" + name + ") отсутствуют активные отчеты. Мы обязательно уведомим Вас о начале новой отчетной кампании.";
                    else Label1.Text = "В данный момент для вашего подразделения отсутствуют активные отчеты. Мы обязательно уведомим Вас о начале новой отчетной кампании.";

                   // Label2.Text = "С уважением администрация ИАС \"КФУ-Программа развития\"";
                } 


                #endregion
            }

        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчёта               
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                var login =
                        (from a in kpiWebDataContext.UsersTable
                         where a.UsersTableID == (int)ViewState["LocalUserID"]
                         select a.Email).FirstOrDefault();
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0CR1: Пользователь (Директор академии) " + login + " зашел на страницу просмотра отчета с ID = " + paramSerialization.ReportStr);

                Response.Redirect("~/Director/DReportView.aspx");
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }

        protected void Button23_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Director/DCheckedInfo.aspx");
        }
    }
}
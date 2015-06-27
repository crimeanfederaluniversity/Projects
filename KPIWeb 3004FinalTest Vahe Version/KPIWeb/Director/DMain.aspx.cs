using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Director
{
    public partial class DMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 4)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                #region get user reports
                List<ReportArchiveTable> reportsArchiveTablesTable = (
                                                from a in kpiWebDataContext.UsersTable
                                                join b in kpiWebDataContext.FirstLevelSubdivisionTable
                                                on a.FK_FirstLevelSubdivisionTable equals b.FirstLevelSubdivisionTableID
                                                join c in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId
                                                join d in kpiWebDataContext.ReportArchiveTable
                                                on c.FK_ReportArchiveTableId equals d.ReportArchiveTableID
                                                where a.UsersTableID == UserSer.Id
                                                && a.Active == true
                                                && b.Active == true
                                                && c.Active == true
                                                && d.Active == true
                                                && d.StartDateTime < DateTime.Now
                                                && d.EndDateTime > DateTime.Now
                                                select d).ToList();
                ///тут мы получили список активных отччетов пользователя
                /// пользователь привязан к таблице первого подразделения
                /// таблица первого подразделения привязана к таблице отчётов(через таблицу связи)
                /// на данный момент отчёт можно назначать только первому подразделению!!!                
                /// 

                UsersTable user = (from a in kpiWebDataContext.UsersTable
                                   where a.UsersTableID == userID
                                   select a).FirstOrDefault();

                int l_0 = user.FK_ZeroLevelSubdivisionTable == null ? 0 : (int)user.FK_ZeroLevelSubdivisionTable;
                int l_1 = user.FK_FirstLevelSubdivisionTable == null ? 0 : (int)user.FK_FirstLevelSubdivisionTable;
                int l_2 = user.FK_SecondLevelSubdivisionTable == null ? 0 : (int)user.FK_SecondLevelSubdivisionTable;
                int l_3 = user.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)user.FK_ThirdLevelSubdivisionTable;
                int l_4 = user.FK_FourthLevelSubdivisionTable == null ? 0 : (int)user.FK_FourthLevelSubdivisionTable;
                int l_5 = user.FK_FifthLevelSubdivisionTable == null ? 0 : (int)user.FK_FifthLevelSubdivisionTable;
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

                foreach (ReportArchiveTable ReportRow in reportsArchiveTablesTable)
                {
                    #region
                    int can_view =
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
                                         //&& d.SubvisionLevel == 3 //нужный уровень заполняющего
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                                         && c.CanView == true
                                         && c.Active == true // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList().Count;
                    if (can_view > 0)
                    {

                    }
                    else
                    {
                        continue;
                    }


                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                    dataRow["ReportName"] = ReportRow.Name;
                    dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];//только дата// время обрезается сплитом
                    dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0];

                    //нужно определить статус данных                                     
                    dataRow["Status"] = "В разработке";
                    dataTable.Rows.Add(dataRow);

                    #endregion
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
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
        
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
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
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                List<ReportArchiveTable> reportsArchiveTablesTable =  (
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
                /// таблица первого подразделения привязана к таблице отчетов(через таблицу связи)
                /// на данный момент отчет можно назначать только первому подразделению!!!                
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

                    if (userLevel == 3)
                    {
                        Button1.Visible = true;
                    }
                    else
                    {
                        Button1.Visible = false;
                    }
                /// 
                /// ///////////////////////
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
            
                foreach (ReportArchiveTable ReportRow in reportsArchiveTablesTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                                dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                                dataRow["ReportName"] = ReportRow.Name;
                                dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];//только дата// время обрезается сплитом
                                dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0]; ;
                                dataTable.Rows.Add(dataRow); 
                }           
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                ///вывели все отчеты с параметрами в гридвью          
            }
        }

        protected void ButtonEditClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета               
                Serialization modeSer = new Serialization(0,null,null);
                Session["mode"] = modeSer;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета               
                Serialization modeSer = new Serialization(1, null, null);
                Session["mode"] = modeSer;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void ButtonConfirmClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета               
                Serialization modeSer = new Serialization(2, null, null);
                Session["mode"] = modeSer;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/SpecializationParametrs.aspx");            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            for (int i = 1; i <= GridView1.Columns.Count; i++)
            {
                Button btnEdit = e.Row.FindControl("ButtonEditReport") as Button;
                Button btnView = e.Row.FindControl("ButtonViewReport") as Button;
                Button btnConfirm = e.Row.FindControl("ButtonConfirmReport") as Button;
                
                if ((btnEdit != null)&&(btnView != null)&&(btnConfirm != null))
                {
                    btnConfirm.Enabled = true;
                    btnEdit.Enabled = true;
                    btnView.Enabled = true;                         
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

                    if (userLevel == 3)
                    {
                        int ReportArchiveID = Convert.ToInt32(btnEdit.CommandArgument);

                        int kaf_edit =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where
                                 a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчета
                                 && c.FK_UsersTable == userID // свяный с пользователем
                                 && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                 && a.Active == true // запись в таблице связей показателя и отчета активна
                                 && c.CanEdit == true
                                 && c.Active == true // запись в таблице связей показателя и пользователей активна
                                 && d.Calculated == false
                             // этот показатель нужно вводить а не считать
                             select b).ToList().Count;
                        int kaf_view =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where
                                 a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчета
                                 && c.FK_UsersTable == userID // свяный с пользователем
                                 && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                 && a.Active == true // запись в таблице связей показателя и отчета активна
                                 && c.CanView == true
                                 && c.Active == true // запись в таблице связей показателя и пользователей активна
                                 && d.Calculated == false
                             // этот показатель нужно вводить а не считать
                             select b).ToList().Count;
                        int kaf_conf =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where
                                 a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчета
                                 && c.FK_UsersTable == userID // свяный с пользователем
                                 && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                 && a.Active == true // запись в таблице связей показателя и отчета активна
                                 && c.CanConfirm == true
                                 && c.Active == true // запись в таблице связей показателя и пользователей активна
                                 && d.Calculated == false
                             // этот показатель нужно вводить а не считать
                             select b).ToList().Count;

                        int specEdit =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where a.FK_ReportArchiveTable == ReportArchiveID //для отчета
                                   && d.SubvisionLevel == 4 // для уровня заполняющего
                                   && d.Calculated == false //только вводимые параметры
                                   && c.FK_UsersTable == userID // связаннаые с пользователем
                                   && a.Active == true
                                   && c.CanEdit == true
                                   && c.Active == true
                             select b).ToList().Count;
                        int specView =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where a.FK_ReportArchiveTable == ReportArchiveID //для отчета
                                   && d.SubvisionLevel == 4 // для уровня заполняющего
                                   && d.Calculated == false //только вводимые параметры
                                   && c.FK_UsersTable == userID // связаннаые с пользователем
                                   && a.Active == true
                                   && c.CanView == true
                                   && c.Active == true
                             select b).ToList().Count;
                        int specConf =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where a.FK_ReportArchiveTable == ReportArchiveID //для отчета
                                   && d.SubvisionLevel == 4 // для уровня заполняющего
                                   && d.Calculated == false //только вводимые параметры
                                   && c.FK_UsersTable == userID // связаннаые с пользователем
                                   && a.Active == true
                                   && c.CanConfirm == true
                                   && c.Active == true
                             select b).ToList().Count;

                        btnEdit.Enabled = (kaf_edit + specEdit) > 0 ? true : false;
                        btnView.Enabled = (kaf_view + specView) > 0 ? true : false;
                        btnConfirm.Enabled = (kaf_conf + specConf) > 0 ? true : false;
                    }                   
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Head
{
    public partial class HeadShowReportResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable_ =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable_.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем

            if (mode == 1)
            {
                Button1.Text = "Вернуться в меню выбора";
            }
            else if (mode == 2)
            {
                Button1.Text = "Подтвердить правильность рассчитанных данных";
            }
            if (!Page.IsPostBack)
            {
                int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                UsersTable userTable = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == UserID select a).FirstOrDefault();
                int l_0 = (userTable.FK_ZeroLevelSubdivisionTable == null)?0:(int)userTable.FK_ZeroLevelSubdivisionTable;
                int l_1 = (userTable.FK_FirstLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_FirstLevelSubdivisionTable;
                int l_2 = (userTable.FK_SecondLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_SecondLevelSubdivisionTable;
                int l_3 = (userTable.FK_ThirdLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_ThirdLevelSubdivisionTable;
                int l_4 = (userTable.FK_FourthLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_FourthLevelSubdivisionTable;
                int l_5 = (userTable.FK_FifthLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_FifthLevelSubdivisionTable;

                Serialization level = (Serialization)Session["level"];
                if (level == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                //int level = level.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем
                l_0 = level.l0;
                l_1 = level.l1;
                l_2 = level.l2;
                l_3 = level.l3;
                l_4 = level.l4;
                l_5 = level.l5;

                DataTable dt_basic = new DataTable();
                DataTable dt_calculate = new DataTable();
                DataTable dt_indicator = new DataTable();

                dt_basic.Columns.Add(new DataColumn("BasicParametrsName", typeof(string)));
                dt_basic.Columns.Add(new DataColumn("BasicParametrsResult", typeof(string)));
                dt_basic.Columns.Add(new DataColumn("checkBoxBasicId", typeof(string)));
                dt_basic.Columns.Add(new DataColumn("checkBoxBasic", typeof(string)));

                dt_calculate.Columns.Add(new DataColumn("CalculatedParametrsName", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("CalculatedParametrsResult", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("checkBoxCalcId", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("checkBoxCalc", typeof(string)));

                dt_indicator.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
                dt_indicator.Columns.Add(new DataColumn("IndicatorResult", typeof(string)));
                dt_indicator.Columns.Add(new DataColumn("checkBoxIndId", typeof(string)));
                dt_indicator.Columns.Add(new DataColumn("checkBoxInd", typeof(string)));

                List<BasicParametersTable> list_basicParametrs = 
                    (from a in kpiWebDataContext.BasicParametersTable
                        join b in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                        on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                        join c in kpiWebDataContext.BasicParametrsAndUsersMapping 
                        on a.BasicParametersTableID equals  c.FK_ParametrsTable
                        where b.FK_ReportArchiveTable == ReportArchiveID
                        && c.FK_UsersTable == UserID
                        && (((c.CanEdit == true) && mode == 0)
                        || ((c.CanView == true) && mode == 1)
                        || ((c.CanConfirm == true) && mode == 2))
                        select  a).ToList();
                List<CalculatedParametrs> list_calcParams = 
                    (from a in kpiWebDataContext.CalculatedParametrs
                        join b in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                        on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                        join c in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                        on a.CalculatedParametrsID equals c.FK_CalculatedParametrsTable
                        where b.FK_ReportArchiveTable == ReportArchiveID 
                        && c.FK_UsersTable == UserID
                         && (((c.CanEdit == true) && mode == 0)
                                || ((c.CanView == true) && mode == 1)
                                || ((c.CanConfirm == true) && mode == 2))
                        select  a).ToList();
                List<IndicatorsTable> list_indicators =
                    (from a in kpiWebDataContext.IndicatorsTable
                     join b in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                     on a.IndicatorsTableID equals b.FK_IndicatorsTable
                     join c in kpiWebDataContext.IndicatorsAndUsersMapping
                     on a.IndicatorsTableID equals c.FK_IndicatorsTable
                     where b.FK_ReportArchiveTable == ReportArchiveID
                     && c.FK_UsresTable == UserID
                     && (((c.CanEdit == true) && mode == 0)
                                || ((c.CanView == true) && mode == 1)
                                || ((c.CanConfirm == true) && mode == 2))
                     select a).ToList();

                foreach (BasicParametersTable basicParametr in list_basicParametrs)
                {
                    DataRow dataRow = dt_basic.NewRow();
                    dataRow["BasicParametrsName"] = basicParametr.Name;
                    dataRow["BasicParametrsResult"] = CalculateAbb.SumForLevel(basicParametr.BasicParametersTableID, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5).ToString();
                    dataRow["checkBoxBasicId"] = 1;
                    dt_basic.Rows.Add(dataRow);
                }
                
                foreach (CalculatedParametrs calcParam in list_calcParams)
                {
                    DataRow dataRow = dt_calculate.NewRow();
                    dataRow["CalculatedParametrsName"] = calcParam.Name;
                    dataRow["CalculatedParametrsResult"] = CalculateAbb.CalculateForLevel(calcParam.Formula, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5, 0);
                    dataRow["checkBoxCalcId"] = 1;
                    dt_calculate.Rows.Add(dataRow);
                }

                foreach (IndicatorsTable indicator in list_indicators)
                {
                    DataRow dataRow = dt_indicator.NewRow();
                    dataRow["IndicatorName"] = indicator.Name;
                    dataRow["IndicatorResult"] = CalculateAbb.CalculateForLevel(indicator.Formula, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5, 0);
                    dataRow["checkBoxIndId"] = 1;
                    dt_indicator.Rows.Add(dataRow);
                }

                BasicParametrsTable.DataSource = dt_basic;
                BasicParametrsTable.DataBind();
                CalculatedParametrsTable.DataSource = dt_calculate;
                CalculatedParametrsTable.DataBind();
                IndicatorsTable.DataSource = dt_indicator;
                IndicatorsTable.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем

            if (mode == 1)
            {
                Response.Redirect("~/Default.aspx");
            }
            else if (mode == 2)
            {
                //вытаскиваем и подтвердаем
            }
        }
    }
}
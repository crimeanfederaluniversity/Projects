using System;
using System.Collections.Generic;
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
                Response.Redirect("~/Account/Login.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Account/Login.aspx");
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

                
                DataTable dt_basic = new DataTable();
                DataTable dt_calculate = new DataTable();
                DataTable dt_indicator = new DataTable();

                dt_basic.Columns.Add(new DataColumn("BasicParametrsName", typeof(string)));
                dt_basic.Columns.Add(new DataColumn("BasicParametrsResult", typeof(string)));

                dt_calculate.Columns.Add(new DataColumn("CalculatedParametrsName", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("CalculatedParametrsResult", typeof(string)));

                dt_indicator.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
                dt_indicator.Columns.Add(new DataColumn("IndicatorResult", typeof(string)));

                List<BasicParametersTable> list_basicParametrs = 
                    (from a in kpiWebDataContext.BasicParametersTable
                        join b in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                        on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                        where b.FK_ReportArchiveTable == ReportArchiveID 
                        select  a).ToList();
                List<CalculatedParametrs> list_calcParams = 
                    (from a in kpiWebDataContext.CalculatedParametrs
                        join b in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                        on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                        where b.FK_ReportArchiveTable == ReportArchiveID 
                        select  a).ToList();
                List<IndicatorsTable> list_indicators =
                    (from a in kpiWebDataContext.IndicatorsTable
                     join b in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                     on a.IndicatorsTableID equals b.FK_IndicatorsTable
                     where b.FK_ReportArchiveTable == ReportArchiveID
                     select a).ToList();

                foreach (BasicParametersTable basicParametr in list_basicParametrs)
                {
                    DataRow dataRow = dt_basic.NewRow();
                    dataRow["BasicParametrsName"] = basicParametr.Name;
                    dataRow["BasicParametrsResult"] = CalculateAbb.SumForLevel(basicParametr.BasicParametersTableID, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5).ToString();
                    dt_basic.Rows.Add(dataRow);
                }
                

                foreach (CalculatedParametrs calcParam in list_calcParams)
                {
                    DataRow dataRow = dt_calculate.NewRow();
                    dataRow["CalculatedParametrsName"] = calcParam.Name;
                    dataRow["CalculatedParametrsResult"] = CalculateAbb.CalculateForLevel(calcParam.Formula, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5);
                    dt_calculate.Rows.Add(dataRow);
                }

                foreach (IndicatorsTable indicator in list_indicators)
                {
                    DataRow dataRow = dt_indicator.NewRow();
                    dataRow["IndicatorName"] = indicator.Name;
                    dataRow["IndicatorResult"] = CalculateAbb.CalculateForLevel(indicator.Formula, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5);
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
    }
}
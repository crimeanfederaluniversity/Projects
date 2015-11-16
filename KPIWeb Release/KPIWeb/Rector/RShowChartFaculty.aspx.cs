using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RShowChartFaculty : System.Web.UI.Page
    {        
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

            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }

            int reportId = 0;
            if (!Page.IsPostBack)
            {
                RectorChartSession RectorChart = (RectorChartSession) Session["RectorChart"];
                if (RectorChart == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                reportId = RectorChart.reportId;
                ViewState["reportId"] = reportId;
                RectorChooseReportClass rectorChooseReportClass = new RectorChooseReportClass();
                RectorChooseReportDropDown.Items.AddRange(rectorChooseReportClass.GetListItemCollectionWithReports());
                RectorChooseReportDropDown.SelectedValue = reportId.ToString();
            }

            String AcademyID = (string)Session["AcademyToDetailed"];
            int academy = Convert.ToInt32(AcademyID);

            String IndicatorID = (string)Session["IndicatorToDetailed"];
            int indicator = Convert.ToInt32(IndicatorID);

            ChartItems chartItems = new ChartItems();
            ForRCalc forRCalc = new ForRCalc();
            ChartValueArray DataForChart = forRCalc.IndicatorsForAcademyFacultys(indicator, academy, reportId);

            // Формируем GridView
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Ratio", typeof(string)));
            dataTable.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("IndicatorValue", typeof(string)));

            #region график + чуток gridview
            // Форматировать диаграмму
            Chart1.BackColor = Color.White;
            Chart1.BackSecondaryColor = Color.WhiteSmoke;
            Chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderlineColor = Color.Black;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматировать область диаграммы
            Chart1.ChartAreas[0].BackColor = Color.White;

            // Добавить и форматировать заголовок
            Chart1.Titles.Add(DataForChart.chartName);
            Chart1.Titles[0].Font = new Font("Utopia", 16);

            Chart1.Series.Add(new Series("Default")
            {
                ChartType = SeriesChartType.StackedBar,
                Color = Color.SteelBlue
            });

            Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

            int ratio = 1;
            var measure  = (from ind in kPiDataContext.IndicatorsTable
             where ind.IndicatorsTableID == indicator
             select ind.Measure).FirstOrDefault().ToString();

            foreach (ChartOneValue item in chartItems.SortReverse(DataForChart.ChartValues)) // сортировка для gridview FIFO
            {
                if (item.value == 0) continue;

                DataRow dataRow = dataTable.NewRow();
                dataRow["IndicatorID"] =
                    (from a in kPiDataContext.FirstLevelSubdivisionTable
                     where a.Name.Equals(item.name)
                     select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                dataRow["Ratio"] = ratio;
                dataRow["IndicatorName"] = item.name;


                IndicatorsTable CurrentIndicator = (from a in kPiDataContext.IndicatorsTable
                                                    where a.IndicatorsTableID == indicator
                                                    select a).FirstOrDefault();

                dataRow["IndicatorValue"] = forRCalc.FloatToStrFormat(item.value, item.planned, (Int32)CurrentIndicator.DataType) + " " + measure; //////////


                dataTable.Rows.Add(dataRow);

                ratio++;
            }

            
            foreach (ChartOneValue item in chartItems.Sort(DataForChart.ChartValues)) // для chart FILO
            {
            if (item.value == 0) continue;

            IndicatorsTable CurrentIndicator = (from a in kPiDataContext.IndicatorsTable
                                                where a.IndicatorsTableID == indicator
                                                select a).FirstOrDefault();
            float value = item.value;
            if (CurrentIndicator.DataType == 1)
                value = (float)Math.Ceiling(value);

            chartItems.AddChartItem(item.name, value);

            //chartItems.AddChartItem(item.name, item.value);

            }
            // Привязать источник к диаграмме
            Chart1.DataSource = chartItems.GetDataSource();
            Chart1.Series[0].XValueMember = "Name";
            Chart1.Series[0].YValueMembers = "Value";

            if (measure.Length > 3)
            {
                Chart1.Series[0].Label = "#VALY" + " " +measure.Substring(0, 3);
            }
            else
            {
                Chart1.Series[0].Label = "#VALY" + " " +measure;
            }
            
            Chart1.Series[0].ToolTip = "#VALX #VALY"+ " " + measure;
            #endregion

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            if (GridView1.Rows.Count < 1)
            {
                noDataMessage.Visible = true;
                Chart1.Visible = false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void RectorChooseReportDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"];
            if (RectorChart == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            RectorChart.reportId = Convert.ToInt32(RectorChooseReportDropDown.SelectedValue);
            Session["RectorChart"] = RectorChart;
            Response.Redirect("~/Rector/RShowChartFaculty.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RShowChartDetailed.aspx");
        }
    }
}
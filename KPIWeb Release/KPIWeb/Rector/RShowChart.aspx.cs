using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RShowChart : System.Web.UI.Page
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

            RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"];
            if (RectorChart == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            List<int> IndicatorsList = RectorChart.IndicatorsList;
            int reportId = RectorChart.reportId;
            ViewState["IndicatorsList"] = IndicatorsList;
            ViewState["reportId"] = reportId;

            if (RectorChart.prorectorId != null)
            {
                UsersTable currentProrector = (from a in kPiDataContext.UsersTable
                    where a.UsersTableID == Convert.ToInt32(RectorChart.prorectorId)
                    select a).FirstOrDefault();
                prorectorNameDiv.Visible = true;
                prorectorNameLabel.Text = currentProrector.Position;
            }
            //IndicatorsForCFU(IndicatorsList,1);   // ВОЗВРАЩАЕТ ДАННЫЕ ДЛЯ ЧАРТА

            if (!IsPostBack)
            {

                RectorChooseReportClass rectorChooseReportClass = new RectorChooseReportClass();
                RectorChooseReportDropDown.Items.AddRange(rectorChooseReportClass.GetListItemCollectionWithReports());
                RectorChooseReportDropDown.SelectedValue = reportId.ToString();
                // Формируем GridView
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Button_Text", typeof(string)));
                dataTable.Columns.Add(new DataColumn("IsResponsibleForWrongData", typeof(string)));
                foreach (var item in IndicatorsList)
                {
                    string namestr =
                        (from i in kPiDataContext.IndicatorsTable where i.IndicatorsTableID == item select i.Name)
                            .FirstOrDefault();
                    string tmp;

                    if (namestr.Length > 124) tmp = namestr.Substring(0, 125) + "...";
                    else tmp = namestr;

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["IndicatorID"] = item;
                    dataRow["IndicatorName"] = tmp;
                    dataRow["Button_Text"] = "Подробнее";
                    dataRow["IsResponsibleForWrongData"] = "";
                    if (((item == 1019) || (item == 1020) || (item == 1021) || (item == 1022) || (item == 1023) || (item == 1033) || (item == 1037))&& (reportId == 4 || reportId == 4))
                    {
                        // dataRow["Button_Text"] = "Подробнее" + Environment.NewLine + "(рассчитано по" + Environment.NewLine + "неполным данным)";
                        dataRow["IsResponsibleForWrongData"] = "Недостаточно данных:"+Environment.NewLine+"Проректор по научной деятельности";
                    }

                    if (((item == 1016) || (item == 1017) || (item == 1018) || (item == 1029) || (item == 1030) || (item == 1032) || (item == 1036) || (item == 1038)) && (reportId == 4 || reportId == 4))
                    {
                        // dataRow["Button_Text"] = "Подробнее" + Environment.NewLine + "(рассчитано по" + Environment.NewLine + "неполным данным)";
                        dataRow["IsResponsibleForWrongData"] = "Недостаточно данных:" + Environment.NewLine + "Проректор по учебной и методической деятельности";
                    }
 
                  /*  
                    if ((item == 1027) || (item == 1028) || (item == 1026))
                        dataRow["Button_Text"] = "Подробнее" + Environment.NewLine + "(рассчитано по" + Environment.NewLine + "неполным данным)";
                  */
                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();

            }
        }       
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int reportId = (int) ViewState["reportId"];
                //// tooltip
                ForRCalc forRCalc = new ForRCalc();
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<int> IndicatorsList = (List<int>)ViewState["IndicatorsList"];
                var indicator = (from ind in kPiDataContext.IndicatorsTable
                                 where ind.IndicatorsTableID == IndicatorsList[e.Row.RowIndex]
                                 select ind).FirstOrDefault();

                        // ERROR != null

                string tooltip = indicator.Name;
                if (tooltip.Count() > 124)
                    e.Row.Cells[1].ToolTip = tooltip;
                ///////
                
                string indID = DataBinder.Eval(e.Row.DataItem, "IndicatorID").ToString();
                Chart chart = (Chart)e.Row.FindControl("Chart3");
                Button button = (Button)e.Row.FindControl("Button7");
                //chart.TempDirectory = "/app/tmp"; 
                if (chart != null)
                {
                    ChartOneValue DataForChart = forRCalc.IndicatorsForCFUOneIndicator(Convert.ToInt32(indID), reportId); // 1 индикатор в разрезе КФУ взятый по ID

                    // Формирую чарт
                    //chart.Series["ValueSeries"].Color = Color.CornflowerBlue;   //Color.FromArgb(255, 100, 149, 0);
                    //chart.Series["ValueSeries"].Color = Color.FromArgb(255, 100, 149, 237);
                    chart.Series["ValueSeries"].Color = Color.FromArgb(255, 100, 149, 237);
                    chart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount; // масштабирование разметки
                    chart.ChartAreas["ChartArea1"].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount; // -==--
                    //chart.Series["TargetSeries"].Color = Color.FromArgb(122, 50, 255, 0);
                    chart.Series["TargetSeries"].Color = Color.FromArgb(75, 20, 30, 0);

                    ChartItems chartItems = new ChartItems(); // класс для работы с чартом

                    chartItems.AddChartItem(DataForChart.name, DataForChart.value); // добавляем по оси X value

                    if (DataForChart.planned > 0)
                        chart.Series["TargetSeries"].Points.AddY(DataForChart.planned); // добавляем по оси Y Target value

                    // Привязываем класс с объектом к диаграмме
                    chart.DataSource = chartItems.GetDataSource();
                    //chart.Series["ValueSeries"].XValueMember = "Name";
                    chart.Series["ValueSeries"].YValueMembers = "Value";
                    if (DataForChart.value != 0)
                    {
                        chart.Series["ValueSeries"].Label = "#VALY " + indicator.Measure;
                        chart.Series["ValueSeries"].Font = new Font("Arial", 10f, FontStyle.Bold);
                    }
                    chart.Series["ValueSeries"].LegendText = "#AXISLABEL (#PERCENT{P0})";
                    if (DataForChart.planned != 0)
                    {
                        chart.Series["ValueSeries"].ToolTip = "Целевое: " + DataForChart.planned + ". Достигнуто на: " + Convert.ToInt32(DataForChart.value / DataForChart.planned * 100) + "%";
                        chart.Series["TargetSeries"].ToolTip = "Целевое: " + DataForChart.planned + ". Достигнуто на: " + Convert.ToInt32(DataForChart.value / DataForChart.planned * 100) + "%";
                    }
                    else
                    {
                        chart.Series["ValueSeries"].ToolTip = "Целевое: " + DataForChart.planned ;
                        chart.Series["TargetSeries"].ToolTip = "Целевое: " + DataForChart.planned;
                    }

                    // Линния планового значения
                    VerticalLineAnnotation verticalLine = new VerticalLineAnnotation();
                    verticalLine.AxisX = chart.ChartAreas["ChartArea1"].AxisX;
                    verticalLine.AxisY = chart.ChartAreas["ChartArea1"].AxisY;
                    verticalLine.Width = 3;
                    verticalLine.IsInfinitive = true; // либо высоту
                    verticalLine.LineDashStyle = ChartDashStyle.Solid;
                    verticalLine.LineColor = Color.Crimson;
                    verticalLine.LineWidth = 3;
                    verticalLine.AnchorX = DataForChart.planned;
                    verticalLine.Name = "myLine"; // !!
                    verticalLine.AnchorY = 0;
                    verticalLine.X = DataForChart.planned; ;
                    verticalLine.Y = 0;

                    //Прямоугольник со значением 
                    RectangleAnnotation RA = new RectangleAnnotation();
                    RA.AxisX = chart.ChartAreas["ChartArea1"].AxisX;
                    RA.IsSizeAlwaysRelative = false;

                    // КОСТЫЛЬ "формула" расчета масштабируемости прямоугольника
                    try
                    {
                        if (DataForChart.value > DataForChart.planned)
                        {
                            int tmp = Math.Round(DataForChart.value, 3).ToString().Count();
                            if (tmp > 3)
                                RA.Width = 20*DataForChart.value/(500 - ((tmp - 3)*60));
                            else
                            {
                                RA.Width = 20*DataForChart.value/500;
                            }
                        }
                        else
                        {
                            int tmp = Math.Round(DataForChart.planned, 3).ToString().Count();
                            if (tmp > 3)
                                RA.Width = 20*DataForChart.planned/(500 - ((tmp - 3)*60));
                            else
                            {
                                RA.Width = 20*DataForChart.planned/500;
                            }
                        }
                    }
                    catch 
                    {
                        RA.Width = 290000000 - 1 ; 
                    }
                    // END КОСТЫЛЬ
                    
                    RA.Height = 8 * 0.04;       
                    verticalLine.Name = "myRect"; // !!
                    RA.LineColor = Color.Red;
                    RA.BackColor = Color.Red;
                    RA.AxisY = chart.ChartAreas["ChartArea1"].AxisY;
                    RA.Y = -RA.Height;
                    RA.X = verticalLine.X - RA.Width / 2;

                    RA.Text = DataForChart.planned.ToString();
                    RA.ForeColor = Color.White;
                    RA.Font = new System.Drawing.Font("Arial", 8f, FontStyle.Bold);


                    // Consider adding transparency so that the strip lines are lighter
                   // stripLine1.BackColor = Color.FromArgb(200, 200, 0, 0);

                   // stripLine1.BackSecondaryColor = Color.FromArgb(122, 250, 255, 0);
                   // stripLine1.BackGradientStyle = GradientStyle.LeftRight;

                    // Add the strip line to the chart

                    chart.Annotations.Add(verticalLine);
                    chart.Annotations.Add(RA);

                    if (Convert.ToInt32(indID) == 1028 || Convert.ToInt32(indID) == 1027 || Convert.ToInt32(indID) == 1026 || DataForChart.value == 0)
                    {
                        button.Enabled = false;
                    }

                    if (reportId == 100500 && (Convert.ToInt32(indID) == 1021
                                               || Convert.ToInt32(indID) == 1023
                                               || Convert.ToInt32(indID) == 1026
                                               || Convert.ToInt32(indID) == 1027
                                               || Convert.ToInt32(indID) == 1033))
                    {
                        button.Enabled = false;
                    }

                    List<CollectedIndicatorsForR> currentCollectedForRectorList =
                        (from a in kPiDataContext.CollectedIndicatorsForR
                            where (a.FK_ReportArchiveTable == reportId || reportId == 0 || reportId == 100500)
                            && a.FK_FirstLevelSubdivisionTable != null
                                  && a.Active == true
                                  && a.FK_IndicatorsTable == Convert.ToInt32(indID)
                                  && a.Value != null
                                  && a.Value != 0
                            select a).ToList();
                    if (currentCollectedForRectorList.Count == 0)
                    {
                        button.Enabled = false;
                        
                    }
                }
            }
        }
        protected void DetailedButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var par = button.CommandArgument.ToString();
                Session["IndicatorToDetailed"] = par;
                Response.Redirect("~/Rector/RShowChartDetailed.aspx");
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {

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
            Response.Redirect("~/Rector/RShowChart.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RAnalitics.aspx");
        }
    }
}
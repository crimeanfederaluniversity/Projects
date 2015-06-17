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
            ViewState["IndicatorsList"] = IndicatorsList;
            
            //IndicatorsForCFU(IndicatorsList,1);   // ВОЗВРАЩАЕТ ДАННЫЕ ДЛЯ ЧАРТА

            if (!IsPostBack)
            {
                // Формируем GridView
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("IndicatorName", typeof(string)));

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
                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();

            }
        }
        public ChartOneValue GetCalculatedIndicator(int ReportID, IndicatorsTable Indicator, FirstLevelSubdivisionTable Academy, SecondLevelSubdivisionTable Faculty) // academyID == null && facultyID==null значит для всего КФУ
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            float Planned_Value = 0;
            string Name_ = "";
            float Value_ = 0;
            #region plannedIndicator
            PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                             where a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                                                   && a.Date > DateTime.Now
                                             select a).OrderBy(x => x.Date).FirstOrDefault();
            if (plannedValue != null)
            {
                Planned_Value = (float)plannedValue.Value;
            }
            #endregion
            #region Name
            if ((Academy == null) && (Faculty == null))
            {
                if (Indicator.Measure != null)
                {
                    if (Indicator.Measure.Length > 0)
                    {
                        Name_ = Indicator.Name + " (" + Indicator.Measure + ")";
                    }
                    else
                    {
                        Name_ = Indicator.Name;
                    }
                }
                else
                {
                    Name_ = Indicator.Name;
                }
            }
            else if (Faculty != null)
            {
                Name_ = Faculty.Name;
            }
            else if (Academy != null)
            {
                Name_ = Academy.Name;
            }



            #endregion
            #region
            //ForRCalc.Struct mainStruct = mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
            CollectedIndicatorsForR collected = new CollectedIndicatorsForR();
            if ((Academy == null) && (Faculty == null))
            {
                //mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
                collected = (from a in kpiWebDataContext.CollectedIndicatorsForR
                             where a.FK_ReportArchiveTable == ReportID
                             && a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                             && a.FK_FirstLevelSubdivisionTable == null
                             && a.FK_SecondLevelSubdivisionTable == null
                             select a).FirstOrDefault();
            }
            else if (Faculty != null)
            {
                //mainStruct = new ForRCalc.Struct(1, Faculty.FK_FirstLevelSubdivisionTable, Faculty.SecondLevelSubdivisionTableID, 0, 0, "N");
                collected = (from a in kpiWebDataContext.CollectedIndicatorsForR
                             where a.FK_ReportArchiveTable == ReportID
                             && a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                             && a.FK_FirstLevelSubdivisionTable == Faculty.FK_FirstLevelSubdivisionTable
                             && a.FK_SecondLevelSubdivisionTable == Faculty.SecondLevelSubdivisionTableID
                             select a).FirstOrDefault();
            }
            else if (Academy != null)
            {
                //mainStruct = new ForRCalc.Struct(1, Academy.FirstLevelSubdivisionTableID, 0, 0, 0, "N");
                collected = (from a in kpiWebDataContext.CollectedIndicatorsForR
                             where a.FK_ReportArchiveTable == ReportID
                             && a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                             && a.FK_FirstLevelSubdivisionTable == Academy.FirstLevelSubdivisionTableID
                             && a.FK_SecondLevelSubdivisionTable == null
                             select a).FirstOrDefault();
            }
            /*    
        float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0, Indicator.IndicatorsTableID, ReportID, 0));

        if (tmp == (float)1E+20)
        {
            Value_ = 0;
        }
        else
        {
            Value_ = tmp;
        }
        */

            if (collected.Value == null)
            {
                Value_ = 0;
            }
            else
            {
                Value_ = (float)collected.Value;
            }
            #endregion
            ChartOneValue DataRowForChart = new ChartOneValue(Name_, Value_, Planned_Value);
            return DataRowForChart;
        }
        public ChartValueArray AllIndicatorsForAcademys(int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            List<IndicatorsTable> Indicators = (
                            from a in kpiWebDataContext.IndicatorsTable
                            where
                                a.Active == true
                            select a).OrderBy(mc => mc.IndicatorsTableID).ToList();

            ChartValueArray DataForChart = new ChartValueArray("График достижения плановых значений целевых показателей");
            foreach (IndicatorsTable CurrentIndicator in Indicators)
            {
                DataForChart.ChartValues.Add(GetCalculatedIndicator(1, CurrentIndicator, null, null));
            }
            return DataForChart;
        }
        public ChartValueArray IndicatorForAllAcademys(int IndicatorID, int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                         where a.IndicatorsTableID == IndicatorID
                                         select a).FirstOrDefault();
            List<FirstLevelSubdivisionTable> AcademyList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                            where a.Active == true
                                                            select a).ToList();

            ChartValueArray DataForChart = new ChartValueArray("Целевой показатель '" + Indicator.Name + "' в разрезе академий КФУ");

            foreach (FirstLevelSubdivisionTable CurrentAcademy in AcademyList)
            {
                DataForChart.ChartValues.Add(GetCalculatedIndicator(1, Indicator, CurrentAcademy, null));
            }
            return DataForChart;
        }
        public ChartValueArray AllIndicatorsForOneAcademy(int AcademyID, int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            List<IndicatorsTable> Indicators = (
                            from a in kpiWebDataContext.IndicatorsTable
                            where
                                a.Active == true
                            select a).OrderBy(mc => mc.IndicatorsTableID).ToList();
            FirstLevelSubdivisionTable FirstLevelRow = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                        where a.FirstLevelSubdivisionTableID == AcademyID
                                                        select a).FirstOrDefault();
            ChartValueArray DataForChart = new ChartValueArray("График достижения плановых значений целевых показателей для академии " + FirstLevelRow.Name);
            foreach (IndicatorsTable CurrentIndicator in Indicators)
            {
                DataForChart.ChartValues.Add(GetCalculatedIndicator(1, CurrentIndicator, FirstLevelRow, null));
            }
            return DataForChart;
        }
        public ChartValueArray IndicatorsForCFU(List<int> Indicators, int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            ChartValueArray DataForChart = new ChartValueArray("График достижения выбранных плановых значений целевых показателей для КФУ");
            foreach (int CurrentIndicatorID in Indicators)
            {
                IndicatorsTable Indicator = (
                               from a in kpiWebDataContext.IndicatorsTable
                               where
                                   a.Active == true
                                   && a.IndicatorsTableID == CurrentIndicatorID
                               select a).FirstOrDefault();
                DataForChart.ChartValues.Add(GetCalculatedIndicator(1, Indicator, null, null));
            }
            return DataForChart;
        }
        public ChartOneValue IndicatorsForCFUOneIndicator(int curIndicator, int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            IndicatorsTable Indicator = (
                           from a in kpiWebDataContext.IndicatorsTable
                           where
                               a.Active == true
                               && a.IndicatorsTableID == curIndicator
                           select a).FirstOrDefault();

            return GetCalculatedIndicator(1, Indicator, null, null);
        }
        public ChartValueArray IndicatorsForAllFacultys(int IndicatorID, int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                         where a.IndicatorsTableID == IndicatorID
                                         select a).FirstOrDefault();
            List<SecondLevelSubdivisionTable> FacultyList = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                             where a.Active == true
                                                             select a).ToList();

            ChartValueArray DataForChart = new ChartValueArray("Целевой показатель '" + Indicator.Name + "' в разрезе факультетов КФУ");

            foreach (SecondLevelSubdivisionTable CurrentFavulty in FacultyList)
            {
                FirstLevelSubdivisionTable Academy = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                      where a.FirstLevelSubdivisionTableID == CurrentFavulty.FK_FirstLevelSubdivisionTable
                                                      select a).FirstOrDefault();
                DataForChart.ChartValues.Add(GetCalculatedIndicator(1, Indicator, Academy, CurrentFavulty));
            }
            return DataForChart;
        }

        protected void Button1_Click(object sender, EventArgs e) //Значение каждого отмеченного показателя рассчитанное по КФУ
        {
            RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"]; //вытаскиваем из сессии список айдишников показателей
            ChartValueArray NewDataForChart = IndicatorsForCFU(RectorChart.IndicatorsList, 1);
        }

        
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //// tooltip
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
                if (chart != null)
                {
                    ChartOneValue DataForChart = IndicatorsForCFUOneIndicator(Convert.ToInt32(indID), 1); // 1 индикатор в разрезе КФУ взятый по ID

                    // Формирую чарт
                    //chart.Series["ValueSeries"].Color = Color.CornflowerBlue;   //Color.FromArgb(255, 100, 149, 0);
                    chart.Series["ValueSeries"].Color = Color.FromArgb(255, 100, 149, 237);
                    chart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount; // масштабирование разметки
                    chart.ChartAreas["ChartArea1"].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount; // -==--
                    chart.Series["TargetSeries"].Color = Color.FromArgb(122, 50, 255, 0);

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
                    chart.Series["ValueSeries"].ToolTip = "Целевое: " + DataForChart.planned + ". Достигнуто на: " + Convert.ToInt32(DataForChart.value / DataForChart.planned * 100) + "%";
                    chart.Series["TargetSeries"].ToolTip = "Целевое: " + DataForChart.planned + ". Достигнуто на: " + Convert.ToInt32(DataForChart.value / DataForChart.planned * 100) + "%";


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
                    RA.Font = new System.Drawing.Font("Arial", 8f);


                    // Consider adding transparency so that the strip lines are lighter
                   // stripLine1.BackColor = Color.FromArgb(200, 200, 0, 0);

                   // stripLine1.BackSecondaryColor = Color.FromArgb(122, 250, 255, 0);
                   // stripLine1.BackGradientStyle = GradientStyle.LeftRight;

                    // Add the strip line to the chart

                    chart.Annotations.Add(verticalLine);
                    chart.Annotations.Add(RA);
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
    }
}
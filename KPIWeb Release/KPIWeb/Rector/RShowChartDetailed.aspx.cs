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
    public partial class RShowChartDetailed : System.Web.UI.Page
    {
        public string FloatToStrFormat(float value, float plannedValue, int DataType)
        {
            if (DataType == 1)
            {
                string tmpValue = Math.Ceiling(value).ToString();// value.ToString("0");
                return tmpValue;
            }
            else if (DataType == 2)
            {
                string tmpValue = value.ToString();
                string tmpPlanned = plannedValue.ToString();
                int PlannedNumbersAftepPoint = 2;
                if (tmpPlanned.IndexOf(',') != -1)
                {
                    PlannedNumbersAftepPoint = (tmpPlanned.Length - tmpPlanned.IndexOf(',') + 1);
                }
                int ValuePointIndex = tmpValue.IndexOf(',');
                if (ValuePointIndex != -1)
                {
                    if ((tmpValue.Length - ValuePointIndex - PlannedNumbersAftepPoint) > 0)
                    {
                        tmpValue = tmpValue.Remove(ValuePointIndex + PlannedNumbersAftepPoint, tmpValue.Length - ValuePointIndex - PlannedNumbersAftepPoint);
                    }
                }
                return tmpValue;
            }

            return "0";
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
            if (collected == null)
            {
                Value_ = 0;
            }
            else
            {
                if (collected.Value == null)
                {
                    Value_ = 0;
                }
                else
                {
                    Value_ = (float)collected.Value;
                }
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
                            select a).OrderBy(mc => mc.SortID).ToList();

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
                            select a).OrderBy(mc => mc.SortID).ToList();
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

            String IndicatorID = (string)Session["IndicatorToDetailed"];
            int indicator = Convert.ToInt32(IndicatorID);
            List<int> Columnindicators = new List<int> { 1016, 1017, 1024, 1035, 1026};

            ChartItems chartItems = new ChartItems();

            ChartValueArray DataForChart = IndicatorForAllAcademys(indicator, 1);

            // Формируем GridView
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Ratio", typeof(string)));
            dataTable.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("IndicatorValue", typeof(string)));

            #region график + чуток gridview
            // Форматируем диаграмму
            Chart1.BackColor = Color.White;
            Chart1.BackSecondaryColor = Color.White;
            Chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderlineColor = Color.Black;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматируем область диаграммы
            Chart1.ChartAreas[0].BackColor = Color.White;
            
            //  заголовок
            Chart1.Titles.Add(DataForChart.chartName);
            Chart1.Titles[0].Font = new Font("Utopia", 16);

            if (Columnindicators.Contains(indicator)) // Column для нескольки показателей из ColumnIndicators
            {
                Chart1.Series.Add(new Series("Value")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.SeaGreen
                });

                Chart1.Series.Add(new Series("Planned")
                {
                    ChartType = SeriesChartType.FastLine,
                    Color = Color.Red,
                    BorderWidth = 4,
                });


                Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                Chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                int ratio = 1;
                List<ChartOneValue> sortItems = chartItems.SortReverse(DataForChart.ChartValues);
                ViewState["Items"] = sortItems;

                var measure = (from ind in kPiDataContext.IndicatorsTable
                    where ind.IndicatorsTableID == indicator
                    select ind.Measure).FirstOrDefault().ToString();

         
               // chartItems.AddChartItem("", 0);
                ChartOneValue DataForChartKFUvalue = IndicatorsForCFUOneIndicator(indicator, 1);
                foreach (ChartOneValue item in sortItems)
                {
                    if (item.value == 0) continue;
                    chartItems.AddChartItem(item.name, item.value);
                    Chart1.Series["Planned"].Points.AddY(DataForChartKFUvalue.value);

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["IndicatorID"] = (from a in kPiDataContext.FirstLevelSubdivisionTable
                        where a.Name.Equals(item.name)
                        select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                        // Не индикаторID а FirstLevelSubdivisionTableID 
                    dataRow["Ratio"] = ratio; //Ratio
                    dataRow["IndicatorName"] = item.name;
                    dataRow["IndicatorValue"] = Math.Round(item.value, 3) + " " + measure;
                    dataTable.Rows.Add(dataRow);

                    ratio++;
                }
                Chart1.Legends.Add(new Legend("Default") {Docking = Docking.Right, Font = new Font("Arial", 11)});

                // Chart1.Legends["Default"].Font = new Font("Utopia", 16);

                // Привязать источник к диаграмме
                Chart1.DataSource = chartItems.GetDataSource();
                Chart1.Series["Value"].XValueMember = "Name";
                Chart1.Series["Value"].YValueMembers = "Value";

               // Random random = new Random();
                //foreach (var item in Chart1.Series[0].Points)
                //{
                  //  Color c = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                    //item.Color = c;
                //}

                Chart1.Series["Value"].Label = "#VALY" + " %";
                Chart1.Series["Value"].LegendText = "Значение по каждой академии";
                Chart1.Series["Planned"].LegendText = "Плановое значение по КФУ";

                Chart1.ChartAreas["ChartArea1"].AxisX = new Axis
                {
                    LabelStyle = new LabelStyle() {Font = new Font("Verdana", 6.5f)}
                };

                Chart1.ChartAreas["ChartArea1"].AxisX.LabelAutoFitMinFontSize = 5;
                //Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 30;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
                Chart1.Series["Value"].ToolTip = "#VALX" + ", \n значение = "+ "#VALY" +" "+ measure;
                Chart1.Series["Value"].Font = new Font("Arial", 9f, FontStyle.Bold);
                

             

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                Chart1.Series["Value"].PostBackValue = "#INDEX";

            }

            else if (indicator == 1036) // Костыль для V.20
            {
                Chart1.Series.Add(new Series("Default")
                {
                    ChartType = SeriesChartType.Pie,
                    Color = Color.CornflowerBlue
                });

                Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                Chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                int ratio = 1;
                List<ChartOneValue> sortItems = chartItems.SortReverse(DataForChart.ChartValues);
                ViewState["Items"] = sortItems;

                var measure = (from ind in kPiDataContext.IndicatorsTable
                               where ind.IndicatorsTableID == indicator
                               select ind.Measure).FirstOrDefault().ToString();

                foreach (ChartOneValue item in sortItems)
                {
                    if (item.value == 0) continue;

                    IndicatorsTable CurrentIndicator = (from a in kPiDataContext.IndicatorsTable
                                                        where a.IndicatorsTableID == indicator
                                                        select a).FirstOrDefault();
                    float value = item.value;
                    if (CurrentIndicator.DataType == 1)
                        value = (float)Math.Ceiling(value);

                    chartItems.AddChartItem(item.name, value);

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["IndicatorID"] = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                              where a.Name.Equals(item.name)
                                              select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                    // Не индикаторID а FirstLevelSubdivisionTableID 
                    dataRow["Ratio"] = ratio; //Ratio
                    dataRow["IndicatorName"] = item.name;
                    dataRow["IndicatorValue"] = FloatToStrFormat(item.value, item.planned, (Int32)CurrentIndicator.DataType) + " " + measure; 
                    dataTable.Rows.Add(dataRow);

                    ratio++;
                }

                Chart1.Legends.Add(new Legend("Default") { Docking = Docking.Right, Font = new Font("Arial", 11) });

                // Chart1.Legends["Default"].Font = new Font("Utopia", 16);

                // Привязать источник к диаграмме
                Chart1.DataSource = chartItems.GetDataSource();
                Chart1.Series[0].XValueMember = "Name";
                Chart1.Series[0].YValueMembers = "Value";

                Chart1.Series[0].Label = "#VALY" + " %";


                Chart1.Series[0].LegendText = "#AXISLABEL";

                Chart1.Series[0].ToolTip = "#VALX";



                Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                Chart1.Series[0].PostBackValue = "#INDEX";
            }

            else // Pie для остальных
            {

                Chart1.Series.Add(new Series("Default")
                {
                    ChartType = SeriesChartType.Pie,
                    Color = Color.CornflowerBlue
                });

                Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                Chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                int ratio = 1;
                List<ChartOneValue> sortItems = chartItems.SortReverse(DataForChart.ChartValues);
                ViewState["Items"] = sortItems;

                var measure = (from ind in kPiDataContext.IndicatorsTable
                    where ind.IndicatorsTableID == indicator
                    select ind.Measure).FirstOrDefault().ToString();

                foreach (ChartOneValue item in sortItems)
                {
                    if (item.value == 0) continue;
                    IndicatorsTable CurrentIndicator = (from a in kPiDataContext.IndicatorsTable
                                                        where a.IndicatorsTableID == indicator
                                                        select a).FirstOrDefault();
                    float value = item.value;
                    if (CurrentIndicator.DataType == 1)
                        value = (float)Math.Ceiling(value);

                    chartItems.AddChartItem(item.name, value);

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["IndicatorID"] = (from a in kPiDataContext.FirstLevelSubdivisionTable
                        where a.Name.Equals(item.name)
                        select a.FirstLevelSubdivisionTableID).FirstOrDefault();
                        // Не индикаторID а FirstLevelSubdivisionTableID 
                    dataRow["Ratio"] = ratio; //Ratio
                    dataRow["IndicatorName"] = item.name;
                    dataRow["IndicatorValue"] = FloatToStrFormat(item.value, item.planned, (Int32)CurrentIndicator.DataType) + " " + measure; 
                    dataTable.Rows.Add(dataRow);

                    ratio++;
                }

                Chart1.Legends.Add(new Legend("Default") {Docking = Docking.Right, Font = new Font("Arial", 11)});

                // Chart1.Legends["Default"].Font = new Font("Utopia", 16);

                // Привязать источник к диаграмме
                Chart1.DataSource = chartItems.GetDataSource();
                Chart1.Series[0].XValueMember = "Name";
                Chart1.Series[0].YValueMembers = "Value";

                Chart1.Series[0].Label = "#PERCENT{P0}";


                Chart1.Series[0].LegendText = "#AXISLABEL";

                Chart1.Series[0].ToolTip = "#VALX";

             

                Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                Chart1.Series[0].PostBackValue = "#INDEX";
            }
            #endregion
        }

        protected void FacultyButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var par = button.CommandArgument.ToString();
                Session["AcademyToDetailed"] = par;
                Response.Redirect("~/Rector/RShowChartFaculty.aspx");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                List<int> ExceptItems = new List<int>
                {
                    1022,
                    1023,
                    1025,
                    1012,
                    1026,
                    1013,
                    1027,
                    1020,
                    1018,
                    1017,
                    1034,
                    1021,
                    1015,
                    1028,
                    1029,
                    1030,
                    1019,
                    1033,
                    1031,
                    1032
                }; // ID Академий с фейками на уровне кафедр

                string indID = DataBinder.Eval(e.Row.DataItem, "IndicatorID").ToString();
                if (ExceptItems.Contains(Convert.ToInt32(indID)))
                {
                    Button button = (Button) e.Row.FindControl("Button7");
                    if (button != null)
                        button.Enabled = false;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                List<int> ExceptItems = new List<int>
                {
                    1022,
                    1023,
                    1025,
                    1012,
                    1026,
                    1013,
                    1027,
                    1020,
                    1018,
                    1017,
                    1034,
                    1021,
                    1015,
                    1028,
                    1029,
                    1030,
                    1019,
                    1033,
                    1031,
                    1032
                }; // ID Академий с фейками на уровне кафедр

                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<ChartOneValue> item = (List<ChartOneValue>) ViewState["Items"];

                int id = (from a in kPiDataContext.FirstLevelSubdivisionTable
                    where a.Name.Equals(item[Convert.ToInt32(e.PostBackValue)].name)
                    select a.FirstLevelSubdivisionTableID).FirstOrDefault();

                if (!ExceptItems.Contains(Convert.ToInt32(id)))
                {
                    Session["AcademyToDetailed"] = id.ToString();
                    Response.Redirect("~/Rector/RShowChartFaculty.aspx");
                }
                else
                {
                    DisplayAlert("Невозможно детализировать");
                }
            }
            catch (Exception)
            {
                // error
            }

        }
        private void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
    }


}
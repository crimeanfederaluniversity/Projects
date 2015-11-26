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





            int reportId = 0;
            if (!Page.IsPostBack)
            {

                RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"];
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




            ChartItems chartItems = new ChartItems();
            ForRCalc forRCalc = new ForRCalc();
            ChartValueArray DataForChart = forRCalc.IndicatorForAllAcademys(indicator, reportId);

           

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

                ChartOneValue DataForChartKFUvalue = forRCalc.IndicatorsForCFUOneIndicator(indicator, reportId);
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
                    dataRow["IndicatorValue"] = forRCalc.FloatToStrFormat(item.value, item.planned, (Int32)CurrentIndicator.DataType) + " " + measure; 
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
                    dataRow["IndicatorValue"] = forRCalc.FloatToStrFormat(item.value, item.planned, (Int32)CurrentIndicator.DataType) + " " + measure; 
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


            if (GridView1.Rows.Count < 1)
            {
                noDataMessage.Visible = true;
                Chart1.Visible = false;
            }
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

        protected void RectorChooseReportDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"];
            if (RectorChart == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            RectorChart.reportId = Convert.ToInt32(RectorChooseReportDropDown.SelectedValue);
            Session["RectorChart"] = RectorChart;
            Response.Redirect("~/Rector/RShowChartDetailed.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RShowChart.aspx");
        }
    }


}
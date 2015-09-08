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
            if (collected != null) //  Крашило тут мед академию
            if (collected.Value == null)
            {
                Value_ = 0;
            }
            else
            {
                Value_ = (float)collected.Value;
            }
            else
            {
                // error
            }
            #endregion
            ChartOneValue DataRowForChart = new ChartOneValue(Name_, Value_, Planned_Value);
            return DataRowForChart;
        }

        public ChartValueArray IndicatorsForAcademyFacultys(int IndicatorID, int AcademyID,  int ReportID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                         where a.IndicatorsTableID == IndicatorID
                                         select a).FirstOrDefault();

            List<SecondLevelSubdivisionTable> FacultyList = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                             where a.Active == true && a.FK_FirstLevelSubdivisionTable == AcademyID
                                                             select a).ToList();

            ChartValueArray DataForChart = new ChartValueArray("Целевой показатель '" + Indicator.Name + "' в разрезе "+ (from b in kpiWebDataContext.FirstLevelSubdivisionTable where b.FirstLevelSubdivisionTableID == AcademyID select b.Name).FirstOrDefault());

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

            String AcademyID = (string)Session["AcademyToDetailed"];
            int academy = Convert.ToInt32(AcademyID);

            String IndicatorID = (string)Session["IndicatorToDetailed"];
            int indicator = Convert.ToInt32(IndicatorID);

            ChartItems chartItems = new ChartItems();

            ChartValueArray DataForChart = IndicatorsForAcademyFacultys(indicator,academy, 1);

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

                dataRow["IndicatorValue"] = FloatToStrFormat(item.value, item.planned, (Int32)CurrentIndicator.DataType) + " " + measure; //////////


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
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
    }
}
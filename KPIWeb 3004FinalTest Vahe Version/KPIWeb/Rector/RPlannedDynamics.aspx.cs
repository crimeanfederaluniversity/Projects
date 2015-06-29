using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RPlannedDynamics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /////
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<IndicatorsTable> Indicators = (from a in kpiWebDataContext.IndicatorsTable
                                         where a.Active == true
                                         select a).ToList();
            //!=null
            /////////////

            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Выберите целевой показатель");

            foreach (var item in Indicators)
                dictionary.Add(item.IndicatorsTableID, item.Name);



            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();
            ////////////
            
            ChartDraw(Indicators.FirstOrDefault().IndicatorsTableID);
        }

        private void ChartDraw(int IndicatorID)
        {
            ChartValueWithAllPlanned NewChartValue = ForRCalc.GetAllPlannedForIndicator(IndicatorID);

           // Форматировать диаграмму
            Chart1.BackColor = Color.Gray;
            Chart1.BackSecondaryColor = Color.WhiteSmoke;
            Chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderlineColor = Color.Gray;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматировать область диаграммы
            Chart1.ChartAreas[0].BackColor = Color.Wheat;

            // Добавить и форматировать заголовок
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            Chart1.Titles.Add((from a in kpiWebDataContext.IndicatorsTable
                              where a.Active == true && a.IndicatorsTableID == IndicatorID
                              select a.Name).FirstOrDefault());

            Chart1.Titles[0].Font = new Font("Utopia", 16);

            ChartItems chartItems = new ChartItems();

            Chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            Chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

            foreach (var item in NewChartValue.PlannedAndRealValuesList)
            {
                chartItems.AddChartItem(item.Date.Year.ToString(), item.RealValue);
                Chart1.Series[1].Points.AddY(item.PlannedValue);
            }
            Chart1.DataSource = chartItems.GetDataSource();
            Chart1.Series[0].XValueMember = "Name";
            Chart1.Series[0].YValueMembers = "Value";


            // Chart1.Series[0].Points.DataBindY(
            // new int[] { 5, 3, 12, 14, 11, 7, 3, 5, 9, 12, 11, 10 });

            // Chart1.Series[1].Points.DataBindY(
            // new int[] { 3, 7, 13, 2, 7, 15, 23, 20, 1, 5, 7, 6 });
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartDraw(DropDownList1.SelectedIndex);
            Chart1.DataBind();
        }
    }
}
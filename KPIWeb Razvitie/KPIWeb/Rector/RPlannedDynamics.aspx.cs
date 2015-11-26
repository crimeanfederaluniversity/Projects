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

            if (!IsPostBack)
            {
                /////
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                List<IndicatorsTable> Indicators = (from a in kpiWebDataContext.IndicatorsTable
                    where a.Active == true
                                                    select a).OrderBy(mc => mc.SortID).ToList();
                //!=null
                /////////////

                var dictionary = new Dictionary<int, string>();
                //dictionary.Add(0, "Выберите целевой показатель");

                foreach (var item in Indicators)
                    dictionary.Add(item.IndicatorsTableID, item.Name);



                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
                ////////////

                ChartDraw(Indicators.FirstOrDefault().IndicatorsTableID);
            }
        }

        private void ChartDraw(int IndicatorID)
        {
            ChartValueWithAllPlanned NewChartValue = ForRCalc.GetAllPlannedForIndicator(IndicatorID);

           // Формат диаграммы
            Chart1.BackColor = Color.Gray;
            Chart1.BackSecondaryColor = Color.WhiteSmoke;
            Chart1.BackGradientStyle = GradientStyle.DiagonalRight;

            Chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            Chart1.BorderlineColor = Color.Gray;
            Chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Формат области диаграммы
            Chart1.ChartAreas[0].BackColor = Color.Gainsboro;
            

            // формат заголовока
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            var namemeasure = (from a in kpiWebDataContext.IndicatorsTable
                where a.Active == true && a.IndicatorsTableID == IndicatorID
                select a).FirstOrDefault();
            Chart1.Titles.Add(namemeasure.Name + " (" + namemeasure.Measure+")");

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
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           ChartDraw(Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)); 
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
    }
}
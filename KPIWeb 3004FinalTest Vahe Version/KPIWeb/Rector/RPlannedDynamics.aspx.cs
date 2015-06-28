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
            Chart1.Titles.Add("1234567890");
            Chart1.Titles[0].Font = new Font("Utopia", 16);

            Chart1.Series[0].Points.DataBindY(
            new int[] { 5, 3, 12, 14, 11, 7, 3, 5, 9, 12, 11, 10 });

            Chart1.Series[1].Points.DataBindY(
            new int[] { 3, 7, 13, 2, 7, 15, 23, 20, 1, 5, 7, 6 });

        }
    }
}
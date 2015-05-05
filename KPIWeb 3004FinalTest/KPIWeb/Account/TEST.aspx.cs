using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Account
{
    public partial class TEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void abbreviatureStringToPolishString(string abbreviaturetring)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = Polish.Calculate(TextBox1.Text).ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label1.Text = CalculateAbb.Calculate(TextBox1.Text).ToString();
        }
    }
}
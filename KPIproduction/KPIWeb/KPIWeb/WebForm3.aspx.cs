using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ValidateDayOff2.Text = "13213213213";
            ValidateDayOff2.MaximumValue = "1000";


        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Summary.ShowMessageBox = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string pattern = @"\b(\.+\d+\.)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match("31.02.12");

            char[] charsToTrim = { '.', ' ', '\'' };

            string result = match.Groups[1].Value.Trim(charsToTrim);
            TextBox1.Text = result;
        }
    }
}
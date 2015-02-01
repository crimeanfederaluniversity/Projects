using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Form_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "Kafedra" && TextBox2.Text == "kaf")
                Response.Redirect("Form_kafedra.aspx");
            else
            {
                TextBox1.Text = "Ошибка";
                TextBox2.Text = "Ошибка";
            }
        }
    }
}
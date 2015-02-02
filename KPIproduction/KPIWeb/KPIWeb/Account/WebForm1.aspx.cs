using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication3;

namespace KPIWeb.Account
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           Users user = (Users)Session["sessionKPI"];
            Label1.Text = "Логин " + user.login.ToString();
            Label2.Text = "Пароль" + user.password.ToString();
            Label3.Text = "Актив " + user.active.ToString();
            Label4.Text = "ID " + user.id_users.ToString();
            Label5.Text = "fk_fifth_stage " + user.fk_fifth_stage.ToString();
            Label6.Text = "fk_second_stage" + user.fk_second_stage.ToString();
            Label7.Text = "fk_third_stage" + user.fk_third_stage.ToString();


        }
    }
}
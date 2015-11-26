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
            UsersTable user = (UsersTable)Session["user"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Label1.Text = "Логин " + user.Login;
                Label2.Text = "Пароль" + user.Password;
                Label3.Text = "Актив " + user.Active;
                Label4.Text = "ID " + user.UsersTableID.ToString();
                Label5.Text = "FK_FirstLevelSubdivisionTable " + user.FK_FirstLevelSubdivisionTable.ToString();
                Label6.Text = "FK_SecondLevelSubdivisionTable " + user.FK_SecondLevelSubdivisionTable.ToString();
                Label7.Text = "FK_ThirdLevelSubdivisionTable " + user.FK_ThirdLevelSubdivisionTable.ToString();
            }

        }
    }
}
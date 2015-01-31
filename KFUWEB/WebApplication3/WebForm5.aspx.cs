using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm5 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<Users> listUsers = DataBaseCommunicator.GetUsersTable("petya", "123");


            Label1.Text = " ";
            foreach (Users user in listUsers)
            {
              
                Label1.Text +=  "Логин " + user.login + "  Пароль " + user.password;
            }
           




        }
    }
}
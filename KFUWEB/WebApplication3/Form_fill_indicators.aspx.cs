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
    public partial class Form_fill_indicators : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

          
         
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Button1.Text = e.ToString();
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            Button1.Text = e.ToString();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Button1.Text = e.ToString();
        }

        protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            Button1.Text = "123";
        }
    }
}
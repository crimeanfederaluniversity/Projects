using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class EditAllProjects : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
                Response.Redirect("~/Default.aspx");

            List<Projects> allprojects = (from a in zakupkaDB.Projects where a.active == true select a).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            foreach (Projects value in allprojects)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = value.projectID;
                dataRow["Name"] = value.name;
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void EditButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["pridforchange"] = button.CommandArgument;
                Response.Redirect("~/Event/CreateProjectPage.aspx");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Zakupka.Event
{
    public partial class ProjectPage : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
                Refresh();
        }
             protected void Refresh()
        {
            int eventID = (int)Session["eventID"];
            Events name = (from a in zakupkaDB.Events where a.eventID == eventID select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("projectID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("projectName", typeof(string)));

            List<Projects> projectList = (from a in zakupkaDB.Projects where a.active == true && a.fk_event == eventID select a).ToList();
            foreach (Projects currentProject in projectList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["projectID"] = currentProject.projectID;
                dataRow["projectName"] = currentProject.name;
                dataTable.Rows.Add(dataRow);
            }

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void GoButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["projectID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/ContractPage.aspx");
            }

        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {
            int eventID = (int)Session["eventID"];
            Projects newproject = new Projects();
            newproject.active = true;
            newproject.name = TextBox1.Text;
            newproject.fk_event = eventID;
            zakupkaDB.Projects.InsertOnSubmit(newproject);
            zakupkaDB.SubmitChanges();
            Refresh();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/EventPage.aspx");
        }
    }
}
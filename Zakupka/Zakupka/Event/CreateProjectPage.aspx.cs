using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class CreateProjectPage : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
                Response.Redirect("~/Default.aspx");

            if (!Page.IsPostBack)
            {
                if (Session["userID"] != null)
                
                    GridApdate();
            }
        }
        protected void GridApdate()
        {
            int id = Convert.ToInt32(Session["pridforchange"]);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Access", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            List<Events> groups =  (from a in zakupkaDB.Events
                 where a.active == true
                 select a).Distinct().ToList();
            foreach (var grouped in groups)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = grouped.eventID;
                dataRow["Name"] = grouped.name;
              
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {
            Projects newEvent = new Projects();
            newEvent.active = true;
            newEvent.name = TextBox1.Text;
            zakupkaDB.Projects.InsertOnSubmit(newEvent);
            zakupkaDB.SubmitChanges();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox confirmed = (CheckBox)GridView1.Rows[i].FindControl("Access");
                Label label = (Label)GridView1.Rows[i].FindControl("ID");
                if (confirmed.Checked == true)
                { 
                        ProjectEventMappingTable newlink = new ProjectEventMappingTable();
                        newlink.active = true;
                        newlink.fk_project = newEvent.projectID;
                        newlink.fk_event = Convert.ToInt32(label.Text);
                        zakupkaDB.ProjectEventMappingTable.InsertOnSubmit(newlink);
                        zakupkaDB.SubmitChanges();
                    
                }

            }
            Response.Redirect("~/Event/MainEventPage.aspx");
        }
    }
}
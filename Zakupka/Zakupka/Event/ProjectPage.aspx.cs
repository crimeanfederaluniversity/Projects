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
            if (Session["userID"] == null)
                Response.Redirect("~/Default.aspx");

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

            List<Projects> projectList = (from a in zakupkaDB.Projects where a.active == true  
                                          join b in zakupkaDB.ProjectEventMappingTable on a.projectID equals b.fk_project
                                          where b.fk_event == eventID && b.active == true select a).ToList();
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
                int eventID = (int)Session["eventID"];
                Session["eventId"] = eventID;
                Session["projectID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/ContractPage.aspx");
            }

        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int eventID = (int)Session["eventId"];              
                List<ProjectEventMappingTable> deletelink = (from a in zakupkaDB.ProjectEventMappingTable where a.active == true && a.fk_project == Convert.ToInt32(button.CommandArgument) select a).ToList();
                if (deletelink.Count == 1)
                {
                    Projects deletecontract = (from a in zakupkaDB.Projects where a.active == true && a.projectID == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();
                    deletecontract.active = false;
                    zakupkaDB.SubmitChanges();
                    List<ProjectsValues> values = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_project == Convert.ToInt32(button.CommandArgument) select a).ToList();
                    foreach (ProjectsValues n in values)
                    {
                        n.active = false;
                        zakupkaDB.SubmitChanges();
                    }
                    foreach (ProjectEventMappingTable n in deletelink)
                    {
                        n.active = false;
                        zakupkaDB.SubmitChanges();
                    }
                }
                else
                {
                    foreach (ProjectEventMappingTable n in deletelink)
                    {
                        if (n.fk_event == eventID)
                        {
                            n.active = false;
                            zakupkaDB.SubmitChanges();
                            List<ProjectsValues> values = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_project == n.fk_project && a.fk_event == eventID select a).ToList();
                            foreach (ProjectsValues tmp in values)
                            {
                                tmp.active = false;
                                zakupkaDB.SubmitChanges();
                            }
                        }
                    }
                }
                int projectID = Convert.ToInt32(button.CommandArgument);
                Calculations calc = new Calculations();
                calc.CalculateMIyProject(projectID, eventID);
                Refresh();
            }

        }
        protected void EditButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                
                Session["projectID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/ProjectEdit.aspx");
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/EventPage.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Zakupka.Event
{
    public partial class EventPage : System.Web.UI.Page
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
            int mainid = Convert.ToInt32(Session["maineventID"]);
            string name = (from a in zakupkaDB.MainEvent where a.Active == true && a.ID == mainid select a.MainEvent1).FirstOrDefault();
            Label1.Text = name.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("eventID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("eventName", typeof(string)));
            
            List<Events> eventList = (from a in zakupkaDB.Events where a.active == true && a.fk_mainevent == mainid select a).ToList();
            foreach (Events currentEvent in eventList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["eventID"] = currentEvent.eventID;
                dataRow["eventName"] = currentEvent.name;
                dataTable.Rows.Add(dataRow);
            }
            
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void GoButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["eventID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/ProjectPage.aspx");
            }

        }
    

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/MainEventPage.aspx");
        }
    }
}
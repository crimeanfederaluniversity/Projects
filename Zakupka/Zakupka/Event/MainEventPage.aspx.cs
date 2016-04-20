using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class MainEventPage : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
                // Refresh();

                RenderGrid(FillData());

        }
        protected void Refresh()
        {
            /* DataTable dataTable = new DataTable();
             dataTable.Columns.Add(new DataColumn("maineventID", typeof(string)));
             dataTable.Columns.Add(new DataColumn("maineventName", typeof(string)));

             List<MainEvent> eventList = (from a in zakupkaDB.MainEvent where a.Active == true select a).ToList();
             foreach (MainEvent currentEvent in eventList)
             {
                 DataRow dataRow = dataTable.NewRow();
                 dataRow["maineventID"] = currentEvent.ID;
                 dataRow["maineventName"] = currentEvent.MainEvent1;
                 dataTable.Rows.Add(dataRow);
             }

             GridView1.DataSource = dataTable;
             GridView1.DataBind();*/
 
        }


        private List<MainEvent> FillData()

        {
            return (from a in zakupkaDB.MainEvent where a.Active == true select a).ToList();
        }

        private void RenderGrid (List<MainEvent> data)
        {
            GridView1.DataSource = data;

            BoundField boundField = new BoundField();
            boundField.DataField = "ID";
            boundField.HeaderText = "Id";
            boundField.Visible = true;
            GridView1.Columns.Add(boundField);

            BoundField boundField2 = new BoundField();
            boundField2.DataField = "MainEvent1";
            boundField2.HeaderText = "Название";
            boundField2.Visible = true;
            GridView1.Columns.Add(boundField2);

            ButtonField coluButtonField2 = new ButtonField();
            coluButtonField2.Text = "Перейти";
            coluButtonField2.ButtonType = ButtonType.Button;
            coluButtonField2.CommandName = "Rema";
            coluButtonField2.ControlStyle.CssClass = "btn btn-default";
            GridView1.Columns.Add(coluButtonField2);

            DataBind();
        }


  

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/View1.aspx");
        }

        protected void CreateContract_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/CreateContractPage.aspx");
        }

        protected void CreateProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/CreateProjectPage.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int idProcess = Convert.ToInt32( GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text );

            switch (e.CommandName)

            {

                case "Rema":
                    {
                        Session["maineventID"] = idProcess;
                        Response.Redirect("~/Event/EventPage.aspx");
                    }
                    break;
            }
            }

        protected void EditProjects_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/EditAllProjects.aspx");
        }

        protected void EditContracts_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/EditAllContracts.aspx");
        }
    }
}
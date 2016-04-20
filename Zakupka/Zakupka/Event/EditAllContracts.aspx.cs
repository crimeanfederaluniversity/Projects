using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class EditAllContracts : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Contracts> allcontracts = (from a in zakupkaDB.Contracts where a.active == true select a).ToList();
      
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            foreach (Contracts value in allcontracts)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = value.contractID;
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
                Session["conidforchange"] = button.CommandArgument;
                Response.Redirect("~/Event/CreateContractPage.aspx");
            }
        }
            
    }
}
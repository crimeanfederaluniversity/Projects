using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class CreateContractPage : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["userID"] == null)
                    Response.Redirect("~/Default.aspx");

                GridApdate();
            }
        }
        protected void GridApdate()
        {
            int id = Convert.ToInt32(Session["conidforchange"]);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Access", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));

            List<Projects> groups = (from a in zakupkaDB.Projects  where a.active == true select a).Distinct().ToList();
            foreach (var grouped in groups)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = grouped.projectID;
                dataRow["Name"] = grouped.name;
                           
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {        
            Contracts newcontract = new Contracts();
            newcontract.active = true;
            newcontract.name = TextBox1.Text;
            zakupkaDB.Contracts.InsertOnSubmit(newcontract);
            zakupkaDB.SubmitChanges();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox confirmed = (CheckBox)GridView1.Rows[i].FindControl("Access");
                Label label = (Label)GridView1.Rows[i].FindControl("ID");

                if (confirmed.Checked == true)
                { 
                        ContractProjectMappingTable newlink = new ContractProjectMappingTable();
                        newlink.Active = true;
                        newlink.fk_contract = newcontract.contractID;
                        newlink.fk_project = Convert.ToInt32(label.Text);
                        zakupkaDB.ContractProjectMappingTable.InsertOnSubmit(newlink);
                        zakupkaDB.SubmitChanges();
                                                                         
                }

            }
            Response.Redirect("~/Event/MainEventPage.aspx");
        }
    }
}
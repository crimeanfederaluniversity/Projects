using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Zakupka.Event
{
    public partial class ContractPage : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            int projectID = (int)Session["projectID"];
            Contracts name = (from a in zakupkaDB.Contracts where a.fk_project == projectID select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("contractID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("contractName", typeof(string)));

            List<Contracts> contractList = (from a in zakupkaDB.Contracts where a.active == true && a.fk_project == projectID select a).ToList();
            foreach (Contracts currentContract in contractList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["contractID"] = currentContract.contractID;
                dataRow["contractName"] = currentContract.name;
                dataTable.Rows.Add(dataRow);
            }

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void EditButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["step"] = 1;
                Session["contractID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/CreateEditContract.aspx");
            }
        }
        protected void EditButton2Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["step"] = 2;
                Session["contractID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/CreateEditContract.aspx");
            }
        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {
            int projectID = (int)Session["projectID"];
            Contracts newcontract = new Contracts();
            newcontract.active = true;
            newcontract.name = TextBox1.Text;
            newcontract.fk_project = projectID;
            zakupkaDB.Contracts.InsertOnSubmit(newcontract);
            zakupkaDB.SubmitChanges();

        }
    }
}
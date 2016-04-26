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
            if (Session["userID"] == null)
                Response.Redirect("~/Default.aspx");
            Refresh();
        }
            protected void Refresh()
        {
            int projectID = (int)Session["projectID"];
            Projects name = (from a in zakupkaDB.Projects where a.projectID == projectID select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("contractID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("contractName", typeof(string)));

            List<Contracts> contractList = (from a in zakupkaDB.Contracts
                                            where a.active == true
                                            join b in zakupkaDB.ContractProjectMappingTable on a.contractID equals b.fk_contract
                                            where b.fk_project == projectID && b.Active == true
                                            select a).ToList();
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
                
                Session["contractID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Event/CreateEditContract.aspx");
            }
        }
      
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int eventID = (int)Session["eventId"];
                int projectID = (int)Session["projectID"];              
                List<ContractProjectMappingTable> deletelink = (from a in zakupkaDB.ContractProjectMappingTable where a.Active == true && a.fk_contract == Convert.ToInt32(button.CommandArgument) select a).ToList();
                if (deletelink.Count == 1)
                {
                    Contracts deletecontract = (from a in zakupkaDB.Contracts where a.active == true && a.contractID == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();
                    deletecontract.active = false;
                    zakupkaDB.SubmitChanges();
                    List<CollectedValues> values = (from a in zakupkaDB.CollectedValues where a.active == true && a.fk_contract == Convert.ToInt32(button.CommandArgument)   select a).ToList();
                    foreach (CollectedValues n in values)
                    {
                        n.active = false;
                        zakupkaDB.SubmitChanges();
                    }
                    foreach (ContractProjectMappingTable n in deletelink)
                    {
                        n.Active = false;
                        zakupkaDB.SubmitChanges();
                    }
                }
                else
                {
                    foreach (ContractProjectMappingTable n in deletelink)
                    {
                        if(n.fk_project == projectID)
                        {
                            n.Active = false;
                            zakupkaDB.SubmitChanges();
                            List<CollectedValues> values = (from a in zakupkaDB.CollectedValues where a.active == true && a.fk_contract == n.fk_contract && a.fk_project == projectID  select a).ToList();
                            foreach (CollectedValues tmp in values)
                            {
                                tmp.active = false;
                                zakupkaDB.SubmitChanges();
                            }
                        }                      
                    }
                }
                
                Calculations calc = new Calculations();
                calc.CalculateMIyProject(projectID, eventID);
                Refresh();
            }
        }
      

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/ProjectPage.aspx");
        }
    }
}
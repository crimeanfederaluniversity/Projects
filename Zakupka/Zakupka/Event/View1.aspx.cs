using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class View1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
 
            ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
            Table tableToReturn = new Table();          
            List<Fields> allfields = (from f in zakupkaDB.Fields
                                      where f.active == true
                                      join z in zakupkaDB.ViewField
                                      on f.filedID equals z.fk_field
                                      where z.viewtype == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value) && z.active == true
                                      select f).ToList();
                                                                                                                                                                                                                                                                          
            TableRow newNameRow= new TableRow();
                newNameRow.Cells.Add(new TableCell());
            foreach (Fields field in allfields)
            {

                TableCell newField = new TableCell();
                TableCell newFieldId = new TableCell();

                newField.Text = field.name;
         
                newNameRow.Cells.Add(newField);
                tableToReturn.Rows.Add(newNameRow);

            }
            List<Events> allevents = (from a in zakupkaDB.Events
                                      where a.active
                                      select a).ToList();
                foreach (Events n in allevents)
                {
                    TableRow newRow = new TableRow();
                    TableCell newCell = new TableCell();
                    newCell.Text = n.name;
                    newCell.Style.Add("background-color", "red");
                    newRow.Cells.Add(newCell);

                    tableToReturn.Rows.Add(newRow);
                    List<Projects> allprojects = (from a in zakupkaDB.Projects
                                                  where a.active && a.fk_event == n.eventID
                                                  select a).ToList();
                    foreach (Projects a in allprojects)
                    {
                        TableRow newRow1 = new TableRow();
                        TableCell newCell1 = new TableCell();
                        newCell1.Text = a.name;
                        newCell1.Style.Add("background-color", "yellow");
                        newRow1.Cells.Add(newCell1);
                        tableToReturn.Rows.Add(newRow1);
                        List<Contracts> allcontracts = (from b in zakupkaDB.Contracts
                                                        where b.active  && b.fk_project == a.projectID
                                                        select b).ToList();
                        foreach (Contracts b in allcontracts)
                        {
                            TableRow newRow2 = new TableRow();
                            TableCell newCell2 = new TableCell();
                            newCell2.Text = b.name;
                            newRow2.Cells.Add(newCell2);

                            foreach (Fields field in allfields)
                            {
                                TableCell newValue = new TableCell();

                                newValue.Text = (from c in zakupkaDB.CollectedValues
                                             where c.active && c.fk_contract == b.contractID && c.fk_field == field.filedID
                                             select c.value).FirstOrDefault();
                                newRow2.Cells.Add(newValue);
                               tableToReturn.Rows.Add(newRow2);
                            }
                       // tableToReturn.Rows.Add(newRow2);
                      }
                    }
                              
            }
            TableDiv.Controls.Add(tableToReturn);
        }
    }
}
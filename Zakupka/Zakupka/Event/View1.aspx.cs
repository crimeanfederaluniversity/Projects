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
                                      join z in zakupkaDB.ViewField.OrderBy(mc=>mc.@orderby)
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
                    TableRow eventRow = new TableRow();
                    TableCell eventCell = new TableCell();
                eventCell.Text = n.name;
                eventCell.Style.Add("background-color", "red");
                    eventRow.Cells.Add(eventCell);

               
                foreach(Fields field in allfields)
                {
                    TableCell sumValue = new TableCell();
                    if (field.sumby == true)
                    {
                        List<string> strList = (from c in zakupkaDB.CollectedValues
                                                where c.active == true
                                                && c.fk_field == field.filedID
                                                join k in zakupkaDB.Contracts on c.fk_contract equals k.contractID
                                                where k.active == true
                                                join d in zakupkaDB.Projects
                                                on k.fk_project equals d.projectID
                                                where d.fk_event == n.eventID
                                                    select c.value).ToList();

                        decimal eventsum=0;
                        foreach (var itm  in strList)
                        {
                            Decimal tmp=0;
                            Decimal.TryParse(itm, out tmp);
                            eventsum += tmp;
                        }
                        sumValue.Text = eventsum.ToString();
                    }            

                   
                    eventRow.Cells.Add(sumValue);
                }
                
                   tableToReturn.Rows.Add(eventRow);

                List<Projects> allprojects = (from a in zakupkaDB.Projects
                                                  where a.active && a.fk_event == n.eventID
                                                  select a).ToList();
                    foreach (Projects a in allprojects)
                    {
                        TableRow projectRow = new TableRow();
                        TableCell projectCell = new TableCell();
                    projectCell.Text = a.name;
                    projectCell.Style.Add("background-color", "yellow");
                    projectRow.Cells.Add(projectCell);
                    foreach (Fields field in allfields)
                    {
                        TableCell sumForPrValue = new TableCell();
                        if (field.sumby == true)
                        {
                            List<string> strList =  (from c in zakupkaDB.CollectedValues
                             where c.active == true
                             && c.fk_field == field.filedID
                             join k in zakupkaDB.Contracts on c.fk_contract equals k.contractID
                             where k.active == true 
                             && k.fk_project == a.projectID
                             select c.value).Distinct().ToList();

                            decimal prsum = 0;
                            foreach (var itm in strList)
                            {
                                Decimal tmp = 0;
                                Decimal.TryParse(itm, out tmp);
                                prsum += tmp;
                            }
                            sumForPrValue.Text = prsum.ToString();                       
                        }
                        projectRow.Cells.Add(sumForPrValue);
                    }
                    tableToReturn.Rows.Add(projectRow);

                    List<Contracts> allcontracts = (from b in zakupkaDB.Contracts
                                                        where b.active  && b.fk_project == a.projectID
                                                        select b).ToList();
                        foreach (Contracts b in allcontracts)
                        {
                            TableRow contractRow = new TableRow();
                            TableCell contractCell = new TableCell();
                        contractCell.Text = b.name;
                        contractRow.Cells.Add(contractCell);

                            foreach (Fields field in allfields)
                            {
                                TableCell newValue = new TableCell();

                                newValue.Text = (from c in zakupkaDB.CollectedValues
                                             where c.active && c.fk_contract == b.contractID && c.fk_field == field.filedID
                                             select c.value).FirstOrDefault();
                            contractRow.Cells.Add(newValue);
                               tableToReturn.Rows.Add(contractRow);
                            }
                       // tableToReturn.Rows.Add(newRow2);
                      }
                    }
                              
            }
            TableDiv.Controls.Add(tableToReturn);
        }
    }
}
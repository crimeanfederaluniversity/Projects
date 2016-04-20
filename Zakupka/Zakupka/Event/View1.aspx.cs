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

            //найдем все основные мероприятия
            List<MainEvent>  allMainEvents = (from a in zakupkaDB.MainEvent where a.Active == true  select a).ToList();
    
            //пройдемся по основным мероприятиям
            foreach (MainEvent n in allMainEvents)
            {
                TableRow maineventRow = new TableRow();
                maineventRow.Cells.Add(new TableCell() { Text = n.MainEvent1, BackColor = System.Drawing.Color.Red });
                foreach (Fields field in allfields)
                {
                    TableCell sumValue = new TableCell();
                    #region вывод заначений для мероприятия
                    //некоторые колонки суммируются
                    if (field.sumby == true)
                    {
                        List<CollectedValues> colList = (from a in zakupkaDB.Events
                                                         where a.fk_mainevent == n.ID
                                                         && a.active == true
                                                         join b in zakupkaDB.CollectedValues
                                                         on a.eventID equals b.fk_event
                                                         where b.active == true
                                                         && b.fk_field == field.filedID
                                                         select b).Distinct().ToList();
                        List<String> strList = (from a in colList select a.value).ToList();
                        decimal eventsum = 0;
                        foreach (var itm in strList)
                        {
                            Decimal tmp = 0;
                            Decimal.TryParse(itm, out tmp);
                            eventsum += tmp;
                        }
                        sumValue.Text = eventsum.ToString();
                    }
                    maineventRow.Cells.Add(sumValue);
                }
                tableToReturn.Rows.Add(maineventRow);
                #endregion
                List<Events> allevents = (from k in zakupkaDB.Events
                                          where k.active && k.fk_mainevent == n.ID
                                          select k).Distinct().ToList();
                //пройдемся по всем подмероприятиям       
                foreach (Events o in allevents)
                {
                    TableRow eventRow = new TableRow();
                    eventRow.Cells.Add(new TableCell() { Text = o.name, BackColor = System.Drawing.Color.Green });
                    foreach (Fields field in allfields)
                    {
                        TableCell sumForPmValue = new TableCell();
                        #region вывод заначений для подмероприятий
                        if (field.sumby == true)
                        {
                            List<CollectedValues> colList = (from w in zakupkaDB.CollectedValues
                                                             where w.fk_event == o.eventID &&
                                                             w.active == true && w.fk_field == field.filedID
                                                             select w).Distinct().ToList();

                            List<String> strList = (from p in colList select p.value).ToList();
                            decimal pmsum = 0;
                            foreach (var itm in strList)
                            {
                                Decimal tmp = 0;
                                Decimal.TryParse(itm, out tmp);
                                pmsum += tmp;
                            }
                            sumForPmValue.Text = pmsum.ToString();
                        }
                        eventRow.Cells.Add(sumForPmValue);
                    }
                    tableToReturn.Rows.Add(eventRow);
                    #endregion
                    List<Projects> allprojects = (from a in zakupkaDB.Projects
                                                  where a.active
                                                  join b in zakupkaDB.ProjectEventMappingTable
                                                  on a.projectID equals b.fk_project
                                                  where b.active
                                                  join c in zakupkaDB.Events
                                                  on b.fk_event equals c.eventID
                                                  where c.active == true
                                                  where c.fk_mainevent == n.ID
                                                  select a).Distinct().ToList();
                    //пройдемся по всем проектам        
                    foreach (Projects a in allprojects)
                    {
                        TableRow projectRow = new TableRow();
                        projectRow.Cells.Add(new TableCell() { Text = a.name, BackColor = System.Drawing.Color.Yellow });
                        foreach (Fields field in allfields)
                        {
                            TableCell sumForPrValue = new TableCell();
                            #region вывод заначений для проектов
                            if (field.sumby == true)
                            {
                                List<CollectedValues> colList = (from p in zakupkaDB.Projects
                                                                 where p.projectID == a.projectID
                                                                 && a.active == true
                                                                 join b in zakupkaDB.CollectedValues
                                                                 on p.projectID equals b.fk_project
                                                                 where b.active == true
                                                                 && b.fk_field == field.filedID
                                                                 select b).Distinct().ToList();
                                List<String> strList = (from p in colList select p.value).ToList();
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
                        #endregion
                        List<Contracts> allcontracts = (from c in zakupkaDB.Contracts
                                                        where c.active
                                                        join b in zakupkaDB.ContractProjectMappingTable
                                                        on c.contractID equals b.fk_contract
                                                        where b.Active == true
                                                        join z in zakupkaDB.Projects
                                                        on b.fk_project equals z.projectID
                                                        where z.active == true
                                                        where z.projectID == a.projectID
                                                        select c).Distinct().ToList();
                        //пройдемся по всем договорам 
                        foreach (Contracts b in allcontracts)
                        {
                            TableRow contractRow = new TableRow();
                            TableCell contractCell = new TableCell();
                            contractCell.Text = b.name;
                            contractRow.Cells.Add(contractCell);
                            #region вывод заначений для договоров                  
                            foreach (Fields field in allfields)
                            {
                                TableCell newValue = new TableCell();
                                newValue.Text = (from c in zakupkaDB.CollectedValues
                                                 where c.active == true
                                                 && c.fk_contract == b.contractID
                                                 && c.fk_field == field.filedID
                                                 && c.fk_project == a.projectID
                                                 && c.fk_event == o.eventID
                                                 select c.value).FirstOrDefault();
                                contractRow.Cells.Add(newValue);
                            }
                            tableToReturn.Rows.Add(contractRow);
                            #endregion
                        }
                    }
                }
                    TableDiv.Controls.Add(tableToReturn);
                
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
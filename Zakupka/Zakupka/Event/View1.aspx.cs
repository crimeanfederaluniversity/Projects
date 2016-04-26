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
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
                Response.Redirect("~/Default.aspx");
            {         
                Table tableToReturn = new Table();
                List<Fields> allfields = new List<Fields>();
                #region view1
                if (DropDownList2.SelectedIndex == 0)
                {
                    ViewOne();
                }
                if (DropDownList2.SelectedIndex == 1)
                {
                    ViewSecond();
                }
                if(DropDownList2.SelectedIndex == 2)
                {
                    ViewThird();
                }
        }
        public void ViewOne()
        {
            List<Fields> allfields = new List<Fields>();
            Table tableToReturn = new Table();
            allfields = (from f in zakupkaDB.Fields where f.active == true  join z in zakupkaDB.ViewField
                         on f.filedID equals z.fk_field  where z.viewtype == 1 && z.active == true select f).ToList();
            TableRow newNameRow = new TableRow();
            newNameRow.Cells.Add(new TableCell() { Text = "Договор" });
            newNameRow.Cells.Add(new TableCell() { Text = "Проект" });
            foreach (Fields field in allfields)
            {
                TableCell newField = new TableCell();
                TableCell newFieldId = new TableCell();
                newField.Text = field.name;
                newNameRow.Cells.Add(newField);
                tableToReturn.Rows.Add(newNameRow);
            }
            List<Contracts> allcontracts = (from c in zakupkaDB.Contracts where c.active select c).ToList();
            //пройдемся по всем договорам 
            foreach (Contracts b in allcontracts)
            {
                List<Projects> contractProjects = (from a in zakupkaDB.Projects join d in zakupkaDB.ContractProjectMappingTable
                                                   on a.projectID equals d.fk_project  where d.fk_contract == b.contractID
                                                   where a.active == true  && d.Active == true  select a).ToList();
                int i = 0;
                int prjCnt = contractProjects.Count();
                foreach (Projects tmp in contractProjects)
                {
                    TableRow contractRow = new TableRow();
                    if (i == 0)
                    {
                        TableCell contractCell = new TableCell();
                        contractCell.Text = b.name;
                        contractCell.RowSpan = prjCnt;
                        contractRow.Cells.Add(contractCell);
                    }
                    contractRow.Cells.Add(new TableCell() { Text = tmp.name });
                    #region вывод заначений для договоров                  
                    foreach (Fields field in allfields)
                    {
                        TableCell newValue = new TableCell();
                        bool isForPrj = (bool)(from a in zakupkaDB.ViewField  where a.fk_field == field.filedID
                                               && a.viewtype == 1  select a.byproject).FirstOrDefault();
                        if (!isForPrj && i == 0)
                        {
                            newValue.RowSpan = prjCnt;
                            CollectedValues myValue = (from c in zakupkaDB.CollectedValues
                                                       where c.active == true && c.fk_contract == b.contractID
                                                       && c.fk_field == field.filedID
                                                       select c).FirstOrDefault();
                            if (myValue != null)
                            {
                                newValue.Text = myValue.value;
                                contractRow.Cells.Add(newValue);
                            }
                            else
                            {
                                newValue.Text = "";
                                contractRow.Cells.Add(newValue);
                            }
                        }
                        else if (isForPrj)
                        {
                            List<CollectedValues> myValue = (from c in zakupkaDB.CollectedValues  where c.active == true && c.fk_contract == b.contractID
                                                             && c.fk_field == field.filedID  && c.fk_project == tmp.projectID select c).ToList();
                            Fields type = (from c in zakupkaDB.Fields where c.active == true && c.filedID == field.filedID select c).FirstOrDefault();
                            {
                                string name = "";
                                foreach (CollectedValues n in myValue)
                                {
                                    name += n.value;
                                }
                                newValue.Text = name;
                                contractRow.Cells.Add(newValue);
                            }
                        }
                    }
                    tableToReturn.Rows.Add(contractRow);
                    i++;
                }
                TableDiv.Controls.Add(tableToReturn);
                #endregion
            }
        }
        #endregion

        public void ViewSecond()
        {
            List<Fields> allfields = new List<Fields>();

            Table tableToReturn = new Table();

            allfields = (from f in zakupkaDB.Fields  where f.active == true
                         join z in zakupkaDB.ViewField on f.filedID equals z.fk_field
                         where z.viewtype == 2 && z.active == true select f).ToList();
            int allFieldCnt = allfields.Count();

            List<MainEvent> allEvents = (from a in zakupkaDB.MainEvent where a.Active == true select a).ToList();
            int allEventCount = allEvents.Count();
            TableRow newNameRow = new TableRow();
            newNameRow.Cells.Add(new TableCell() { Text = "Проект" , RowSpan = 2});
            foreach (Fields field in allfields)
            {
                bool isFieldMulti =(bool) (from a in zakupkaDB.ViewField
                                       where a.fk_field == field.filedID
                                         && a.viewtype == 2 && a.active == true
                                       select a.byproject).FirstOrDefault();
                TableCell newField = new TableCell();
                if (isFieldMulti)
                {
                    newField.ColumnSpan = allEventCount;
                }
                else
                {
                    newField.RowSpan = 2;
                }
                newField.Text = field.name;
                newNameRow.Cells.Add(newField);               
            }
            tableToReturn.Rows.Add(newNameRow);
            newNameRow = new TableRow();
            foreach (Fields field in allfields)
            {
                bool isFieldMulti = (bool)(from a in zakupkaDB.ViewField
                                           where a.fk_field == field.filedID
                                             && a.viewtype == 2 && a.active == true
                                           select a.byproject).FirstOrDefault();                
                if (isFieldMulti)
                {
                    foreach(MainEvent even in allEvents)
                    {
                        TableCell newField = new TableCell();
                        newField.Text = even.ID.ToString();
                        newNameRow.Cells.Add(newField);                       
                    }                   
                }
                else
                {
                    
                }
            }
            tableToReturn.Rows.Add(newNameRow);

            List<Projects> allprojects = (from a in zakupkaDB.Projects where a.active select a).Distinct().ToList();
            foreach (Projects b in allprojects)
            {
                TableRow newRow = new TableRow();
                newRow.Cells.Add(new TableCell() { Text = b.name });
                foreach (Fields field in allfields)
                {
                    bool isFieldMulti = (bool)(from a in zakupkaDB.ViewField
                                               where a.fk_field == field.filedID
                                                 && a.viewtype == 2 && a.active == true
                                               select a.byproject).FirstOrDefault();
                    if (isFieldMulti)
                    {
                        foreach (MainEvent even in allEvents)
                        {
                            TableCell newField = new TableCell();
                            ProjectsValues value = (from a in zakupkaDB.ProjectsValues
                                                    where a.fk_field == field.filedID
                                                    && a.fk_project == b.projectID
                                                    join c in zakupkaDB.Events
                                                    on a.fk_event equals c.eventID
                                                    where c.fk_mainevent == even.ID
                                                    select a).FirstOrDefault();
                            if (value != null)
                                newField.Text = value.value;
                            newRow.Cells.Add(newField);
                        }
                    }
                    else
                    {
                        TableCell newField = new TableCell();
                        ProjectsValues value = (from a in zakupkaDB.ProjectsValues
                                                where a.fk_field == field.filedID
                                                && a.fk_project == b.projectID
                                                select a).FirstOrDefault();
                        if (value != null)
                            newField.Text = value.value;
                        newRow.Cells.Add(newField);
                    }
                }
                tableToReturn.Rows.Add(newRow);
            }               
                TableDiv.Controls.Add(tableToReturn);         
        }

        public void ViewThird()
        {
            Table tableToReturn = new Table();
            List<Fields> allfields = new List<Fields>();
            allfields = (from f in zakupkaDB.Fields  where f.active == true
                         join z in zakupkaDB.ViewField.OrderBy(mc => mc.@orderby)
                         on f.filedID equals z.fk_field where z.viewtype == 3 && z.active == true
                         select f).ToList();

            TableRow newNameRow = new TableRow();

            newNameRow.Cells.Add(new TableCell() { Text = "Мероприятие" });

            foreach (Fields field in allfields)
            {
                TableCell newField = new TableCell();
                newField.Text = field.name;
                newNameRow.Cells.Add(newField);
            }
            tableToReturn.Rows.Add(newNameRow);

            List <MainEvent> allevents = (from k in zakupkaDB.MainEvent where k.Active == true select k).ToList();
            //пройдемся по всем мероприятиям       
            foreach (MainEvent o in allevents)
            {
                TableRow eventRow = new TableRow();
                eventRow.Cells.Add(new TableCell()
                { Text = o.MainEvent1 });
                foreach (Fields field in allfields)
                {
                    TableCell sumForMerValue = new TableCell();
                    #region вывод заначений для мероприятий

                    List<ProjectsValues> eventList = (from w in zakupkaDB.ProjectsValues
                                                      join x in zakupkaDB.Events on w.fk_event equals x.eventID
                                                      where x.fk_mainevent == o.ID && w.active == true && w.fk_field == field.filedID  select w).Distinct().ToList();
                    if (eventList.Count == 1)
                    {
                        foreach (ProjectsValues n in eventList)
                        {
                            sumForMerValue.Text = n.value;
                            eventRow.Cells.Add(sumForMerValue);
                        }
                    }
                    else
                    {
                        Fields type = (from c in zakupkaDB.Fields where c.active == true && c.filedID == field.filedID select c).FirstOrDefault();
                        if (type.type == "float")
                        {
                            decimal sum = 0;
                            foreach (ProjectsValues n in eventList)
                            {
                                decimal tmp = 0;
                                Decimal.TryParse(n.value, out tmp);
                                sum += tmp;
                            }
                            sumForMerValue.Text = sum.ToString();
                            eventRow.Cells.Add(sumForMerValue);
                        }
                        if (type.type == "string")
                        {
                            string name = "По мероприятиям:";
                            foreach (ProjectsValues n in eventList)
                            {
                                name += n.value + ",";
                            }
                            sumForMerValue.Text = name;
                            eventRow.Cells.Add(sumForMerValue);
                        }
                    }
                }
                tableToReturn.Rows.Add(eventRow);
                #endregion
            }
            TableDiv.Controls.Add(tableToReturn);
        }
 
    }
}
          
        
  
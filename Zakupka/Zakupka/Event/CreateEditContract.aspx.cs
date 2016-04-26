using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Zakupka.Event
{
    public partial class CreateEditContract : System.Web.UI.Page
    {

        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();        
        public List<ValueSaveClass> ValuesList = new List<ValueSaveClass>();
        public DataValidator validator = new DataValidator();
        public class ValueSaveClass
        {
            public TextBox Value { get; set; }
            public int FieldId { get; set; }
            public int ContractId { get; set; }
            public int ProjectId { get; set; }
            public int EventId { get; set; }

        }
        public void SaveCollectedValue(int fieldId, int contractId, int projectid, int eventid, string value)
        {
            Fields typeoffield = (from a in zakupkaDB.Fields where a.filedID == fieldId where a.active select a).FirstOrDefault();
            if (typeoffield.commonfield == true)
            {
                CollectedValues fk = (from a in zakupkaDB.CollectedValues
                                      where a.fk_contract == contractId  && a.fk_field == fieldId
                                        && a.fk_project == null  && a.fk_event == null  select a).FirstOrDefault();
                fk.value = value;
                zakupkaDB.SubmitChanges();
            }
            else
            {
                CollectedValues fk = (from a in zakupkaDB.CollectedValues
                                      where a.fk_contract == contractId
                                      && a.fk_field == fieldId  && a.fk_project == projectid  && a.fk_event == eventid select a).FirstOrDefault();
                fk.value = value;
                zakupkaDB.SubmitChanges();
            }                          
        }
        public string GetCollectedValue(int fieldId, int contractId, int projectid, int eventid)
        {
                CollectedValues fk = (from a in zakupkaDB.CollectedValues   where   a.fk_contract == contractId
                                          && a.fk_field == fieldId   && (a.fk_project == projectid || a.fk_project == null)
                                          && (a.fk_event == eventid || a.fk_event == null)   select a).FirstOrDefault();
                if(fk != null)
                {
                    if (fk.active == false)
                    {
                        fk.active = true;
                        zakupkaDB.SubmitChanges();
                    }                       
                }
                else
                {
                Fields typeoffield = (from a in zakupkaDB.Fields where a.filedID == fieldId where a.active select a).FirstOrDefault();
                if (typeoffield.commonfield == true)
                {
                    fk = new CollectedValues();
                    fk.active = true;
                    fk.value = "";
                    fk.fk_field = fieldId;
                    fk.fk_contract = contractId;
                    fk.fk_project = null;
                    fk.fk_event = null;
                    zakupkaDB.CollectedValues.InsertOnSubmit(fk);
                    zakupkaDB.SubmitChanges();
                }
                else
                {
                    fk = new CollectedValues();
                    fk.active = true;
                    fk.value = "";
                    fk.fk_field = fieldId;
                    fk.fk_contract = contractId;
                    fk.fk_project = projectid;
                    fk.fk_event = eventid;
                    zakupkaDB.CollectedValues.InsertOnSubmit(fk);
                    zakupkaDB.SubmitChanges();
                }                
              }                               
                return fk.value;
        }
        public Table CreateNewTable ()
        {
            int userID = Convert.ToInt32(Session["userID"]);
            int conId = Convert.ToInt32(Session["contractID"]);
            int projectId = Convert.ToInt32(Session["projectID"]);
            int eventId = Convert.ToInt32(Session["eventID"]);
            Contracts name = (from a in zakupkaDB.Contracts where a.contractID == conId select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            Table tableToReturn = new Table();
            List<Fields> allFields = (from a in zakupkaDB.Fields where a.active && a.step == 1  select a).ToList();
            int maxLine = (from a in allFields select a.line).OrderByDescending(mc => mc).FirstOrDefault();

            for (int i = 0; i < maxLine + 1; i++)
            {
                TableRow newRow = new TableRow();
                TableRow headerNewRow = new TableRow();
                List<Fields> fieldsInLine = (from a in allFields where a.line == i select a).OrderBy(mc => mc.col).ToList();
                foreach (Fields field in fieldsInLine)
                {
                    TableCell newCell = new TableCell();
                    TableCell headerNewCell = new TableCell();
                    headerNewCell.Text = field.name;
                    if (field.staticvalue != true)
                    {
                        TextBox newTextBox = validator.GetTextBox(field.type);
                        newTextBox.Width = field.width;
                        newTextBox.Height = field.height;
                        newTextBox.ID = "textBox" + field.filedID.ToString();
                        int contractId = conId;// вытаскиваеш из сессии
                        newTextBox.Text = GetCollectedValue(field.filedID, contractId, projectId, eventId);

                        ValueSaveClass classTmp = new ValueSaveClass();
                        classTmp.Value = newTextBox;
                        classTmp.FieldId = field.filedID;
                        classTmp.ContractId = contractId;
                        classTmp.ProjectId = projectId;
                        classTmp.EventId = eventId;
                        ValuesList.Add(classTmp);

                        newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.filedID, "textBox" + field.filedID.ToString(), field.type));
                        newCell.Controls.Add(newTextBox);
                    }
                    else
                    {
                        CollectedValues value = (from a in zakupkaDB.CollectedValues where a.active == true && a.fk_field == field.filedID && a.fk_contract == conId && a.fk_project == projectId && a.fk_event == eventId select a).FirstOrDefault();
                        if(value != null)
                        {
                            newCell.Text = value.value.ToString();
                           // newRow.Cells.Add(new TableCell() { Text = value.value.ToString() });
                        }                       
                    }
                    newRow.Cells.Add(newCell);
                    headerNewRow.Cells.Add(headerNewCell);
                }
                tableToReturn.Rows.Add(headerNewRow);
                tableToReturn.Rows.Add(newRow);
            }
            Session["contractvalues"] = ValuesList;
            return tableToReturn;                     
        }      
        public void SaveAll()
        {            
            foreach (ValueSaveClass tmp in ValuesList)
            {
                SaveCollectedValue(tmp.FieldId, tmp.ContractId,tmp.ProjectId,tmp.EventId, tmp.Value.Text);
            }
            int projectId = Convert.ToInt32(Session["projectID"]);
            int eventId = Convert.ToInt32(Session["eventID"]);
            int conId = Convert.ToInt32(Session["contractID"]);
          
            if (CostClass.SelectedIndex != 0)
            {
                CollectedValues currentcost = (from a in zakupkaDB.CollectedValues where a.active == true &&  a.fk_contract == conId
                                               join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                               where b.engname == "costclass"
                                               select a).FirstOrDefault();
                if (currentcost != null)
                {
                    currentcost.value = CostClass.SelectedValue;
                    zakupkaDB.SubmitChanges();
                }
                else
                {
                    int id = (from b in zakupkaDB.Fields where b.engname == "costclass" select b.filedID).FirstOrDefault();
                    CollectedValues cost = new CollectedValues();
                    cost.active = true;
                    cost.value = CostClass.SelectedValue;
                    cost.fk_field = id;
                    cost.fk_contract = conId;
                    cost.fk_project = projectId;
                    cost.fk_event = eventId;
                    zakupkaDB.CollectedValues.InsertOnSubmit(cost);
                    zakupkaDB.SubmitChanges();
                }
            }           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
                Response.Redirect("~/Default.aspx");

            int projectID = (int)Session["projectID"];
            int conId = Convert.ToInt32(Session["contractID"]);
            ValuesList = (List<ValueSaveClass>)Session["contractvalues"];
            if (ValuesList == null)
                ValuesList = new List<ValueSaveClass>();
            Refresh();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
                if (!Page.IsPostBack)
                {
                    ProjectsValues currentcost = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_field == 27 && a.fk_project == projectID select a).FirstOrDefault();
                    if (currentcost != null) { CostClass.Visible = false; }
                    else
                    {
                        CollectedValues contractcost = (from a in zakupkaDB.CollectedValues where a.active == true && a.fk_field == 27 && a.fk_contract == conId select a).FirstOrDefault();
                        string currentCostTextValue = "";
                        if (contractcost != null)
                        {
                            currentCostTextValue = contractcost.value;
                        }
                        List<CostClassTable> allcost = (from a in zakupkaDB.CostClassTable where a.active == true select a).ToList();
                        CostClass.Items.Add(new ListItem { Text = "Выберите вид расходов", Value = 0.ToString() });
                        foreach (var item in allcost)
                            CostClass.Items.Add(new ListItem { Text = item.name, Value = item.name, Selected = (item.name == currentCostTextValue) });
                    }
                }
            }
        
        protected void Refresh()
        {
            int eventId = Convert.ToInt32(Session["eventID"]);
            int conId = Convert.ToInt32(Session["contractID"]);
            int projectID = (int)Session["projectID"];
            Contracts name = (from a in zakupkaDB.Contracts where a.contractID == conId  select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Id", typeof(string)));
            dataTable.Columns.Add(new DataColumn("type", typeof(string)));
            dataTable.Columns.Add(new DataColumn("kosgu", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Sum", typeof(string)));

            List<CollectedValues> valueList = (from a in zakupkaDB.CollectedValues where a.active == true  
                                               && a.fk_kosgu != null && a.fk_contract == conId &&  a.fk_project == projectID && a.fk_event == eventId select a).ToList();     
            foreach (CollectedValues value in valueList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["Id"] = value.collectedFieldID;
                dataRow["type"] = (from a in zakupkaDB.KOSGUtable where a.Active == true && a.ID == value.fk_kosgu select a.Class).FirstOrDefault().ToString();
                dataRow["kosgu"] = (from a in zakupkaDB.KOSGUtable where a.Active == true && a.ID == value.fk_kosgu select a.Name).FirstOrDefault().ToString();
                dataRow["Sum"] = Convert.ToDecimal(value.value);
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void kosgutype_SelectedIndexChanged(object sender, EventArgs e)
        {
            int conId = Convert.ToInt32(Session["contractID"]);
            List<KOSGUtable> allkosgu = (from a in zakupkaDB.KOSGUtable where a.Active == true && a.Class == Convert.ToInt32(kosgutype.Items[kosgutype.SelectedIndex].Value)
                                         join b in zakupkaDB.ProjectsValues on a.ID equals b.fk_kosgu
                                         where b.active == true
                                         join c in zakupkaDB.ContractProjectMappingTable on b.fk_project equals c.fk_project
                                         where c.Active == true && c.fk_contract == conId
                                         select a).Distinct().ToList();
            if (allkosgu != null && allkosgu.Count() > 0)
            {
                kosgu.Visible = true;
                AddRowButton.Visible = true;
                TextBox1.Visible = true;
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");
                foreach (var item in allkosgu)
                    dictionary.Add(item.ID, item.Name);
                kosgu.DataTextField = "Value";
                kosgu.DataValueField = "Key";
                kosgu.DataSource = dictionary;
                kosgu.DataBind();
            }
            else
            {
                kosgu.Visible = false;
                AddRowButton.Visible = false;
                TextBox1.Visible = false; 
            }
        }
        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            SaveAll();
            if (TextBox1.Text != null && kosgu.SelectedIndex != 0 && kosgutype.SelectedIndex != 0)
            {
                int eventId = Convert.ToInt32(Session["eventID"]);
                int conId = Convert.ToInt32(Session["contractID"]);
                int projectID = (int)Session["projectID"];           
                TableDiv.Controls.Clear();
                TableDiv.Controls.Add(CreateNewTable());
                kosguError.Visible = false;
                List<ProjectsValues> max = (from a in zakupkaDB.ProjectsValues
                                            where a.active == true && a.fk_kosgu == Convert.ToInt32(kosgu.Items[kosgu.SelectedIndex].Value)
                                            join b in zakupkaDB.ContractProjectMappingTable on a.fk_project equals b.fk_project
                                            where b.Active == true && b.fk_contract == conId
                                            select a).ToList();
                foreach (ProjectsValues n in max)
                {
                    if (Convert.ToDecimal(TextBox1.Text) > Convert.ToDecimal(n.value))
                    {
                        kosguError.Visible = true;
                    }
                }
                if (kosguError.Visible == false)
                {
                    CollectedValues newValue = new CollectedValues();
                    newValue.active = true;
                    newValue.fk_project = projectID;
                    newValue.fk_kosgu = Convert.ToInt32(kosgu.Items[kosgu.SelectedIndex].Value);
                    newValue.value = TextBox1.Text;
                    newValue.fk_event = eventId;
                    newValue.fk_contract = conId;
                    zakupkaDB.CollectedValues.InsertOnSubmit(newValue);
                    zakupkaDB.SubmitChanges();
                    Response.Redirect("~/Event/CreateEditContract.aspx");
                }
            }
        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                TextBox textBox = (TextBox)row.FindControl("Sum");
                CollectedValues value = (from a in zakupkaDB.CollectedValues
                                         where a.collectedFieldID == Convert.ToInt32(button.CommandArgument) && a.active == true
                                        select a).FirstOrDefault();
                if (value != null)
                {                 
                    value.value = textBox.Text;
                    zakupkaDB.SubmitChanges();
                    Refresh();
                }
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                CollectedValues value = (from a in zakupkaDB.CollectedValues
                                         where a.collectedFieldID == Convert.ToInt32(button.CommandArgument) && a.active == true
                                        select a).FirstOrDefault();
                if (value != null)
                {
                    value.active = false;
                    zakupkaDB.SubmitChanges();
                    Refresh();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int projectId = Convert.ToInt32(Session["projectID"]);
            int eventId = Convert.ToInt32(Session["eventID"]);
            int conId = Convert.ToInt32(Session["contractID"]);
            SaveAll();
            
            #region СМЕТА ПО ДОГОВОРУ В ДАННОМ ПРОЕКТЕ
            CollectedValues smeta = (from a in zakupkaDB.CollectedValues
                                     where a.active == true && a.fk_contract == conId && a.fk_project == projectId && a.fk_event == eventId
                                     join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                     where b.engname == "contractsum"
                                     select a).FirstOrDefault();
            if (smeta == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "contractsum" select b.filedID).FirstOrDefault();
                smeta = new CollectedValues();
                smeta.active = true;
                smeta.fk_field = id;
                smeta.fk_contract = conId;
                smeta.fk_event = eventId;
                smeta.fk_project = projectId;
                zakupkaDB.CollectedValues.InsertOnSubmit(smeta);
                zakupkaDB.SubmitChanges();              
            }
            List<CollectedValues> valueList = (from a in zakupkaDB.CollectedValues
                                               where a.active == true && a.fk_field == null  && a.fk_kosgu != null && a.fk_contract == conId && a.fk_project == projectId && a.fk_event == eventId
                                               select a).ToList();

            if (valueList.Count != 0)
                {
                    Decimal sum = 0;
                    foreach (var n in valueList)
                    {
                        sum = sum + Convert.ToDecimal(n.value);
                    }
                    smeta.value = sum.ToString();
                    zakupkaDB.SubmitChanges();
                }
            else
            {
                smeta.value = "0";
                zakupkaDB.SubmitChanges();
            }
            #endregion

            Calculations calc = new Calculations();
            calc.CalculateMIyProject(projectId, eventId);

            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
            Response.Redirect("~/Event/CreateEditContract.aspx");
            
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/ContractPage.aspx");
        }
    }
}
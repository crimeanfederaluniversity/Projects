using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class ProjectEdit : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        public List<ProjectValueSaveClass> ValuesList = new List<ProjectValueSaveClass>();
        public DataValidator validator = new DataValidator();
        public class ProjectValueSaveClass
        {
            public TextBox Value { get; set; }
            public int FieldId { get; set; }       
            public int ProjectId { get; set; }
            public int EventId { get; set; }
        }
        public void SaveCollectedValue(int fieldId,  int projectid, int eventid, string value)
        {
            ProjectsValues fk = (from a in zakupkaDB.ProjectsValues  where  a.fk_field == fieldId  && a.fk_project == projectid  && a.fk_event == eventid  select a).FirstOrDefault();
            fk.value = value;
            zakupkaDB.SubmitChanges();
        }
        public string GetCollectedValue(int fieldId,  int projectid, int eventid)
        {
            ProjectsValues fk = (from a in zakupkaDB.ProjectsValues  where a.fk_field == fieldId  && a.fk_project == projectid  && a.fk_event == eventid select a).FirstOrDefault();
            if (fk != null)
            {
                if (fk.active == false)
                {
                    fk.active = true;
                    zakupkaDB.SubmitChanges();
                }
            }
            else
            {
                fk = new ProjectsValues();
                fk.active = true;
                fk.value = "";
                fk.fk_field = fieldId;
                fk.fk_project = projectid;
                fk.fk_event = eventid;
                zakupkaDB.ProjectsValues.InsertOnSubmit(fk);
                zakupkaDB.SubmitChanges();
            }
            return fk.value;
        }
        public Table CreateNewTable()
        {
            int userID = Convert.ToInt32(Session["userID"]);
            int projectId = Convert.ToInt32(Session["projectID"]);
            int eventId = Convert.ToInt32(Session["eventID"]);
            Projects name = (from a in zakupkaDB.Projects where a.projectID == projectId select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            Table tableToReturn = new Table();
            List<Fields> allFields = (from a in zakupkaDB.Fields  where a.active && a.step == 2   select a).ToList();
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
                newTextBox.Text = GetCollectedValue(field.filedID, projectId, eventId);
                ProjectValueSaveClass classTmp = new ProjectValueSaveClass();
                classTmp.Value = newTextBox;
                classTmp.FieldId = field.filedID;
                classTmp.ProjectId = projectId;
                classTmp.EventId = eventId;
                ValuesList.Add(classTmp);
                newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.filedID, "textBox" + field.filedID.ToString(), field.type));
                newCell.Controls.Add(newTextBox);               
            }
            else
            {
                string value = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_field == field.filedID && a.fk_project == projectId && a.fk_event == eventId select a.value).FirstOrDefault();
                        if(value != null)
                        { 
                            newRow.Cells.Add(new TableCell() { Text = value.ToString() });
                        }           
            }
            newRow.Cells.Add(newCell);
            headerNewRow.Cells.Add(headerNewCell);
                }
                tableToReturn.Rows.Add(headerNewRow);
                tableToReturn.Rows.Add(newRow);
            }
            Session["values"] = ValuesList;
            return tableToReturn;
        }
        public void SaveAll()
        {
            foreach (ProjectValueSaveClass tmp in ValuesList)
            {
                SaveCollectedValue(tmp.FieldId, tmp.ProjectId, tmp.EventId, tmp.Value.Text);
            }
            int projectId = Convert.ToInt32(Session["projectID"]);
            int eventId = Convert.ToInt32(Session["eventID"]);
         
            if (Struct.SelectedIndex != 0)
            {
                ProjectsValues currentstruct = (from a in zakupkaDB.ProjectsValues where a.active == true  && a.fk_project == projectId
                                                join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                                where b.engname == "struct"  select a).FirstOrDefault();
                if (currentstruct != null)
                {
                    currentstruct.value = Struct.SelectedValue;
                    zakupkaDB.SubmitChanges();
                }
                else
                {
                    int id = (from b in zakupkaDB.Fields where b.engname == "struct" select b.filedID).FirstOrDefault();
                    ProjectsValues str = new ProjectsValues();
                    str.active = true;
                    str.value = Struct.SelectedValue;
                    str.fk_field = id;
                    str.fk_project = projectId;
                    zakupkaDB.ProjectsValues.InsertOnSubmit(str);
                    zakupkaDB.SubmitChanges();
                }
            }
            if (CostClass.SelectedIndex != 0)
            {
                ProjectsValues currentcost = (from a in zakupkaDB.ProjectsValues where a.active == true  && a.fk_project == projectId
                                              join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                              where b.engname == "costclass"  select a).FirstOrDefault();
                if(currentcost != null)
                {
                    currentcost.value = CostClass.SelectedValue;
                    zakupkaDB.SubmitChanges();
                }
                else
                  {
                    int id = (from b in zakupkaDB.Fields where b.engname == "costclass" select b.filedID).FirstOrDefault();
                    ProjectsValues cost = new ProjectsValues();
                    cost.active = true;
                    cost.value = CostClass.SelectedValue;
                    cost.fk_field = id;
                    cost.fk_project = projectId;
                    zakupkaDB.ProjectsValues.InsertOnSubmit(cost);
                    zakupkaDB.SubmitChanges();
                }
            }                   
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int projectID = (int)Session["projectID"];
            Projects name = (from a in zakupkaDB.Projects where a.projectID == projectID select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            Refresh();
            ValuesList = (List<ProjectValueSaveClass>)Session["values"];
            if (ValuesList == null) ValuesList = new List<ProjectValueSaveClass>();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
            if (!Page.IsPostBack)
            {
                ProjectsValues currentstruct = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_field == 26 && a.fk_project == projectID select a).FirstOrDefault();
                string currentStructTextValue = "";
                if (currentstruct != null)
                { currentStructTextValue = currentstruct.value; }
                
                List<StructTable> allstruct = (from a in zakupkaDB.StructTable where a.active == true select a).ToList();
                Struct.Items.Add(new ListItem {Text= "Выберите структурное подразделение", Value="0" });
                foreach (var item in allstruct)
                    Struct.Items.Add(new ListItem { Text = item.name, Value = item.name, Selected=(item.name == currentStructTextValue) });
             

                ProjectsValues currentcost = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_field == 27 && a.fk_project == projectID select a).FirstOrDefault();
                string currentCostTextValue = "";
                if (currentcost != null)
                {
                    currentCostTextValue = currentcost.value;
                }
                List<CostClassTable> allcost = (from a in zakupkaDB.CostClassTable where a.active == true select a).ToList();
                CostClass.Items.Add(new ListItem { Text = "Выберите вид расходов", Value = 0.ToString() });
                foreach (var item in allcost)
                    CostClass.Items.Add(new ListItem { Text = item.name, Value = item.name, Selected = (item.name == currentCostTextValue) });
            }
        }     
        protected void Refresh()
        {
            int eventId = Convert.ToInt32(Session["eventID"]);
            int projectID = (int)Session["projectID"];
            Projects name = (from a in zakupkaDB.Projects where a.projectID == projectID select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Id", typeof(string)));
            dataTable.Columns.Add(new DataColumn("type", typeof(string)));
            dataTable.Columns.Add(new DataColumn("kosgu", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Sum", typeof(string)));
            List<ProjectsValues> valueList = (from a in zakupkaDB.ProjectsValues where a.active == true && a.fk_kosgu != null && a.fk_project == projectID && a.fk_event == eventId select a).ToList();                   
            foreach (ProjectsValues value in valueList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["Id"] = value.Id;
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
            List<KOSGUtable> allkosgu = (from a in zakupkaDB.KOSGUtable where a.Active == true && a.Class == Convert.ToInt32(kosgutype.Items[kosgutype.SelectedIndex].Value) select a).ToList();
            if (allkosgu != null && allkosgu.Count() > 0)
            {
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");
                foreach (var item in allkosgu)
                    dictionary.Add(item.ID, item.Name);
                kosgu.DataTextField = "Value";
                kosgu.DataValueField = "Key";
                kosgu.DataSource = dictionary;
                kosgu.DataBind();
            }
        }
        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != null && kosgu.SelectedIndex != 0 && kosgutype.SelectedIndex != 0)
            {
                int eventId = Convert.ToInt32(Session["eventID"]);
                int userID = (int)Session["userID"];
                int projectID = (int)Session["projectID"];
                ProjectsValues newValue = new ProjectsValues();
                newValue.active = true;
                newValue.fk_project = projectID;
                newValue.fk_kosgu = Convert.ToInt32(kosgu.Items[kosgu.SelectedIndex].Value);
                newValue.value = TextBox1.Text;
                newValue.fk_event = eventId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(newValue);
                zakupkaDB.SubmitChanges();
                SaveAll();
                TableDiv.Controls.Clear();
                TableDiv.Controls.Add(CreateNewTable());
                Response.Redirect("~/Event/ProjectEdit.aspx");
            }
        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                TextBox textBox = (TextBox)row.FindControl("Sum");
                ProjectsValues value = (from a in zakupkaDB.ProjectsValues
                                        where a.Id == Convert.ToInt32(button.CommandArgument) && a.active == true
                                         select a).FirstOrDefault();
                if (value != null )
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
                ProjectsValues value = (from a in zakupkaDB.ProjectsValues
                                        where a.Id == Convert.ToInt32(button.CommandArgument) && a.active == true
                                         select a).FirstOrDefault(); 
                if(value!= null)
                {
                    value.active = false;
                    zakupkaDB.SubmitChanges();
                    Refresh();
                }
            }
         }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
    
        }
        protected void kosgu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/ProjectPage.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int eventId = Convert.ToInt32(Session["eventID"]);
            int projectId = (int)Session["projectID"];
            SaveAll();
            Calculations calc = new Calculations();
            calc.CalculateMIyProject(projectId,eventId);

            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
            Response.Redirect("~/Event/ProjectEdit.aspx");
        }

        protected void Struct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CostClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
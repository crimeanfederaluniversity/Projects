using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class TamplatesList : System.Web.UI.Page
    {
        EDMdbDataContext _edm = new EDMdbDataContext();
        ProcessMainFucntions _main = new ProcessMainFucntions();


        protected void Refresh()
        {
            int userID;
            int.TryParse(Session["userID"].ToString(), out userID);

            List<ProcessTemplate> userTampleList = (from a in _edm.ProcessTemplate
                                                    where a.active == true
                                                    && a.fk_owner == userID
                                                    select a).ToList();

            Table newTable = new Table();
            newTable.CssClass = "table table-striped edm-table edm-PocessEdit-table centered-block";

            TableHeaderRow newHeaderRow = new TableHeaderRow();
            TableHeaderCell headerCell1 = new TableHeaderCell();
            TableHeaderCell headerCell2 = new TableHeaderCell();
            TableHeaderCell headerCell3 = new TableHeaderCell();


            headerCell1.Text = "Название шаблона";
            headerCell2.Text = "Переход к редактированию шаблона";
            headerCell3.Text = "Удаление шаблона";


            newHeaderRow.Cells.Add(headerCell1);
            newHeaderRow.Cells.Add(headerCell2);
            newHeaderRow.Cells.Add(headerCell3);

            newTable.Rows.Add(newHeaderRow);

            foreach (ProcessTemplate processTemplate in userTampleList)
            {
                TableRow newRow = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();

                cell1.Text = processTemplate.name;

                Button viewButton = new Button();
                viewButton.ID = "viewButton" + processTemplate.processTemplateId;
                viewButton.Text = "Просмотреть";
                viewButton.CommandArgument = processTemplate.processTemplateId.ToString();
                viewButton.CausesValidation = false;
                viewButton.Click += ViewProcessTemplate;
                cell2.Controls.Add(viewButton);

                Button deleteButton = new Button();
                deleteButton.ID = "delButton" + processTemplate.processTemplateId;
                deleteButton.Text = "Удалить";
                deleteButton.CommandArgument = processTemplate.processTemplateId.ToString();
                deleteButton.CausesValidation = false;
                deleteButton.Click += DeleteProcessTemplate;
                cell3.Controls.Add(deleteButton);


                newRow.Cells.Add(cell1);
                newRow.Cells.Add(cell2);
                newRow.Cells.Add(cell3);
                newTable.Rows.Add(newRow);
            }
            mainDiv.Controls.Clear();
            mainDiv.Controls.Add(newTable);
        }

        protected void ViewProcessTemplate(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            int processTemplateId = Convert.ToInt32(button.CommandArgument);
            Session["ProcessTemplateId"] = processTemplateId;
            Response.Redirect("CreateEditTemplate.aspx");
        }

        protected void DeleteProcessTemplate(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            int processTemplateId = Convert.ToInt32(button.CommandArgument);
            ProcessTemplate template = (from a in _edm.ProcessTemplate
                where a.processTemplateId == processTemplateId
                select a).FirstOrDefault();
            template.active = false;
            _edm.SubmitChanges();
            Refresh();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            Refresh();

        }

        protected void CreateTemplate(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID;
            int.TryParse(Session["userID"].ToString(), out userID);
            
            ProcessTemplate newTemplate = new ProcessTemplate();
            newTemplate.active = true;
            newTemplate.name = NewTemplateNameTextBox.Text;
            newTemplate.fk_owner = userID;
            newTemplate.type = ProcessTypeDropDown.SelectedValue;
            _edm.ProcessTemplate.InsertOnSubmit(newTemplate);
            _edm.SubmitChanges();
            Refresh();
        }

        protected void goBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}
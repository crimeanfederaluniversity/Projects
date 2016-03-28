using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class SubmittedPage : System.Web.UI.Page
    {
        ProcessMainFucntions main = new ProcessMainFucntions();


        protected void PrintListClick (object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string tmp = button.CommandArgument;
            int idProcess = 0;
            Int32.TryParse(tmp, out idProcess);

            Session["processID"] = idProcess;
            Response.Redirect("PrintList.aspx");
        }

        protected void GetHistoryClick (object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string tmp = button.CommandArgument;
            int idProcess = 0;
            Int32.TryParse(tmp, out idProcess);

            Session["processID"] = idProcess;
            Session["archiveVersion"] = -1;
            Response.Redirect("ProcessHistory.aspx");
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            Users user = main.GetUserById((int)userId);
            Submitters submitter = main.GetSubmitterIdByUsrerId(user.userID);
            List<Processes> allSubmitedProcessses = main.GetProcessesWhereSubmitter(submitter.submitterId);


            Table mainTable = new Table();
            mainTable.CssClass = "table table-striped edm-table";
            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell headerCell1 = new TableHeaderCell();
            headerCell1.Text = "Название процесса";
            headerRow.Cells.Add(headerCell1);

            TableHeaderCell headerCell2 = new TableHeaderCell();
            headerCell2.Text = "Инициатор";
            headerRow.Cells.Add(headerCell2);

            TableHeaderCell headerCell3 = new TableHeaderCell();
            headerCell3.Text = "Дата завершения";
            headerRow.Cells.Add(headerCell3);

            TableHeaderCell headerCell4 = new TableHeaderCell();
            headerCell4.Text = "История";
            headerRow.Cells.Add(headerCell4);

            TableHeaderCell headerCell5 = new TableHeaderCell();
            headerCell5.Text = "Лист согласования";
            headerRow.Cells.Add(headerCell5);


            mainTable.Rows.Add(headerRow);
            foreach (Processes submitterProcess in allSubmitedProcessses)
            {
                TableRow newRow = new TableRow();

                TableCell newCell1 =  new TableCell();
                newCell1.Text = submitterProcess.name;
                newRow.Cells.Add(newCell1);

                TableCell newCell2 = new TableCell();
                newCell2.Text = main.GetUserById(submitterProcess.fk_initiator).name;//submitterProcess.name;
                newRow.Cells.Add(newCell2);

                TableCell newCell3 = new TableCell();
                newCell3.Text = submitterProcess.endDate.ToString();//submitterProcess.name;
                newRow.Cells.Add(newCell3);

                TableCell newCell4 = new TableCell();
                Button historyButton= new Button();
                historyButton.Text = "История";
                historyButton.CommandArgument = submitterProcess.processID.ToString();
                historyButton.Click += GetHistoryClick;
                newCell4.Controls.Add(historyButton);
                newRow.Cells.Add(newCell4);

                TableCell newCell5 = new TableCell();
                Button signListButton = new Button();
                signListButton.Text = "Лист согласования";
                signListButton.CommandArgument = submitterProcess.processID.ToString();
                signListButton.Click += PrintListClick;
                newCell5.Controls.Add(signListButton);
                newRow.Cells.Add(newCell5);

                mainTable.Controls.Add(newRow);
            }

            mainDiv.Controls.Add(mainTable);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class ProcessStarterPage : System.Web.UI.Page
    {
        ProcessMainFucntions main = new ProcessMainFucntions();
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID;
            int.TryParse(userId.ToString(), out userID);
            Users user = main.GetUserById(userID);
            ProcessStarterUser starter = main.GetProcessStarterByUserId(userID);
            Struct structToShow = main.GetStructById(starter.fk_struct);
            structLabel.Text = structToShow.name;

            List<Processes> processesToShow = main.GetAllProcessesInStructWithNoInitiateCreator(structToShow.structID);

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
            headerCell5.Text = "Запустить";
            headerRow.Cells.Add(headerCell5);

            mainTable.Rows.Add(headerRow);

            int i = 0;
            foreach (Processes submitterProcess in processesToShow)
            {
                TableRow newRow = new TableRow();

                TableCell newCell1 = new TableCell();
                newCell1.Text = submitterProcess.name;
                newRow.Cells.Add(newCell1);

                TableCell newCell2 = new TableCell();
                newCell2.Text = main.GetUserById(submitterProcess.fk_initiator).name;//submitterProcess.name;
                newRow.Cells.Add(newCell2);

                TableCell newCell3 = new TableCell();
                newCell3.Text = submitterProcess.endDate.ToString();//submitterProcess.name;
                newRow.Cells.Add(newCell3);

                TableCell newCell4 = new TableCell();
                Button historyButton = new Button();
                historyButton.Text = "История";
                historyButton.CommandArgument = submitterProcess.processID.ToString();
                historyButton.Click += GetHistoryClick;
                newCell4.Controls.Add(historyButton);
                newRow.Cells.Add(newCell4);

                TableCell newCell5 = new TableCell();
                Button signListButton = new Button();
                signListButton.Text = "Запустить";
                signListButton.ID = "startButton" + i;
                signListButton.OnClientClick = "javascript: if (confirm('Вы уверены что хотите запустить процесс согласования?') == true) { showLoadingScreenWithText('Запускаем процесс. Пожалуйста дождитесь завершения!'); } else return false";
                signListButton.CommandArgument = submitterProcess.processID.ToString();
                signListButton.Click += StartProcessClick;
                newCell5.Controls.Add(signListButton);
                newRow.Cells.Add(newCell5);

                mainTable.Controls.Add(newRow);
                i++;
            }

            mainDiv.Controls.Add(mainTable);

            if (!Page.IsPostBack)
            {
                

            }
        }
        protected void StartProcessClick(object sender, EventArgs e)
        {
            EmailFuncs ef = new EmailFuncs();
            Button button = (Button) sender;
            int procId = 0;
            Int32.TryParse(button.CommandArgument, out procId);
            Processes process = main.GetProcessById(procId);
            if (process != null && process.status == -1)
            {
                main.SetProcessStatus(process.processID, 0);
                ef.StartProcess(process.processID);
                Response.Redirect("ProcessStarterPage.aspx");
            }
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert",
                    "<script> alert('Ошибка запуска процесса!');</script>");
        }
        protected void GetHistoryClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string tmp = button.CommandArgument;
            int idProcess = 0;
            Int32.TryParse(tmp, out idProcess);

            Session["processID"] = idProcess;
            Session["archiveVersion"] = -1;
            Response.Redirect("ProcessHistory.aspx");
        }
    }
}
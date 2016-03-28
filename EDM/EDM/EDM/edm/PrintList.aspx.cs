using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class PrintList : System.Web.UI.Page
    {
        EDMdbDataContext _edmDb = new EDMdbDataContext();
        ProcessMainFucntions main = new ProcessMainFucntions();
        protected void Page_Load(object sender, EventArgs e)
        {


            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }






            int processId = (int) HttpContext.Current.Session["processID"];
            ProcessVersions LAstVersion = main.GetLastVersionInProcess(processId);
            List<Steps> lastVersionSteps = main.GetStepsInProcessVersion(LAstVersion.processVersionID);

            Processes proc = main.GetProcessById(processId);

            if (!Page.IsPostBack)
            {
                PrintHistory newHistory = new PrintHistory();
                newHistory.active = true;
                newHistory.fk_user = (int) userID;
                newHistory.fk_process = processId;
                newHistory.date = DateTime.Now;
                _edmDb.PrintHistory.InsertOnSubmit(newHistory);
                _edmDb.SubmitChanges();
            }

            if (proc.fk_template != null)
            {
                int templateId = (int) proc.fk_template;
                ProcessTemplate template = main.GetProcessTemplateById(templateId);
                Table headerTable = new Table();
                headerTable.Width = 800;

                TableRow headerRow1 = new TableRow();
                TableCell headerCell1 = new TableCell();
                headerCell1.HorizontalAlign =HorizontalAlign.Center;
                headerCell1.Text = template.title;
                headerRow1.Cells.Add(headerCell1);
                headerTable.Rows.Add(headerRow1);

                TableRow headerRow2 = new TableRow();
                TableCell headerCell2 = new TableCell();
                headerCell2.HorizontalAlign = HorizontalAlign.Center;
                headerCell2.Text = template.content_;
                headerRow2.Cells.Add(headerCell2);
                headerTable.Rows.Add(headerRow2);

                TemplateHeaderDiv.Controls.Add(headerTable);
            }


            Table newTable = new Table();
            foreach(Steps step in lastVersionSteps)
            {

                TableRow newRow = new TableRow();
                TableCell newCell1 = new TableCell();
                TableCell newCell2 = new TableCell();
                TableCell newCell3 = new TableCell();

                Participants participant = (from a in _edmDb.Participants
                                            where a.participantID == step.fk_participent
                                            select a).FirstOrDefault();

                newCell1.Text = main.GetUserById(participant.fk_user).name;
                newCell2.Text = step.date.ToString().Split(' ')[0];
                newCell3.Text = step.comment;

                newRow.Cells.Add(newCell1);

                newRow.Cells.Add(newCell2);

                newRow.Cells.Add(newCell3);

                newTable.Rows.Add(newRow);

            }
            newTable.Width = 800;
            PrintMainDiv.Controls.Add(newTable);


        }
    }
}
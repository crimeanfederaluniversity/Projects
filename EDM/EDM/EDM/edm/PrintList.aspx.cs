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
        OtherFuncs of = new OtherFuncs();
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

            var documentsInProc = (from a in _edmDb.ProcVersionDocsMap
                where a.active && a.fk_processVersion == of.GetProcMaxVersion(processId)
                select a.fk_documents).ToList();

            foreach (var doc in documentsInProc)
            {
                idDocLabel.Text += "ДОКУМЕНТ № " + doc + " ( md5: " + _edmDb.Documents.Where(d => d.documentID == doc).Select(h => h.md5).FirstOrDefault() + " ) "+ "<br/>";
            }


            Processes proc = main.GetProcessById(processId);

            processNameLabel.Text = proc.name;
            initiatorStructLabel.Text ="Проект подготовлен: " + main.GetStructById((int)main.GetUserById((int) userID).fk_struct).name ;
            initiatorNameLabel.Text = "Исполнитель: " + main.GetUserById((int) userID).name;
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

            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableHeaderCell() { Text = "№ п/п" });
            headerRow.Cells.Add(new TableHeaderCell() { Text = "Должность" });
            headerRow.Cells.Add(new TableHeaderCell() { Text = "Ф.И.О." });
            headerRow.Cells.Add(new TableHeaderCell() { Text = "Замечания" });
            headerRow.Cells.Add(new TableHeaderCell() { Text = "Подпись" });
            newTable.Rows.Add(headerRow);
            int i = 1;
            foreach (Steps step in lastVersionSteps)
            {

                TableRow newRow = new TableRow();
                newRow.Cells.Add(new TableCell() {Text = i.ToString() + "."});
                Participants participant = (from a in _edmDb.Participants
                                            where a.participantID == step.fk_participent
                                            select a).FirstOrDefault();
                newRow.Cells.Add(new TableCell() { Text = main.GetUserById(participant.fk_user).@struct});
                newRow.Cells.Add(new TableCell() {Text = main.GetUserById(participant.fk_user).name});
                if (step.commentType == 2)
                {
                    newRow.Cells.Add(new TableCell() {Text = step.comment});
                }
                else
                {
                    newRow.Cells.Add(new TableCell());
                }
                newRow.Cells.Add(new TableCell() { Text = "электронная подпись "+ main.GetUserById(participant.fk_user).email });
                //  newCell2.Text = step.date.ToString().Split(' ')[0];
                newTable.Rows.Add(newRow);
                i++;
            }
            newTable.Width = 800;
            mainDiv.Controls.Add(newTable);


        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class ProcessHistory : System.Web.UI.Page
    {
        ProcessMainFucntions _main = new ProcessMainFucntions();
        public TableCell DocumentsInVersion(int processVersionId,int processId)
        {
            List<Documents> documentsInVersion = _main.GetDocumentsInProcessVersion(processVersionId, false);
            TableCell documentsInVersionCell = new TableCell();
            if (documentsInVersion.Count > 0)
            {
                Table docTable = new Table();
                docTable.Style.Add("height","100%");
                docTable.Style.Add("width", "100%");
                foreach (Documents document in documentsInVersion)
                {
                    TableRow docTableRow = new TableRow();
                    TableCell docLinkCell = new TableCell();
                    TableCell docCommentTableCell = new TableCell();
                    docCommentTableCell.Text = document.documentComment;
                    LinkButton newLinkButton = new LinkButton();

                    // newLinkButton.LinkButtonToDocument.ID = "linkButton" + currentDocument.documentID;
                    newLinkButton.CommandArgument = processId + "/" + document.documentID;
                    ProcessEdit proc = new ProcessEdit();
                    newLinkButton.Click += proc.GetDocumentClick;


                    newLinkButton.Text = document.documentName;
                    docLinkCell.Controls.Add(newLinkButton);
                    docTableRow.Cells.Add(docLinkCell);
                    docTableRow.Cells.Add(docCommentTableCell);
                    docTable.Rows.Add(docTableRow);
                }
                documentsInVersionCell.Controls.Add(docTable);
            }
            else
            {
                documentsInVersionCell.Text = "Документов нет";
            }
            return documentsInVersionCell;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int processId = -1;
            var procIdStr = HttpContext.Current.Session["processID"];
            if (procIdStr == null)
                Response.Redirect("Dashboard.aspx");
            Int32.TryParse(procIdStr.ToString(), out processId);
            if (processId < 0)
                Response.Redirect("Dashboard.aspx");

            Processes currentProcess = _main.GetProcessById(processId);

            ProcessNameLabel.Text = currentProcess.name;
            ProcessTypeLabel.Text = _main.GetProcessTypeNameByType(currentProcess.type);
            StartDateLebel.Text = currentProcess.startDate.ToString();
            EndDateLabel.Text = currentProcess.endDate.ToString();
            InitiatorLabel.Text = _main.GetUserById(currentProcess.fk_initiator).name;

            Table historyTable = new Table();

            #region headers

            TableHeaderRow historyTableHeaderRow = new TableHeaderRow();
            TableCell cell1 = new TableCell();
            cell1.Text = "Версия процесса";
            historyTableHeaderRow.Cells.Add(cell1);

            TableCell cell2 = new TableCell();
            cell2.Text = "Комментарий к версии процесса";
            historyTableHeaderRow.Cells.Add(cell2);

            TableCell cell3 = new TableCell();
            cell3.Text = "Статус";
            historyTableHeaderRow.Cells.Add(cell3);

            TableCell cell4 = new TableCell();
            cell4.Text = "Документы";
            historyTableHeaderRow.Cells.Add(cell4);

            TableCell cell5 = new TableCell();
            cell5.Text = "Пользователь";
            historyTableHeaderRow.Cells.Add(cell5);

            TableCell cell6 = new TableCell();
            cell6.Text = "Дата";
            historyTableHeaderRow.Cells.Add(cell6);

            TableCell cell7 = new TableCell();
            cell7.Text = "Результат";
            historyTableHeaderRow.Cells.Add(cell7);

            TableCell cell8 = new TableCell();
            cell8.Text = "Комментарий";
            historyTableHeaderRow.Cells.Add(cell8);

            historyTable.Rows.Add(historyTableHeaderRow);

            #endregion

            List<ProcessVersions> versionsInProcess = _main.GetProcessVersionsInProcess(processId);
            foreach (ProcessVersions currentVersion in versionsInProcess)
            {
                TableRow processVersionRow = new TableRow();

                
                List<Steps> stepsInVersion = _main.GetStepsInProcessVersion(currentVersion.processVersionID);
                int stepsCount = stepsInVersion.Count;

                TableCell versionIdCell = new TableCell();
                versionIdCell.Text = currentVersion.version.ToString();
                versionIdCell.RowSpan = stepsCount;
                processVersionRow.Cells.Add(versionIdCell);

                TableCell versionComment = new TableCell();
                versionComment.Text = currentVersion.comment.ToString();
                versionComment.RowSpan = stepsCount;
                processVersionRow.Cells.Add(versionComment);

                TableCell versionStatus = new TableCell();
                versionStatus.Text = currentVersion.status.ToString();
                versionStatus.RowSpan = stepsCount;
                processVersionRow.Cells.Add(versionStatus);

                TableCell documentsCell = DocumentsInVersion(currentVersion.processVersionID,processId);
                documentsCell.RowSpan = stepsCount;
                processVersionRow.Cells.Add(documentsCell);


                if (stepsCount == 0)
                {
                    processVersionRow.Cells.Add(new TableCell());
                    processVersionRow.Cells.Add(new TableCell());
                    processVersionRow.Cells.Add(new TableCell());
                    processVersionRow.Cells.Add(new TableCell());
                    historyTable.Rows.Add(processVersionRow);
                }

                bool firstStep = true;
                foreach (Steps step in stepsInVersion)
                {
                    TableRow processVersionStepRow = new TableRow();
                    if (firstStep)
                    {
                        firstStep = false;
                        processVersionStepRow = processVersionRow;
                    }

                    Participants participant = _main.GetParticipantById(step.fk_participent);

                    TableCell stepUserCell = new TableCell();
                    stepUserCell.Text = _main.GetUserById(participant.fk_user).name;
                    processVersionStepRow.Cells.Add(stepUserCell);

                    TableCell stepDateCell = new TableCell();
                    stepDateCell.Text = step.date.ToString();
                    processVersionStepRow.Cells.Add(stepDateCell);

                    TableCell stepResultCell = new TableCell();
                    stepResultCell.Text = step.stepResult.ToString();
                    processVersionStepRow.Cells.Add(stepResultCell);

                    TableCell stepCommentCell = new TableCell();
                    stepCommentCell.Text = step.comment.ToString();
                    processVersionStepRow.Cells.Add(stepCommentCell);

                    historyTable.Rows.Add(processVersionStepRow);
                }


                
            }
            historyTable.BorderWidth = 1;
            
            historyTableDiv.Controls.Add(historyTable);
        }
    }
}
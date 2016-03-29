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
                    docCommentTableCell.Text = _main.GetDocumentComment(document.documentID, processVersionId);// document.documentComment;
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
            int archiveVersion = -1;

            var procIdStr = HttpContext.Current.Session["processID"];
            if (procIdStr == null)
                Response.Redirect("Dashboard.aspx");
            Int32.TryParse(procIdStr.ToString(), out processId);
            int.TryParse(HttpContext.Current.Session["archiveVersion"].ToString(), out archiveVersion);
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
            TableHeaderCell cell1 = new TableHeaderCell();
            cell1.Text = "Версия процесса";
            historyTableHeaderRow.Cells.Add(cell1);

            TableHeaderCell cell2 = new TableHeaderCell();
            cell2.Text = "Комментарий к версии процесса";
            historyTableHeaderRow.Cells.Add(cell2);

            TableHeaderCell cell3 = new TableHeaderCell();
            cell3.Text = "Статус";
            historyTableHeaderRow.Cells.Add(cell3);

            TableHeaderCell cell4 = new TableHeaderCell();
            cell4.Text = "Документы";
            historyTableHeaderRow.Cells.Add(cell4);

            TableHeaderCell cell5 = new TableHeaderCell();
            cell5.Text = "Пользователь";
            historyTableHeaderRow.Cells.Add(cell5);

            TableHeaderCell cell6 = new TableHeaderCell();
            cell6.Text = "Дата";
            historyTableHeaderRow.Cells.Add(cell6);

            TableHeaderCell cell7 = new TableHeaderCell();
            cell7.Text = "Срок";
            historyTableHeaderRow.Cells.Add(cell7);

            TableHeaderCell cell8 = new TableHeaderCell();
            cell8.Text = "Результат";
            historyTableHeaderRow.Cells.Add(cell8);

            TableHeaderCell cell9 = new TableHeaderCell();
            cell9.Text = "Комментарий";
            historyTableHeaderRow.Cells.Add(cell9);

            TableHeaderCell cell10 = new TableHeaderCell();
            cell10.Text = "Прикрепленный файл";
            historyTableHeaderRow.Cells.Add(cell10);

            historyTable.Rows.Add(historyTableHeaderRow);

            #endregion
            List<ProcessVersions> versionsInProcess = new List<ProcessVersions>();

            if (archiveVersion != -1)
            {
                EDMdbDataContext dc = new EDMdbDataContext();

                versionsInProcess = (from a in dc.ProcessVersions
                                     where a.active && a.fk_process == processId && a.version == archiveVersion
                                     select a).ToList();
            }
            else
            versionsInProcess = _main.GetProcessVersionsInProcess(processId);
            ProcessVersions lastVersion =_main.GetProcessVersionsLastVerson(processId);
            //ProcessVersions lastVersion = versionsInProcess[versionsInProcess.Count - 1];
            foreach (ProcessVersions currentVersion in versionsInProcess)
            {
                TableRow processVersionRow = new TableRow();

                
                List<Steps> stepsInVersion = _main.GetStepsInProcessVersion(currentVersion.processVersionID);
                int stepsCount = stepsInVersion.Count;
                int rowSpanCount = 0;
                List<Participants> participantsWithoutStep = new List<Participants>();
                if (lastVersion == currentVersion)
                {
                     participantsWithoutStep = _main.GetParticipantsInProcessWithNoStep(processId,
                        lastVersion.processVersionID);
                    rowSpanCount = stepsCount + participantsWithoutStep.Count;
                }

                

                TableCell versionIdCell = new TableCell();
                versionIdCell.Text = currentVersion.version.ToString();
                versionIdCell.RowSpan = rowSpanCount;
                processVersionRow.Cells.Add(versionIdCell);

                TableCell versionComment = new TableCell();
                versionComment.Text = currentVersion.comment.ToString();
                versionComment.RowSpan = rowSpanCount;
                processVersionRow.Cells.Add(versionComment);

                TableCell versionStatus = new TableCell();
                versionStatus.Text = currentVersion.status.ToString();
                versionStatus.RowSpan = rowSpanCount;
                processVersionRow.Cells.Add(versionStatus);

                TableCell documentsCell = DocumentsInVersion(currentVersion.processVersionID,processId);
                documentsCell.RowSpan = rowSpanCount;
                processVersionRow.Cells.Add(documentsCell);


                if (rowSpanCount == 0)
                {
                    processVersionRow.Cells.Add(new TableCell());
                    processVersionRow.Cells.Add(new TableCell());
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

                    TableCell stepEndDateCell = new TableCell();
                    stepEndDateCell.Text = participant.dateEnd.ToString();//step.date.ToString();
                    processVersionStepRow.Cells.Add(stepEndDateCell);

                    TableCell stepResultCell = new TableCell();
                    stepResultCell.Text = step.stepResult.ToString();
                    if (step.stepResult == -2)
                    {
                        stepResultCell.Text = "Отказано";
                    }

                    if (step.stepResult == 1)
                    {
                        stepResultCell.Text = "Согласовано";
                    }



                    processVersionStepRow.Cells.Add(stepResultCell);
                    TableCell stepCommentCell = new TableCell();
                    stepCommentCell.Text = step.comment.ToString();
                    processVersionStepRow.Cells.Add(stepCommentCell);

                    DocumentsInStep doc = _main.GetDocumentInStep(step.stepID);
                    TableCell linkButtonCell = new TableCell();
                    if (doc != null)
                    {

                        LinkButton linkButton = new LinkButton();
                        linkButton.Text = doc.documentName;
                        StepDocInStepMap tmp = _main.GetFirstStepDocMapByDoc(doc.documentsInStepId);
                        Steps tmpStep = _main.GetStepById(tmp.fk_step);
                        ProcessVersions version = _main.GetProcessversionById(tmpStep.fk_processVersion);
                        


                        linkButton.CommandArgument = version.fk_process + "stepsDocs/" +
                                                     doc.documentsInStepId.ToString();
                        linkButton.Click += GetDocumentClick;
                        linkButtonCell.Controls.Add(linkButton);


                    }
                    else
                    {
                        linkButtonCell.Text = "Документов нет";
                    }
                    processVersionStepRow.Cells.Add(linkButtonCell);

                    historyTable.Rows.Add(processVersionStepRow);
                }

                foreach (Participants part in participantsWithoutStep)
                {
                    TableRow processVersionStepRow = new TableRow();

                    if (stepsCount == 0 && firstStep)
                    {
                        firstStep = false;
                        processVersionStepRow = processVersionRow;
                    }

                    TableCell stepUserCell = new TableCell();
                    stepUserCell.Text = _main.GetUserById(part.fk_user).name;
                    processVersionStepRow.Cells.Add(stepUserCell);

                    TableCell stepDateCell = new TableCell();
                    processVersionStepRow.Cells.Add(stepDateCell);

                    TableCell stepEndDateCell = new TableCell();
                    stepEndDateCell.Text = part.dateEnd.ToString();
                    processVersionStepRow.Cells.Add(stepEndDateCell);

                    TableCell stepResultCell = new TableCell();
                    stepResultCell.Text = "Не просмотрено";
                    processVersionStepRow.Cells.Add(stepResultCell);

                    TableCell stepCommentCell = new TableCell();
                    processVersionStepRow.Cells.Add(stepCommentCell);

                    TableCell stepDocCell = new TableCell();
                    processVersionStepRow.Cells.Add(stepDocCell);

                    historyTable.Rows.Add(processVersionStepRow);
                }

            }
            historyTable.BorderWidth = 1;
            historyTable.CssClass = "table edm-table edm-history-table centered-block";
            
            historyTableDiv.Controls.Add(historyTable);
        }
        public void GetDocumentClick(object sender, EventArgs e)
        {
            LinkButton thisButton = (LinkButton)sender;
            string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + thisButton.CommandArgument + "/" + thisButton.Text);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + thisButton.Text + ";");
            response.TransmitFile(path);
            response.Flush();
            response.End();
        }
        protected void goBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}
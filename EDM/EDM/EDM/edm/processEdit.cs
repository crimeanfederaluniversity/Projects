using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public class ProcessMainFucntions
    {
        EDMdbDataContext _edmDb = new EDMdbDataContext();
        public List<TextBox> ExistingParticipants = new List<TextBox>();
        public List<ProcessEdit.Participant> ParticipantsList = new List<ProcessEdit.Participant>();
        #region create

        
        public void CreateNewStepDocumentConnection(int stepId, int docId)
        {
            StepDocInStepMap newstepDocMap = new StepDocInStepMap();
            newstepDocMap.active = true;
            newstepDocMap.fk_step = stepId;
            newstepDocMap.fk_documentInStep = docId;
            _edmDb.StepDocInStepMap.InsertOnSubmit(newstepDocMap);
            _edmDb.SubmitChanges();
        }
        public int CreateNewProcessVersion(int processId, string comment, int version, string status)
        {
            Processes currentProcess = GetProcessById(processId);
            currentProcess.status = -1;

            ProcessVersions newVersion = new ProcessVersions();
            newVersion.active = true;
            newVersion.comment = comment;
            newVersion.fk_process = processId;
            newVersion.version = version;
            newVersion.status = status;
            _edmDb.ProcessVersions.InsertOnSubmit(newVersion);
            _edmDb.SubmitChanges();
            return newVersion.processVersionID;
        }
        public int CreateNewDocument(string docName, int processVersion, string docComment)
        {
            Documents newDocument = new Documents();
            newDocument.active = true;
            newDocument.documentName = docName;
            _edmDb.Documents.InsertOnSubmit(newDocument);
            _edmDb.SubmitChanges();

            ProcVersionDocsMap newDocsMap = new ProcVersionDocsMap();
            newDocsMap.active = true;
            newDocsMap.fk_processVersion = processVersion;
            
            newDocsMap.documentComment = docComment;
            newDocsMap.fk_documents = newDocument.documentID;
            _edmDb.ProcVersionDocsMap.InsertOnSubmit(newDocsMap);
            _edmDb.SubmitChanges();

            return newDocument.documentID;
        }
        public int CreateNewStepDocument(string docName, int stepId, string docComment)
        {
            DocumentsInStep newDocument = new DocumentsInStep();
            newDocument.active = true;
            newDocument.documentName = docName;
            //newDocument.fk_step = stepId;
            newDocument.documentComment = docComment;
            _edmDb.DocumentsInStep.InsertOnSubmit(newDocument);
            _edmDb.SubmitChanges();

            StepDocInStepMap newstepDocMap = new StepDocInStepMap();
            newstepDocMap.active = true;
            newstepDocMap.fk_step = stepId;
            newstepDocMap.fk_documentInStep = newDocument.documentsInStepId;
            _edmDb.StepDocInStepMap.InsertOnSubmit(newstepDocMap);
            _edmDb.SubmitChanges();

            return newDocument.documentsInStepId;
        }
        public int CreateChildProcess(int parentProcessId)
        {

            int userId;
            int.TryParse(HttpContext.Current.Session["userID"].ToString(), out userId);


            Processes parentProcess = GetProcessById(parentProcessId);
            int childProcessId = CreateProcessByType(parentProcess.type, userId, parentProcess.name,/* parentProcess.fk_template*/ null, null, parentProcess.fk_processCharacter);
            Processes childProcess = GetProcessById(childProcessId);
            childProcess.fk_parentProcess = parentProcessId;
            ProcessVersions parentProcessLastVersion = GetProcessVersionsLastVerson(parentProcessId);
            int childProcVersion = CreateNewProcessVersion(childProcessId, parentProcessLastVersion.comment, 0, "");

            List<Documents> parentDocuments = GetDocumentsInProcessVersion(parentProcessLastVersion.processVersionID, false);

            foreach (Documents currentDoc in parentDocuments)
            {
                ProcVersionDocsMap newMap = new ProcVersionDocsMap();
                newMap.active = true;
                newMap.fk_documents = currentDoc.documentID;
                newMap.fk_processVersion = childProcVersion;
                newMap.documentComment = GetDocumentComment(currentDoc.documentID, childProcVersion);
                _edmDb.ProcVersionDocsMap.InsertOnSubmit(newMap);

            }
            _edmDb.SubmitChanges();

            return childProcess.processID;
        }
        public int CreateProcessByType(string type, int userId, string name, int? templateId, int? submitter , int? processCharacterid)
        {
            Processes newProcess = new Processes();
            newProcess.fk_submitter = submitter;
            newProcess.fk_processCharacter = processCharacterid;
            newProcess.active = true;
            newProcess.fk_initiator = userId;
            newProcess.type = type;
            newProcess.name = name;
            newProcess.fk_template = templateId;
            newProcess.status = -1;
            _edmDb.Processes.InsertOnSubmit(newProcess);
            _edmDb.SubmitChanges();
            return newProcess.processID;
        }
        public int CreateNewTemplateParticipent(int templateId, int userId, int queue)
        {
            ProcessTemplateParticipant newParticipant = new ProcessTemplateParticipant();
            newParticipant.active = true;
            newParticipant.fk_user = userId;
            newParticipant.fk_template = templateId;
            newParticipant.queue = queue;

            _edmDb.ProcessTemplateParticipant.InsertOnSubmit(newParticipant);
            _edmDb.SubmitChanges();
            return newParticipant.processTemplateParticipantId;

        }
        public int CreateNewParticipent(int processId, int userId, int queue, DateTime? endDate)
        {
            Participants newParticipant = new Participants();
            newParticipant.active = true;
            newParticipant.fk_user = userId;
            newParticipant.dateStart = DateTime.Now;
            newParticipant.dateEnd = endDate;
            newParticipant.status = 0;
            newParticipant.fk_process = processId;
            newParticipant.queue = queue;
            newParticipant.isNew = true; // 


            _edmDb.Participants.InsertOnSubmit(newParticipant);
            _edmDb.SubmitChanges();
            return newParticipant.participantID;

        }
        #endregion
        #region kill
        public void KillDocument(int docId, int versionId)
        {
            /*Documents docToKill =
                (from a in _edmDb.Documents where a.documentID == docId select a).FirstOrDefault();
            docToKill.active = false;*/
            ProcVersionDocsMap procVersionToKill = (from a in _edmDb.ProcVersionDocsMap
                                                    where a.fk_processVersion == versionId
                                                          && a.fk_documents == docId
                                                    select a).FirstOrDefault();
            procVersionToKill.active = false;
            _edmDb.SubmitChanges();
        }
        public void KillParticipant(int participantId)
        {
            Participants participantToKill =
                (from a in _edmDb.Participants where a.participantID == participantId select a).FirstOrDefault();
            participantToKill.active = false;
            _edmDb.SubmitChanges();
        }
        public void KillParticipantTemplate(int participantTemplateId)
        {
            ProcessTemplateParticipant participantToKill =
                (from a in _edmDb.ProcessTemplateParticipant where a.processTemplateParticipantId == participantTemplateId select a).FirstOrDefault();
            participantToKill.active = false;
            _edmDb.SubmitChanges();
        }
        #endregion
        #region get

        public string GetOnlyIONameById(int userId)
        {
            Users user = GetUserById(userId);
            string name = user.name.Trim();
            List<string> tmp = name.Split(' ').ToList();
            tmp[0] = "";
            name=String.Join(" ",tmp).Trim();
            return name;
        }

        public string GetShortFIONameById(int userId)
        {
            Users user = GetUserById(userId);
            string name = user.name.Trim();
            List<string> tmp = name.Split(' ').ToList();
            for (int i = 1; i < tmp.Count; i++)
            {
                tmp[i] = tmp[i].Trim()[0] + ".";
            }
            name = String.Join(" ", tmp).Trim();
            return name;
        }
        public ProcessCharacter GetProcessCharacterById(int procCharId)
        {
            return (from a in _edmDb.ProcessCharacter
                    where a.processCharacterId == procCharId
                    select a).FirstOrDefault();
        }
        public List<ProcessCharacter> GetProcessCharactersList()
        {
            return (from a in _edmDb.ProcessCharacter
                where a.active == true
                select a).ToList();
        } 
        public void GetDocumentClick(object sender, EventArgs e)
        {
            LinkButton thisButton = (LinkButton)sender;
           // string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + thisButton.CommandArgument + "/" + thisButton.Text);
            DownloadStepFile(Convert.ToInt32(thisButton.CommandArgument));
        }
        public TableCell GetDocumentsInVersion(int processVersionId, int processId)
        {
            List<Documents> documentsInVersion = GetDocumentsInProcessVersion(processVersionId, false);
            TableCell documentsInVersionCell = new TableCell();
            if (documentsInVersion.Count > 0)
            {
                Table docTable = new Table();
                docTable.Style.Add("height", "100%");
                docTable.Style.Add("width", "100%");
                foreach (Documents document in documentsInVersion)
                {
                    TableRow docTableRow = new TableRow();
                    TableCell docLinkCell = new TableCell();
                    TableCell docCommentTableCell = new TableCell();
                    docCommentTableCell.Text = GetDocumentComment(document.documentID, processVersionId);// document.documentComment;
                    LinkButton newLinkButton = new LinkButton();

                    newLinkButton.Font.Bold = true;
                    // newLinkButton.LinkButtonToDocument.ID = "linkButton" + currentDocument.documentID;
                    newLinkButton.CommandArgument =  document.documentID.ToString();
                    ProcessEdit proc = new ProcessEdit();
                    newLinkButton.Click += proc.GetDocumentClick;


                    newLinkButton.Text = document.documentName;
                    docLinkCell.Controls.Add(newLinkButton);
                    docLinkCell.Controls.Add(new Label()
                    {
                        Text = "<br/>" + document.md5,
                        ForeColor = Color.Green
                    });

                    docLinkCell.Controls.Add(new Label()
                    {
                        Text = "<br/>" + "(" + _edmDb.Users.Where(u => u.userID == document.userDownload && u.active).Select(u => u.name).FirstOrDefault() + ")",
                        ForeColor = Color.Black
                    });
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
        public Table GetHistoryTable(int archiveVersion, int processId)
        {
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
                versionsInProcess = GetProcessVersionsInProcess(processId);
            ProcessVersions lastVersion = GetProcessVersionsLastVerson(processId);
            foreach (ProcessVersions currentVersion in versionsInProcess)
            {
                TableRow processVersionRow = new TableRow();
                List<Steps> stepsInVersion = GetStepsInProcessVersion(currentVersion.processVersionID);
                int stepsCount = stepsInVersion.Count;
                int rowSpanCount = 0;
                List<Participants> participantsWithoutStep = new List<Participants>();
                if (lastVersion == currentVersion)
                {
                    participantsWithoutStep = GetParticipantsInProcessWithNoStep(processId,
                        lastVersion.processVersionID);
                    rowSpanCount = stepsCount + participantsWithoutStep.Count;
                }
                else
                {
                    rowSpanCount = stepsCount;
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

                TableCell documentsCell = GetDocumentsInVersion(currentVersion.processVersionID, processId);
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

                    Participants participant = GetParticipantById(step.fk_participent);

                    TableCell stepUserCell = new TableCell();
                    stepUserCell.Text = GetUserById(participant.fk_user).name;
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

                    DocumentsInStep doc = GetDocumentInStep(step.stepID);
                    TableCell linkButtonCell = new TableCell();
                    if (doc != null)
                    {

                        LinkButton linkButton = new LinkButton();
                        linkButton.Text = doc.documentName;
                        StepDocInStepMap tmp = GetFirstStepDocMapByDoc(doc.documentsInStepId);
                        Steps tmpStep = GetStepById(tmp.fk_step);
                        ProcessVersions version = GetProcessversionById(tmpStep.fk_processVersion);



                        linkButton.CommandArgument = doc.documentsInStepId.ToString(); /*version.fk_process + "stepsDocs/" +
                                                     doc.documentsInStepId.ToString();*/
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
                    stepUserCell.Text = GetUserById(part.fk_user).name;
                    processVersionStepRow.Cells.Add(stepUserCell);

                    TableCell stepDateCell = new TableCell();
                    processVersionStepRow.Cells.Add(stepDateCell);

                    TableCell stepEndDateCell = new TableCell();
                    stepEndDateCell.Text = part.dateEnd.ToString();
                    processVersionStepRow.Cells.Add(stepEndDateCell);

                    TableCell stepResultCell = new TableCell();
                    stepResultCell.Text = (part.isNew == true) ? "Не заходил" : "Просмотрел";


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
            return historyTable;
        }
        public List<Processes> GetAllProcessesInStructWithNoInitiateCreator(int structId)
        {
             OtherFuncs other = new OtherFuncs();
             List<int> usersIds = new List<int>(){structId};
             List<int> users = other.GetSlaves(structId, ref usersIds);
            //List<Users> user = GetRecursiveChildAllStructList(structId, false);
            //List<int> users = (from a in user select a.userID).ToList();
            List <Processes> processesToShow = (from a in _edmDb.Processes
                where a.active == true
                      && a.status == -1
                join b in _edmDb.Users
                    on a.fk_initiator equals b.userID
                where b.active == true
                      && b.canInitiate == false
                      && users.Contains(b.userID)
                select a).Distinct().ToList();
            return processesToShow;
        }
        public ProcessStarterUser GetProcessStarterByUserId(int userId )
        {
            return (from a in _edmDb.ProcessStarterUser
                where a.active == true
                      && a.fk_user == userId
                select a).FirstOrDefault();
        }
        public ProcessVersions GetProcessversionById(int versionId )
        {
            return (from a in _edmDb.ProcessVersions
                where a.processVersionID == versionId
                select a).FirstOrDefault();
        }
        public Steps GetStepById(int stepId)
        {
            return (from a in _edmDb.Steps
                where a.stepID == stepId
                select a).FirstOrDefault();
        }
        public StepDocInStepMap GetFirstStepDocMapByDoc(int docId)
        {
            return (from a in _edmDb.StepDocInStepMap
                where a.fk_documentInStep == docId
                select a).FirstOrDefault();

        }
        public Processes GetChildProcess(int procId,int initiatorId)
        {
            return (from a in _edmDb.Processes
                where a.fk_parentProcess == procId
                && a.fk_initiator == initiatorId
                      && a.active == true
                select a).FirstOrDefault();
        }
        public Submitters GetSubmitterIdByUsrerId(int userId)
        {
            return (from a in _edmDb.Submitters where a.fk_user == userId select a).FirstOrDefault();
        }
        public List<Processes> GetProcessesWhereSubmitter(int submitterId )
        {
            List<Processes> returnList = (from a in _edmDb.Processes
                where a.active == true
                      && a.fk_submitter == submitterId
                      && a.status == 10
                select a).ToList();
            return returnList;
        } 
        public Panel GetPanelByProcessPanel(int processId, string processType)
        {
            Panel panelToReturn = new Panel();

            Label commentNameLabel = new Label();
            commentNameLabel.Text = "Комментарий";
            panelToReturn.Controls.Add(commentNameLabel);

            TextBox commentTextBox = new TextBox();
            commentTextBox.TextMode = TextBoxMode.MultiLine;
            commentTextBox.Width = 800;
            commentTextBox.Height = 100;

            Table participantsTable = new Table();
            if (ExistingParticipants.Count == 0)
            {
                TableRow participantRow = new TableRow();

                TableCell participantIdCell = new TableCell();
                Label participantIdLabel = new Label();
                participantIdLabel.Text = 1.ToString();
                participantIdCell.Controls.Add(participantIdLabel);
                participantRow.Cells.Add(participantIdCell);

                TableCell participantChooseCell = new TableCell();
                TextBox participantChooseTextBox = new TextBox();
                participantIdCell.Controls.Add(participantChooseTextBox);
                participantRow.Cells.Add(participantChooseCell);
                ExistingParticipants.Add(participantChooseTextBox);

                participantsTable.Rows.Add(participantRow);
            }

            if (processId != 0) // edit
            {
                Processes currentProcess = GetProcessById(processId);
                ProcessVersions currentProcessVersion = GetProcessVersionsLastVerson(processId);
                commentTextBox.Text = currentProcessVersion.comment;
            }
            else
            {

            }

            Button addParticipantButton = new Button();
            addParticipantButton.Text = "Добавить согласующего";
            addParticipantButton.Click += AddParticipantClick;

            //ImageButton addParticipantImageButton

            panelToReturn.Controls.Add(commentTextBox);
            panelToReturn.Controls.Add(participantsTable);
            panelToReturn.Controls.Add(addParticipantButton);
            return panelToReturn;
        }
        public string GetProcessTypeNameByType(string type)
        {
            switch (type)
            {
                case "parallel": return "Параллельное согласование";
                case "serial": return "Последовательное согласование";
                case "review": return "Рецензия";
                default: return "";
            }
        }
        public ProcessVersions GetProcessVersionsLastVerson(int processId)
        {
            return (from a in _edmDb.ProcessVersions
                    where a.active == true
                          && a.fk_process == processId
                    select a).OrderByDescending(mc => mc.version).FirstOrDefault();
        }
        public Processes GetProcessById(int processId)
        {
            return (from a in _edmDb.Processes where a.processID == processId select a).FirstOrDefault();
        }
        public string GetProcessNameById(int processId)
        {
            Processes currentProcess = (from a in _edmDb.Processes
                                        where a.processID == processId
                                        select a).FirstOrDefault();
            if (currentProcess == null)
                return "";
            return currentProcess.name;
        }
        public List<ProcessEdit.Participant> GetParticipantsListInProcess(int processId)
        {
            List<ProcessEdit.Participant> listToReturn = new List<ProcessEdit.Participant>();

            List<Participants> participants =
                (from a in _edmDb.Participants where a.active == true && a.fk_process == processId select a).OrderBy(
                    mc => mc.queue).ToList();
            foreach (Participants participant in participants)
            {
                ProcessEdit.Participant newParticipant = new ProcessEdit.Participant();
                newParticipant.ParticipantEndDateTextBox = new TextBox();
                newParticipant.ParticipantEndDateTextBox.Text = participant.dateEnd.ToString();
                newParticipant.ParticipantQueueTextBox = new TextBox();
                newParticipant.ParticipantQueueTextBox.Text = participant.queue.ToString();
                newParticipant.ParticipantTextBox = new TextBox();
                newParticipant.ParticipantTextBox.Text = participant.fk_user.ToString();
                newParticipant.ParticipantId = participant.participantID;
                listToReturn.Add(newParticipant);
            }

            return listToReturn;
        }
        public ListItem[] GetAllStructToDropDown(int checkedStruct)
        {
            List<Struct> allStruct = (from a in _edmDb.Struct
                where a.active == true
                select a).ToList();
            ListItem[] itemsToReturn = new ListItem[allStruct.Count];
            int i = 0;
            foreach (Struct curStruct in allStruct)
            {
                ListItem newItem = new ListItem();
                newItem.Text = curStruct.name;
                newItem.Value = curStruct.structID.ToString();
                if (curStruct.structID == checkedStruct)
                    newItem.Selected = true;
                itemsToReturn[i] = newItem;
                i++;
            }
            return itemsToReturn;
        }
        public List<ProcessEdit.Participant> GetParticipantsInTempolate(int templateId)
        {

            List<ProcessEdit.Participant> listToReturn = new List<ProcessEdit.Participant>();

            List<ProcessTemplateParticipant> templateParticipants =
                (from a in _edmDb.ProcessTemplateParticipant where a.active == true && a.fk_template == templateId select a).OrderBy(
                    mc => mc.queue).ToList();
            foreach (ProcessTemplateParticipant templateParticipant in templateParticipants)
            {
                ProcessEdit.Participant newParticipant = new ProcessEdit.Participant();
                newParticipant.ParticipantQueueTextBox = new TextBox();
                newParticipant.ParticipantQueueTextBox.Text = templateParticipant.queue.ToString();
                newParticipant.ParticipantTextBox = new TextBox();
                newParticipant.ParticipantTextBox.Text = templateParticipant.fk_user.ToString();
                newParticipant.ParticipantId = templateParticipant.processTemplateParticipantId;
                listToReturn.Add(newParticipant);
            }

            return listToReturn;
        }
        public List<Participants> GetParticipantsInProcessWithNoStep(int processId, int processLastVersion)
        {
            List<Participants> participantsWithStep = (from a in _edmDb.Participants
                join b in _edmDb.Steps
                    on a.participantID equals b.fk_participent
                where a.active == true
                      && b.active == true
                      && b.fk_processVersion == processLastVersion
                select a).Distinct().ToList();
            List<Participants> allParticipantsInVersion = (from a in _edmDb.Participants
                where a.fk_process == processId
                where a.active == true
                select a).ToList();
            List<Participants> participantsToReturn = new List<Participants>();

            foreach (Participants participant in allParticipantsInVersion)
            {
                if (participantsWithStep.Contains(participant))
                {
                    
                }
                else
                {
                    participantsToReturn.Add(participant);
                }
            }
            return participantsToReturn;
        }
        public List<Participants> GetParticipantsInProcess(int processId)
        {
            // List <ProcessEdit.Participant> listToReturn = new List<ProcessEdit.Participant>();

            List<Participants> participants =
                (from a in _edmDb.Participants where a.active == true && a.fk_process == processId select a).OrderBy(
                    mc => mc.queue).ToList();
            return participants;



        }
        public ProcessVersions GetLastVersionInProcess(int processId)
        {
            return
                (from a in _edmDb.ProcessVersions where a.active == true && a.fk_process == processId select a)
                    .OrderByDescending(mc => mc.version).FirstOrDefault();
        }
        public Participants GetParticipantById(int participantId)
        {
            return (from a in _edmDb.Participants where a.participantID == participantId select a).FirstOrDefault();
        }
        public List<Documents> GetDocumentsInProcessVersion(int processVersionId, bool onlyActive)
        {
            return (from a in _edmDb.Documents
                    join b in _edmDb.ProcVersionDocsMap
                        on a.documentID equals b.fk_documents
                    where b.active == true
                          && a.active == true
                          && b.fk_processVersion == processVersionId
                    select a).ToList();
        }
        public DocumentsInStep GetDocumentInStep(int stepid)
        {
            return (from a in _edmDb.DocumentsInStep
                    join b in _edmDb.StepDocInStepMap
                    on a.documentsInStepId equals  b.fk_documentInStep
                    where b.fk_step == stepid
                          && a.active == true
                    select a).FirstOrDefault();
        }
        public Users GetUserById(int userId)
        {
            return (from a in _edmDb.Users where a.userID == userId select a).FirstOrDefault();
        }
        public string GetDocumentComment(int docId, int versionId)
        {
            return (from a in _edmDb.ProcVersionDocsMap
                    where a.fk_processVersion == versionId
                          && a.fk_documents == docId
                    select a.documentComment).FirstOrDefault();
        }
        public string GetCommentForLastVersion(int processId)
        {
            ProcessVersions currentVersion = GetLastVersionInProcess(processId);
            if (currentVersion != null)
                return GetLastVersionInProcess(processId).comment;
            return "";
        }
        public List<Users> GetUsersBySearch(string searchValue)
        {
            List<Users> listToReturn;
            listToReturn = (from a in _edmDb.Users
                            where a.active == true
                            select a).ToList();

            return listToReturn;
        }
        public ListItem[] GetProcessCharacterList(int selectedId)
        {
            List<ProcessCharacter> processCharacters = GetProcessCharactersList();

            ListItem[] listToReturn = new ListItem[processCharacters.Count + 1];
            int i = 1;
            bool anySelected = false;
            foreach (ProcessCharacter currentprocessCharacter in processCharacters)
            {
                ListItem newListItem = new ListItem();
                newListItem.Text = currentprocessCharacter.name;
                newListItem.Value = currentprocessCharacter.processCharacterId.ToString();
                if (currentprocessCharacter.processCharacterId == selectedId)
                {
                    newListItem.Selected = true;
                    anySelected = true;
                }
                listToReturn[i] = newListItem;
                i++;
            }

            ListItem newListItemZero = new ListItem();
            newListItemZero.Text = "";
            newListItemZero.Value = "0";
            if (!anySelected)
                newListItemZero.Selected = true;

            listToReturn[0] = newListItemZero;


            return listToReturn;
        }
        public ListItem[] GetSubmittersList(int selectedId)
        {
            List<Submitters> submitters = (from a in _edmDb.Submitters
                                           where a.active == true
                                           select a).ToList();

            ListItem[] listToReturn = new ListItem[submitters.Count + 1];


            int i = 1;
            bool anySelected = false;
            foreach (Submitters currentSubmitter in submitters)
            {
                ListItem newListItem = new ListItem();
                newListItem.Text = GetUserById(currentSubmitter.fk_user).name;
                newListItem.Value = currentSubmitter.submitterId.ToString();
                if (currentSubmitter.submitterId == selectedId)
                {
                    newListItem.Selected = true;
                    anySelected = true;
                }
                listToReturn[i] = newListItem;
                i++;
            }

            ListItem newListItemZero = new ListItem();
            newListItemZero.Text = "";
            newListItemZero.Value = "0";
            if (!anySelected)
                newListItemZero.Selected = true;

            listToReturn[0] = newListItemZero;


            return listToReturn;
        }
        public List<ProcessTemplateParticipant> GetProcessTemplateParticipantsInTemplate(int templateId)
        {
            return
                (from a in _edmDb.ProcessTemplateParticipant
                 where a.active == true && a.fk_template == templateId
                 select a).OrderByDescending(mc => mc.queue).ToList();
        }
        public List<ProcessTemplate> GetAllProcessTemplates()
        {
            return (from a in _edmDb.ProcessTemplate where a.active == true select a).ToList();
        }
        public Struct GetStructById (int structId)
        {
            return (from a in _edmDb.Struct
                where a.structID == structId
                select a).FirstOrDefault();
        }
        public List<ProcessTemplate> GetAllProcessTemplatesByStruct(int structId)
        {
            List<ProcessTemplate> toReturn = new List<ProcessTemplate>();
            int currentStruct = structId;
            while (true)
            {
                List<ProcessTemplate> tmpList = (from a in _edmDb.ProcessTemplate
                    where a.active == true
                          && a.fk_struct == currentStruct
                    select a).ToList();
                foreach (ProcessTemplate tmp in tmpList)
                {
                    toReturn.Add(tmp);
                }
                if (currentStruct == 2)
                    break;
                currentStruct = (int) GetStructById(currentStruct).fk_parent;

            }


            return toReturn;
        }
        public ProcessTemplate GetProcessTemplateById(int templateId)
        {
            return (from a in _edmDb.ProcessTemplate where a.processTemplateId == templateId select a).FirstOrDefault();

        }
        public List<Users> GetUsersInStruct(int structId)
        {
            return (from a in _edmDb.Users
                    where a.fk_struct == structId
                    && a.active == true
                    select a).ToList();
        }
        public List<Steps> GetStepsInProcessVersion(int processVersoinId)
        {
            return
                (from a in _edmDb.Steps where a.active == true && a.fk_processVersion == processVersoinId select a)
                    .ToList();
        }
        public List<ProcessVersions> GetProcessVersionsInProcess(int processId)
        {
            return (from a in _edmDb.ProcessVersions
                    where a.active == true
                          && a.fk_process == processId
                    select a).ToList();
        }
        public List<Users> GetRecursiveChildStructList(int structId , bool isFirstStep)
        {
            List<Users> ListToReturn = new List<Users>();
            if (!isFirstStep)
            {
                ListToReturn = (from a in _edmDb.Users
                    where a.active == true
                          && a.fk_struct == structId
                    select a).ToList();
                if (ListToReturn.Any())
                {
                    return ListToReturn;
                }
            }
            List<Struct> childStructs = new List<Struct>();
            
            childStructs = (from a in _edmDb.Struct
                where a.fk_parent == structId
                select a).ToList();

            foreach (Struct currentChild in childStructs)
            {
                List<Users> tmp = GetRecursiveChildStructList(currentChild.structID,false);
                foreach (Users curTmp in tmp)
                {
                    ListToReturn.Add(curTmp);
                    return ListToReturn;
                }
            }
            return ListToReturn;

        }
        public List<Users> GetRecursiveChildAllStructList(int structId, bool isFirstStep)
        {
            List<Users> ListToReturn = new List<Users>();
            if (!isFirstStep)
            {
                ListToReturn = (from a in _edmDb.Users
                                where a.active == true
                                      && a.fk_struct == structId
                                select a).ToList();
                if (ListToReturn.Any())
                {
                    return ListToReturn;
                }
            }
            List<Struct> childStructs = new List<Struct>();

            childStructs = (from a in _edmDb.Struct
                            where a.fk_parent == structId
                            select a).ToList();

            foreach (Struct currentChild in childStructs)
            {
                List<Users> tmp = GetRecursiveChildAllStructList(currentChild.structID, false);
                foreach (Users curTmp in tmp)
                {
                    ListToReturn.Add(curTmp);
                }
            }
            return ListToReturn;

        }

        #endregion
        #region set
        public void SetDocumentToVersion(int documentId, int newVersion, string newComment) // раньше меняли просто фк, но теперь есть таблица связи поэтому создадим новую связь
        {
            ProcVersionDocsMap newDocMap = new ProcVersionDocsMap();
            newDocMap.active = true;
            newDocMap.fk_processVersion = newVersion;
            newDocMap.fk_documents = documentId;
            newDocMap.documentComment = newComment;
            _edmDb.ProcVersionDocsMap.InsertOnSubmit(newDocMap);
            _edmDb.SubmitChanges();
        }
        public void SetProcessSubmitter(int procId, int? submitterId)
        {
            Processes proc = GetProcessById(procId);
            proc.fk_submitter = submitterId;
            _edmDb.SubmitChanges();
        }
        public void SetProcessCharacter(int procId, int? characterId)
        {
            Processes proc = GetProcessById(procId);
            proc.fk_processCharacter = characterId;
            _edmDb.SubmitChanges();
        }
        
        public void SetProcessStatus(int procId, int newStatus)
        {
            Processes proc = GetProcessById(procId);
            proc.status = newStatus;
            _edmDb.SubmitChanges();
        }

        public void SetTemplateParams(string name, string title, string content, int templateId, int submitterId,int processCharacterId, int fkStruct, bool allowChangeProcess)
        {
            ProcessTemplate currentTemplate = GetProcessTemplateById(templateId);
            currentTemplate.content_ = content;
            currentTemplate.name = name;
            
            currentTemplate.title = title;
            currentTemplate.allowEditProcess = allowChangeProcess;
            currentTemplate.fk_struct = fkStruct;
            if (submitterId == 0)
            {
                currentTemplate.fk_submitter = null;
            }
            else
            {
                currentTemplate.fk_submitter = submitterId;
            }

            if (processCharacterId == 0)
            {
                currentTemplate.fk_processCharacter = null;
            }
            else
            {
                currentTemplate.fk_processCharacter = processCharacterId;
            }

            _edmDb.SubmitChanges();
        }
        #endregion
        #region check

        public bool IsProcessOk(int procId)
        {
            if (GetLastVersionInProcess(procId) != null)
                return true;
            return false;
        }
        public bool CanUserPrint(int userId)
        {
            return (from a in _edmDb.Users
                    where a.userID == userId
                    select a.canPrintResult).FirstOrDefault();

        }
        public bool CanUserStart(int userId)
        {
            return (from a in _edmDb.Users
                    where  a.userID == userId
                    select a.canInitiate).FirstOrDefault();

        }

        public bool IsUserStarter(int userId)
        {
            return (from a in _edmDb.ProcessStarterUser
                where a.active == true
                      && a.fk_user == userId
                select a).Any();

        }
        public bool IsAnyTemplateByThatName(string name)
        {
           return (from a in _edmDb.ProcessTemplate
                where a.name == name
                      && a.active == true
                select a).Any();
        }
        public bool IsUserHead(int userId)
        {
            Users headuser = GetUserById(userId);
            if (headuser.fk_struct == null)
                return false;
            return GetRecursiveChildStructList((int)headuser.fk_struct,true).Any();
        }
        public bool IsUserSubmitter(int userId)
        {
            if ((from a in _edmDb.Submitters
                 where a.active == true
                       && a.fk_user == userId
                 select a).Any())
                return true;
            return false;
        }
        public bool CanUserDoTemplate(int userId)
        {
            Users user = GetUserById(userId);
            if (user != null)
            {
                return user.canCreateTemplate;
            }
            return false;
        }
        public bool IsTheDayWorkingDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                bool isWorkingDay = ((from a in _edmDb.Calendar
                                      where a.active == true
                                            && a.date == date.Date
                                            && a.isWorkingDay == true
                                      select a).Any());
                return isWorkingDay;
            }
            else
            {
                bool isDayOff = (from a in _edmDb.Calendar
                                 where a.active == true
                                       && a.date == date.Date
                                       && a.isDayOff
                                 select a).Any();
                return !isDayOff;
            }
        }

        public bool IsAnyStepInProcess(int procId)
        {
           return (from a in _edmDb.ProcessVersions
                where a.fk_process == procId
                join b in _edmDb.Steps
                    on a.processVersionID equals b.fk_processVersion
                where b.active == true
                select b).Any();
        }
        public bool WithQueueuByType(string type)
        {
            if (type == "serial")
                return true;
            return false;
        }
        public bool WithQueueByProcess(int processId)
        {
            Processes currentProc = GetProcessById(processId);
            return WithQueueuByType(currentProc.type);
        }
        #endregion
        #region other
        public void AddParticipantClick(object sender, EventArgs e)
        {
            TextBox newParticipant = new TextBox();
            ExistingParticipants.Add(newParticipant);
        }
        public void DownloadFile(int docInProcId)
        {
            Documents doc = (from a in _edmDb.Documents
                             where a.documentID == docInProcId
                             select a).FirstOrDefault();
            ProcVersionDocsMap procVersionDocsMap = (from a in _edmDb.ProcVersionDocsMap
                                                     where a.fk_documents == docInProcId
                                                     select a).FirstOrDefault();

            ProcessVersions version = (from a in _edmDb.ProcessVersions
                                       where a.processVersionID == procVersionDocsMap.fk_processVersion
                                       select a).FirstOrDefault();

            string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + version.fk_process + "/" + doc.documentID + "/" + doc.documentName);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            string name = doc.documentName.Replace(',', '.');
            response.AddHeader("Content-Disposition", "attachment; filename=" + name + ";");
            response.TransmitFile(path);
            response.Flush();
            response.End();
        }
        public void DownloadStepFile(int docInStepId)
        {
            DocumentsInStep doc = (from a in _edmDb.DocumentsInStep
                where a.documentsInStepId == docInStepId
                                   select a).FirstOrDefault();
            StepDocInStepMap stepDocInStepMap = (from a in _edmDb.StepDocInStepMap
                                                   where a.fk_documentInStep == docInStepId
                                                   select a).FirstOrDefault();
            Steps step = (from a in _edmDb.Steps
                where a.stepID == stepDocInStepMap.fk_step
                select a).FirstOrDefault();
            ProcessVersions version = (from a in _edmDb.ProcessVersions
                where a.processVersionID == step.fk_processVersion
                select a).FirstOrDefault();

            string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + version.fk_process + "stepsDocs/" + doc.documentsInStepId + "/"+ doc.documentName);
            //string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + procVersionDocsMap.ProcVersionDocsMapId + "/" + doc.documentID + "/" + doc.documentName);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            string name = doc.documentName.Replace(',', '.');
            response.AddHeader("Content-Disposition", "attachment; filename=" + name + ";");
            response.TransmitFile(path);
            response.Flush();
            response.End();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
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

        public List<ProcessEdit.Participant> ParticipantsList = new List<ProcessEdit.Participant>();
        public int CreateNewParticipent(int processId, int userId , int queue ,DateTime? endDate)
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

        public void SetTemplateParams(string name, string title, string content,int templateId)
        {
            ProcessTemplate currentTemplate = GetProcessTemplateById(templateId);
            currentTemplate.content_ = content;
            currentTemplate.name = name;
            currentTemplate.title = title;
            _edmDb.SubmitChanges();
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

        public void MakeAllParticipantsIsNew(int processId)
        {
            List<Participants> participants = (from a in _edmDb.Participants
                where a.active == true
                      && a.fk_process == processId
                select a).ToList();
            foreach (Participants participant in participants)
            {
                participant.isNew = true;
            }
            _edmDb.SubmitChanges();
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
        public string GetCommentForLastVersion(int processId)
        {
            ProcessVersions currentVersion = GetLastVersionInProcess(processId);
            if (currentVersion!=null)
            return GetLastVersionInProcess(processId).comment;
            return "";
        }
        public List<Users> GetUsersBySearch(string searchValue)
        {
            List<Users> listToReturn;
            /*if (searchValue == "")
            {
                listToReturn = (from a in _edmDb.Users
                    where a.active == true
                          &&
                          (a.email.Contains(searchValue)
                           || a.login.Contains(searchValue)
                           || a.name.Contains(searchValue)
                           || a.@struct.Contains(searchValue)
                              )
                    select a).ToList();
            }
            */
                listToReturn = (from a in _edmDb.Users
                                where a.active == true
                                select a).ToList();
            
            return listToReturn;
        } 
        public int CreateProcessByType(string type, int userId,string name)
        {
            Processes newProcess = new Processes();
            newProcess.active = true;
            newProcess.fk_initiator = userId;
            newProcess.type = type;
            newProcess.name = name;
            newProcess.status = -1;
            _edmDb.Processes.InsertOnSubmit(newProcess);
            _edmDb.SubmitChanges();
            return newProcess.processID;
        }
        public List<TextBox> ExistingParticipants = new List<TextBox>();
        public void AddParticipantClick(object sender, EventArgs e)
        {
            TextBox newParticipant = new TextBox();
            ExistingParticipants.Add(newParticipant);
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
            List <ProcessEdit.Participant> listToReturn = new List<ProcessEdit.Participant>();

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

        public List<Documents> GetDocumentsInProcessVersion(int processVersionId,bool onlyActive)
        {
            return (from a in _edmDb.Documents
                                         join b in _edmDb.ProcVersionDocsMap
                                             on a.documentID equals b.fk_documents
                                         where b.active == true
                                               && a.active == true
                                               && b.fk_processVersion == processVersionId
                                         select a).ToList();

            /*
            return (from a in _edmDb.Documents
                where a.fk_processVersion == processVersionId
                      && (a.active == true || !onlyActive)
                select a).ToList();
                */
        }


        public DocumentsInStep GetDocumentInStep(int stepid)
        {
            return (from a in _edmDb.DocumentsInStep
                    where a.fk_step == stepid
                          && a.active == true 
                    select a).FirstOrDefault();
        }


        public Users GetUserById(int userId)
        {
            return (from a in _edmDb.Users where a.userID == userId select a).FirstOrDefault();
        }
        public void KillDocument(int docId,int versionId)
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

        public string GetDocumentComment(int docId, int versionId)
        {
            return (from a in _edmDb.ProcVersionDocsMap
                where a.fk_processVersion == versionId
                      && a.fk_documents == docId
                select a.documentComment).FirstOrDefault();
        }

        public void SetDocumentToVersion(int documentId, int newVersion, string newComment) // раньше меняли просто фк, но теперь есть таблица связи поэтому создадим новую связь
        {

            ProcVersionDocsMap newDocMap = new ProcVersionDocsMap();
            newDocMap.active = true;
            newDocMap.fk_processVersion = newVersion;
            newDocMap.fk_documents = documentId;
            newDocMap.documentComment = newComment;
            _edmDb.ProcVersionDocsMap.InsertOnSubmit(newDocMap);
            _edmDb.SubmitChanges();
            /*
            Documents doc = (from a in _edmDb.Documents
                where a.documentID == documentId
                select a).FirstOrDefault();
            doc.fk_processVersion = newVersion;
            doc.documentComment = newComment;
            _edmDb.SubmitChanges();
            */
        }
        public int CreateNewProcessVersion(int processId,string comment, int version, string status)
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
        public int CreateNewDocument(string docName, int processVersion , string docComment)
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
            newDocument.fk_step = stepId;
            newDocument.documentComment = docComment;
            _edmDb.DocumentsInStep.InsertOnSubmit(newDocument);
            _edmDb.SubmitChanges();
            return newDocument.documentsInStepId;
        }

        public int CreateChildProcess(int parentProcessId)
        {

                int userId;
                int.TryParse(HttpContext.Current.Session["userID"].ToString(), out userId);


                Processes parentProcess = GetProcessById(parentProcessId);
            int childProcessId = CreateProcessByType(parentProcess.type, userId, parentProcess.name);
            Processes childProcess = GetProcessById(childProcessId);
            childProcess.fk_parentProcess = parentProcessId;
            ProcessVersions parentProcessLastVersion = GetProcessVersionsLastVerson(parentProcessId);
            int childProcVersion = CreateNewProcessVersion(childProcessId, parentProcessLastVersion.comment, 0, "");

            List<Documents> parentDocuments = GetDocumentsInProcessVersion(parentProcessLastVersion.processVersionID,false);

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
    }
}
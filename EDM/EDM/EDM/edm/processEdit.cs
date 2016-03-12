using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public class ProcessMainFucntions
    {
        EDMdbDataContext _edmDb = new EDMdbDataContext();
        public void KillParticipant(int participantId)
        {
            Participants participantToKill =
                (from a in _edmDb.Participants where a.participantID == participantId select a).FirstOrDefault();
            participantToKill.active = false;
            _edmDb.SubmitChanges();
        }
        public List<ProcessEdit.Participant> ParticipantsList = new List<ProcessEdit.Participant>();
        public int CreateNewParticipent(int processId, int userId , int queue ,DateTime endDate)
        {
            Participants newParticipant = new Participants();
            newParticipant.active = true;
            newParticipant.fk_user = userId;
            newParticipant.dateStart = DateTime.Now;
            newParticipant.dateEnd = endDate;
            newParticipant.status = 0;
            newParticipant.fk_process = processId;
            newParticipant.queue = queue;
            
            
            _edmDb.Participants.InsertOnSubmit(newParticipant);
            _edmDb.SubmitChanges();
            return newParticipant.participantID;

        }
        public bool WithQueueByProcess(int processId)
        {
            Processes currentProc = GetProcessById(processId);
            if (currentProc.type == "serial")
                return true;
            return false;
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
        public List<Documents> GetDocumentsInProcessVersion(int processVersionId)
        {
            return (from a in _edmDb.Documents
                where a.fk_processVersion == processVersionId
                      && a.active == true
                select a).ToList();
        }
        public Users GetUserById(int userId)
        {
            return (from a in _edmDb.Users where a.userID == userId select a).FirstOrDefault();
        }
        public void KillDocument(int docId)
        {
            Documents docToKill =
                (from a in _edmDb.Documents where a.documentID == docId select a).FirstOrDefault();
            docToKill.active = false;
            _edmDb.SubmitChanges();
        }
        public void SetDocumentToVersion(int documentId, int newVersion)
        {
            Documents doc = (from a in _edmDb.Documents
                where a.documentID == documentId
                select a).FirstOrDefault();
            doc.fk_processVersion = newVersion;
            _edmDb.SubmitChanges();
        }
        public int CreateNewProcessVersion(int processId,string comment, int version, string status)
        {
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
            newDocument.fk_processVersion = processVersion;
            newDocument.documentComment = docComment; 
            _edmDb.Documents.InsertOnSubmit(newDocument);
            _edmDb.SubmitChanges();
            return newDocument.documentID;
        }
    }
}
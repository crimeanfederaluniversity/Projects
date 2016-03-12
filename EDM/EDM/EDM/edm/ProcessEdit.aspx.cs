using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class ProcessEdit : System.Web.UI.Page
    {
        #region static
        public static List<Participant> ParticipantsList = new List<Participant>();  // Осторожно с ним  // обнуляю его в pageload 
        public static List<DocumentsClass> DocumentsList = new List<DocumentsClass>(); // и с ним осторожно
        public class Participant
        {
            public int ParticipantId { get; set; }
            public TextBox ParticipantQueueTextBox { get; set; }
            public TextBox ParticipantTextBox { get; set; }
            public TextBox ParticipantEndDateTextBox { get; set; }
        }
        public class DocumentsClass
        {
            public int DocumentId { get; set; }
            public LinkButton LinkButtonToDocument { get; set; }                       
            public FileUpload DocumentFileUpload { get; set; }
            public TextBox DocumentCommentTextBox { get; set; }
        }
        public void DeleteDocumentRow(object sender, EventArgs e)
        {
            int rowId = 0;
            Button delButton = (Button) sender;
            Int32.TryParse(delButton.CommandArgument, out rowId);
            if (rowId < DocumentsList.Count)
            {
                DocumentsClass docToDel = DocumentsList[rowId];
                DocumentsList.Remove(docToDel);
            }
            Refersh();
        }
        public void DeleteParticipentRow(object sender, EventArgs e)
        {
            int rowId = 0;
            Button delButton = (Button)sender;
            Int32.TryParse(delButton.CommandArgument, out rowId);
            if (rowId < ParticipantsList.Count)
            {
                Participant participantToDel = ParticipantsList[rowId];
                ParticipantsList.Remove(participantToDel);
            }
            Refersh();
        }
        public void AddDocumentRow(object sender, EventArgs e)
        {
            DocumentsClass newDoc = new DocumentsClass();
            newDoc.DocumentFileUpload = new FileUpload();
            newDoc.DocumentCommentTextBox = new TextBox();      
            DocumentsList.Add(newDoc);
            Refersh();
        }
        public void AddParticipentRow(object sender, EventArgs e)
        {
            Participant newParticipant = new Participant();
            newParticipant.ParticipantEndDateTextBox = new TextBox();
            newParticipant.ParticipantQueueTextBox = new TextBox();
            newParticipant.ParticipantTextBox = new TextBox();
            ParticipantsList.Add(newParticipant);
            Refersh();
        }
        #endregion
        ProcessMainFucntions main = new ProcessMainFucntions();
        #region views
        public Table GetSearchResults(string searchText, int rowId)
        {
            Table tableToReturn = new Table();
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headercell1 = new TableHeaderCell();
            headercell1.Text = "Имя";
            headerRow.Cells.Add(headercell1);
            TableHeaderCell headercell2 = new TableHeaderCell();
            headercell2.Text = "email";
            headerRow.Cells.Add(headercell2);
            TableHeaderCell headercell3 = new TableHeaderCell();
            headercell2.Text = "Структура";
            headerRow.Cells.Add(headercell3);
            TableHeaderCell headercell4 = new TableHeaderCell();
            headercell2.Text = "Выбрать";
            headerRow.Cells.Add(headercell4);

            List<Users> users = main.GetUsersBySearch(searchText);
            foreach (Users user in users)
            {

                TableRow userRow = new TableRow();

                TableCell cell1 = new TableHeaderCell();
                cell1.Text = user.name;
                userRow.Cells.Add(cell1);
                TableCell cell2 = new TableHeaderCell();
                cell2.Text = user.email;
                userRow.Cells.Add(cell2);
                TableCell cell3 = new TableHeaderCell();
                cell3.Text = user.@struct;
                userRow.Cells.Add(cell3);

                TableCell cell4 = new TableHeaderCell();
                Button chooseButton = new Button();

                chooseButton.Text = "Выбрать";
                chooseButton.OnClientClick = "document.getElementById('MainContent_ParticipentIdTextBox" + rowId.ToString() + "').value = " + user.userID + "; document.getElementById('MainContent_chooseUserPanel" + rowId + "').style.visibility = 'hidden';  return false; ";
                cell4.Controls.Add(chooseButton);
                userRow.Cells.Add(cell4);
                tableToReturn.Rows.Add(userRow);
            }
            return tableToReturn;
        }
        public Panel GetFiexdPanel(int rowId)
        {
            Panel panelToReturn = new Panel();
            panelToReturn.ID = "chooseUserPanel" + rowId;
            panelToReturn.Style.Add("top", "50%");
            panelToReturn.Style.Add("left", "50%");
            panelToReturn.Style.Add("margin", "-250px 0px 0px -470px");

            panelToReturn.Style.Add("border", "1px solid black");
            panelToReturn.Style.Add("z-index", "21");
            panelToReturn.Style.Add("position", "fixed");
            panelToReturn.Style.Add("background-color", "white");
            panelToReturn.Style.Add("visibility", "hidden");
            panelToReturn.Style.Add("height", "500px");
            panelToReturn.Style.Add("width", "940px");

            Panel scrollPanel = new Panel();
            scrollPanel.ID = "chooseUserScrollPanel" + rowId;
            scrollPanel.Style.Add("overflow", "scroll");
            scrollPanel.Style.Add("height", "100%");

            scrollPanel.Controls.Add(GetSearchResults("", rowId));

            panelToReturn.Controls.Add(scrollPanel);
            return panelToReturn;
        }
        public Table GetNewParticipantsTable()
        {
            int processId = 0;
            Table participantsTable = new Table();
            Int32.TryParse(HttpContext.Current.Session["processID"].ToString(), out processId);
            if (processId == 0)
            {
                return participantsTable;
            }

            int participantsCount = ParticipantsList.Count;

            if (participantsCount != 0)
            {
                TableHeaderRow tableHeaderRow = new TableHeaderRow();
                TableHeaderCell tableQueueHeaderCell = new TableHeaderCell();
                tableQueueHeaderCell.Text = "Очередь";
                TableHeaderCell tableEndDateHeaderCell = new TableHeaderCell();
                tableEndDateHeaderCell.Text = "Срок";
                TableHeaderCell tableParticipentHeaderCell = new TableHeaderCell();
                tableParticipentHeaderCell.Text = "Пользователь";
                TableHeaderCell tableDeleteParticipentHeaderCell = new TableHeaderCell();
                tableDeleteParticipentHeaderCell.Text = "Удалить";

                tableHeaderRow.Cells.Add(tableQueueHeaderCell);
                tableHeaderRow.Cells.Add(tableEndDateHeaderCell);
                tableHeaderRow.Cells.Add(tableParticipentHeaderCell);
                tableHeaderRow.Cells.Add(tableDeleteParticipentHeaderCell);
                participantsTable.Rows.Add(tableHeaderRow);
                for (int i = 0; i < participantsCount; i++)
                {
                    TableRow participantRow = new TableRow();

                    TableCell queueCell = new TableCell();

                    queueCell.Controls.Add(ParticipantsList[i].ParticipantQueueTextBox);
                    participantRow.Cells.Add(queueCell);

                    TableCell endDateCell = new TableCell();
                    ParticipantsList[i].ParticipantEndDateTextBox.ID = "ParticipentEndDateTextBox" + i;
                    endDateCell.Controls.Add(ParticipantsList[i].ParticipantEndDateTextBox);

                    participantRow.Cells.Add(endDateCell);

                    TableCell participentCell = new TableCell();
                    ParticipantsList[i].ParticipantTextBox.Visible = true;
                    ParticipantsList[i].ParticipantTextBox.ID = "ParticipentIdTextBox" + i;
                    participentCell.Controls.Add(ParticipantsList[i].ParticipantTextBox);

                    Button openPunelButton = new Button();
                    openPunelButton.Text = "Выбрать";
                    openPunelButton.OnClientClick = "document.getElementById('MainContent_chooseUserPanel" +
                                                    i.ToString() +
                                                    "').style.visibility = 'visible'; return false; ";
                    participentCell.Controls.Add(GetFiexdPanel(i));
                    participentCell.Controls.Add(openPunelButton);
                    participantRow.Cells.Add(participentCell);

                    TableCell deleteParticipentCell = new TableCell();

                    Button deleteParticipentButton = new Button();
                    deleteParticipentButton.Text = "Удалить";
                    deleteParticipentButton.CommandArgument = i.ToString();
                    deleteParticipentButton.ID = "deleteParticipentButton"+ i.ToString();
                    deleteParticipentButton.Click +=  DeleteParticipentRow;
                    
                    deleteParticipentCell.Controls.Add(deleteParticipentButton);
                    participantRow.Cells.Add(deleteParticipentCell);




                    participantsTable.Rows.Add(participantRow);
                }
            }
            Button addRowToParticipantButton = new Button();
            addRowToParticipantButton.Text = "Добавить согласующего";
            addRowToParticipantButton.ID = "addRowToParticipantButton";
            addRowToParticipantButton.Click += AddParticipentRow;

            TableRow addNewRow = new TableRow();
            TableCell addNewRowCell = new TableCell();
            addNewRowCell.ColumnSpan = 2;
            addNewRowCell.Controls.Add(addRowToParticipantButton);
            addNewRow.Cells.Add(addNewRowCell);
            participantsTable.Rows.Add(addNewRow);


            return participantsTable;
        }
        public Table GetDocumentsTable()
        {
            Table tableToReturn = new Table();

            if (DocumentsList.Count != 0)
            {
                TableHeaderRow headerRow = new TableHeaderRow();
                TableHeaderCell headerCell1 = new TableHeaderCell();
                TableHeaderCell headerCell2 = new TableHeaderCell();
                TableHeaderCell headerCell3 = new TableHeaderCell();

                headerCell1.Text = "Документ";
                headerCell2.Text = "Название документа";
                headerCell3.Text = "Удалить";

                headerRow.Cells.Add(headerCell1);
                headerRow.Cells.Add(headerCell2);
                headerRow.Cells.Add(headerCell3);

                tableToReturn.Rows.Add(headerRow);
                int rowId = 0;
                foreach (DocumentsClass currentDocument in DocumentsList)
                {

                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();

                    if (currentDocument.DocumentFileUpload !=null)
                    cell1.Controls.Add(currentDocument.DocumentFileUpload);
                    if (currentDocument.LinkButtonToDocument != null)
                        cell1.Controls.Add(currentDocument.LinkButtonToDocument);

                    cell2.Controls.Add(currentDocument.DocumentCommentTextBox);

                    Button DeleteRowButton = new Button();
                    DeleteRowButton.Text = "Удалить";
                    DeleteRowButton.CommandArgument = rowId.ToString();
                    DeleteRowButton.ID = "DeleteRowButton"+rowId;
                    DeleteRowButton.Click += DeleteDocumentRow;

                    cell3.Controls.Add(DeleteRowButton);

                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);

                    tableToReturn.Rows.Add(row);
                    rowId++;
                }
            }
            Button addRowToDocButton = new Button();
            addRowToDocButton.ID = "addRowToDocButton";
            addRowToDocButton.Text = "Добавить документ";
            addRowToDocButton.Click += AddDocumentRow;

            TableRow addNewRow = new TableRow();
            TableCell addNewRowCell = new TableCell();
            addNewRowCell.ColumnSpan = 3;
            addNewRowCell.Controls.Add(addRowToDocButton);
            addNewRow.Cells.Add(addNewRowCell);
            tableToReturn.Rows.Add(addNewRow);

            return tableToReturn;
        }
        public void Refersh()
        {
            int processId = 0;
            Int32.TryParse(HttpContext.Current.Session["processID"].ToString(), out processId);
            if (processId != 0)
            {
                createNewProcessDiv.Visible = false;
                existingProcessTitleDiv.Visible = true;
                ParticipantsDiv.Visible = true;
                documentsDiv.Visible = true;
                SaveAllDiv.Visible = true;

                ProcessIdLabel.Text = main.GetProcessNameById(processId);

                ParticipantsDiv.Controls.Clear();
                ParticipantsDiv.Controls.Add(GetNewParticipantsTable());
                documentsDiv.Controls.Clear();
                documentsDiv.Controls.Add(GetDocumentsTable());
            }
            else
            {
                existingProcessTitleDiv.Visible = false;
                ParticipantsDiv.Visible = false;
                documentsDiv.Visible = false;
                SaveAllDiv.Visible = false;
            }
        }
        #endregion
        public void CreateNewProcess(object sender, EventArgs e)
        {
            int processId = main.CreateProcessByType(ProcessTypeDropDown.SelectedValue, (int)HttpContext.Current.Session["userID"], ProcessNameTextBox.Text);
            HttpContext.Current.Session["processID"] = processId;
            int participantsCount = 0;
            Int32.TryParse(ParticipantsCountTextBox.Text, out participantsCount);

            for (int i = 0; i < participantsCount; i++)
            {
                Participant newparticipant = new Participant();
                newparticipant.ParticipantQueueTextBox = new TextBox();

                newparticipant.ParticipantQueueTextBox.Text = i.ToString();

                newparticipant.ParticipantTextBox = new TextBox();
                newparticipant.ParticipantEndDateTextBox = new TextBox();
                newparticipant.ParticipantEndDateTextBox.Attributes.Add("onfocus", "this.select();lcs(this)");
                newparticipant.ParticipantEndDateTextBox.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");
                ParticipantsList.Add(newparticipant);
            }
            Refersh();
        }

        protected void SaveAllButton_Click(object sender, EventArgs e)
        {
            int processId = 0;
            Int32.TryParse(HttpContext.Current.Session["processID"].ToString(), out processId);
            List<Participant> participantsToAdd;
            List<DocumentsClass> documentsToAdd;

            List<Participant> oldParticipants = main.GetParticipantsInProcess(processId);
            if (oldParticipants.Count > 0)
            {
                List<Participant> participantsToDelete = oldParticipants.Except((from a in ParticipantsList
                                                          join b in oldParticipants 
                                                          on a.ParticipantTextBox.Text equals b.ParticipantTextBox.Text
                                                          where a.ParticipantEndDateTextBox.Text == b.ParticipantEndDateTextBox.Text
                                                          && a.ParticipantQueueTextBox.Text == b.ParticipantQueueTextBox.Text
                                                          select b).Distinct().ToList()).ToList();
                foreach (Participant currentParticipant in participantsToDelete)
                {
                     main.KillParticipant(currentParticipant.ParticipantId);   
                }

                participantsToAdd = ParticipantsList.Except((from a in ParticipantsList
                                                            join b in oldParticipants
                                                            on a.ParticipantTextBox.Text equals b.ParticipantTextBox.Text
                                                            where a.ParticipantEndDateTextBox.Text == b.ParticipantEndDateTextBox.Text
                                                            && a.ParticipantQueueTextBox.Text == b.ParticipantQueueTextBox.Text
                                                            select a).Distinct().ToList()).ToList();
            }
            else
            {
                participantsToAdd = ParticipantsList;
            }
            List<DocumentsClass> oldDocuments = main.GetDocumentsInProcess(processId);
            if (oldDocuments.Count>0)
            {
                            
                documentsToAdd = new List<DocumentsClass>();
            }
            else
            {

                documentsToAdd = DocumentsList;
            }

            foreach (Participant currentParticipant in participantsToAdd)
            {              
                int userId = 0;
                int queue = 0;
                DateTime endDateTime = DateTime.Now;
                
                Int32.TryParse(currentParticipant.ParticipantTextBox.Text, out userId);
                Int32.TryParse(currentParticipant.ParticipantQueueTextBox.Text, out queue);
                DateTime.TryParse(currentParticipant.ParticipantEndDateTextBox.Text, out endDateTime);

                main.CreateNewParticipent(processId, userId, queue, endDateTime);
            }
            int processVersionId = main.CreateNewProcessVersion(processId, "1234", 0, "-1");


            foreach (DocumentsClass document in documentsToAdd)
            {
                FileUpload currentFileUpload = document.DocumentFileUpload;
                if (currentFileUpload.HasFile)
                {
                    try
                    {
                        int userId = 1;
                        var sesUserId = HttpContext.Current.Session["userID"];
                        if (sesUserId != null)
                        {
                            Int32.TryParse(sesUserId.ToString(), out userId);
                        }

                        int docId = main.CreateNewDocument(currentFileUpload.FileName, processVersionId, document.DocumentCommentTextBox.Text);

                        string directoryToSave =
                            HttpContext.Current.Server.MapPath("~/edm/documents/" + processId + "/" + docId + "/");
                        if (!Directory.Exists(directoryToSave))
                        {
                            Directory.CreateDirectory(directoryToSave);
                        }

                        currentFileUpload.SaveAs(directoryToSave + currentFileUpload.FileName);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }
            Response.Redirect("~/Default.aspx");
        }
        public void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session["userID"] = 1;
                //Session["processID"] = 0;
                ParticipantsList = new List<Participant>();
                DocumentsList = new List<DocumentsClass>();
                int processId = 0;
                Int32.TryParse(HttpContext.Current.Session["processID"].ToString(), out processId);
                if (processId != 0)
                {
                    ParticipantsList = main.GetParticipantsInProcess(processId);
                    DocumentsList = main.GetDocumentsInProcess(processId);

                }
            }
            Refersh();
        }

    }
}
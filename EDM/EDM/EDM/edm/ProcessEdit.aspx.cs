﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
            public TextBox ParticipantNameTextBox { get; set; }
            public TextBox ParticipantEndDateTextBox { get; set; }

            public RequiredFieldValidator ParticipantQueueValidator { get; set; }
            public RequiredFieldValidator ParticipantUserValidator { get; set; }
            public RequiredFieldValidator ParticipantUserNameValidator { get; set; }
            public RequiredFieldValidator ParticipantEndDateValidator { get; set; }

        }

        public class DocumentsClass
        {
            public int DocumentId { get; set; }
            public LinkButton LinkButtonToDocument { get; set; }                       
            public FileUpload DocumentFileUpload { get; set; }
            public TextBox DocumentCommentTextBox { get; set; }
        }


        public void RefreshQueueInParticipantsList(bool withQueue)
        {           
            int i = 0;
            foreach (Participant participant in ParticipantsList)
            {
                participant.ParticipantQueueTextBox.Text = i.ToString();
                if (withQueue)
                i++;
            }
        }

        public Participant CreateParticipant(int rowId,int participientId,string queue,string endDate,string userId, string userName)
        {
            Participant newParticipant = new Participant();

            if (participientId != 0)
                newParticipant.ParticipantId = participientId;

            newParticipant.ParticipantEndDateTextBox = new TextBox();
            if (endDate.Length > 0) newParticipant.ParticipantEndDateTextBox.Text = endDate;
            newParticipant.ParticipantEndDateTextBox.Attributes.Add("onfocus", "this.select();lcs(this)");
            newParticipant.ParticipantEndDateTextBox.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");
            newParticipant.ParticipantEndDateTextBox.ID = "ParticipentEndDateTextBox" + rowId;
            RequiredFieldValidator endDateValidate = new RequiredFieldValidator();
            endDateValidate.ID = "RequiredValidator" + newParticipant.ParticipantEndDateTextBox.ID;
            endDateValidate.ControlToValidate = newParticipant.ParticipantEndDateTextBox.ID;
            endDateValidate.ErrorMessage = "!";
            endDateValidate.ForeColor = Color.Red;
            newParticipant.ParticipantEndDateValidator = endDateValidate;
            
            newParticipant.ParticipantQueueTextBox = new TextBox();
            if (queue.Length > 0) newParticipant.ParticipantQueueTextBox.Text = queue;
            newParticipant.ParticipantQueueTextBox.Text = rowId.ToString();
            newParticipant.ParticipantQueueTextBox.ID= "Queue" + rowId;
            RequiredFieldValidator queueDateValidate = new RequiredFieldValidator();
            queueDateValidate.ID = "RequiredValidator" + newParticipant.ParticipantQueueTextBox.ID;
            queueDateValidate.ControlToValidate = newParticipant.ParticipantQueueTextBox.ID;
            queueDateValidate.ErrorMessage = "!";
            queueDateValidate.ForeColor = Color.Red;
            newParticipant.ParticipantQueueValidator = queueDateValidate;

            newParticipant.ParticipantTextBox = new TextBox();
            if (userId.Length > 0) newParticipant.ParticipantTextBox.Text = userId;
            newParticipant.ParticipantTextBox.ID = "ParticipentIdTextBox" + rowId;
            newParticipant.ParticipantTextBox.Attributes.Add("readonly","true");
            newParticipant.ParticipantTextBox.Style.Add("display", "none");
            //newParticipant.ParticipantTextBox.ReadOnly = true;
            //newParticipant.ParticipantTextBox.Visible = false;
            RequiredFieldValidator userValidate = new RequiredFieldValidator();
            userValidate.ID = "RequiredValidator" + newParticipant.ParticipantTextBox.ID;
            userValidate.ControlToValidate = newParticipant.ParticipantTextBox.ID;
            userValidate.ErrorMessage = "!";
            userValidate.ForeColor = Color.Red;
            newParticipant.ParticipantUserValidator = userValidate;


            newParticipant.ParticipantNameTextBox = new TextBox();
            if (userName.Length > 0) newParticipant.ParticipantNameTextBox.Text = userName;
            newParticipant.ParticipantNameTextBox.ID = "ParticipentNameTextBox" + rowId;
            newParticipant.ParticipantNameTextBox.Attributes.Add("readonly", "true");
            RequiredFieldValidator userNameValidate = new RequiredFieldValidator();
            userNameValidate.ID = "RequiredValidator" + newParticipant.ParticipantNameTextBox.ID;
            userNameValidate.ControlToValidate = newParticipant.ParticipantNameTextBox.ID;
            userNameValidate.ErrorMessage = "!";
            userNameValidate.ForeColor = Color.Red;
            newParticipant.ParticipantUserNameValidator = userNameValidate;

            return newParticipant;
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
            //if (rowId < ParticipantsList.Count)
            //{
            foreach (Participant currentParticipant in ParticipantsList)
            {
                int newRow = -1;
                string tmpstr = currentParticipant.ParticipantNameTextBox.ID.Replace("ParticipentNameTextBox", "");
                Int32.TryParse(tmpstr, out newRow);
                if (newRow == rowId)
                {
                    ParticipantsList.Remove(currentParticipant);
                    Refersh();
                    return;
            }
        }

                


            //}
            Refersh();
        }
        public void AddDocumentRow(object sender, EventArgs e)
        {
            DocumentsClass newDoc = new DocumentsClass();
            newDoc.DocumentFileUpload = new FileUpload();
            newDoc.DocumentFileUpload.ClientIDMode = ClientIDMode.Static;
            newDoc.DocumentFileUpload.ID = "documentFileUpload" + DocumentsList.Count;
            newDoc.DocumentCommentTextBox = new TextBox();
            newDoc.DocumentCommentTextBox.ID = "documentFileComment" + DocumentsList.Count;
            DocumentsList.Add(newDoc);
            Refersh();
        }
        public void AddParticipentRow(object sender, EventArgs e)
        {
            int rowId = 0;
            if (ParticipantsList.Count > 0)
            {
                string tmpstr = ParticipantsList[ParticipantsList.Count - 1].ParticipantNameTextBox.ID.Replace("ParticipentNameTextBox", "");
                Int32.TryParse(tmpstr, out rowId);
                rowId++;
            }
            else
            {
                rowId = 0;
            }
            ParticipantsList.Add(CreateParticipant(rowId, 0, "", "", "", ""));
            
            Refersh();
        }
        public List<DocumentsClass> GetDocumentsInProcess(int processId)
        {
            List<ProcessEdit.DocumentsClass> listToReturn = new List<ProcessEdit.DocumentsClass>();
            ProcessVersions currentVersion = main.GetLastVersionInProcess(processId);
            if (currentVersion != null)
            {
                List<Documents> documentsInCurrentVersion = main.GetDocumentsInProcessVersion(currentVersion.processVersionID,true);
                foreach (Documents currentDocument in documentsInCurrentVersion)
                {
                    DocumentsClass newDocClass = new ProcessEdit.DocumentsClass();

                    newDocClass.LinkButtonToDocument = new LinkButton();
                    newDocClass.LinkButtonToDocument.ID = "linkButton" + currentDocument.documentID;
                    newDocClass.LinkButtonToDocument.Text = currentDocument.documentName;
                    newDocClass.LinkButtonToDocument.CommandArgument = processId+"/"+ currentDocument.documentID;
                    newDocClass.LinkButtonToDocument.Click += GetDocumentClick;
                    
                    newDocClass.DocumentId = currentDocument.documentID;
                    newDocClass.DocumentCommentTextBox = new TextBox();
                    newDocClass.DocumentCommentTextBox.ID = "docComment" + currentDocument.documentID;
                    newDocClass.DocumentCommentTextBox.Text = currentDocument.documentComment;
                    newDocClass.DocumentCommentTextBox.CssClass = "form-control form-inline";

                    listToReturn.Add(newDocClass);
                }
            }
            return listToReturn;
        }
        public void GetDocumentClick(object sender, EventArgs e)
        {
            LinkButton thisButton = (LinkButton)sender;
            string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + thisButton.CommandArgument+"/"+ thisButton.Text);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + thisButton.Text + ";");
            response.TransmitFile(path);
            response.Flush();
            response.End();
        }
        #endregion
        ProcessMainFucntions main = new ProcessMainFucntions();
        #region views


        private TreeNode RecursiveGetTreeNode(int parentId, List<Struct> structList, string panelId, string userNameField,string userIdField, string backValue, bool fullStruct)
        {
            TreeNode nodeToReturn = new TreeNode();
            nodeToReturn.SelectAction = TreeNodeSelectAction.None;
            string value = (from a in structList
                            where a.structID == parentId
                            select a.name).FirstOrDefault();
            if (backValue.Length > 2 && fullStruct)
            {
                value = backValue + ", " + value;
            }

            nodeToReturn.Value = value;
            nodeToReturn.Text = (from a in structList
                                 where a.structID == parentId
                                 select a.name).FirstOrDefault();

            List<Users> usersInStruct = main.GetUsersInStruct(parentId);
            foreach (Users user in usersInStruct)
            {
                TreeNode userNode = new TreeNode();
                userNode.SelectAction = TreeNodeSelectAction.Select;
                userNode.Value = user.name;
                userNode.Text = "<font  color='blue'>"+user.name+"</font>";
                userNode.NavigateUrl = "javascript:putValueAndClose('" + panelId + "','" + userNameField + "','" + userIdField + "','" + user.name + "','" + user.userID + "')";
                nodeToReturn.ChildNodes.Add(userNode);
            }

            List<Struct> children = (from a in structList
                                     where a.fk_parent == parentId
                                     select a).ToList();
            foreach (Struct currentStruct in children)
            {
                nodeToReturn.ChildNodes.Add(RecursiveGetTreeNode(currentStruct.structID, structList, panelId, userNameField, userIdField, value, fullStruct));
            }

            return nodeToReturn;
        }

        public TreeNode GetStructTreeViewNode(string panelId, string userNameField, string userIdField, bool fullStruct)
        {
            
            TreeNode nodeToReturn = new TreeNode();
            nodeToReturn.SelectAction = TreeNodeSelectAction.Select;

            EDMdbDataContext _edm = new EDMdbDataContext();
            List<Struct> outStruct = (from a in _edm.Struct
                                      where a.active == true
                                      select a).ToList();

            nodeToReturn = RecursiveGetTreeNode(2, outStruct, panelId, userNameField, userIdField, "", fullStruct);

            return nodeToReturn;
        }

        public TreeView GetTreeViewWithPepole(int rowId)
        {
            TreeView strucTreeView = new TreeView();
            strucTreeView.ID = "treeView" + rowId;
            strucTreeView.Nodes.Add(GetStructTreeViewNode("MainContent_chooseUserPanel" + rowId, "MainContent_ParticipentNameTextBox" + rowId, "MainContent_ParticipentIdTextBox"+rowId, false));
            strucTreeView.ExpandAll();
            return strucTreeView;
        }

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
                chooseButton.OnClientClick = "document.getElementById('MainContent_ParticipentIdTextBox" + rowId.ToString() + "').value = " + user.userID + "; " +

                                             "document.getElementById('MainContent_ParticipentNameTextBox" + rowId.ToString() + "').value = '" + user.name +" " +user.@struct+ "'; " +
                                             "document.getElementById('MainContent_chooseUserPanel" + rowId + "').style.visibility = 'hidden';  return false; ";
                //chooseButton.CssClass = "btn btn-default float-right";
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

           //scrollPanel.Controls.Add(GetSearchResults("", rowId));
            scrollPanel.Controls.Add(GetTreeViewWithPepole(rowId));
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
            bool withQueu = main.WithQueueByProcess(processId);
            int participantsCount = ParticipantsList.Count;

            if (participantsCount != 0)
            {
                TableHeaderRow tableHeaderRow = new TableHeaderRow();



                TableHeaderCell tableQueueHeaderCell = new TableHeaderCell();
                tableQueueHeaderCell.Text = "Очередь";
                tableHeaderRow.Cells.Add(tableQueueHeaderCell);

                TableHeaderCell tableEndDateHeaderCell = new TableHeaderCell();
                tableEndDateHeaderCell.Text = "Срок";
                tableHeaderRow.Cells.Add(tableEndDateHeaderCell);

                TableHeaderCell tableParticipentHeaderCell = new TableHeaderCell();
                tableParticipentHeaderCell.Text = "Пользователь";
                tableHeaderRow.Cells.Add(tableParticipentHeaderCell);

                TableHeaderCell tableDeleteParticipentHeaderCell = new TableHeaderCell();
                tableDeleteParticipentHeaderCell.Text = "Удалить";
                tableHeaderRow.Cells.Add(tableDeleteParticipentHeaderCell);

                participantsTable.Rows.Add(tableHeaderRow);
                for (int i = 0; i < participantsCount; i++)
                {
                    TableRow participantRow = new TableRow();

                    TableCell queueCell = new TableCell();
                    if (!withQueu)
                        ParticipantsList[i].ParticipantQueueTextBox.Text = 0.ToString();




                    ParticipantsList[i].ParticipantQueueTextBox.Attributes.Add("readonly","true");
                    queueCell.Controls.Add(ParticipantsList[i].ParticipantQueueTextBox);
                    queueCell.Controls.Add(ParticipantsList[i].ParticipantQueueValidator);
                    participantRow.Cells.Add(queueCell);

                    TableCell endDateCell = new TableCell();
                    //ParticipantsList[i].ParticipantEndDateTextBox.ID = "ParticipentEndDateTextBox" + i;
                    endDateCell.Controls.Add(ParticipantsList[i].ParticipantEndDateTextBox);
                    //endDateCell.Controls.Add(ParticipantsList[i].ParticipantEndDateValidator);

                    participantRow.Cells.Add(endDateCell);

                    TableCell participentCell = new TableCell();
                    //ParticipantsList[i].ParticipantTextBox.Visible = true;
                    //ParticipantsList[i].ParticipantTextBox.ID = "ParticipentIdTextBox" + i;
                    participentCell.Controls.Add(ParticipantsList[i].ParticipantTextBox);
                    participentCell.Controls.Add(ParticipantsList[i].ParticipantUserValidator);
                    participentCell.Controls.Add(ParticipantsList[i].ParticipantNameTextBox);
                    participentCell.Controls.Add(ParticipantsList[i].ParticipantUserNameValidator);


                    Button openPunelButton = new Button();


                    int rowId = i;
                    string tmpstr = ParticipantsList[i].ParticipantNameTextBox.ID.Replace("ParticipentNameTextBox", "");
                    Int32.TryParse(tmpstr, out rowId);
                   // rowId++;



                    openPunelButton.Text = "Выбрать";
                    openPunelButton.OnClientClick = "document.getElementById('MainContent_chooseUserPanel" +
                                                    rowId.ToString() +
                                                    "').style.visibility = 'visible'; return false; ";
                    participentCell.Controls.Add(GetFiexdPanel(rowId));
                    openPunelButton.CssClass = "btn btn-sm btn-default";
                    //participentCell.Controls.Add(GetFiexdPanel(rowId));
                    participentCell.Controls.Add(openPunelButton);
                    participantRow.Cells.Add(participentCell);

                    TableCell deleteParticipentCell = new TableCell();

                    Button deleteParticipentButton = new Button();
                    deleteParticipentButton.CausesValidation = false;
                    deleteParticipentButton.Text = "Удалить";
                    deleteParticipentButton.CommandArgument = rowId.ToString();
                    deleteParticipentButton.ID = "deleteParticipentButton"+ rowId.ToString();
                    deleteParticipentButton.Click +=  DeleteParticipentRow;
                    deleteParticipentButton.CssClass = "btn btn-sm btn-danger";
                    deleteParticipentButton.OnClientClick = "showSimpleLoadingScreen()";
                    deleteParticipentCell.Controls.Add(deleteParticipentButton);
                    participantRow.Cells.Add(deleteParticipentCell);

                    participantsTable.CssClass = "centered-block";


                    participantsTable.Rows.Add(participantRow);
                }

            }

            Button addRowToParticipantButton = new Button();
            addRowToParticipantButton.CausesValidation = false;
            addRowToParticipantButton.Text = "Добавить согласующего";
            addRowToParticipantButton.ID = "addRowToParticipantButton";
            addRowToParticipantButton.CssClass = "btn btn-default float-right";
            addRowToParticipantButton.Click += AddParticipentRow;
            addRowToParticipantButton.OnClientClick = "showSimpleLoadingScreen()";

            TableRow addNewRow = new TableRow();
            TableCell addNewRowCell = new TableCell();
            addNewRowCell.ColumnSpan = 3;
            addNewRowCell.Controls.Add(addRowToParticipantButton);
            addNewRow.Cells.Add(new TableCell());
            addNewRow.Cells.Add(addNewRowCell);
            participantsTable.Rows.Add(addNewRow);
            if (!withQueu)
                participantsTable.CssClass = "noFirstColumn centered-block";
                return participantsTable;
        }
        public Table GetDocumentsTable()
        {
            Table tableToReturn = new Table();
            // tableToReturn.Style["width"] = "70%";

            if (DocumentsList.Count != 0)
            {
                TableHeaderRow headerRow = new TableHeaderRow();
                TableHeaderCell headerCell1 = new TableHeaderCell();
                TableHeaderCell headerCell2 = new TableHeaderCell();
                TableHeaderCell headerCell3 = new TableHeaderCell();

                headerCell1.Text = "Документ";
                headerCell2.Text = "Описание";
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

                    if (currentDocument.DocumentFileUpload != null)
                    {
                        
                        RequiredFieldValidator documentRequiere = new RequiredFieldValidator();
                        documentRequiere.ID = "require" + currentDocument.DocumentFileUpload.ID;
                        documentRequiere.ControlToValidate = currentDocument.DocumentFileUpload.ID;
                        documentRequiere.ErrorMessage = "!";
                        documentRequiere.ForeColor = Color.Red;
                        cell1.Controls.Add(currentDocument.DocumentFileUpload);
                        cell1.Controls.Add(documentRequiere);
                    }

                    if (currentDocument.LinkButtonToDocument != null)
                    {
                        LinkButton newLinkButton = new LinkButton();
                        newLinkButton.ID = currentDocument.LinkButtonToDocument.ID;
                        newLinkButton.CommandArgument = currentDocument.LinkButtonToDocument.CommandArgument;
                        newLinkButton.Text = currentDocument.LinkButtonToDocument.Text;
                        newLinkButton.Click += GetDocumentClick;
                        cell1.Controls.Add(newLinkButton);
                    }
                        

                    cell2.Controls.Add(currentDocument.DocumentCommentTextBox);
                   /* RequiredFieldValidator documentCommentRequiere = new RequiredFieldValidator();
                    documentCommentRequiere.ID = "require" + currentDocument.DocumentCommentTextBox.ID;
                    documentCommentRequiere.ControlToValidate = currentDocument.DocumentCommentTextBox.ID;
                    documentCommentRequiere.ErrorMessage = "!";
                    documentCommentRequiere.ForeColor = Color.Red;
                    cell2.Controls.Add(documentCommentRequiere);
                    */


                    Button DeleteRowButton = new Button();
                    DeleteRowButton.CausesValidation = false;
                    DeleteRowButton.Text = "Удалить";
                    DeleteRowButton.CommandArgument = rowId.ToString();
                    DeleteRowButton.ID = "DeleteRowButton"+rowId;
                    DeleteRowButton.OnClientClick = "showSimpleLoadingScreen()";
                    DeleteRowButton.CssClass = "btn btn-sm btn-danger float-right";
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
            addRowToDocButton.CausesValidation = false;
            addRowToDocButton.ID = "addRowToDocButton";
            addRowToDocButton.Text = "Добавить документ";
            addRowToDocButton.Click += AddDocumentRow;
            addRowToDocButton.OnClientClick = "showSimpleLoadingScreen()";
            addRowToDocButton.CssClass = "btn btn-default float-right";

            TableRow addNewRow = new TableRow();
            TableCell addNewRowCell = new TableCell();
            addNewRowCell.ColumnSpan = 3;
            addNewRowCell.Controls.Add(addRowToDocButton);
            addNewRow.Cells.Add(addNewRowCell);
            tableToReturn.Rows.Add(addNewRow);

            tableToReturn.CssClass = "centered-block";

            return tableToReturn;
        }
        public void Refersh()
        {
        
            int processId = 0;
            Int32.TryParse(HttpContext.Current.Session["processID"].ToString(), out processId);
            if (processId != 0)
            {
                bool withQueue = main.WithQueueByProcess(processId);
                RefreshQueueInParticipantsList(withQueue);
                Processes currentProcess = main.GetProcessById(processId);
                if (currentProcess.status >= 0)
                {
                    SaveAllDiv.Visible = false;
                    SaveAllButton.Enabled = false;
                }
                else
                {
                    SaveAllDiv.Visible = true;
                }
                createNewProcessDiv.Visible = false;
                existingProcessTitleDiv.Visible = true;
                ParticipantsDiv.Visible = true;
                documentsDiv.Visible = true;
                
                commentForVersionDiv.Visible = true;

                ProcessIdLabel.Text = main.GetProcessNameById(processId);

                ParticipantsDiv.Controls.Clear();
                ParticipantsDiv.Controls.Add(GetNewParticipantsTable());
                documentsDiv.Controls.Clear();
                documentsDiv.Controls.Add(GetDocumentsTable());
            }
            else
            {
                commentForVersionDiv.Visible = false;
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
                ParticipantsList.Add(CreateParticipant(i,0, "", "", "", ""));
            }
            Refersh();
        }
        protected void SaveAllButton_Click(object sender, EventArgs e)
        {
            int processId = 0;
            Int32.TryParse(HttpContext.Current.Session["processID"].ToString(), out processId);
            int tmp = (from a in ParticipantsList
                join b in ParticipantsList
                    on a.ParticipantTextBox.Text equals b.ParticipantTextBox.Text
                select a).Count();
            if (tmp > 4)
            {
                int error = 1;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Согласующие дублируются!');</script>");
                return;
            }
            {
                int userId = 1;
                var sesUserId = HttpContext.Current.Session["userID"];
                if (sesUserId != null)
                {
                    Int32.TryParse(sesUserId.ToString(), out userId);
                }

                if ((from a in ParticipantsList where a.ParticipantTextBox.Text == userId.ToString() select a).Count() >
                    0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Вы не можете быть согласующим!');</script>");
                    return;
                }

            }
            if (DocumentsList.Count < 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Не прикреплен документ!');</script>");
                return;
            }


            #region do participants





            List<Participant> participantsToAdd;
            List<Participant> oldParticipants = main.GetParticipantsListInProcess(processId);
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
            #endregion
            #region do documents
            List<DocumentsClass> documentsToAdd;
            List<DocumentsClass> documentsToUpdateVersion = new List<DocumentsClass>();


           

            List<DocumentsClass> documentsToUpdateComment = new List<DocumentsClass>();
            List<DocumentsClass> oldDocuments = GetDocumentsInProcess(processId);

            if (oldDocuments.Count>0)
            {                
                documentsToAdd = (from a in DocumentsList where a.DocumentFileUpload!=null select a).ToList();
                documentsToUpdateVersion = (from a in oldDocuments join b in DocumentsList on a.DocumentId equals b.DocumentId select a).OrderBy(mc=>mc.DocumentId).ToList();
                documentsToUpdateComment = (from a in oldDocuments join b in DocumentsList on a.DocumentId equals b.DocumentId select b).OrderBy(mc => mc.DocumentId).ToList();
                List<DocumentsClass>  documentsToDelete = oldDocuments.Except(documentsToUpdateVersion).ToList();
                foreach (DocumentsClass docToDel in documentsToDelete)
                {
                    main.KillDocument(docToDel.DocumentId);
                }
            }
            else
            {

                documentsToAdd = DocumentsList;
            }
            #endregion
            foreach (Participant currentParticipant in participantsToAdd)
            {              
                int userId = 0;
                int queue = 0;
                DateTime endDateTime = DateTime.MinValue;
                
                Int32.TryParse(currentParticipant.ParticipantTextBox.Text, out userId);
                Int32.TryParse(currentParticipant.ParticipantQueueTextBox.Text, out queue);

                DateTime.TryParse(currentParticipant.ParticipantEndDateTextBox.Text, out endDateTime);

                DateTime? nullEndDateTime = endDateTime;

                if (nullEndDateTime.Value.Year < DateTime.Now.Year - 100)
                    nullEndDateTime = null;
                main.CreateNewParticipent(processId, userId, queue, nullEndDateTime);
            }

            ProcessVersions lastProcessVerson = main.GetLastVersionInProcess(processId);
            int lastVesion = 0;
            if (lastProcessVerson != null)
            {
                lastVesion = lastProcessVerson.version+1;
            }
            int processVersionId = main.CreateNewProcessVersion(processId, commentForVersionTextBox.Text, lastVesion, "Новая версия процесса");

            Approvment approvment = new Approvment();
            approvment.ContinueApprove(processId);

            int iii = 0;
            foreach (DocumentsClass document in documentsToUpdateVersion)
            {

                main.SetDocumentToVersion(document.DocumentId, processVersionId, documentsToUpdateComment[iii].DocumentCommentTextBox.Text);
                iii++;
            }

            main.MakeAllParticipantsIsNew(processId);
            //List<Participant> oldParticipants = main.GetParticipantsListInProcess(processId);

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




            Session["direction"] = 0;
            Response.Redirect("Dashboard.aspx");
        }
        public void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ParticipantsList = new List<Participant>();
                DocumentsList = new List<DocumentsClass>();
                int processId = -1;
                var procIdStr = HttpContext.Current.Session["processID"];
                if(procIdStr == null)
                    Response.Redirect("Dashboard.aspx");
                Int32.TryParse(procIdStr.ToString(), out processId);
                if (processId <0)
                    Response.Redirect("Dashboard.aspx");
                if (processId != 0)
                {
                    int i = 0;
                    foreach (Participants participant in main.GetParticipantsInProcess(processId))
                    {
                        Users user = main.GetUserById(participant.fk_user);
                        ParticipantsList.Add(CreateParticipant(i, participant.participantID,participant.queue.ToString(),participant.dateEnd.ToString(),participant.fk_user.ToString(), user.name+" "+user.@struct));
                        i++;
                    }
                    
                    DocumentsList = GetDocumentsInProcess(processId);
                    commentForVersionTextBox.Text = main.GetCommentForLastVersion(processId);
                }
            }
            Refersh();
        }
    }
}
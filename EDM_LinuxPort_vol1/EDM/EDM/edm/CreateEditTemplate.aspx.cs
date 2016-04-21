using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class CreateEditTamplate : System.Web.UI.Page
    {
        ProcessMainFucntions _main = new ProcessMainFucntions();
        ProcessEdit proc = new ProcessEdit();
        public List<ProcessEdit.Participant> ParticipantsTemplateList = new List<ProcessEdit.Participant>();
        public void Refresh()
        {
            int templateId = 0;
            var templateIdSes = Session["ProcessTemplateId"];
            Int32.TryParse(templateIdSes.ToString(), out templateId);
            ProcessTemplate currentTemplate = _main.GetProcessTemplateById(templateId);
            bool withQueue = _main.WithQueueuByType(currentTemplate.type);

            Table participantsTable = new Table();

            TableHeaderRow tableHeaderRow = new TableHeaderRow();

            TableHeaderCell tableQueueHeaderCell = new TableHeaderCell();
            tableQueueHeaderCell.Text = "Очередь";
            tableHeaderRow.Cells.Add(tableQueueHeaderCell);

            TableHeaderCell tableParticipentHeaderCell = new TableHeaderCell();
            tableParticipentHeaderCell.Text = "Пользователь";
            tableHeaderRow.Cells.Add(tableParticipentHeaderCell);

            TableHeaderCell tableDeleteParticipentHeaderCell = new TableHeaderCell();
            tableDeleteParticipentHeaderCell.Text = "Удалить";

            tableHeaderRow.Cells.Add(tableDeleteParticipentHeaderCell);

            participantsTable.Rows.Add(tableHeaderRow);
            //int i = 0;
            if (ParticipantsTemplateList!=null)
            foreach (ProcessEdit.Participant currentParticipant  in ParticipantsTemplateList)
            {
                if (!withQueue)
                    currentParticipant.ParticipantQueueTextBox.Text = 0.ToString();
                currentParticipant.ParticipantQueueTextBox.Attributes.Add("readonly", "true");

                TableRow tableRow = new TableRow();

                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();

                cell1.Controls.Add(currentParticipant.ParticipantQueueTextBox);


                Button openPunelButton = new Button();
                string tmpstr = currentParticipant.ParticipantNameTextBox.ID.Replace("ParticipentNameTextBox", "");
                int rowId = 0;
                Int32.TryParse(tmpstr, out rowId);

                openPunelButton.Text = "Выбрать";
                openPunelButton.OnClientClick = "document.getElementById('ctl00_MainContent_chooseUserPanel" +
                                                rowId.ToString() +
                                                "').style.visibility = 'visible'; return false; ";



                cell2.Controls.Add(proc.GetFiexdPanel(rowId,2));
                openPunelButton.CssClass = "btn btn-sm btn-default";
                cell2.Controls.Add(openPunelButton);
                cell2.Controls.Add(currentParticipant.ParticipantNameTextBox);
                cell2.Controls.Add(currentParticipant.ParticipantTextBox);
                    cell2.Controls.Add(currentParticipant.ParticipantUserValidator);
                                    Button deleteParticipant = new Button();
                deleteParticipant.Text = "Удалить";
                deleteParticipant.CausesValidation = false;
                deleteParticipant.CommandArgument = rowId.ToString();
                deleteParticipant.ID = "deleteTemplateParticipentButton" + rowId.ToString();
                deleteParticipant.Click += DeleteTemplateParticipentRow;
                deleteParticipant.CssClass = "btn btn-sm btn-danger";
                deleteParticipant.OnClientClick = "showSimpleLoadingScreen()";
                cell3.Controls.Add(deleteParticipant);

                tableRow.Cells.Add(cell1);
                tableRow.Cells.Add(cell2);
                tableRow.Cells.Add(cell3);
                
                participantsTable.Rows.Add(tableRow);
                //i++;
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

            participantsDiv.Controls.Clear();
            participantsDiv.Controls.Add(participantsTable);

        }

        protected void AddParticipentRow(object sender, EventArgs e)
        {
            int rowId = 0;
            if (ParticipantsTemplateList.Count > 0)
            {
                string tmpstr = ParticipantsTemplateList[ParticipantsTemplateList.Count - 1].ParticipantNameTextBox.ID.Replace("ParticipentNameTextBox", "");
                Int32.TryParse(tmpstr, out rowId);
                rowId++;
            }
            else
            {
                rowId = 0;
            }
            ParticipantsTemplateList.Add(proc.CreateParticipant(rowId, 0, "", "", "", ""));
            Refresh();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {

                int templateId = 0;
                var templateIdSes = Session["ProcessTemplateId"];
                Int32.TryParse(templateIdSes.ToString(), out templateId);
                ProcessTemplate currentTemplate = _main.GetProcessTemplateById(templateId);

                TemplateTitleTextBox.Text = currentTemplate.title;
                TemplateNameTextBox.Text = currentTemplate.name;
                TemplateContentTextBox.Text = currentTemplate.content_;
                AprrovalTypeLabel.Text = _main.GetProcessTypeNameByType(currentTemplate.type);
                int submitterId = 0;
                Int32.TryParse(currentTemplate.fk_submitter.ToString(), out submitterId);
                int processCharacterId = 0;
                Int32.TryParse(currentTemplate.fk_processCharacter.ToString(), out processCharacterId);
                SubmitterDropDown.Items.AddRange(_main.GetSubmittersList(submitterId));
                ProcessCharacterDD.Items.AddRange(_main.GetProcessCharacterList(processCharacterId));
                AllowChangeProcessCheckBox.Checked = currentTemplate.allowEditProcess;
                ChooseStructDropDown.Items.AddRange(_main.GetAllStructToDropDown(currentTemplate.fk_struct));

                ParticipantsTemplateList = new List<ProcessEdit.Participant>();

                List<ProcessTemplateParticipant> allTemplateParticipants = _main.GetProcessTemplateParticipantsInTemplate(templateId);
                int i = 0;
                foreach (ProcessTemplateParticipant currentTamplate in allTemplateParticipants)
                {
                    ParticipantsTemplateList.Add(proc.CreateParticipant(i, currentTamplate.processTemplateParticipantId, currentTamplate.queue.ToString(), "", currentTamplate.fk_user.ToString(), _main.GetUserById(currentTamplate.fk_user).name));
                    i++;
                }
               
                Session["ParticipantsTemplateList"] = ParticipantsTemplateList;
            }

            ParticipantsTemplateList = (List<ProcessEdit.Participant>) Session["ParticipantsTemplateList"];
            int processTemplateId = (int) Session["ProcessTemplateId"] ;

            Refresh();

        }

        public void DeleteTemplateParticipentRow(object sender, EventArgs e)
        {
            int rowId = 0;
            Button delButton = (Button) sender;
            Int32.TryParse(delButton.CommandArgument, out rowId);

            foreach (ProcessEdit.Participant currentParticipant in ParticipantsTemplateList)
            {
                int newRow = -1;
                string tmpstr = currentParticipant.ParticipantNameTextBox.ID.Replace("ParticipentNameTextBox", "");
                Int32.TryParse(tmpstr, out newRow);
                if (newRow == rowId)
                {
                    ParticipantsTemplateList.Remove(currentParticipant);
                    Refresh();
                    return;
                }
            }
        }

        protected void SaveAllButton_Click(object sender, EventArgs e)
        {

            int tmp = (from a in ParticipantsTemplateList
                       join b in ParticipantsTemplateList
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

                if ((from a in ParticipantsTemplateList where a.ParticipantTextBox.Text == userId.ToString() select a).Count() >
                    0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Вы не можете быть согласующим!');</script>");
                    return;
                }

            }

            int templateId = 0;
            var templateIdSes = Session["ProcessTemplateId"];
            Int32.TryParse(templateIdSes.ToString(), out templateId);

            string submitterString = SubmitterDropDown.SelectedValue;
            int submitterId = 0;
            Int32.TryParse(submitterString, out submitterId);

            string processCharacterString = ProcessCharacterDD.SelectedValue;
            int processCharacterId = 0;
            Int32.TryParse(processCharacterString, out processCharacterId);

            int fkStruct = 2;
            Int32.TryParse(ChooseStructDropDown.SelectedValue, out fkStruct);
            _main.SetTemplateParams(TemplateNameTextBox.Text, TemplateTitleTextBox.Text, TemplateContentTextBox.Text, templateId, submitterId, processCharacterId ,fkStruct, AllowChangeProcessCheckBox.Checked);

            #region do participants
            List<ProcessEdit.Participant> participantsToAdd;
            List<ProcessEdit.Participant> oldParticipants = _main.GetParticipantsInTempolate(templateId);
            if (oldParticipants.Count > 0)
            {
                List<ProcessEdit.Participant> participantsToDelete = oldParticipants.Except((from a in ParticipantsTemplateList
                                                                                             join b in oldParticipants
                                                                                 on a.ParticipantTextBox.Text equals b.ParticipantTextBox.Text
                                                                                 where 
                                                                                 a.ParticipantQueueTextBox.Text == b.ParticipantQueueTextBox.Text
                                                                                 select b).Distinct().ToList()).ToList();
                foreach (ProcessEdit.Participant currentParticipant in participantsToDelete)
                {
                    _main.KillParticipantTemplate(currentParticipant.ParticipantId);
                }

                participantsToAdd = ParticipantsTemplateList.Except((from a in ParticipantsTemplateList
                                                                     join b in oldParticipants
                                                             on a.ParticipantTextBox.Text equals b.ParticipantTextBox.Text
                                                             where a.ParticipantQueueTextBox.Text == b.ParticipantQueueTextBox.Text
                                                             select a).Distinct().ToList()).ToList();
            }
            else
            {
                participantsToAdd = ParticipantsTemplateList;
            }
            #endregion

            foreach (ProcessEdit.Participant currentParticipant in participantsToAdd)
            {
                int userId = 0;
                int queue = 0;
                DateTime endDateTime = DateTime.MinValue;

                Int32.TryParse(currentParticipant.ParticipantTextBox.Text, out userId);
                Int32.TryParse(currentParticipant.ParticipantQueueTextBox.Text, out queue);


                DateTime? nullEndDateTime = endDateTime;

                if (nullEndDateTime.Value.Year < DateTime.Now.Year - 100)
                {
                    endDateTime = DateTime.Now;
                    endDateTime = endDateTime.AddHours(24);
                }
                _main.CreateNewTemplateParticipent(templateId, userId, queue);
            }




            Response.Redirect("CreateEditTemplate.aspx");
        }

        protected void goBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("TemplatesList.aspx");
        }
    }
}
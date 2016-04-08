using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class DocumentView : System.Web.UI.Page
    {
        LogHandler log = new LogHandler();
        public ProcessMainFucntions main = new ProcessMainFucntions();
        public Panel GetChooseFromInnerProcessDocumentsPanel(int procId,int userId)
        {
            Panel panelToReturn = new Panel();
            panelToReturn.ID = "chooseInnerProcPanel";
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

            Panel scrollPanelComment = new Panel();
            scrollPanelComment.ID = "chooseInnerProcPanelScrollComment";
            scrollPanelComment.Style.Add("height","40%");
            scrollPanelComment.ScrollBars = ScrollBars.Vertical;

            Panel scrollPanelDocs = new Panel();
            scrollPanelDocs.ID = "chooseInnerProcPanelScrollDocs";
            scrollPanelDocs.Style.Add("height", "40%");
            scrollPanelDocs.ScrollBars = ScrollBars.Vertical;

            Processes childProc = main.GetChildProcess(procId, userId);
            ProcessVersions childLastVersion = main.GetProcessVersionsLastVerson(childProc.processID);
            List<Steps> childLastVersionSteps = main.GetStepsInProcessVersion(childLastVersion.processVersionID);

            Label title1 = new Label();
            Label title2 = new Label();
            title1.Text = "Комментарии:";
            
            title2.Text = "Документы:";

            CheckBoxList commentsList = new CheckBoxList();
            RadioButtonList docsList = new RadioButtonList();

            commentsList.CssClass = "RBL";
            docsList.CssClass = "RBL";

            foreach (Steps curStep in childLastVersionSteps)
            {
                if (curStep.comment.Any())
                {           
                    ListItem newCommentItem = new ListItem();
                    newCommentItem.Text = curStep.comment;
                    newCommentItem.Value = curStep.comment;
                    commentsList.Items.Add(newCommentItem);
                }
                DocumentsInStep doc = main.GetDocumentInStep(curStep.stepID);
                if (doc != null)
                {
                    ListItem newDocItem = new ListItem();
                    newDocItem.Text = doc.documentName;
                    newDocItem.Value = doc.documentsInStepId.ToString();
                    docsList.Items.Add(newDocItem);
                }
            }

            
                
            scrollPanelComment.Controls.Add(commentsList);
            scrollPanelDocs.Controls.Add(docsList);
            panelToReturn.Controls.Add(title1);
            panelToReturn.Controls.Add(scrollPanelComment);
            panelToReturn.Controls.Add(title2);
            panelToReturn.Controls.Add(scrollPanelDocs);

            Button setAllButton = new Button();
            setAllButton.Style.Add("width","100%");
            setAllButton.Text = "Ок"; 
            setAllButton.OnClientClick = "setValues(); return false;";

            Button cancelButton = new Button();
            cancelButton.Style.Add("width", "100%");
            cancelButton.Text = "Отмена";

            cancelButton.OnClientClick = "document.getElementById('MainContent_chooseInnerProcPanel').style.visibility = 'hidden'; return false; ";

            panelToReturn.Controls.Add(setAllButton);
            panelToReturn.Controls.Add(cancelButton);

            return panelToReturn;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int proc;
            var procIdStr = HttpContext.Current.Session["processID"];

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                HttpContext.Current.Session["processID"] = Request.QueryString["id"];

            if (procIdStr == null)
                Response.Redirect("Dashboard.aspx");

            int.TryParse(Session["processID"].ToString(), out proc);
            //////////////////////////////////////////////////
            historyDiv.Controls.Add(main.GetHistoryTable(-1, proc));
            if (!Page.IsPostBack)
            {
                

                EDMdbDataContext dataContext = new EDMdbDataContext();
                Users initiator = main.GetUserById(main.GetProcessById(proc).fk_initiator);
                InitiatorLabel.Text = "Инициатор :" + initiator.name + "   " +initiator.@struct;

                bool procHasChild = (main.GetChildProcess(proc, (int) userId) != null);
                if (procHasChild)
                {
                    useInnerProc.Visible = true;
                    useInnerProc.Controls.Add(GetChooseFromInnerProcessDocumentsPanel(proc, (int)userId));
                    OpenFixedPanelButton.OnClientClick = "document.getElementById('MainContent_chooseInnerProcPanel').style.visibility = 'visible'; return false; ";
                }

                int userID;
                int.TryParse(Session["userID"].ToString(), out userID);
                int procMaxVersion =
                    (from a in dataContext.ProcessVersions where a.fk_process == proc && a.active select a)
                        .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                #region Security :)

                var userParticInProc =
                    (from a in dataContext.Participants
                     where a.active && a.fk_user == (int)userId && a.fk_process == proc
                     select a.participantID).ToList();

                if (!userParticInProc.Any())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Вы не являетесь участником этого процесса. Ваши действия занесены в лог'); showSimpleLoadingScreen(); window.location.href = '../Default.aspx'", true);
                    // кикнуть
                }

                var stepsForuser = (from a in dataContext.Steps
                    where a.active && a.fk_processVersion == procMaxVersion && a.fk_participent == userParticInProc[0]
                    select a).FirstOrDefault();

                if (stepsForuser != null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Вы уже приняли участие в этом процессе'); showSimpleLoadingScreen(); window.location.href = '../Default.aspx'", true);
                }

                #endregion Secutiry

                ProcessVersions procVer =
                    (from b in dataContext.ProcessVersions where b.active && b.processVersionID == procMaxVersion select b)
                        .FirstOrDefault();
                if (procVer != null) { procVer.status = "В работе " + (from a in dataContext.Users where a.userID == userID select a.name).FirstOrDefault() + " / " + DateTime.Now.ToShortDateString(); } else throw new Exception("Невозможно присвоить версии процесса в статус. Скорее всего он не существует");
                dataContext.SubmitChanges();

                LabelComment.Text =
                    (from a in dataContext.ProcessVersions
                     where a.active && a.processVersionID == procMaxVersion
                     select a.comment).FirstOrDefault();

                #region isNew

                Participants part =
                    (from a in dataContext.Participants
                        where a.active && a.fk_user == userID && a.fk_process == proc
                        select a).FirstOrDefault();

                if ( part!= null && part.isNew == true)
                {
                    part.isNew = false;
                    part.dateIsNew = DateTime.Now;
                    dataContext.SubmitChanges();
                }

                #endregion

                #region отображать или нет кнопки
                if (
                    (from a in dataContext.Processes where a.active && a.processID == proc select a.status)
                        .FirstOrDefault() == 1) // Изменить статус для согласованного
                {
                    ApproveButton.Enabled = false;
                    RejectButton.Enabled = false;
                }
                if (
                    (from a in dataContext.Processes where a.active && a.processID == proc select a.type).FirstOrDefault
                        () == "review")
                {
                    RejectButton.Enabled = false;
                    RejectButton.Visible = false;

                    ApproveButton.Text = "Отправить комментарий";
                }

                string prevComent = string.Empty;
                var steps = (from a in dataContext.Participants where a.active && a.fk_user == userID && a.fk_process == proc
                             join b in dataContext.Steps on a.participantID equals b.fk_participent where b.active
                             select b).OrderByDescending(v=>v.fk_processVersion).ToList();

                if (steps.Any())
                {
                    prevComent = steps[steps.Count - 1].comment;
                    ButtonPrevComment.Visible = true;
                    LabelPrevComment.Text = prevComent;
                }

                #endregion

                RefreshGrid(dataContext, procMaxVersion);

            }
        }
        private void RefreshGrid(EDMdbDataContext dataContext, int procMaxVersion)
        {       
           var documents = (from a in dataContext.Documents
                         join b in dataContext.ProcVersionDocsMap
                             on a.documentID equals b.fk_documents
                         where b.active == true
                               && a.active == true
                               && b.fk_processVersion == procMaxVersion
                         select new {a.documentID,a.documentName,b.documentComment}).ToList();

            docGridView.DataSource = documents;


            BoundField boundField = new BoundField();
            boundField.DataField = "documentID";
            boundField.HeaderText = "ИН";
            boundField.Visible = true;
            docGridView.Columns.Add(boundField);


            ButtonField coluButtonField = new ButtonField();
            coluButtonField.HeaderText = "Документ";
            coluButtonField.DataTextField = "documentName";
            coluButtonField.ButtonType = ButtonType.Link;      
            coluButtonField.CommandName = "openDocument"; 
            docGridView.Columns.Add(coluButtonField);

            BoundField boundField2 = new BoundField();
            boundField2.DataField = "documentComment";
            boundField2.HeaderText = "Примечание автора";
            boundField2.Visible = true;
            docGridView.Columns.Add(boundField2);

            DataBind();
        }
        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            //if (CommentTextBox.Text.Any())
            {
            int proc;
            int userId;

            int.TryParse(Session["userID"].ToString(), out userId);
            int.TryParse(Session["processID"].ToString(), out proc);

            Approvment approve = new Approvment();

            EDMdbDataContext dataContext = new EDMdbDataContext();
            OtherFuncs of = new OtherFuncs();

            int procMaxVersion =
                    (from a in dataContext.ProcessVersions where a.fk_process == proc && a.active select a)
                        .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                #region Security :)

                var userParticInProc =
                    (from a in dataContext.Participants
                     where a.active && a.fk_user == (int)userId && a.fk_process == proc
                     select a.participantID).ToList();

                if (!userParticInProc.Any())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Вы не являетесь участником этого процесса. Ваши действия занесены в лог'); showSimpleLoadingScreen(); window.location.href = '../Default.aspx'", true);
                    // кикнуть
                }

                var stepsForuser = (from a in dataContext.Steps
                                    where a.active && a.fk_processVersion == procMaxVersion && a.fk_participent == userParticInProc[0]
                                    select a).FirstOrDefault();

                if (stepsForuser != null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Вы уже приняли участие в этом процессе'); showSimpleLoadingScreen(); window.location.href = '../Default.aspx'", true);
                }

                #endregion Secutiry

                var sss = Convert.ToInt32(RadioButtonList1.Items[RadioButtonList1.SelectedIndex].Value);

                int stepId = approve.AddApprove(userId, procMaxVersion, CommentTextBox.Text, Convert.ToInt32(RadioButtonList1.Items[RadioButtonList1.SelectedIndex].Value));
                log.AddInfo("Согласована версия процесса с  id= "+ procMaxVersion +", и комментарием: '"+ CommentTextBox.Text+"'");
                if (ExistingDocIdTextBox.Text != "")
                {
                    int tmp = 0;
                    Int32.TryParse(ExistingDocIdTextBox.Text, out tmp);
                    main.CreateNewStepDocumentConnection(stepId, tmp);
                }
                else if (AddStepFileFileUpload.HasFile)
                {
                    try
                    {
                        ProcessMainFucntions main = new ProcessMainFucntions();
                        int docId = main.CreateNewStepDocument(AddStepFileFileUpload.FileName, stepId, "");

                        string directoryToSave =
                            HttpContext.Current.Server.MapPath("~/edm/documents/" + proc + "stepsDocs/" + docId + "/");
                        if (!Directory.Exists(directoryToSave))
                        {
                            Directory.CreateDirectory(directoryToSave);
                        }
                        AddStepFileFileUpload.SaveAs(directoryToSave + AddStepFileFileUpload.FileName);
                        of.DocAddmd5(docId, directoryToSave + AddStepFileFileUpload.FileName);
                    }
                    catch (Exception ex)
                    {
                        log.AddError("Ошибка записи документа в процессе " + proc + "; " + ex.ToString());
                        HttpContext.Current.Response.Redirect("Dashboard.aspx"); //LOG
                    }
                }

                HttpContext.Current.Response.Redirect("Dashboard.aspx");
            }
           /* else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Отсутствует комментарий к процессу. Пожалуйста заполните!');</script>");
            }*/

        }
        protected void RejectButton_Click(object sender, EventArgs e)
        {
            //if (CommentTextBox.Text.Any())
            {
                int proc;
                int userId;

                int.TryParse(Session["userID"].ToString(), out userId);
                int.TryParse(Session["processID"].ToString(), out proc);

                Approvment approve = new Approvment();

                EDMdbDataContext dataContext = new EDMdbDataContext();

                int procMaxVersion =
                    (from a in dataContext.ProcessVersions where a.fk_process == proc && a.active select a)
                        .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                #region Security :)

                var userParticInProc =
                    (from a in dataContext.Participants
                     where a.active && a.fk_user == (int)userId && a.fk_process == proc
                     select a.participantID).ToList();

                if (!userParticInProc.Any())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Вы не являетесь участником этого процесса. Ваши действия занесены в лог'); showSimpleLoadingScreen(); window.location.href = '../Default.aspx'", true);
                    // кикнуть
                }

                var stepsForuser = (from a in dataContext.Steps
                                    where a.active && a.fk_processVersion == procMaxVersion && a.fk_participent == userParticInProc[0]
                                    select a).FirstOrDefault();

                if (stepsForuser != null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Вы уже приняли участие в этом процессе'); showSimpleLoadingScreen(); window.location.href = '../Default.aspx'", true);
                }

                #endregion Secutiry

                int stepId = approve.RejectApprove(userId, procMaxVersion, CommentTextBox.Text);

                log.AddInfo("Отклонена версия процесса с  id= " + procMaxVersion + ", и комментарием: '" + CommentTextBox.Text + "'");

                if (ExistingDocIdTextBox.Text != "")
                {
                    int tmp = 0;
                    Int32.TryParse(ExistingDocIdTextBox.Text, out tmp);
                    main.CreateNewStepDocumentConnection(stepId, tmp);
                }
                else if (AddStepFileFileUpload.HasFile)
                {
                    try
                    {
                        ProcessMainFucntions main = new ProcessMainFucntions();
                        int docId = main.CreateNewStepDocument(AddStepFileFileUpload.FileName, stepId, "");

                        string directoryToSave =
                            HttpContext.Current.Server.MapPath("~/edm/documents/" + proc + "stepsDocs/" + docId + "/");
                        if (!Directory.Exists(directoryToSave))
                        {
                            Directory.CreateDirectory(directoryToSave);
                        }
                        AddStepFileFileUpload.SaveAs(directoryToSave + AddStepFileFileUpload.FileName);
                    }
                    catch (Exception ex)
                    {
                        log.AddError("Ошибка записи документа в процессе " + proc + "; " + ex.ToString());
                        HttpContext.Current.Response.Redirect("Dashboard.aspx"); //LOG
                    }
                }



                HttpContext.Current.Response.Redirect("Dashboard.aspx");
            }
            /*else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Отсутствует комментарий к процессу. Пожалуйста заполните!');</script>");
            }*/
        }       
        protected void docGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "openDocument")
            {
                    int rowId = -1;
                    GridView grid = (GridView) sender;
                    Int32.TryParse(e.CommandArgument.ToString(), out rowId);
                    if (rowId<0) return;

                    GridViewRow row = grid.Rows[rowId];
                    DataControlFieldCell field1 = (DataControlFieldCell) row.Controls[0];
                    DataControlFieldCell field2 = (DataControlFieldCell) row.Controls[1];
                    Control contr = (Control) field2.Controls[0];
                    int docId = -1;
                    Int32.TryParse(field1.Text, out docId);
                    if (docId < 0) return;

                    int procId=-1;
                    int.TryParse(Session["processID"].ToString(), out procId);
                    if (procId < 0) return;
                   // EDMdbDataContext edmDb = new EDMdbDataContext();
                /*    string fileName =
                     (from a in edmDb.Documents where a.documentID == docId select a.documentName).FirstOrDefault();
                    if (fileName==null)
                    return;
                    */
                    
                main.DownloadFile(docId);
              
            }
        }
        protected void goBackButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("Dashboard.aspx");
        }
        protected void ButtonPrevComment_Click(object sender, EventArgs e)
        {
            bool isPrevCommentShow;
            bool.TryParse(Session["isPrevCommentShow"].ToString(), out isPrevCommentShow);

            if (isPrevCommentShow)
            {
                LabelPrevComment.Visible = true;
                ButtonPrevComment.Text = "Скрыть Ваш комментарий из предыдущей версии процесса";
                Session["isPrevCommentShow"] = false;
            }
            else
            {
                LabelPrevComment.Visible = false;
                ButtonPrevComment.Text = "Показать Ваш комментарий из предыдущей версии процесса";
                Session["isPrevCommentShow"] = true;
            }
        }
    }
}
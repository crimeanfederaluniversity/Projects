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
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////

            if (!Page.IsPostBack)
            {

                int proc;
                int.TryParse(Session["processID"].ToString(), out proc);

                EDMdbDataContext dataContext = new EDMdbDataContext();


                int userID;
                int.TryParse(Session["userID"].ToString(), out userID);
                int procMaxVersion =
                    (from a in dataContext.ProcessVersions where a.fk_process == proc && a.active select a)
                        .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                ProcessVersions procVer =
                    (from b in dataContext.ProcessVersions where b.active && b.processVersionID == procMaxVersion select b)
                        .FirstOrDefault();
                if (procVer != null) { procVer.status = "В работе " + (from a in dataContext.Users where a.userID == userID select a.name).FirstOrDefault() + " / " + DateTime.Now.ToShortDateString(); } else throw new Exception("Не возможно присвоить версии процесса в статус. Скорее всего он не существует");
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
            List<Documents> documents =
                (from d in dataContext.Documents where d.fk_processVersion == procMaxVersion && d.active select d)
                    .ToList();

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
            if (CommentTextBox.Text.Any())
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

            int stepId = approve.AddApprove(userId, procMaxVersion, CommentTextBox.Text);

                if (AddStepFileFileUpload.HasFile)
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
                        HttpContext.Current.Response.Redirect("Dashboard.aspx"); //LOG
                    }
                }

                HttpContext.Current.Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Отсутствует комментарий к процессу. Пожалуйста заполните!');</script>");
            }

        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            if (CommentTextBox.Text.Any())
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

                int stepId = approve.RejectApprove(userId, procMaxVersion, CommentTextBox.Text);


                if (AddStepFileFileUpload.HasFile)
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
                        HttpContext.Current.Response.Redirect("Dashboard.aspx"); //LOG
                    }
                }



                HttpContext.Current.Response.Redirect("Dashboard.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Отсутствует комментарий к процессу. Пожалуйста заполните!');</script>");
            }
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
                    EDMdbDataContext edmDb = new EDMdbDataContext();
                    string fileName =
                     (from a in edmDb.Documents where a.documentID == docId select a.documentName).FirstOrDefault();
                    if (fileName==null)
                    return;
                    
                    string path = HttpContext.Current.Server.MapPath("~/edm/documents/" + procId+"/"+ docId +"/"+ fileName);

                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = "text/plain";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                    response.TransmitFile(path);
                    response.Flush();
                    response.End();                  
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
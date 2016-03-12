using System;
using System.Collections.Generic;
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

                // отображать или нет кнопки
                if (
                    (from a in dataContext.Processes where a.active && a.processID == proc select a.status)
                        .FirstOrDefault() == 1) // Изменить статус для согласованного
                {
                    ApproveButton.Enabled = false;
                    RejectButton.Enabled = false;
                }
                //

                int procMaxVersion =
                    (from a in dataContext.ProcessVersions where a.fk_process == proc && a.active select a)
                        .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                LabelComment.Text =
                    (from a in dataContext.ProcessVersions
                        where a.active && a.processVersionID == procMaxVersion
                        select a.comment).FirstOrDefault();

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
            boundField.HeaderText = "ID";
            boundField.Visible = true;
            docGridView.Columns.Add(boundField);


            ButtonField coluButtonField = new ButtonField();
            coluButtonField.HeaderText = "Название документа";
            coluButtonField.DataTextField = "documentName";
            coluButtonField.ButtonType = ButtonType.Link;
            coluButtonField.CommandName = "Open";

            docGridView.Columns.Add(coluButtonField);
            DataBind();
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
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

            approve.AddApprove(userId,procMaxVersion,this);

        }

        protected void RejectButton_Click(object sender, EventArgs e)
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

            approve.RejectApprove(userId, procMaxVersion, CommentTextBox.Text, this);
        }
    }
}
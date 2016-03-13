using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public class DataOne
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int InitiatorId { get; set; }
            public string Initiator { get; set; }
            public int Status { get; set; }
            public string Status4Init { get; set; }
            public string Status4All { get; set; }
            public string Type { get; set; }
            public int Version { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////////

            if (!Page.IsPostBack)
            {
                int userID;
                int direction;
                int.TryParse(Session["userID"].ToString(), out userID);
                int.TryParse(Session["direction"].ToString(), out direction);

                RenderGrid(dashGridView, FillingGrid(direction, userID), direction);

            }
        }

        private static List<DataOne> FillingGrid(int direction, int userID)
        {
            EDMdbDataContext dataContext = new EDMdbDataContext();
            List<DataOne> dataOneSource = new List<DataOne>();

            #region Входяшие

            if (direction == 1)
            {
                var userIsParticipant =
                    (from a in dataContext.Participants where a.active && a.fk_user == userID select a.fk_process)
                        .Distinct().ToList();

                var procesesToUser = (from p in userIsParticipant
                    join proc in dataContext.Processes on p equals proc.processID
                    where proc.active && proc.status == 0
                    select proc).OrderByDescending(startD => startD.startDate).ToList();


                foreach (var proc in procesesToUser)
                {
                    var userQueue = (from a in dataContext.Participants
                        where a.fk_process == proc.processID && a.fk_user == userID && a.active
                        select a.queue).FirstOrDefault(); // очередь пользователя

                    var userParticipant = (from a in dataContext.Participants
                        where a.active && a.fk_process == proc.processID && a.fk_user == userID
                        select a.participantID).FirstOrDefault(); // id-шник этого пользователя в участниках процесса

                    var versionMax =
                        (from a in dataContext.ProcessVersions where a.active && a.fk_process == proc.processID select a)
                            .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                    // если очередь == 0 то это либо || либо --. но первый в очереди
                    if (userQueue == 0)
                    {
                        if (proc.type.Equals("serial"))
                        {
                            var step4Proc =
                                (from a in dataContext.Steps
                                    where a.active && a.fk_processVersion == versionMax
                                    select a).FirstOrDefault();

                            // если Step для -- процесса не существует, значит это первый в очереди и он еще не согласовал
                            if (step4Proc == null)
                            {
                                //addToShow
                                dataOneSource.Add(new DataOne()
                                {
                                    Id = proc.processID,
                                    Name = proc.name,
                                    InitiatorId = proc.fk_initiator,
                                    Status = proc.status
                                });
                            }

                        }
                        else
                        {
                            var step4UserParticipant = (from a in dataContext.Steps
                                where
                                    a.active && a.fk_participent == userParticipant && a.fk_processVersion == versionMax
                                select a).FirstOrDefault();

                            // если пользователь уже согласовал (в ||) то не отображать
                            if (step4UserParticipant == null)
                            {
                                //addToShow
                                dataOneSource.Add(new DataOne()
                                {
                                    Id = proc.processID,
                                    Name = proc.name,
                                    InitiatorId = proc.fk_initiator,
                                    Status = proc.status
                                });
                            }
                        }

                    }
                    else
                    {
                        // если последовательное (queue > 0)

                        var lastStep =
                            (from a in dataContext.Steps where a.active && a.fk_processVersion == versionMax select a)
                                .OrderByDescending(d => d.date).FirstOrDefault();

                        if (lastStep != null)
                        {
                            var lastStepUser = lastStep.fk_participent;

                            var lastUserQueue = (from a in dataContext.Participants
                                where a.active && a.fk_process == proc.processID && a.participantID == lastStepUser
                                select a.queue).FirstOrDefault();

                            if (lastUserQueue + 1 == userQueue)
                            {
                                //addToShow
                                dataOneSource.Add(new DataOne()
                                {
                                    Id = proc.processID,
                                    Name = proc.name,
                                    InitiatorId = proc.fk_initiator,
                                    Status = proc.status
                                });
                            }

                        }

                    }
                }
            }

            #endregion Входяшие

            #region Исходящие

            if (direction == 0)
            {
                var allProcessInitByUser =
                    (from a in dataContext.Processes where a.active && a.fk_initiator == userID select a).ToList();

                foreach (var proc in allProcessInitByUser)
                {
                    dataOneSource.Add(new DataOne()
                    {
                        Id = proc.processID,
                        Name = proc.name,
                        InitiatorId = proc.fk_initiator,
                        Status4Init =
                            (from a in dataContext.ProcessVersions where a.fk_process == proc.processID select a)
                                .OrderByDescending(v => v.version).Select(s => s.status).FirstOrDefault(),
                        Status = proc.status,
                        Type = proc.type
                    });
                }
            }

            #endregion Исходящие

            return dataOneSource;
        }

        public void RenderGrid(GridView gridView, List<DataOne> dataOneSource, int direction)
        {
            EDMdbDataContext dataContext = new EDMdbDataContext();

            if (direction == 1)
            {
                foreach (var itm in dataOneSource)
                {
                    itm.Initiator =
                        (from a in dataContext.Users where a.userID == itm.InitiatorId select a.name).FirstOrDefault();

                }
                gridView.DataSource = dataOneSource;


                BoundField boundField = new BoundField();
                boundField.DataField = "Id";
                boundField.HeaderText = "ID";
                boundField.Visible = true;
                gridView.Columns.Add(boundField);

                BoundField boundField2 = new BoundField();
                boundField2.DataField = "Name";
                boundField2.HeaderText = "Название процесса";
                boundField2.Visible = true;
                gridView.Columns.Add(boundField2);

                BoundField boundField3 = new BoundField();
                boundField3.DataField = "Initiator";
                boundField3.HeaderText = "Инициатор";
                boundField3.Visible = true;
                gridView.Columns.Add(boundField3);

                /* BoundField boundField4 = new BoundField();
                 boundField4.DataField = "Status";
                 boundField4.HeaderText = "Статус";
                 boundField4.Visible = true;
                 gridView.Columns.Add(boundField4);
                 */
                ButtonField coluButtonField = new ButtonField();
                coluButtonField.Text = "Подробнее";
                coluButtonField.ButtonType = ButtonType.Button;
                coluButtonField.CommandName = "ButtonR1";
                gridView.Columns.Add(coluButtonField);

                DataBind();
            }

            if (direction == 0)
            {
                foreach (var itm in dataOneSource)
                {
                    switch (itm.Status)
                    {
                        case -2:
                        {
                            itm.Status4All = "Возвращен на доработку";
                        }
                            break;
                        case -1:
                        {
                            itm.Status4All = "Создан, ждет запуска";
                        }
                            break;
                        case 0:
                        {
                            itm.Status4All = "В процессе";
                        }
                            break;
                        case 1:
                        {
                            itm.Status4All = "Согласован";
                        }
                            break;
                    }
                }
                gridView.DataSource = dataOneSource;

                BoundField boundField = new BoundField();
                boundField.DataField = "Id";
                boundField.HeaderText = "ID";
                boundField.Visible = true;
                gridView.Columns.Add(boundField);

                BoundField boundField2 = new BoundField();
                boundField2.DataField = "Name";
                boundField2.HeaderText = "Название процесса";
                boundField2.Visible = true;
                gridView.Columns.Add(boundField2);

                BoundField boundField22 = new BoundField();
                boundField22.DataField = "Type";
                boundField22.HeaderText = "Тип процесса";
                boundField22.Visible = true;
                gridView.Columns.Add(boundField22);

                BoundField boundField3 = new BoundField();
                boundField3.DataField = "Status4All";
                boundField3.HeaderText = "Общий статус";
                boundField3.Visible = true;
                gridView.Columns.Add(boundField3);

                BoundField boundField4 = new BoundField();
                boundField4.DataField = "Status4Init";
                boundField4.HeaderText = "Текущий статус";
                boundField4.Visible = true;
                gridView.Columns.Add(boundField4);

                ButtonField coluButtonField = new ButtonField();
                coluButtonField.Text = "Редактировать";
                coluButtonField.ButtonType = ButtonType.Button;
                coluButtonField.CommandName = "ButtonR0";
                gridView.Columns.Add(coluButtonField);

                ButtonField coluButtonField2 = new ButtonField();
                coluButtonField2.Text = "История";
                coluButtonField2.ButtonType = ButtonType.Button;
                coluButtonField2.CommandName = "HistoryP";
                gridView.Columns.Add(coluButtonField2);

                ButtonField coluButtonField3 = new ButtonField();
                coluButtonField3.Text = "Запустить";
                coluButtonField3.ButtonType = ButtonType.Button;
                coluButtonField3.CommandName = "StartP";
                gridView.Columns.Add(coluButtonField3);

                ButtonField coluButtonField4 = new ButtonField();
                coluButtonField4.Text = "Удалить";
                coluButtonField4.ButtonType = ButtonType.Button;
                coluButtonField4.CommandName = "DeleteP";
                gridView.Columns.Add(coluButtonField4);

                DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // 0 - исходящие
            // 1 - входящие
            // 2 - архив

            EDMdbDataContext dataContext = new EDMdbDataContext();
            int idProcess = Convert.ToInt32(dashGridView.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

            switch (e.CommandName)

            {
                case "ButtonR1":
                {
                    Session["processID"] = idProcess;
                    Response.Redirect("DocumentView.aspx");
                }
                    break;
                case "ButtonR0":
                {
                    Session["processID"] = idProcess;
                    Response.Redirect("ProcessEdit.aspx");
                }
                    break;
                case "ButtonR2":
                {
                    // do smth
                }
                    break;
                case "HistoryP":
                {

                }
                    break;
                case "StartP":
                {
                    Processes process =
                        (from a in dataContext.Processes where a.active && a.processID == idProcess select a)
                            .FirstOrDefault();
                    if (process != null)
                    {
                        process.status = 0;
                        dataContext.SubmitChanges();
                        Response.Redirect("Dashboard.aspx");
                    }
                    else if (process.status == 0) Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> confirm('Процесс уже запущен!');</script>");
                        else Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> confirm('Ошибка запуска процесса!');</script>");
                    }
                    break;
                case "DeleteP":
                {
                    Processes process =
                        (from a in dataContext.Processes where a.active && a.processID == idProcess select a)
                            .FirstOrDefault();
                        if (process != null)
                        {
                            process.active = false;
                    dataContext.SubmitChanges();
                        Response.Redirect("Dashboard.aspx");
                        }
                        else Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> confirm('Ошибка удаления процесса!');</script>");
                    }
                    break;
            }
        }

        protected void dashGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int id = e.Row.RowIndex;
                    Button btnDel = (Button) e.Row.Cells[8].Controls[0];
                    Button btnStart = (Button)e.Row.Cells[7].Controls[0];
                        btnDel.OnClientClick = "javascript: if (confirm('Вы уверены что хотите удалить?') == true) {__doPostBack('ctl00$MainContent$dashGridView','DeleteP$"+id+"')} else return false";
                        btnStart.OnClientClick = "javascript: if (confirm('Вы уверены что хотите запустить процесс согласования?') == true) {__doPostBack('ctl00$MainContent$dashGridView','StartP$" + id + "')} else return false";
                }
            }

        }




    }
}
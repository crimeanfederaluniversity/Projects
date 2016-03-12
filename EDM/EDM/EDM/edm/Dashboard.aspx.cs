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
                var procesesToUser = (from p in dataContext.Participants
                    where p.fk_user == userID && p.active
                    join proc in dataContext.Processes on p.fk_process equals proc.processID
                    where proc.active && proc.status == 0
                    select proc).OrderByDescending(startD => startD.startDate).ToList();

                foreach (var proc in procesesToUser)
                {
                    var userQueue = (from a in dataContext.Participants
                        where a.fk_process == proc.processID && a.fk_user == userID && a.active
                        select a.queue).FirstOrDefault(); // очередь пользователя

                    if (userQueue == 0)
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
                    else
                    {
                        List<Participants> allParticipantsInProcess =
                            (from a in dataContext.Participants
                                where a.fk_process == proc.processID && a.active
                                select a).OrderByDescending(q => q.queue).ToList();

                        int versionMax =
                            (from a in dataContext.ProcessVersions where a.fk_process == proc.processID select a)
                                .OrderByDescending(v => v.version).Select(v => v.version).FirstOrDefault();


                        foreach (var partic in allParticipantsInProcess)
                        {
                            var step = (from a in dataContext.Steps
                                where
                                    a.fk_processVersion == versionMax && a.active &&
                                    a.fk_participent == partic.participantID
                                select a).FirstOrDefault();

                            // если существует Steps для текущего participant
                            if (step != null)
                            {
                                // очередь для этого участника
                                int currentPqueue =
                                    (from a in dataContext.Participants
                                        where a.active && a.participantID == partic.participantID
                                        select a.queue).FirstOrDefault();

                                // если наш пользователь следующий в очереди то отображать
                                if (currentPqueue + 1 == userQueue)
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
                                else
                                {
                                    break;
                                }
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
                        Status = proc.status
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

                BoundField boundField4 = new BoundField();
                boundField4.DataField = "Status";
                boundField4.HeaderText = "Статус";
                boundField4.Visible = true;
                gridView.Columns.Add(boundField4);

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
                coluButtonField.Text = "Подробнее";
                coluButtonField.ButtonType = ButtonType.Button;
                coluButtonField.CommandName = "ButtonR0";
                gridView.Columns.Add(coluButtonField);

                DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // 0 - исходящие
            // 1 - входящие
            // 2 - архив
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

            }
        }
    }
}
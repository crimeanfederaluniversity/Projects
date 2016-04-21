using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public class TableActions
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

        private Dashboard _dashboard;
        private DocumentView _docView;

        public TableActions(Dashboard dashboard)
        {
            _dashboard = dashboard;
        }

        public TableActions(DocumentView docView)
        {
            _docView = docView;
        }

        public static List<DataOne> FillingDashGrid(int direction, int userID)
        {
            EDMdbDataContext dataContext = new EDMdbDataContext();
            List<DataOne> dataOneSource = new List<DataOne>();

            #region Входяшие

            if (direction == 1)
            {
                var userIsParticipant =
                    (from a in dataContext.Participants where a.active && a.fk_user == userID select a.fk_process).Distinct().ToList();

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
                        select a.participantID).FirstOrDefault();// id-шник этого пользователя в участниках процесса

                    var versionMax =
                        (from a in dataContext.ProcessVersions where a.fk_process == proc.processID select a)
                            .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                    // если очередь == 0 то это либо || либо --. но первый в очереди
                    if (userQueue == 0)
                    {
                        if (proc.type.Equals("serial"))
                        {
                            int procMaxVersion =
                                (from a in dataContext.ProcessVersions
                                    where a.fk_process == proc.processID && a.active
                                    select a).OrderByDescending(v => v.version)
                                    .Select(v => v.processVersionID)
                                    .FirstOrDefault();

                            var step4Proc =
                                (from a in dataContext.Steps
                                    where a.active && a.fk_processVersion == procMaxVersion
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
                            (from a in dataContext.Steps where a.active && a.fk_processVersion == versionMax select a).OrderByDescending(d => d.date).FirstOrDefault();

                        if (lastStep != null)
                        {
                            var lastStepUser = lastStep.fk_participent;

                            var lastUserQueue = (from a in dataContext.Participants
                                where a.active && a.fk_process == proc.processID && a.fk_user == lastStepUser
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
                        Status = proc.status
                    });
                }
            }

            #endregion Исходящие

            return dataOneSource;
        }

        public void RenderDashGrid(GridView gridView, List<DataOne> dataOneSource, int direction)
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

                _dashboard.DataBind();
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

                _dashboard.DataBind();
            }
        }

        public void RenderDocGrid(GridView docGridView, EDMdbDataContext dataContext, int procMaxVersion)
        {
            /*
            List<Documents> documents =
                (from d in dataContext.Documents where d.fk_processVersion == procMaxVersion && d.active select d)
                    .ToList();
                    */

            List<Documents> documents = (from a in dataContext.Documents
                                         join b in dataContext.ProcVersionDocsMap
                                             on a.documentID equals b.fk_documents
                                         where b.active == true
                                               && a.active == true
                                               && b.fk_processVersion == procMaxVersion
                                         select a).ToList();


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
            _docView.DataBind();
        }
    }
}
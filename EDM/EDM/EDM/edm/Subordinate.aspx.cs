using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class Subordinate : System.Web.UI.Page
    {
        public class DataOne
        {
            public int Id { get; set; }
            public string ProcName { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public DateTime? DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
            public string Initiator { get; set; }
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
                int direction;
                int.TryParse(Session["directionS"].ToString(), out direction);

                RenderSubGrid(direction, userId, FillGrid(direction, userId));
            }
        }

        private void RenderSubGrid(int direction, object userId, List<DataOne> data )
        {
            EDMdbDataContext dc = new EDMdbDataContext();
            #region 10

            if (direction == 10)
            {
                foreach (var itm in data)
                {
                    switch (itm.Status)
                    {
                        case -2:
                        {
                            itm.StatusName = "Возвращен на доработку";
                        }
                            break;
                        case -1:
                        {
                            itm.StatusName = "Создан, ждет запуска";
                        }
                            break;
                        case 0:
                        {
                            itm.StatusName = "В процессе";
                        }
                            break;
                        case 1:
                        {
                            itm.StatusName = "Согласован, ждет утверждения";
                        }
                            break;
                        case 10:
                        {
                            itm.StatusName = "Согласован и Утвержден";
                        }
                            break;
                    }
                }

                directionLabel.Visible = true;
                directionLabel.Text = "Процессы подчиненных";
                subGridView.DataSource = data;

                BoundField boundField = new BoundField();
                boundField.DataField = "Id";
                boundField.HeaderText = "ИН";
                boundField.Visible = true;
                subGridView.Columns.Add(boundField);

                BoundField boundField2 = new BoundField();
                boundField2.DataField = "ProcName";
                boundField2.HeaderText = "Название процесса";
                boundField2.Visible = true;
                subGridView.Columns.Add(boundField2);

                BoundField boundField3 = new BoundField();
                boundField3.DataField = "StatusName";
                boundField3.HeaderText = "Общий статус";
                boundField3.Visible = true;
                subGridView.Columns.Add(boundField3);

                BoundField boundField4 = new BoundField();
                boundField4.DataField = "Initiator";
                boundField4.HeaderText = "Инициатор";
                boundField4.Visible = true;
                subGridView.Columns.Add(boundField4);

                BoundField boundField5 = new BoundField();
                boundField5.DataField = "DateStart";
                boundField5.HeaderText = "Дата начала";
                boundField5.Visible = true;
                subGridView.Columns.Add(boundField5);

                BoundField boundField6 = new BoundField();
                boundField6.DataField = "DateEnd";
                boundField6.HeaderText = "Дата окончания";
                boundField6.Visible = true;
                subGridView.Columns.Add(boundField6);

                ButtonField coluButtonField = new ButtonField();
                coluButtonField.Text = "Подробнее";
                coluButtonField.ButtonType = ButtonType.Button;
                coluButtonField.CommandName = "ButtonR10";
                coluButtonField.ControlStyle.CssClass = "btn btn-default";
                subGridView.Columns.Add(coluButtonField);

                DataBind();
            }

            #endregion 10

            #region 20

            if (direction == 20)
            {
             
            }

            #endregion 20
        }

        private  List<DataOne> FillGrid(int direction, object userId)
        {
            List<DataOne> directionData = new List<DataOne>();

            #region 10
            if (direction == 10)
            {
                EDMdbDataContext dc = new EDMdbDataContext();
                OtherFuncs of = new OtherFuncs();
                List<int> slavesId = new List<int>();
                List<int> slavesStructId = new List<int>();

                slavesId =
                    of.GetSlaves((int)(from a in dc.Users where a.active && a.userID == (int) userId select a.fk_struct).FirstOrDefault(),
                        ref slavesStructId); // TryParse

                var processIncludeSlaves =
                    (from a in dc.Participants where a.active && slavesId.Contains(a.fk_user) select a.fk_process)
                        .Distinct().ToList();

                    directionData = (from a in dc.Processes
                    where a.active && processIncludeSlaves.Contains(a.processID)
                    join b in dc.Participants on a.processID equals b.fk_process
                    where b.active
                    select new DataOne()
                    {
                        Id = a.processID,
                        ProcName = a.name,
                        Status = a.status,
                        Initiator = (from u in dc.Users where u.active && u.userID == a.fk_initiator select u.name).FirstOrDefault(),
                        DateStart = b.dateStart,
                        DateEnd = b.dateEnd
                    }
                    ).OrderByDescending(d => d.DateStart).GroupBy(x => x.Id).Select(x => x.First()).ToList(); ;
            }
            #endregion 10

            return directionData;
        }

        protected void subGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idProcess = Convert.ToInt32(subGridView.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

            switch (e.CommandName)
            {
                case "ButtonR10":
                {
                        Session["processID"] = idProcess;
                        Response.Redirect("ProcessHistory.aspx");
                    }
                    break;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class Subordinate : System.Web.UI.Page
    {
        [Serializable]
        public class DataOne
        {
            public int Id { get; set; }
            public string ProcName { get; set; }
            public int Status { get; set; }
            public string StatusName { get; set; }
            public DateTime? DateStart { get; set; }
            public DateTime? DateEnd { get; set; }
            public string Initiator { get; set; }
            public string UserName { get; set; }
            public int Participation { get; set; }
            public int Approve { get; set; }
            public int Reject { get; set; }
            public int Overdue { get; set; }
            public int InProcess { get; set; }


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
                DateTime strDateTime;
                DateTime strDateTimeEnd;
                DateTime.TryParse(Session["dateStartSearch"].ToString(), out strDateTime);
                DateTime.TryParse(Session["dateEndSearch"].ToString(), out strDateTimeEnd);

                RenderSubGrid(direction, userId,
                    strDateTime != DateTime.MinValue
                        ? FillGrid(direction, userId, Session["searchName"].ToString(), strDateTime.ToString(),strDateTimeEnd.ToString())
                        : FillGrid(direction, userId, Session["searchName"].ToString()));

            }
        }

        private void RenderSubGrid(int direction, object userId, List<DataOne> data )
        {
            EDMdbDataContext dc = new EDMdbDataContext();
            subGridView.DataSource = data;

            #region 10

            if (direction == 10)
            {
                //directionLabel.Text = "История процесов";
                Button10.BackColor = Color.WhiteSmoke;
                Button10.BorderStyle = BorderStyle.Inset;
                Button10.BorderWidth = 2;
                Button10.BorderColor = Color.OrangeRed;

                Div1.Visible = true;

                NameSearchBox.Text = Session["searchName"].ToString();
                DateSearchBox.Text = Session["dateStartSearch"].ToString();
                DateSearchBoxEnd.Text = Session["dateEndSearch"].ToString();

                var structSearch = (KeyValuePair<int, string>) Session["searchStruct"];
                DropDownList1.SelectedIndex = structSearch.Key;

                searchDiv.Visible = true;

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

                //directionLabel.Visible = true;
                directionLabel.Text = "Процессы подчиненных";

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
                Button20.BackColor = Color.WhiteSmoke;
                Button20.BorderStyle = BorderStyle.Inset;
                Button20.BorderWidth = 2;
                Button20.BorderColor = Color.OrangeRed;

                Div1.Visible = false;
                searchDiv.Visible = false;

                BoundField boundField = new BoundField();
                boundField.DataField = "Id";
                boundField.HeaderText = "ИН";
                boundField.Visible = true;
                subGridView.Columns.Add(boundField);

                BoundField boundField2 = new BoundField();
                boundField2.DataField = "UserName";
                boundField2.HeaderText = "Имя";
                boundField2.Visible = true;
                subGridView.Columns.Add(boundField2);

                BoundField boundField3 = new BoundField();
                boundField3.DataField = "Participation";
                boundField3.HeaderText = "Участие";
                boundField3.Visible = true;
                subGridView.Columns.Add(boundField3);

                BoundField boundField4 = new BoundField();
                boundField4.DataField = "Approve";
                boundField4.HeaderText = "Утвердил";
                boundField4.Visible = true;
                subGridView.Columns.Add(boundField4);

                BoundField boundField5 = new BoundField();
                boundField5.DataField = "Reject";
                boundField5.HeaderText = "Отклонил";
                boundField5.Visible = true;
                subGridView.Columns.Add(boundField5);

                BoundField boundField6 = new BoundField();
                boundField6.DataField = "Overdue";
                boundField6.HeaderText = "Просрочил";
                boundField6.Visible = true;
                subGridView.Columns.Add(boundField6);

                BoundField boundField7 = new BoundField();
                boundField7.DataField = "InProcess";
                boundField7.HeaderText = "В процессе";
                boundField7.Visible = true;
                subGridView.Columns.Add(boundField7);

                ButtonField coluButtonField = new ButtonField();
                coluButtonField.Text = "Подробнее";
                coluButtonField.ButtonType = ButtonType.Button;
                coluButtonField.CommandName = "ButtonR20";
                coluButtonField.ControlStyle.CssClass = "btn btn-default";
                subGridView.Columns.Add(coluButtonField);

                DataBind();
            }

            #endregion 20
        }

        private  List<DataOne> FillGrid(int direction, object userId, params string[] searchValues)
        {
            List<DataOne> directionData = new List<DataOne>();
            EDMdbDataContext dc = new EDMdbDataContext();
            OtherFuncs of = new OtherFuncs();

            List<int> slavesId = new List<int>();
            List<int> slavesStructId = new List<int>();

            slavesId =
                of.GetSlaves((int)(from a in dc.Users where a.active && a.userID == (int)userId select a.fk_struct).FirstOrDefault(),
                    ref slavesStructId); // TryParse

            #region 10
            if (direction == 10)
            {
                #region Струкутра в dropdown

                Dictionary<int, string> structList = new Dictionary<int, string> { { -1, "Выберите структурное" } };
                foreach (var s in slavesStructId)
                {
                    structList.Add(s, dc.Struct.Where(st=>st.structID == s && st.active).Select(st=>st.name).FirstOrDefault());
                }

                foreach (var itm in structList)
                {
                    DropDownList1.Items.Add(new ListItem(itm.Value, itm.Key.ToString()));
                }     

                #endregion


                if (searchValues.Any())
                {
                    slavesId = (from a in dc.Users
                                where a.active && slavesId.Contains(a.userID) && a.name.Contains(searchValues[0])
                                select a.userID).ToList();
                }

                var structSearch = (KeyValuePair<int, string>) Session["searchStruct"];

               if (structSearch.Key != 0)
                {
                    slavesId = (from a in dc.Users
                                where a.active && slavesId.Contains(a.userID) && a.name.Contains(searchValues[0]) && a.fk_struct == Convert.ToInt32(structSearch.Value)
                                select a.userID).ToList();
                }

                var processIncludeSlaves =
                    (from a in dc.Participants
                        where a.active && slavesId.Contains(a.fk_user)
                        join pr in dc.Processes on a.fk_process equals pr.processID
                        where pr.active
                        select pr.processID).Distinct().ToList();

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
                    ).OrderByDescending(d => d.DateStart).GroupBy(x => x.Id).Select(x => x.First()).ToList();

                if (searchValues.Count() > 1)
                {
                    List<DateTime> compDate;
                    DateTime compStartDate;
                    DateTime compEndDate;
                    DateTime.TryParse(searchValues[1], out compStartDate);
                    DateTime.TryParse(searchValues[2], out compEndDate);

                    compDate = of.GetDatesBetween(compStartDate, compEndDate);

                    directionData = directionData.Where(
                                a =>
                                a.DateStart != null &&
                                compDate.Select(x => x.Year).ToList().Contains(a.DateStart.Value.Year) && //a.DateStart.Value.Year.ToString().Contains(compDate.Year.ToString()) &&
                                compDate.Select(x => x.Month).ToList().Contains(a.DateStart.Value.Month) && //a.DateStart.Value.Month.ToString().Contains(compDate.Month.ToString()) &&
                                compDate.Select(x => x.Day).ToList().Contains(a.DateStart.Value.Day)) //a.DateStart.Value.Day.ToString().Contains(compDate.Day.ToString()))
                                .OrderByDescending(d => d.DateStart).GroupBy(x => x.Id).Select(x => x.First()).ToList();
                }
            }
            #endregion 10

            #region 20

            if (direction == 20)
            {    
                directionData = (from a in dc.Users
                    where a.active && slavesId.Contains(a.userID)
                    select new DataOne()
                    {
                        Id = a.userID,
                        UserName = a.name,

                        Participation = (from b in dc.Participants where b.active && b.fk_user == a.userID 
                                         join pr in dc.Processes on b.fk_process equals pr.processID where pr.active select pr.processID).Distinct().Count(),

                        Approve = (from b in dc.Participants where b.active && b.fk_user == a.userID
                                   join s in dc.Steps on b.participantID equals s.fk_participent
                                   where s.active && s.stepResult == 1 && !s.comment.Contains("автомат") // ПОДУМАТЬ
                                   select s).OrderByDescending(d => d.fk_processVersion).GroupBy(x => x.fk_processVersion).Select(x => x.First()).Distinct().Count(),

                        Reject = (from b in dc.Participants
                                  where b.active && b.fk_user == a.userID
                                  join s in dc.Steps on b.participantID equals s.fk_participent
                                  where s.active && s.stepResult == -2
                                  select s).OrderByDescending(d => d.fk_processVersion).GroupBy(x => x.fk_processVersion).Select(x => x.First()).Distinct().Count(),

                        Overdue = (from b in dc.Participants
                                   where b.active && b.fk_user == a.userID
                                   join s in dc.Steps on b.participantID equals s.fk_participent
                                   where s.active && s.stepResult == 1 && s.comment.Contains("автомат") // ПОДУМАТЬ
                                   select s).OrderByDescending(d => d.fk_processVersion).GroupBy(x => x.fk_processVersion).Select(x => x.First()).Distinct().Count(),

                    }
                    ).OrderBy(n=>n.UserName).ToList();

                foreach (var itm in directionData)
                {
                    itm.InProcess = itm.Participation - itm.Approve - itm.Reject - itm.Overdue;
                }

            }

            #endregion 20

            ViewState["dataOneSource"] = directionData;
            return directionData;
        }

        protected void subGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EDMdbDataContext dc = new EDMdbDataContext();
            int id = Convert.ToInt32(subGridView.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text);

            switch (e.CommandName)
            {
                case "ButtonR10":
                {
                    Session["processID"] = id;
                    Response.Redirect("ProcessHistory.aspx");
                }
                    break;
                case "ButtonR20":
                {
                    Session["searchName"] = (from a in dc.Users where a.active && a.userID == id select a.name).FirstOrDefault();
                    Session["directionS"] = 10;
                    Response.Redirect("Subordinate.aspx");
                }
                    break;
            }
        }

        protected void subGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subGridView.PageIndex = e.NewPageIndex;
            subGridView.DataSource = ViewState["dataOneSource"];
            subGridView.DataBind();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            Session["searchStruct"]= new KeyValuePair<int, string>(DropDownList1.SelectedIndex, DropDownList1.SelectedValue);
            Session["searchName"] = NameSearchBox.Text;
            Session["dateStartSearch"] = DateSearchBox.Text;
            Session["dateEndSearch"] = (DateSearchBoxEnd.Text.Any()) ? DateSearchBoxEnd.Text : DateSearchBox.Text;
            Response.Redirect("Subordinate.aspx");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Session["directionS"] = 10;
            Response.Redirect("Subordinate.aspx");
        }

        protected void Button20_Click(object sender, EventArgs e)
        {
            Session["directionS"] = 20;
            Response.Redirect("Subordinate.aspx");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace KPIWeb.Head
{
    public partial class HeadWatchResult : System.Web.UI.Page
    {
        public string FloatToStrFormat(float value, float plannedValue, int DataType)
        {
            if (DataType == 1)
            {
                string tmpValue = Math.Ceiling(value).ToString();// value.ToString("0");
                return tmpValue;
            }
            else if (DataType == 2)
            {
                string tmpValue = value.ToString();
                string tmpPlanned = plannedValue.ToString();
                int PlannedNumbersAftepPoint = 2;
                if (tmpPlanned.IndexOf(',') != -1)
                {
                    PlannedNumbersAftepPoint = (tmpPlanned.Length - tmpPlanned.IndexOf(',') + 1);
                }
                int ValuePointIndex = tmpValue.IndexOf(',');
                if (ValuePointIndex != -1)
                {
                    if ((tmpValue.Length - ValuePointIndex - PlannedNumbersAftepPoint) > 0)
                    {
                        tmpValue = tmpValue.Remove(ValuePointIndex + PlannedNumbersAftepPoint, tmpValue.Length - ValuePointIndex - PlannedNumbersAftepPoint);
                    }
                }
                return tmpValue;
            }

            return "0";
        } 
        protected void Page_Load(object sender, EventArgs e)
        {

            var reportIdTmp = ViewState["ReportId"] ?? "0";
            int reportId = Convert.ToInt32(reportIdTmp);
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable.AccessLevel != 8)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                List<ReportArchiveTable> allReports = (from a in kpiWebDataContext.ReportArchiveTable
                    where a.Active == true
                    select a).ToList();
                ListItem listItem = new ListItem();
                    listItem.Text = "Текущее значение";
                listItem.Value = "0";
                DropDownList1.Items.Add(listItem);

                ListItem listItem2 = new ListItem();
                listItem2.Text = "Расчет нулевых значений целевых показателей.";
                listItem2.Value = "100500";
                DropDownList1.Items.Add(listItem2);

                foreach (ReportArchiveTable report in allReports)
                {
                    ListItem listItemRep = new ListItem();
                    listItemRep.Text = report.Name;
                    listItemRep.Value = report.ReportArchiveTableID.ToString();
                    DropDownList1.Items.Add(listItemRep);
                }
                DropDownList1.SelectedIndex = reportId;

                
            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("HeadMain.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int reportId = Convert.ToInt32(DropDownList1.SelectedValue);
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ParamID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Response", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Measure", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Planned", typeof(string)));
            for (int k = 0; k <= 40; k++) //создаем кучу полей
            {
                dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
            }
            List<string> columnNames = new List<string>();
            List<bool> ToShow = new List<bool>();
            List<FirstLevelSubdivisionTable> FirstLevelList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                               join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                               on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubmisionTableId
                                                               where (b.FK_ReportArchiveTableId == reportId || reportId == 0 ||
                                                               (reportId == 100500 && (b.FK_ReportArchiveTableId == 3 || b.FK_ReportArchiveTableId==1)))
                                                             //  && a.Active == true
                                                               && b.Active == true
                                                               select a).Distinct().ToList();
            List<IndicatorsTable> IndicatorsList = (from a in kpiWebDataContext.IndicatorsTable
                                                    join b in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                                    on a.IndicatorsTableID equals b.FK_IndicatorsTable
                                                    where (b.FK_ReportArchiveTable == reportId || reportId == 0 || 
                                                    (reportId == 100500 && (b.FK_ReportArchiveTable == 3 || b.FK_ReportArchiveTable == 1)))
                                                    && a.Active == true
                                                    && b.Active == true
                                                    select a).Distinct().OrderBy(mc => mc.SortID).ToList();
            columnNames.Add("КФУ");
            ToShow.Add(false);
            foreach (FirstLevelSubdivisionTable First in FirstLevelList)
            {
                columnNames.Add(First.AbbRu);
                ToShow.Add(false);
            }
            int additionalColumnCount = FirstLevelList.Count;
            foreach (IndicatorsTable currentIndicator in IndicatorsList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ParamID"] = currentIndicator.IndicatorsTableID.ToString();
                dataRow["Name"] = currentIndicator.Name;
                dataRow["Response"] = (from a in kpiWebDataContext.UsersTable
                                       where a.Active == true
                                       join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                       on a.UsersTableID equals b.FK_UsresTable
                                       where b.Active == true
                                       && b.CanConfirm == true
                                       && b.FK_IndicatorsTable == currentIndicator.IndicatorsTableID
                                       select a.Position).FirstOrDefault();
                dataRow["Measure"] = currentIndicator.Measure;

                PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                                 where a.FK_IndicatorsTable == currentIndicator.IndicatorsTableID
                                                 && a.Date > DateTime.Now
                                                 select a).OrderBy(x => x.Date).FirstOrDefault();

                if (plannedValue != null)
                {
                    dataRow["Planned"] = plannedValue.Value;
                }
                else
                {
                    dataRow["Planned"] = "Не определено";
                }

                Rector.ChartOneValue Value0 = Rector.ForRCalc.GetCalculatedIndicator(reportId, currentIndicator, null, null,
                    null, null);
                if (Value0 != null)
                {
                    if (Value0.value != 0)
                    {
                        dataRow["Value0"] = FloatToStrFormat((float)Value0.value, (float)plannedValue.Value, (int)currentIndicator.DataType);
                        ToShow[0] = true;
                    }
                    else
                    {
                        dataRow["Value0"] = 0;
                    }
                }

                int i = 1;
                foreach (FirstLevelSubdivisionTable FirstLevel in FirstLevelList)
                {
                    Rector.ChartOneValue Value = Rector.ForRCalc.GetCalculatedIndicator(reportId, currentIndicator, FirstLevel, null, null, null);
                    if (Value != null)
                    {
                        if (Value.value != 0)
                        {
                            dataRow["Value" + i.ToString()] = FloatToStrFormat((float)Value.value, (float)plannedValue.Value, (int)currentIndicator.DataType);
                            ToShow[i] = true;
                        }
                        else
                        {
                            dataRow["Value" + i.ToString()] = 0;
                        }
                    }
                    else
                    {
                        dataRow["Value" + i.ToString()] = 0;
                    }
                    i++;
                }
                dataTable.Rows.Add(dataRow);
            }
            GridviewCollected.DataSource = dataTable;

            for (int j = 0; j < additionalColumnCount+1; j++)
            {
                //if (ToShow[j])
                {
                    GridviewCollected.Columns[j + 5].Visible = true;
                    GridviewCollected.Columns[j + 5].HeaderText = columnNames[j];
                }
            }
            GridviewCollected.DataBind();
        }
    }
}
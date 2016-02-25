using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPIWeb.Rector;
namespace KPIWeb.Director
{
    public partial class DRating : System.Web.UI.Page
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

        public class ClassForGV
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public float ValueForSort { get; set; }
            public string Value { get; set; }
            public float Planned { get; set; }
            public int Number { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 4)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
               
                RectorChooseReportClass rectorChooseReportClass = new RectorChooseReportClass();
                RectorChooseReportDropDown.Items.AddRange(rectorChooseReportClass.GetListItemCollectionWithReports());
                RefreshGrid();
            }
        }
        public void RefreshGrid()
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            int reportId = Convert.ToInt32(RectorChooseReportDropDown.SelectedValue);
           
            if (reportId == 100500)
            {
                ReportTitle.Text = "Расчет целевых показателей";
             
            }
            else
            {
            ReportArchiveTable ReportTable = (from a in kpiWebDataContext.ReportArchiveTable
                                                  where a.ReportArchiveTableID == reportId
                                                  select a).FirstOrDefault();
            
            ReportTitle.Text = ReportTable.Name + " " + ReportTable.StartDateTime.ToString().Split(' ')[0] + " - " + ReportTable.EndDateTime.ToString().Split(' ')[0];
            }
             DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
            dataTable.Columns.Add(new DataColumn("PlannedValue", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Detail", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ButtonEnabled", typeof(bool)));
           
            
            List<IndicatorsTable> indicatorsList = (from a in kpiWebDataContext.IndicatorsTable
                                                    where a.Active == true
                                                    select a).ToList();

            FirstLevelSubdivisionTable Academy = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                  where a.Active == true
                                                  join b in kpiWebDataContext.UsersTable
                                                  on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                                  where b.UsersTableID == userID && b.Active == true
                                                  select a).FirstOrDefault();

            ForRCalc rectorCalculate = new ForRCalc();
  
            foreach (IndicatorsTable indicator in indicatorsList)
            {
 
                ChartOneValue CurrentValue = ForRCalc.GetCalculatedIndicator(reportId, indicator, Academy, null, null, null);
      
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = indicator.IndicatorsTableID.ToString();
                dataRow["Name"] = indicator.Name.ToString();
                dataRow["Value"] = CurrentValue.value.ToString();
                dataRow["PlannedValue"] = CurrentValue.planned.ToString();
                dataRow["Detail"] = indicator.IndicatorsTableID.ToString();
                dataRow["ButtonEnabled"] = true;
                if (reportId == 100500 || reportId == 4 )
                {
                    dataRow["ButtonEnabled"] = false;
                }
                
                dataTable.Rows.Add(dataRow);
           
            }
            Grid.DataSource = dataTable;
            Grid.DataBind();
        }
        protected void DetailClick(object sender, EventArgs e)
        {            
            Button button = (Button)sender;
            {
                /*Serialization report = (Serialization)Session["ReportID"];
                RectorChooseReportClass rectorChooseReportClass = new RectorChooseReportClass();
                RectorChooseReportDropDown.Items.AddRange(rectorChooseReportClass.GetListItemCollectionWithReports());              
                report.Id = Convert.ToInt32(RectorChooseReportDropDown.SelectedValue);
                Session["ReportID"] = report;  

                Serialization indicator = (Serialization)Session["IndicatorID"];
                indicator.Id = Convert.ToInt32(button.CommandArgument.ToString());
                Session["IndicatorID"] = indicator;  
                Response.Redirect("~/Director/DDetailRating.aspx");
                 */
                Ramsi newSession =new Ramsi();
                newSession.ReportId = Convert.ToInt32(RectorChooseReportDropDown.SelectedValue);
                newSession.IndicatorId = Convert.ToInt32(button.CommandArgument.ToString());
                Session["DirectorSession"] = newSession;  
                Response.Redirect("~/Director/DDetailRating.aspx");
            }
            
        }
                 protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Director/DMain.aspx");
        }
          
          protected void RectorChooseReportDropDown_SelectedIndexChanged(object sender, EventArgs e)
          {            
              RefreshGrid();

        }
          protected void Button22_Click(object sender, EventArgs e)
          {
              Response.Redirect("~/Director/DMain.aspx");
          }


          protected void Button5_Click(object sender, EventArgs e)
          {
              Response.Redirect("~/Rector/ViewDocument.aspx");
          }
        
    }
}
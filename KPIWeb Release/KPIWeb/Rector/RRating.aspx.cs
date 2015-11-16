using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace KPIWeb.Rector
{
    public partial class RRating : System.Web.UI.Page
    {
        public string FloatToStrFormat(float value,float plannedValue,int DataType)
        {
            if (DataType == 1)
            {
                string tmpValue = Math.Ceiling(value).ToString();// value.ToString("0");
                return tmpValue;
            }
            else if(DataType == 2)
            {
                string tmpValue = value.ToString();
                string tmpPlanned = plannedValue.ToString();
                int PlannedNumbersAftepPoint = 2;
                if (tmpPlanned.IndexOf(',') != -1)
                {
                    PlannedNumbersAftepPoint = (tmpPlanned.Length - tmpPlanned.IndexOf(',')+1);
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
           public int ID {get;set;}
           public string Name {get;set;}
           public float ValueForSort { get; set; }
           public string Value {get;set;}
           public float Planned {get;set;}
           public int Number {get; set;}
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

            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }

            //-------------------------------------------------------------------------------------------------------------
            if (!IsPostBack)
            {
                #region session
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                string val = this.Request.QueryString["HLevel"]; //hisoty level сова придумал)
                if (val != null)
                {
                    rectorHistory.CurrentSession = Convert.ToInt32(val);
                    Session["rectorHistory"] = rectorHistory;
                }

                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;


                if ((rectorHistory.SessionCount - rectorHistory.CurrentSession) < 2)
                {
                    GoForwardButton.Enabled = false;
                }

                #endregion
                RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"];
                if (RectorChart == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int reportId = RectorChart.reportId;

                RectorChooseReportClass rectorChooseReportClass = new RectorChooseReportClass();
                RectorChooseReportDropDown.Items.AddRange(rectorChooseReportClass.GetListItemCollectionWithReports());
                RectorChooseReportDropDown.SelectedValue = reportId.ToString();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
                dataTable.Columns.Add(new DataColumn("PlannedValue", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Button_Text", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ButtonDetal1Visible", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("ButtonDetal2Visible", typeof(bool)));
                                   
                List<ClassForGV> GridViewClassList = new List<ClassForGV>();
                ForRCalc forRCalc = new ForRCalc();
                if (ViewType == 0) // просмотр для структурных подразделений
                {
                    #region forAcademys
                    Title.Text = (from a in kpiWebDataContext.IndicatorsTable
                                  where a.IndicatorsTableID == ParamID
                                  select a.Name).FirstOrDefault() + " (" + (from a in kpiWebDataContext.IndicatorsTable
                                                                            where a.IndicatorsTableID == ParamID
                                                                            select a.Measure).FirstOrDefault() + ")";
                    List<ForRCalc.Struct> currentStructList = new List<ForRCalc.Struct>();
                    IndicatorsTable CurrentIndicator = (from a in kpiWebDataContext.IndicatorsTable
                                                        where a.IndicatorsTableID == ParamID
                                                        select a).FirstOrDefault();
                    currentStructList = ForRCalc.GetChildStructList(mainStruct, reportId);

                    foreach (ForRCalc.Struct currentStruct in currentStructList)
                    {
                        ClassForGV curGVRow = new ClassForGV();
                        curGVRow.ID = ForRCalc.GetLastID(currentStruct);
                        curGVRow.Name =  currentStruct.Name;
                        FirstLevelSubdivisionTable Academy = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                              where a.FirstLevelSubdivisionTableID == currentStruct.Lv_1
                                                              select a).FirstOrDefault();

                        

                        ChartOneValue CurrentValue = ForRCalc.GetCalculatedIndicator(reportId, CurrentIndicator, Academy, null,null,null);
                        curGVRow.ValueForSort = CurrentValue.value;
                        curGVRow.Value = FloatToStrFormat(CurrentValue.value, CurrentValue.planned, (Int32)CurrentIndicator.DataType);
                        GridViewClassList.Add(curGVRow);                      
                    }
                        List<ClassForGV> SortedList = GridViewClassList.OrderByDescending(o=>o.ValueForSort).ToList();
                    bool isAllDataZero = true;
                        foreach (ClassForGV curGVRow in SortedList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = curGVRow.ID.ToString();
                        dataRow["Number"] = "";
                        dataRow["Name"] = curGVRow.Name;
                        dataRow["Value"] = curGVRow.Value;
                        if (curGVRow.Value != "0")
                            isAllDataZero = false;
                        else
                        {
                            continue;
                        }
                        dataRow["Button_Text"] = "Детализировать";
                        dataRow["ButtonDetal1Visible"] = false;
                        dataRow["ButtonDetal2Visible"] = false;
                        dataTable.Rows.Add(dataRow);
                    }

                    noDataMessage.Visible = isAllDataZero;
                    Grid.Visible = !isAllDataZero;
                    Grid.Columns[1].HeaderText = "Структурное подразделение";
                    Grid.Columns[3].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[5].Visible = false;

                    #endregion
                }
                else if (ViewType == 1)
                {
                    #region IndicatorsForCFU
                    Title.Text = "Целевые показатели";
                    if (ParamType == 0) //считаем целевой показатель
                    {
                        List<IndicatorsTable> Indicators = (
                            from a in kpiWebDataContext.IndicatorsTable
                            join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                on a.IndicatorsTableID equals b.FK_IndicatorsTable
                            join c in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                            on a.IndicatorsTableID equals c.FK_IndicatorsTable
                            where
                                a.Active == true
                                && b.CanView == true
                                && b.FK_UsresTable == userID
                            select a).Distinct().OrderBy(mc => mc.SortID).ToList();

                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentIndicator.IndicatorsTableID; //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "";
                          
                            ChartOneValue CurrentValue = forRCalc.IndicatorsForCFUOneIndicator(CurrentIndicator.IndicatorsTableID, reportId); // 1 индикатор в разрезе КФУ взятый по ID
                            dataRow["PlannedValue"] = CurrentValue.planned;
                            dataRow["Name"] = CurrentValue.name;

                            float tmp = CurrentValue.value;
                            dataRow["ButtonDetal1Visible"] = !(tmp == 0);
                            dataRow["ButtonDetal2Visible"] = !(tmp == 0);
                            if (tmp == (float)1E+20)
                            {
                                dataRow["Value"] = "Рассчет невозможен";
                                dataRow["ButtonDetal1Visible"] = false;
                                dataRow["ButtonDetal2Visible"] = false;
                            }
                            else
                            {
                                dataRow["Value"] = FloatToStrFormat(tmp, CurrentValue.planned, (Int32)CurrentIndicator.DataType);
                            }



                            if ((CurrentIndicator.IndicatorsTableID == 1028 ||
                                CurrentIndicator.IndicatorsTableID == 1027 ||
                                CurrentIndicator.IndicatorsTableID == 1026 ||
                                tmp == 0) && (reportId == 1 || reportId == 3 ))
                            {
                                dataRow["ButtonDetal1Visible"] = false;
                                dataRow["ButtonDetal2Visible"] = false;
                            }

                            if (reportId == 100500 && (CurrentIndicator.IndicatorsTableID == 1021
                                                       || CurrentIndicator.IndicatorsTableID == 1023
                                                       || CurrentIndicator.IndicatorsTableID == 1026
                                                       || CurrentIndicator.IndicatorsTableID == 1027
                                                       || CurrentIndicator.IndicatorsTableID == 1033))
                            {
                                dataRow["ButtonDetal1Visible"] = false;
                                dataRow["ButtonDetal2Visible"] = false;
                            }

                            List<CollectedIndicatorsForR> currentCollectedForRectorList =
                                (from a in kpiWebDataContext.CollectedIndicatorsForR
                                 where (a.FK_ReportArchiveTable == reportId || reportId == 0 || reportId == 100500)
                                 && a.FK_FirstLevelSubdivisionTable != null
                                       && a.Active == true
                                       && a.FK_IndicatorsTable == Convert.ToInt32(CurrentIndicator.IndicatorsTableID)
                                       && a.Value != null
                                       && a.Value != 0
                                 select a).ToList();
                            if (currentCollectedForRectorList.Count == 0)
                            {
                                dataRow["ButtonDetal1Visible"] = false;
                                dataRow["ButtonDetal2Visible"] = false;
                            }
                            




                            dataRow["Button_Text"] = "Детализировать";
                           /* if ((CurrentIndicator.IndicatorsTableID == 1027) || (CurrentIndicator.IndicatorsTableID == 1028) || (CurrentIndicator.IndicatorsTableID == 1026))
                                dataRow["Button_Text"] = "Детализировать" + Environment.NewLine + "(рассчитано по"+Environment.NewLine +  "неполным данным)";*/
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    Grid.Columns[0].Visible = false;
                    #endregion
                }
                else if (ViewType == 5)
                {
                    #region for faculties
                    Title.Text = (from a in kpiWebDataContext.IndicatorsTable
                                  where a.IndicatorsTableID == ParamID
                                  select a.Name).FirstOrDefault() + " (" + (from a in kpiWebDataContext.IndicatorsTable
                                                                            where a.IndicatorsTableID == ParamID
                                                                            select a.Measure).FirstOrDefault() + ")";

                    List<ForRCalc.Struct> currentStructList = new List<ForRCalc.Struct>();
                    currentStructList = ForRCalc.GetAllSecondLevelInReport(reportId);
                    IndicatorsTable CurrentIndicator = (from a in kpiWebDataContext.IndicatorsTable
                                                        where a.IndicatorsTableID == ParamID
                                                        select a).FirstOrDefault();

                    foreach (ForRCalc.Struct currentStruct in currentStructList)
                    {

                        ClassForGV curGVRow = new ClassForGV();
                        curGVRow.ID = ForRCalc.GetLastID(currentStruct);
                        string academyName  = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                                        where a.FirstLevelSubdivisionTableID == currentStruct.Lv_1
                                                                        select a.Name).FirstOrDefault();
                        academyName = academyName.Replace("\r", string.Empty);
                        academyName = academyName.Replace("\n", string.Empty);
                        string facultyName = currentStruct.Name.Replace("\n", string.Empty);
                        facultyName = facultyName.Replace("\r", string.Empty);
                        if (facultyName == academyName)
                        {
                            curGVRow.Name = academyName;
                        }
                        else
                        {
                            curGVRow.Name = facultyName + ", " + academyName;
                        }
                        
                        FirstLevelSubdivisionTable Academy = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                              where a.FirstLevelSubdivisionTableID == currentStruct.Lv_1
                                                              select a).FirstOrDefault();                       
                        SecondLevelSubdivisionTable Facullty = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                where a.SecondLevelSubdivisionTableID == currentStruct.Lv_2
                                                                select a).FirstOrDefault();

                        ChartOneValue CurrentValue = ForRCalc.GetCalculatedIndicator(reportId, CurrentIndicator, Academy, Facullty,null,null);
                        curGVRow.ValueForSort = CurrentValue.value;
                        curGVRow.Value = FloatToStrFormat(CurrentValue.value, CurrentValue.planned, (Int32)CurrentIndicator.DataType);
                        GridViewClassList.Add(curGVRow);
                    }

                    List<ClassForGV> SortedList = GridViewClassList.OrderByDescending(o => o.ValueForSort).ToList();
                    bool isAllDataZero = true;
                    foreach (ClassForGV curGVRow in SortedList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = curGVRow.ID.ToString();
                        dataRow["Number"] = "";
                        dataRow["Name"] = curGVRow.Name;
                        dataRow["Value"] = curGVRow.Value;
                        if (curGVRow.Value != "0")
                            isAllDataZero = false;
                        else
                        {
                            continue;
                        }
                        dataRow["Button_Text"] = "Детализировать";
                        dataRow["ButtonDetal1Visible"] = false;
                        dataRow["ButtonDetal2Visible"] = false;
                        dataTable.Rows.Add(dataRow);
                    }
                                    
                    noDataMessage.Visible = isAllDataZero;
                    Grid.Visible = !isAllDataZero;

                    Grid.Columns[1].HeaderText = "Структурное подразделение";

                    Grid.Columns[3].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[5].Visible = false;
                    #endregion
                }

                int Number = 0;
                foreach (DataRow row in dataTable.Rows)
                    row["Number"] = ++Number;
                Grid.DataSource = dataTable;
                Grid.DataBind();
                if (Grid.Rows.Count <1)
                {
                    noDataMessage.Visible = true;
                }
            }
        }
        protected void Button1Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "");

                if (currentRectorSession.sesViewType == 1)
                {//впервые перешли на разложение по структуре сразу после показателя
                    currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 0;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
                }               
                else if (currentRectorSession.sesViewType == 3)
                {
                    currentRectorSession.sesStruct = ForRCalc.StructDeeper(currentRectorSession.sesStruct, Convert.ToInt32(button.CommandArgument));
                }
                else
                {
                    currentRectorSession.sesStruct = ForRCalc.StructDeeper(currentRectorSession.sesStruct, Convert.ToInt32(button.CommandArgument));
                }
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/RRating.aspx");
            }
        }
        protected void Button2Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "");

                    currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 5;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
          
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/RRating.aspx");
            }
        }
        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
             RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (rectorHistory.CurrentSession == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            rectorHistory.CurrentSession--;
            Session["rectorHistory"] = rectorHistory;
            Response.Redirect("~/Rector/RRating.aspx");
        }
        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (rectorHistory.CurrentSession < rectorHistory.SessionCount) // есть куда переходить
            {
                rectorHistory.CurrentSession++;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/RRating.aspx");
            }
        }
        protected void RectorChooseReportDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            RectorChartSession RectorChart = (RectorChartSession)Session["RectorChart"];
            if (RectorChart == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            RectorChart.reportId = Convert.ToInt32(RectorChooseReportDropDown.SelectedValue);
            Session["RectorChart"] = RectorChart;
            Response.Redirect("~/Rector/RRating.aspx");

        }       
    }
}
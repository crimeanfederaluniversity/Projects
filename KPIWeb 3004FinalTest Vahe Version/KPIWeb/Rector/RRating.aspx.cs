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

                #endregion
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
                dataTable.Columns.Add(new DataColumn("PlannedValue", typeof(string)));

                ReportArchiveTable ReportTable = (from a in kpiWebDataContext.ReportArchiveTable
                                                  where a.ReportArchiveTableID == ReportID
                                                  select a).FirstOrDefault();

                if (ViewType == 0) // просмотр для структурных подразделений
                {
                    Title.Text = (from a in kpiWebDataContext.IndicatorsTable
                                  where a.IndicatorsTableID == ParamID
                                  select a.Name).FirstOrDefault();
                    List<ForRCalc.Struct> currentStructList = new List<ForRCalc.Struct>();
                    currentStructList = ForRCalc.GetChildStructList(mainStruct, ReportID);
                    foreach (ForRCalc.Struct currentStruct in currentStructList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = ForRCalc.GetLastID(currentStruct).ToString();
                        dataRow["Name"] = currentStruct.Name;
                        dataRow["Value"] =
                            ForRCalc.GetCalculatedWithParams(currentStruct, ParamType, ParamID, ReportID, SpecID).ToString();
                        dataTable.Rows.Add(dataRow);
                    }

                    Grid.Columns[3].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[5].Visible = false;

                    Grid.DataSource = dataTable;
                    Grid.DataBind();
                }
                else if (ViewType == 1)
                {
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
                                && c.FK_ReportArchiveTable == ReportID
                            select a).OrderBy(mc => mc.IndicatorsTableID).ToList();

                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentIndicator.IndicatorsTableID; //GetLastID(currentStruct).ToString();

                            if (CurrentIndicator.Measure != null)
                            {
                                if (CurrentIndicator.Measure.Length > 2)
                                {
                                    dataRow["Name"] = CurrentIndicator.Name + " (" + CurrentIndicator.Measure + ")";
                                }
                                else 
                                {
                                    dataRow["Name"] = CurrentIndicator.Name;
                                }
                                
                            }
                            else
                            {
                                dataRow["Name"] = CurrentIndicator.Name;
                            }

                            PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                                             where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                                                   && a.Date > DateTime.Now
                                                             select a).OrderBy(x => x.Date).FirstOrDefault();

                            if (plannedValue != null)
                            {
                                dataRow["PlannedValue"] = plannedValue.Value;
                            }
                            else
                            {
                                dataRow["PlannedValue"] = "Не определено";
                            }

                            float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, ParamType,
                                                CurrentIndicator.IndicatorsTableID, ReportID, SpecID));

                            if (tmp == (float)1E+20)
                            {
                                dataRow["Value"] = "Рассчет невозможен";
                            }
                            else
                            {
                                dataRow["Value"] = tmp.ToString("0.00");
                            }

                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.DataBind();

                    #endregion
                }
                else if (ViewType == 5)
                {
                    Title.Text = (from a in kpiWebDataContext.IndicatorsTable
                                  where a.IndicatorsTableID == ParamID
                                  select a.Name).FirstOrDefault();

                    List<ForRCalc.Struct> currentStructList = new List<ForRCalc.Struct>();
                    currentStructList = ForRCalc.GetAllSecondLevel();

                    foreach (ForRCalc.Struct currentStruct in currentStructList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = ForRCalc.GetLastID(currentStruct).ToString();
                        dataRow["Name"] = currentStruct.Name +", "+(from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                                        where a.FirstLevelSubdivisionTableID == currentStruct.Lv_1
                                                                        select a.Name).FirstOrDefault();
                        dataRow["Value"] =
                            ForRCalc.GetCalculatedWithParams(currentStruct, ParamType, ParamID, ReportID, SpecID).ToString();
                        dataTable.Rows.Add(dataRow);
                    }

                    Grid.Columns[3].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[5].Visible = false;

                    Grid.DataSource = dataTable;
                    Grid.DataBind();
                }
                else
                {
                    //error // wrong ViewType
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
    }
}
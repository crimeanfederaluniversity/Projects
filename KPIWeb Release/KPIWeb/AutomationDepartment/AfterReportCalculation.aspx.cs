using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPIWeb.Rector;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.AutomationDepartment
{
    public partial class AfterReportCalculation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            
            if ((userTable.AccessLevel != 10)&&(userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {


                List<ReportArchiveTable> activeReports = (from a in kPiDataContext.ReportArchiveTable
                                                          where a.Active == true
                                                          select a).ToList();
                List<IndicatorsTable> activeIndicators = (from a in kPiDataContext.IndicatorsTable
                                                          where a.Active == true
                                                          select a).ToList();
                foreach (ReportArchiveTable curentReport in activeReports)
                {
                    ListItem tmpItem = new ListItem();
                    tmpItem.Text = curentReport.Name + " " + curentReport.StartDateTime.ToString().Split(' ')[0] + " - " + curentReport.EndDateTime.ToString().Split(' ')[0];
                    tmpItem.Value = curentReport.ReportArchiveTableID.ToString();
                    reportList.Items.Add(tmpItem);
                }

                foreach (IndicatorsTable currentIndicator in activeIndicators)
                {
                    ListItem tmpItem = new ListItem();
                    tmpItem.Text = "<span class=\"smallerText\">" + currentIndicator.Name + "</span>";
                    tmpItem.Value = currentIndicator.IndicatorsTableID.ToString();
                    indicatorsList.Items.Add(tmpItem);
                }
            }
        }

        protected void calculateButton_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int ReportID = Convert.ToInt32(reportList.Items[reportList.SelectedIndex].Value); // пока только один отчет и надо сделать быстро)
            #region init
            //сначала берем все показатели целевые которые включены в выбранный отчет
            //находим все структурные подразделения 1-го уровня которые участвуют в этом отчете
            //находим все структурные подразделения 2-го уровня которые участвуют в этом отчете
            List<IndicatorsTable> IndicatorsToCalculateList = new List<IndicatorsTable>();
            foreach (ListItem tmpItem in indicatorsList.Items)
            {
                if (tmpItem.Selected == true)
                {
                    IndicatorsTable tmpIndicator = (from a in kPiDataContext.IndicatorsTable 
                         where a.IndicatorsTableID == Convert.ToInt32(tmpItem.Value) 
                         join b in kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                         on a.IndicatorsTableID equals  b.FK_IndicatorsTable
                         where b.Active == true
                         && b.FK_ReportArchiveTable == ReportID
                         select a).FirstOrDefault();
                    if (tmpIndicator == null)
                    {
                        //ERROR
                        IndicatorsToCalculateList.Clear();
                        break;
                    }
                    else
                    {
                        IndicatorsToCalculateList.Add(tmpIndicator);
                    }

                    
                }
            }

            if (IndicatorsToCalculateList.Count > 0)
            {
                List<FirstLevelSubdivisionTable> FirstLevelToCalculate =
                    (from a in kPiDataContext.FirstLevelSubdivisionTable
                        join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                            on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubmisionTableId
                        where b.FK_ReportArchiveTableId == ReportID
                        select a).Distinct().ToList();

                List<SecondLevelSubdivisionTable> SecondLevelToCalculate =
                    (from a in kPiDataContext.SecondLevelSubdivisionTable
                        join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                            on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                        where b.FK_ReportArchiveTableId == ReportID
                        select a).Distinct().ToList();
                //теперь пройдемся поочереди по каждому показателю
                //первым делом посчитаем показатель для КФУ
                //потом посчитаем показатель для каждого структурного 1-го уровня
                //потом посчитаем показатель для каждого структурного 2-го уровня
                //поехали:)

                #endregion

                foreach (IndicatorsTable CurrentIndicator in IndicatorsToCalculateList)
                    //считаем каждый показатель для каждого факультета, каждой академии и всего КФУ
                {
                    #region calcForCFU

                    {
                        //считай для КФУ
                        ForRCalc.Struct mainStruct = mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
                        double tmp =
                            ForRCalc.CalculatedForDB(
                                (float)
                                    ForRCalc.GetCalculatedWithParams(mainStruct, 0, CurrentIndicator.IndicatorsTableID,
                                        ReportID, 0, 0 /*тут должен быть ID проректора*/));
                        CollectedIndicatorsForR newCollected = new CollectedIndicatorsForR();
                        newCollected.Active = true;
                        newCollected.CreatedDateTime = DateTime.Now;
                        newCollected.FK_ReportArchiveTable = ReportID;
                        newCollected.FK_IndicatorsTable = CurrentIndicator.IndicatorsTableID;
                        newCollected.FK_FirstLevelSubdivisionTable = null;
                        newCollected.FK_SecondLevelSubdivisionTable = null;
                        newCollected.FK_ThirdLevelSubdivisionTable = null;
                        newCollected.FK_FourthLelevlSubdivisionTable = null;
                        newCollected.FK_FifthLevelSubdivisionTable = null;
                        if (tmp == (float) 1E+20)
                        {
                            newCollected.Value = null;
                        }
                        else
                        {
                            newCollected.Value = tmp;
                        }
                        kPiDataContext.CollectedIndicatorsForR.InsertOnSubmit(newCollected);
                    }

                    #endregion

                    kPiDataContext.SubmitChanges();

                    #region calcForAcademys

                    foreach (FirstLevelSubdivisionTable CurrentFirstLevel in FirstLevelToCalculate)
                    {
                        //считай для академий
                        ForRCalc.Struct mainStruct =
                            mainStruct =
                                new ForRCalc.Struct(1, CurrentFirstLevel.FirstLevelSubdivisionTableID, 0, 0, 0, "N");
                        double tmp =
                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0,
                                CurrentIndicator.IndicatorsTableID, ReportID, 0,0 /*тут должен быть ID проректора*/));
                        CollectedIndicatorsForR newCollected = new CollectedIndicatorsForR();
                        newCollected.Active = true;
                        newCollected.CreatedDateTime = DateTime.Now;
                        newCollected.FK_ReportArchiveTable = ReportID;
                        newCollected.FK_IndicatorsTable = CurrentIndicator.IndicatorsTableID;
                        newCollected.FK_FirstLevelSubdivisionTable = CurrentFirstLevel.FirstLevelSubdivisionTableID;
                        newCollected.FK_SecondLevelSubdivisionTable = null;
                        newCollected.FK_ThirdLevelSubdivisionTable = null;
                        newCollected.FK_FourthLelevlSubdivisionTable = null;
                        newCollected.FK_FifthLevelSubdivisionTable = null;
                        if (tmp == (float) 1E+20)
                        {
                            newCollected.Value = null;
                        }
                        else
                        {
                            newCollected.Value = tmp;
                        }
                        kPiDataContext.CollectedIndicatorsForR.InsertOnSubmit(newCollected);
                    }

                    #endregion

                    kPiDataContext.SubmitChanges();

                    #region CalcForFaculys

                    foreach (SecondLevelSubdivisionTable CurrentSecondLevel in SecondLevelToCalculate)
                    {
                        // считай для кафедр
                        ForRCalc.Struct mainStruct =
                            mainStruct =
                                new ForRCalc.Struct(1, CurrentSecondLevel.FK_FirstLevelSubdivisionTable,
                                    CurrentSecondLevel.SecondLevelSubdivisionTableID, 0, 0, "N");
                        double tmp =
                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0,
                                CurrentIndicator.IndicatorsTableID, ReportID, 0, 0 /*тут должен быть ID проректора*/));
                        CollectedIndicatorsForR newCollected = new CollectedIndicatorsForR();
                        newCollected.Active = true;
                        newCollected.CreatedDateTime = DateTime.Now;
                        newCollected.FK_ReportArchiveTable = ReportID;
                        newCollected.FK_IndicatorsTable = CurrentIndicator.IndicatorsTableID;
                        newCollected.FK_FirstLevelSubdivisionTable = CurrentSecondLevel.FK_FirstLevelSubdivisionTable;
                        newCollected.FK_SecondLevelSubdivisionTable = CurrentSecondLevel.SecondLevelSubdivisionTableID;
                        newCollected.FK_ThirdLevelSubdivisionTable = null;
                        newCollected.FK_FourthLelevlSubdivisionTable = null;
                        newCollected.FK_FifthLevelSubdivisionTable = null;
                        if (tmp == (float) 1E+20)
                        {
                            newCollected.Value = null;
                        }
                        else
                        {
                            newCollected.Value = tmp;
                        }
                        kPiDataContext.CollectedIndicatorsForR.InsertOnSubmit(newCollected);
                    }

                    #endregion

                    kPiDataContext.SubmitChanges();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach(ListItem tmpItem in indicatorsList.Items)
            {
                tmpItem.Selected = true;
          }           
        }
    }
}
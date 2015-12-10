using System;
using System.Collections.Generic;
using System.Configuration;
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
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 1, 2, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
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

        protected void CalculateButton_Click(object sender, EventArgs e)
        {
            #region init
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int reportId = Convert.ToInt32(reportList.Items[reportList.SelectedIndex].Value); // пока только один отчет и надо сделать быстро)
            //сначала берем все показатели целевые которые включены в выбранный отчет
            //находим все структурные подразделения 1-го уровня которые участвуют в этом отчете
            //находим все структурные подразделения 2-го уровня которые участвуют в этом отчете
            List<IndicatorsTable> indicatorsToCalculateList = new List<IndicatorsTable>();
            foreach (ListItem tmpItem in indicatorsList.Items)
            {
                if (tmpItem.Selected == true)
                {
                    IndicatorsTable tmpIndicator = (from a in kPiDataContext.IndicatorsTable 
                         where a.IndicatorsTableID == Convert.ToInt32(tmpItem.Value) 
                         join b in kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                         on a.IndicatorsTableID equals  b.FK_IndicatorsTable
                         where b.Active == true
                         && b.FK_ReportArchiveTable == reportId
                         select a).FirstOrDefault();
                    if (tmpIndicator == null)
                    {

                    }
                    else
                    {
                        indicatorsToCalculateList.Add(tmpIndicator);
                    }                  
                }
            }
            #endregion
            if (indicatorsToCalculateList.Count > 0)
            {
                #region getFirstAndSecondListInReport
                List<FirstLevelSubdivisionTable> firstLevelToCalculate =
                    (from a in kPiDataContext.FirstLevelSubdivisionTable
                        join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                            on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubmisionTableId
                        where b.FK_ReportArchiveTableId == reportId
                        select a).Distinct().ToList();

                List<SecondLevelSubdivisionTable> secondLevelToCalculate =
                    (from a in kPiDataContext.SecondLevelSubdivisionTable
                        join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                            on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                        where b.FK_ReportArchiveTableId == reportId
                        select a).Distinct().ToList();
                //теперь пройдемся поочереди по каждому показателю
                //первым делом посчитаем показатель для КФУ
                //потом посчитаем показатель для каждого структурного 1-го уровня
                //потом посчитаем показатель для каждого структурного 2-го уровня
                //поехали:)
                #endregion
                foreach (IndicatorsTable currentIndicator in indicatorsToCalculateList)
                    //считаем каждый показатель для каждого факультета, каждой академии и всего КФУ
                {
                    #region calcForCFU

                    {
                        //считай для КФУ
                        ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
                        double tmp =
                            ForRCalc.CalculatedForDB(
                                (float)
                                    ForRCalc.GetCalculatedWithParams(mainStruct, 0, currentIndicator.IndicatorsTableID,
                                        reportId, 0, 0 /*тут должен быть ID проректора*/));
                        CollectedIndicatorsForR newCollected = new CollectedIndicatorsForR();
                        newCollected.Active = true;
                        newCollected.CreatedDateTime = DateTime.Now;
                        newCollected.FK_ReportArchiveTable = reportId;
                        newCollected.FK_IndicatorsTable = currentIndicator.IndicatorsTableID;
                        newCollected.FK_FirstLevelSubdivisionTable =  null;
                        newCollected.FK_SecondLevelSubdivisionTable = null;
                        newCollected.FK_ThirdLevelSubdivisionTable =  null;
                        newCollected.FK_FourthLelevlSubdivisionTable = null;
                        newCollected.FK_FifthLevelSubdivisionTable = null;
                        if (tmp > (float) 1E+19)
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

                    //kPiDataContext.SubmitChanges();

                    #region calcForAcademys

                    foreach (FirstLevelSubdivisionTable currentFirstLevel in firstLevelToCalculate)
                    {
                        //считай для академий
                        ForRCalc.Struct mainStruct =
                            mainStruct =
                                new ForRCalc.Struct(1, currentFirstLevel.FirstLevelSubdivisionTableID, 0, 0, 0, "N");
                        double tmp =
                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0,
                                currentIndicator.IndicatorsTableID, reportId, 0,0 /*тут должен быть ID проректора*/));
                        CollectedIndicatorsForR newCollected = new CollectedIndicatorsForR
                        {
                            Active = true,
                            CreatedDateTime = DateTime.Now,
                            FK_ReportArchiveTable = reportId,
                            FK_IndicatorsTable = currentIndicator.IndicatorsTableID,
                            FK_FirstLevelSubdivisionTable = currentFirstLevel.FirstLevelSubdivisionTableID,
                            FK_SecondLevelSubdivisionTable = null,
                            FK_ThirdLevelSubdivisionTable = null,
                            FK_FourthLelevlSubdivisionTable = null,
                            FK_FifthLevelSubdivisionTable = null
                        };
                        if (tmp > (float) 1E+19)
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

                    //kPiDataContext.SubmitChanges();

                    #region CalcForFaculys

                    foreach (SecondLevelSubdivisionTable currentSecondLevel in secondLevelToCalculate)
                    {
                        // считай для кафедр
                        ForRCalc.Struct mainStruct =
                            mainStruct =
                                new ForRCalc.Struct(1, currentSecondLevel.FK_FirstLevelSubdivisionTable,
                                    currentSecondLevel.SecondLevelSubdivisionTableID, 0, 0, "N");
                        double tmp =
                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0,
                                currentIndicator.IndicatorsTableID, reportId, 0, 0 /*тут должен быть ID проректора*/));
                        CollectedIndicatorsForR newCollected = new CollectedIndicatorsForR();
                        newCollected.Active = true;
                        newCollected.CreatedDateTime = DateTime.Now;
                        newCollected.FK_ReportArchiveTable = reportId;
                        newCollected.FK_IndicatorsTable = currentIndicator.IndicatorsTableID;
                        newCollected.FK_FirstLevelSubdivisionTable = currentSecondLevel.FK_FirstLevelSubdivisionTable;
                        newCollected.FK_SecondLevelSubdivisionTable = currentSecondLevel.SecondLevelSubdivisionTableID;
                        newCollected.FK_ThirdLevelSubdivisionTable = null;
                        newCollected.FK_FourthLelevlSubdivisionTable = null;
                        newCollected.FK_FifthLevelSubdivisionTable = null;
                        if (tmp > (float) 1E+19)
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
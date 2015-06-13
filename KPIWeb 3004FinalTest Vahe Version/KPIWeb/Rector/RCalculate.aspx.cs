using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RCalculate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int ReportID = 1; // пока только один отчет и надо сделать быстро)
            #region init
            //сначала берем все показатели целевые которые включены в выбранный отчет
            //находим все структурные подразделения 1-го уровня которые участвуют в этом отчете
            //находим все структурные подразделения 2-го уровня которые участвуют в этом отчете
            List<IndicatorsTable> IndicatorsToCalculateList = (from a in kPiDataContext.IndicatorsTable 
                                     join b in kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                                         on a.IndicatorsTableID equals b.FK_IndicatorsTable
                                         where b.FK_ReportArchiveTable == ReportID
                                         && b.Active == true
                                         select a).ToList();

            List<FirstLevelSubdivisionTable> FirstLevelToCalculate = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                                                      join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                                                                          on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubmisionTableId
                                                                      where b.FK_ReportArchiveTableId == ReportID
                                                                      select a).ToList();
            List<SecondLevelSubdivisionTable> SecondLevelToCalculate = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                                                        join b in kPiDataContext.FirstLevelSubdivisionTable
                                                                            on a.FK_FirstLevelSubdivisionTable equals b.FirstLevelSubdivisionTableID
                                                                        join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                                                            on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId
                                                                        where c.FK_ReportArchiveTableId == ReportID
                                                                        select a).ToList();
            //теперь пройдемся поочереди по каждому показателю
            //первым делом посчитаем показатель для КФУ
            //потом посчитаем показатель для каждого структурного 1-го уровня
            //потом посчитаем показатель для каждого структурного 2-го уровня
            //поехали:)
            #endregion
            foreach (IndicatorsTable CurrentIndicator in IndicatorsToCalculateList) //считаем каждый показатель для каждого факультета, каждой академии и всего КФУ
            {               
                #region calcForCFU
                {
                    //считай для КФУ
                    ForRCalc.Struct mainStruct = mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
                    float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0, CurrentIndicator.IndicatorsTableID, ReportID, 0));
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
                    if (tmp == (float)1E+20)
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
                #region calcForAcademys
                foreach (FirstLevelSubdivisionTable CurrentFirstLevel in FirstLevelToCalculate)
                {
                    //считай для академий
                    ForRCalc.Struct mainStruct = mainStruct = new ForRCalc.Struct(1, CurrentFirstLevel.FirstLevelSubdivisionTableID, 0, 0, 0, "N");
                    float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0, CurrentIndicator.IndicatorsTableID, ReportID, 0));
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
                    if (tmp == (float)1E+20)
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
                #region CalcForFaculys
                foreach (SecondLevelSubdivisionTable CurrentSecondLevel in SecondLevelToCalculate)
                {
                    // считай для кафедр
                    ForRCalc.Struct mainStruct = mainStruct = new ForRCalc.Struct(1, CurrentSecondLevel.FK_FirstLevelSubdivisionTable, CurrentSecondLevel.SecondLevelSubdivisionTableID, 0, 0, "N");
                    float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0, CurrentIndicator.IndicatorsTableID, ReportID, 0));
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
                    if (tmp == (float)1E+20)
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
            }
            kPiDataContext.SubmitChanges();           
        }
    }
}


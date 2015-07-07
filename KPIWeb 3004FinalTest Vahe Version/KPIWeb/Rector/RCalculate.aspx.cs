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
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Default.aspx");
            }
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
                                         select a).Distinct().OrderBy(c => c.SortID).ToList();

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
                if (CurrentIndicator.IndicatorsTableID != 1027)
                    continue;
       
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
                kPiDataContext.SubmitChanges();  
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
                kPiDataContext.SubmitChanges();  
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
                kPiDataContext.SubmitChanges();  
            }
                     
        }

        #region patterns
        protected double pattern1(UsersTable user, int ReportArchiveID, int spectype_, string basicAbb, string basicAbb2) // по областям знаний
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                         join z in kpiWebDataContext.BasicParametersTable
                         on a.FK_BasicParametersTable equals z.BasicParametersTableID
                         join b in kpiWebDataContext.FourthLevelParametrs
                         on a.FK_FourthLevelSubdivisionTable equals b.FourthLevelParametrsID
                         join c in kpiWebDataContext.ThirdLevelParametrs
                         on a.FK_ThirdLevelSubdivisionTable equals c.ThirdLevelParametrsID
                         join d in kpiWebDataContext.FourthLevelSubdivisionTable
                         on a.FK_FourthLevelSubdivisionTable equals d.FourthLevelSubdivisionTableID
                         join e in kpiWebDataContext.SpecializationTable
                         on d.FK_Specialization equals e.SpecializationTableID
                         where
                            a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                         && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                         && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                         && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                         && ((z.AbbreviationEN == basicAbb) || ((basicAbb2 != null) && (z.AbbreviationEN == basicAbb2)))
                         && a.FK_ReportArchiveTable == ReportArchiveID
                         && b.SpecType == spectype_
                         && a.Active == true
                         && d.Active == true
                         && (e.FK_FieldOfExpertise == 10 || e.FK_FieldOfExpertise == 11 || e.FK_FieldOfExpertise == 12)
                         select a.CollectedValue).Sum());
        }
        protected double pattern2(UsersTable user, int ReportArchiveID, string basicAbb) // для инстранцев
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                      (from a in kpiWebDataContext.CollectedBasicParametersTable
                       join z in kpiWebDataContext.BasicParametersTable
                       on a.FK_BasicParametersTable equals z.BasicParametersTableID
                       join b in kpiWebDataContext.FourthLevelParametrs
                       on a.FK_FourthLevelSubdivisionTable equals b.FourthLevelParametrsID
                       where
                          a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                       && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                       && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                       && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                       && z.AbbreviationEN == basicAbb
                       && a.FK_ReportArchiveTable == ReportArchiveID
                       && b.IsForeignStudentsAccept == true
                       && a.Active == true
                       && z.Active == true
                       && b.Active == true
                       select a.CollectedValue).Sum());
        }
        protected double pattern3(UsersTable user, int ReportArchiveID, int SpecType) // Кол-во ООП // считает кол-во прикрепленных специальностьей
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && a.Active == true
                     // && z.Active == true
                       && b.Active == true
                 select b).ToList().Count);

        }
        protected double pattern4(UsersTable user, int ReportArchiveID, int SpecType) // Кол-во ООП с условиями для инвалидов
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && c.IsInvalidStudentsFacilities == true
                       && a.Active == true
                       && b.Active == true
                 select b).ToList().Count);
        }
        protected double pattern5(UsersTable user, int ReportArchiveID, int SpecType)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && c.IsNetworkComunication == true
                       && a.Active == true
                       && b.Active == true
                 select b).ToList().Count);
        }
        protected double pattern6(UsersTable user, int ReportArchiveID, int SpecType)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && a.Active == true
                       && b.Active == true
                       && c.IsModernEducationTechnologies == true
                 select b).ToList().Count);
        }
        protected double pattern7(int SpecID, int typeOfCost, int ReportID, FourthLevelSubdivisionTable Fourth, int SpecType) // type 0 очное // 1 очное иностранцы // 2 заочное // 3 вечернее
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            if (typeOfCost == 0)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                               where a.Active == true
                               && a.FK_Specialization == SpecID
                               select a.CostOfCommercOch).FirstOrDefault()
                             *
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                       where a.FK_ReportArchiveTable == ReportID
                                       && a.FK_FourthLevelSubdivisionTable == Fourth.FourthLevelSubdivisionTableID
                                       join c in kpiWebDataContext.BasicParametersTable
                                       on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                       where

                                       ((c.AbbreviationEN == "a_Och_M_Kom" && SpecType == 3)
                                       || (c.AbbreviationEN == "a_Och_B_Kom" && SpecType == 1)
                                       || (c.AbbreviationEN == "a_Och_S_Kom" && SpecType == 2)
                                       || (c.AbbreviationEN == "a_Och_A_Kom" && SpecType == 4))

                                       select a.CollectedValue).Sum());
            }
            else if (typeOfCost == 1)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == SpecID
                                         select a.CostOfCommercOchIn).FirstOrDefault()

                                         *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == ReportID
                                  && a.FK_FourthLevelSubdivisionTable == Fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where 
                                  ((c.AbbreviationEN == "a_Och_In_M"&&SpecType == 3) 
                                      || (c.AbbreviationEN == "a_Och_In_B"&&SpecType == 1) 
                                      || (c.AbbreviationEN == "a_Och_In_S"&&SpecType == 2) 
                                      || (c.AbbreviationEN == "a_Och_In_A"&&SpecType == 4))

                                  select a.CollectedValue).Sum());

            }
            else if (typeOfCost == 2)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == SpecID
                                         select a.CostOfCommercZaoch).FirstOrDefault()

                                          *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == ReportID
                                  && a.FK_FourthLevelSubdivisionTable == Fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "c_Z_A_Kom" && SpecType == 3)
                                  || (c.AbbreviationEN == "c_Z_B_Kom" && SpecType == 1)
                                  || (c.AbbreviationEN == "c_Z_S_Kom" && SpecType == 2)
                                  || (c.AbbreviationEN == "c_Z_M_Kom" && SpecType == 4))
                                  select a.CollectedValue).Sum());
            }
            else if (typeOfCost == 3)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == SpecID
                                         select a.CostOfCommercEvening).FirstOrDefault()

                                               *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == ReportID
                                  && a.FK_FourthLevelSubdivisionTable == Fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "b_OchZ_S_Kom" && SpecType == 3)
                                  || (c.AbbreviationEN == "b_OchZ_M_Kom" && SpecType == 1)
                                  || (c.AbbreviationEN == "b_OchZ_A_Kom" && SpecType == 2)
                                  || (c.AbbreviationEN == "b_OchZ_B_Kom" && SpecType == 4))
                                  select a.CollectedValue).Sum());
            }
            return 0;
        }
        protected double pattern8(int SpecID, int typeOfCost, int ReportID, FourthLevelSubdivisionTable Fourth, int SpecType) // type 0 очное // 1 заочное
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            if (typeOfCost == 0)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == SpecID
                                         select a.CostOfBudjetOch).FirstOrDefault()
                             *
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                     where a.FK_ReportArchiveTable == ReportID
                     && a.FK_FourthLevelSubdivisionTable == Fourth.FourthLevelSubdivisionTableID
                     join c in kpiWebDataContext.BasicParametersTable
                     on a.FK_BasicParametersTable equals c.BasicParametersTableID
                     where

                     ((c.AbbreviationEN == "a_Och_M" && SpecType == 3)
                     || (c.AbbreviationEN == "a_Och_B" && SpecType == 1)
                     || (c.AbbreviationEN == "a_Och_S" && SpecType == 2)
                     || (c.AbbreviationEN == "a_Och_A" && SpecType == 4))

                     select a.CollectedValue).Sum());
            }
            else if (typeOfCost == 1)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == SpecID
                                         select a.CostOfBudjetZaoch).FirstOrDefault()

                                          *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == ReportID
                                  && a.FK_FourthLevelSubdivisionTable == Fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "c_Z_A" && SpecType == 4)
                                  || (c.AbbreviationEN == "c_Z_B" && SpecType == 1)
                                  || (c.AbbreviationEN == "c_Z_S" && SpecType == 2)
                                  || (c.AbbreviationEN == "c_Z_M" && SpecType == 3))
                                  select a.CollectedValue).Sum());
            }
            return 0;
        }

        public void patternSwitch(int ReportArchiveID, BasicParametersTable basicParam, FourthLevelSubdivisionTable FourthLevel, int fourthCnt, UsersTable user)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            double tmp = 1000000000001;
            if (fourthCnt > 0)
            {
                if (FourthLevel == null)
                {
                    if (basicParam.AbbreviationEN == "a_Och_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "a_Och_M", "a_Och_M_Kom");
                    if (basicParam.AbbreviationEN == "b_OchZ_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "b_OchZ_M", "b_OchZ_M_Kom");
                    if (basicParam.AbbreviationEN == "c_Z_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "c_Z_M", "c_Z_M_Kom");
                    if (basicParam.AbbreviationEN == "d_E_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "d_E_M", "d_E_M_Kom");

                    if (basicParam.AbbreviationEN == "a_Och_M_NoIn") tmp = pattern2(user, ReportArchiveID, "a_Och_M");
                    if (basicParam.AbbreviationEN == "b_OchZ_M_NoIn") tmp = pattern2(user, ReportArchiveID, "b_OchZ_M");
                    if (basicParam.AbbreviationEN == "c_Z_M_NoIn") tmp = pattern2(user, ReportArchiveID, "c_Z_M");
                    if (basicParam.AbbreviationEN == "d_E_M_NoIn") tmp = pattern2(user, ReportArchiveID, "d_E_M");

                    if (basicParam.AbbreviationEN == "a_Och_M_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "a_Och_M_Kom");
                    if (basicParam.AbbreviationEN == "b_OchZ_M_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "b_OchZ_M_Kom");
                    if (basicParam.AbbreviationEN == "c_Z_M_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "c_Z_M_Kom");
                    if (basicParam.AbbreviationEN == "d_E_M_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "d_E_M_Kom");

                    if (basicParam.AbbreviationEN == "a_Och_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "a_Och_S", "a_Och_S_Kom");
                    if (basicParam.AbbreviationEN == "b_OchZ_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "b_OchZ_S", "b_OchZ_S_Kom");
                    if (basicParam.AbbreviationEN == "c_Z_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "c_Z_S", "c_Z_S_Kom");
                    if (basicParam.AbbreviationEN == "d_E_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "d_E_S", "c_Z_S_Kom");

                    if (basicParam.AbbreviationEN == "a_Och_S_NoIn") tmp = pattern2(user, ReportArchiveID, "a_Och_S");
                    if (basicParam.AbbreviationEN == "b_OchZ_S_NoIn") tmp = pattern2(user, ReportArchiveID, "b_OchZ_S");
                    if (basicParam.AbbreviationEN == "c_Z_S_NoIn") tmp = pattern2(user, ReportArchiveID, "c_Z_S");
                    if (basicParam.AbbreviationEN == "d_E_S_NoIn") tmp = pattern2(user, ReportArchiveID, "d_E_S");

                    if (basicParam.AbbreviationEN == "a_Och_S_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "a_Och_S_Kom");
                    if (basicParam.AbbreviationEN == "b_OchZ_S_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "b_OchZ_S_Kom");
                    if (basicParam.AbbreviationEN == "c_Z_S_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "c_Z_S_Kom");
                    if (basicParam.AbbreviationEN == "d_E_S_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "d_E_S_Kom");

                    if (basicParam.AbbreviationEN == "a_Och_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "a_Och_B", "a_Och_B_Kom");
                    if (basicParam.AbbreviationEN == "b_OchZ_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "b_OchZ_B", "b_OchZ_B_Kom");
                    if (basicParam.AbbreviationEN == "c_Z_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "c_Z_B", "c_Z_B_Kom");
                    if (basicParam.AbbreviationEN == "d_E_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "d_E_B", "d_E_B_Kom");

                    if (basicParam.AbbreviationEN == "a_Och_B_NoIn") tmp = pattern2(user, ReportArchiveID, "a_Och_B");
                    if (basicParam.AbbreviationEN == "b_OchZ_B_NoIn") tmp = pattern2(user, ReportArchiveID, "b_OchZ_B");
                    if (basicParam.AbbreviationEN == "c_Z_B_NoIn") tmp = pattern2(user, ReportArchiveID, "c_Z_B");
                    if (basicParam.AbbreviationEN == "d_E_B_NoIn") tmp = pattern2(user, ReportArchiveID, "d_E_B");

                    if (basicParam.AbbreviationEN == "a_Och_B_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "a_Och_B_Kom");
                    if (basicParam.AbbreviationEN == "b_OchZ_B_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "b_OchZ_B_Kom");
                    if (basicParam.AbbreviationEN == "c_Z_B_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "c_Z_B_Kom");
                    if (basicParam.AbbreviationEN == "d_E_B_NoIn_Kom") tmp = pattern2(user, ReportArchiveID, "d_E_B_Kom");

                    if (basicParam.AbbreviationEN == "OOP_M") tmp = pattern3(user, ReportArchiveID, 3);
                    if (basicParam.AbbreviationEN == "kol_M_OP") tmp = pattern4(user, ReportArchiveID, 3);
                    if (basicParam.AbbreviationEN == "kol_M_OP_SV") tmp = pattern5(user, ReportArchiveID, 3);
                    if (basicParam.AbbreviationEN == "OOP_M_SOT") tmp = pattern6(user, ReportArchiveID, 3);

                    if (basicParam.AbbreviationEN == "OOP_S") tmp = pattern3(user, ReportArchiveID, 2);
                    if (basicParam.AbbreviationEN == "kol_S_OP") tmp = pattern4(user, ReportArchiveID, 2);
                    if (basicParam.AbbreviationEN == "kol_S_OP_SV") tmp = pattern5(user, ReportArchiveID, 2);
                    if (basicParam.AbbreviationEN == "OOP_S_SOT") tmp = pattern6(user, ReportArchiveID, 2);

                    if (basicParam.AbbreviationEN == "OOP_B") tmp = pattern3(user, ReportArchiveID, 1);
                    if (basicParam.AbbreviationEN == "kol_B_OP") tmp = pattern4(user, ReportArchiveID, 1);
                    if (basicParam.AbbreviationEN == "kol_B_OP_SV") tmp = pattern5(user, ReportArchiveID, 1);
                    if (basicParam.AbbreviationEN == "OOP_B_SOT") tmp = pattern6(user, ReportArchiveID, 1);

                    if (basicParam.AbbreviationEN == "OOP_A") tmp = pattern3(user, ReportArchiveID, 4);
                    if (basicParam.AbbreviationEN == "kol_A_OP") tmp = pattern4(user, ReportArchiveID, 4);
                    if (basicParam.AbbreviationEN == "kol_A_OP_SV") tmp = pattern5(user, ReportArchiveID, 4);
                    if (basicParam.AbbreviationEN == "OOP_A_SOT") tmp = pattern6(user, ReportArchiveID, 4);

                    //новые показатели 13.06.2015
                    if (basicParam.AbbreviationEN == "a_Och_M_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 3, "a_Och_M_C", null);
                    if (basicParam.AbbreviationEN == "b_OchZ_M_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 3, "b_OchZ_M_C", null);
                    if (basicParam.AbbreviationEN == "c_Z_M_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 3, "c_Z_M_C", null);
                    if (basicParam.AbbreviationEN == "d_E_M_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 3, "d_E_M_C", null);

                    if (basicParam.AbbreviationEN == "a_Och_B_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 1, "a_Och_B_C", null);
                    if (basicParam.AbbreviationEN == "d_E_B_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 1, "d_E_B_C", null);
                    if (basicParam.AbbreviationEN == "c_Z_B_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 1, "c_Z_B_C", null);
                    if (basicParam.AbbreviationEN == "d_E_B_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 1, "d_E_B_C", null);

                    if (basicParam.AbbreviationEN == "a_Och_S_CO_R") tmp =
                        pattern1(user, ReportArchiveID, 2, "a_Och_S_C", null);
                    if (basicParam.AbbreviationEN == "b_OchZ_S_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 2, "b_OchZ_S_C", null);
                    if (basicParam.AbbreviationEN == "c_Z_S_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 2, "c_Z_S_C", null);
                    if (basicParam.AbbreviationEN == "d_E_S_CO_R") tmp = 
                        pattern1(user, ReportArchiveID, 2, "d_E_S_C", null);
                    //новые показатели 
                }
                else
                {
                    //новейшие показатели 19.06.2015  
                    //// type 0 очное // 1 очное иностранцы // 2 заочное // 3 вечернее
                    if (basicParam.AbbreviationEN == "a_Och_B_Kom_money") tmp = 
                        pattern7(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel,1);
                    if (basicParam.AbbreviationEN == "a_OchZ_B_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 3, ReportArchiveID, FourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_Z_B_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 2, ReportArchiveID, FourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_IN_B_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 1);

                    if (basicParam.AbbreviationEN == "a_Och_S_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_OchZ_S_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 3, ReportArchiveID, FourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_Z_S_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 2, ReportArchiveID, FourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_IN_S_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 2);


                    if (basicParam.AbbreviationEN == "a_Och_M_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_OchZ_M_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 3, ReportArchiveID, FourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_Z_M_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 2, ReportArchiveID, FourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_IN_M_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 3);

                    if (basicParam.AbbreviationEN == "a_Och_A_Kom_money") tmp = 
                        pattern7(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_OchZ_A_Kom_money") tmp =
                        pattern7(FourthLevel.FK_Specialization, 3, ReportArchiveID, FourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_Z_A_Kom_money") tmp = 
                        pattern7(FourthLevel.FK_Specialization, 2, ReportArchiveID, FourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_IN_A_Kom_money") tmp = 
                        pattern7(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 4);


                    // 01.07.2015
                    if (basicParam.AbbreviationEN == "a_Och_A_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_Z_A_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 4);

                    if (basicParam.AbbreviationEN == "a_Och_M_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_Z_M_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 3);

                    if (basicParam.AbbreviationEN == "a_Och_S_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_Z_S_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 2);

                    if (basicParam.AbbreviationEN == "a_Och_B_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_Z_B_money") tmp =
                        pattern8(FourthLevel.FK_Specialization, 1, ReportArchiveID, FourthLevel, 1);
                    //01.07.2015

                }
                //новейшие показатели
            }
            else
            {
                if (basicParam.AbbreviationEN == "Kol_Kaf_R")
                    tmp = Convert.ToDouble((from a in kpiWebDataContext.ThirdLevelParametrs
                                            where a.ThirdLevelParametrsID == user.FK_ThirdLevelSubdivisionTable
                                            select a.IsBasic).FirstOrDefault());
            }
            if (tmp < 1000000000000)
            {
                CollectedBasicParametersTable collectedBasicTmp = new CollectedBasicParametersTable();
                int basicParamLevel = (int)(from a in kpiWebDataContext.BasicParametrAdditional
                                            where a.BasicParametrAdditionalID == basicParam.BasicParametersTableID
                                            select a.SubvisionLevel).FirstOrDefault();
                if (basicParamLevel == 3)
                {
                    collectedBasicTmp =
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                     where a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                         && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                         && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                         && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                         && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                         && a.FK_ReportArchiveTable == ReportArchiveID
                     select a).FirstOrDefault();
                }
                else
                {
                    collectedBasicTmp =
                       (from a in kpiWebDataContext.CollectedBasicParametersTable
                        where a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                            && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                            && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                            && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                            && a.FK_FourthLevelSubdivisionTable == FourthLevel.FourthLevelSubdivisionTableID
                            && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                            && a.FK_ReportArchiveTable == ReportArchiveID
                        select a).FirstOrDefault();
                }

                if (collectedBasicTmp == null) // надо создать
                {
                    collectedBasicTmp = new CollectedBasicParametersTable();
                    collectedBasicTmp.Active = true;
                    collectedBasicTmp.Status = 0;
                    collectedBasicTmp.FK_UsersTable = user.UsersTableID;
                    collectedBasicTmp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                    collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                    collectedBasicTmp.CollectedValue = tmp;
                    collectedBasicTmp.UserIP = "0.0.0.0";//Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                    collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                    collectedBasicTmp.SavedDateTime = DateTime.Now;
                    collectedBasicTmp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                    collectedBasicTmp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                    collectedBasicTmp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                    collectedBasicTmp.FK_ThirdLevelSubdivisionTable = user.FK_ThirdLevelSubdivisionTable;

                    if (basicParamLevel == 4)
                    {
                        collectedBasicTmp.FK_FourthLevelSubdivisionTable = FourthLevel.FourthLevelSubdivisionTableID;
                    }

                    kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                    kpiWebDataContext.SubmitChanges();
                }
                else
                {
                    collectedBasicTmp.CollectedValue = tmp;
                    kpiWebDataContext.SubmitChanges();
                }
            }
        }
        /*
        protected void ConfCalculate(int ReportArchiveID, UsersTable user)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<BasicParametersTable> calcBasicParams =
            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
             join b in kpiWebDataContext.BasicParametersTable
             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
             on b.BasicParametersTableID equals c.FK_ParametrsTable
             join d in kpiWebDataContext.BasicParametrAdditional
             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
             where
                   a.FK_ReportArchiveTable == ReportArchiveID  //из нужного отчёта
                && c.FK_UsersTable == user.UsersTableID // свяный с пользователем
                && (d.SubvisionLevel == 3 || d.SubvisionLevel == 4)//нужный уровень заполняющего
                && a.Active == true  // запись в таблице связей показателя и отчёта активна
                && c.Active == true  // запись в таблице связей показателя и пользователей активна
                && d.Calculated == true // этот показатель нужно считать
             select b).ToList();

            //узнали показатели кафедры(отчёт,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
            foreach (BasicParametersTable basicParam in calcBasicParams) //пройдемся по показателям
            {
                CollectedBasicParametersTable collectedBasicTmp =
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                     where a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                           && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                           && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                           && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                           && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                           && a.FK_ReportArchiveTable == ReportArchiveID
                     select a).FirstOrDefault();
                if (collectedBasicTmp != null) // надо создать
                {
                    collectedBasicTmp.Status = 4;
                    kpiWebDataContext.SubmitChanges();
                }
            }
        }
        */
        protected void CalcCalculate(int ReportArchiveID, UsersTable user)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<BasicParametersTable> calcBasicParams =
            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
             join b in kpiWebDataContext.BasicParametersTable
             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
             on b.BasicParametersTableID equals c.FK_ParametrsTable
             join d in kpiWebDataContext.BasicParametrAdditional
             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
             where
                   a.FK_ReportArchiveTable == ReportArchiveID  //из нужного отчёта
                && c.FK_UsersTable == user.UsersTableID // свяный с пользователем
                && (d.SubvisionLevel == 3 || d.SubvisionLevel == 4)//нужный уровень заполняющего
                && a.Active == true  // запись в таблице связей показателя и отчёта активна
                && c.Active == true  // запись в таблице связей показателя и пользователей активна
                && d.Calculated == true // этот показатель нужно считать
             select b).ToList();

            #region toDelete
            /*
            ThirdLevelParametrs Cangrad = (from a in kpiWebDataContext.ThirdLevelParametrs
                                           where a.CanGraduate == true
                                           && a.ThirdLevelParametrsID == user.FK_ThirdLevelSubdivisionTable
                                           select a).FirstOrDefault();
            if (Cangrad != null) // кафедра выпускает
            {
                //определим какого типа специальности есть на данной кафедре             
                bool AnyB = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 1
                             select a).ToList().Count() > 0 ? true : false;

                bool AnyS = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 2
                             select a).ToList().Count() > 0 ? true : false;

                bool AnyM = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 3
                             select a).ToList().Count() > 0 ? true : false;

                bool AnyA = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 4
                             select a).ToList().Count() > 0 ? true : false;
                //узнали показатели кафедры(отчёт,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)     
             */
            #endregion

            List<FourthLevelSubdivisionTable> FourtLevels = new List<FourthLevelSubdivisionTable>();

            if (user.FK_ThirdLevelSubdivisionTable != null)
            {
                FourtLevels = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                               where a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                               && a.Active == true
                               select a).ToList();
            }

            foreach (BasicParametersTable basicParam in calcBasicParams) //пройдемся по показателям
            {
                if (basicParam.BasicParametersTableID != 3912)
                    continue;
                int ii = (int)(from a in kpiWebDataContext.BasicParametrAdditional
                               where a.BasicParametrAdditionalID == basicParam.BasicParametersTableID
                               select a.SubvisionLevel).FirstOrDefault();
                if (ii == 4)
                {
                    if (FourtLevels.Count() > 0) // есть хоть одна специальность
                    {
                        foreach (FourthLevelSubdivisionTable CurrentFourth in FourtLevels)
                        {
                            patternSwitch(ReportArchiveID, basicParam, CurrentFourth, FourtLevels.Count(), user); // считаем для каждой специальности
                        }
                    }

                }
                else
                {
                    patternSwitch(ReportArchiveID, basicParam, null, FourtLevels.Count(), user);
                }

            }
            // }

        }
        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<ThirdLevelSubdivisionTable> ThirdLevelList = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                                               join b in kPiDataContext.CollectedBasicParametersTable
                                                                   on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                               where 
                                                               b.CollectedValue !=null
                                                               &&
                                                               b.FK_ReportArchiveTable == 1
                                                               select a).Distinct().ToList();

/*          UsersTable UUUUUSER = (from a in kPiDataContext.UsersTable

                                   where a.UsersTableID == 12489
                                           select a).FirstOrDefault();
            CalcCalculate(1, UUUUUSER);
*/
            
             //   ThirdLevelList.Clear();
              //      ThirdLevelList.Add((from a in kPiDataContext.ThirdLevelSubdivisionTable where a.ThirdLevelSubdivisionTableID == 4684 select a).FirstOrDefault());
            foreach (ThirdLevelSubdivisionTable thirdLevel in ThirdLevelList)
            {
                UsersTable UsersToCalculate = (from a in kPiDataContext.UsersTable
                                                 join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                     on a.UsersTableID equals b.FK_UsersTable
                                                     //join c in kPiDataContext.CollectedBasicParametersTable 
                                                    // on b.FK_ParametrsTable equals c.FK_BasicParametersTable
                                                 where a.FK_ThirdLevelSubdivisionTable == thirdLevel.ThirdLevelSubdivisionTableID
                                                         && b.CanEdit == true select a).FirstOrDefault();
                if (UsersToCalculate != null)
                CalcCalculate(1, UsersToCalculate);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<ThirdLevelSubdivisionTable> ThirdLevelList = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                                    where a.Active == true
                                                    select a).ToList();
            TextBox1.Text = "";
            foreach (ThirdLevelSubdivisionTable CurrentThird in ThirdLevelList)
            {
                int ConfirmedCnt = (from a in kPiDataContext.CollectedBasicParametersTable
                                    where a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                    && a.Active == true
                                    && a.Status == 4
                                    && a.FK_ReportArchiveTable == 1
                                    select a).Count();
                CollectedBasicParametersTable FirstCollected = (from a in kPiDataContext.CollectedBasicParametersTable
                                                                where a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                                && a.FK_ReportArchiveTable == 1
                                                                && a.Active == true
                                                                select a).FirstOrDefault();
                
                if ((ConfirmedCnt>10)&&(FirstCollected.Status == 4))
                {
                    TextBox1.Text += "утверждено" + ConfirmedCnt.ToString();
                    TextBox1.Text += " всего" + (from a in kPiDataContext.CollectedBasicParametersTable
                                                            where a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                            && a.FK_ReportArchiveTable == 1
                                                            && a.Active == true
                                                            select a).Count().ToString();
                    
                    List<CollectedBasicParametersTable> CollectedToChange = (from a in kPiDataContext.CollectedBasicParametersTable
                                                                             where a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                                             && a.FK_ReportArchiveTable == 1
                                                                             && a.Active == true
                                                                             select a).ToList();
                    foreach (CollectedBasicParametersTable CollectedBasic in CollectedToChange)
                    {
                        CollectedBasic.Status = 4;
                    }
                    kPiDataContext.SubmitChanges();
                    TextBox1.Text += " сделано";
                    TextBox1.Text += Environment.NewLine;
                }
                else if (ConfirmedCnt > 0)
                {
                    TextBox1.Text += "ОШИБКА утверждено" + ConfirmedCnt.ToString();
                    TextBox1.Text += " не утверждено" + (from a in kPiDataContext.CollectedBasicParametersTable
                                                        where a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                        && a.FK_ReportArchiveTable == 1
                                                        && a.Active == true
                                                        select a).Count().ToString();

                    TextBox1.Text += " ID 3-го уровня" + CurrentThird.ThirdLevelSubdivisionTableID.ToString();
                    TextBox1.Text += Environment.NewLine;
                }
                else if ((FirstCollected!=null)&&(FirstCollected.Status == 4))
                {
                    TextBox1.Text += "ОШИБКА утверждено" + ConfirmedCnt.ToString();
                    TextBox1.Text += " не утверждено" + (from a in kPiDataContext.CollectedBasicParametersTable
                                                        where a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                        && a.FK_ReportArchiveTable == 1
                                                        && a.Active == true
                                                        select a).Count().ToString();
                    TextBox1.Text += " ID 3-го уровня" + CurrentThird.ThirdLevelSubdivisionTableID.ToString();
                    TextBox1.Text += Environment.NewLine;
                }
                else
                {

                }

            }
            

        }


        public void gogo (int ParamID,int UserID)
    {
        KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////
                BasicParametrsAndUsersMapping newconnection = (from a in kPiDataContext.BasicParametrsAndUsersMapping
                                                               where a.Active == true
                                                               && a.FK_ParametrsTable == ParamID
                                                               && a.FK_UsersTable == UserID
                                                               select a).FirstOrDefault();
                if (newconnection == null)
                {
                    newconnection = new BasicParametrsAndUsersMapping();
                    newconnection.FK_UsersTable = UserID;
                    newconnection.FK_ParametrsTable = ParamID;
                    newconnection.Active = true;
                    newconnection.CanView = true;
                    kPiDataContext.BasicParametrsAndUsersMapping.InsertOnSubmit(newconnection);
                }
                else
                {
                    newconnection.Active = true;
                    newconnection.CanView = true;
                }
                kPiDataContext.SubmitChanges();
                //////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
        public void gogo2(int ParamID, int UserID)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            BasicParametrsAndUsersMapping newconnection = (from a in kPiDataContext.BasicParametrsAndUsersMapping
                                                           where a.Active == true
                                                           && a.FK_ParametrsTable == ParamID
                                                           && a.FK_UsersTable == UserID
                                                           select a).FirstOrDefault();
            if (newconnection == null)
            {
                newconnection = new BasicParametrsAndUsersMapping();
                newconnection.FK_UsersTable = UserID;
                newconnection.FK_ParametrsTable = ParamID;
                newconnection.Active = true;
                newconnection.CanEdit = true;
                kPiDataContext.BasicParametrsAndUsersMapping.InsertOnSubmit(newconnection);
            }
            else
            {
                newconnection.Active = true;
                newconnection.CanEdit = true;
            }
            kPiDataContext.SubmitChanges();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        public void gogo3(int ParamID, int UserID)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            BasicParametrsAndUsersMapping newconnection = (from a in kPiDataContext.BasicParametrsAndUsersMapping
                                                           where a.Active == true
                                                           && a.FK_ParametrsTable == ParamID
                                                           && a.FK_UsersTable == UserID
                                                           select a).FirstOrDefault();
            if (newconnection == null)
            {
                newconnection = new BasicParametrsAndUsersMapping();
                newconnection.FK_UsersTable = UserID;
                newconnection.FK_ParametrsTable = ParamID;
                newconnection.Active = true;
                newconnection.CanConfirm = true;
                kPiDataContext.BasicParametrsAndUsersMapping.InsertOnSubmit(newconnection);
            }
            else
            {
                newconnection.Active = true;
                newconnection.CanConfirm = true;
            }
            kPiDataContext.SubmitChanges();
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
             KPIWebDataContext kPiDataContext = new KPIWebDataContext();
             List<UsersTable> UsersList = (from a in kPiDataContext.UsersTable
                                           join b in kPiDataContext.BasicParametrsAndUsersMapping
                                               on a.UsersTableID equals b.FK_UsersTable
                                           where a.Active == true
                                           && b.Active == true
                                           && b.CanView == true
                                           && b.FK_ParametrsTable == 3605
                                           select a).Distinct().ToList();
            foreach (UsersTable currentUser in UsersList)
            {
                gogo(3912, currentUser.UsersTableID);
                gogo(3914, currentUser.UsersTableID);
                gogo(3916, currentUser.UsersTableID);
                gogo(3918, currentUser.UsersTableID);
                gogo(3920, currentUser.UsersTableID);
                gogo(3922, currentUser.UsersTableID);
                gogo(3924, currentUser.UsersTableID);
                gogo(3926, currentUser.UsersTableID);
            }
            List<UsersTable> UsersList2 = (from a in kPiDataContext.UsersTable
                                          join b in kPiDataContext.BasicParametrsAndUsersMapping
                                              on a.UsersTableID equals b.FK_UsersTable
                                          where a.Active == true
                                          && b.Active == true
                                          && b.CanEdit == true
                                          && b.FK_ParametrsTable == 3605
                                          select a).Distinct().ToList();
            foreach (UsersTable currentUser in UsersList2)
            {
                gogo2(3912, currentUser.UsersTableID);
                gogo2(3914, currentUser.UsersTableID);
                gogo2(3916, currentUser.UsersTableID);
                gogo2(3918, currentUser.UsersTableID);
                gogo2(3920, currentUser.UsersTableID);
                gogo2(3922, currentUser.UsersTableID);
                gogo2(3924, currentUser.UsersTableID);
                gogo2(3926, currentUser.UsersTableID);
                
            }
            List<UsersTable> UsersList3 = (from a in kPiDataContext.UsersTable
                                           join b in kPiDataContext.BasicParametrsAndUsersMapping
                                               on a.UsersTableID equals b.FK_UsersTable
                                           where a.Active == true
                                           && b.Active == true
                                           && b.CanConfirm == true
                                           && b.FK_ParametrsTable == 3605
                                           select a).Distinct().ToList();
            foreach (UsersTable currentUser in UsersList3)
            {
                gogo3(3912, currentUser.UsersTableID);
                gogo3(3914, currentUser.UsersTableID);
                gogo3(3916, currentUser.UsersTableID);
                gogo3(3918, currentUser.UsersTableID);
                gogo3(3920, currentUser.UsersTableID);
                gogo3(3922, currentUser.UsersTableID);
                gogo3(3924, currentUser.UsersTableID);
                gogo3(3926, currentUser.UsersTableID);              
            }
        }

        public  class
            newclass
        {
            public string nameofacadmy { get; set; }
            public string nameofIndicator { get; set; }
            public float value { get; set; }
            public float planned { get; set; }
            public string measure { get; set; }
            }

        protected void Button5_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<FirstLevelSubdivisionTable> firstList = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                                          where a.Active == true
                                                          select a).Distinct().ToList();
            List<IndicatorsTable> IndicatorsList = (from a in kPiDataContext.IndicatorsTable 
                                                        where a.Active == true
                                                        select a).ToList();
            List < newclass > classlist= new List<newclass>();
            foreach (FirstLevelSubdivisionTable currentFirst in firstList)
            {
                foreach (IndicatorsTable CurrentIndicator in IndicatorsList)
                {
                    if ((CurrentIndicator.IndicatorsTableID!=1026)&&(CurrentIndicator.IndicatorsTableID!=1027)&&(CurrentIndicator.IndicatorsTableID!=1028))
                    {
                        continue;
                    }
                    CollectedIndicatorsForR curcollected = (from a in kPiDataContext.CollectedIndicatorsForR
                                                            where a.Active == true
                                                            && a.FK_FirstLevelSubdivisionTable == currentFirst.FirstLevelSubdivisionTableID
                                                            && a.FK_SecondLevelSubdivisionTable == null
                                                            && a.FK_ReportArchiveTable == 1
                                                            && a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                                            select a).OrderByDescending(mc => mc.CreatedDateTime).FirstOrDefault();
                    newclass newone_ = new newclass();
                    newone_.nameofacadmy = currentFirst.Name;
                    newone_.nameofIndicator = CurrentIndicator.Name;
                    newone_.measure = CurrentIndicator.Measure;
                    PlannedIndicator planned = (from a in kPiDataContext.PlannedIndicator
                                       where a.Active == true
                                       && a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID

                                       select a).OrderBy(mc => mc.Date).FirstOrDefault();
                    if (planned != null)
                        newone_.planned = (float) planned.Value;

                    if (curcollected !=null)
                    {
                        if (curcollected.Value!=null)
                        {
                            newone_.value = (float) curcollected.Value;
                            classlist.Add(newone_);
                        }
                    }
                }
            }

            GridView1.DataSource = classlist;
            GridView1.DataBind();
        }
        

    }
}


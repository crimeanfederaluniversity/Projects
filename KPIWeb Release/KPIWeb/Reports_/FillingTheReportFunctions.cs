using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb.Reports_
{
    public class FillingTheReportFunctions
    {
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
                                  ((c.AbbreviationEN == "a_Och_In_M" && SpecType == 3)
                                      || (c.AbbreviationEN == "a_Och_In_B" && SpecType == 1)
                                      || (c.AbbreviationEN == "a_Och_In_S" && SpecType == 2)
                                      || (c.AbbreviationEN == "a_Och_In_A" && SpecType == 4))

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
                                  ((c.AbbreviationEN == "c_Z_A_Kom" && SpecType == 4)
                                  || (c.AbbreviationEN == "c_Z_B_Kom" && SpecType == 1)
                                  || (c.AbbreviationEN == "c_Z_S_Kom" && SpecType == 2)
                                  || (c.AbbreviationEN == "c_Z_M_Kom" && SpecType == 3))
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
                                  ((c.AbbreviationEN == "b_OchZ_S_Kom" && SpecType == 2)
                                  || (c.AbbreviationEN == "b_OchZ_M_Kom" && SpecType == 3)
                                  || (c.AbbreviationEN == "b_OchZ_A_Kom" && SpecType == 4)
                                  || (c.AbbreviationEN == "b_OchZ_B_Kom" && SpecType == 1))
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
                        pattern7(FourthLevel.FK_Specialization, 0, ReportArchiveID, FourthLevel, 1);
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
        public void CalcCalculate(int ReportArchiveID, UsersTable user)
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
    }
}
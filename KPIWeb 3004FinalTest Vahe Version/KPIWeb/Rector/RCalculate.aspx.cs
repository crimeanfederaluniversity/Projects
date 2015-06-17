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


#region
         #region patterns
        protected double pattern1(UsersTable user, int ReportArchiveID, int spectype_, string basicAbb,string basicAbb2)
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
                         && ((z.AbbreviationEN == basicAbb)||((basicAbb2 != null) && (z.AbbreviationEN == basicAbb2)))
                         && a.FK_ReportArchiveTable == ReportArchiveID
                         && b.SpecType == spectype_
                         && a.Active == true
                         && d.Active == true
                         && (e.FK_FieldOfExpertise == 10 || e.FK_FieldOfExpertise == 11 || e.FK_FieldOfExpertise == 12)
                         select a.CollectedValue).Sum());
        }
        protected double pattern2(UsersTable user, int ReportArchiveID, string basicAbb)
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
        protected double pattern3(UsersTable user, int ReportArchiveID, int SpecType)
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
        protected double pattern4(UsersTable user, int ReportArchiveID, int SpecType)
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
        #endregion
       
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
                foreach (BasicParametersTable basicParam in calcBasicParams) //пройдемся по показателям
                {
                    double tmp = 1000000000001;

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
                    if (basicParam.AbbreviationEN == "a_Och_M_CO_R") tmp = pattern1(user, ReportArchiveID, 3, "a_Och_M_C", null);
                    if (basicParam.AbbreviationEN == "b_OchZ_M_CO_R") tmp = pattern1(user, ReportArchiveID, 3, "b_OchZ_M_C", null);
                    if (basicParam.AbbreviationEN == "c_Z_M_CO_R") tmp = pattern1(user, ReportArchiveID, 3, "c_Z_M_C", null);
                    if (basicParam.AbbreviationEN == "d_E_M_CO_R") tmp = pattern1(user, ReportArchiveID, 3, "d_E_M_C", null);

                    if (basicParam.AbbreviationEN == "a_Och_B_CO_R") 
                        tmp = pattern1(user, ReportArchiveID, 1, "a_Och_B_C", null);
                    if (basicParam.AbbreviationEN == "d_E_B_CO_R") tmp = pattern1(user, ReportArchiveID, 1, "d_E_B_C", null);
                    if (basicParam.AbbreviationEN == "c_Z_B_CO_R") tmp = pattern1(user, ReportArchiveID, 1, "c_Z_B_C", null);
                    if (basicParam.AbbreviationEN == "d_E_B_CO_R") tmp = pattern1(user, ReportArchiveID, 1, "d_E_B_C", null);

                    if (basicParam.AbbreviationEN == "a_Och_S_CO_R") tmp = pattern1(user, ReportArchiveID, 2, "a_Och_S_C", null);
                    if (basicParam.AbbreviationEN == "b_OchZ_S_CO_R") tmp = pattern1(user, ReportArchiveID, 2, "b_OchZ_S_C", null);
                    if (basicParam.AbbreviationEN == "c_Z_S_CO_R") tmp = pattern1(user, ReportArchiveID, 2, "c_Z_S_C", null);
                    if (basicParam.AbbreviationEN == "d_E_S_CO_R") tmp = pattern1(user, ReportArchiveID, 2, "d_E_S_C", null);
                    //новые показатели                 

                    if (basicParam.AbbreviationEN == "Kol_Kaf_R")
                        tmp = Convert.ToDouble((from a in kpiWebDataContext.ThirdLevelParametrs
                                                where a.ThirdLevelParametrsID == user.FK_ThirdLevelSubdivisionTable
                                                select a.IsBasic).FirstOrDefault());

                    if (tmp < 1000000000000)
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
                        if (collectedBasicTmp == null) // надо создать
                        {
                            collectedBasicTmp = new CollectedBasicParametersTable();
                            collectedBasicTmp.Active = true;
                            collectedBasicTmp.Status = 0;
                            collectedBasicTmp.FK_UsersTable = user.UsersTableID;
                            collectedBasicTmp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                            collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                            collectedBasicTmp.CollectedValue = tmp;
                           // collectedBasicTmp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                            collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                            collectedBasicTmp.SavedDateTime = DateTime.Now;
                            collectedBasicTmp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                            collectedBasicTmp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                            collectedBasicTmp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                            collectedBasicTmp.FK_ThirdLevelSubdivisionTable = user.FK_ThirdLevelSubdivisionTable;

                            kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                            kpiWebDataContext.SubmitChanges();
                        }
                        else
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

                            collectedBasicTmp.CollectedValue = tmp;
                            kpiWebDataContext.SubmitChanges();
                        }
                    }
                }
            }
        }
#endregion


        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<ThirdLevelSubdivisionTable> ThirdLevelList = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                                               join b in kPiDataContext.CollectedBasicParametersTable
                                                                   on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                               where b.FK_BasicParametersTable == 3637
                                                               && b.FK_ReportArchiveTable == 1
                                                               select a).Distinct().ToList();

/*          UsersTable UUUUUSER = (from a in kPiDataContext.UsersTable

                                   where a.UsersTableID == 12489
                                           select a).FirstOrDefault();
            CalcCalculate(1, UUUUUSER);
*/
            foreach (ThirdLevelSubdivisionTable thirdLevel in ThirdLevelList)
            {
                UsersTable UsersToCalculate = (from a in kPiDataContext.UsersTable
                                                 join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                     on a.UsersTableID equals b.FK_UsersTable
                                                 where a.FK_ThirdLevelSubdivisionTable == thirdLevel.ThirdLevelSubdivisionTableID
                                                         && b.CanEdit == true select a).FirstOrDefault();
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
        
    }
}


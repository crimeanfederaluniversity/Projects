using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net.Layout;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.Head
{
    public partial class HeadShowStructure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentReport = 4009;

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int allZeroLevel = 0;
            int insertZeroLevel =0;
            int confirmZeroLevel = 0;
            #region get zero leve list
            List<ZeroLevelSubdivisionTable> zeroLevelList = (from a in kPiDataContext.ZeroLevelSubdivisionTable 
                                                             where a.Active==true 
                                                             select a).ToList();
            #endregion
            foreach (ZeroLevelSubdivisionTable zeroLevelItem in zeroLevelList)//по каждому университету
            {
                int allFirstLevel = 0;
                int insertFirstLevel = 0;
                int confirmFirstLevel = 0;
                #region get first level list
                List<FirstLevelSubdivisionTable> firstLevelList = (from b in kPiDataContext.FirstLevelSubdivisionTable
                    join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                        on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId
                    where b.FK_ZeroLevelSubvisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                          && c.FK_ReportArchiveTableId == currentReport
                          && b.Active == true
                          && c.Active ==true
                    select b).ToList();
                #endregion
                ListBox1.Items.Add("__" + zeroLevelItem.Name);
                foreach (FirstLevelSubdivisionTable firstLevelItem in firstLevelList)//по каждой академии
                {
                    int allSecondLevel = 0;
                    int insertSecondLevel = 0;
                    int confirmSecondLevel = 0;
                    #region get second level list
                    List<SecondLevelSubdivisionTable> secondLevelList =
                        (from d in kPiDataContext.SecondLevelSubdivisionTable
                            where d.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                            && d.Active == true
                            select d).ToList();
                    #endregion
                    ListBox1.Items.Add("____" + firstLevelItem.Name);
                    foreach (SecondLevelSubdivisionTable secondLevelItem in secondLevelList)//по каждому факультету
                    {
                        #region counting level basics
                        int insertThirdLevel = (from a in kPiDataContext.CollectedBasicParametersTable
                                                 join b in kPiDataContext.BasicParametrAdditional
                                                 on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                 where b.Calculated == false
                                                 && b.SubvisionLevel == 2
                                                 && a.FK_ReportArchiveTable == currentReport
                                                 && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                                 && a.CollectedValue != null
                                                 select a).ToList().Count();

                        int confirmThirdLevel = (from a in kPiDataContext.CollectedBasicParametersTable
                                                  join b in kPiDataContext.BasicParametrAdditional
                                                  on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                  where
                                                  b.Calculated == false
                                                  && b.SubvisionLevel == 2
                                                  && a.FK_ReportArchiveTable == currentReport
                                                  && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                                  && a.Status == 4 // данные верифицированы первым уровнем
                                                  select a).ToList().Count();

                        //status=0 данных нет 
                        //status=1 данные вернули на доработку
                        //status=2 данные есть
                        //status=3 данные отправлены на верификацию
                        //status=4 данные верифицированы первым первым уровнем(кафедрой)
                        //status=5 
                        //status=6
                        

                        int allThirdLevel = (from m4 in kPiDataContext.BasicParametersTable
                                              join s4 in kPiDataContext.BasicParametrAdditional
                                                  on m4.BasicParametersTableID equals s4.BasicParametrAdditionalID
                                              join n4 in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                  on m4.BasicParametersTableID equals n4.FK_BasicParametrsTable
                                              join o4 in kPiDataContext.BasicParametrsAndUsersMapping
                                                  on m4.BasicParametersTableID equals o4.FK_ParametrsTable
                                              join p4 in kPiDataContext.UsersTable
                                                  on o4.FK_UsersTable equals p4.UsersTableID
                                              where
                                                  s4.SubvisionLevel == 2
                                                  && s4.Calculated == false
                                                  && n4.FK_ReportArchiveTable == currentReport
                                                  && o4.CanEdit
                                                  && p4.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                              select m4).ToList().Count();
                        #endregion
                        ListBox1.Items.Add("______" + secondLevelItem.Name + " " + allThirdLevel + " " + insertThirdLevel + " " + confirmThirdLevel);
                        #region get third level list
                        List<ThirdLevelSubdivisionTable> thirdLevelList =
                            (from f in kPiDataContext.ThirdLevelSubdivisionTable
                                where f.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                && f.Active==true
                                select f).ToList();
                        #endregion                     
                        foreach (ThirdLevelSubdivisionTable thirdLevelItem in thirdLevelList)//по кафедре
                        {
                            #region counting level basics
                            int insertFourthLevel = (from a in kPiDataContext.CollectedBasicParametersTable
                                                    join b in kPiDataContext.BasicParametrAdditional
                                                    on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                    where  b.Calculated == false
                                                    && b.SubvisionLevel == 3
                                                    && a.FK_ReportArchiveTable == currentReport
                                                    && a.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                                    && a.CollectedValue != null
                                                    select a).ToList().Count();

                            int confirmFourthLevel = (from a in kPiDataContext.CollectedBasicParametersTable
                                                     join b in kPiDataContext.BasicParametrAdditional
                                                     on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                     where
                                                     b.Calculated == false
                                                     && b.SubvisionLevel == 3
                                                     && a.FK_ReportArchiveTable == currentReport
                                                     && a.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                                     && a.Status == 4 // верифицированы первым уровнем
                                                     select a).ToList().Count();

                            int allFourthLevel = (from m4 in kPiDataContext.BasicParametersTable
                                                 join s4 in kPiDataContext.BasicParametrAdditional
                                                     on m4.BasicParametersTableID equals s4.BasicParametrAdditionalID
                                                 join n4 in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                     on m4.BasicParametersTableID equals n4.FK_BasicParametrsTable
                                                 join o4 in kPiDataContext.BasicParametrsAndUsersMapping
                                                     on m4.BasicParametersTableID equals o4.FK_ParametrsTable
                                                 join p4 in kPiDataContext.UsersTable
                                                     on o4.FK_UsersTable equals p4.UsersTableID
                                                 where
                                                     s4.SubvisionLevel == 3
                                                     && s4.Calculated == false
                                                     && n4.FK_ReportArchiveTable == currentReport
                                                     && o4.CanEdit
                                                     && p4.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID                                      
                                                 select m4).ToList().Count();
                            #endregion
                            ListBox1.Items.Add("________" + thirdLevelItem.Name + " " + allFourthLevel + " " + insertFourthLevel + " " + confirmFourthLevel);
                            #region get fourth level list

                            bool canGrad = (from g3 in kPiDataContext.ThirdLevelParametrs
                                where g3.ThirdLevelParametrsID == thirdLevelItem.ThirdLevelSubdivisionTableID
                                select g3.CanGraduate).FirstOrDefault();
                            List<FourthLevelSubdivisionTable> fourthLevelList = new List<FourthLevelSubdivisionTable>();
                            if (canGrad)
                            {
                                 fourthLevelList = (from g in kPiDataContext.FourthLevelSubdivisionTable
                                                    where
                                                    g.FK_ThirdLevelSubdivisionTable ==
                                                    thirdLevelItem.ThirdLevelSubdivisionTableID
                                                    && g.Active == true
                                                    select g).ToList();
                            }
                            
                            #endregion                   
                            foreach (FourthLevelSubdivisionTable fourthLevelItem in fourthLevelList)//по специальности
                            {
                                FourthLevelParametrs forthParam =
                                    (from f5 in kPiDataContext.FourthLevelParametrs
                                        where f5.FourthLevelParametrsID == fourthLevelItem.FourthLevelSubdivisionTableID
                                        select f5).FirstOrDefault();
                                #region counting level basics
                                int insertFifthLevel = (from a in kPiDataContext.CollectedBasicParametersTable
                                                 join b in kPiDataContext.BasicParametrAdditional
                                                 on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                 where 
                                                    b.Calculated == false
                                                 && a.FK_ReportArchiveTable == currentReport
                                                 && a.FK_FourthLevelSubdivisionTable == fourthLevelItem.FourthLevelSubdivisionTableID
                                                 && a.CollectedValue != null                                               
                                                 select a).ToList().Count();

                                int confirmFifthLevel = (from a in kPiDataContext.CollectedBasicParametersTable
                                                        join b in kPiDataContext.BasicParametrAdditional
                                                        on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                        where
                                                           b.Calculated == false
                                                        && a.FK_ReportArchiveTable == currentReport
                                                        && a.FK_FourthLevelSubdivisionTable == fourthLevelItem.FourthLevelSubdivisionTableID
                                                        && a.Status == 4
                                                        select a).ToList().Count();

                                int allFifthLevel = (from m4 in kPiDataContext.BasicParametersTable
                                    join s4 in kPiDataContext.BasicParametrAdditional
                                        on m4.BasicParametersTableID equals s4.BasicParametrAdditionalID
                                    join n4 in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                        on m4.BasicParametersTableID equals n4.FK_BasicParametrsTable
                                    join o4 in kPiDataContext.BasicParametrsAndUsersMapping
                                        on m4.BasicParametersTableID equals o4.FK_ParametrsTable
                                    join p4 in kPiDataContext.UsersTable
                                        on o4.FK_UsersTable equals p4.UsersTableID
                                    join q4 in kPiDataContext.ThirdLevelParametrs
                                        on p4.FK_ThirdLevelSubdivisionTable equals q4.ThirdLevelParametrsID
                                    where
                                        s4.SubvisionLevel == 4
                                        && s4.Calculated == false
                                        && n4.FK_ReportArchiveTable == currentReport
                                        && s4.SpecType == forthParam.SpecType                                
                                        && o4.CanEdit
                                        && p4.FK_ZeroLevelSubdivisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                                        && p4.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                        && p4.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                        && p4.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                        &&
                                        ((forthParam.IsForeignStudentsAccept == true) ||
                                         (forthParam.IsForeignStudentsAccept == s4.ForForeignStudents))
                                    select m4).ToList().Count();
                                #endregion
                                ListBox1.Items.Add("__________" + fourthLevelItem.Name+" "+allFifthLevel + " " + insertFifthLevel + " " + confirmFifthLevel);
                            }
                        }
                    }
                }
            }
        }
    }
}
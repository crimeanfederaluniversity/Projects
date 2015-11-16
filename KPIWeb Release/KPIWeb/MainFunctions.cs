using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using iTextSharp.xmp.impl;

namespace KPIWeb
{
    public class MainFunctions
    {
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();
        public UsersTable GetUserById(int userId)
        {
            
            UsersTable userTable =
            (from a in _kPiDataContext.UsersTable where a.UsersTableID == userId select a).FirstOrDefault();
            return userTable;
        }
        public ReportArchiveTable GetReportById(int reportId)
        {
            ReportArchiveTable reportArchive = (from a in _kPiDataContext.ReportArchiveTable
                where a.ReportArchiveTableID == reportId
                select a).FirstOrDefault();
            return reportArchive;
        }
        public FirstLevelSubdivisionTable GetFirstLevelById(int firstLevelId)
        {
            FirstLevelSubdivisionTable firstLevelTable = (from a in _kPiDataContext.FirstLevelSubdivisionTable
                                                where a.FirstLevelSubdivisionTableID == firstLevelId
                                                select a).FirstOrDefault();
            return firstLevelTable;
        }
        public SecondLevelSubdivisionTable GetSecondLevelById(int secondLevelId)
        {
            SecondLevelSubdivisionTable secondLevelTable = (from a in _kPiDataContext.SecondLevelSubdivisionTable
                                                           where a.SecondLevelSubdivisionTableID == secondLevelId
                                                          select a).FirstOrDefault();
            return secondLevelTable;
        }
        public ThirdLevelSubdivisionTable GetThirdLevelById(int thirdLevel)
        {
            ThirdLevelSubdivisionTable thirdLevelTable = (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                                                           where a.ThirdLevelSubdivisionTableID == thirdLevel
                                                          select a).FirstOrDefault();
            return thirdLevelTable;
        }
        public bool CanUserEditAnyInReport(int userId, int reportId)
        {
            int count = (from a in _kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                where a.Active == true
                join b in _kPiDataContext.BasicParametrsAndUsersMapping
                    on a.FK_BasicParametrsTable equals b.FK_ParametrsTable
                where b.Active == true
                      && b.CanEdit == true
                      && a.FK_ReportArchiveTable == reportId
                      && b.FK_UsersTable == userId
                select a).Distinct().Count();
            return count > 0 ? true : false;
        }
        public string GetUserIp()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip =>
                                ip.AddressFamily ==
                                System.Net.Sockets.AddressFamily.InterNetwork)
                        .Select(ip => ip.ToString())
                        .FirstOrDefault() ?? "";
        }
        private int FindSubdivisionParentId(int currendSubdivisionLevel, int? currentSubdivisionId)
        {
            if (currentSubdivisionId == null)
            {
                return 0;
            }
            switch (currendSubdivisionLevel)
            {
                case 0:
                {
                    return 0;
                }
                case 1:
                {
                    FirstLevelSubdivisionTable tmpZero = (from a in _kPiDataContext.FirstLevelSubdivisionTable
                        where a.FirstLevelSubdivisionTableID == currentSubdivisionId
                        select a).FirstOrDefault();
                    if (tmpZero != null)
                        return (int)tmpZero.FK_ZeroLevelSubvisionTable;
                    return 0;
                }
                case 2:
                {
                    SecondLevelSubdivisionTable tmpFirst = (from a in _kPiDataContext.SecondLevelSubdivisionTable
                                                          where a.SecondLevelSubdivisionTableID == currentSubdivisionId
                                                          select a).FirstOrDefault();
                    if (tmpFirst != null)
                        return (int)tmpFirst.FK_FirstLevelSubdivisionTable;
                    return 0;
                }
                case 3:
                {
                    ThirdLevelSubdivisionTable tmpSecond = (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                                                            where a.ThirdLevelSubdivisionTableID == currentSubdivisionId
                                                            select a).FirstOrDefault();
                    if (tmpSecond != null)
                        return (int)tmpSecond.FK_SecondLevelSubdivisionTable;
                    return 0;
                }
                case 4:
                {
                    FourthLevelSubdivisionTable tmpThird = (from a in _kPiDataContext.FourthLevelSubdivisionTable
                                                            where a.FourthLevelSubdivisionTableID == currentSubdivisionId
                                                            select a).FirstOrDefault();
                    if (tmpThird != null)
                        return (int)tmpThird.FK_ThirdLevelSubdivisionTable;
                    return 0;
                }
                case 5:
                {
                    FifthLevelSubdivisionTable tmpFourth = (from a in _kPiDataContext.FifthLevelSubdivisionTable
                                                            where a.FifthLevelSubdivisionTableID == currentSubdivisionId
                                                             select a).FirstOrDefault();
                    if (tmpFourth != null)
                        return (int)tmpFourth.FK_FourthLevelSubdivisionTable;
                    return 0;
                }
                default:
                {
                    return 0;
                }
            }
        }
        public int?[] GetSubdivisonIds(int subdivisionLevel, int levelId)
        {
            int?[] subdivisionIds = new int?[6];
            subdivisionIds[subdivisionLevel] = levelId;
            for (int i = 4; i >= 0; i--)
            {
                int tmp = FindSubdivisionParentId(i + 1, subdivisionIds[i + 1]);
                if (tmp != 0)
                    subdivisionIds[i] = tmp;
            }
            return subdivisionIds;
        }      
        public double? ClearDouble(double value)
        {
            if ((double.IsInfinity(value)) || (double.IsNaN(value))
                || (double.IsNegativeInfinity(value)) || (double.IsPositiveInfinity(value)))
            {
                return null;
            }
            else
            {
                return value;
            }
        }
        public int IntNullTo0(int? valueToChect)
        {
            if (valueToChect == null)
                return 0;
            return (int) valueToChect;
        }
        public string GetCommentForBasicInReport(int basicParametrId, int reportId)
        {
            CommetntForBasicInReport currentComment = (from a in _kPiDataContext.CommetntForBasicInReport
                where a.FK_BasickParamets == basicParametrId
                      && a.FK_Report == reportId
                      && a.Active == true
                select a).FirstOrDefault();
            if (currentComment == null)
                return "Без комментария";
            if (currentComment.Comment == null)
                return "Без комментария";
            if (currentComment.Comment.Length < 3)
                return "Без комментария";
            return currentComment.Comment;
        }
        public List<ThirdLevelSubdivisionTable> GetThirdLevelListBySecondId(int secondLevelId)
        {
            List<ThirdLevelSubdivisionTable> tmpThirdLevelList = (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                where a.Active == true
                      && a.FK_SecondLevelSubdivisionTable == secondLevelId
                select a).Distinct().ToList();
            return tmpThirdLevelList;
        }
        public List<ThirdLevelSubdivisionTable> GetThirdLevelListByFirstId(int firstLevelId)
        {
            List<ThirdLevelSubdivisionTable> tmpThirdLevelList = (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                                                                  where a.Active == true
                                                                  join b in _kPiDataContext.SecondLevelSubdivisionTable
                                                                  on a.FK_SecondLevelSubdivisionTable equals b.SecondLevelSubdivisionTableID
                                                                  where b.Active == true
                                                                        && b.FK_FirstLevelSubdivisionTable == firstLevelId
                                                                  select a).Distinct().ToList();
            return tmpThirdLevelList;
        }
        public List<ThirdLevelSubdivisionTable> GetThirdLevelListByZeroId(int zeroLevelId)
        {
            List<ThirdLevelSubdivisionTable> tmpThirdLevelList = (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                                                                  where a.Active == true
                                                                  join b in _kPiDataContext.SecondLevelSubdivisionTable
                                                                  on a.FK_SecondLevelSubdivisionTable equals b.SecondLevelSubdivisionTableID
                                                                  where b.Active == true
                                                                  join c in _kPiDataContext.FirstLevelSubdivisionTable
                                                                  on b.FK_FirstLevelSubdivisionTable equals c.FirstLevelSubdivisionTableID
                                                                  where c.Active == true
                                                                  && c.FK_ZeroLevelSubvisionTable == zeroLevelId
                                                                  select a).Distinct().ToList();
            return tmpThirdLevelList;
        }
        public List<ThirdLevelSubdivisionTable> GetAllThirdLevelsByLevelAndId(int subdivisionLevel, int subdivisionId)
        {
            
            List<ThirdLevelSubdivisionTable> thirdLevelList = new List<ThirdLevelSubdivisionTable>();
            if (subdivisionLevel == 3)
            {
                thirdLevelList.Add(GetThirdLevelById(subdivisionId));
            }
            else if (subdivisionLevel == 2)
            {
                thirdLevelList = GetThirdLevelListBySecondId(subdivisionId);
            }
            else if (subdivisionLevel == 1)
            {
                thirdLevelList = GetThirdLevelListByFirstId(subdivisionId);
            }
            else if (subdivisionLevel == 0)
            {
                thirdLevelList = GetThirdLevelListByZeroId(subdivisionId);
            }
            return thirdLevelList;
        }
        public List<FourthLevelSubdivisionTable> GetFourhLevelsByThirdId(int thirdLevevlId)
        {
            List<FourthLevelSubdivisionTable> fourthLevelList = (from a in _kPiDataContext.FourthLevelSubdivisionTable
                where a.FK_ThirdLevelSubdivisionTable == thirdLevevlId
                      && a.Active == true
                select a).Distinct().ToList();
            return fourthLevelList;
        }

        public List<IndicatorsTable> GetIndicatorsInReport(int reportId)
        {
            List<IndicatorsTable> newIndicatorsTable = (from a in _kPiDataContext.IndicatorsTable
                where a.Active == true
                join b in _kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                    on a.IndicatorsTableID equals b.FK_IndicatorsTable
                where b.Active == true
                      && b.FK_ReportArchiveTable == reportId
                select a).Distinct().ToList();
            return newIndicatorsTable;
        }
        public List<CalculatedParametrs> GetCalculatedParametrsInReport(int reportId)
        {
            return (from a in _kPiDataContext.CalculatedParametrs
                        where a.Active == true 
                        join b in _kPiDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                        on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                        where b.Active == true
                        && b.FK_ReportArchiveTable == reportId
                        select  a).Distinct().ToList();
        }
        public string GetResponsibleProrectorPositionForIndicator (int indicatorId)
        {
            UsersTable responsibleProrector = (from a in _kPiDataContext.UsersTable
                where a.Active == true
                      && a.AccessLevel == 5
                join b in _kPiDataContext.IndicatorsAndUsersMapping
                    on a.UsersTableID equals b.FK_UsresTable
                where b.Active == true
                      && b.CanConfirm == true
                      && b.FK_IndicatorsTable == indicatorId
                      && a.UsersTableID != 12769
                      && a.UsersTableID != 12803
                      && a.UsersTableID != 12806
                select a).FirstOrDefault();
            if (responsibleProrector == null)
            {
                return "Ответственный проректор не назначен";
            }
            else
            {
                return responsibleProrector.Position;
            }
        }
        public string GetResponsibleProrectorPositionForCalculated(int calculatedId)
        {
            UsersTable responsibleProrector = (from a in _kPiDataContext.UsersTable
                                               where a.Active == true
                                                     && a.AccessLevel == 5
                                               join b in _kPiDataContext.CalculatedParametrsAndUsersMapping
                                                   on a.UsersTableID equals b.FK_UsersTable
                                               where b.Active == true
                                                     && b.CanConfirm == true
                                                     && b.FK_CalculatedParametrsTable == calculatedId
                                                     && a.UsersTableID != 12769
                                                     && a.UsersTableID != 12803
                                                     && a.UsersTableID != 12806
                                               select a).FirstOrDefault();
            if (responsibleProrector == null)
            {
                return "Ответственный проректор не назначен";
            }
            else
            {
                return responsibleProrector.Position;
            }
        }
        public CollectedIndocators GetCollectedIndicatorInReport(int indicatorsId, int reportId)
        {
            return (from a in _kPiDataContext.CollectedIndocators
                where a.Active == true
                      && a.FK_ReportArchiveTable == reportId
                      && a.FK_Indicators == indicatorsId
                select a).FirstOrDefault();
        }
        public CollectedCalculatedParametrs GetCollectedCalculatedInRepost(int calculatedId, int reportId)
        {
            return (from a in _kPiDataContext.CollectedCalculatedParametrs
                    where a.Active == true
                          && a.FK_ReportArchiveTable == reportId
                          && a.FK_CalculatedParametrs == calculatedId
                    select a).FirstOrDefault();
        }
        public ConfirmationHistory GetConfirmationHistoryLine(int indicatorId, int calculatedId, int reportId)
        {
            return (from a in _kPiDataContext.ConfirmationHistory
                where (a.FK_CalculatedParamTable == calculatedId || calculatedId == 0)
                      && (a.FK_IndicatorsTable == indicatorId || indicatorId == 0)
                      && a.FK_ReportTable == reportId
                select a).Distinct().FirstOrDefault();
        }
    }
    public class RangeValidatorFunctions
    {
        public double GetMinValueForDataType(int dataType)
        {
            switch (dataType)
            {
                case 0:
                {
                    return 0;
                }
                case 1:
                {
                    return 0;
                }
                case 2:
                {
                    return 0;
                }
                default:
                {
                    return 0;
                }
            }
        }
        public double GetMaxValueForDataType(int dataType)
        {
            switch (dataType)
            {
                case 0:
                    {
                        return 1;
                    }
                case 1:
                    {
                        return 1000000000;
                    }
                case 2:
                    {
                        return 100000000000000;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        public ValidationDataType GetValidateTypeForDataType(int dataType)
        {
            switch (dataType)
            {
                case 0:
                    {
                        return ValidationDataType.Integer;
                    }
                case 1:
                    {
                        return ValidationDataType.Integer;
                    }
                case 2:
                    {
                        return ValidationDataType.Double;
                    }
                default:
                    {
                        return ValidationDataType.String;
                    }
            }
        }
        public string GetValidateErrorTextForDataType(int dataType)
        {
            switch (dataType)
            {
                case 0:
                {
                    return "Только 0 или 1";
                }
                case 1:
                {
                    return "Только целочисленное значение";
                }
                case 2:
                {
                    return "Только цифры и запятая";
                }
                default:
                {
                    return "Ошибка";
                }
            }
        }

        public bool GetValidateEnabledForDataType(int dataType)
        {
            switch (dataType)
            {
                case 0:
                {
                    return true;
                }
                case 1:
                    {
                        return true;
                    }
                case 2:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }
    public class CollectedDataStatusProcess
    {
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();
        private string GetStatusNameByCollectedCount(int sum,int st0,int st1,int st2, int st3,int st4,int st5,int st6)
        {
            int allStSum = st0 + st1 + st2 + st3 + st4 + st5 + st6;
            if (sum != allStSum)
            {
                return "Данные частично внесены";
            }

            if (sum == 0)
            {
                return "Данные не вносились";
            }
            if (sum == st0)
            {
                return "Данные внесены";
            }
            if (sum == st1)
            {
                return "Данные возвращены на доработку";
            }
            if (sum == st2)
            {
                return "Данные внесены";
            }
            if (sum == st3)
            {
                return "Данные ожидают отправки";
            }
            if (sum == st4)
            {
                return "Данные ожидают отправки";
            }
            if (sum == st5)
            {
                return "Данные отправлены";
            }
            if (sum == st6)
            {
                return "";
            }

            if (st5 > 0)
            {
                return "Данные частично отправлены";
            }
            if (st4 > 0)
            {
                return "Данные частично отправлены";
            }
           
            return "Данные частично внесены";
        }
        public int[] GetStatusIdsCountForStructInReport(int l0, int l1, int l2, int l3, int l4, int l5, int reportId, int userId, bool withCalculated)
        {
            CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
            List<CollectedBasicParametersTable> collectedList = collectedDataProcess.GetListOfCollectedDataByParams(l0, l1, l2, l3, l4, l5, reportId, userId, withCalculated);
                     
            int collectedWithStatus0 = (from a in collectedList where (a.Status == 0) &&(a.CollectedValue.ToString().Any())  select a).Count();  //status=0 данных нет 
            int collectedWithStatus1 = (from a in collectedList where (a.Status == 1) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=1 данные вернули на доработку
            int collectedWithStatus2 = (from a in collectedList where (a.Status == 2) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=2 данные есть
            int collectedWithStatus3 = (from a in collectedList where (a.Status == 3) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=3 данные отправлены на верификацию
            int collectedWithStatus4 = (from a in collectedList where (a.Status == 4) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=4 данные верифицированы первым первым уровнем(кафедрой)
            int collectedWithStatus5 = (from a in collectedList where (a.Status == 5) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=5 данные утверждены директором академии
            int collectedWithStatus6 = (from a in collectedList where (a.Status == 6) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=6 запас

            return new int[]
            {   
                collectedList.Count(),
                collectedWithStatus0, collectedWithStatus1, collectedWithStatus2, collectedWithStatus3,
                collectedWithStatus4, collectedWithStatus5, collectedWithStatus6
            };

        }
        public string GetStatusNameForStructInReportByStructIdNLevel(int structId, int structLevel, int reportId,
            int userId,bool withCalculated)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(structLevel, structId);
            int [] collectedWithStatusCnt = GetStatusIdsCountForStructInReport(mainFunctions.IntNullTo0(subdivisionIds[0]),
                mainFunctions.IntNullTo0(subdivisionIds[1]),
                mainFunctions.IntNullTo0(subdivisionIds[2]),
                mainFunctions.IntNullTo0(subdivisionIds[3]),
                mainFunctions.IntNullTo0(subdivisionIds[4]),
                mainFunctions.IntNullTo0(subdivisionIds[5]),
                reportId, userId, withCalculated);
            
            return GetStatusNameByCollectedCount(collectedWithStatusCnt[0],
                collectedWithStatusCnt[1],collectedWithStatusCnt[2],
                collectedWithStatusCnt[3],collectedWithStatusCnt[4],
                collectedWithStatusCnt[5],collectedWithStatusCnt[6],collectedWithStatusCnt[7]);          
        }
        public bool DoesAllCollectedHaveNeededStatus(int neededStatus, int structLevel, int structId, int reportId,
            int userId,bool withCalculated)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(structLevel, structId);
            int[] collectedWithStatusCnt = GetStatusIdsCountForStructInReport(mainFunctions.IntNullTo0(subdivisionIds[0]),
                mainFunctions.IntNullTo0(subdivisionIds[1]),
                mainFunctions.IntNullTo0(subdivisionIds[2]),
                mainFunctions.IntNullTo0(subdivisionIds[3]),
                mainFunctions.IntNullTo0(subdivisionIds[4]),
                mainFunctions.IntNullTo0(subdivisionIds[5]),
                reportId, userId, withCalculated);
           /* int sum = collectedWithStatusCnt[0] + collectedWithStatusCnt[1] + collectedWithStatusCnt[2] +
                      collectedWithStatusCnt[3] + collectedWithStatusCnt[4] + collectedWithStatusCnt[5] +
                      collectedWithStatusCnt[6];*/
            if (collectedWithStatusCnt[neededStatus + 1] == collectedWithStatusCnt[0])
            {
                return true;
            }
            return false;
        }
        public bool DoesAnyCollectedHaveNeededStatus(int neededStatus, int structLevel, int structId, int reportId,
            int userId, bool withCalculated)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(structLevel, structId);
            int[] collectedWithStatusCnt = GetStatusIdsCountForStructInReport(mainFunctions.IntNullTo0(subdivisionIds[0]),
                mainFunctions.IntNullTo0(subdivisionIds[1]),
                mainFunctions.IntNullTo0(subdivisionIds[2]),
                mainFunctions.IntNullTo0(subdivisionIds[3]),
                mainFunctions.IntNullTo0(subdivisionIds[4]),
                mainFunctions.IntNullTo0(subdivisionIds[5]),
                reportId, userId,withCalculated);           
            if (collectedWithStatusCnt[neededStatus+1] >0)
            {
                return true;
            }
            return false;
        }
        public bool DoesAnyCollectedHaveNullValue(int structLevel, int structId, int reportId,
            int userId, bool withCalculated)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(structLevel, structId);
            int[] collectedWithStatusCnt = GetStatusIdsCountForStructInReport(mainFunctions.IntNullTo0(subdivisionIds[0]),
                mainFunctions.IntNullTo0(subdivisionIds[1]),
                mainFunctions.IntNullTo0(subdivisionIds[2]),
                mainFunctions.IntNullTo0(subdivisionIds[3]),
                mainFunctions.IntNullTo0(subdivisionIds[4]),
                mainFunctions.IntNullTo0(subdivisionIds[5]),
                reportId, userId, withCalculated);
            int sum = collectedWithStatusCnt.Sum() - collectedWithStatusCnt[0];
            if (collectedWithStatusCnt[0] == sum)
            {
                return false;
            }
            return true;
        }
        public string GetStatusNameForStructListInReportByStructIdListnLevel(List<int> structIds, int structLevel,
            int reportId, int userId, bool withCalculated)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int[] collectedWithStatusCount = new int[]{0,0,0,0,0,0,0,0};
            int count = 0;
            foreach (int structId in structIds)
            {
                int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(structLevel, structId);
                int[]  tmpWithStatusCount = GetStatusIdsCountForStructInReport(mainFunctions.IntNullTo0(subdivisionIds[0]),
                   mainFunctions.IntNullTo0(subdivisionIds[1]),
                   mainFunctions.IntNullTo0(subdivisionIds[2]),
                   mainFunctions.IntNullTo0(subdivisionIds[3]),
                   mainFunctions.IntNullTo0(subdivisionIds[4]),
                   mainFunctions.IntNullTo0(subdivisionIds[5]),
                   reportId, userId, withCalculated);
                
                collectedWithStatusCount[0] += tmpWithStatusCount[0];
                collectedWithStatusCount[1] += tmpWithStatusCount[1];
                collectedWithStatusCount[2] += tmpWithStatusCount[2];
                collectedWithStatusCount[3] += tmpWithStatusCount[3];
                collectedWithStatusCount[4] += tmpWithStatusCount[4];
                collectedWithStatusCount[5] += tmpWithStatusCount[5];
                collectedWithStatusCount[6] += tmpWithStatusCount[6];

                
            }
            count = collectedWithStatusCount[0] + collectedWithStatusCount[1] + collectedWithStatusCount[2] +
                    collectedWithStatusCount[3] + collectedWithStatusCount[4] + collectedWithStatusCount[5] +
                    collectedWithStatusCount[6];
            return GetStatusNameByCollectedCount(collectedWithStatusCount[0],
                collectedWithStatusCount[1], collectedWithStatusCount[2],
                collectedWithStatusCount[3], collectedWithStatusCount[4],
                collectedWithStatusCount[5], collectedWithStatusCount[6], collectedWithStatusCount[7]); 
        }
    }
    public class ToGetOnlyNeededStructAutoFilter
    {
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();
        public List<FirstLevelSubdivisionTable>  GetFirstLevelList(int reportId,int userId)
        {
            List<FirstLevelSubdivisionTable> listToReturn = new List<FirstLevelSubdivisionTable>();

            List<FirstLevelSubdivisionTable> firstLevelList =
                   (from a in _kPiDataContext.FirstLevelSubdivisionTable
                    where a.Active == true
                    join b in _kPiDataContext.ReportArchiveAndLevelMappingTable
                        on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubmisionTableId
                    where b.Active == true
                          && b.FK_ReportArchiveTableId == reportId
                    select a).Distinct().ToList();

            List<BasicParametersTable> basicParamsOfUserInReport = (from a in _kPiDataContext.BasicParametersTable
                                                                    where a.Active == true
                                                                    join b in _kPiDataContext.BasicParametrsAndUsersMapping
                                                                        on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                    where b.Active == true
                                                                          && b.FK_UsersTable == userId
                                                                          && b.CanEdit == true
                                                                    join c in _kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                        on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                    where c.Active == true
                                                                          && c.FK_ReportArchiveTable == reportId
                                                                    select a).Distinct().ToList();

            foreach (FirstLevelSubdivisionTable currentFirstLevel in firstLevelList)
            {
                List<ThirdLevelParametrs> thirdLevelsOfThisFourth =
                    (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                     where a.Active == true
                     join b in _kPiDataContext.SecondLevelSubdivisionTable
                         on a.FK_SecondLevelSubdivisionTable equals b.SecondLevelSubdivisionTableID
                     where b.FK_FirstLevelSubdivisionTable == currentFirstLevel.FirstLevelSubdivisionTableID
                     join c in _kPiDataContext.ThirdLevelParametrs
                         on a.ThirdLevelSubdivisionTableID equals c.ThirdLevelParametrsID
                     select c).Distinct().ToList();

                List<int?> FK_ToSubdiviionClasses =
                    (from a in thirdLevelsOfThisFourth select a.FK_SubdivisionClassTable).Distinct().ToList();

                List<BasicParametersTable> basics = new List<BasicParametersTable>();

                foreach (int? currentInt in FK_ToSubdiviionClasses)
                {
                    List<BasicParametersTable> basicParametersOfCurrentInt =
                   (from a in _kPiDataContext.BasicParametersTable
                    where a.Active
                    join b in _kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                        on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                    where b.Active == true
                    where b.FK_SubdivisionClassTable == currentInt
                    select a).Distinct().ToList();

                    foreach (BasicParametersTable currentBasic in basicParametersOfCurrentInt)
                    {
                        basics.Add(currentBasic);
                    }

                }


                if (((from a in basicParamsOfUserInReport
                    join b in basics
                        on a.BasicParametersTableID equals b.BasicParametersTableID
                    select a.BasicParametersTableID).Distinct().ToList().Count) > 0)
                {
                    listToReturn.Add(currentFirstLevel);
                }

            }

            return listToReturn;
        }
        public List<SecondLevelSubdivisionTable> GetSecondLevelList(int reportId, int userId , int firstLevelId)
        {
            List<SecondLevelSubdivisionTable> listToReturn = new List<SecondLevelSubdivisionTable>();

            List<SecondLevelSubdivisionTable> secondLevelList =
                   (from a in _kPiDataContext.SecondLevelSubdivisionTable
                    where a.Active == true
                    join b in _kPiDataContext.ReportArchiveAndLevelMappingTable
                        on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                    where b.Active == true
                          && b.FK_ReportArchiveTableId == reportId
                          && a.FK_FirstLevelSubdivisionTable == firstLevelId
                    select a).Distinct().ToList();

            List<BasicParametersTable> basicParamsOfUserInReport = (from a in _kPiDataContext.BasicParametersTable
                                                                    where a.Active == true
                                                                    join b in _kPiDataContext.BasicParametrsAndUsersMapping
                                                                        on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                    where b.Active == true
                                                                          && b.FK_UsersTable == userId
                                                                          && b.CanEdit == true
                                                                    join c in _kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                        on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                    where c.Active == true
                                                                          && c.FK_ReportArchiveTable == reportId
                                                                    select a).Distinct().ToList();

            foreach (SecondLevelSubdivisionTable currentSecondLevel in secondLevelList)
            {
                List<ThirdLevelParametrs> thirdLevelsOfThisSecond =
                    (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                     where a.Active == true
                     && a.FK_SecondLevelSubdivisionTable == currentSecondLevel.SecondLevelSubdivisionTableID                    
                     join c in _kPiDataContext.ThirdLevelParametrs
                         on a.ThirdLevelSubdivisionTableID equals c.ThirdLevelParametrsID
                     select c).Distinct().ToList();

                List<int?> FK_ToSubdiviionClasses =
                    (from a in thirdLevelsOfThisSecond select a.FK_SubdivisionClassTable).Distinct().ToList();

                List<BasicParametersTable> basics = new List<BasicParametersTable>();

                foreach (int? currentInt in FK_ToSubdiviionClasses)
                {
                    List<BasicParametersTable> basicParametersOfCurrentInt =
                   (from a in _kPiDataContext.BasicParametersTable
                    where a.Active
                    join b in _kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                        on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                    where b.Active == true
                    where b.FK_SubdivisionClassTable == currentInt
                    select a).Distinct().ToList();

                    foreach (BasicParametersTable currentBasic in basicParametersOfCurrentInt)
                    {
                        basics.Add(currentBasic);
                    }

                }


                if (((from a in basicParamsOfUserInReport
                      join b in basics
                          on a.BasicParametersTableID equals b.BasicParametersTableID
                      select a.BasicParametersTableID).Distinct().ToList().Count) > 0)
                {
                    listToReturn.Add(currentSecondLevel);
                }

            }

            return listToReturn;
        }
        public List<ThirdLevelSubdivisionTable>  GetThirdLevelList(int reportId, int userId, int secondLevelId,int subdivisionClass)
        {
            List<ThirdLevelSubdivisionTable> listToReturn = new List<ThirdLevelSubdivisionTable>();

            List<ThirdLevelSubdivisionTable> thirdLevelList =
                   (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                    where a.Active == true
                    join b in _kPiDataContext.ReportArchiveAndLevelMappingTable
                        on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                    where b.Active == true
                          && b.FK_ReportArchiveTableId == reportId
                          && a.FK_SecondLevelSubdivisionTable == secondLevelId
                          join c in _kPiDataContext.ThirdLevelParametrs
                          on a.ThirdLevelSubdivisionTableID equals c.ThirdLevelParametrsID
                    where ((c.FK_SubdivisionClassTable == subdivisionClass) || (subdivisionClass == 0))
                    select a).Distinct().ToList();

            List<BasicParametersTable> basicParamsOfUserInReport = (from a in _kPiDataContext.BasicParametersTable
                                                                    where a.Active == true
                                                                    join b in _kPiDataContext.BasicParametrsAndUsersMapping
                                                                        on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                    where b.Active == true
                                                                          && b.FK_UsersTable == userId
                                                                          && b.CanEdit == true
                                                                          
                                                                    join c in _kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                        on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                    where c.Active == true
                                                                          && c.FK_ReportArchiveTable == reportId
                                                                    select a).Distinct().ToList();

            foreach (ThirdLevelSubdivisionTable currentThirdLevel in thirdLevelList)
            {
                List<ThirdLevelParametrs> thirdLevelsOfThisSecond =
                    (from a in _kPiDataContext.ThirdLevelParametrs
                     where a.ThirdLevelParametrsID == currentThirdLevel.ThirdLevelSubdivisionTableID
                     select a).Distinct().ToList();

                List<int?> FK_ToSubdiviionClasses =
                    (from a in thirdLevelsOfThisSecond select a.FK_SubdivisionClassTable).Distinct().ToList();

                List<BasicParametersTable> basics = new List<BasicParametersTable>();

                foreach (int? currentInt in FK_ToSubdiviionClasses)
                {
                    List<BasicParametersTable> basicParametersOfCurrentInt =
                   (from a in _kPiDataContext.BasicParametersTable
                    where a.Active
                    join b in _kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                        on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                    where b.Active == true
                    where b.FK_SubdivisionClassTable == currentInt
                    join c in _kPiDataContext.BasicParametrAdditional
                    on a.BasicParametersTableID equals c.BasicParametrAdditionalID
                    where c.Calculated == false
                    select a).Distinct().ToList();

                    foreach (BasicParametersTable currentBasic in basicParametersOfCurrentInt)
                    {
                        basics.Add(currentBasic);
                    }

                }

                List<string> same = (from a in basicParamsOfUserInReport
                    join b in basics
                        on a.BasicParametersTableID equals b.BasicParametersTableID
                    select a.Name).Distinct().ToList();
                List<int> same2 = (from a in basicParamsOfUserInReport
                                     join b in basics
                                         on a.BasicParametersTableID equals b.BasicParametersTableID
                                     select a.BasicParametersTableID).Distinct().ToList();
               

                int tmp = ((from a in basicParamsOfUserInReport
                    join b in basics
                        on a.BasicParametersTableID equals b.BasicParametersTableID
                    select a.BasicParametersTableID).Distinct().ToList().Count);
                if (tmp> 0)
                {
                    listToReturn.Add(currentThirdLevel);
                }

            }

            return listToReturn;
        }
        public List<ThirdLevelSubdivisionTable> GetAllThirdLevelList(int reportId, int userId)
        {
            List<ThirdLevelSubdivisionTable> listToReturn = new List<ThirdLevelSubdivisionTable>();

            List<ThirdLevelSubdivisionTable> thirdLevelList =
                   (from a in _kPiDataContext.ThirdLevelSubdivisionTable
                    where a.Active == true
                    join b in _kPiDataContext.ReportArchiveAndLevelMappingTable
                        on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                    where b.Active == true
                          && b.FK_ReportArchiveTableId == reportId
                    join d in _kPiDataContext.SecondLevelSubdivisionTable
                    on a.FK_SecondLevelSubdivisionTable equals d.SecondLevelSubdivisionTableID
                    where d.Active== true
                    join f in _kPiDataContext.FirstLevelSubdivisionTable
                    on d.FK_FirstLevelSubdivisionTable equals f.FirstLevelSubdivisionTableID
                    where f.Active== true
                    && f.FK_ZeroLevelSubvisionTable == 1
                    join c in _kPiDataContext.ThirdLevelParametrs
                    on a.ThirdLevelSubdivisionTableID equals c.ThirdLevelParametrsID
                    select a).Distinct().ToList();

            List<BasicParametersTable> basicParamsOfUserInReport = (from a in _kPiDataContext.BasicParametersTable
                                                                    where a.Active == true
                                                                    join b in _kPiDataContext.BasicParametrsAndUsersMapping
                                                                        on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                    where b.Active == true
                                                                          && b.FK_UsersTable == userId
                                                                          && b.CanEdit == true
                                                                    join c in _kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                        on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                    where c.Active == true
                                                                          && c.FK_ReportArchiveTable == reportId
                                                                    select a).Distinct().ToList();

            foreach (ThirdLevelSubdivisionTable currentThirdLevel in thirdLevelList)
            {
                List<ThirdLevelParametrs> thirdLevelsOfThisSecond =
                    (from a in _kPiDataContext.ThirdLevelParametrs
                     where a.ThirdLevelParametrsID == currentThirdLevel.ThirdLevelSubdivisionTableID
                     select a).Distinct().ToList();

                List<int?> FK_ToSubdiviionClasses =
                    (from a in thirdLevelsOfThisSecond select a.FK_SubdivisionClassTable).Distinct().ToList();

                List<BasicParametersTable> basics = new List<BasicParametersTable>();

                foreach (int? currentInt in FK_ToSubdiviionClasses)
                {
                    List<BasicParametersTable> basicParametersOfCurrentInt =
                   (from a in _kPiDataContext.BasicParametersTable
                    where a.Active
                    join b in _kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                        on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                    where b.Active == true
                    where b.FK_SubdivisionClassTable == currentInt
                    select a).Distinct().ToList();

                    foreach (BasicParametersTable currentBasic in basicParametersOfCurrentInt)
                    {
                        basics.Add(currentBasic);
                    }

                }


                if (((from a in basicParamsOfUserInReport
                      join b in basics
                          on a.BasicParametersTableID equals b.BasicParametersTableID
                      select a.BasicParametersTableID).Distinct().ToList().Count) > 0)
                {
                    listToReturn.Add(currentThirdLevel);
                }

            }

            return listToReturn;
        }
    }
    public class CheckBoxesToShow
    {
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();
        public bool CanUserEditCheckBoxForeignStudents (int userId)
        {
            if ((from a in _kPiDataContext.BasicParametrsAndUsersMapping
                where a.FK_ParametrsTable == 3681
                      && a.FK_UsersTable == userId
                      && a.Active == true
                      && a.CanEdit == true
                select a).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanUserEditCheckBoxNetwork(int userId)
        {
            if ((from a in _kPiDataContext.BasicParametrsAndUsersMapping
                 where a.FK_ParametrsTable == 3805
                       && a.FK_UsersTable == userId
                       && a.Active == true
                       && a.CanEdit == true
                 select a).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanUserEditCheckBoxModern(int userId)
        {
            if ((from a in _kPiDataContext.BasicParametrsAndUsersMapping
                 where a.FK_ParametrsTable == 3809
                       && a.FK_UsersTable == userId
                       && a.Active == true
                       && a.CanEdit == true
                 select a).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanUserEditCheckBoxInvalid(int userId)
        {
            if ((from a in _kPiDataContext.BasicParametrsAndUsersMapping
                 where a.FK_ParametrsTable == 3801
                       && a.FK_UsersTable == userId
                       && a.Active == true
                       && a.CanEdit == true
                 select a).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ShowUserChecBoxPage(int userId)
        {
            if ((CanUserEditCheckBoxForeignStudents(userId)) || (CanUserEditCheckBoxNetwork(userId)) ||
                (CanUserEditCheckBoxModern(userId)) || (CanUserEditCheckBoxInvalid(userId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class AutoCalculateAfterSave
    {
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();
        #region patterns
        private double Pattern1(int? l0, int? l1, int? l2, int? l3, int reportArchiveId, int specType, string basicAbb, string basicAbb2) // по областям знаний
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
                            a.FK_ZeroLevelSubdivisionTable == l0
                         && a.FK_FirstLevelSubdivisionTable == l1
                         && a.FK_SecondLevelSubdivisionTable == l2
                         && a.FK_ThirdLevelSubdivisionTable == l3
                         && ((z.AbbreviationEN == basicAbb) || ((basicAbb2 != null) && (z.AbbreviationEN == basicAbb2)))
                         && a.FK_ReportArchiveTable == reportArchiveId
                         && b.SpecType == specType
                         && a.Active
                         && d.Active
                         && (e.FK_FieldOfExpertise == 10 || e.FK_FieldOfExpertise == 11 || e.FK_FieldOfExpertise == 12)
                         select a.CollectedValue).Sum());
        }
        private double Pattern2(int? l0, int? l1, int? l2, int? l3, int reportArchiveId, string basicAbb) // для инстранцев
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                      (from a in kpiWebDataContext.CollectedBasicParametersTable
                       join z in kpiWebDataContext.BasicParametersTable
                       on a.FK_BasicParametersTable equals z.BasicParametersTableID
                       join b in kpiWebDataContext.FourthLevelParametrs
                       on a.FK_FourthLevelSubdivisionTable equals b.FourthLevelParametrsID
                       where
                          a.FK_ZeroLevelSubdivisionTable == l0
                       && a.FK_FirstLevelSubdivisionTable == l1
                       && a.FK_SecondLevelSubdivisionTable == l2
                       && a.FK_ThirdLevelSubdivisionTable == l3
                       && z.AbbreviationEN == basicAbb
                       && a.FK_ReportArchiveTable == reportArchiveId
                       && b.IsForeignStudentsAccept == true
                       && a.Active 
                       && z.Active 
                       && b.Active == true
                       select a.CollectedValue).Sum());
        }
        private double Pattern3(int? l3, int specType) // Кол-во ООП // считает кол-во прикрепленных специальностьей
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
                 where c.SpecType == specType
                       && a.ThirdLevelSubdivisionTableID == l3
                       && d.CanGraduate
                       && a.Active 
                       && b.Active 
                 select b).ToList().Count);

        }
        private double Pattern4(int? l3, int specType) // Кол-во ООП с условиями для инвалидов
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
                 where c.SpecType == specType
                       && a.ThirdLevelSubdivisionTableID == l3
                       && d.CanGraduate
                       && c.IsInvalidStudentsFacilities == true
                       && a.Active
                       && b.Active
                 select b).ToList().Count);
        }
        private double Pattern5(int? l3, int specType) // Кол-во ООП с сетевые
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Double tmp = Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == specType
                       && a.ThirdLevelSubdivisionTableID == l3
                       && d.CanGraduate
                       && c.IsNetworkComunication == true
                       && a.Active 
                       && b.Active 
                 select b).ToList().Count);
            return tmp;
        }
        private double Pattern6(int? l3, int specType) // Кол-во ООП с современные образовательные
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
                 where c.SpecType == specType
                       && a.ThirdLevelSubdivisionTableID == l3
                       && d.CanGraduate
                       && a.Active 
                       && b.Active 
                       && c.IsModernEducationTechnologies == true
                 select b).ToList().Count);
        }
        private double Pattern7(int specId, int typeOfCost, int reportId, FourthLevelSubdivisionTable fourth, int specType) // type 0 очное // 1 очное иностранцы // 2 заочное // 3 вечернее
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            if (typeOfCost == 0)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == specId
                                         select a.CostOfCommercOch).FirstOrDefault()
                             *
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                     where a.FK_ReportArchiveTable == reportId
                     && a.FK_FourthLevelSubdivisionTable == fourth.FourthLevelSubdivisionTableID
                     join c in kpiWebDataContext.BasicParametersTable
                     on a.FK_BasicParametersTable equals c.BasicParametersTableID
                     where

                     ((c.AbbreviationEN == "a_Och_M_Kom" && specType == 3)
                     || (c.AbbreviationEN == "a_Och_B_Kom" && specType == 1)
                     || (c.AbbreviationEN == "a_Och_S_Kom" && specType == 2)
                     || (c.AbbreviationEN == "a_Och_A_Kom" && specType == 4))

                     select a.CollectedValue).Sum());
            }
            else if (typeOfCost == 1)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == specId
                                         select a.CostOfCommercOchIn).FirstOrDefault()

                                         *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == reportId
                                  && a.FK_FourthLevelSubdivisionTable == fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "a_Och_In_M" && specType == 3)
                                      || (c.AbbreviationEN == "a_Och_In_B" && specType == 1)
                                      || (c.AbbreviationEN == "a_Och_In_S" && specType == 2)
                                      || (c.AbbreviationEN == "a_Och_In_A" && specType == 4))

                                  select a.CollectedValue).Sum());

            }
            else if (typeOfCost == 2)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == specId
                                         select a.CostOfCommercZaoch).FirstOrDefault()

                                          *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == reportId
                                  && a.FK_FourthLevelSubdivisionTable == fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "c_Z_A_Kom" && specType == 4)
                                  || (c.AbbreviationEN == "c_Z_B_Kom" && specType == 1)
                                  || (c.AbbreviationEN == "c_Z_S_Kom" && specType == 2)
                                  || (c.AbbreviationEN == "c_Z_M_Kom" && specType == 3))
                                  select a.CollectedValue).Sum());
            }
            else if (typeOfCost == 3)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == specId
                                         select a.CostOfCommercEvening).FirstOrDefault()

                                               *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == reportId
                                  && a.FK_FourthLevelSubdivisionTable == fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "b_OchZ_S_Kom" && specType == 2)
                                  || (c.AbbreviationEN == "b_OchZ_M_Kom" && specType == 3)
                                  || (c.AbbreviationEN == "b_OchZ_A_Kom" && specType == 4)
                                  || (c.AbbreviationEN == "b_OchZ_B_Kom" && specType == 1))
                                  select a.CollectedValue).Sum());
            }
            return 0;
        }
        private double Pattern8(int specId, int typeOfCost, int reportId, FourthLevelSubdivisionTable fourth, int specType) // type 0 очное // 1 заочное
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            if (typeOfCost == 0)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == specId
                                         select a.CostOfBudjetOch).FirstOrDefault()
                             *
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                     where a.FK_ReportArchiveTable == reportId
                     && a.FK_FourthLevelSubdivisionTable == fourth.FourthLevelSubdivisionTableID
                     join c in kpiWebDataContext.BasicParametersTable
                     on a.FK_BasicParametersTable equals c.BasicParametersTableID
                     where

                     ((c.AbbreviationEN == "a_Och_M" && specType == 3)
                     || (c.AbbreviationEN == "a_Och_B" && specType == 1)
                     || (c.AbbreviationEN == "a_Och_S" && specType == 2)
                     || (c.AbbreviationEN == "a_Och_A" && specType == 4))

                     select a.CollectedValue).Sum());
            }
            else if (typeOfCost == 1)
            {
                return Convert.ToDouble((from a in kpiWebDataContext.EducationCostTable
                                         where a.Active == true
                                         && a.FK_Specialization == specId
                                         select a.CostOfBudjetZaoch).FirstOrDefault()

                                          *

                                 (from a in kpiWebDataContext.CollectedBasicParametersTable
                                  where a.FK_ReportArchiveTable == reportId
                                  && a.FK_FourthLevelSubdivisionTable == fourth.FourthLevelSubdivisionTableID
                                  join c in kpiWebDataContext.BasicParametersTable
                                  on a.FK_BasicParametersTable equals c.BasicParametersTableID
                                  where
                                  ((c.AbbreviationEN == "c_Z_A" && specType == 4)
                                  || (c.AbbreviationEN == "c_Z_B" && specType == 1)
                                  || (c.AbbreviationEN == "c_Z_S" && specType == 2)
                                  || (c.AbbreviationEN == "c_Z_M" && specType == 3))
                                  select a.CollectedValue).Sum());
            }
            return 0;
        }    
        #endregion
        private double? CalculateByParamIdReportIdSudivisionId(BasicParametersTable basicParam, int reportId, int? l0, int? l1, int? l2, int? l3, int? l4)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            double tmp = 1000000000001;
            if (l4 == null) //кафедра
            {
                if (basicParam.AbbreviationEN == "a_Och_M_IZO") tmp = Pattern1(l0,l1,l2,l3, reportId, 3, "a_Och_M", "a_Och_M_Kom");
                if (basicParam.AbbreviationEN == "b_OchZ_M_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 3, "b_OchZ_M", "b_OchZ_M_Kom");
                if (basicParam.AbbreviationEN == "c_Z_M_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 3, "c_Z_M", "c_Z_M_Kom");
                if (basicParam.AbbreviationEN == "d_E_M_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 3, "d_E_M", "d_E_M_Kom");

                if (basicParam.AbbreviationEN == "a_Och_M_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "a_Och_M");
                if (basicParam.AbbreviationEN == "b_OchZ_M_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "b_OchZ_M");
                if (basicParam.AbbreviationEN == "c_Z_M_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "c_Z_M");
                if (basicParam.AbbreviationEN == "d_E_M_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "d_E_M");

                if (basicParam.AbbreviationEN == "a_Och_M_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "a_Och_M_Kom");
                if (basicParam.AbbreviationEN == "b_OchZ_M_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "b_OchZ_M_Kom");
                if (basicParam.AbbreviationEN == "c_Z_M_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "c_Z_M_Kom");
                if (basicParam.AbbreviationEN == "d_E_M_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "d_E_M_Kom");

                if (basicParam.AbbreviationEN == "a_Och_S_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 2, "a_Och_S", "a_Och_S_Kom");
                if (basicParam.AbbreviationEN == "b_OchZ_S_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 2, "b_OchZ_S", "b_OchZ_S_Kom");
                if (basicParam.AbbreviationEN == "c_Z_S_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 2, "c_Z_S", "c_Z_S_Kom");
                if (basicParam.AbbreviationEN == "d_E_S_IZO") tmp = Pattern1(l0, l1, l2, l3, reportId, 2, "d_E_S", "c_Z_S_Kom");

                if (basicParam.AbbreviationEN == "a_Och_S_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "a_Och_S");
                if (basicParam.AbbreviationEN == "b_OchZ_S_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "b_OchZ_S");
                if (basicParam.AbbreviationEN == "c_Z_S_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "c_Z_S");
                if (basicParam.AbbreviationEN == "d_E_S_NoIn") tmp = Pattern2(l0, l1, l2, l3, reportId, "d_E_S");

                if (basicParam.AbbreviationEN == "a_Och_S_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "a_Och_S_Kom");
                if (basicParam.AbbreviationEN == "b_OchZ_S_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "b_OchZ_S_Kom");
                if (basicParam.AbbreviationEN == "c_Z_S_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "c_Z_S_Kom");
                if (basicParam.AbbreviationEN == "d_E_S_NoIn_Kom") tmp = Pattern2(l0, l1, l2, l3, reportId, "d_E_S_Kom");

                if (basicParam.AbbreviationEN == "a_Och_B_IZO") tmp = Pattern1(l0,l1,l2,l3, reportId, 1, "a_Och_B", "a_Och_B_Kom");
                if (basicParam.AbbreviationEN == "b_OchZ_B_IZO") tmp = Pattern1(l0,l1,l2,l3, reportId, 1, "b_OchZ_B", "b_OchZ_B_Kom");
                if (basicParam.AbbreviationEN == "c_Z_B_IZO") tmp = Pattern1(l0,l1,l2,l3, reportId, 1, "c_Z_B", "c_Z_B_Kom");
                if (basicParam.AbbreviationEN == "d_E_B_IZO") tmp = Pattern1(l0,l1,l2,l3, reportId, 1, "d_E_B", "d_E_B_Kom");

                if (basicParam.AbbreviationEN == "a_Och_B_NoIn") tmp = Pattern2(l0,l1,l2,l3, reportId, "a_Och_B");
                if (basicParam.AbbreviationEN == "b_OchZ_B_NoIn") tmp = Pattern2(l0,l1,l2,l3, reportId, "b_OchZ_B");
                if (basicParam.AbbreviationEN == "c_Z_B_NoIn") tmp = Pattern2(l0,l1,l2,l3, reportId, "c_Z_B");
                if (basicParam.AbbreviationEN == "d_E_B_NoIn") tmp = Pattern2(l0,l1,l2,l3, reportId, "d_E_B");

                if (basicParam.AbbreviationEN == "a_Och_B_NoIn_Kom") tmp = Pattern2(l0,l1,l2,l3, reportId, "a_Och_B_Kom");
                if (basicParam.AbbreviationEN == "b_OchZ_B_NoIn_Kom") tmp = Pattern2(l0,l1,l2,l3, reportId, "b_OchZ_B_Kom");
                if (basicParam.AbbreviationEN == "c_Z_B_NoIn_Kom") tmp = Pattern2(l0,l1,l2,l3, reportId, "c_Z_B_Kom");
                if (basicParam.AbbreviationEN == "d_E_B_NoIn_Kom") tmp = Pattern2(l0,l1,l2,l3, reportId, "d_E_B_Kom");

                if (basicParam.AbbreviationEN == "OOP_M") tmp = Pattern3(l3, 3);
                if (basicParam.AbbreviationEN == "kol_M_OP") tmp = Pattern4(l3, 3);
                if (basicParam.AbbreviationEN == "kol_M_OP_SV") tmp = Pattern5(l3, 3);
                if (basicParam.AbbreviationEN == "OOP_M_SOT") tmp = Pattern6(l3, 3);

                if (basicParam.AbbreviationEN == "OOP_S") tmp = Pattern3(l3, 2);
                if (basicParam.AbbreviationEN == "kol_S_OP") tmp = Pattern4(l3, 2);
                if (basicParam.AbbreviationEN == "kol_S_OP_SV") tmp = Pattern5(l3, 2);
                if (basicParam.AbbreviationEN == "OOP_S_SOT") tmp = Pattern6(l3, 2);

                if (basicParam.AbbreviationEN == "OOP_B") tmp = Pattern3(l3, 1);
                if (basicParam.AbbreviationEN == "kol_B_OP") tmp = Pattern4(l3, 1);
                if (basicParam.AbbreviationEN == "kol_B_OP_SV") tmp = Pattern5(l3, 1);
                if (basicParam.AbbreviationEN == "OOP_B_SOT") tmp = Pattern6(l3, 1);

                if (basicParam.AbbreviationEN == "OOP_A") tmp = Pattern3(l3, 4);
                if (basicParam.AbbreviationEN == "kol_A_OP") tmp = Pattern4(l3, 4);
                if (basicParam.AbbreviationEN == "kol_A_OP_SV") tmp = Pattern5(l3, 4);
                if (basicParam.AbbreviationEN == "OOP_A_SOT") tmp = Pattern6(l3, 4);

                //новые показатели 13.06.2015
                if (basicParam.AbbreviationEN == "a_Och_M_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 3, "a_Och_M_C", null);
                if (basicParam.AbbreviationEN == "b_OchZ_M_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 3, "b_OchZ_M_C", null);
                if (basicParam.AbbreviationEN == "c_Z_M_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 3, "c_Z_M_C", null);
                if (basicParam.AbbreviationEN == "d_E_M_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 3, "d_E_M_C", null);

                if (basicParam.AbbreviationEN == "a_Och_B_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 1, "a_Och_B_C", null);
                if (basicParam.AbbreviationEN == "d_E_B_CO_R") tmp =
                    Pattern1(l0,l1,l2,l3, reportId, 1, "d_E_B_C", null);
                if (basicParam.AbbreviationEN == "c_Z_B_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 1, "c_Z_B_C", null);
                if (basicParam.AbbreviationEN == "d_E_B_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 1, "d_E_B_C", null);

                if (basicParam.AbbreviationEN == "a_Och_S_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 2, "a_Och_S_C", null);
                if (basicParam.AbbreviationEN == "b_OchZ_S_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 2, "b_OchZ_S_C", null);
                if (basicParam.AbbreviationEN == "c_Z_S_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 2, "c_Z_S_C", null);
                if (basicParam.AbbreviationEN == "d_E_S_CO_R") tmp =
                    Pattern1(l0, l1, l2, l3, reportId, 2, "d_E_S_C", null);
                //новые показатели 
            }
            else
            {
                FourthLevelSubdivisionTable fourthLevel = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                    where a.FourthLevelSubdivisionTableID == l4
                    select a).FirstOrDefault();
                if (fourthLevel != null)
                {
                    //новейшие показатели 19.06.2015  
                    //// type 0 очное // 1 очное иностранцы // 2 заочное // 3 вечернее
                    if (basicParam.AbbreviationEN == "a_Och_B_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_OchZ_B_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 3, reportId, fourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_Z_B_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 2, reportId, fourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_IN_B_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 1);

                    if (basicParam.AbbreviationEN == "a_Och_S_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_OchZ_S_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 3, reportId, fourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_Z_S_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 2, reportId, fourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_IN_S_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 2);


                    if (basicParam.AbbreviationEN == "a_Och_M_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_OchZ_M_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 3, reportId, fourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_Z_M_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 2, reportId, fourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_IN_M_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 3);

                    if (basicParam.AbbreviationEN == "a_Och_A_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_OchZ_A_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 3, reportId, fourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_Z_A_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 2, reportId, fourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_IN_A_Kom_money")
                        tmp =
                            Pattern7(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 4);

                    // 01.07.2015
                    if (basicParam.AbbreviationEN == "a_Och_A_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 4);
                    if (basicParam.AbbreviationEN == "a_Z_A_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 4);

                    if (basicParam.AbbreviationEN == "a_Och_M_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 3);
                    if (basicParam.AbbreviationEN == "a_Z_M_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 3);

                    if (basicParam.AbbreviationEN == "a_Och_S_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 2);
                    if (basicParam.AbbreviationEN == "a_Z_S_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 2);

                    if (basicParam.AbbreviationEN == "a_Och_B_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 0, reportId, fourthLevel, 1);
                    if (basicParam.AbbreviationEN == "a_Z_B_money")
                        tmp =
                            Pattern8(fourthLevel.FK_Specialization, 1, reportId, fourthLevel, 1);
                    //01.07.2015
                }
            }
            if (Math.Abs(tmp - (double)1000000000001) < 10)
            {
                return 0;
            }
            return tmp;
        }
        private void FindBasicsAndCalculate(double? defaultValue,int reportId, int userId, int subdivisionLevel, int subdivisionId,int? thirdLevelFfToSubdivisionClassTable)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(subdivisionLevel, subdivisionId);
            List<BasicParametersTable> basicParametersToCalculate = (from a in _kPiDataContext.BasicParametersTable
                                                                     where a.Active
                                                                     join b in _kPiDataContext.BasicParametrsAndUsersMapping
                                                                         on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                     where b.Active
                                                                     join c in _kPiDataContext.BasicParametrAdditional
                                                                         on a.BasicParametersTableID equals c.BasicParametrAdditionalID                                                                                                                                         
                                                                     join d in _kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                         on a.BasicParametersTableID equals d.FK_BasicParametrsTable
                                                                     where d.Active
                                                                     join f in _kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                                                                     on a.BasicParametersTableID equals f.FK_BasicParametrsTable
                                                                     where f.Active == true

                                                                     && c.Calculated == true // 1
                                                                     && d.FK_ReportArchiveTable == reportId  // 2  
                                                                     && b.FK_UsersTable == userId  // 3
                                                                     && b.CanEdit          //3                                            
                                                                     && c.SubvisionLevel == subdivisionLevel // 4
                                                                     && f.FK_SubdivisionClassTable == thirdLevelFfToSubdivisionClassTable // 5
                                                                    
                                                                     select a).Distinct().ToList();
            //Нашли базовые показателя с условиями
            //Показатель рассчетный
            //Показатель относится к отчету
            //Показатель относится к пользователю с правом CanEdit
            //Показатель рассчитывется для данного уровня
            //Показатель относится к данному структурному подразделению
            CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
            foreach (BasicParametersTable currentBasic in basicParametersToCalculate)
            {
                CollectedBasicParametersTable parametr = collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId, currentBasic.BasicParametersTableID,
                    subdivisionLevel, subdivisionId, true, defaultValue, userId);
                parametr.CollectedValue = CalculateByParamIdReportIdSudivisionId(currentBasic, reportId, subdivisionIds[0], subdivisionIds[1],
                    subdivisionIds[2], subdivisionIds[3], subdivisionIds[4])??defaultValue;
                _kPiDataContext.SubmitChanges();
            }
        }
        public void AutoCalculate(int reportId, int userId, int subdivisionId, int subdivisionLevel, List<ThirdLevelSubdivisionTable> thirdLevelListIn, double? defaultValue)
        {         
            MainFunctions mainFunctions = new MainFunctions();
            List<ThirdLevelSubdivisionTable> thirdLevelList = thirdLevelListIn ?? mainFunctions.GetAllThirdLevelsByLevelAndId(subdivisionLevel, subdivisionId);
            foreach (ThirdLevelSubdivisionTable currentThird in thirdLevelList)
            {
                if ((from a in _kPiDataContext.ReportArchiveAndLevelMappingTable
                    where
                        a.FK_ReportArchiveTableId == reportId &&
                        a.FK_ThirdLevelSubdivisionTable == currentThird.ThirdLevelSubdivisionTableID
                    select a).Any())
                {
                    
                }
                else
                {                    
                    continue;
                }

                ThirdLevelParametrs thirdLevelParams = (from a in _kPiDataContext.ThirdLevelParametrs
                    where a.ThirdLevelParametrsID == currentThird.ThirdLevelSubdivisionTableID
                    select a).FirstOrDefault();
                List<FourthLevelSubdivisionTable> fourthLevelList = mainFunctions.GetFourhLevelsByThirdId(currentThird.ThirdLevelSubdivisionTableID);
                if (fourthLevelList != null)
                { // есть специальности
                    foreach (FourthLevelSubdivisionTable currentFourthLevel in fourthLevelList)
                    {
                        // вызываем для каждого четвертого
                        FindBasicsAndCalculate(defaultValue,reportId, userId, 4, currentFourthLevel.FourthLevelSubdivisionTableID, thirdLevelParams == null ? null : thirdLevelParams.FK_SubdivisionClassTable);
                    }
                }
                //вызываем для каждого третьего
                FindBasicsAndCalculate(defaultValue,reportId, userId, 3, currentThird.ThirdLevelSubdivisionTableID, thirdLevelParams == null ? null : thirdLevelParams.FK_SubdivisionClassTable);
            }           
        }
    }
    public class CollectedDataProcess
    {
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();

        public List<CollectedBasicParametersTable> GetListOfCollectedDataByParams(int l0, int l1, int l2, int l3, int l4, int l5, int reportId,
    int userId, bool withCalculated)
        {
            List<CollectedBasicParametersTable> collectedList;
            List<CollectedBasicParametersTable> collectedListForThird;
            List<CollectedBasicParametersTable> collectedListForFourth;
            {
                collectedListForThird = (from a in _kPiDataContext.CollectedBasicParametersTable
                                         where a.FK_ReportArchiveTable == reportId
                                               && ((a.FK_ZeroLevelSubdivisionTable == l0) || (l0 == 0))
                                               && ((a.FK_FirstLevelSubdivisionTable == l1) || (l1 == 0))
                                               && ((a.FK_SecondLevelSubdivisionTable == l2) || (l2 == 0))
                                               && ((a.FK_ThirdLevelSubdivisionTable == l3) || (l3 == 0))
                                               && ((a.FK_FourthLevelSubdivisionTable == l4) || (l4 == 0))
                                               && ((a.FK_FifthLevelSubdivisionTable == l5) || (l5 == 0))
                                         join b in _kPiDataContext.BasicParametrAdditional
                                             on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                         where (b.Calculated == false || withCalculated == true)
                                         && b.SubvisionLevel == 3
                                         join c in _kPiDataContext.BasicParametersTable
                                             on b.BasicParametrAdditionalID equals c.BasicParametersTableID
                                         where c.Active == true
                                         join d in _kPiDataContext.BasicParametrsAndUsersMapping
                                             on c.BasicParametersTableID equals d.FK_ParametrsTable
                                         where d.Active == true
                                               && d.CanEdit == true
                                               && d.FK_UsersTable == userId
                                         join s1 in _kPiDataContext.FirstLevelSubdivisionTable
                                             on a.FK_FirstLevelSubdivisionTable equals s1.FirstLevelSubdivisionTableID
                                         join s2 in _kPiDataContext.SecondLevelSubdivisionTable
                                             on a.FK_SecondLevelSubdivisionTable equals s2.SecondLevelSubdivisionTableID
                                         join s3 in _kPiDataContext.ThirdLevelSubdivisionTable
                                             on a.FK_ThirdLevelSubdivisionTable equals s3.ThirdLevelSubdivisionTableID

                                         select a).Distinct().ToList();
            }
            {
                collectedListForFourth = (from a in _kPiDataContext.CollectedBasicParametersTable
                                          where a.FK_ReportArchiveTable == reportId
                                                && ((a.FK_ZeroLevelSubdivisionTable == l0) || (l0 == 0))
                                                && ((a.FK_FirstLevelSubdivisionTable == l1) || (l1 == 0))
                                                && ((a.FK_SecondLevelSubdivisionTable == l2) || (l2 == 0))
                                                && ((a.FK_ThirdLevelSubdivisionTable == l3) || (l3 == 0))
                                                && ((a.FK_FourthLevelSubdivisionTable == l4) || (l4 == 0))
                                                && ((a.FK_FifthLevelSubdivisionTable == l5) || (l5 == 0))
                                          join b in _kPiDataContext.BasicParametrAdditional
                                              on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                          where (b.Calculated == false || withCalculated == true)
                                          && b.SubvisionLevel == 4
                                          join c in _kPiDataContext.BasicParametersTable
                                              on b.BasicParametrAdditionalID equals c.BasicParametersTableID
                                          where c.Active == true
                                          join d in _kPiDataContext.BasicParametrsAndUsersMapping
                                              on c.BasicParametersTableID equals d.FK_ParametrsTable
                                          where d.Active == true
                                                && d.CanEdit == true
                                                && d.FK_UsersTable == userId

                                          join s1 in _kPiDataContext.FirstLevelSubdivisionTable
                                              on a.FK_FirstLevelSubdivisionTable equals s1.FirstLevelSubdivisionTableID
                                          join s2 in _kPiDataContext.SecondLevelSubdivisionTable
                                              on a.FK_SecondLevelSubdivisionTable equals s2.SecondLevelSubdivisionTableID
                                          join s3 in _kPiDataContext.ThirdLevelSubdivisionTable
                                              on a.FK_ThirdLevelSubdivisionTable equals s3.ThirdLevelSubdivisionTableID
                                          join s4 in _kPiDataContext.FourthLevelSubdivisionTable
                                          on a.FK_FourthLevelSubdivisionTable equals s4.FourthLevelSubdivisionTableID
                                          join s4Param in _kPiDataContext.FourthLevelParametrs
                                          on s4.FourthLevelSubdivisionTableID equals s4Param.FourthLevelParametrsID
                                          where s1.Active == true
                                          && s2.Active == true
                                          && s3.Active == true
                                          && s4.Active == true
                                          && ((b.ForForeignStudents == false) || (b.ForForeignStudents == s4Param.IsForeignStudentsAccept))
                                          select a).Distinct().ToList();
            }

            collectedList = collectedListForThird.Concat(collectedListForFourth).Distinct().ToList();
            return collectedList;
        }

        public CollectedBasicParametersTable GetCollectedBasicParametrByReportBasicLevel(int reportId, int basicParamId,
            int subdivisionLevel, int levelId, bool createIfNotExist, double? defaultValue, int userId)
        {
            MainFunctions mainFunctions = new MainFunctions();
            /*CollectedBasicParametersTable collectedBasicParameter =
                (from a in _kPiDataContext.CollectedBasicParametersTable
                 where a.Active == true
                       && a.FK_BasicParametersTable == basicParamId
                       && a.FK_ReportArchiveTable == reportId
                       && (((subdivisionLevel == 0) && (levelId == a.FK_ZeroLevelSubdivisionTable))
                           || ((subdivisionLevel == 1) && (levelId == a.FK_FirstLevelSubdivisionTable))
                           || ((subdivisionLevel == 2) && (levelId == a.FK_SecondLevelSubdivisionTable))
                           || ((subdivisionLevel == 3) && (levelId == a.FK_ThirdLevelSubdivisionTable))
                           || ((subdivisionLevel == 4) && (levelId == a.FK_FourthLevelSubdivisionTable)))
                 select a).FirstOrDefault();
            */

             CollectedBasicParametersTable collectedBasicParameter =
            (from a in _kPiDataContext.CollectedBasicParametersTable
             where a.Active == true
                   && a.FK_BasicParametersTable == basicParamId
                   && a.FK_ReportArchiveTable == reportId
                   && (((subdivisionLevel == 0) && (levelId == a.FK_ZeroLevelSubdivisionTable) && (a.FK_FirstLevelSubdivisionTable == null))
                       || ((subdivisionLevel == 1) && (levelId == a.FK_FirstLevelSubdivisionTable) && (a.FK_SecondLevelSubdivisionTable == null))
                       || ((subdivisionLevel == 2) && (levelId == a.FK_SecondLevelSubdivisionTable) && (a.FK_ThirdLevelSubdivisionTable == null))
                       || ((subdivisionLevel == 3) && (levelId == a.FK_ThirdLevelSubdivisionTable) && (a.FK_FourthLevelSubdivisionTable == null))
                       || ((subdivisionLevel == 4) && (levelId == a.FK_FourthLevelSubdivisionTable) && (a.FK_FifthLevelSubdivisionTable == null)))
             select a).FirstOrDefault();
        
            if ((collectedBasicParameter == null) && (createIfNotExist))
            {
                int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(subdivisionLevel, levelId);
                collectedBasicParameter = new CollectedBasicParametersTable();
                collectedBasicParameter.Active = true;
                collectedBasicParameter.Status = 0;
                collectedBasicParameter.CollectedValue = defaultValue;
                collectedBasicParameter.UserIP = mainFunctions.GetUserIp();
                collectedBasicParameter.FK_UsersTable = userId;
                collectedBasicParameter.FK_ReportArchiveTable = reportId;
                collectedBasicParameter.FK_BasicParametersTable = basicParamId;
                collectedBasicParameter.FK_ZeroLevelSubdivisionTable = subdivisionIds[0];
                collectedBasicParameter.FK_FirstLevelSubdivisionTable = subdivisionIds[1];
                collectedBasicParameter.FK_SecondLevelSubdivisionTable = subdivisionIds[2];
                collectedBasicParameter.FK_ThirdLevelSubdivisionTable = subdivisionIds[3];
                collectedBasicParameter.FK_FourthLevelSubdivisionTable = subdivisionIds[4];
                collectedBasicParameter.FK_FifthLevelSubdivisionTable = subdivisionIds[5];
                collectedBasicParameter.LastChangeDateTime = DateTime.Now;
                collectedBasicParameter.SavedDateTime = DateTime.Now;
                _kPiDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicParameter);
                _kPiDataContext.SubmitChanges();
            }
            else if (createIfNotExist)
            {
                if (collectedBasicParameter.CollectedValue == null)
                {
                    collectedBasicParameter.CollectedValue = defaultValue;
                    _kPiDataContext.SubmitChanges();
                }
            }
            return collectedBasicParameter;
        }

        public void SetStatusToAllCollected(int reportId, int subdivisionLevel, int levelId ,int userId, int statusToSet)
        {
            MainFunctions mainFunctions = new MainFunctions();
            int?[] subdivisionIds = mainFunctions.GetSubdivisonIds(subdivisionLevel, levelId);
            List<CollectedBasicParametersTable> collectedList =
                GetListOfCollectedDataByParams(mainFunctions.IntNullTo0(subdivisionIds[0]),
                    mainFunctions.IntNullTo0(subdivisionIds[1]),
                    mainFunctions.IntNullTo0(subdivisionIds[2]),
                    mainFunctions.IntNullTo0(subdivisionIds[3]),
                    mainFunctions.IntNullTo0(subdivisionIds[4]),
                    mainFunctions.IntNullTo0(subdivisionIds[5]), reportId, userId, true);

            foreach (CollectedBasicParametersTable currentCollected in collectedList)
            {
                currentCollected.Status = statusToSet;
            }      
            _kPiDataContext.SubmitChanges();
        }

        public void ConfirmCollectedBasic(int reportId, int basicParamId, int subdivisionLevel, int levelId)
        {
            CollectedBasicParametersTable collectedBasicParameter =
                (from a in _kPiDataContext.CollectedBasicParametersTable
                 where a.Active == true
                       && a.FK_BasicParametersTable == basicParamId
                       && a.FK_ReportArchiveTable == reportId
                       && (((subdivisionLevel == 0) && (levelId == a.FK_ZeroLevelSubdivisionTable))
                           || ((subdivisionLevel == 1) && (levelId == a.FK_FirstLevelSubdivisionTable))
                           || ((subdivisionLevel == 2) && (levelId == a.FK_SecondLevelSubdivisionTable))
                           || ((subdivisionLevel == 3) && (levelId == a.FK_ThirdLevelSubdivisionTable))
                           || ((subdivisionLevel == 4) && (levelId == a.FK_FourthLevelSubdivisionTable)))
                 select a).FirstOrDefault();

            if (collectedBasicParameter != null)
            {
                collectedBasicParameter.Status = 5;
                _kPiDataContext.SubmitChanges();
            }
        }
    }
    public class TmpProrectorFillFunctions
    {
        /*

        12758 form.politic@mail.ru		Проректор по международной деятельности и информационной политике
        12401 elena-chuyan@rambler.ru	Первый проректор
         * 
        
         * 
        12399 napks@napks.edu.ua		Проректор по научной деятельности
         * 
        12324 vladimir@crimea.edu		Проректор по учебной и методической деятельности
        12402 va.mikheev@mail.ru		Проректор по организационной и правовой деятельности
        12405 barkova.cfu@yandex.ru		Проректор по финансовой и экономической деятельности       
         * 
        12412 kfu.innovacia@mail.ru		Проректор по инновационной деятельности и перспективному развитию
         */
        private readonly KPIWebDataContext _kPiDataContext = new KPIWebDataContext();
        private int[] prorectorFillsByStruct = new[] { 12758, 12401, 12399, 12412 }; // заполняют по кафедрам 
        private int[] prorectorFillsForAllStruct = new[] { 12402, 12405,12324 };// заполняют для всего КФУ сразу
        private int[] reportsToAllowFillForAllStruct = new[] {4};
        private int[] reportsToAllowFillByStruct = new[] {4};

        private int[] basicParametrsNotToShow0 = new[] { 3730, 3731, 3732, 3733, 3734, 3735, 3736, 3737, 3738 };
        private int[] basicParametrsNotToShow1 = new[] { 3740, 3741, 3742, 3743, 3744, 3745, 3746, 3747, 3748 };
        private int[] basicParametrsNotToShow2 = new[] { 3750, 3751, 3752, 3753, 3754, 3755, 3756, 3757, 3758 };
        private int[] basicParametrsNotToShow3 = new[] { 3760, 3761, 3762, 3763, 3764, 3765, 3766, 3767, 3768 };
        private int[] basicParametrsNotToShow4 = new[] { 3770, 3771, 3772, 3773, 3774, 3775, 3776, 3777, 3778 };

        public bool IsBasicNotToShow(int id)
        {
            if (basicParametrsNotToShow0.Contains(id))
                return true;
            if (basicParametrsNotToShow1.Contains(id))
                return true;
            if (basicParametrsNotToShow2.Contains(id))
                return true;
            if (basicParametrsNotToShow3.Contains(id))
                return true;
            if (basicParametrsNotToShow4.Contains(id))
                return true;

            return false;
        }
        public bool CanProrectorFillReportByStruct(int reportId, int userId)
        {
            if ((reportsToAllowFillByStruct.Contains(reportId)) && (prorectorFillsByStruct.Contains(userId)))
                return true;
            return false;
        }
        public bool CanProrectorFillReportForAllStruct(int reportId, int userId)
        {
            if ((prorectorFillsForAllStruct.Contains(userId)) && (reportsToAllowFillForAllStruct.Contains(reportId)))
                return true;
            return false;
        }
        public string GetStatusNameForStructOnly(int? l0,int? l1,int? l2, int? l3, int? l4, int? l5 , int userId, int reportId)
        {
            List<CollectedBasicParametersTable> collectedList = (from a in _kPiDataContext.CollectedBasicParametersTable
                      where (a.FK_ZeroLevelSubdivisionTable == l0 || (a.FK_ZeroLevelSubdivisionTable == null && l0==null))
                      && (a.FK_FirstLevelSubdivisionTable == l1 || (a.FK_FirstLevelSubdivisionTable == null && l1 == null))
                      && (a.FK_SecondLevelSubdivisionTable == l2 || (a.FK_SecondLevelSubdivisionTable == null && l2 == null))
                      && (a.FK_ThirdLevelSubdivisionTable == l3 || (a.FK_ThirdLevelSubdivisionTable == null && l3 == null))
                      && (a.FK_FourthLevelSubdivisionTable == l4 || (a.FK_FourthLevelSubdivisionTable == null && l4 == null))
                      && (a.FK_FifthLevelSubdivisionTable == l5 || (a.FK_FifthLevelSubdivisionTable == null && l5 == null))
                      && a.FK_ReportArchiveTable == reportId
                      && a.Active == true
                join b in _kPiDataContext.BasicParametrsAndUsersMapping
                    on a.FK_BasicParametersTable equals b.FK_ParametrsTable
                where b.CanEdit == true
                      && b.Active == true
                      && b.FK_UsersTable == userId
                select a).Distinct().ToList();

            int collectedWithStatus0 = (from a in collectedList where (a.Status == 0) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=0 данных нет 
            int collectedWithStatus2 = (from a in collectedList where (a.Status == 2) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=2 данные есть
            int collectedWithStatus5 = (from a in collectedList where (a.Status == 5) && (a.CollectedValue.ToString().Any()) select a).Count();  //status=5 данные утверждены директором академии
            if (collectedList.Count == 0)
                return "Данные не вносились";
            if (collectedList.Count == collectedWithStatus5)
                return "Данные отправлены";
            if (collectedList.Count == collectedWithStatus0 + collectedWithStatus2)
                return "Данные ожидают отправки";
            return "Данные частично внесены";
        }
    }
}
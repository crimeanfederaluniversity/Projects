using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.DynamicData;
using Microsoft.Office.Interop.Word;
using WebGrease;

namespace Competitions
{
    public class Status
    {
        private int dataTypeNotToCheck0 = 7;
        private int dataTypeNotToCheck1 = 8;
        private int dataTypeNotToCheck2 = 13;
        private int dataTypeNotToCheck3 = 11;
        private int dataTypeNotToCheck4 = 12;
        private int dataTypeNotToCheck5 = 13;
        private int dataTypeNotToCheck6 = 14;
        private int dataTypeNotToCheck11 = 15;  
        private int dataTypeNotToCheck7 = 16;
        private int dataTypeNotToCheck8 = 17; //17
        private int dataTypeNotToCheck9 = 18;
        private int dataTypeNotToCheck10 = 19;
        
        private int statusNoData = 0;
        private int statusPartly = 1;
        private int statusAllData = 2;
        
        private readonly CompetitionDataContext _competitionDataBase = new CompetitionDataContext();
        public bool IsApplicationReadyToSend(int applicationId)
        {
            List<zBlockTable> blockList = (from a in _competitionDataBase.zBlockTable
                                              where a.Active == true
                                              select a).ToList();
            zApplicationTable currentApplication  = (from a in _competitionDataBase.zApplicationTable
                                                     where a.ID == applicationId
                                                     && a.Active == true
                                                     select a).FirstOrDefault();
            if (currentApplication == null)
                return false;
            if (currentApplication.StartProjectDate == null)
                return false;
            if (currentApplication.EndProjectDate == null)
                return false;

            foreach (zBlockTable block in blockList)
            {
                if (GetStatusIdForBlockInApplication(block.ID, applicationId) != statusAllData)
                {
                    return false;
                }
            }            
            return true;
        }
        public bool IsDataReady(int status)
        {
            if (status == statusAllData)
                return true;
            return false;
        }
        public string GetStatusNameByStatusId(int statusId)
        {
            switch (statusId)
            {
                case 0:
                {
                    return "Данных нет";
                }
                case 1:
                {
                    return "Данные частично внесены";
                }
                case 2:
                {
                    return "Данные внесены";
                }
                default:
                {
                    return "error";
                }
            }
        }
        private bool DoesSectionContainColumnWithDataType(int dataType,int sectionId)
        {
            return ((from a in _competitionDataBase.zColumnTable
                where a.Active == true
                      && a.FK_SectionTable == sectionId
                      && a.DataType == dataType
                select a).ToList().Count > 0);
        }
        private bool DoesAllNescesearyDataExist(int blockId,int applicationId)
        {
            DataBaseSimilarRequests dataBaseSimilarRequests = new DataBaseSimilarRequests();
            zBlockTable currentBlock = dataBaseSimilarRequests.GetBlockTableById(blockId);
            if (currentBlock != null)
            {
                List<zSectionTable> sectionsInCurrentBlock =
                    dataBaseSimilarRequests.GetSectionsListInBlock(currentBlock.ID,applicationId);
                if (sectionsInCurrentBlock.Any())
                {
                    ///нужно удалить из списка всех секций те, которые могут не существовать
                    ///это те секции которые зависят от предыдущих и кол-во строк в них прямо зависит от кол-ва галочек
                    ///эти секции не будем учитывать, это конечно не очень хорошо, но другого варианта нет.
                    ///проверим все колонки в секциях по datatype
                    /// data type не должен быть равен 16 (зависимость существования от чекбокса)

                    List<zSectionTable> clearSectionList = new List<zSectionTable>();
                    foreach (zSectionTable currentSection in sectionsInCurrentBlock)
                    {
                        if (!DoesSectionContainColumnWithDataType(16, currentSection.ID))
                        {
                            clearSectionList.Add(currentSection);
                        }
                    }
                    //получили список с секциями которые обязательно будут заполнены
                    if (clearSectionList.Any())
                    {
                        //тааак теперь найдем все колонки в этих секциях
                        List<zColumnTable> columnsInSections = new List<zColumnTable>();
                        foreach (zSectionTable currentSection in clearSectionList)
                        {
                            columnsInSections.AddRange(dataBaseSimilarRequests.GetColumnsListInSection(currentSection.ID));
                        }
                        //получили список всех колонок в которых должны быть данные
                        //надо проверить есть ли в каждой из этих колонок в этой заявке хотябы одно значение

                        foreach (zColumnTable currentColumn in columnsInSections)
                        {
                            if (currentColumn.DataType == dataTypeNotToCheck0) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck1) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck2) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck3) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck4) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck5) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck6) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck7) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck8) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck9) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck10) //автоинкремент
                                continue;
                            if (currentColumn.DataType == dataTypeNotToCheck11) //автоинкремент
                                continue;
                            if (!DoesColumnInApplicationHasData(currentColumn.ID, applicationId))
                                return false;
                        }
                        return true;

                    }
                    else
                    {
                        //список пуст // нет обязательных секций
                    }
                }
                else
                {
                    //в блоке нет секций
                }
            }
            else
            {
                //блок не найден
            }
            return false;
        }
        private bool DoesColumnInApplicationHasData(int columnId, int applicationId)
        {
            List<zCollectedDataTable> collectedDataList = (from a in _competitionDataBase.zCollectedDataTable
                where a.FK_ColumnTable == columnId
                join b in _competitionDataBase.zCollectedRowsTable
                    on a.FK_CollectedRowsTable equals b.ID
                where b.Active == true
                      && b.FK_ApplicationTable == applicationId
                select a).Distinct().ToList();
            if (collectedDataList.Any()) return true;
            return false;
        }
        private bool DoesAnyDataExistInBlockByApplication(int blockId ,int  applicationId )
        {
             if((from a in _competitionDataBase.zCollectedDataTable
                where a.Active == true
                join b in _competitionDataBase.zCollectedRowsTable
                    on a.FK_CollectedRowsTable equals b.ID
                where b.Active == true
                      && b.FK_ApplicationTable == applicationId
                join c in _competitionDataBase.zColumnTable
                    on a.FK_ColumnTable equals c.ID
                where c.Active == true
                join d in _competitionDataBase.zSectionTable
                    on c.FK_SectionTable equals d.ID
                where d.Active == true
                      && d.FK_BlockID == blockId
                select a).Distinct().ToList().Any())
                return true;
            return false;
        }
        private bool IsAllDataNotNullInBlockByApplication(int blockId, int applicationId)
        {
            List<zCollectedDataTable> allDataInApplication = (from a in _competitionDataBase.zCollectedDataTable
                join b in _competitionDataBase.zCollectedRowsTable
                    on a.FK_CollectedRowsTable equals b.ID
                where a.Active == true
                      && b.Active == true
                      && b.FK_ApplicationTable == applicationId

                join c in _competitionDataBase.zColumnTable
                    on a.FK_ColumnTable equals c.ID
                join d in _competitionDataBase.zSectionTable
                    on c.FK_SectionTable equals d.ID
                where c.Active == true
                      && d.Active == true
                      && d.FK_BlockID == blockId

                 where c.DataType!= dataTypeNotToCheck0
                 && c.DataType != dataTypeNotToCheck1
                 && c.DataType != dataTypeNotToCheck2
                 && c.DataType!= dataTypeNotToCheck3
                 && c.DataType != dataTypeNotToCheck4
                 && c.DataType != dataTypeNotToCheck5
                 && c.DataType!= dataTypeNotToCheck6
                 && c.DataType != dataTypeNotToCheck7
                 && c.DataType != dataTypeNotToCheck8
                 && c.DataType != dataTypeNotToCheck9
                 && c.DataType != dataTypeNotToCheck10
                 && c.DataType !=  dataTypeNotToCheck11
                select a).Distinct().ToList();

            int allCollectedCount = allDataInApplication.Count;
            int allCollectedWithData = (from a in allDataInApplication
                where a.ValueBit != null
                      || a.ValueDataTime != null
                      || a.ValueDouble != null
                      || a.ValueFK_CollectedDataTable != null
                      || a.ValueInt != null
                      || a.ValueText != null
                select a).Count();
            if (allCollectedCount == allCollectedWithData)
            return true;
            return false;
        }
        public int GetStatusIdForBlockInApplication(int blockId, int applicationId)
        {
            bool allNescesearyDataExist = DoesAllNescesearyDataExist(blockId, applicationId);
            bool isThereAnyData = DoesAnyDataExistInBlockByApplication(blockId, applicationId);
            bool isAllDataNotNull = IsAllDataNotNullInBlockByApplication(blockId, applicationId);

            if (!isThereAnyData)
                return 0;
            if (allNescesearyDataExist && isAllDataNotNull)
                return 2;
            return 1;
        }
        public string GetStatusNameForBlockInApplication(int blockId,int applicationId)
        {
            return GetStatusNameByStatusId(GetStatusIdForBlockInApplication(blockId, applicationId));
        }
    }
}
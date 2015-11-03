using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using Antlr.Runtime.Misc;

namespace Competitions
{
    public class XMLExport
    {
         
    }
    struct TagToReplace
    {
        public int ReplaceType; //0 error // 1 Line // 2 Table
        public List<string> ReplacemantList;
        public XmlNode TagsNode;
    }
    class TagsToReplace
    {
        List<TagToReplace> _tagsToReplaces = new List<TagToReplace>();
        private string TableTag = "Table";
        private string LineTag = "Line";

        private char TagStart = 'Z';
        private char TagEnd = 'Z';
        private char TagMiddle = 'z';

        private int TagNameToTagReplaceType(string tagName)
        {
            if (tagName == LineTag)
            {
                return 1;
            }
            if (tagName == TableTag)
            {
                return 2;
            }
            return 0;
        }

        private List<XmlNode> _xmlNodeListWithTag = new List<XmlNode>();
        private List<TagToReplace> _xmlTagsToReplacesList = new List<TagToReplace>();
        private bool FillListWithNodesToReplaceByValue(XmlNodeList list, string nodeValue)
        {
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.Value != null)
                    {
                        if (node.Value.Contains(nodeValue))
                        {
                            _xmlNodeListWithTag.Add(node);
                        }
                    }
                    if (node.HasChildNodes)
                    {
                        FillListWithNodesToReplaceByValue(node.ChildNodes, nodeValue);
                    }
                }
            }
            return true;
        }
        private void FillListWithNodesWithAllTags(XmlDocument document)
        {
            FillListWithNodesToReplaceByValue(document.ChildNodes, TagStart + TableTag + TagMiddle);
            FillListWithNodesToReplaceByValue(document.ChildNodes, TagStart + LineTag + TagMiddle);
        }
        public List<TagToReplace> GetTagsToReplace(XmlDocument document)
        {
            FillListWithNodesWithAllTags(document);
            foreach (var currentNodeWithTag in _xmlNodeListWithTag)
            {
                TagToReplace newTagToReplace = new TagToReplace();
                string valueTmp = currentNodeWithTag.Value;
                if (valueTmp != null)
                {
                    if (valueTmp.Any())
                    {
                        valueTmp = valueTmp.Remove(0, 1);
                        valueTmp = valueTmp.Remove(valueTmp.Length - 1, 1);
                        string[] tagWithAttributes = valueTmp.Split(TagMiddle);
                        if (tagWithAttributes.Count() > 1)
                        {
                            int tagType = TagNameToTagReplaceType(tagWithAttributes[0]);
                            if (tagType > 0)
                            {
                                newTagToReplace.ReplaceType = tagType;
                                newTagToReplace.TagsNode = currentNodeWithTag;
                                List<string> tmpStringList = new List<string>();
                                for (int i = 1; i < tagWithAttributes.Count(); i++)
                                {
                                    tmpStringList.Add(tagWithAttributes[i]);
                                }
                                newTagToReplace.ReplacemantList = tmpStringList;
                                _xmlTagsToReplacesList.Add(newTagToReplace);
                            }
                        }
                    }
                }
            }
            return _xmlTagsToReplacesList;
        }

    }
    class CreateXmlTable
    {
        private string GetValueFromKnownRowByUniqueMark(zCollectedRowsTable currentRow, string uniqueName, int applicationId,int rowId)
        {
            zColumnTable currentColumn = GetColumnByUniqueMark(uniqueName, applicationId);
            if (currentColumn == null)
            {
                return "";
            }
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedDataTable currentCollectedData = (from a in competitionDataBase.zCollectedDataTable
                                                        where a.FK_CollectedRowsTable == currentRow.ID
                                                              && a.FK_ColumnTable == currentColumn.ID
                                                        select a).FirstOrDefault();
            if (currentCollectedData == null)
            {
                return "";
            }

            DataProcess dataProcess = new DataProcess();
            dataProcess.ClearTotalUp();
            int dType = (int)currentColumn.DataType;

            if (dataProcess.IsCellReadWrite(dType))
            {
                return dataProcess.GetReadWriteString(currentColumn, currentCollectedData);
            }

            if (dataProcess.IsCellCheckBox(dType))
            {
                if (dataProcess.GetBoolDataValue(currentColumn, currentCollectedData))
                {
                    return "☑";
                }
                else
                {
                    return "☐"; 
                }
            }

            if (dataProcess.IsCellDate(dType))
            {
                return dataProcess.GetDateDataValue(currentColumn, currentCollectedData).ToString();
            }

            if (dataProcess.IsCellReadOnly(dType))
            {
                return dataProcess.GetReadOnlyString(currentColumn, currentCollectedData, applicationId, currentRow.ID, rowId);
            }
            if (dataProcess.IsCellDropDown(dType))
            {
                return dataProcess.GetDropDownSelectedValueString(currentColumn, currentCollectedData, applicationId, currentRow.ID);
            }
            if (dataProcess.IsCellFileUpload(dType))
            {
                if (dataProcess.GetIsFileConnected(currentCollectedData))
                {
                    return "☑";
                }
                else
                {
                    return "☐"; 
                }
            }
            
            
            return "";
        }
        private List<string> GetListWithDataFromRowByUnique(zCollectedRowsTable currentRow, List<string> columnsUniqueNames, int applicationId,int rowId)
        {
            List<string> newValueRowList = new List<string>();
            foreach (string uniqueName in columnsUniqueNames)
            {
                newValueRowList.Add(GetValueFromKnownRowByUniqueMark(currentRow, uniqueName, applicationId,rowId));
            }
            return newValueRowList;
        }
        private zColumnTable GetColumnByUniqueMark(string mark, int applicationId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zColumnTable> currentColumnList = (from a in competitionDataBase.zColumnTable
                                                    where a.Active == true
                                                    && a.UniqueMark.Length > 2
                                                    join b in competitionDataBase.zSectionTable
                                                    on a.FK_SectionTable equals b.ID
                                                    where b.Active == true
                                                    join c in competitionDataBase.zApplicationTable
                                                    on b.FK_CompetitionsTable equals c.FK_CompetitionTable
                                                    where c.Active == true
                                                    && c.ID == applicationId
                                                    && a.UniqueMark == mark
                                                    select a).Distinct().ToList();
            if (currentColumnList.Count != 1)
            {
                return null;//или такого ключа нет или их несколько, это ошибка
            }
            return currentColumnList[0];
        }
        private List<zCollectedRowsTable> GetRowsThatHaveColumn(zColumnTable firstColumn, int applicationId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedRowsTable> currentCollecterRowsTable =
                                                    (from a in competitionDataBase.zCollectedRowsTable
                                                     where a.Active == true
                                                     && a.FK_ApplicationTable == applicationId
                                                     join b in competitionDataBase.zCollectedDataTable
                                                         on a.ID equals b.FK_CollectedRowsTable
                                                     where b.Active == true
                                                     && b.FK_ColumnTable == firstColumn.ID
                                                     select a).Distinct().ToList();
            if (currentCollecterRowsTable.Count < 1)
            {
                return null;//нет строк
            }
            return currentCollecterRowsTable;
        }
        private List<string> GetColmnNamesForNestedList(List<string> columnsUniqueNames, int applicationId)
        {
            List<string> columnNames = new List<string>();
            foreach (string currentUnique in columnsUniqueNames)
            {
                zColumnTable currentColumn = GetColumnByUniqueMark(currentUnique, applicationId);
                //CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                if (currentColumn != null)
                    columnNames.Add(currentColumn.Name);
                else
                    columnNames.Add("Без названия");
            }
            return columnNames;
        }
        private bool IsColumnTotalUpByUniqueMark(string uniqueMark, int applicationId)
        {
            zColumnTable myColumn = GetColumnByUniqueMark (uniqueMark, applicationId);
            if (myColumn==null)
                return false;
            if (myColumn.TotalUp == true)
                return true;
            return false;
        }
        private List<int> GetToTotalUpColumnIdList(List<string> columnsUniqueNames,int applicationId)
        {
            List<int> toTotalUp = new List<int>();
            int i = 0;
            foreach (string currentColumnMark in columnsUniqueNames)
            {
                if (IsColumnTotalUpByUniqueMark(currentColumnMark,applicationId))
                toTotalUp.Add(i);
                i++;
            }
            return toTotalUp;
        }
        public List<string> GetStringListByColumnIdNNestedList(List<List<string>> nestedList, int columnId)
        {
            List<string> newStringList = new List<string>();
            foreach (List<string> stringList in nestedList)
            {
                newStringList.Add(stringList[columnId]);
            }
            return newStringList;
        }
        public string GetTotalUpValue(List<string> valuesList)
        {
            double sumValue = 0;
            for (int i = 1; i < valuesList.Count; i++)
            {
                sumValue += Convert.ToDouble(valuesList[i]);
            }
            return sumValue.ToString();
        }
        private List<string> GetTotalUpStringList(List<List<string>> nestedList, List<int> columnIdsToTotalUp)
        {
            List<string> newStringList = new List<string>();
            int columnCount = nestedList[0].Count;
            for (int i = 0; i < columnCount; i++)
            {
                if (columnIdsToTotalUp.Contains(i))
                {
                    newStringList.Add(GetTotalUpValue(GetStringListByColumnIdNNestedList(nestedList,i)));
                }
                else
                {
                    newStringList.Add("");
                }               
            }
            return newStringList;
        }
        private List<List<string>> GetNestedDataList(List<string> columnsUniqueNames, int applicationId)
        {
            List<List<string>> newNestedList = new List<List<string>>();
            //по первому марку найдем список строк в базе
            zColumnTable firstColumnInUniques = GetColumnByUniqueMark(columnsUniqueNames[0], applicationId);
            if (firstColumnInUniques == null)
            {
                return null;
            }
            List<zCollectedRowsTable> rowsList = GetRowsThatHaveColumn(firstColumnInUniques, applicationId);
            
            newNestedList.Add(GetColmnNamesForNestedList(columnsUniqueNames, applicationId));
            int i = 1;
            foreach (zCollectedRowsTable currentRow in rowsList)
            {
                newNestedList.Add(GetListWithDataFromRowByUnique(currentRow, columnsUniqueNames, applicationId,i));
                i++;
            }

            List<int> toTotalUp = GetToTotalUpColumnIdList(columnsUniqueNames, applicationId);
            if (toTotalUp.Any())
            {
                newNestedList.Add(GetTotalUpStringList(newNestedList, toTotalUp));
            }
            return newNestedList;
        }
        public XmlNode GetXmlTable(XmlDocument document, TagToReplace currentTagToReplace, int applicationId)
        {
            XmlTableCreate xmlTableCreate = new XmlTableCreate();
            List<List<string>> newNestedList = GetNestedDataList(currentTagToReplace.ReplacemantList, applicationId);
            if (newNestedList == null)
                return null;
            XmlNode newXmlTableNode = xmlTableCreate.GetXmlTable(document, newNestedList,false);
            return newXmlTableNode;
        }
    }
    public class CreateXmlFile
    {
        public string ConvertedFileExtension;
        public string ConvertedFilePath;
        private string FindValue(zColumnTable column, int applicationId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();

            List<zCollectedDataTable> collectedDataList = (from a in competitionDataBase.zCollectedDataTable
                                                           where a.FK_ColumnTable == column.ID
                                                                 && a.Active == true
                                                           join b in competitionDataBase.zCollectedRowsTable
                                                               on a.FK_CollectedRowsTable equals b.ID
                                                           where b.Active == true
                                                                 && b.FK_ApplicationTable == applicationId
                                                           select a).Distinct().ToList();

            if (collectedDataList.Count == 1)
            {
                if (dataType.IsDataTypeText(column.DataType))
                {
                    return collectedDataList[0].ValueText;
                }
            }

            return "NULL";
        }
        private zColumnTable FindColumnWithUniqueMark(int applicationId, string uniqueMark)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zColumnTable currentColumn = (from a in competitionDataBase.zColumnTable
                                          where a.Active == true
                                          && a.UniqueMark == uniqueMark
                                          join b in competitionDataBase.zSectionTable
                                          on a.FK_SectionTable equals b.ID
                                          where b.Active == true
                                          join c in competitionDataBase.zApplicationTable
                                          on b.FK_CompetitionsTable equals c.FK_CompetitionTable
                                          where c.Active == true
                                          && c.ID == applicationId
                                          select a).FirstOrDefault();

            return currentColumn;
        }
        private XmlNode FindNode(XmlNodeList list, string nodeName)
        {
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.Name.Equals(nodeName)) return node;
                    if (node.HasChildNodes)
                    {
                        XmlNode nodeFound = FindNode(node.ChildNodes, nodeName);
                        if (nodeFound != null)
                            return nodeFound;
                    }
                }
            }
            return null;
        }
        private XmlNode FindNodeByValue(XmlNodeList list, string nodeValue)
        {
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.Value != null)
                    {
                        if (node.Value.Equals(nodeValue)) return node;
                    }
                    if (node.HasChildNodes)
                    {
                        XmlNode nodeFound = FindNodeByValue(node.ChildNodes, nodeValue);
                        if (nodeFound != null)
                            return nodeFound;
                    }
                }
            }
            return null;
        }
        private XmlNode FindAfterParentNode(XmlNode parentNode, XmlNode childNodeWithValue)
        {
            XmlNode currentNode = childNodeWithValue;
            for (; ; )
            {
                if (currentNode.ParentNode == parentNode)
                {
                    return currentNode;
                }
                else
                {
                    currentNode = currentNode.ParentNode;
                }
            }
        }
        public bool CreateDocument(string templatePath, string newFilePath, int applicationId,int documentType)
        {
            #region создаем экземпляры нужных классов
            XmlTableCreate xmlTableCreate = new XmlTableCreate();
            #endregion
            #region открываем шаблон
            string xmlFile = File.ReadAllText(templatePath);
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlFile);
            #endregion
            #region старый вариант
            /*
            List<zColumnTable> columnListWithUniqueMark = FindColumnsWithUniqueMarkExist(applicationId);
            foreach (zColumnTable currentColumn in columnListWithUniqueMark)
            {
                xmlFile = xmlFile.Replace(currentColumn.UniqueMark, FindValue(currentColumn, applicationId));
            }*/
            #endregion
            XmlNode sectNode = FindNode(document.ChildNodes, "wx:sect");
            //нам нужен список того в файле что нужно заменить)
            // пока что 2 вхождения ищем
            //1) #Table* заменяем весь ноде после секции 
            //2) #Line*  заменяем только значение нода
            TagsToReplace tagsToReplaceClass = new TagsToReplace();
            CreateXmlTable createXmlTableClass = new CreateXmlTable();
            List<TagToReplace> tagsToReplaces = tagsToReplaceClass.GetTagsToReplace(document);
            //мы получили список того что нужно заменить
            #region поочереди заменяем
            foreach (TagToReplace currentTagToReplace in tagsToReplaces)
            {
                if (currentTagToReplace.ReplaceType == 2)
                {
                    XmlNode newXmlNode = createXmlTableClass.GetXmlTable(document, currentTagToReplace, applicationId);
                    if (newXmlNode != null)
                    {
                        document.ImportNode(newXmlNode, true);
                        sectNode.AppendChild(newXmlNode);
                        XmlNode lastNode = FindNodeByValue(sectNode.ChildNodes, currentTagToReplace.TagsNode.OuterXml);
                        XmlNode nodeToReplace = FindAfterParentNode(sectNode, lastNode);
                        if (nodeToReplace != null)
                        {
                            sectNode.ReplaceChild(newXmlNode, nodeToReplace);
                        }
                    }
                    else
                    {
                        XmlNode childNode = FindNodeByValue(document.ChildNodes, currentTagToReplace.TagsNode.Value);
                        childNode.Value = "Данные не внесены";
                    }
                }
                else if (currentTagToReplace.ReplaceType == 1)
                {
                    XmlNode childNode = FindNodeByValue(document.ChildNodes, "ZLinez" + currentTagToReplace.ReplacemantList[0] + "Z");
                    zColumnTable currentColumn = FindColumnWithUniqueMark(applicationId, currentTagToReplace.ReplacemantList[0]);
                    if (currentColumn != null)
                    {
                        childNode.Value = FindValue(currentColumn, applicationId);
                    }
                    if (childNode == null)
                    {
                        //childNode.Value = "Значение отсутствует";
                    }
                }
            }
            #endregion
            #region подчищаем и сохраняем в файл
            string newXmlFile = document.OuterXml;
            newXmlFile = newXmlFile.Replace("xmlns:w=\"w\"", "").Replace("xmlns:wx=\"wx\"", "").Replace("xmlns:wsp=\"wsp\"", "");
            File.WriteAllText(newFilePath, newXmlFile);
            #endregion
            #region конвертируем его в другой формат
                Converter converter = new Converter();
                converter.Convert(newFilePath, newFilePath, documentType);
            ConvertedFileExtension = converter.ConvertedFilaExtension;
            ConvertedFilePath = converter.ConvertedFilePath;
            #endregion
            return true;
        }
        public bool CreateExpertDocument(string templateFilePath, string newFilePath, int applicationId, int userId)
        {
            //открываем шаблон       
            string xmlFile = File.ReadAllText(templateFilePath);
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlFile);

            TagsToReplace tagsToReplaceClass = new TagsToReplace();
            List<TagToReplace> tagsToReplaces = tagsToReplaceClass.GetTagsToReplace(document);
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var idshape = (from a in competitionDataBase.zExpertPoints where a.Active == true select a.ID).ToList();
            foreach (var id in idshape)
            {
                XmlNode childNode = FindNodeByValue(document.ChildNodes, "ZLinez" + id + "Z");
                if (childNode != null)
                {
                    childNode.Value = ((from a in competitionDataBase.zExpertPointsValue 
                                        where a.FK_ExpertPoints == id
                                        && a.FK_ApplicationTable== applicationId
                                        && a.FK_ExpertsTable == userId
                                        select a.Value).FirstOrDefault()).ToString();
                }
                 
                if (childNode == null) 
                {
                    //childNode.Value = "Значение отсутствует";
                }
            }
            //подчищаем и сохраняем в файл
            string newXmlFile = document.OuterXml;             
            File.WriteAllText(newFilePath, newXmlFile);
            return true;
        }
    }
}
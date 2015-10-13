using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;

namespace Competitions.User
{


    public partial class UserMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var userIdtmp = Session["UserID"];
                    if (userIdtmp == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    int userId = (int) userIdtmp;

                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;

                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Number", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Budjet", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("StartDate", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("EndDate", typeof (string)));
                    
                    List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                        where a.Active == true
                        select a).ToList();

                    foreach (zCompetitionsTable currentCompetition in competitionsList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentCompetition.ID;
                        dataRow["Name"] = currentCompetition.Name;
                        dataRow["Number"] = currentCompetition.Number;
                        dataRow["Budjet"] = Convert.ToInt32(currentCompetition.Budjet);
                        dataRow["StartDate"] = currentCompetition.StartDate.ToString().Split(' ')[0];
                        dataRow["EndDate"] = currentCompetition.EndDate.ToString().Split(' ')[0];

                        dataTable.Rows.Add(dataRow);
                    }
                    MainGV.DataSource = dataTable;
                    MainGV.DataBind();
                }
                ////////////////////////////////////////////////////////////////////////////////////////
                {                  
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof (string)));

                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId && a.Sended == false  && a.Active == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID where b.Active == true
                                                               && b.OpenForApplications == true  
                                                               select a).Distinct().ToList();

                    foreach (zApplicationTable currentApplication in applicationList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentApplication.ID;
                        dataRow["Name"] = currentApplication.Name;
                        dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                            where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                            select a.Name).FirstOrDefault();
                        dataTable.Rows.Add(dataRow);
                    }
                    ApplicationGV.DataSource = dataTable;
                    ApplicationGV.DataBind();
                }
                //////////////////////////////////////////////////////////
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("SendedDate", typeof(string)));
                    
                    List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                               where a.FK_UsersTable == userId  && a.Active == true && a.Sended == true
                                                               join b in competitionDataBase.zCompetitionsTable
                                                               on a.FK_CompetitionTable equals b.ID
                                                               where b.Active == true && b.OpenForApplications == true
                                                               select a).Distinct().ToList();

                    foreach (zApplicationTable currentApplication in applicationList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentApplication.ID;
                        dataRow["Name"] = currentApplication.Name;
                        dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                                                      where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                                                      select a.Name).FirstOrDefault();
                        if (currentApplication.SendedDataTime == null)
                        {
                            dataRow["SendedDate"] = "Не отправлялось на рассмотрение";
                        }
                        else
                        {
                            dataRow["SendedDate"] = currentApplication.SendedDataTime.ToString().Split(' ')[0];
                        }          
                        dataTable.Rows.Add(dataRow);
                    }
                    ArchiveApplicationGV.DataSource = dataTable;
                    ArchiveApplicationGV.DataBind(); 
                }
            }
        }
        protected void MyApplication_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ChooseApplication.aspx");
        }
        protected void NewApplication_Click1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (CompetitionDataContext newBid = new CompetitionDataContext())
                {
                    Session["ID_Konkurs"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/User/ApplicationCreateEdit.aspx");
                }
            }
        }
        protected void NewApplication_Click(object sender, EventArgs e)
        {
            Session["ApplicationID"] = 0;
            Response.Redirect("ApplicationCreateEdit.aspx");
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
                            valueTmp = valueTmp.Remove(0,1);
                            valueTmp = valueTmp.Remove(valueTmp.Length - 1, 1);
                            string [] tagWithAttributes = valueTmp.Split(TagMiddle);
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
            private string GetSumValueFromKAllRowsByUniqueMark(string uniqueName, int applicationId)
            {
                List<zCollectedRowsTable> rowsList = GetRowsListByUniqueColumnName(uniqueName, applicationId);
                double sumValue = 0;
                foreach (zCollectedRowsTable currentRow in rowsList)
                {

                    string tmpStringToMakeSum = GetValueFromKnownRowByUniqueMark(currentRow, uniqueName, applicationId,
                        0);
                    try
                    {
                        sumValue += Convert.ToDouble(tmpStringToMakeSum);
                    }
                    catch (Exception)
                    {                   
                        throw;
                    }
                    
                }
                //newValueRowList.Add(GetValueFromKnownRowByUniqueMark(currentRow, uniqueName, applicationId, rowId));
                return sumValue.ToString();
            }
            private string GetValueFromKnownRowByUniqueMark(zCollectedRowsTable currentRow, string uniqueName,int applicationId,int rowId)
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
                    return dataProcess.GetBoolDataValue(currentColumn, currentCollectedData).ToString();
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
                    return dataProcess.GetDropDownSelectedValueString(currentColumn, currentCollectedData, applicationId,
                        currentRow.ID);
                }

                return "";
            }
            private List<string> GetListWithDataFromRowByUnique(zCollectedRowsTable currentRow, List<string> columnsUniqueNames,int applicationId,int rowId)
            {
                List<string> newValueRowList = new List<string>();
                foreach (string uniqueName in columnsUniqueNames)
                {
                    newValueRowList.Add(GetValueFromKnownRowByUniqueMark(currentRow, uniqueName, applicationId, rowId));
                }
                return newValueRowList;
            }

            private List<string> GetListWithSumByByUique(List<string> columnsUniqueNames, int applicationId)
            {
                List<string> newValueRowList = new List<string>();
                foreach (string uniqueName in columnsUniqueNames)
                {
                    List<string> newTmpList = new List<string>();
                    newTmpList.Add(uniqueName);
                    if (DoesTagHasTotalUpValue(newTmpList, applicationId))
                    {
                        newValueRowList.Add(GetSumValueFromKAllRowsByUniqueMark(uniqueName, applicationId));
                    }
                    else
                    {
                        newValueRowList.Add("");
                    }
                }
                return newValueRowList;
            }

            private zColumnTable GetColumnByUniqueMark (string mark,int applicationId)
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
            private List<string> GetColmnNamesForNestedList(List<string> columnsUniqueNames ,int applicationId)
            {
                List<string> columnNames = new List<string>();
                foreach (string currentUnique in columnsUniqueNames)
                {
                    zColumnTable currentColumn = GetColumnByUniqueMark(currentUnique, applicationId);
                    //CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                    if (currentColumn!=null)
                        columnNames.Add(currentColumn.Name);
                    else
                     columnNames.Add("Без названия");
                }
                return columnNames;
            }

            private List<zCollectedRowsTable> GetRowsListByUniqueColumnName(string columnUniqueName,int applicationId)
            {
                //по первому марку найдем список строк в базе
                zColumnTable firstColumnInUniques = GetColumnByUniqueMark(columnUniqueName, applicationId);
                List<zCollectedRowsTable> rowsList = GetRowsThatHaveColumn(firstColumnInUniques, applicationId);
                return rowsList;
            }
            private List<List<string>> GetNestedDataList(List<string> columnsUniqueNames, int applicationId, bool isLastLineSum)
            {
                List<List<string>> newNestedList = new List<List<string>>();
                List<zCollectedRowsTable> rowsList = GetRowsListByUniqueColumnName(columnsUniqueNames[0], applicationId);
                
                newNestedList.Add(GetColmnNamesForNestedList(columnsUniqueNames, applicationId));
                int i = 0;
                foreach (zCollectedRowsTable currentRow in rowsList)
                {
                    i++;
                    newNestedList.Add(GetListWithDataFromRowByUnique(currentRow, columnsUniqueNames,applicationId,i));
                }
                if (isLastLineSum)
                    newNestedList.Add(GetListWithSumByByUique(columnsUniqueNames, applicationId));

                return newNestedList;
            }
            private bool DoesTagHasTotalUpValue(List<String> replacemantList,int applicationId)
            {
                foreach (string currentMark in replacemantList)
                {
                    zColumnTable column = GetColumnByUniqueMark(currentMark, applicationId);
                    if (column != null)
                    {
                        if (column.TotalUp == true)
                            return true;
                    }
                }              
                return false;
            }
            public XmlNode GetXmlTable(XmlDocument document, TagToReplace currentTagToReplace,int applicationId)
            {
                XmlTableCreate xmlTableCreate = new XmlTableCreate();
                bool isLastLineSum = DoesTagHasTotalUpValue(currentTagToReplace.ReplacemantList, applicationId);
                List<List<string>> newNestedList = GetNestedDataList(currentTagToReplace.ReplacemantList, applicationId, isLastLineSum);
                XmlNode newXmlTableNode = xmlTableCreate.GetXmlTable(document, newNestedList,isLastLineSum);
                return newXmlTableNode;
            }
        }       
        protected void FillButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["ApplicationID"] = iD;
                Response.Redirect("ChooseApplicationAction.aspx");
            }
        }
        protected void SendButtonClick(object sender, EventArgs e)
        {
             Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                    where a.Active == true
                          && a.ID == iD
                    select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    currentApplication.Sended = true;
                    currentApplication.SendedDataTime = DateTime.Now;
                    competitionDataBase.SubmitChanges();
                }
            }
            Response.Redirect("UserMainPage.aspx");
        }
        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

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
        private zColumnTable FindColumnWithUniqueMark(int applicationId,string uniqueMark)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zColumnTable currentColumn = (from a in  competitionDataBase.zColumnTable
                                 where a.Active == true
                                 && a.UniqueMark == uniqueMark
                                 join b in competitionDataBase.zSectionTable
                                 on a.FK_SectionTable equals  b.ID
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
            for (;;)
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
        private bool CreateDocument(string templatePath, string newFilePath , int applicationId)
        {
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
                    document.ImportNode(newXmlNode, true);
                    sectNode.AppendChild(newXmlNode);
                    XmlNode lastNode = FindNodeByValue(sectNode.ChildNodes, currentTagToReplace.TagsNode.OuterXml);
                    XmlNode nodeToReplace = FindAfterParentNode(sectNode, lastNode);
                    if (nodeToReplace!=null)
                    {
                        sectNode.ReplaceChild(newXmlNode, nodeToReplace);
                    }
                }
                else if (currentTagToReplace.ReplaceType == 1)
                {
                    XmlNode childNode = FindNodeByValue(document.ChildNodes, "ZLinez" + currentTagToReplace.ReplacemantList[0]+"Z");
                    zColumnTable currentColumn = FindColumnWithUniqueMark(applicationId,currentTagToReplace.ReplacemantList[0]);
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
            newXmlFile=newXmlFile.Replace("xmlns:w=\"w\"", "").Replace("xmlns:wx=\"wx\"", "").Replace("xmlns:wsp=\"wsp\"", "");
            File.WriteAllText(newFilePath,newXmlFile);
            #endregion
            return true;
        }
        protected void GetDocButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                
                zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                                                         join b in competitionDataBase.zApplicationTable
                                                         on a.ID equals b.FK_CompetitionTable
                    where b.ID == iD
                    select a).FirstOrDefault();
                if (currentCompetition != null)
                {
                    var userIdtmp = Session["UserID"];
                    if (userIdtmp == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    int userId = (int)userIdtmp;

                    if (currentCompetition.TemplateDocName!=null)
                    {
                        if (currentCompetition.TemplateDocName.Any())
                        {
                            string templateFilePath = Server.MapPath("~/documents/templates") + "\\" + currentCompetition.ID.ToString() + "\\" + currentCompetition.TemplateDocName;
                            string newFileName = DateTime.Now.ToString() +" "+ currentCompetition.TemplateDocName;
                            newFileName = newFileName.Replace(":", "_");
                            string newFileDirectory = Server.MapPath("~/documents/generated") + "\\" + userId.ToString();
                            string newFilePath =  newFileDirectory + "\\" + newFileName;

                            Directory.CreateDirectory(newFileDirectory);
                            CreateXmlFile createXmlFile = new CreateXmlFile();
                            createXmlFile.CreateDocument(templateFilePath, newFilePath, iD);
                                                      
                            HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + currentCompetition.TemplateDocName);
                            HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(newFilePath));
                            HttpContext.Current.Response.End();
                            Response.End();
                        }
                    }
                }
            }
        }
        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }
        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }
        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
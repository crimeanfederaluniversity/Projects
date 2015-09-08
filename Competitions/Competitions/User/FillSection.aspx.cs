using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.User
{
    public partial class FillSection : System.Web.UI.Page
    {
        private int columnCount = 10;
        private bool permitAddRow = true;
        private bool permitDeleteRow = true;
        public zCollectedRowsTable CreateNewRow(int applicationId,int sectionId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedRowsTable newRow= new zCollectedRowsTable();
            newRow.Active = true;
            newRow.FK_ApplicationTable = applicationId;
            newRow.FK_SectionTable = sectionId;
            competitionDataBase.zCollectedRowsTable.InsertOnSubmit(newRow);
            competitionDataBase.SubmitChanges();
            return newRow;
        }
        public zCollectedDataTable CreateNewData(int fkRow,int fkColumn)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedDataTable newData = new zCollectedDataTable();
            newData.Active = true;
            newData.FK_CollectedRowsTable = fkRow;
            newData.FK_ColumnTable = fkColumn;
            newData.CreateDateTime = DateTime.Now;
            newData.LastChangeDateTime = DateTime.Now;
            competitionDataBase.zCollectedDataTable.InsertOnSubmit(newData);
            competitionDataBase.SubmitChanges();
            return newData;
        }
        public bool AddRowWithConstant(int constantListId,int applicationId,int sectionId,int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            //берем список того что у нас должно быть
            //проходимся по нему
            //если такой есть то не трогаем
            //если такого нет то создаем
            List<zCollectedDataTable> mustBeDataList = (from a in competitionDataBase.zCollectedDataTable
                                                        where a.Active == true
                                                              && a.FK_ConstantListTable == constantListId
                                                        select a).ToList();
            foreach (zCollectedDataTable currentMustBeData in mustBeDataList)
            {
                zCollectedDataTable currentCollectedDataTable = (from a in competitionDataBase.zCollectedDataTable
                    join b in competitionDataBase.zCollectedRowsTable
                        on a.FK_CollectedRowsTable equals b.ID
                    where b.Active == true
                          && a.Active == true
                          && b.FK_ApplicationTable == applicationId
                          && b.FK_SectionTable == sectionId
                          && a.ValueFK_CollectedDataTable == currentMustBeData.ID
                    select a).FirstOrDefault();
                if (currentCollectedDataTable == null)
                {
                    //noDataNoRow
                    //создадим строку
                    zCollectedRowsTable newRow = CreateNewRow(applicationId, sectionId);
                    zCollectedDataTable newData = CreateNewData(newRow.ID, columnId);                   
                    zCollectedDataTable newDataChange = (from a in competitionDataBase.zCollectedDataTable
                        where a.ID == newData.ID
                        select a).FirstOrDefault();
                    newDataChange.ValueFK_CollectedDataTable = currentMustBeData.ID;
                    competitionDataBase.SubmitChanges();
                }
            }
            return true;
        }
        public bool DeleteRowsWithDisabledConstant(int constantListId, int applicationId, int sectionId, int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> existingCollectedDataTableList = (from a in competitionDataBase.zCollectedDataTable
                join b in competitionDataBase.zCollectedRowsTable
                    on a.FK_CollectedRowsTable equals b.ID
                where b.Active == true
                      && a.Active == true
                      && b.FK_ApplicationTable == applicationId
                      && b.FK_SectionTable == sectionId
                select a).ToList();

            foreach (zCollectedDataTable currentExistingData in existingCollectedDataTableList)
            {
                zCollectedDataTable constantData = (from a in competitionDataBase.zCollectedDataTable
                    where a.Active == true
                          && a.FK_ConstantListTable == constantListId
                          && a.ID == currentExistingData.ValueFK_CollectedDataTable
                    select a).FirstOrDefault();
                if (constantData == null)
                {
                    currentExistingData.Active = false;
                    zCollectedRowsTable rowToDelete = (from a in competitionDataBase.zCollectedRowsTable
                        where a.ID == currentExistingData.FK_CollectedRowsTable
                        select a).FirstOrDefault();
                    rowToDelete.Active = false;
                    competitionDataBase.SubmitChanges();
                }
            }
          
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            if (!Page.IsPostBack)
            {
                #region SETUP
                #region session
                var sessionParam1 = Session["ApplicationID"];
                var sessionParam2 = Session["SectionID"];
                var userIdParam = Session["UserID"];

                if ((sessionParam1 == null) || (sessionParam2 == null) || (userIdParam == null))
                {
                    //error
                    Response.Redirect("ChooseSection.aspx");
                }

                int applicationId = Convert.ToInt32(sessionParam1);
                int sectionId = Convert.ToInt32(sessionParam2);
                int userId = Convert.ToInt32(userIdParam);
                #endregion
                //создадим большой dataTable
                #region dataTableCreate
                DataTable dataTable = new DataTable();
                for (int i = 0; i < 10; i++)
                {
                    dataTable.Columns.Add(new DataColumn("ID" + i.ToString(), typeof(string))); // тут будет id collectedData

                    dataTable.Columns.Add(new DataColumn("ReadOnlyLablelVisible" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("ReadOnlyLablelValue" + i.ToString(), typeof(string)));

                    dataTable.Columns.Add(new DataColumn("EditTextBoxVisible" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("EditTextBoxValue" + i.ToString(), typeof(string)));

                    dataTable.Columns.Add(new DataColumn("EditBoolCheckBoxVisible" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("EditBoolCheckBoxValue" + i.ToString(), typeof(bool)));

                    dataTable.Columns.Add(new DataColumn("ChooseOnlyDropDownVisible" + i.ToString(), typeof(bool)));
                   // dataTable.Columns.Add(new DataColumn("ChooseOnlyDropDownValue" + i.ToString(), typeof(ListItemCollection)));

                    dataTable.Columns.Add(new DataColumn("ChooseDateCalendarVisible" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("ChooseDateCalendarValue" + i.ToString(), typeof(DateTime)));
                }

                #endregion
                DataType dataType = new DataType();
                //нам нужно узнать как выглядит таблица и создать её
                //у каждой сектии свой тип таблиц сначала возьмем текующую секцию
                zSectionTable currentSection = (from a in competitionDataBase.zSectionTable
                                                where a.ID == sectionId
                                                select a).FirstOrDefault();
                //найдем все колонки привязанные к этой секции
                List<zColumnTable> columnInSectionList = (from a in competitionDataBase.zColumnTable
                                                          where a.Active == true
                                                                && a.FK_SectionTable == sectionId
                                                          select a).ToList();

                #region constRosManage

                foreach (zColumnTable currentColumn in columnInSectionList)
                {
                    if (dataType.IsDataTypeConstantNecessarilyShow(currentColumn.DataType))
                    {
                        permitAddRow = false;
                        permitDeleteRow = false;


                       // DeleteRowsWithDisabledConstant((int)currentColumn.FK_ConstantListsTable, applicationId, sectionId,
                       //     currentColumn.ID);
                        AddRowWithConstant((int) currentColumn.FK_ConstantListsTable, applicationId, sectionId,
                            currentColumn.ID);
                    }
                }

                #endregion

                //найдем теперь все строки созданные и заполненные пользователем в этой секции этой заявки
                List<zCollectedRowsTable> currentRowsList = (from a in competitionDataBase.zCollectedRowsTable
                                                             where a.Active == true
                                                                   && a.FK_ApplicationTable == applicationId
                                                                   && a.FK_SectionTable == sectionId
                                                             select a).ToList();
                //если человек заходит в первый раз то может и не быть строк, надо создать одну
                if (currentRowsList.Count == 0)
                {
                    zCollectedRowsTable newRow = new zCollectedRowsTable();
                    newRow.Active = true;
                    newRow.FK_SectionTable = sectionId;
                    newRow.FK_ApplicationTable = applicationId;
                    competitionDataBase.zCollectedRowsTable.InsertOnSubmit(newRow);
                    competitionDataBase.SubmitChanges();
                    currentRowsList.Add(newRow);
                }

                //теперь есть как минимум одна строка :)

                //ограничим кол-во строк в таблицах
                
                #endregion
                //пройдемся по каждой строке
                int iterator = 0;
                foreach (zCollectedRowsTable currentRow in currentRowsList)
                {
                    iterator++;
                    //в каждой строке есть одна или больше колонок
                    //пройдемся по каждой колонке
                    
                    DataRow newDataRow = dataTable.NewRow();
                    //foreach (zColumnTable currentColumn in columnInSectionList)
                    for (int i = 0; i < 10; i++)
                    {
                        #region записываем стандартный значения во все поля
                        newDataRow["ID" + i.ToString()] = 0;
                        newDataRow["ReadOnlyLablelVisible" + i.ToString()] = false;
                        newDataRow["ReadOnlyLablelValue" + i.ToString()] = "";
                        newDataRow["EditTextBoxVisible" + i.ToString()] = false;
                        newDataRow["EditTextBoxValue" + i.ToString()] = "";
                        newDataRow["EditBoolCheckBoxVisible" + i.ToString()] = false;
                        newDataRow["EditBoolCheckBoxValue" + i.ToString()] = false;
                        newDataRow["ChooseOnlyDropDownVisible" + i.ToString()] = false;
                        //newDataRow["ChooseOnlyDropDownValue" + i.ToString()] = null;
                        newDataRow["ChooseDateCalendarVisible" + i.ToString()] = false;
                        newDataRow["ChooseDateCalendarValue" + i.ToString()] = DateTime.Now;
                        #endregion
                        #region записываем имеющиеся данные в нужные поля
                        if (i < columnInSectionList.Count)
                        {
                            zColumnTable currentColumn = columnInSectionList[i];
                            //надо найти значение в CollectedData
                            zCollectedDataTable currentCollectedData =
                                (from a in competitionDataBase.zCollectedDataTable
                                 where a.Active == true
                                       && a.FK_CollectedRowsTable == currentRow.ID
                                       && a.FK_ColumnTable == currentColumn.ID
                                 select a).FirstOrDefault();
                            //если такого поля не существует значит его надо создать
                            //потом можно будет перенести в отдельный класс если понадобится
                            if (currentCollectedData == null)
                            {
                                zCollectedDataTable newCollectedData = new zCollectedDataTable();
                                newCollectedData.Active = true;
                                newCollectedData.FK_CollectedRowsTable = currentRow.ID;
                                newCollectedData.FK_ColumnTable = currentColumn.ID;
                                newCollectedData.CreateDateTime = DateTime.Now;
                                newCollectedData.LastChangeDateTime = DateTime.Now;
                                competitionDataBase.zCollectedDataTable.InsertOnSubmit(newCollectedData);
                                competitionDataBase.SubmitChanges();
                                currentCollectedData = newCollectedData;
                            }
                            //теперь в currentCollectedData точно есть поле в таблице
                            //начнем заполнять DataTable
                            newDataRow["ID" + i.ToString()] = currentCollectedData.ID;
                            #region float
                            if (dataType.IsDataTypeFloat(currentColumn.DataType))
                            {
                                newDataRow["EditTextBoxVisible" + i.ToString()] = true;
                                newDataRow["EditTextBoxValue" + i.ToString()] = currentCollectedData.ValueDouble.ToString(); // currentCollectedData.ValueDouble;
                            }
                            #endregion
                            #region int
                            if (dataType.IsDataTypeInteger(currentColumn.DataType))
                            {
                                newDataRow["EditTextBoxVisible" + i.ToString()] = true;
                                newDataRow["EditTextBoxValue" + i.ToString()] = currentCollectedData.ValueInt.ToString(); // currentCollectedData.ValueInt;
                            }
                            #endregion
                            #region text
                            if (dataType.IsDataTypeText(currentColumn.DataType))
                            {
                                newDataRow["EditTextBoxVisible" + i.ToString()] = true;
                                newDataRow["EditTextBoxValue" + i.ToString()] = currentCollectedData.ValueText; // currentCollectedData.ValueText;
                            }
                            #endregion
                            #region bit
                            if (dataType.IsDataTypeBit(currentColumn.DataType))
                            {
                                newDataRow["EditBoolCheckBoxVisible" + i.ToString()] = true;
                                if (currentCollectedData.ValueBit == null)
                                {
                                    newDataRow["EditBoolCheckBoxValue" + i.ToString()] = false;
                                }
                                else
                                {
                                    newDataRow["EditBoolCheckBoxValue" + i.ToString()] = currentCollectedData.ValueBit;
                                }
                            }
                            #endregion
                            #region dropDown
                            if (dataType.IsDataTypeDropDown(currentColumn.DataType))
                            {
                                newDataRow["ChooseOnlyDropDownVisible" + i.ToString()] = true;
                            }
                            #endregion
                            #region constDropDown
                            if (dataType.IsDataTypeConstantDropDown(currentColumn.DataType))
                            {
                                newDataRow["ChooseOnlyDropDownVisible" + i.ToString()] = true;
                            }
                            #endregion
                            #region date
                            if (dataType.IsDataTypeDate(currentColumn.DataType))
                            {
                                newDataRow["ChooseDateCalendarVisible" + i.ToString()] = true;
                                if (currentCollectedData.ValueDataTime == null)
                                {
                                    newDataRow["ChooseDateCalendarValue" + i.ToString()] = DateTime.Today;                                  
                                }
                                else
                                {
                                    newDataRow["ChooseDateCalendarValue" + i.ToString()] =
                                        ((DateTime) currentCollectedData.ValueDataTime);
                                }
                            }
                            #endregion
                            #region iterator
                            if (dataType.IsDataTypeIterator(currentColumn.DataType))
                            {
                                newDataRow["ReadOnlyLablelVisible" + i.ToString()] = true;
                                newDataRow["ReadOnlyLablelValue" + i.ToString()] = (iterator).ToString(); 
                            }
                            #endregion
                            #region sum
                            if (dataType.IsDataTypeSum(currentColumn.DataType))
                            {
                                zColumnTable columnToSum = (from a in competitionDataBase.zColumnTable
                                    where a.ID == currentColumn.FK_ColumnTable
                                    select a).FirstOrDefault();
                                newDataRow["ReadOnlyLablelVisible" + i.ToString()] = true;
                                double AllSum = 0;
                                if (dataType.IsDataTypeInteger(columnToSum.DataType))
                                {
                                    newDataRow["ReadOnlyLablelValue" + i.ToString()] =
                                        (from a in competitionDataBase.zCollectedDataTable
                                            where a.FK_ColumnTable == columnToSum.ID
                                                  && a.Active == true
                                            join b in competitionDataBase.zCollectedRowsTable
                                                on a.FK_CollectedRowsTable equals b.ID
                                            where b.Active == true
                                                  && b.FK_ApplicationTable == applicationId
                                            select a.ValueInt).Distinct().Sum().ToString();
                                }
                                if (dataType.IsDataTypeFloat(columnToSum.DataType))
                                {
                                    newDataRow["ReadOnlyLablelValue" + i.ToString()] =
                                        (from a in competitionDataBase.zCollectedDataTable
                                         where a.FK_ColumnTable == columnToSum.ID
                                               && a.Active == true
                                         join b in competitionDataBase.zCollectedRowsTable
                                             on a.FK_CollectedRowsTable equals b.ID
                                         where b.Active == true
                                               && b.FK_ApplicationTable == applicationId
                                         select a.ValueDouble).Distinct().Sum().ToString();
                                }
                                if (dataType.IsDataTypeBit(columnToSum.DataType))
                                {
                                    newDataRow["ReadOnlyLablelValue" + i.ToString()] =
                                        (from a in competitionDataBase.zCollectedDataTable
                                         where a.FK_ColumnTable == columnToSum.ID
                                               && a.Active == true
                                         join b in competitionDataBase.zCollectedRowsTable
                                             on a.FK_CollectedRowsTable equals b.ID
                                         where b.Active == true
                                               && b.FK_ApplicationTable == applicationId
                                               && a.ValueBit == true
                                         select a).Distinct().Count().ToString();
                                }
                                                                   
                            }
                            #endregion
                            #region ConstantNecessarily
                            if (dataType.IsDataTypeConstantNecessarilyShow(currentColumn.DataType))
                            {
                                newDataRow["ReadOnlyLablelVisible" + i.ToString()] = true;
                                zCollectedDataTable getCollectedData =
                                    (from a in competitionDataBase.zCollectedDataTable
                                        where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                                        select a).FirstOrDefault();
                                newDataRow["ReadOnlyLablelValue" + i.ToString()] = getCollectedData.ValueText;
                            }
                           
                            #endregion
                        }
                        #endregion
                    }      
                    dataTable.Rows.Add(newDataRow);
                }
                #region DataBaseBind

                if (currentRowsList.Count >= currentSection.ColumnMaxCount)
                {
                    permitAddRow = false;
                }
                if (currentSection.ColumnMaxCount == 1)
                {
                    permitAddRow = false;
                    permitDeleteRow = false;
                }

                FillingGV.DataSource = dataTable;

                FillingGV.Columns[10].Visible = permitDeleteRow;
                AddRowButton.Visible = AddRowButton.Enabled = permitAddRow;

                for (int i = 0; i < columnInSectionList.Count; i++)
                {
                    FillingGV.Columns[i].HeaderText = columnInSectionList[i].Name;
                    FillingGV.Columns[i].Visible = true;
                }
                FillingGV.DataBind();
                #endregion
            }
        }
        protected void FillingGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                for (int i = 0; i <= columnCount; i++)
                {
                    {
                        Label labelId = e.Row.FindControl("ID" + i) as Label;
                        if (labelId != null)
                        {
                            if (labelId.Text != "0")
                            {
                                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                                zCollectedDataTable currentCollectedData =
                                    (from a in competitionDataBase.zCollectedDataTable
                                        where a.ID == Convert.ToInt32(labelId.Text)
                                        select a).FirstOrDefault();
                                if (currentCollectedData != null)
                                {
                                    zColumnTable currentColumn = (from a in competitionDataBase.zColumnTable
                                        where a.ID == currentCollectedData.FK_ColumnTable
                                        select a).FirstOrDefault();
                                    DataType dataType = new  DataType();

                                    #region session
                                    var sessionParam1 = Session["ApplicationID"];
                                    var sessionParam2 = Session["SectionID"];
                                    var userIdParam = Session["UserID"];

                                    if ((sessionParam1 == null) || (sessionParam2 == null) || (userIdParam == null))
                                    {
                                        //error
                                        Response.Redirect("ChooseSection.aspx");
                                    }

                                    int applicationId = Convert.ToInt32(sessionParam1);
                                    int sectionId = Convert.ToInt32(sessionParam2);
                                    int userId = Convert.ToInt32(userIdParam);
                                    #endregion
                                    #region dropDown
                                    if (dataType.IsDataTypeDropDown(currentColumn.DataType))
                                    {
                                        DropDownList currentDropDownListDownList = e.Row.FindControl("ChooseOnlyDropDown" + i) as DropDownList;
                                        
                                        if (currentDropDownListDownList != null)
                                        {
                                            zColumnTable DropDownItemsColumn =
                                                (from a in competitionDataBase.zColumnTable
                                                    where a.ID == currentColumn.FK_ColumnTable
                                                    && a.Active == true
                                                    select a).FirstOrDefault();

                                            List<zCollectedDataTable> collectedDataList =
                                                (from a in competitionDataBase.zCollectedDataTable
                                                    join b in competitionDataBase.zCollectedRowsTable
                                                        on a.FK_CollectedRowsTable equals b.ID
                                                    where b.FK_ApplicationTable == applicationId
                                                          && a.Active == true
                                                          && b.Active == true
                                                          && a.FK_ColumnTable == currentColumn.FK_ColumnTable
                                                    select a).Distinct().ToList();

                                            foreach (zCollectedDataTable tmpCollectedForDropDown in collectedDataList)
                                            {                                              
                                                #region вытаскиваем в зависимости от типа данных
                                                ListItem newListItem = new ListItem();
                                                if (currentCollectedData.ValueFK_CollectedDataTable ==
                                                    tmpCollectedForDropDown.ID)
                                                {
                                                    newListItem.Selected = true;
                                                }
                                                newListItem.Value = tmpCollectedForDropDown.ID.ToString();
                                                //---------------------------------------------------------------------------------------------------------------
                                                if (dataType.IsDataTypeBit(DropDownItemsColumn.DataType))
                                                {
                                                    newListItem.Text = tmpCollectedForDropDown.ValueBit.ToString();
                                                }
                                                //---------------------------------------------------------------------------------------------------------------
                                                if (dataType.IsDataTypeInteger(DropDownItemsColumn.DataType))
                                                {
                                                    newListItem.Text = tmpCollectedForDropDown.ValueInt.ToString();
                                                }
                                                //---------------------------------------------------------------------------------------------------------------
                                                if (dataType.IsDataTypeFloat(DropDownItemsColumn.DataType))
                                                {
                                                    newListItem.Text = tmpCollectedForDropDown.ValueDouble.ToString();
                                                }
                                                //---------------------------------------------------------------------------------------------------------------
                                                if (dataType.IsDataTypeText(DropDownItemsColumn.DataType))
                                                {
                                                    newListItem.Text = tmpCollectedForDropDown.ValueText;
                                                }
                                                //---------------------------------------------------------------------------------------------------------------
                                                if (dataType.IsDataTypeDate(DropDownItemsColumn.DataType))
                                                {
                                                    newListItem.Text = tmpCollectedForDropDown.ValueDataTime.ToString();
                                                }
                                                currentDropDownListDownList.Items.Add(newListItem);
                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion
                                    #region constDropDown
                                    if (dataType.IsDataTypeConstantDropDown(currentColumn.DataType))
                                    {
                                        DropDownList currentDropDownListDownList = e.Row.FindControl("ChooseOnlyDropDown" + i) as DropDownList;
                                        if (currentDropDownListDownList != null)
                                        {
                                            zConstantListTable currentConstList = (from a in competitionDataBase.zConstantListTable
                                                where a.ID == currentColumn.FK_ConstantListsTable
                                                && a.Active == true
                                                select a).FirstOrDefault();
                                         
                                            List<zCollectedDataTable> collectedDataList =
                                                (from a in competitionDataBase.zCollectedDataTable
                                                 where a.FK_ConstantListTable == currentConstList.ID
                                                 && a.Active == true
                                                 select a).Distinct().ToList();

                                            foreach (zCollectedDataTable tmpCollectedForDropDown in collectedDataList)
                                            {
                                                ListItem newListItem = new ListItem();
                                                if (currentCollectedData.ValueFK_CollectedDataTable == tmpCollectedForDropDown.ID)
                                                {
                                                    newListItem.Selected = true;
                                                }
                                                newListItem.Value = tmpCollectedForDropDown.ID.ToString();
                                                newListItem.Text = tmpCollectedForDropDown.ValueText;       
                                      
                                                currentDropDownListDownList.Items.Add(newListItem);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void DeleteRowButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                #region session
                var sessionParam1 = Session["ApplicationID"];
                var sessionParam2 = Session["SectionID"];
                var userIdParam = Session["UserID"];

                if ((sessionParam1 == null) || (sessionParam2 == null) || (userIdParam == null))
                {
                    //error
                    Response.Redirect("ChooseSection.aspx");
                }

                int applicationId = Convert.ToInt32(sessionParam1);
                int sectionId = Convert.ToInt32(sessionParam2);
                int userId = Convert.ToInt32(userIdParam);
                #endregion
                zCollectedRowsTable currentRow = (from a in competitionDataBase.zCollectedRowsTable
                                                where 
                                                a.FK_SectionTable == sectionId
                                                join b in competitionDataBase.zCollectedDataTable
                                                on a.ID equals b.FK_CollectedRowsTable
                                                where b.ID == Convert.ToInt32(button.CommandArgument)
                                                select a).FirstOrDefault();

                if (currentRow != null)
                {
                    currentRow.Active = false;
                    competitionDataBase.SubmitChanges();
                    Response.Redirect("FillSection.aspx");
                }
            }   
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseSection.aspx");
        }
        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            #region session
            var sessionParam1 = Session["ApplicationID"];
            var sessionParam2 = Session["SectionID"];
            var userIdParam = Session["UserID"];

            if ((sessionParam1 == null) || (sessionParam2 == null) || (userIdParam == null))
            {
                //error
                Response.Redirect("ChooseSection.aspx");
            }

            int applicationId = Convert.ToInt32(sessionParam1);
            int sectionId = Convert.ToInt32(sessionParam2);
            int userId = Convert.ToInt32(userIdParam);
            #endregion
            zCollectedRowsTable newRow = new zCollectedRowsTable();
            newRow.Active = true;
            newRow.FK_SectionTable = sectionId;
            newRow.FK_ApplicationTable = applicationId;
            competitionDataBase.zCollectedRowsTable.InsertOnSubmit(newRow);
            competitionDataBase.SubmitChanges();
            Response.Redirect("FillSection.aspx");
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();
            foreach (GridViewRow currentRow in FillingGV.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    Label idLabel = (Label) currentRow.FindControl("ID"+i.ToString());
                    zCollectedDataTable currentCollectedData = (from a in competitionDataBase.zCollectedDataTable
                        where a.ID == Convert.ToInt32(idLabel.Text)
                        select a).FirstOrDefault();
                    if (currentCollectedData != null)
                    {
                        #region сохраняем в зависимоти от типа данных
                        zColumnTable currenColumn = (from a in competitionDataBase.zColumnTable
                            where a.ID == currentCollectedData.FK_ColumnTable
                            select a).FirstOrDefault();
                        //---------------------------------------------------------------------------------------------------------------
                        if (dataType.IsDataTypeBit(currenColumn.DataType))
                        {
                            CheckBox gvCheckBox = (CheckBox)currentRow.FindControl("EditBoolCheckBox" + i.ToString());
                            if (gvCheckBox != null)
                            {
                                currentCollectedData.ValueBit = gvCheckBox.Checked;
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------
                        if (dataType.IsDataTypeInteger(currenColumn.DataType))
                        {
                            TextBox gvTextBox = (TextBox)currentRow.FindControl("EditTextBox" + i.ToString());
                            if (gvTextBox != null)
                            {
                                currentCollectedData.ValueInt = Convert.ToInt32(gvTextBox.Text);
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------
                        if (dataType.IsDataTypeFloat(currenColumn.DataType))
                        {
                            TextBox gvTextBox = (TextBox)currentRow.FindControl("EditTextBox" + i.ToString());
                            if (gvTextBox != null)
                            {
                                if (gvTextBox.Text.Any())
                                currentCollectedData.ValueDouble = Convert.ToDouble(gvTextBox.Text);
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------
                        if (dataType.IsDataTypeText(currenColumn.DataType))
                        {
                            TextBox gvTextBox = (TextBox)currentRow.FindControl("EditTextBox" + i.ToString());
                            if (gvTextBox != null)
                            {
                                currentCollectedData.ValueText = gvTextBox.Text;
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------
                        if (dataType.IsDataTypeDate(currenColumn.DataType))
                        {
                            Calendar gvCalendar = (Calendar)currentRow.FindControl("ChooseDateCalendar" + i.ToString());
                            if (gvCalendar != null)
                            {
                                currentCollectedData.ValueDataTime = gvCalendar.SelectedDate;
                            }
                        }
                        if ((dataType.IsDataTypeDropDown(currenColumn.DataType)) || (dataType.IsDataTypeConstantDropDown(currenColumn.DataType)))
                        {
                            DropDownList gvDropDownList = (DropDownList)currentRow.FindControl("ChooseOnlyDropDown" + i.ToString());
                            if (gvDropDownList != null)
                            {
                                currentCollectedData.ValueFK_CollectedDataTable = Convert.ToInt32(gvDropDownList.SelectedValue);
                            }
                        }                      
                        #endregion
                        competitionDataBase.SubmitChanges();
                    }
                }              
            }
            Response.Redirect("FillSection.aspx");
        }
        
    }
}
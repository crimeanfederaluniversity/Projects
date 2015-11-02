using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.User
{
    public partial class FillSection : System.Web.UI.Page
    {
        const int columnCount = 10;
        private bool permitAddRow = true;
        private bool permitDeleteRow = true;
        public int GetApplicationIdFromSession()
        {
            FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
            if (newPagesParams.SectionId == null)
            {
                //error
                Response.Redirect("ChooseApplicationAction.aspx");
            }

            int applicationId = newPagesParams.ApplicationId;
            return applicationId;
        }
        public int GetBlockIdFromSession()
        {
            FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
            if (newPagesParams.BlockId == null)
            {
                //error
                Response.Redirect("ChooseApplicationAction.aspx");
            }

            int blockId = newPagesParams.BlockId;
            return blockId;
        }
        public int GetSectionId()
        {
            FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
            if (newPagesParams.SectionId == null)
            {
                //error
                Response.Redirect("ChooseApplicationAction.aspx");
            }
            if (newPagesParams.SectionId.Count == 0)
            {
                return 0;
            }
            int sectionId = newPagesParams.SectionId[newPagesParams.CurrentPage];
            return sectionId;
        }
        private string UploadFile(FileUpload fileUpload, int collectedDataId)
        {         
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedRowsTable currentRow = (from a in competitionDataBase.zCollectedDataTable
                join b in competitionDataBase.zCollectedRowsTable
                    on a.FK_CollectedRowsTable equals b.ID
                where a.ID == collectedDataId
                && a.Active == true
                && b.Active == true
                select b).FirstOrDefault();
            if (currentRow == null)
                return null;
            int applicationId = currentRow.FK_ApplicationTable;
            string documentsPath = Server.MapPath("~/documents/byApplication");
            Directory.CreateDirectory(documentsPath + "\\\\" + applicationId.ToString());
            string applicationPath = Server.MapPath("~/documents/byApplication/"+applicationId.ToString());
            Directory.CreateDirectory(applicationPath + "\\\\" + collectedDataId.ToString());
            if (fileUpload.HasFile)
            {
                if (fileUpload.PostedFiles.Count > 1)
                    {
                        //NOT ALLOWED
                        /*
                            int AttachOK = 0;
                            int AttachError = 0;
                            string DBLinkCombine = "";
                            foreach (var file in FileUpload1.PostedFiles)
                            {
                                try
                                {
                                    file.SaveAs(path + "\\\\" + applicationId.ToString() + "\\\\" + file.FileName);
                                    NewDocument(applicationId, file.FileName);
                                    AttachOK++;
                                }
                                catch (Exception ex)
                                {
                                    AttachError++;
                                }

                                //  FileStatusLabel.Text = "Загружено " + AttachOK.ToString() + " файлов из " + (AttachError + AttachOK).ToString();
                                // NewCampaign.DocumentLocation = DBLinkCombine;
                                // DBConnection.SubmitChanges();
                            }*/
                        }
                        else
                        {
                            try
                            {
                                fileUpload.PostedFile.SaveAs(applicationPath + "\\\\" + collectedDataId.ToString()+"\\\\" + fileUpload.FileName);
                                return fileUpload.FileName;
                            }
                            catch (Exception ex)
                            {
                                return null;
                                //  FileStatusLabel.Text = "Не удалось загрузить файл.";
                            }
                }
            }
            return null;
        }
        private string GetFileByCollectedData(int collectedDataId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedDataTable currentCollected = (from a in competitionDataBase.zCollectedDataTable
                where a.ID == collectedDataId
                select a).FirstOrDefault();
            if (currentCollected == null)
                return null;
            zCollectedRowsTable currentRow = (from a in competitionDataBase.zCollectedRowsTable
                where a.ID == currentCollected.FK_CollectedRowsTable
                select a).FirstOrDefault();
            if (currentRow == null)
                return null;
            int applicationId = currentRow.FK_ApplicationTable;
            string filePath = Server.MapPath("~/documents/byApplication/" + applicationId.ToString() + "/" + collectedDataId.ToString() + "/" + currentCollected.ValueText);
            if (!File.Exists(filePath))
                return null;
            return filePath;
        }
        private string GetFileNameByCollectedData(int collectedDataId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedDataTable currentCollected = (from a in competitionDataBase.zCollectedDataTable
                where a.ID == collectedDataId
                select a).FirstOrDefault();
            if (currentCollected == null)
                return null;
            return currentCollected.ValueText;
        }
        public bool SaveChanges()
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();
            foreach (GridViewRow currentRow in FillingGV.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    Label idLabel = (Label)currentRow.FindControl("ID" + i.ToString());
                    if (idLabel == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
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
                                if (gvTextBox.Text.Any())
                                {
                                    currentCollectedData.ValueInt = Convert.ToInt32(gvTextBox.Text);

                                }
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
                                if (gvTextBox.Text.Any())
                                {
                                    currentCollectedData.ValueText = gvTextBox.Text;
                                }
                            }
                        }
                        //---------------------------------------------------------------------------------------------------------------
                        if (dataType.IsDataTypeDate(currenColumn.DataType))
                        {
                            TextBox gvTextBox = (TextBox)currentRow.FindControl("EditTextBox" + i.ToString());
                            if (gvTextBox != null)
                            {
                                if (gvTextBox.Text.Any())
                                {
                                    DateTime? newDateTime = null;
                                    try
                                    {
                                        if (Convert.ToDateTime(gvTextBox.Text) > DateTime.MinValue)
                                        {
                                            newDateTime = Convert.ToDateTime(gvTextBox.Text);
                                            currentCollectedData.ValueDataTime = newDateTime;
                                        }
                                    
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                        throw;
                                    }
                                    
                                }
                            }
                        }
                        if ((dataType.IsDataTypeDropDown(currenColumn.DataType)) || (dataType.IsDataTypeConstantDropDown(currenColumn.DataType))|| (dataType.IsDataTypeConstntAtLeastOneWithCheckBoxParam(currenColumn.DataType)))
                        {
                            DropDownList gvDropDownList = (DropDownList)currentRow.FindControl("ChooseOnlyDropDown" + i.ToString());
                            if (gvDropDownList != null)
                            {
                                if (gvDropDownList.SelectedValue.Any())
                                currentCollectedData.ValueFK_CollectedDataTable = Convert.ToInt32(gvDropDownList.SelectedValue);
                            }
                        }
                        if (dataType.IsDataTypeFileUpload(currenColumn.DataType))
                        {
                            FileUpload gvFileUpload = (FileUpload)currentRow.FindControl("FileUpload" + i.ToString());
                            if (gvFileUpload != null)
                            {
                                if (gvFileUpload.Visible == true)
                                currentCollectedData.ValueText = UploadFile(gvFileUpload, currentCollectedData.ID);
                            }
                            
                        }
                        #endregion
                        competitionDataBase.SubmitChanges();
                    }
                }
            }
            return true;
        }
        public string GetShortString(string textValue, int maxLength,string endingString )
        {
            if (textValue.Length > maxLength)
            {
                return textValue.Remove(maxLength, textValue.Length - maxLength) + endingString;
            }
            return textValue;
        }
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
        public bool CreateRowsForConstantAndNecessary(List<zCollectedDataTable> listOfCollectedData, int applicationId, int sectionId, int columnId)
        {
            foreach (zCollectedDataTable currentMustBeData in listOfCollectedData)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
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
        public bool AddRowWithConstant(int constantListId,int applicationId,int sectionId,int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> mustBeDataList = (from a in competitionDataBase.zCollectedDataTable
                                                        where a.Active == true
                                                              && a.FK_ConstantListTable == constantListId
                                                        select a).ToList();

            CreateRowsForConstantAndNecessary(mustBeDataList, applicationId, sectionId, columnId);

            return true;
        }
        public List<zCollectedRowsTable> GetMustBeRows(int applicationId, int? columnTableWithNeededValueId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            return (from a in competitionDataBase.zCollectedRowsTable
                    where a.Active == true
                          && a.FK_ApplicationTable == applicationId
                    join b in competitionDataBase.zCollectedDataTable
                        on a.ID equals b.FK_CollectedRowsTable
                    where b.Active == true
                          && b.FK_ColumnTable == columnTableWithNeededValueId
                    select a).Distinct().ToList();
        }
        public List<zCollectedDataTable> GetMustNotBeByCheckBoxesData(int applicationId, int columnTableWithNeededValueId, int columnTableWithCheckBox)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> mustNOTBeDataList = new List<zCollectedDataTable>();
            //найдем лист строк которые на значение которых ссылается наша колонка 
            List<zCollectedRowsTable> mustBeDataRows = GetMustBeRows(applicationId, columnTableWithNeededValueId);
            // пройдемся по этому листу чтобы найти строки которые нужно удалить из-за отсутстви галочки          
            foreach (zCollectedRowsTable curRow in mustBeDataRows)
            {
                zCollectedDataTable data = (from a in competitionDataBase.zCollectedDataTable
                                            where a.FK_CollectedRowsTable == curRow.ID
                                                  && a.Active == true
                                                  && a.FK_ColumnTable == columnTableWithNeededValueId
                                            select a).FirstOrDefault();
                //нашли в строке ячейку ссылкой на колонку значения
                zCollectedDataTable checkBox = (from a in competitionDataBase.zCollectedDataTable
                                                where a.FK_CollectedRowsTable == curRow.ID
                                                      && a.Active == true
                                                      && a.FK_ColumnTable == columnTableWithCheckBox
                                                select a).FirstOrDefault();
                //нашли в строке ячейку с галочкой
                if (checkBox != null)
                {
                    if (checkBox.ValueBit != null)
                    {
                        if (checkBox.ValueBit != true)
                        {
                            mustNOTBeDataList.Add(data);
                        }
                    }
                    else
                    {
                        mustNOTBeDataList.Add(data);
                    }
                }
                //создали лист с строками где чекбокс без галочки
            }
            return mustNOTBeDataList;
        }
        public List<zCollectedDataTable> GetMustBeByCheckBoxeData(int applicationId, int? columnTableWithNeededValueId, int? columnTableWithCheckBox)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> mustBeDataList = new List<zCollectedDataTable>();
            List<zCollectedRowsTable> mustBeDataRows = GetMustBeRows(applicationId, columnTableWithNeededValueId);

            foreach (zCollectedRowsTable curRow in mustBeDataRows)
            {
                zCollectedDataTable data = (from a in competitionDataBase.zCollectedDataTable
                                            where a.FK_CollectedRowsTable == curRow.ID
                                                  && a.Active == true
                                                  && a.FK_ColumnTable == columnTableWithNeededValueId
                                            select a).FirstOrDefault();
                zCollectedDataTable checkBox = (from a in competitionDataBase.zCollectedDataTable
                                                where a.FK_CollectedRowsTable == curRow.ID
                                                      && a.Active == true
                                                      && a.FK_ColumnTable == columnTableWithCheckBox
                                                select a).FirstOrDefault();
                if (checkBox != null)
                {
                    if (checkBox.ValueBit != null)
                    {
                        if (checkBox.ValueBit == true)
                        {
                            mustBeDataList.Add(data);
                        }
                    }
                }
            }
            return mustBeDataList;
        }
        public bool AddRowWithNecwssairlyLines (int columnTableId,int applicationId,int sectionId,int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> mustBeDataList = (from a in competitionDataBase.zCollectedDataTable
                                                        where a.Active == true
                                                              && a.FK_ColumnTable == columnTableId
                                                              join b in competitionDataBase.zCollectedRowsTable
                                                              on a.FK_CollectedRowsTable equals b.ID
                                                              where b.Active == true
                                                              && b.FK_ApplicationTable == applicationId
                                                        select a).ToList();

            CreateRowsForConstantAndNecessary(mustBeDataList, applicationId, sectionId, columnId);
            return false;
        }
        public bool AddRowWithNecwssairlyLinesWithParams(int columnTableWithNeededValueId, int applicationId, int currentSectionId, int currentColumnId, int columnTableWithCheckBox)
        {
            // columnTableWithNeededValueId  // наша колонка ссылаетс на эту чтобы получить значение 
            // constantListWithNeededValueId // наша колонка ссылаетс на этот лист констант чтобы получить значение 
            // currentColumnId               // айдишник самой колонки
            // columnTableWithCheckBox       // наша колонка ссылается на эту чтобы проверить чекбокс
            List<zCollectedDataTable> mustBeDataList = GetMustBeByCheckBoxeData(applicationId,
                columnTableWithNeededValueId, columnTableWithCheckBox);
            CreateRowsForConstantAndNecessary(mustBeDataList, applicationId, currentSectionId, currentColumnId);
            return false;
        }
        public bool DeleteRowWithUnSelectedCheckBox(int columnTableWithNeededValueId, int applicationId, int currentSectionId, int currentColumnId, int columnTableWithCheckBox)
        {
            // columnTableWithNeededValueId  // наша колонка ссылаетс на эту чтобы получить значение 
            // constantListWithNeededValueId // наша колонка ссылаетс на этот лист констант чтобы получить значение 
            // currentColumnId               // айдишник самой колонки
            // columnTableWithCheckBox       // наша колонка ссылается на эту чтобы проверить чекбокс
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> mustNotBeDataList = GetMustNotBeByCheckBoxesData(applicationId,
                columnTableWithNeededValueId, columnTableWithCheckBox);
            foreach (zCollectedDataTable currentDataToDel in mustNotBeDataList)
            {
                List<zCollectedRowsTable> rowsToDelList = (from a in competitionDataBase.zCollectedRowsTable
                    where a.Active == true
                    join b in competitionDataBase.zCollectedDataTable
                        on a.ID equals b.FK_CollectedRowsTable
                    where
                        b.ValueFK_CollectedDataTable == currentDataToDel.ID
                    select a).Distinct().ToList(); // надо бы считать лист и если там их много то удалить сразу все
                foreach (zCollectedRowsTable currentRowToDel in rowsToDelList)
                {
                    if (currentRowToDel != null)
                    {
                        currentRowToDel.Active = false;
                        competitionDataBase.SubmitChanges();
                    }
                }         
            }
            
            return false;
        }
        public bool DeleteRow(int rowId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zCollectedRowsTable rowToDelete = (from a in competitionDataBase.zCollectedRowsTable
                                               where a.ID == rowId
                                               select a).FirstOrDefault();
            if (rowToDelete != null)
            {
                rowToDelete.Active = false;
                competitionDataBase.SubmitChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool DeleteRowWithBadFk(int applicationId, int sectionId, int columnId , bool isDataConstant)
        {
            //эта функция удаляет строку если в ней одна из колонок ссылается на несуществующее значение
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zCollectedDataTable> existingCollectedDataTableList = (
                from a in competitionDataBase.zCollectedDataTable
                join b in competitionDataBase.zCollectedRowsTable
                on a.FK_CollectedRowsTable equals b.ID
                where b.Active == true
                      && a.Active == true
                      && b.FK_ApplicationTable == applicationId
                      && b.FK_SectionTable == sectionId
                      && a.FK_ColumnTable == columnId
                select a).ToList();
            foreach (zCollectedDataTable currentCollectedData in existingCollectedDataTableList)
            {
                zCollectedDataTable fkOfCurrent = (from a in competitionDataBase.zCollectedDataTable
                    where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                    select a).FirstOrDefault();
                if (fkOfCurrent==null)
                {
                    DeleteRow((int)currentCollectedData.FK_CollectedRowsTable);
                }
                else if (fkOfCurrent.Active == false)
                {
                    DeleteRow((int)currentCollectedData.FK_CollectedRowsTable);
                }
                else if (!isDataConstant) // у констант нет ссылки на Row
                {
                    if (fkOfCurrent.FK_CollectedRowsTable == null)
                    {
                        DeleteRow((int) currentCollectedData.FK_CollectedRowsTable);
                    }
                    else 
                    {
                        zCollectedRowsTable rowOfFkOfCurrent =
                        (from a in competitionDataBase.zCollectedRowsTable
                         where a.ID == fkOfCurrent.FK_CollectedRowsTable
                         select a).FirstOrDefault();
                        if (rowOfFkOfCurrent == null)
                        {
                            DeleteRow((int) currentCollectedData.FK_CollectedRowsTable);
                        }
                        else
                        {
                            if (rowOfFkOfCurrent.Active == false)
                            {
                                DeleteRow((int)currentCollectedData.FK_CollectedRowsTable);
                            }
                        }
                    }
                }                              
            }
            return true;
        }
        public double GetValueFromCollectedData(int rowId, int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zColumnTable currentColumn = (from a in competitionDataBase.zColumnTable
                where a.ID == columnId
                select a).FirstOrDefault();
            DataType dataType = new DataType();
            if (columnId != null)
            {
                zCollectedDataTable currenCollectedDataTable = (from a in competitionDataBase.zCollectedDataTable
                                                                where a.Active == true
                                                                      && a.FK_ColumnTable == columnId
                                                                      && a.FK_CollectedRowsTable == rowId
                                                                select a).FirstOrDefault();

                if (dataType.IsDataTypeFloat(currentColumn.DataType))
                {
                    return (double) currenCollectedDataTable.ValueDouble;
                }
                if (dataType.IsDataTypeInteger(currentColumn.DataType))
                {
                    return (double)currenCollectedDataTable.ValueInt;
                }
                if (dataType.IsDataTypeBit(currentColumn.DataType))
                {
                    if ((bool)currenCollectedDataTable.ValueBit)
                    {
                        return 1;
                    }
                }

            }
            return 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {           
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            if (!Page.IsPostBack)
            {
                #region SETUP
                DataType dataType = new DataType();
                #region session
                var userIdParam = Session["UserID"];
                FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
                if ((userIdParam == null) || (newPagesParams.SectionId==null))
                {
                    //error
                    Response.Redirect("ChooseSection.aspx");
                }

                int applicationId = newPagesParams.ApplicationId;

                CompetitionCountDown competitionCountDown = new CompetitionCountDown();
                CountDownLabel.Text =
                    competitionCountDown.GetDaysBeforeCompetitionEndMessageByApplicationId(applicationId);

                int sectionId = GetSectionId();
                if (sectionId == 0)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int userId = Convert.ToInt32(userIdParam);
                #endregion
                #region dataTableCreate создадим большой dataTable
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

                    dataTable.Columns.Add(new DataColumn("FileUploadVisible" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("FileLinkButtonVisible" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("FileLinkButtonText" + i.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("DeleteFileLinkButtonVisible" + i.ToString(), typeof(bool)));

                    dataTable.Columns.Add(new DataColumn("CalendarButtonClick" + i.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CalendarButtonVisible" + i.ToString(), typeof(bool)));
                    
                    // dataTable.Columns.Add(new DataColumn("ChooseOnlyDropDownValue" + i.ToString(), typeof(ListItemCollection)));

                    // валидаор
                    dataTable.Columns.Add(new DataColumn("TextBoxValidateEnable" + i.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("TextBoxValidateMinValue" + i.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("TextBoxValidateMaxValue" + i.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("TextBoxValidateType" + i.ToString(), typeof(ValidationDataType)));
                    dataTable.Columns.Add(new DataColumn("TextBoxValidateText" + i.ToString(), typeof(string)));

                    dataTable.Columns.Add(new DataColumn("TextBoxRequireValidateEnable" + i.ToString(), typeof(bool)));

                    dataTable.Columns.Add(new DataColumn("EditTextBoxMode" + i.ToString(), typeof(TextBoxMode)));     
                    
                    
                        
                }

                #endregion                
                #region найдем нашу секцию currentSection
                int blockId = GetBlockIdFromSession();
                //нам нужно узнать как выглядит таблица и создать её
                //у каждой сектии свой тип таблиц сначала возьмем текующую секцию
               
                zSectionTable currentSection = (from a in competitionDataBase.zSectionTable
                                                      where  a.ID == sectionId
                                                      select a).FirstOrDefault();
                #endregion
                #region сделаем легенду
                List<zSectionTable> allSection = (from a in competitionDataBase.zSectionTable
                                                  where a.FK_BlockID == blockId
                                                  join b in competitionDataBase.zApplicationTable
                                                  on a.FK_CompetitionsTable equals b.FK_CompetitionTable
                                                  where b.ID == applicationId
                                                  select a).ToList();
                string namesInLine = " ";
                if (allSection.Count != null)
                {
                    foreach (var n in allSection)
                    {
                        if (n.ID == currentSection.ID)                        
                        namesInLine += "<font size=\"3\" color=\"red\" face=\"Arial\">" + n.Name + "</font> -->";                        
                        else
                        namesInLine +=  n.Name + "</font> -->";                      
                    }                    
                    LabelHint.Text = namesInLine;
                }
                #endregion
                if (currentSection.Description != "123")
                {
                    LabelDescription.Text = currentSection.Description;
                }
                //найдем все колонки привязанные к этой секции
                List<zColumnTable> columnInSectionList = (from a in competitionDataBase.zColumnTable
                                                          where a.Active == true
                                                                && a.FK_SectionTable == sectionId
                                                          select a).ToList();

                #region constRowManage

                foreach (zColumnTable currentColumn in columnInSectionList)
                {
                    if (dataType.IsDataTypeConstantNecessarilyShow(currentColumn.DataType))
                    {
                        permitAddRow = false;
                        permitDeleteRow = false;

                        DeleteRowWithBadFk(applicationId, sectionId, currentColumn.ID,true);
                        AddRowWithConstant((int) currentColumn.FK_ConstantListsTable, applicationId, sectionId,
                            currentColumn.ID);
                    }
                    if (dataType.IsDataTypeNecessarilyShow(currentColumn.DataType))
                    {
                        permitAddRow = false;
                        permitDeleteRow = false;

                        DeleteRowWithBadFk(applicationId, sectionId,currentColumn.ID,false);
                        AddRowWithNecwssairlyLines ((int)currentColumn.FK_ColumnTable, applicationId, sectionId,
                            currentColumn.ID);
                    }
                    if (dataType.IsDataTypeNecessarilyShowWithParam(currentColumn.DataType))
                    {
                        permitAddRow = false;
                        permitDeleteRow = false;
                        DeleteRowWithBadFk(applicationId, sectionId, currentColumn.ID, false);
                        DeleteRowWithUnSelectedCheckBox((int)currentColumn.FK_ColumnTable, applicationId, sectionId,
                            currentColumn.ID, (int)currentColumn.FK_ColumnConnectToTable);
                        AddRowWithNecwssairlyLinesWithParams((int)currentColumn.FK_ColumnTable,applicationId, sectionId,
                            currentColumn.ID,(int)currentColumn.FK_ColumnConnectToTable);
                    }
                    if (dataType.IsDataTypeConstntAtLeastOneWithCheckBoxParam(currentColumn.DataType))
                    {
                        permitAddRow = true;
                        permitDeleteRow = true;
                        DeleteRowWithUnSelectedCheckBox((int)currentColumn.FK_ColumnTable, applicationId, sectionId,
                            currentColumn.ID, (int)currentColumn.FK_ColumnConnectToTable);
                        AddRowWithNecwssairlyLinesWithParams((int)currentColumn.FK_ColumnTable, applicationId, sectionId,
                            currentColumn.ID, (int)currentColumn.FK_ColumnConnectToTable);
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
                double[] totalUpSums = new double[10];
                for (int i = 0; i < 10; i++)
                    totalUpSums[i] = 0;
                foreach (zCollectedRowsTable currentRow in currentRowsList)
                {
                    iterator++;
                    //в каждой строке есть одна или больше колонок
                    //пройдемся по каждой колонке    
                    bool doAddRow = true;
                    DataRow newDataRow = dataTable.NewRow();
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
                       
                        newDataRow["FileUploadVisible" + i.ToString()] = false;
                        newDataRow["FileLinkButtonVisible" + i.ToString()] = false;
                        newDataRow["DeleteFileLinkButtonVisible" + i.ToString()] = false;
                        newDataRow["FileLinkButtonText" + i.ToString()] = "";

                        newDataRow["CalendarButtonClick" + i.ToString()] = "";
                        newDataRow["CalendarButtonVisible" + i.ToString()] = false;
                        //validator
                        newDataRow["TextBoxValidateEnable" + i.ToString()] = false;
                        newDataRow["TextBoxValidateMinValue" + i.ToString()] = 0;
                        newDataRow["TextBoxValidateMaxValue" + i.ToString()] = 0;
                        newDataRow["TextBoxValidateType" + i.ToString()] = ValidationDataType.String;
                        newDataRow["TextBoxValidateText" + i.ToString()] = "";

                        newDataRow["TextBoxRequireValidateEnable" + i.ToString()] = false;

                        newDataRow["EditTextBoxMode" + i.ToString()] = TextBoxMode.SingleLine;

                        
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
                            DataProcess dataProcess = new DataProcess();
                            dataProcess.ClearTotalUp();
                            int dType = (int) currentColumn.DataType;

                            #region readwrite
                            if (dataProcess.IsCellReadWrite(dType))
                            {
                                newDataRow["EditTextBoxVisible" + i.ToString()] = true;
                                string str = dataProcess.GetReadWriteString(currentColumn, currentCollectedData);
                                newDataRow["EditTextBoxValue" + i.ToString()] = str;                                    
                                ValidatorParams currentValidator = dataProcess.GetValidatorParams();
                                newDataRow["TextBoxRequireValidateEnable" + i.ToString()] = currentValidator.RequireValidatorEnabled;
                                newDataRow["TextBoxValidateEnable" + i.ToString()] = currentValidator.Enabled;
                                newDataRow["TextBoxValidateMinValue" + i.ToString()] = currentValidator.MinValue;
                                newDataRow["TextBoxValidateMaxValue" + i.ToString()] = currentValidator.MaxValue;
                                newDataRow["TextBoxValidateType" + i.ToString()] = currentValidator.ContentType;
                                newDataRow["TextBoxValidateText" + i.ToString()] = currentValidator.ErrorMessage;
                                newDataRow["EditTextBoxMode" + i.ToString()] = currentValidator.TextBoxTextMode;
                            }
                            #endregion
                            #region checkbox
                            if (dataProcess.IsCellCheckBox(dType))
                            {
                                newDataRow["EditBoolCheckBoxVisible" + i.ToString()] = true;
                                newDataRow["EditBoolCheckBoxValue" + i.ToString()] =
                                    dataProcess.GetBoolDataValue(currentColumn, currentCollectedData);
                            }
                            #endregion
                            #region readonly
                            if (dataProcess.IsCellReadOnly(dType))
                            {
                                newDataRow["ReadOnlyLablelVisible" + i.ToString()] = true;
                                string tmp = dataProcess.GetReadOnlyString(currentColumn, currentCollectedData,applicationId,currentRow.ID,iterator);
                                if (tmp == "")
                                {
                                    doAddRow = false;
                                }
                                newDataRow["ReadOnlyLablelValue" + i.ToString()] = tmp;

                               
                            }
                            #endregion
                            #region dropdown
                            if (dataProcess.IsCellDropDown(dType))
                            {
                                newDataRow["ChooseOnlyDropDownVisible" + i.ToString()] = true;
                            }
                            #endregion
                            #region fileupload
                            if (dataProcess.IsCellFileUpload(dType))
                            {
                                if (currentCollectedData.ValueText!=null)
                                {
                                    if (currentCollectedData.ValueText.Length > 3)
                                    {
                                        newDataRow["FileLinkButtonVisible" + i.ToString()] = true;
                                        newDataRow["DeleteFileLinkButtonVisible" + i.ToString()] = true;
                                        newDataRow["FileLinkButtonText" + i.ToString()] = currentCollectedData.ValueText;
                                    }
                                    else
                                    {
                                        newDataRow["FileUploadVisible" + i.ToString()] = true;
                                    }
                                }
                                else
                                {
                                    newDataRow["FileUploadVisible" + i.ToString()] = true;
                                }
                            }
                            #endregion                          
                            #region additionalForDateCalendar
                            if (dataProcess.IsCellDate(dType))
                            {
                                newDataRow["CalendarButtonClick" + i.ToString()] = "displayDatePicker('ctl00$MainContent$FillingGV$ctl" + (iterator+1).ToString("D2") + "$EditTextBox" + i + "', false, 'ymd', '-')";
                                newDataRow["CalendarButtonVisible" + i.ToString()] = true;
                            }
                            #endregion
                            totalUpSums[i] += dataProcess.GetToTotalUpValue();
                        }
                        #endregion
                    }    
                    if (doAddRow)
                    dataTable.Rows.Add(newDataRow);
                }
                #region TotalUp
                DataRow newDataRowForTotalUp = dataTable.NewRow();
                bool anyTotalUp = false;
                //foreach (zColumnTable currentColumn in columnInSectionList)
                for (int i = 0; i < 10; i++)
                {
                    #region записываем стандартный значения во все поля
                    newDataRowForTotalUp["ID" + i.ToString()] = 0;
                    newDataRowForTotalUp["ReadOnlyLablelVisible" + i.ToString()] = false;
                    newDataRowForTotalUp["ReadOnlyLablelValue" + i.ToString()] = "";
                    newDataRowForTotalUp["EditTextBoxVisible" + i.ToString()] = false;
                    newDataRowForTotalUp["EditTextBoxValue" + i.ToString()] = "";
                    newDataRowForTotalUp["EditBoolCheckBoxVisible" + i.ToString()] = false;
                    newDataRowForTotalUp["EditBoolCheckBoxValue" + i.ToString()] = false;
                    newDataRowForTotalUp["ChooseOnlyDropDownVisible" + i.ToString()] = false;
                    //newDataRow["ChooseOnlyDropDownValue" + i.ToString()] = null;


                    newDataRowForTotalUp["FileUploadVisible" + i.ToString()] = false;
                    newDataRowForTotalUp["FileLinkButtonVisible" + i.ToString()] = false;
                    newDataRowForTotalUp["DeleteFileLinkButtonVisible" + i.ToString()] = false;
                    newDataRowForTotalUp["FileLinkButtonText" + i.ToString()] = "";

                    newDataRowForTotalUp["CalendarButtonClick" + i.ToString()] = "";
                    newDataRowForTotalUp["CalendarButtonVisible" + i.ToString()] = false;

                    newDataRowForTotalUp["TextBoxValidateEnable" + i.ToString()] = false;
                    newDataRowForTotalUp["TextBoxValidateMinValue" + i.ToString()] = 0;
                    newDataRowForTotalUp["TextBoxValidateMaxValue" + i.ToString()] = 0;
                    newDataRowForTotalUp["TextBoxValidateType" + i.ToString()] = ValidationDataType.String;
                    newDataRowForTotalUp["TextBoxValidateText" + i.ToString()] = "";

                    newDataRowForTotalUp["TextBoxRequireValidateEnable" + i.ToString()] = false;

                    newDataRowForTotalUp["EditTextBoxMode" + i.ToString()] = TextBoxMode.SingleLine;
                    #endregion                  
                    #region

                    if (i < columnInSectionList.Count)
                    {
                        if (columnInSectionList[i].TotalUp != null)
                        {
                            if ((bool) columnInSectionList[i].TotalUp)
                            {
                                anyTotalUp = true;
                                newDataRowForTotalUp["ReadOnlyLablelVisible" + i.ToString()] = true;
                                newDataRowForTotalUp["ReadOnlyLablelValue" + i.ToString()] ="Итого: " + totalUpSums[i].ToString();
                            }
                        }
                    }

                    #endregion
                }
               if (anyTotalUp) dataTable.Rows.Add(newDataRowForTotalUp);
                #endregion
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
                /*
                DataView dv = dataTable.DefaultView;
                dv.Sort = "[ReadOnlyLablelValue0]";
                DataTable sortedDT = dv.ToTable();
                */
                FillingGV.DataSource = dataTable;

                FillingGV.Columns[10].Visible = permitDeleteRow;
                AddRowButton.Visible = AddRowButton.Enabled = permitAddRow;

                for (int i = 0; i < columnInSectionList.Count; i++)
                {
                    FillingGV.Columns[i].HeaderText = columnInSectionList[i].Name;
                    FillingGV.Columns[i].Visible = true;
                }
                FillingGV.DataBind();

                if (FillingGV.Rows.Count==0)
                {
                    var isGoBack = Session["IsGoBack"];
                    FillingPages newPagesParamsTmp = (FillingPages)Session["PagesParams"];
                    if (isGoBack != null)
                    {
                        newPagesParamsTmp.CurrentPage++;
                    }
                    else if (((bool) isGoBack) == false)
                    {
                        newPagesParamsTmp.CurrentPage++;
                    }
                    else
                    {
                        newPagesParamsTmp.CurrentPage--;
                    }
                                    
                    if (newPagesParamsTmp.CurrentPage == newPagesParamsTmp.PagesCount)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    Session["PagesParams"] = newPagesParamsTmp;
                    Response.Redirect("FillSection.aspx");
                }

                foreach (GridViewRow row in FillingGV.Rows)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        TextBox textbox = row.FindControl("EditTextBox"+i) as TextBox;
                        if (textbox != null)
                        {
                            if (textbox.TextMode == TextBoxMode.MultiLine)
                            {
                                textbox.Attributes.Add("onkeyup", "setHeightAndWidth(this)");
                                textbox.Attributes.Add("onkeydown", "setHeightAndWidth(this)");
                                textbox.Attributes.Add("class", "textBox1");
                            }
                            else
                            {
                                textbox.Attributes.Add("class", "textBox");
                            }
                        }
                    }                
                }
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
                                    DataType dataType = new DataType();
                                    DropDownList currentDropDownListDownList =
                                        e.Row.FindControl("ChooseOnlyDropDown" + i) as DropDownList;

                                    if (currentDropDownListDownList != null)
                                    {
                                        #region session

                                        var userIdParam = Session["UserID"];
                                        FillingPages newPagesParams = (FillingPages) Session["PagesParams"];
                                        if ((userIdParam == null) || (newPagesParams.SectionId == null))
                                        {
                                            //error
                                            Response.Redirect("ChooseSection.aspx");
                                        }

                                        int applicationId = newPagesParams.ApplicationId;
                                        int sectionId = newPagesParams.SectionId[newPagesParams.CurrentPage];
                                        int userId = Convert.ToInt32(userIdParam);

                                        #endregion

                                        #region dropDown

                                        if (dataType.IsDataTypeDropDown(currentColumn.DataType))
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

                                                foreach (
                                                    zCollectedDataTable tmpCollectedForDropDown in collectedDataList)
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
                                                        newListItem.Text =
                                                            tmpCollectedForDropDown.ValueDouble.ToString();
                                                    }
                                                    //---------------------------------------------------------------------------------------------------------------
                                                    if (dataType.IsDataTypeText(DropDownItemsColumn.DataType))
                                                    {
                                                        newListItem.Text = tmpCollectedForDropDown.ValueText;
                                                    }
                                                    //---------------------------------------------------------------------------------------------------------------
                                                    if (dataType.IsDataTypeDate(DropDownItemsColumn.DataType))
                                                    {
                                                        newListItem.Text =
                                                            tmpCollectedForDropDown.ValueDataTime.ToString();
                                                    }
                                                    newListItem.Attributes.Add("title", newListItem.Text);
                                                    //newListItem.Text = GetShortString(newListItem.Text, 50, "...");
                                                    currentDropDownListDownList.Items.Add(newListItem);

                                                    #endregion
                                                }
                                         }
                      

                                    #endregion

                                        #region constDropDown

                                    {
                                        if (dataType.IsDataTypeConstantDropDown(currentColumn.DataType))
                                        {
                                            zConstantListTable currentConstList =
                                                (from a in competitionDataBase.zConstantListTable
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
                                                if (currentCollectedData.ValueFK_CollectedDataTable ==
                                                    tmpCollectedForDropDown.ID)
                                                {
                                                    newListItem.Selected = true;
                                                }
                                                newListItem.Value = tmpCollectedForDropDown.ID.ToString();
                                                newListItem.Text = tmpCollectedForDropDown.ValueText;
                                                newListItem.Attributes.Add("title", newListItem.Text);
                                                //newListItem.Text = GetShortString(newListItem.Text, 50, "...");
                                                currentDropDownListDownList.Items.Add(newListItem);
                                            }
                                        }
                                    }

                                    #endregion

                                        #region atLeastOne

                                        if (dataType.IsDataTypeConstntAtLeastOneWithCheckBoxParam(currentColumn.DataType))
                                        {
                                            List<zCollectedDataTable> checkedItemsToPutInDropDown =
                                                GetMustBeByCheckBoxeData(applicationId,currentColumn.FK_ColumnTable,
                                                    currentColumn.FK_ColumnConnectToTable);
                                            DataProcess dataProcess = new DataProcess();
                                            foreach (zCollectedDataTable currentData in checkedItemsToPutInDropDown)
                                            {
                                                ListItem newListItem = new ListItem();
                                                if (currentData.ID == currentCollectedData.ValueFK_CollectedDataTable)
                                                  newListItem.Selected = true;
                                                newListItem.Value = currentData.ID.ToString();
                                                newListItem.Text = (from a in competitionDataBase.zCollectedDataTable
                                                    where a.ID == currentData.ValueFK_CollectedDataTable
                                                    select a.ValueText).FirstOrDefault();
                                                currentDropDownListDownList.Items.Add(newListItem);
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
        }
        protected void DeleteRowButtonClick(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            {
                SaveChanges();
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                #region session
                var userIdParam = Session["UserID"];

                if (userIdParam == null)
                {
                    //error
                    Response.Redirect("ChooseSection.aspx");
                }

                int applicationId = GetApplicationIdFromSession();
                int sectionId = GetSectionId();
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
        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            #region session
            var userIdParam = Session["UserID"];
            FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
            if ((userIdParam == null) || (newPagesParams.SectionId == null))
            {
                //error
                Response.Redirect("ChooseSection.aspx");
            }

            int applicationId = newPagesParams.ApplicationId;
            int sectionId = newPagesParams.SectionId[newPagesParams.CurrentPage];
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
            SaveChanges();
            Response.Redirect("FillSection.aspx");
        }
        protected void PreviousSection_Click(object sender, EventArgs e)
        {
            Session["IsGoBack"] = true;
            SaveChanges();
            FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
            newPagesParams.CurrentPage--;
            if (newPagesParams.CurrentPage == -1)
            {
                Response.Redirect("ChooseApplicationAction.aspx");
            }
            Session["PagesParams"] = newPagesParams;
            Response.Redirect("FillSection.aspx");
        }
        protected void NextSection_Click(object sender, EventArgs e)
        {
            Session["IsGoBack"] = false;
            SaveChanges();
            FillingPages newPagesParams = (FillingPages)Session["PagesParams"];
            newPagesParams.CurrentPage++;
            if (newPagesParams.CurrentPage == newPagesParams.PagesCount)
            {
                Response.Redirect("ChooseApplicationAction.aspx");
            }
            Session["PagesParams"] = newPagesParams;
            Response.Redirect("FillSection.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            SaveChanges();
            Response.Redirect("ChooseApplicationAction.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SaveChanges();
            Response.Redirect("~/Default.aspx");
        }
        protected void GvOpenDocumentButtonClick(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            {
                
                if (button!=null)
                {
                    string filePath = 
                    (GetFileByCollectedData(Convert.ToInt32(button.CommandArgument)));
                    string fileName = (GetFileNameByCollectedData(Convert.ToInt32(button.CommandArgument)));
                    //Response.ContentType = "Application/pdf";
                   // string FilePath = MapPath("acrobat.pdf");
                    Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                //Response.Redirect("FillSection.aspx");
            }
        }
        protected void GvDeleteDocumentButtonClick(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCollectedDataTable currentCollected = (from a in competitionDataBase.zCollectedDataTable
                                                        where a.ID == Convert.ToInt32(button.CommandArgument)
                                                        select a).FirstOrDefault();
                if (currentCollected != null)
                {
                    currentCollected.ValueText = null;
                    competitionDataBase.SubmitChanges();
                }
                Response.Redirect("FillSection.aspx");
            }
        }     
    }
}
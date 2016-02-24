﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using Chancelerry;
using Chancelerry.kanz;

namespace Chancelerry.kanz
{
    public class CardCommonFunctions
    {
        private ChancelerryDBDataContext chancDb = new ChancelerryDBDataContext();
        public RegistersModels GetRegisterModelById(int registerModelId)
        {
            return
                (from a in chancDb.RegistersModels where a.registerModelID == registerModelId && a.active select a)
                    .FirstOrDefault();
        }
        public List<FieldsGroups> GetFieldsGroupsInRegisterModelOrderByLine(int registerModelId)
        {
            return
                (from a in chancDb.FieldsGroups where a.fk_registerModel == registerModelId && a.active select a)
                    .Distinct().OrderBy(li => li.line).ToList();
        }
        public List<Fields> GetFieldsInFieldGroupOrderByLine(int fieldGroupId)
        {
            return
                (from a in chancDb.Fields where a.fk_fieldsGroup == fieldGroupId && a.active select a).Distinct()
                    .OrderBy(li => li.line)
                    .ToList();
        }
        public Registers GetRegisterById(int registerId)
        {
            return (from a in chancDb.Registers where a.active && a.registerID == registerId select a).FirstOrDefault();
        }
        public string GetFieldValueByCardVersionInstance(int fieldId, int cardId, int version, int instance)
        {
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                where a.active
                      && a.fk_field == fieldId
                      && a.fk_collectedCard == cardId
                      && a.instance == instance
                      && a.version <= version
                select a).OrderByDescending(ver => ver.version).FirstOrDefault();
            return currentCollectedFieldsValues.valueText;
        }
        public int GetCollectedValueIdByCardVersionInstance(int fieldId, int cardId, int version, int instance)
        {
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                                                                  where a.active
                                                                        && a.fk_field == fieldId
                                                                        && a.fk_collectedCard == cardId
                                                                        && a.instance == instance
                                                                        && a.version <= version
                                                                  select a).OrderByDescending(ver => ver.version).FirstOrDefault();
            if (currentCollectedFieldsValues == null)
                return 0;
            return currentCollectedFieldsValues.collectedFieldValueID;
        }
        public List<CollectedFieldsValues> GetCollectedFieldsByCardVersionGroup(int cardId, int groupId, int version)
        {
            return (from a in chancDb.CollectedFieldsValues
            where a.active  
            && a.fk_collectedCard == cardId
            && a.version <= version
            join b in chancDb.Fields
            on a.fk_field equals b.fieldID
            where b.active
            && b.fk_fieldsGroup == groupId
            select a).Distinct().ToList();             
        }
        public int GetLastInstanceByGroupCard(int groupId, int cardId)
        {
            return (from a in chancDb.CollectedFieldsValues
                where a.active && a.fk_collectedCard == cardId
                join b in chancDb.Fields on a.fk_field equals b.fieldID
                where b.active
                      && b.fk_fieldsGroup == groupId
                select a.instance).Distinct().OrderByDescending(mc => mc).FirstOrDefault();
        }
        public int GetLastVersionByCard(int cardId)
        {
            if (cardId == 0) return 0;
            CollectedFieldsValues tmp = (from a in chancDb.CollectedFieldsValues
                where a.active && a.fk_collectedCard == cardId
                select a).OrderByDescending(mc => mc.version).FirstOrDefault();
            if (tmp == null) return 0;
            return tmp.version;
        }
        public int GetMaxValueByFieldRegister(int fieldId, int registerId)
        {
            List<CollectedFieldsValues> values = (from a in chancDb.CollectedFieldsValues
                where a.active
                      && a.fk_field == fieldId
                join b in chancDb.CollectedCards
                    on a.fk_collectedCard equals b.collectedCardID
                where b.active == true
                      && b.fk_register == registerId
                select a).Distinct().ToList();
            List<int> valuesAsInt = new List<int>();
            foreach (CollectedFieldsValues currentValue in values)
            {
                int tmpValue = 0;
                Int32.TryParse(currentValue.valueText, out tmpValue);
                valuesAsInt.Add(tmpValue);
            }
            List<int> valuesSortedList =
                (from a in valuesAsInt select a).Distinct().OrderByDescending(mc => mc).ToList();

            if (valuesSortedList.Any()) return valuesSortedList[0];
            return 0;
        }
        public ListItem[] GetDictionaryValues(int dictionaryId)
        {
            
            List<string> valuesList = (from a in chancDb.DictionarysValues
                where a.active == true
                      && a.fk_dictionary == dictionaryId
                select a.value).ToList();
            ListItem[] resultItems = new ListItem[valuesList.Count];
            for (int i = 0; i < valuesList.Count; i++)
            {
                resultItems[i]= new ListItem(valuesList[i]);
            }
            return resultItems;
        }
    }
    public class DataTypes
    {
        private CardCommonFunctions _common = new CardCommonFunctions();
        public RangeValidator GetRangeValidator(string rangeValidatorId, string fieldToValidateId,string type)
        {
            RangeValidator fieldRangeValidator = new RangeValidator();
            fieldRangeValidator.ID = rangeValidatorId;
            fieldRangeValidator.ControlToValidate = fieldToValidateId;
            fieldRangeValidator.ForeColor = Color.Red;
            switch (type)
            {
                case "bit": //bit
                    {
                        fieldRangeValidator.MinimumValue = 0.ToString();
                        fieldRangeValidator.MaximumValue = 1.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "Только 0 или 1";
                        break;
                    }
                case "int": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "Только целочисленное значение";
                        break;
                    }
                case "float": //double
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Double;
                        fieldRangeValidator.ErrorMessage = "Только числовое значение";
                        break;
                    }
                case "date": //date
                {
                    fieldRangeValidator.MinimumValue = "1/1/1900";
                    fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "Только дата";
                        break;
                    }
                case "singleLineText": //text
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        //fieldRangeValidator.ErrorMessage = "Только текст";
                        fieldRangeValidator.Enabled = false;
                        fieldRangeValidator.Type = ValidationDataType.String;
                        break;
                    }
                case "multiLineText": //text
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        //fieldRangeValidator.ErrorMessage = "Только текст";
                        fieldRangeValidator.Enabled = false;
                        fieldRangeValidator.Type = ValidationDataType.String;
                        break;
                    }
                case "autoIncrement": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "Только целочисленное значение";
                        break;
                    }
                case "autoDate": //date
                    {
                        fieldRangeValidator.MinimumValue = "1/1/1900";
                        fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "Только дата";
                        break;
                    }
                default:
                    {
                        fieldRangeValidator.Enabled = false;
                        break;
                    }
            }
            return fieldRangeValidator;
        }
        public TextBox GetTextBox(string type)
        {
            TextBox textBoxToReturn = new TextBox();
            switch (type)
            {
                case "bit": //bit
                    {
                        break;
                    }
                case "int": //int
                    {
                        break;
                    }
                case "float": //double
                    {
                        
                        break;
                    }
                case "date": //date
                    {
                        textBoxToReturn.TextMode = TextBoxMode.Date;
                        break;
                    }
                case "singleLineText": //text
                    {
                        textBoxToReturn.TextMode = TextBoxMode.SingleLine;
                        break;
                    }
                case "multiLineText": //text
                    {
                        textBoxToReturn.TextMode = TextBoxMode.MultiLine;
                        break;
                    }
                case "singleLineTextWithUniqueCheck": //text
                    {
                        textBoxToReturn.TextMode = TextBoxMode.SingleLine;
                        break;
                    }
                case "autoIncrement": //Автоинкремент
                    {
                        //textBoxToReturn.TextMode = TextBoxMode.SingleLine;
                        break;
                    }
                case "autoDate": // АвтоДата
                    {
                        //textBoxToReturn.TextMode = TextBoxMode.SingleLine;
                        textBoxToReturn.TextMode = TextBoxMode.Date;
                        break;
                    }

            }
            return textBoxToReturn;
        }
        public bool IsFieldAutoFill(string type)
        {
            if (type == "autoIncrement" || type == "autoDate") return true;
            return false;
        }
        public string GetAutoValue(string type,int fieldId,int registerId)
        {
            if (type == "autoDate")
            {
                return DateTime.Now.ToString("u").Split(' ')[0];
            }
            if (type == "autoIncrement")
            {
                int tmp = _common.GetMaxValueByFieldRegister(fieldId, registerId);
                return (tmp + 1).ToString();
            }
            return "";
        }
    }
    public class CardCreateView
    {
        private CardCommonFunctions _common = new CardCommonFunctions();
        private DataTypes _dataTypes = new DataTypes();
        private CardCreateEdit _cardCreateEdit = new CardCreateEdit();
        private int _cardId;
        private int _nextVersion;
        private int _registerId;
        private int _version;
        private int textBoxesIdsCounter = 0;
        public List<TextBox> allFieldsInCard;       
        private void RefreshPage()
        {
            HttpContext.Current.Response.Redirect("CardEdit.aspx", true);
        }
        private void AddInstanceButtonClick(object sender, EventArgs e)
        {
            _cardId = _cardCreateEdit.SaveCard(_registerId, _cardId, allFieldsInCard);
            HttpContext.Current.Session["cardID"]= _cardId;
            ImageButton thisButton = (ImageButton) sender;
            string commandArgument = thisButton.CommandArgument;
            int groupId = Convert.ToInt32(commandArgument);
            int lastInstance = _common.GetLastInstanceByGroupCard(groupId, _cardId);
            int newInstance = lastInstance + 1;
            int lastVersion = _common.GetLastVersionByCard(_cardId);
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            List<Fields> fieldsToCreate = _common.GetFieldsInFieldGroupOrderByLine(groupId);
            foreach (Fields currentField in fieldsToCreate)
            {
                cardCreateEdit.CreateNewFieldValueInCard(_cardId,1,currentField.fieldID,lastVersion+1,newInstance,"",false);
            }
            RefreshPage();
        }
        private void DelInstanceButtonClick(object sender, EventArgs e)
        {
            ImageButton thisButton = (ImageButton)sender;
            string commandArgument = thisButton.CommandArgument;
            int groupId = Convert.ToInt32(commandArgument.Split('_')[0]);
            int instance = Convert.ToInt32(commandArgument.Split('_')[1]);
                   
            int lastVersion = _common.GetLastVersionByCard(_cardId);
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            List<Fields> fieldsToDel = _common.GetFieldsInFieldGroupOrderByLine(groupId);
            foreach (Fields currentField in fieldsToDel)
            {
                cardCreateEdit.CreateNewFieldValueInCard(_cardId, 1, currentField.fieldID, lastVersion + 1, instance, "",true);
            }
            RefreshPage();


        }
        public List<Fields> GetFieldsByLineSortedByColumn(int line, List<Fields> sourseFieldsList)
        {
            List<Fields> fieldsByLineSorted = (from a in sourseFieldsList
                where a.line == line
                select a).OrderBy(col => col.columnNumber).ToList();
            return fieldsByLineSorted;
        }
        public List<int> GetDistinctedSordetLinesFromFieldsList(List<Fields> sourseFieldsList)
        {
            return (from a in sourseFieldsList select a.line).Distinct().OrderBy(li => li).ToList();
        }
        public Table CreateLineTable(List<Fields> fieldsInLine,int leftPaddingSpaceBetween,int cardId, int version,int instance, bool _readonly)
        {
            int leftPadding = leftPaddingSpaceBetween;
            Table lineTable = new Table();
            //в таблице две строки и сколько угодно колонок, каджая колонка один филд
            TableRow tableRow1 = new TableRow(); //названия филдов
            TableRow tableRow2 = new TableRow(); //сам филд  //нашли все филды в лайне            
            foreach (Fields currentField in fieldsInLine)
            {
                TableCell tableCell1 = new TableCell(); //заголовок
                TableCell tableCell2 = new TableCell(); //само поле
                int fieldId = ++textBoxesIdsCounter;                                
                //создадим само поле
                TextBox currentFieldTextBox = _dataTypes.GetTextBox(currentField.type);
                currentFieldTextBox.ReadOnly = _readonly;
                currentFieldTextBox.Style["Height"] = currentField.height + "px";
                currentFieldTextBox.Style["Width"] = currentField.width + "px";
                currentFieldTextBox.Attributes.Add("_myFieldId", currentField.fieldID.ToString());
                currentFieldTextBox.Attributes.Add("_myCollectedFieldInstance", instance.ToString());
                currentFieldTextBox.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.fieldID, cardId, version, instance).ToString());
                currentFieldTextBox.ID = "TextBox" + fieldId;
                //создаем название поля и mandatory валидатор
                Label currentFieldTitle = new Label();
                currentFieldTitle.ID = "Title" + fieldId;
                #region mandatory
                if (currentField.mandatory)
                {
                    currentFieldTitle.Text = currentField.name + "*";
                    RequiredFieldValidator currentFieldRequiredFieldValidator = new RequiredFieldValidator();
                    currentFieldRequiredFieldValidator.ID = "RequiredValidator" + fieldId;
                    currentFieldRequiredFieldValidator.ControlToValidate = currentFieldTextBox.ID;
                    currentFieldRequiredFieldValidator.ErrorMessage = " Обязательно";
                    currentFieldRequiredFieldValidator.ForeColor = Color.Red;
                    tableCell1.Controls.Add(currentFieldTitle);
                    tableCell1.Controls.Add(currentFieldRequiredFieldValidator);                    
                }
                else
                {
                    currentFieldTitle.Text = currentField.name;
                    tableCell1.Controls.Add(currentFieldTitle);
                }
                #endregion
                tableCell1.Controls.Add(_dataTypes.GetRangeValidator("RangeValidator" + fieldId, currentFieldTextBox.ID,currentField.type));
                if (cardId != 0)
                {
                    currentFieldTextBox.Text = _common.GetFieldValueByCardVersionInstance(currentField.fieldID, cardId, version, instance);
                }
                else if (_dataTypes.IsFieldAutoFill(currentField.type))
                {
                    currentFieldTextBox.Text = _dataTypes.GetAutoValue(currentField.type,currentField.fieldID,_registerId);
                    //currentFieldTextBox.Text = "123";
                }
                #region dropdown
                if (currentField.fk_dictionary != null && !_readonly) // создаем выпадающий список
                {
                    currentFieldTextBox.Style["display"] = "none";
                    DropDownList dicrionaryDropDownList = new DropDownList();
                    dicrionaryDropDownList.Style["Height"] = currentField.height + "px";
                    dicrionaryDropDownList.Style["Width"] = currentField.width + "px";
                    dicrionaryDropDownList.ID = "DictionaryDropDown" + fieldId;
                    dicrionaryDropDownList.Attributes.Add("onchange", "document.getElementById('MainContent_" + currentFieldTextBox.ID + "').value=document.getElementById('MainContent_" + dicrionaryDropDownList.ID + "').value;");                    
                    int dictionaryId = currentField.fk_dictionary ?? 0;
                    dicrionaryDropDownList.Items.AddRange(_common.GetDictionaryValues(dictionaryId));
                    if (currentFieldTextBox.Text.Any())
                    {
                        dicrionaryDropDownList.SelectedValue = currentFieldTextBox.Text;
                        if (dicrionaryDropDownList.SelectedValue != currentFieldTextBox.Text) // значение в нашем списке отсутствует
                        {
                            dicrionaryDropDownList.Items.Add(currentFieldTextBox.Text);
                            dicrionaryDropDownList.SelectedValue = currentFieldTextBox.Text;
                        }
                    }                
                    tableCell2.Controls.Add(dicrionaryDropDownList);
                }
                #endregion
                allFieldsInCard.Add(currentFieldTextBox); //запоминаем какие textbox у нас на карте
                tableCell2.Controls.Add(currentFieldTextBox);  
                leftPadding += leftPaddingSpaceBetween + currentField.width;
                tableRow1.Cells.Add(tableCell1);
                tableRow2.Cells.Add(tableCell2);
            }
            lineTable.Rows.Add(tableRow1);
            lineTable.Rows.Add(tableRow2);
            return lineTable;
        }
        private List<int> GetInstancesListByGroupAndVersion(int cardId,int groupId,int version)
        {
            List<CollectedFieldsValues> collectedValues = _common.GetCollectedFieldsByCardVersionGroup(cardId, groupId, version);
            List<int> isDeletedInstances = (from a in collectedValues where a.isDeleted select a.instance).Distinct().ToList();
            List<int> allInstances = (from a in collectedValues select a.instance).Distinct().ToList();
            List<int> onlyActiveInstances = allInstances;
            foreach (int currendDeletedInstance in isDeletedInstances)
            {
                onlyActiveInstances.Remove(currendDeletedInstance);
            }
            return onlyActiveInstances;
        } 
        public Panel CreateViewByRegisterAndCard(int registerId, int cardId , int version, bool _readonly)
        {
            _cardId = cardId;
            _registerId = registerId;
            _nextVersion = _common.GetLastVersionByCard(cardId);

            allFieldsInCard = new List<TextBox>(); // будем запоминать все поля чтобы потом сохранять
            int leftPaddingSpaceBetween = 5; // отступы между полями
            Panel cardMainPanel = new Panel(); // основная панель
            cardMainPanel.Style.Add("border", "1px solid DimGray;");
            Registers currentRegister = _common.GetRegisterById(registerId); // текущий реестр
            RegistersModels currentRegisterModel = _common.GetRegisterModelById(currentRegister.fk_registersModel);
            // модель реестра
            cardMainPanel.GroupingText = currentRegister.name; //Title панели равен названию регистра
            List<FieldsGroups> fieldGroupsToShow =
                _common.GetFieldsGroupsInRegisterModelOrderByLine(currentRegisterModel.registerModelID);
            //группы которые нужно показать
            //нашли все группы
            foreach (FieldsGroups currentFieldGroup in fieldGroupsToShow)
            {
                List<int> instancesList;
                if (cardId == 0)
                {
                    instancesList = new List<int>();
                    instancesList.Add(0);
                }
                else
                {
                    instancesList = GetInstancesListByGroupAndVersion(cardId, currentFieldGroup.fieldsGroupID, version);
                }
                 
                Panel groupPanel = new Panel(); // отдельно по панели на каждую группу
                groupPanel.GroupingText = currentFieldGroup.name;
                int instanceNumber = 0;
                int instancesCount = instancesList.Count;

                foreach (int currentInstance in instancesList)
                {
                    Table instanceTable = new Table();
                    if (instancesCount > 1)
                    {
                        instanceTable.Style.Add("border", "1px solid DarkGray ;");
                        instanceTable.Style.Add("width", "100% ;");
                    }
                    TableRow instanceRow = new TableRow();
                    TableCell instanceCell = new TableCell();
                    TableCell delInstanceCell = new TableCell();
                    //TableCell tableCell2 = new TableCell();
                    //if (instancesCount > 0) instancePanel.GroupingText = (instanceNumber++).ToString();

                    List<Fields> fieldToShow = _common.GetFieldsInFieldGroupOrderByLine(currentFieldGroup.fieldsGroupID);
                    List<int> linesToShow = GetDistinctedSordetLinesFromFieldsList(fieldToShow);
                    //нашли все лайны внутри группы
                    foreach (int currentFieldsline in linesToShow)
                    {
                        List<Fields> fieldsInLine = GetFieldsByLineSortedByColumn(currentFieldsline, fieldToShow);
                        Table tableToAdd = CreateLineTable(fieldsInLine, leftPaddingSpaceBetween, cardId, version,
                            currentInstance, _readonly);

                        instanceCell.Controls.Add(tableToAdd);
                    }
                    instanceRow.Cells.Add(instanceCell);
                    if (instancesCount > 1 && !_readonly)
                    {
                        ImageButton delGroupButton = new ImageButton();
                        delGroupButton.ImageUrl = "~/kanz/icons/delButtonIcon.jpg";
                        delGroupButton.Height = 20;
                        delGroupButton.Width = 20;
                        
                        delGroupButton.CommandArgument = currentFieldGroup.fieldsGroupID+"_"+ currentInstance;
                        delGroupButton.CausesValidation = false;
                        delGroupButton.Click += DelInstanceButtonClick;
                        delInstanceCell.Controls.Add(delGroupButton);
                        instanceRow.Cells.Add(delInstanceCell);
                    }
                    instanceTable.Rows.Add(instanceRow);
                    groupPanel.Controls.Add(instanceTable);
                }
                cardMainPanel.Controls.Add(groupPanel);
                if (currentFieldGroup.multiple && !_readonly)
                {
                    ImageButton addGroupButton = new ImageButton();
                    addGroupButton.ImageUrl = "~/kanz/icons/addButtonIcon.jpg";
                    addGroupButton.Height = 20;
                    addGroupButton.Width = 20;
                    addGroupButton.CommandArgument = currentFieldGroup.fieldsGroupID.ToString();
                    addGroupButton.CausesValidation = false;
                    addGroupButton.Click += AddInstanceButtonClick;
                    cardMainPanel.Controls.Add(addGroupButton);
                }
            }
            return cardMainPanel;

        }
    }
    public class CardCreateEdit
    {
        private int _userId = 1;
        private ChancelerryDBDataContext chancDb = new ChancelerryDBDataContext();
        private CardCommonFunctions _common = new CardCommonFunctions();
        public int CreateNewCardInRegister(int registerId)
        {
            CollectedCards newCard = new CollectedCards();
            newCard.fk_register = registerId;
            newCard.active = true;
            chancDb.CollectedCards.InsertOnSubmit(newCard);
            chancDb.SubmitChanges();
            return newCard.collectedCardID;
        }
        public int CreateNewFieldValueInCard(int cardId, int userId, int fieldId, int version, int instance,
            string textValue,bool isDeleted)
        {
            CollectedFieldsValues newField = new CollectedFieldsValues();
            newField.active = true;
            newField.fk_collectedCard = cardId;
            newField.fk_field = fieldId;
            newField.fk_user = userId;
            newField.createDateTime = DateTime.Now;
            newField.version = version;
            newField.instance = instance;
            newField.valueText = textValue;
            newField.isDeleted = isDeleted;
            chancDb.CollectedFieldsValues.InsertOnSubmit(newField);
            chancDb.SubmitChanges();
            return newField.collectedFieldValueID;
        }
        public int SaveCard(int registerId, int cardId, List<TextBox> fieldsToSave)
        {
            if (cardId == 0)
                cardId = CreateNewCardInRegister(registerId);
            SaveFieldsValues(fieldsToSave, cardId);
            return cardId;
        }
        public void SaveFieldsValues(List<TextBox> fieldsToSave, int cardId)
        {
            int lastVersion = _common.GetLastVersionByCard(cardId);
            foreach (TextBox currentField in fieldsToSave)
            {

                int currentFieldId = Convert.ToInt32(currentField.Attributes["_myFieldId"]);
                int currentCollectedFieldId = Convert.ToInt32(currentField.Attributes["_myCollectedFieldId"]);
                int currentCollectedFieldInstance = Convert.ToInt32(currentField.Attributes["_myCollectedFieldInstance"]);
                if (currentCollectedFieldId == 0) // нет предыдушего значения
                {
                    CreateNewFieldValueInCard(cardId, _userId, currentFieldId, lastVersion + 1,
                        currentCollectedFieldInstance, currentField.Text,false);
                }
                else
                {
                    string value = _common.GetFieldValueByCardVersionInstance(currentFieldId, cardId, lastVersion,
                        currentCollectedFieldInstance);
                    if (value != currentField.Text)
                    {
                        CreateNewFieldValueInCard(cardId, _userId, currentFieldId, lastVersion + 1,
                            currentCollectedFieldInstance, currentField.Text, false);
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using Chancelerry;
using Chancelerry.kanz;
using Npgsql;

namespace Chancelerry.kanz
{
    public class DataPortKoStyl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
    }
    public class CardCommonFunctions
    {
        private TableActions ta = new TableActions();
        ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        public RegistersModels GetRegisterModelById(int registerModelId)
        {
            return
                (from a in chancDb.RegistersModels where a.RegisterModelID == registerModelId && a.Active select a)
                    .FirstOrDefault();
        }

        public List<FieldsGroups> GetFieldsGroupsInRegisterModelOrderByLine(int registerModelId)
        {
            return
                (from a in chancDb.FieldsGroups where a.FkRegisterModel == registerModelId && a.Active select a)
                    .Distinct().OrderBy(li => li.Line).ToList();
        }
        public List<Fields> GetFieldsInFieldGroupOrderByLine(int fieldGroupId)
        {
            return
                (from a in chancDb.Fields where a.FkFieldsGroup == fieldGroupId && a.Active select a).Distinct()
                    .OrderBy(li => li.Line)
                    .ToList();
        }
        public Registers GetRegisterById(int registerId)
        {
            return (from a in chancDb.Registers where a.Active && a.RegisterID == registerId select a).FirstOrDefault();
        }
        public string GetFieldValueByCardVersionInstance(int fieldId, int cardId, int version, int instance)
        {
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                where a.Active
                      && a.FkField == fieldId
                      && a.FkCollectedCard == cardId
                      && a.Instance == instance
                      && a.Version <= version
                select a).OrderByDescending(ver => ver.Version).FirstOrDefault();
            return currentCollectedFieldsValues.ValueText;
        }
        public int GetCollectedValueIdByCardVersionInstance(int fieldId, int cardId, int version, int instance)
        {
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                                                                  where a.Active
                                                                        && a.FkField == fieldId
                                                                        && a.FkCollectedCard == cardId
                                                                        && a.Instance == instance
                                                                        && a.Version <= version
                                                                  select a).OrderByDescending(ver => ver.Version).FirstOrDefault();
            if (currentCollectedFieldsValues == null)
                return 0;
            return currentCollectedFieldsValues.CollectedFieldValueID;
        }
        public List<CollectedFieldsValues> GetCollectedFieldsByCardVersionGroup(int cardId, int groupId, int version)
        {
            return (from a in chancDb.CollectedFieldsValues
            where a.Active  
            && a.FkCollectedCard == cardId
            && a.Version <= version
            join b in chancDb.Fields
            on a.FkField equals b.FieldID
            where b.Active
            && b.FkFieldsGroup == groupId
            select a).Distinct().ToList();             
        }
        public int GetLastInstanceByGroupCard(int groupId, int cardId)
        {
            return (from a in chancDb.CollectedFieldsValues
                where a.Active && a.FkCollectedCard == cardId
                join b in chancDb.Fields on a.FkField equals b.FieldID
                where b.Active
                      && b.FkFieldsGroup == groupId
                select a.Instance).Distinct().OrderByDescending(mc => mc).FirstOrDefault();
        }
        public int GetLastVersionByCard(int cardId)
        {
            if (cardId == 0) return 0;
            CollectedFieldsValues tmp = (from a in chancDb.CollectedFieldsValues
                where a.Active && a.FkCollectedCard == cardId
                select a).OrderByDescending(mc => mc.Version).FirstOrDefault();
            if (tmp == null) return 0;
            return tmp.Version;
        }
        public int GetMaxValueByFieldRegister(int fieldId, int registerId)
        {
            List<CollectedFieldsValues> values = (from a in chancDb.CollectedFieldsValues
                where a.Active
                      && a.FkField == fieldId
                join b in chancDb.CollectedCards
                    on a.FkCollectedCard equals b.CollectedCardID
                where b.Active == true
                      && b.FkRegister == registerId
                select a).Distinct().ToList();
            List<int> valuesAsInt = new List<int>();
            foreach (CollectedFieldsValues currentValue in values)
            {
                int tmpValue = 0;
                Int32.TryParse(currentValue.ValueText, out tmpValue);
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
                where a.Active == true
                      && a.FkDictionary == dictionaryId
                select a.Value).OrderBy(mc=>mc).ToList();
            ListItem[] resultItems = new ListItem[valuesList.Count+1];
            resultItems[0] = new ListItem("");
            for (int i = 0; i < valuesList.Count; i++)
            {
                resultItems[i+1]= new ListItem(valuesList[i]);
            }
            return resultItems;
        }
        private TreeNode RecursiveGetTreeNode(int parentId,List<Struct> structList ,string fieldId,string panelId,string backValue,bool fullStruct)
        {

            TreeNode nodeToReturn=new TreeNode();
            nodeToReturn.SelectAction = TreeNodeSelectAction.Select;
            string value = (from a in structList
                            where a.STRuCtID == parentId
                            select a.Name).FirstOrDefault();
            if (backValue.Length > 2 && fullStruct)
            {
                value = backValue + ", " + value;
            }

            nodeToReturn.Value = value;
                nodeToReturn.Text = (from a in structList
                                     where a.STRuCtID == parentId
                                     select a.Name).FirstOrDefault();
            nodeToReturn.NavigateUrl = "javascript:putValueAndClose('" + nodeToReturn.Value + "','" + fieldId + "','"+ panelId + "')";

            List<Struct> children = (from a in structList
                where a.FkParent == parentId
                select a).ToList();
            foreach (Struct currentStruct in children)
            {
                nodeToReturn.ChildNodes.Add(RecursiveGetTreeNode(currentStruct.STRuCtID, structList, fieldId, panelId, value , fullStruct));
            }

            return nodeToReturn;
        }
        public TreeNode GetStructTreeViewNode(string fieldId,string panelId,bool fullStruct)
        {
            TreeNode nodeToReturn = new TreeNode();
             nodeToReturn.SelectAction = TreeNodeSelectAction.Select;
            
            List<Struct> outStruct = (from a in chancDb.Struct
                where a.Active == true
                select a).ToList();
            nodeToReturn = RecursiveGetTreeNode(2, outStruct, fieldId,panelId, "",fullStruct);

            return nodeToReturn;
        }

        public TableHeaderRow AddSearchHeaderRoFromListWithData(List<int> FieldID,Dictionary<int,string> searchData)
        {
            TableHeaderRow row = new TableHeaderRow();
            row.BorderStyle = BorderStyle.Inset;
            foreach (int elm in FieldID)
            {
                TableCell cell = new TableCell();
                TextBox tb = new TextBox();
                string tmp = "";
                if (searchData!=null) searchData.TryGetValue(elm, out tmp);
                tb.Attributes.Add("_FieldID4search", elm.ToString());
                tb.ID = "searchTb" + elm.ToString();
                tb.Text = tmp;
                tb.Attributes.Add("onkeypress", "return runScript(event);");
                tb.Attributes.Add("onkeydown", "return runScript(event);");
                tb.Attributes.Add("class", "search-field");
                cell.Controls.Add(tb);
                row.Cells.Add(cell);
            }
            return row;
        }

        public string FastSearch(Dictionary<int, string> searchList, int registerId, int userId, Table vTable, int LineFrom, int LineTo)
        {

            ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

            List<int> cardsToShow = new List<int>(); // сюда будем складывать карточки которые нужно показать

            if (searchList != null) //если фильтры есть
            {
                bool isFirst = true;
                foreach (int currentKey in searchList.Keys) // проходимся по каждому фильтру
                {
                    int fieldId = currentKey;
                    string fieldValue = "";
                    searchList.TryGetValue(fieldId, out fieldValue);  //достаем айдишник нашего филда

                    List<int> cardsWithValue = (from a in chancDb.CollectedFieldsValues
                                                where a.Active == true && a.FkField == fieldId && a.ValueText.Contains(fieldValue)
                                                join b in chancDb.CollectedCards on a.FkCollectedCard equals b.CollectedCardID
                                                
                                                where b.Active == true && b.FkRegister == registerId
                                                select a.FkCollectedCard).Distinct().ToList(); // находим все карточки которые соответсвтуют
                    List<int> tmpList;
                    if (isFirst)
                    {
                        tmpList = cardsWithValue;
                    }
                    else
                    {
                        tmpList = (from a in cardsWithValue join b in cardsToShow on a equals b select a).Distinct().ToList();
                    }
                    isFirst = false;
                    cardsToShow = tmpList;
                }
            }
            else // если фильтров нет, показываем все карточки 
            {
                cardsToShow = (from a in chancDb.CollectedCards
                               where a.Active == true
                                && a.FkRegister == registerId
                               select a.CollectedCardID).Distinct().ToList();
            }
            int idFieldId = (from a in chancDb.Fields
                             join b in chancDb.FieldsGroups
                             on a.FkFieldsGroup equals b.FieldsGroupID
                             join c in chancDb.RegistersModels
                             on b.FkRegisterModel equals c.RegisterModelID
                             join d in chancDb.Registers
                             on c.RegisterModelID equals d.FkRegistersModel
                             where d.RegisterID == registerId
                             && a.Type == "autoIncrement"
                             select a.FieldID).FirstOrDefault();


            /*
                       List<CollectedFieldsValues> collectedIds = (from a in chancDb.CollectedFieldsValues
                                                        where cardsToShow.Contains(a.fk_collectedCard)
                                                        && a.fk_field == idFieldId
                                                        select a).ToList();
            */

            // PORT//
            List<CollectedFieldsValues> collectedIds = cardsToShow.Select(card => (from a in chancDb.CollectedFieldsValues where a.FkCollectedCard == card && a.FkField == idFieldId select a).FirstOrDefault()).Where(collectedId => collectedId != null).ToList();
            //PORT //

            Dictionary<int, int> toSort = new Dictionary<int, int>();
            List<int> sortedCardsToShow = new List<int>();
            foreach (int current in cardsToShow)
            {
                int tmp = 0;
                Int32.TryParse((from a in collectedIds where a.FkCollectedCard == current select a).OrderByDescending(mc => mc.Version).FirstOrDefault().ValueText, out tmp);
                toSort.Add(current, tmp);
            }
            sortedCardsToShow = (from a in toSort select a).OrderByDescending(mc => mc.Value).Select(mc => mc.Key).ToList();
            List<int> sortedCutedCardsToShow = new List<int>();
            
                if (LineTo > sortedCardsToShow.Count())
                { 
                LineTo = sortedCardsToShow.Count();
                }
            if (sortedCardsToShow.Count > 0)
                for (int i = LineFrom;i< LineTo;i++)
            {
                sortedCutedCardsToShow.Add(sortedCardsToShow[i]);
            }

            Dictionary<int, string> allFields = (from a in chancDb.RegistersUsersMap
                                                 join b in chancDb.RegistersView
                                                 on a.RegistersUsersMapID equals b.FkRegistersUsersMap
                                                 join c in chancDb.Fields
                                                 on b.FkField equals c.FieldID
                                                 where
                                                 a.FkUser == userId
                                                 && a.FkRegister == registerId
                                                 && b.Active
                                                 select new DataPortKoStyl(){ Name = c.Name, Id = c.FieldID, Weight = b.Weight }).OrderBy(w => w.Weight).ToDictionary(t => t.Id, t => t.Name); // ПроPORTчено


            /*
                        List<CollectedFieldsValues> allValues = (from a in chancDb.CollectedFieldsValues
                                                      where sortedCutedCardsToShow.Contains(a.fk_collectedCard)
                                                      && allFields.Keys.Contains(a.fk_field)
                                                      select a).ToList();
            */

            // PORT /////
            List<CollectedFieldsValues> allValuesTmp = sortedCutedCardsToShow.Select(sorted => (from a in chancDb.CollectedFieldsValues select a).FirstOrDefault()).Where(allValue => allValue != null).ToList();
            List<CollectedFieldsValues> allValues = (from itm in allFields.Keys from elm in allValuesTmp where elm.FkField == itm select elm).ToList();
            //PORT //

            vTable.Rows.Add(AddSearchHeaderRoFromListWithData(allFields.Keys.ToList(), searchList));
            vTable.Rows.Add(ta.AddHeaderRoFromList(allFields.Values.ToList()));

            foreach (int currentCard in sortedCutedCardsToShow)
            {
                TableRow vRow = new TableRow();
                List<string> rowList = new List<string>();
                foreach(int currentField in allFields.Keys)
                {
                    List<CollectedFieldsValues> collectedFields = (from a in allValues where a.FkField == currentField && a.FkCollectedCard == currentCard select a).ToList();
                    int maxInstance = (from a in collectedFields select a.Instance).OrderByDescending(mc => mc).FirstOrDefault();
                    string value="";
                    for (int i=0;i< maxInstance+1;i++)
                    {
                        CollectedFieldsValues tmp2 = (from a in collectedFields where a.Instance == i select a).OrderByDescending(mc => mc.Version).FirstOrDefault();
                        if (tmp2 == null)
                            continue;
                        if (tmp2.IsDeleted)
                            continue;
                        if (maxInstance > 0)
                        {
                            value += i + 1 + ")" + tmp2.ValueText + "<br />";
                        }
                        else
                        {
                            value = tmp2.ValueText;
                        }
                    }
                    //string currentFieldValue = (from a in allValues where a.FkField == currentField && a.FkCollectedCard == currentCard select a.valueText).FirstOrDefault();
                    rowList.Add(value);
                }
                vTable.Rows.Add(ta.AddRowFromList(rowList, currentCard));
            }
            return "Всего:" + sortedCardsToShow.Count() + "  " + "Показано с "+ (LineFrom+1)+ " по "+(LineTo);
        }

    }
    public class DataTypes
    {
        private CardCommonFunctions _common = new CardCommonFunctions();
        public RangeValidator GetRangeValidator(string rangeValidatorId, string fieldToValidateId,string Type)
        {
            RangeValidator fieldRangeValidator = new RangeValidator();
            fieldRangeValidator.ID = rangeValidatorId;
            fieldRangeValidator.ControlToValidate = fieldToValidateId;
            fieldRangeValidator.ForeColor = Color.Red;
            switch (Type)
            {
                case "bit": //bit
                    {
                        fieldRangeValidator.MinimumValue = 0.ToString();
                        fieldRangeValidator.MaximumValue = 1.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        break;
                    }
                case "int": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        break;
                    }
                case "float": //double
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Double;
                        fieldRangeValidator.ErrorMessage = "!";
                        break;
                    }
                case "date": //date
                {
                    fieldRangeValidator.MinimumValue = "1/1/1900";
                    fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "!";
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
                        fieldRangeValidator.ErrorMessage = "!";
                        break;
                    }
                case "autoDate": //date
                    {
                        fieldRangeValidator.MinimumValue = "1/1/1900";
                        fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "!";
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
        public TextBox GetTextBox(string Type)
        {
            TextBox textBoxToReturn = new TextBox();
            switch (Type)
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
        public bool IsFieldAutoFill(string Type)
        {
            if (Type == "autoIncrement" || Type == "autoDate") return true;
            return false;
        }
        public string GetAutoValue(string Type,int fieldId,int registerId)
        {
            if (Type == "autoDate")
            {
                return DateTime.Now.ToString("u").Split(' ')[0];
            }
            if (Type == "autoIncrement")
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
        public Dictionary<string, TextBox> addCntTextBoxes = new Dictionary<string, TextBox>();
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
            TextBox cnt;
            addCntTextBoxes.TryGetValue(commandArgument, out cnt);
            int newRowsCnt = 1;
            if (cnt!=null)
                Int32.TryParse(cnt.Text, out newRowsCnt);
            int groupId = Convert.ToInt32(commandArgument);
            int lastInstance = _common.GetLastInstanceByGroupCard(groupId, _cardId);
            int newInstance = lastInstance + 1;
            int lastVersion = _common.GetLastVersionByCard(_cardId);
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            List<Fields> fieldsToCreate = _common.GetFieldsInFieldGroupOrderByLine(groupId);
            for (int i=0;i< newRowsCnt;i++)
            {
                foreach (Fields currentField in fieldsToCreate)
                {
                    cardCreateEdit.CreateNewFieldValueInCard(_cardId, 1, currentField.FieldID, lastVersion + 1,
                        newInstance, "", false);
                }
                newInstance++;
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
                cardCreateEdit.CreateNewFieldValueInCard(_cardId, 1, currentField.FieldID, lastVersion + 1, instance, "",true);
            }
            RefreshPage();


        }
        public List<Fields> GetFieldsByLineSortedByColumn(int Line, List<Fields> sourseFieldsList)
        {
            List<Fields> fieldsByLineSorted = (from a in sourseFieldsList
                where a.Line == Line
                select a).OrderBy(col => col.ColumnNumber).ToList();
            return fieldsByLineSorted;
        }
        public List<int> GetDistinctedSordetLinesFromFieldsList(List<Fields> sourseFieldsList)
        {
            return (from a in sourseFieldsList select a.Line).Distinct().OrderBy(li => li).ToList();
        }
        public Table CreateLineTable(List<Fields> fieldsInLine,int leftPaddingSpaceBetween,int cardId, int version,int instance, bool _readonly)
        {
            int leftPadding = leftPaddingSpaceBetween;
            Table LineTable = new Table();
            //в таблице две строки и сколько угодно колонок, каджая колонка один филд
            TableRow tableRow1 = new TableRow(); //названия филдов
            TableRow tableRow2 = new TableRow(); //сам филд  //нашли все филды в лайне            
            foreach (Fields currentField in fieldsInLine)
            {
                TableCell tableCell1 = new TableCell(); //заголовок
                TableCell tableCell2 = new TableCell(); //само поле
                int fieldId = ++textBoxesIdsCounter;                                
                //создадим само поле
                TextBox currentFieldTextBox = _dataTypes.GetTextBox(currentField.Type);
                currentFieldTextBox.ReadOnly = _readonly;
                currentFieldTextBox.Style["Height"] = currentField.Height + "px";
                currentFieldTextBox.Style["Width"] = currentField.Width + "px";
                currentFieldTextBox.Style["max-Width"] = currentField.Width + "px";
                currentFieldTextBox.Attributes.Add("_myFieldId", currentField.FieldID.ToString());
                currentFieldTextBox.Attributes.Add("_myCollectedFieldInstance", instance.ToString());
                currentFieldTextBox.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.FieldID, cardId, version, instance).ToString());
                currentFieldTextBox.ID = "TextBox" + fieldId;
                //создаем название поля и mandatory валидатор
                Label currentFieldTitle = new Label();
                currentFieldTitle.ID = "Title" + fieldId;
                #region mandatory
                if (currentField.Mandatory)
                {
                    currentFieldTitle.Text = currentField.Name + "*";
                    RequiredFieldValidator currentFieldRequiredFieldValidator = new RequiredFieldValidator();
                    currentFieldRequiredFieldValidator.ID = "RequiredValidator" + fieldId;
                    currentFieldRequiredFieldValidator.ControlToValidate = currentFieldTextBox.ID;
                    currentFieldRequiredFieldValidator.ErrorMessage = "!";
                    currentFieldRequiredFieldValidator.ForeColor = Color.Red;
                    tableCell1.Controls.Add(currentFieldTitle);
                    tableCell1.Controls.Add(currentFieldRequiredFieldValidator);                    
                }
                else
                {
                    currentFieldTitle.Text = currentField.Name;
                    tableCell1.Controls.Add(currentFieldTitle);
                }
                #endregion
                tableCell1.Controls.Add(_dataTypes.GetRangeValidator("RangeValidator" + fieldId, currentFieldTextBox.ID,currentField.Type));
                if (cardId != 0)
                {
                    currentFieldTextBox.Text = _common.GetFieldValueByCardVersionInstance(currentField.FieldID, cardId, version, instance);
                }
                else if (_dataTypes.IsFieldAutoFill(currentField.Type))
                {
                    currentFieldTextBox.Text = _dataTypes.GetAutoValue(currentField.Type,currentField.FieldID,_registerId);
                    //currentFieldTextBox.Text = "123";
                }
                #region dropdown
                if (currentField.FkDictionary != null && !_readonly) // создаем выпадающий список
                {
                    if (currentField.Type == "singleLineWithDDText")
                    {
                        currentFieldTextBox.Style["Height"] = currentField.Height + "px";
                        currentFieldTextBox.Style["Width"] = currentField.Width + "px";
                        currentFieldTextBox.Style["max-Width"] = currentField.Width + "px";
                        currentFieldTextBox.Style["display"] = "block";
                    }
                    else
                    {
                        currentFieldTextBox.Style["display"] = "none";

                    }
                    
                    DropDownList dicrionaryDropDownList = new DropDownList();
                    dicrionaryDropDownList.Style["Height"] = currentField.Height + "px";
                    dicrionaryDropDownList.Style["Width"] = currentField.Width + "px";
                    dicrionaryDropDownList.ID = "DictionaryDropDown" + fieldId;
                    dicrionaryDropDownList.Attributes.Add("onchange", "if(document.getElementById('MainContent_" + dicrionaryDropDownList.ID + "').value=='')" +
                                                                      "{document.getElementById('MainContent_" + currentFieldTextBox.ID + "').style.visibility = 'visible';} " +
                                                                      "else {document.getElementById('MainContent_" + currentFieldTextBox.ID + "').style.visibility = 'hidden';}" +
                                                                      "document.getElementById('MainContent_" + currentFieldTextBox.ID + "').value=document.getElementById('MainContent_" + dicrionaryDropDownList.ID + "').value;");                    
                    int dictionaryId = currentField.FkDictionary ?? 0;
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
                #region tree
                if (currentField.Type == "fullStruct" || currentField.Type == "onlyLastStruct")
                {
                    currentFieldTextBox.TextMode = TextBoxMode.MultiLine;
                    bool fullStruct = currentField.Type == "fullStruct";
                   ImageButton structButton = new ImageButton();
                    structButton.ImageUrl = "~/kanz/icons/treeButtonIcon.jpg";
                    structButton.Height = 20;
                    structButton.Width = 20;
                    structButton.CausesValidation = false;
                    structButton.OnClientClick = "document.getElementById('MainContent_treeViewPanel" + fieldId + "').style.visibility = 'visible'; return false;";
                    tableCell2.Controls.Add(structButton);

                    Button cancelButton = new Button();
                    cancelButton.CausesValidation = false;
                    cancelButton.Text = "Отмена";
                    cancelButton.Style.Add("float", "bottom");
                    cancelButton.Style.Add("Width", "100%");
                    cancelButton.Style.Add("heigth", "50px");
                    cancelButton.OnClientClick = "document.getElementById('MainContent_treeViewPanel" + fieldId + "').style.visibility = 'hidden'; return false;";

                    Panel treeViewPanel = new Panel();
                    treeViewPanel.ID = "treeViewPanel"+fieldId;
                    treeViewPanel.Style.Add("top", "50%");
                    treeViewPanel.Style.Add("left", "50%");
                    treeViewPanel.Style.Add("margin", "-250px 0px 0px -470px");

                    treeViewPanel.Style.Add("border", "1px solid black");
                    treeViewPanel.Style.Add("z-index", "21");
                    treeViewPanel.Style.Add("position", "fixed");
                    treeViewPanel.Style.Add("background-color", "white");
                    treeViewPanel.Style.Add("visibility", "hidden");
                    treeViewPanel.Style.Add("Height", "500px");
                    treeViewPanel.Style.Add("Width", "940px");


                    Panel scrollPanel = new Panel();
                    scrollPanel.ID = "treeViewScrollPanel" + fieldId;
                    scrollPanel.Style.Add("overflow", "scroll");
                    scrollPanel.Style.Add("Height", "100%");

                    TreeView strucTreeView = new TreeView();
                    strucTreeView.ID = "treeView" + fieldId;
                    scrollPanel.CssClass = "custom-tree";
                    strucTreeView.CssClass = "custom-tree";
                    //strucTreeView.SelectedNodeChanged += treeViewClick;
                    //strucTreeView.ShowCheckBoxes = TreeNodeTypes.All;
                    strucTreeView.Nodes.Add(_common.GetStructTreeViewNode("MainContent_" + currentFieldTextBox.ID, "MainContent_"+ treeViewPanel.ID,fullStruct));
                    
                    scrollPanel.Controls.Add(strucTreeView);
                    treeViewPanel.Controls.Add(scrollPanel);
                    //treeViewPanel.Controls.Add(okButton);
                    treeViewPanel.Controls.Add(cancelButton);
                    tableCell2.Controls.Add(treeViewPanel);                   
                }
                #endregion
                allFieldsInCard.Add(currentFieldTextBox); //запоминаем какие textbox у нас на карте
                tableCell2.Controls.Add(currentFieldTextBox);  
                leftPadding += leftPaddingSpaceBetween + currentField.Width;
                tableRow1.Cells.Add(tableCell1);
                tableRow2.Cells.Add(tableCell2);
            }
            LineTable.Rows.Add(tableRow1);
            LineTable.Rows.Add(tableRow2);
            return LineTable;
        }
        private List<int> GetInstancesListByGroupAndVersion(int cardId,int groupId,int version)
        {
            List<CollectedFieldsValues> collectedValues = _common.GetCollectedFieldsByCardVersionGroup(cardId, groupId, version);
            List<int> IsDeletedInstances = (from a in collectedValues where a.IsDeleted select a.Instance).Distinct().ToList();
            List<int> allInstances = (from a in collectedValues select a.Instance).Distinct().ToList();
            List<int> onlyActiveInstances = allInstances;
            foreach (int currendDeletedInstance in IsDeletedInstances)
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
            RegistersModels currentRegisterModel = _common.GetRegisterModelById(currentRegister.FkRegistersModel);
            // модель реестра
            cardMainPanel.GroupingText = currentRegister.Name; //Title панели равен названию регистра
            List<FieldsGroups> fieldGroupsToShow =
                _common.GetFieldsGroupsInRegisterModelOrderByLine(currentRegisterModel.RegisterModelID);
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
                    instancesList = GetInstancesListByGroupAndVersion(cardId, currentFieldGroup.FieldsGroupID, version);
                }
                 
                Panel groupPanel = new Panel(); // отдельно по панели на каждую группу
                groupPanel.GroupingText = currentFieldGroup.Name;
                int instanceNumber = 0;
                int instancesCount = instancesList.Count;

                foreach (int currentInstance in instancesList)
                {
                    Table instanceTable = new Table();
                    if (instancesCount > 1)
                    {
                        instanceTable.Style.Add("border", "1px solid DarkGray ;");
                        instanceTable.Style.Add("Width", "100% ;");
                    }
                    TableRow instanceRow = new TableRow();
                    TableCell instanceCell = new TableCell();
                    TableCell delInstanceCell = new TableCell();
                    //TableCell tableCell2 = new TableCell();
                    //if (instancesCount > 0) instancePanel.GroupingText = (instanceNumber++).ToString();

                    List<Fields> fieldToShow = _common.GetFieldsInFieldGroupOrderByLine(currentFieldGroup.FieldsGroupID);
                    List<int> LinesToShow = GetDistinctedSordetLinesFromFieldsList(fieldToShow);
                    //нашли все лайны внутри группы
                    foreach (int currentFieldsLine in LinesToShow)
                    {
                        List<Fields> fieldsInLine = GetFieldsByLineSortedByColumn(currentFieldsLine, fieldToShow);
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
                        
                        delGroupButton.CommandArgument = currentFieldGroup.FieldsGroupID+"_"+ currentInstance;
                        delGroupButton.CausesValidation = false;
                        delGroupButton.Click += DelInstanceButtonClick;
                        delInstanceCell.Controls.Add(delGroupButton);
                        instanceRow.Cells.Add(delInstanceCell);
                    }
                    instanceTable.Rows.Add(instanceRow);
                    groupPanel.Controls.Add(instanceTable);
                }
                cardMainPanel.Controls.Add(groupPanel);
                if (currentFieldGroup.Multiple && !_readonly)
                {
                    TextBox inputCnt = new TextBox();
                    inputCnt.TextMode =TextBoxMode.SingleLine;
                    inputCnt.Width = 20;
                    inputCnt.Height = 20;
                    inputCnt.Text = "1";
                    inputCnt.ID = currentFieldGroup.FieldsGroupID.ToString() + "cnt";
                    
                    ImageButton addGroupButton = new ImageButton();
                    addGroupButton.ImageUrl = "~/kanz/icons/addButtonIcon.jpg";
                    addGroupButton.Height = 20;
                    addGroupButton.Width = 20;
                    addGroupButton.CommandArgument = currentFieldGroup.FieldsGroupID.ToString();
                    addGroupButton.CausesValidation = false;
                    addGroupButton.Click += AddInstanceButtonClick;

                    addCntTextBoxes.Add(addGroupButton.CommandArgument, inputCnt);

                    cardMainPanel.Controls.Add(inputCnt);
                    cardMainPanel.Controls.Add(addGroupButton);
                }
            }
            return cardMainPanel;

        }
    }
    public class CardCreateEdit
    {
        private int _userId = 1;
        private ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        private CardCommonFunctions _common = new CardCommonFunctions();
        public int CreateNewCardInRegister(int registerId)
        {
            CollectedCards newCard = new CollectedCards();
            newCard.FkRegister = registerId;
            newCard.Active = true;
            chancDb.CollectedCards.InsertOnSubmit(newCard);
            chancDb.SubmitChanges();
            return newCard.CollectedCardID;
        }
        public int CreateNewFieldValueInCard(int cardId, int userId, int fieldId, int version, int instance,
            string textValue,bool IsDeleted)
        {
            CollectedFieldsValues newField = new CollectedFieldsValues();
            newField.Active = true;
            newField.FkCollectedCard = cardId;
            newField.FkField = fieldId;
            newField.FkUser = userId;
            newField.CreateDateTime = DateTime.Now;
            newField.Version = version;
            newField.Instance = instance;
            newField.ValueText = textValue;
            newField.IsDeleted = IsDeleted;
            chancDb.CollectedFieldsValues.InsertOnSubmit(newField);
            chancDb.SubmitChanges();
            return newField.CollectedFieldValueID;
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
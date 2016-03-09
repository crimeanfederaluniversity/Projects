using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chancelerry;
using Chancelerry.kanz;

namespace Chancelerry.kanz
{
    public class CardCommonFunctions
    {
        private TableActions ta = new TableActions();
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
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                where a.active
                      && a.fk_field == fieldId
                      && a.fk_collectedCard == cardId
                      && a.instance == instance
                      && a.version <= version
                select a).OrderByDescending(ver => ver.version).FirstOrDefault();
            if (currentCollectedFieldsValues == null)
            {
                int userId = 1;
                var sesUserId = HttpContext.Current.Session["userID"];
                if (sesUserId != null)
                {
                    Int32.TryParse(sesUserId.ToString(), out userId);
                }

                cardCreateEdit.CreateNewFieldValueInCard(cardId, userId, fieldId, version, instance, "", false);
                currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                                                where a.active
                                                      && a.fk_field == fieldId
                                                      && a.fk_collectedCard == cardId
                                                      && a.instance == instance
                                                      && a.version <= version
                                                select a).OrderByDescending(ver => ver.version).FirstOrDefault();
            }
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
                select a.value).OrderBy(mc=>mc).ToList();
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
                            where a.structID == parentId
                            select a.name).FirstOrDefault();
            if (backValue.Length > 2 && fullStruct)
            {
                value = backValue + ", " + value;
            }

            nodeToReturn.Value = value;
                nodeToReturn.Text = (from a in structList
                                     where a.structID == parentId
                                     select a.name).FirstOrDefault();
            nodeToReturn.NavigateUrl = "javascript:putValueAndClose('" + nodeToReturn.Value + "','" + fieldId + "','"+ panelId + "')";

            List<Struct> children = (from a in structList
                where a.fk_parent == parentId
                select a).ToList();
            foreach (Struct currentStruct in children)
            {
                nodeToReturn.ChildNodes.Add(RecursiveGetTreeNode(currentStruct.structID, structList, fieldId, panelId, value , fullStruct));
            }

            return nodeToReturn;
        }
        public TreeNode GetStructTreeViewNode(string fieldId,string panelId,bool fullStruct)
        {
            TreeNode nodeToReturn = new TreeNode();
             nodeToReturn.SelectAction = TreeNodeSelectAction.Select;
            
            List<Struct> outStruct = (from a in chancDb.Struct
                where a.active == true
                select a).ToList();
            nodeToReturn = RecursiveGetTreeNode(2, outStruct, fieldId,panelId, "",fullStruct);

            return nodeToReturn;
        }
        public TableHeaderRow AddSearchHeaderRoFromListWithData(List<int> fieldID,Dictionary<int,string> searchData)
        {
            TableHeaderRow row = new TableHeaderRow();
            row.BorderStyle = BorderStyle.Inset;
            foreach (int elm in fieldID)
            {
                TableCell cell = new TableCell();
                TextBox tb = new TextBox();
                string tmp = "";
                if (searchData!=null) searchData.TryGetValue(elm, out tmp);
                tb.Attributes.Add("_fieldID4search", elm.ToString());
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
        public CollectedCards GetCollevtedCardByCollevtedField(int collectedFieldId)
        {
            CollectedCards cardToReturn = (from a in chancDb.CollectedCards
                join b in chancDb.CollectedFieldsValues
                    on a.collectedCardID equals b.fk_collectedCard
                where b.collectedFieldValueID == collectedFieldId
                select a).FirstOrDefault();
            return cardToReturn;
        }
        public  List<int> GetCardsToShow(Dictionary<int, string> searchList,int registerId)
        {
            List<int> cardsToShow = new List<int>();
            if (searchList != null) //если фильтры есть
            {
                bool isFirst = true;
                foreach (int currentKey in searchList.Keys) // проходимся по каждому фильтру
                {
                    int fieldId = currentKey;
                    string fieldValue = "";
                    searchList.TryGetValue(fieldId, out fieldValue); //достаем айдишник нашего филда
                    List<int> cardsWithValue = (from a in chancDb.CollectedFieldsValues
                                                where a.active == true && a.fk_field == fieldId && a.valueText.Contains(fieldValue)
                                                join b in chancDb.CollectedCards on a.fk_collectedCard equals b.collectedCardID

                                                where b.active == true && b.fk_register == registerId
                                                select b).Distinct()
                        .OrderByDescending(uc => uc.mainFieldId)
                        .Select(vk => vk.collectedCardID)
                        .ToList(); // находим все карточки которые соответсвтуют
                    List<int> tmpList;
                    if (isFirst)
                    {
                        tmpList = cardsWithValue;
                    }
                    else
                    {
                        tmpList =
                            (from a in cardsWithValue join b in cardsToShow on a equals b select a).Distinct().ToList();
                    }
                    isFirst = false;
                    cardsToShow = tmpList;
                }
            }
            else // если фильтров нет, показываем все карточки 
            {
                cardsToShow = (from a in chancDb.CollectedCards
                               where a.active == true
                                     && a.fk_register == registerId
                                     && a.mainFieldId != null
                               /////////////// ПЛОХО
                               select a).OrderByDescending(uc => (int) uc.mainFieldId).Select(vk => vk.collectedCardID).ToList();
            }
            return cardsToShow;
        } 
        public string FastSearch(Dictionary<int, string> searchList, int registerId, int userId, Table vTable, int lineFrom, int lineTo)
        {
            List<int> cardsToShow = GetCardsToShow(searchList, registerId);

            /*
            int idFieldId = (from a in chancDb.Fields
                             join b in chancDb.FieldsGroups
                             on a.fk_fieldsGroup equals b.fieldsGroupID
                             join c in chancDb.RegistersModels
                             on b.fk_registerModel equals c.registerModelID
                             join d in chancDb.Registers
                             on c.registerModelID equals d.fk_registersModel
                             where d.registerID == registerId
                             && ( a.type == "autoIncrement" || a.type == "autoIncrementReadonly")
                             select a.fieldID).FirstOrDefault();

            

            List<CollectedFieldsValues> collectedIdstmp = (from a in chancDb.CollectedFieldsValues
                                                            // where cardsToShow.Contains(a.fk_collectedCard)
                                                        where a.fk_field == idFieldId
                                                        select a).ToList();//ToList();
*/
            /*List<CollectedFieldsValues> collectedIds2 = (from a in chancDb.CollectedFieldsValues
                                                         where cardsToShow.Contains(a.fk_collectedCard)
                                                        where a.fk_field == idFieldId
                                                        select a).ToList();*/
            /*
                        List<CollectedFieldsValues> collectedIds =
                            (from a in collectedIdstmp where cardsToShow.Contains(a.fk_collectedCard) select a).ToList();

                        Dictionary<int, int> toSort = new Dictionary<int, int>();
                        List<int> sortedCardsToShow = new List<int>();

                        foreach (int current in cardsToShow)
                        {
                            int tmp = 0;
                            var collectedValue =
                                (from a in collectedIds where a.fk_collectedCard == current select a).OrderByDescending(
                                    mc => mc.version).FirstOrDefault();
                            if (collectedValue != null)
                            {
                                Int32.TryParse(collectedValue.valueText, out tmp);
                            }
                            toSort.Add(current, tmp);
                        }
                        sortedCardsToShow = (from a in toSort select a).OrderByDescending(mc => mc.Value).Select(mc => mc.Key).ToList();
                        */
            List<int> sortedCardsToShow = cardsToShow;
                        List<int> sortedCutedCardsToShow = new List<int>();

                            if (lineTo > sortedCardsToShow.Count())
                            { 
                            lineTo = sortedCardsToShow.Count();
                            }
                        if (sortedCardsToShow.Count > 0)
                            for (int i = lineFrom;i< lineTo;i++)
                        {
                            sortedCutedCardsToShow.Add(sortedCardsToShow[i]);
                        }
                        

           // List<int> sortedCutedCardsToShow = new List<int>();

            Dictionary<int, string> allFields = (from a in chancDb.RegistersUsersMap
                                                 join b in chancDb.RegistersView
                                                 on a.registersUsersMapID equals b.fk_registersUsersMap
                                                 join c in chancDb.Fields
                                                 on b.fk_field equals c.fieldID
                                                 where
                                                 a.fk_user == userId
                                                 && a.fk_register == registerId
                                                 && b.active
                                                 select new { c.name, c.fieldID, b.weight }).OrderBy(w => w.weight).ToDictionary(t => t.fieldID, t => t.name);

            List<CollectedFieldsValues> allValues = (from a in chancDb.CollectedFieldsValues
                                                      where sortedCutedCardsToShow.Contains(a.fk_collectedCard)
                                                      && allFields.Keys.Contains(a.fk_field)
                                                      select a).ToList();

            vTable.Rows.Add(AddSearchHeaderRoFromListWithData(allFields.Keys.ToList(), searchList));
            vTable.Rows.Add(ta.AddHeaderRoFromList(allFields.Values.ToList()));

            foreach (int currentCard in sortedCutedCardsToShow)
            {
                TableRow vRow = new TableRow();
                List<string> rowList = new List<string>();
                foreach(int currentField in allFields.Keys)
                {
                    List<CollectedFieldsValues> collectedFields = (from a in allValues where a.fk_field == currentField && a.fk_collectedCard == currentCard select a).ToList();
                    int maxInstance = (from a in collectedFields select a.instance).OrderByDescending(mc => mc).FirstOrDefault();
                    string value="";
                    for (int i=0;i< maxInstance+1;i++)
                    {
                       List<CollectedFieldsValues> tmp3 = (from a in collectedFields where a.instance == i  select a).OrderByDescending(mc => mc.version).ToList();
                        CollectedFieldsValues tmp2 = null;
                        if (tmp3.Count > 0)
                        {
                            tmp2 = (from a in tmp3 where a.valueText.Length > 0 select a).FirstOrDefault() ;
                        }
                        if (tmp2 == null)
                            continue;
                        if (tmp2.isDeleted)
                            continue;
                        if (maxInstance > 0)
                        {
                            value += i + 1 + ")" + tmp2.valueText + "<br />";
                        }
                        else
                        {
                            value = tmp2.valueText;
                        }
                    }
                    //string currentFieldValue = (from a in allValues where a.fk_field == currentField && a.fk_collectedCard == currentCard select a.valueText).FirstOrDefault();
                    rowList.Add(value);
                }
                vTable.Rows.Add(ta.AddRowFromList(rowList, currentCard));
            }
            return "Всего:" + sortedCardsToShow.Count() + "  " + "Показано с "+ (lineFrom+1)+ " по "+(lineTo);
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
            fieldRangeValidator.Enabled = false;
            switch (type)
            {
                case "bit": //bit
                    {
                        fieldRangeValidator.MinimumValue = 0.ToString();
                        fieldRangeValidator.MaximumValue = 1.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "int": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "float": //double
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Double;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "date": //date
                {
                    fieldRangeValidator.MinimumValue = "1/1/1900";
                    fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "singleLineText": //text
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        //fieldRangeValidator.ErrorMessage = "Только текст";
                        fieldRangeValidator.Enabled = false;
                        fieldRangeValidator.Type = ValidationDataType.String;
                        //fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "multiLineText": //text
                    {
                        //fieldRangeValidator.MinimumValue = double.MinValue.ToString();
                        //fieldRangeValidator.MaximumValue = double.MaxValue.ToString();
                        //fieldRangeValidator.ErrorMessage = "Только текст";
                        fieldRangeValidator.Enabled = false;
                        fieldRangeValidator.Type = ValidationDataType.String;
                        //fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "autoIncrement": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "autoIncrementReadonly": //int
                    {
                        fieldRangeValidator.MinimumValue = int.MinValue.ToString();
                        fieldRangeValidator.MaximumValue = int.MaxValue.ToString();
                        fieldRangeValidator.Type = ValidationDataType.Integer;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                case "autoDate": //date
                    {
                        fieldRangeValidator.MinimumValue = "1/1/1900";
                        fieldRangeValidator.MaximumValue = "1/1/2090";
                        fieldRangeValidator.Type = ValidationDataType.Date;
                        fieldRangeValidator.ErrorMessage = "!";
                        fieldRangeValidator.Enabled = true;
                        break;
                    }
                default:
                    {
                        
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
                        textBoxToReturn.Attributes.Add("onfocus", "this.select();lcs(this)");
                        textBoxToReturn.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");               
                        //textBoxToReturn.TextMode = TextBoxMode.Date;
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
                        textBoxToReturn.Attributes.Add("iamfieldid", "1");
                        break;
                    }
                case "autoIncrementReadonly": //Автоинкремент
                {

                        textBoxToReturn.Attributes.Add("iamfieldid","1");
                    textBoxToReturn.Enabled = true;
                        //textBoxToReturn.BackColor = Color.Aqua;
                        break;
                    }
                case "autoDate": // АвтоДата
                    {
                        textBoxToReturn.Attributes.Add("onfocus", "this.select();lcs(this)");
                        textBoxToReturn.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");
                        //textBoxToReturn.TextMode = TextBoxMode.SingleLine;
                        //textBoxToReturn.TextMode = TextBoxMode.Date;
                        break;
                    }
                case "fileAttach": // АвтоДата
                {
                    textBoxToReturn.Visible = false;
                        break;
                    }

            }
            return textBoxToReturn;
        }
        public bool IsFieldAutoFill(string type)
        {
            if (type == "autoIncrement" || type == "autoDate" || type == "autoIncrementReadonly") return true;
            return false;
        }
        public bool IsFieldDate(string type)
        {
            if (type == "date" || type == "autoDate") return true;
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
            if (type == "autoIncrementReadonly")
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
        public List<FileUpload> allFileUploadsInCard; 
        public Dictionary<string, TextBox> addCntTextBoxes = new Dictionary<string, TextBox>();
        private void RefreshPage()
        {
            HttpContext.Current.Response.Redirect("CardEdit.aspx", true);
        }
        private void AddInstanceButtonClick(object sender, EventArgs e)
        {
            _cardId = _cardCreateEdit.SaveCard(_registerId, _cardId, allFieldsInCard,allFileUploadsInCard);
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

            int userId = 1;
            var sesUserId = HttpContext.Current.Session["userID"];
            if (sesUserId != null)
            {
                Int32.TryParse(sesUserId.ToString(), out userId);
            }

            for (int i=0;i< newRowsCnt;i++)
            {
                foreach (Fields currentField in fieldsToCreate)
                {

                    cardCreateEdit.CreateNewFieldValueInCard(_cardId, userId, currentField.fieldID, lastVersion + 1,
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

            int userId = 1;
            var sesUserId = HttpContext.Current.Session["userID"];
            if (sesUserId != null)
            {
                Int32.TryParse(sesUserId.ToString(), out userId);
            }

            foreach (Fields currentField in fieldsToDel)
            {

                cardCreateEdit.CreateNewFieldValueInCard(_cardId, userId, currentField.fieldID, lastVersion + 1, instance, "",true);
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
        public void OpenDocument(object sender, EventArgs e)
        {
            LinkButton thisButton = (LinkButton)sender;
            int currentFieldId = Convert.ToInt32(thisButton.Attributes["_myFieldId"]);
            int currentCollectedFieldId = Convert.ToInt32(thisButton.Attributes["_myCollectedFieldId"]);
            int currentCollectedFieldInstance = Convert.ToInt32(thisButton.Attributes["_myCollectedFieldInstance"]);
            CollectedCards card = _common.GetCollevtedCardByCollevtedField(currentCollectedFieldId);
            string path = HttpContext.Current.Server.MapPath("~/kanz/attachedDocs/" + card.collectedCardID + "/" + currentCollectedFieldId + "/"+ thisButton.Text);
           // HttpContext.Current.Response.Redirect(path);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + thisButton.Text + ";");
            response.TransmitFile(path);
            response.Flush();
            response.End();
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
               
                if (currentField.type == "fileAttach") //если файлаплоад то текстовое поле вообще не нужно
                {               
                    LinkButton linkToFile = new LinkButton();
                    linkToFile.Click += OpenDocument;
                    linkToFile.Attributes.Add("_myFieldId", currentField.fieldID.ToString());
                    linkToFile.Attributes.Add("_myCollectedFieldInstance", instance.ToString());
                    linkToFile.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.fieldID, cardId, version, instance).ToString());

                    FileUpload fileUpload = new FileUpload();
                    fileUpload.Style["Height"] = currentField.height + "px";
                    fileUpload.Style["Width"] = currentField.width + "px";
                    fileUpload.Style["max-width"] = currentField.width + "px";
                    fileUpload.Attributes.Add("_myFieldId", currentField.fieldID.ToString());
                    fileUpload.Attributes.Add("_myCollectedFieldInstance", instance.ToString());
                    fileUpload.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.fieldID, cardId, version, instance).ToString());
                    fileUpload.ID = "FileUpload" + fieldId;
                    fileUpload.AllowMultiple = false;
                    fileUpload.ValidateRequestMode=ValidateRequestMode.Disabled;
                    Label currentFieldTitle = new Label();
                    currentFieldTitle.ID = "Title" + fieldId;
                    currentFieldTitle.Text = currentField.name;
                    
                    tableCell1.Controls.Add(currentFieldTitle);
                    //tableCell1.Controls.Add(_dataTypes.GetRangeValidator("RangeValidator" + fieldId, fileUpload.ID, currentField.type));
                    if (cardId != 0)
                    {
                        string tmp = _common.GetFieldValueByCardVersionInstance(currentField.fieldID, cardId, version,
                            instance);
                        if (tmp.Length > 3)
                        {
                            linkToFile.Text = tmp;
                            tableCell2.Controls.Add(linkToFile);
                        }
                        else
                        {
                            allFileUploadsInCard.Add(fileUpload); // запоминаем файлаплоады
                            tableCell2.Controls.Add(fileUpload);
                        }
                    }
                    else
                    {
                        allFileUploadsInCard.Add(fileUpload); // запоминаем файлаплоады
                        tableCell2.Controls.Add(fileUpload);
                    }
                }
                else
                {
                    //создадим само поле
                    TextBox currentFieldTextBox = _dataTypes.GetTextBox(currentField.type);
                    if (currentField.type == "autoIncrementReadonly")
                    {
                        currentFieldTextBox.ReadOnly = true;
                    }
                    else
                    {
                        currentFieldTextBox.ReadOnly = _readonly;
                    }
                    
                    currentFieldTextBox.Style["Height"] = currentField.height + "px";
                    currentFieldTextBox.Style["Width"] = currentField.width + "px";
                    currentFieldTextBox.Style["max-width"] = currentField.width + "px";
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
                        currentFieldRequiredFieldValidator.ErrorMessage = "!";
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
                    if (cardId != 0)
                    {
                        currentFieldTextBox.Text = _common.GetFieldValueByCardVersionInstance(currentField.fieldID, cardId, version, instance);
                    }
                    else if (_dataTypes.IsFieldAutoFill(currentField.type))
                    {
                        currentFieldTextBox.Text = _dataTypes.GetAutoValue(currentField.type, currentField.fieldID, _registerId);
                    }
                    #region dropdown
                    if (currentField.fk_dictionary != null && !_readonly) // создаем выпадающий список
                    {
                        if (currentField.type == "singleLineWithDDText")
                        {
                            currentFieldTextBox.Style["Height"] = currentField.height + "px";
                            currentFieldTextBox.Style["Width"] = currentField.width + "px";
                            currentFieldTextBox.Style["max-width"] = currentField.width + "px";
                            currentFieldTextBox.Style["display"] = "block";
                        }
                        else
                        {
                            currentFieldTextBox.Style["display"] = "none";

                        }

                        DropDownList dicrionaryDropDownList = new DropDownList();
                        dicrionaryDropDownList.Style["Height"] = currentField.height + "px";
                        dicrionaryDropDownList.Style["Width"] = currentField.width + "px";
                        dicrionaryDropDownList.ID = "DictionaryDropDown" + fieldId;
                        dicrionaryDropDownList.Attributes.Add("onchange", "if(document.getElementById('MainContent_" + dicrionaryDropDownList.ID + "').value=='')" +
                                                                          "{document.getElementById('MainContent_" + currentFieldTextBox.ID + "').style.visibility = 'visible';} " +
                                                                          "else {document.getElementById('MainContent_" + currentFieldTextBox.ID + "').style.visibility = 'hidden';}" +
                                                                          "document.getElementById('MainContent_" + currentFieldTextBox.ID + "').value=document.getElementById('MainContent_" + dicrionaryDropDownList.ID + "').value;");
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
                    #region tree
                    if (currentField.type == "fullStruct" || currentField.type == "onlyLastStruct")
                    {
                        currentFieldTextBox.TextMode = TextBoxMode.MultiLine;
                        bool fullStruct = currentField.type == "fullStruct";
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
                        cancelButton.Style.Add("width", "100%");
                        cancelButton.Style.Add("heigth", "50px");
                        cancelButton.OnClientClick = "document.getElementById('MainContent_treeViewPanel" + fieldId + "').style.visibility = 'hidden'; return false;";

                        Panel treeViewPanel = new Panel();
                        treeViewPanel.ID = "treeViewPanel" + fieldId;
                        treeViewPanel.Style.Add("top", "50%");
                        treeViewPanel.Style.Add("left", "50%");
                        treeViewPanel.Style.Add("margin", "-250px 0px 0px -470px");

                        treeViewPanel.Style.Add("border", "1px solid black");
                        treeViewPanel.Style.Add("z-index", "21");
                        treeViewPanel.Style.Add("position", "fixed");
                        treeViewPanel.Style.Add("background-color", "white");
                        treeViewPanel.Style.Add("visibility", "hidden");
                        treeViewPanel.Style.Add("height", "500px");
                        treeViewPanel.Style.Add("width", "940px");


                        Panel scrollPanel = new Panel();
                        scrollPanel.ID = "treeViewScrollPanel" + fieldId;
                        scrollPanel.Style.Add("overflow", "scroll");
                        scrollPanel.Style.Add("height", "100%");

                        TreeView strucTreeView = new TreeView();
                        strucTreeView.ID = "treeView" + fieldId;
                        scrollPanel.CssClass = "custom-tree";
                        strucTreeView.CssClass = "custom-tree";
                        //strucTreeView.SelectedNodeChanged += treeViewClick;
                        //strucTreeView.ShowCheckBoxes = TreeNodeTypes.All;
                        strucTreeView.Nodes.Add(_common.GetStructTreeViewNode("MainContent_" + currentFieldTextBox.ID, "MainContent_" + treeViewPanel.ID, fullStruct));

                        scrollPanel.Controls.Add(strucTreeView);
                        treeViewPanel.Controls.Add(scrollPanel);
                        //treeViewPanel.Controls.Add(okButton);
                        treeViewPanel.Controls.Add(cancelButton);
                        tableCell2.Controls.Add(treeViewPanel);
                    }
                    #endregion
                    tableCell1.Controls.Add(_dataTypes.GetRangeValidator("RangeValidator" + fieldId, currentFieldTextBox.ID, currentField.type));
                    allFieldsInCard.Add(currentFieldTextBox); //запоминаем какие textbox у нас на карте
                    tableCell2.Controls.Add(currentFieldTextBox);
                }
                
               
                leftPadding += leftPaddingSpaceBetween + currentField.width;
                tableRow1.Cells.Add(tableCell1);
                tableRow2.Cells.Add(tableCell2);
            }
            lineTable.Rows.Add(tableRow1);
            lineTable.Rows.Add(tableRow2);
            return lineTable;
        }
       // public string 
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
            allFileUploadsInCard = new List<FileUpload>();
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
                    TextBox inputCnt = new TextBox();
                    inputCnt.TextMode =TextBoxMode.SingleLine;
                    inputCnt.Width = 20;
                    inputCnt.Height = 20;
                    inputCnt.Text = "1";
                    inputCnt.ID = currentFieldGroup.fieldsGroupID.ToString() + "cnt";
                    
                    ImageButton addGroupButton = new ImageButton();
                    addGroupButton.ImageUrl = "~/kanz/icons/addButtonIcon.jpg";
                    addGroupButton.Height = 20;
                    addGroupButton.Width = 20;
                    addGroupButton.CommandArgument = currentFieldGroup.fieldsGroupID.ToString();
                    addGroupButton.CausesValidation = false;
                    addGroupButton.Click += AddInstanceButtonClick;

                    addCntTextBoxes.Add(addGroupButton.CommandArgument, inputCnt);

                    cardMainPanel.Controls.Add(inputCnt);
                    cardMainPanel.Controls.Add(addGroupButton);
                }
            }
            return cardMainPanel;

        }
        public Panel GetPrintVersion(int registerId, int cardId , int version ) // версия для печати
        {
            Panel panelToReturn = new Panel();
            Registers currentRegister = _common.GetRegisterById(registerId); // текущий реестр
            List<FieldsGroups> fieldGroupsToShow = _common.GetFieldsGroupsInRegisterModelOrderByLine(currentRegister.fk_registersModel);
            foreach (FieldsGroups currentFieldGroup in fieldGroupsToShow)
            {
                List<int> instancesList = GetInstancesListByGroupAndVersion(cardId, currentFieldGroup.fieldsGroupID,
                    version);
                foreach (int currentInstance in instancesList)
                {
                    bool addThisInstance = false;
                    Table instanceTable = new Table();
                    TableRow instanceRow = new TableRow();
                    TableCell instanceCell = new TableCell();
                    List<Fields> fieldToShow = _common.GetFieldsInFieldGroupOrderByLine(currentFieldGroup.fieldsGroupID);
                    List<int> linesToShow = GetDistinctedSordetLinesFromFieldsList(fieldToShow);
                    Table lineTable = new Table();
                    lineTable.Width = 800;
                    foreach (int currentFieldsline in linesToShow)
                    {
                        
                        TableRow tableRow1 = new TableRow();
                        TableRow tableRow2 = new TableRow();
                        List<Fields> fieldsInLine = GetFieldsByLineSortedByColumn(currentFieldsline, fieldToShow);
                        foreach (Fields currentField in fieldsInLine)
                        {
                            TableCell tableCell1 = new TableCell(); //заголовок
                            Label cell1 = new Label();
                            cell1.Text = currentField.name;
                            tableCell1.Controls.Add(cell1);
                            TableCell tableCell2 = new TableCell(); //само поле
                            Label cell2 = new Label();
                            cell2.Text = _common.GetFieldValueByCardVersionInstance(currentField.fieldID, cardId,
                                version, currentInstance);
                            if (cell2.Text != "")
                            {
                                addThisInstance = true;
                                tableCell2.Controls.Add(cell2);
                                tableRow1.Cells.Add(tableCell1);
                                tableRow2.Cells.Add(tableCell2);
                            }
                        }
                        lineTable.Rows.Add(tableRow1);
                        lineTable.Rows.Add(tableRow2);
                    }

                    if (addThisInstance)
                    {
                        Label hrLabel = new Label();
                        hrLabel.Text = "<hr>";
                        panelToReturn.Controls.Add(hrLabel);
                        instanceCell.Controls.Add(lineTable);
                        instanceRow.Cells.Add(instanceCell);
                        instanceTable.Rows.Add(instanceRow);
                        panelToReturn.Controls.Add(instanceTable);
                    }
                }
                
            }

            return panelToReturn;
        }
    }
    public class CardCreateEdit
    {
      //  private int _userId = 1;
        private ChancelerryDBDataContext chancDb = new ChancelerryDBDataContext();
        private CardCommonFunctions _common = new CardCommonFunctions();
        public int CreateNewCardInRegister(int registerId,int fieldId)
        {
            CollectedCards newCard = new CollectedCards();
            newCard.fk_register = registerId;
            newCard.mainFieldId = fieldId;
            newCard.active = true;
            chancDb.CollectedCards.InsertOnSubmit(newCard);
            chancDb.SubmitChanges();
            
            return newCard.collectedCardID;
        }
        public int CreateNewFieldValueInCard(int cardId, int userId, int fieldId, int version, int instance,
            string textValue,bool isDeleted)
        {
            int lastVersion = _common.GetLastVersionByCard(cardId); // FORPORT
            CollectedFieldsValues newField = new CollectedFieldsValues();
            newField.active = true;
            newField.fk_collectedCard = cardId;
            newField.fk_field = fieldId;
            newField.fk_user = userId;
            newField.createDateTime = DateTime.Now;
            newField.version = lastVersion+1;   // FORPORT  // все 100500 поменять 200500 в проекте
            
            newField.instance = instance;
            newField.valueText = textValue;
            newField.isDeleted = isDeleted;
            chancDb.CollectedFieldsValues.InsertOnSubmit(newField);
            chancDb.SubmitChanges();
            return newField.collectedFieldValueID;
        }
        public int SaveCard(int registerId, int cardId, List<TextBox> fieldsToSave , List<FileUpload> filesToSave)
        {
            int mainfieldId = 0;
            foreach (TextBox currentTextBox in fieldsToSave)
            {
                if (currentTextBox.Attributes["iamfieldid"]!=null)
                {   if (currentTextBox.Attributes["iamfieldid"] == "1")
                {
                    Int32.TryParse(currentTextBox.Text, out mainfieldId);
                }
                }
            }

            if (cardId == 0)
                cardId = CreateNewCardInRegister(registerId, mainfieldId);
            SaveFieldsValues(fieldsToSave, filesToSave, cardId);
            return cardId;
        }
        public void SaveFieldsValues(List<TextBox> fieldsToSave,List<FileUpload> filesToSave, int cardId)
        {
            int lastVersion = _common.GetLastVersionByCard(cardId);
            #region fileUpload
            foreach (FileUpload currentFileUpload in filesToSave)
            {
                if (currentFileUpload.HasFile)
                {
                    try
                    {
                        int userId = 1;
                        var sesUserId = HttpContext.Current.Session["userID"];
                        if (sesUserId != null)
                        {
                            Int32.TryParse(sesUserId.ToString(), out userId);
                        }

                      //  string filename = Path.GetFileName(currentFileUpload.FileName);
                        /*string cardFolder = HttpContext.Current.Server.MapPath("~/kanz/attachedDocs/"+ cardId);
                        if (!Directory.Exists(cardFolder))
                        {
                            Directory.CreateDirectory(cardFolder);
                        }
                        */
                        int currentFieldId = Convert.ToInt32(currentFileUpload.Attributes["_myFieldId"]);
                        int currentCollectedFieldId = Convert.ToInt32(currentFileUpload.Attributes["_myCollectedFieldId"]);
                        int currentCollectedFieldInstance = Convert.ToInt32(currentFileUpload.Attributes["_myCollectedFieldInstance"]);
                        if (currentCollectedFieldId == 0) // нет предыдушего значения
                        {
                            currentCollectedFieldId = CreateNewFieldValueInCard(cardId, userId, currentFieldId,
                                lastVersion + 1,
                                currentCollectedFieldInstance, currentFileUpload.FileName, false);
                        }
                        else
                        {
                            currentCollectedFieldId =  CreateNewFieldValueInCard(cardId, userId, currentFieldId, lastVersion + 1,
                                    currentCollectedFieldInstance, currentFileUpload.FileName, false);
                        }
                        string fieldFolder = HttpContext.Current.Server.MapPath("~/kanz/attachedDocs/" + cardId + "/" + currentCollectedFieldId + "/");

                        if (!Directory.Exists(fieldFolder))
                        {
                            Directory.CreateDirectory(fieldFolder);
                        }
                        
                        currentFileUpload.SaveAs(fieldFolder + currentFileUpload.FileName);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }
            #endregion
            #region fieldValues
            foreach (TextBox currentField in fieldsToSave)
            {
                int userId = 1;
                var sesUserId = HttpContext.Current.Session["userID"];
                if (sesUserId != null)
                {
                    Int32.TryParse(sesUserId.ToString(), out userId);
                }

                int currentFieldId = Convert.ToInt32(currentField.Attributes["_myFieldId"]);
                int currentCollectedFieldId = Convert.ToInt32(currentField.Attributes["_myCollectedFieldId"]);
                int currentCollectedFieldInstance = Convert.ToInt32(currentField.Attributes["_myCollectedFieldInstance"]);
                if (currentCollectedFieldId == 0) // нет предыдушего значения
                {
                    CreateNewFieldValueInCard(cardId, userId, currentFieldId, lastVersion + 1,
                        currentCollectedFieldInstance, currentField.Text,false);
                }
                else
                {
                    string value = _common.GetFieldValueByCardVersionInstance(currentFieldId, cardId, lastVersion,
                        currentCollectedFieldInstance);
                    if (value != currentField.Text)
                    {
                        CreateNewFieldValueInCard(cardId, userId, currentFieldId, lastVersion + 1,
                            currentCollectedFieldInstance, currentField.Text, false);
                    }
                }
            }
            #endregion
        }       
    }
}
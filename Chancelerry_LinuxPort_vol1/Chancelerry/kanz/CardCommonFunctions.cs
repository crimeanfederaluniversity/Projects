using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Providers.Entities;
using System.Web.UI;
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
        private ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
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
        public string GetFieldValueByCardVersionInstance(int fieldId, int cardId, int Version, int Instance)
        {
            CardCreateEdit cardCreateEdit = new CardCreateEdit();
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                                                                  where a.Active
                                                                        && a.FkField == fieldId
                                                                        && a.FkCollectedCard == cardId
                                                                        && a.Instance == Instance
                                                                        && a.Version <= Version
                                                                  select a).OrderByDescending(ver => ver.Version).FirstOrDefault();
            if (currentCollectedFieldsValues == null)
            {
                int userId = 1;
                var sesUserId = HttpContext.Current.Session["userID"];
                if (sesUserId != null)
                {
                    Int32.TryParse(sesUserId.ToString(), out userId);
                }

                cardCreateEdit.CreateNewFieldValueInCard(cardId, userId, fieldId, Version, Instance, "", false);
                currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                                                where a.Active
                                                      && a.FkField == fieldId
                                                      && a.FkCollectedCard == cardId
                                                      && a.Instance == Instance
                                                      && a.Version <= Version
                                                select a).OrderByDescending(ver => ver.Version).FirstOrDefault();
            }
            return currentCollectedFieldsValues.ValueText;
        }
        public int GetCollectedValueIdByCardVersionInstance(int fieldId, int cardId, int Version, int Instance)
        {
            CollectedFieldsValues currentCollectedFieldsValues = (from a in chancDb.CollectedFieldsValues
                                                                  where a.Active
                                                                        && a.FkField == fieldId
                                                                        && a.FkCollectedCard == cardId
                                                                        && a.Instance == Instance
                                                                        && a.Version <= Version
                                                                  select a).OrderByDescending(ver => ver.Version).FirstOrDefault();
            if (currentCollectedFieldsValues == null)
                return 0;
            return currentCollectedFieldsValues.CollectedFieldValueID;
        }
        public List<CollectedFieldsValues> GetCollectedFieldsByCardVersionGroup(int cardId, int groupId, int Version)
        {
            return (from a in chancDb.CollectedFieldsValues
                    where a.Active
                    && a.FkCollectedCard == cardId
                    && a.Version <= Version
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
            if (dictionaryId == 11)
            {
                List<string> valuesList = (from a in chancDb.CollectedFieldsValues
                                           where a.Active == true
                                           && a.FkField == 119
                                           join b in chancDb.CollectedCards
                                           on a.FkCollectedCard equals b.CollectedCardID
                                           where b.Active == true
                                           && b.FkRegister == 10
                                           select a.ValueText).Distinct().OrderBy(mc => mc).ToList();
                ListItem[] resultItems = new ListItem[valuesList.Count];
               // resultItems[0] = new ListItem("");
                for (int i = 0; i < valuesList.Count; i++)
                {
                    resultItems[i] = new ListItem(valuesList[i]);
                }
                return resultItems;
            }
            else
            {
                List<string> valuesList = (from a in chancDb.DictionarysValues
                    where a.Active == true
                          && a.FkDictionary == dictionaryId
                    select a.Value).OrderBy(mc => mc).ToList();
                ListItem[] resultItems = new ListItem[valuesList.Count + 1];
                resultItems[0] = new ListItem("");
                for (int i = 0; i < valuesList.Count; i++)
                {
                    resultItems[i + 1] = new ListItem(valuesList[i]);
                }
                return resultItems;
            }
        }
        private TreeNode RecursiveGetTreeNode(int parentId, List<Struct> structList, string fieldId, string panelId, string backValue, bool fullStruct)
        {

            TreeNode nodeToReturn = new TreeNode();
            nodeToReturn.SelectAction = TreeNodeSelectAction.Select;
            string Value = (from a in structList
                            where a.STRuCtID == parentId
                            select a.Name).FirstOrDefault();
            if (backValue.Length > 2 && fullStruct)
            {
                Value = backValue + ", " + Value;
            }

            nodeToReturn.Value = Value;
            nodeToReturn.Text = (from a in structList
                                 where a.STRuCtID == parentId
                                 select a.Name).FirstOrDefault();
            nodeToReturn.NavigateUrl = "javascript:putValueAndClose('" + nodeToReturn.Value + "','" + fieldId + "','" + panelId + "')";

            List<Struct> children = (from a in structList
                                     where a.FkParent == parentId
                                     select a).ToList();
            foreach (Struct currentStruct in children)
            {
                nodeToReturn.ChildNodes.Add(RecursiveGetTreeNode(currentStruct.STRuCtID, structList, fieldId, panelId, Value, fullStruct));
            }

            return nodeToReturn;
        }
        public TreeNode GetStructTreeViewNode(string fieldId, string panelId, bool fullStruct)
        {
            TreeNode nodeToReturn = new TreeNode();
            nodeToReturn.SelectAction = TreeNodeSelectAction.Select;

            List<Struct> outStruct = (from a in chancDb.Struct
                                      where a.Active == true
                                      select a).ToList();
            nodeToReturn = RecursiveGetTreeNode(2, outStruct, fieldId, panelId, "", fullStruct);

            return nodeToReturn;
        }
        public TableHeaderRow AddSearchHeaderRoFromListWithData(List<int> FieldID, Dictionary<int, string> searchData)
        {
            TableHeaderRow row = new TableHeaderRow();
            row.BorderStyle = BorderStyle.Inset;
            foreach (int elm in FieldID)
            {
                TableCell cell = new TableCell();
                TextBox tb = new TextBox();
                string tmp = "";
                if (searchData != null) searchData.TryGetValue(elm, out tmp);
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
        public CollectedCards GetCollevtedCardByCollevtedField(int collectedFieldId)
        {
            CollectedCards cardToReturn = (from a in chancDb.CollectedCards
                                           join b in chancDb.CollectedFieldsValues
                                               on a.CollectedCardID equals b.FkCollectedCard
                                           where b.CollectedFieldValueID == collectedFieldId
                                           select a).FirstOrDefault();
            return cardToReturn;
        }
        public int totalCnt = 0;
        public List<int> GetCardsToShow(string cardId, Dictionary<int, string> searchList,string searchAll, int registerId, int lineFrom, int lineTo) ///NEW
        {
            List<int> cardsToShow = new List<int>();
            if (cardId != null)
            {
                if (cardId != null)
                {
                    if (cardId.Length > 0)
                    {
                        int cardIdI = 0;
                        Int32.TryParse(cardId, out cardIdI);
                        if (cardIdI  != 0)
                        {
                             cardsToShow = (from a in chancDb.CollectedCards
                                                                    where a.Active == true 
                                                                    && a.MaInFieldID == cardIdI
                                                                    && a.FkRegister == registerId
                                                                    select a).Distinct().OrderByDescending(uc => uc.MaInFieldID).ToList().Select(mc=>mc.CollectedCardID).ToList();
                        }
                    }
                }
            }
            else if (searchList != null) //если фильтры есть
            {
                bool isFirst = true;
                foreach (int fieldId in searchList.Keys) // проходимся по каждому фильтру
                {
                    //int fieldId = currentKey;
                    string fieldValue = "";
                    searchList.TryGetValue(fieldId, out fieldValue); //достаем айдишник нашего филда
                    List<CollectedCards> cardsWithValue1 = (from a in chancDb.CollectedFieldsValues
                        where a.Active == true && a.FkField == fieldId && a.ValueText.Contains(fieldValue)
                        join b in chancDb.CollectedCards on a.FkCollectedCard equals b.CollectedCardID
                        where b.Active == true && b.FkRegister == registerId
                        select b).Distinct().OrderByDescending(uc => uc.MaInFieldID).ToList();

                    List<int> cardsWithValue = (from a in cardsWithValue1 select a.CollectedCardID).ToList();
                                                /*
                        .
                        .Select(vk => vk.CollectedCardID)
                        .ToList();*/ // находим все карточки которые соответсвтуют
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
                totalCnt = cardsToShow.Count;
                cardsToShow = cardsToShow.Skip(lineFrom).Take(lineTo - lineFrom).ToList();
            }
            else if (searchAll!=null) //фильтр по всему реестру
            {
               /* List<CollectedCards> cardss = (from a in chancDb.CollectedCards
                                               where a.Active == true
                                                     && a.FkRegister == registerId
                                                     && a.MaInFieldID != null // ПЛОХО
                                                     join b in chancDb.CollectedFieldsValues
                                                     on a.CollectedCardID equals b.FkCollectedCard
                                               where b.Active == true
                                               && b.ValueText.Contains(searchAll)
                                               select a).OrderByDescending(uc => (int)uc.MaInFieldID).Skip(lineFrom).Take(lineTo - lineFrom).ToList();
*/
                cardsToShow = (from a in chancDb.CollectedCards
                               where a.Active == true
                                     && a.FkRegister == registerId
                                     && a.MaInFieldID != null // ПЛОХО
                               join b in chancDb.CollectedFieldsValues
                                               on a.CollectedCardID equals b.FkCollectedCard
                               where b.Active == true
                               && b.ValueText.Contains(searchAll)
                               select a).OrderByDescending(uc => (int)uc.MaInFieldID).Select(vk => vk.CollectedCardID).ToList().Distinct().ToList();
                totalCnt = cardsToShow.Count;
                cardsToShow = cardsToShow.Skip(lineFrom).Take(lineTo - lineFrom).ToList();

            }
            else // если фильтров нет, показываем все карточки 
            {
                /*List<CollectedCards> cardss = (from a in chancDb.CollectedCards
                                               where a.Active == true
                                                     && a.FkRegister == registerId
                                                     && a.MaInFieldID != null // ПЛОХО
                                               select a).OrderByDescending(uc => (int)uc.MaInFieldID).Skip(lineFrom).Take(lineTo - lineFrom).ToList();
*/
                cardsToShow = (from a in chancDb.CollectedCards
                               where a.Active == true
                                     && a.FkRegister == registerId
                                     && a.MaInFieldID != null // ПЛОХО
                               select a).OrderByDescending(uc => (int)uc.MaInFieldID).Select(vk => vk.CollectedCardID).ToList();
                totalCnt = cardsToShow.Count;
                cardsToShow = cardsToShow.Skip(lineFrom).Take(lineTo - lineFrom).ToList();
            }
            return cardsToShow;
        }

        public class NameFieldWeightClass
        {
            public string name { get; set; }
            public int FieldID { get; set; }
            public double Weight { get; set; }
        }



        public string FastSearch(string cardId,Dictionary<int, string> searchList,string searchAll, int registerId, int userId, Table vTable, int LineFrom, int LineTo)
        {
            #region GetSortedCutedCardsToShow
            List<int> sortedCutedCardsToShow = GetCardsToShow(cardId,searchList, searchAll, registerId, LineFrom, LineTo); // NEW           
            Dictionary<int, string> allFields = (from a in chancDb.RegistersUsersMap
                                                 join b in chancDb.RegistersView
                                                 on a.RegistersUsersMapID equals b.FkRegistersUsersMap
                                                 join c in chancDb.Fields
                                                 on b.FkField equals c.FieldID
                                                 where
                                                 a.FkUser == userId
                                                 && a.FkRegister == registerId
                                                 && b.Active
                                                 && c.Active
                                                 && a.Active
                                                 select new NameFieldWeightClass {name = c.Name,FieldID = c.FieldID,Weight = b.Weight }).OrderBy(w => w.Weight).ToDictionary(t => t.FieldID, t => t.name);

            if (sortedCutedCardsToShow.Count == 0 || allFields.Count == 0)
                return "Данных нет";


            string sqlquery = "Select * from \"CollectedFieldsValues\" where fk_collectedcard IN (" + string.Join(",", sortedCutedCardsToShow.ToArray()) + ")" +
                "AND  fk_field IN (" + string.Join(",", allFields.Keys.ToArray()) + ")";

            List<CollectedFieldsValues> allValues = chancDb.ExecuteQuery<CollectedFieldsValues>(sqlquery).ToList();
            vTable.Rows.Add(AddSearchHeaderRoFromListWithData(allFields.Keys.ToList(), searchList));
            vTable.Rows.Add(ta.AddHeaderRoFromList(allFields.Values.ToList()));
            #endregion
            #region CreateTable
            foreach (int currentCard in sortedCutedCardsToShow)
            {
                int maxInstanceInCard =
                    (from a in allValues where a.FkCollectedCard == currentCard select a.Instance).OrderByDescending(
                        mc => mc).FirstOrDefault() ;
                int fieldsCount = allFields.Count;

                string [,] arrayOfStrings = new string [fieldsCount,maxInstanceInCard+1];
                for (int i=0;i< fieldsCount ;i++)
                {
                    for (int j=0; j < maxInstanceInCard+1; j++)
                    {
                        arrayOfStrings[i, j] = "~null~";
                    }
                }

                int fieldN = 0;
                foreach (int currentField in allFields.Keys)
                {
                    List<CollectedFieldsValues> collectedFields = (from a in allValues where a.FkField == currentField && a.FkCollectedCard == currentCard select a).ToList();     
                    for (int i = 0;i < maxInstanceInCard+1;i++)
                    {
                        List<CollectedFieldsValues> tmp3 = (from a in collectedFields where a.Instance == i select a).OrderByDescending(mc => mc.Version).ToList();
                        CollectedFieldsValues tmp2 = null;
                        if (tmp3.Count > 0)
                        {
                            tmp2 = (from a in tmp3 where a.ValueText.Length > 0 select a).FirstOrDefault();
                            arrayOfStrings[fieldN, i] = "";
                        }
                        if (tmp2 == null)
                            continue;
                        if (tmp2.IsDeleted)
                            continue;
                        arrayOfStrings[fieldN, i] = tmp2.ValueText;
                    }
                    fieldN++;
                }
                //TableRow row = new TableRow();

                int rowCnt = 1;
                for (int j = 0; j < maxInstanceInCard + 1; j++)
                {
                    TableRow instRow = new TableRow();
                    for (int i = 0; i < fieldsCount; i++)
                    {
                        TableCell cell0 = new TableCell();
                        cell0.BorderStyle = BorderStyle.Solid;
                        if (arrayOfStrings[i, j] == "~null~")
                        {
                            if (j!=0)
                                 continue;
                            arrayOfStrings[i, j] = "";
                        }
                        cell0.Text = arrayOfStrings[i, j];
                        int colSpanCheckCounter = j+1;
                        int colspanTmp = 1;

                        while (colSpanCheckCounter<= maxInstanceInCard && arrayOfStrings[i, colSpanCheckCounter]== "~null~")
                        {
                            colspanTmp++;
                            colSpanCheckCounter++;
                        }
                        cell0.RowSpan = colspanTmp;
                        instRow.Cells.Add(cell0);
                        rowCnt++;

                    }
                    instRow.BorderStyle = BorderStyle.Solid;
                    vTable.Rows.Add(instRow);
                    if (j == 0)
                    {
                        TableCell cell = new TableCell();
                        ImageButton buttonEdit = new ImageButton();
                        buttonEdit.ImageUrl = "~/kanz/icons/editaddButtonIcon.gif";
                        buttonEdit.Height = 20;
                        buttonEdit.Width = 20;
                        buttonEdit.Attributes.Add("_cardID", currentCard.ToString());
                        buttonEdit.Click += ta.RedirectToEdit;
                        ImageButton buttonView = new ImageButton();
                        buttonView.Height = 20;
                        buttonView.Width = 20;
                        buttonView.ImageUrl = "~/kanz/icons/viewButtonIcon.png";
                        buttonView.Attributes.Add("_cardID", currentCard.ToString());
                        buttonView.Click += ta.RedirectToView;
                        ImageButton buttonDelete = new ImageButton();
                        buttonDelete.Height = 20;
                        buttonDelete.Width = 20;
                        buttonDelete.ImageUrl = "~/kanz/icons/delButtonIcon.jpg";
                        buttonDelete.Attributes.Add("_cardID", currentCard.ToString());
                        buttonDelete.Click += ta.DeleteCard;
                        buttonDelete.OnClientClick = "return confirm('Вы уверены, что хотите удалить?');";
                        cell.Controls.Add(buttonEdit);
                        cell.Controls.Add(buttonView);
                        cell.Controls.Add(buttonDelete);
                        cell.RowSpan = maxInstanceInCard+1;
                        instRow.Cells.Add(cell);
                    }
                }
            }
            #endregion
            return "Всего:" + totalCnt.ToString() + "  " + "Показано с " + (LineFrom + 1) + " по " + (LineTo);
        }
    }















    public class DataTypes
    {
        private CardCommonFunctions _common = new CardCommonFunctions();
        public RangeValidator GetRangeValidator(string rangeValidatorId, string fieldToValidateId, string Type)
        {

            RangeValidator fieldRangeValidator = new RangeValidator();
            fieldRangeValidator.ID = rangeValidatorId;
            fieldRangeValidator.ControlToValidate = fieldToValidateId;
            fieldRangeValidator.ForeColor = Color.Red;
            fieldRangeValidator.Enabled = false;
            switch (Type)
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

                        textBoxToReturn.Attributes.Add("iamfieldid", "1");
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
        public bool IsFieldAutoFill(string Type)
        {
            if (Type == "autoIncrement" || Type == "autoDate" || Type == "autoIncrementReadonly") return true;
            return false;
        }
        public bool IsFieldDate(string Type)
        {
            if (Type == "date" || Type == "autoDate") return true;
            return false;
        }
        public string GetAutoValue(string Type, int fieldId, int registerId)
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
            if (Type == "autoIncrementReadonly")
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
        private int _Version;
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
            _cardId = _cardCreateEdit.SaveCard(_registerId, _cardId, allFieldsInCard, allFileUploadsInCard);
            HttpContext.Current.Session["cardID"] = _cardId;
            ImageButton thisButton = (ImageButton)sender;
            string commandArgument = thisButton.CommandArgument;
            TextBox cnt;
            addCntTextBoxes.TryGetValue(commandArgument, out cnt);
            int newRowsCnt = 1;
            if (cnt != null)
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

            for (int i = 0; i < newRowsCnt; i++)
            {
                foreach (Fields currentField in fieldsToCreate)
                {

                    cardCreateEdit.CreateNewFieldValueInCard(_cardId, userId, currentField.FieldID, lastVersion + 1,
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
            int Instance = Convert.ToInt32(commandArgument.Split('_')[1]);

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

                cardCreateEdit.CreateNewFieldValueInCard(_cardId, userId, currentField.FieldID, lastVersion + 1, Instance, "", true);
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
        public void OpenDocument(object sender, EventArgs e)
        {
            LinkButton thisButton = (LinkButton)sender;
            int currentFieldId = Convert.ToInt32(thisButton.Attributes["_myFieldId"]);
            int currentCollectedFieldId = Convert.ToInt32(thisButton.Attributes["_myCollectedFieldId"]);
            int currentCollectedFieldInstance = Convert.ToInt32(thisButton.Attributes["_myCollectedFieldInstance"]);
            CollectedCards card = _common.GetCollevtedCardByCollevtedField(currentCollectedFieldId);
            string path = HttpContext.Current.Server.MapPath("~/kanz/attachedDocs/" + card.CollectedCardID + "/" + currentCollectedFieldId + "/" + thisButton.Text);
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
        public Table CreateLineTable(List<Fields> fieldsInLine, int leftPaddingSpaceBetween, int cardId, int Version, int Instance, bool _readonly)
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

                if (currentField.Type == "fileAttach") //если файлаплоад то текстовое поле вообще не нужно
                {
                    LinkButton linkToFile = new LinkButton();
                    linkToFile.Click += OpenDocument;
                    linkToFile.Attributes.Add("_myFieldId", currentField.FieldID.ToString());
                    linkToFile.Attributes.Add("_myCollectedFieldInstance", Instance.ToString());
                    linkToFile.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.FieldID, cardId, Version, Instance).ToString());

                    FileUpload fileUpload = new FileUpload();
                    fileUpload.Style["Height"] = currentField.Height + "px";
                    fileUpload.Style["Width"] = currentField.Width + "px";
                    fileUpload.Style["max-width"] = currentField.Width + "px";
                    fileUpload.Attributes.Add("_myFieldId", currentField.FieldID.ToString());
                    fileUpload.Attributes.Add("_myCollectedFieldInstance", Instance.ToString());
                    fileUpload.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.FieldID, cardId, Version, Instance).ToString());
                    fileUpload.ID = "FileUpload" + fieldId;
                    Label currentFieldTitle = new Label();
                    currentFieldTitle.ID = "Title" + fieldId;
                    currentFieldTitle.Text = currentField.Name;

                    tableCell1.Controls.Add(currentFieldTitle);
                    //tableCell1.Controls.Add(_dataTypes.GetRangeValidator("RangeValidator" + fieldId, fileUpload.ID, currentField..Type));
                    if (cardId != 0)
                    {
                        string tmp = _common.GetFieldValueByCardVersionInstance(currentField.FieldID, cardId, Version,
                            Instance);
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
                    TextBox currentFieldTextBox = _dataTypes.GetTextBox(currentField.Type);
                    if (currentField.Type == "autoIncrementReadonly")
                    {
                        currentFieldTextBox.ReadOnly = true;
                    }
                    else
                    {
                        currentFieldTextBox.ReadOnly = _readonly;
                    }

                    currentFieldTextBox.Style["Height"] = currentField.Height + "px";
                    currentFieldTextBox.Style["Width"] = currentField.Width + "px";
                    currentFieldTextBox.Style["max-width"] = currentField.Width + "px";
                    currentFieldTextBox.Attributes.Add("_myFieldId", currentField.FieldID.ToString());
                    currentFieldTextBox.Attributes.Add("_myCollectedFieldInstance", Instance.ToString());
                    currentFieldTextBox.Attributes.Add("_myCollectedFieldId", _common.GetCollectedValueIdByCardVersionInstance(currentField.FieldID, cardId, Version, Instance).ToString());
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
                    if (cardId != 0)
                    {
                        currentFieldTextBox.Text = _common.GetFieldValueByCardVersionInstance(currentField.FieldID,
                            cardId, Version, Instance);//Trim();
                    }
                    else if (_dataTypes.IsFieldAutoFill(currentField.Type))
                    {
                        currentFieldTextBox.Text = _dataTypes.GetAutoValue(currentField.Type, currentField.FieldID,
                            _registerId);//.Trim();
                    }
                    else
                    {
                        currentFieldTextBox.Text = "";
                    }
                    #region dropdown
                    if (currentField.FkDictionary != null && !_readonly) // создаем выпадающий список
                    {
                        if (currentField.Type == "singleLineWithDDText")
                        {
                            currentFieldTextBox.Style["Height"] = currentField.Height + "px";
                            currentFieldTextBox.Style["Width"] = currentField.Width + "px";
                            currentFieldTextBox.Style["max-width"] = currentField.Width + "px";
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
                        dicrionaryDropDownList.Attributes.Add("onchange", "if(document.getElementById('ctl00_MainContent_" + dicrionaryDropDownList.ID + "').value=='')" +
                                                                          "{document.getElementById('ctl00_MainContent_" + currentFieldTextBox.ID + "').style.visibility = 'visible';} " +
                                                                          "else {document.getElementById('ctl00_MainContent_" + currentFieldTextBox.ID + "').style.visibility = 'hidden';}" +
                                                                          "document.getElementById('ctl00_MainContent_" + currentFieldTextBox.ID + "').value=document.getElementById('ctl00_MainContent_" + dicrionaryDropDownList.ID + "').value;");
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
                        structButton.OnClientClick = "document.getElementById('ctl00_MainContent_treeViewPanel" + fieldId + "').style.visibility = 'visible'; return false;";
                        tableCell2.Controls.Add(structButton);

                        Button cancelButton = new Button();
                        cancelButton.CausesValidation = false;
                        cancelButton.Text = "Отмена";
                        cancelButton.Style.Add("float", "bottom");
                        cancelButton.Style.Add("width", "100%");
                        cancelButton.Style.Add("heigth", "50px");
                        cancelButton.OnClientClick = "document.getElementById('ctl00_MainContent_treeViewPanel" + fieldId + "').style.visibility = 'hidden'; return false;";

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
                        strucTreeView.Nodes.Add(_common.GetStructTreeViewNode("ctl00_MainContent_" + currentFieldTextBox.ID, "ctl00_MainContent_" + treeViewPanel.ID, fullStruct));

                        scrollPanel.Controls.Add(strucTreeView);
                        treeViewPanel.Controls.Add(scrollPanel);
                        //treeViewPanel.Controls.Add(okButton);
                        treeViewPanel.Controls.Add(cancelButton);
                        tableCell2.Controls.Add(treeViewPanel);
                    }
                    #endregion
                    tableCell1.Controls.Add(_dataTypes.GetRangeValidator("RangeValidator" + fieldId, currentFieldTextBox.ID, currentField.Type));
                    allFieldsInCard.Add(currentFieldTextBox); //запоминаем какие textbox у нас на карте
                    tableCell2.Controls.Add(currentFieldTextBox);
                }


                leftPadding += leftPaddingSpaceBetween + currentField.Width;
                tableRow1.Cells.Add(tableCell1);
                tableRow2.Cells.Add(tableCell2);
            }
            LineTable.Rows.Add(tableRow1);
            LineTable.Rows.Add(tableRow2);
            return LineTable;
        }
        // public string 
        private List<int> GetInstancesListByGroupAndVersion(int cardId, int groupId, int Version)
        {
            List<CollectedFieldsValues> collectedValues = _common.GetCollectedFieldsByCardVersionGroup(cardId, groupId, Version);
            List<int> isDeletedInstances = (from a in collectedValues where a.IsDeleted select a.Instance).Distinct().ToList();
            List<int> allInstances = (from a in collectedValues select a.Instance).Distinct().ToList();
            List<int> onlyActiveInstances = allInstances;
            foreach (int currendDeletedInstance in isDeletedInstances)
            {
                onlyActiveInstances.Remove(currendDeletedInstance);
            }
            return onlyActiveInstances;
        }
        public Panel CreateViewByRegisterAndCard(int registerId, int cardId, int Version, bool _readonly)
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
            RegistersModels currentRegisterModel = _common.GetRegisterModelById(currentRegister.FkRegistersModel);
            // модель реестра
            cardMainPanel.GroupingText = currentRegister.Name; //Title панели равен названию регистра
            List<FieldsGroups> fieldGroupsToShow =
                _common.GetFieldsGroupsInRegisterModelOrderByLine(currentRegisterModel.RegisterModelID);
            //группы которые нужно показать
            //нашли все группы
            foreach (FieldsGroups currentFieldGroup in fieldGroupsToShow)
            {
                List<int> InstancesList;
                if (cardId == 0)
                {
                    InstancesList = new List<int>();
                    InstancesList.Add(0);
                }
                else
                {
                    InstancesList = GetInstancesListByGroupAndVersion(cardId, currentFieldGroup.FieldsGroupID, Version);
                }

                Panel groupPanel = new Panel(); // отдельно по панели на каждую группу
                groupPanel.GroupingText = currentFieldGroup.Name;
                int InstanceNumber = 0;
                int InstancesCount = InstancesList.Count;

                foreach (int currentInstance in InstancesList)
                {
                    Table InstanceTable = new Table();
                    if (InstancesCount > 1)
                    {
                        InstanceTable.Style.Add("border", "1px solid DarkGray ;");
                        InstanceTable.Style.Add("width", "100% ;");
                    }
                    TableRow InstanceRow = new TableRow();
                    TableCell InstanceCell = new TableCell();
                    TableCell delInstanceCell = new TableCell();
                    //TableCell tableCell2 = new TableCell();
                    //if (InstancesCount > 0) InstancePanel.GroupingText = (InstanceNumber++).ToString();

                    List<Fields> fieldToShow = _common.GetFieldsInFieldGroupOrderByLine(currentFieldGroup.FieldsGroupID);
                    List<int> LinesToShow = GetDistinctedSordetLinesFromFieldsList(fieldToShow);
                    //нашли все лайны внутри группы
                    foreach (int currentFieldsLine in LinesToShow)
                    {
                        List<Fields> fieldsInLine = GetFieldsByLineSortedByColumn(currentFieldsLine, fieldToShow);
                        Table tableToAdd = CreateLineTable(fieldsInLine, leftPaddingSpaceBetween, cardId, Version,
                            currentInstance, _readonly);

                        InstanceCell.Controls.Add(tableToAdd);
                    }
                    InstanceRow.Cells.Add(InstanceCell);
                    if (InstancesCount > 1 && !_readonly)
                    {
                        ImageButton delGroupButton = new ImageButton();
                        delGroupButton.ImageUrl = "~/kanz/icons/delButtonIcon.jpg";
                        delGroupButton.Height = 20;
                        delGroupButton.Width = 20;

                        delGroupButton.CommandArgument = currentFieldGroup.FieldsGroupID + "_" + currentInstance;
                        delGroupButton.CausesValidation = false;
                        delGroupButton.Click += DelInstanceButtonClick;
                        delInstanceCell.Controls.Add(delGroupButton);
                        InstanceRow.Cells.Add(delInstanceCell);
                    }
                    InstanceTable.Rows.Add(InstanceRow);
                    groupPanel.Controls.Add(InstanceTable);
                }
                cardMainPanel.Controls.Add(groupPanel);
                if (currentFieldGroup.Multiple && !_readonly)
                {
                    TextBox inputCnt = new TextBox();
                    inputCnt.TextMode = TextBoxMode.SingleLine;
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
        public Panel GetPrintVersion(int registerId, int cardId, int Version) // версия для печати
        {
            Panel panelToReturn = new Panel();
            Registers currentRegister = _common.GetRegisterById(registerId); // текущий реестр
            List<FieldsGroups> fieldGroupsToShow = _common.GetFieldsGroupsInRegisterModelOrderByLine(currentRegister.FkRegistersModel);
            foreach (FieldsGroups currentFieldGroup in fieldGroupsToShow)
            {
                List<int> InstancesList = GetInstancesListByGroupAndVersion(cardId, currentFieldGroup.FieldsGroupID,
                    Version);
                foreach (int currentInstance in InstancesList)
                {
                    bool addThisInstance = false;
                    Table InstanceTable = new Table();
                    TableRow InstanceRow = new TableRow();
                    TableCell InstanceCell = new TableCell();
                    List<Fields> fieldToShow = _common.GetFieldsInFieldGroupOrderByLine(currentFieldGroup.FieldsGroupID);
                    List<int> LinesToShow = GetDistinctedSordetLinesFromFieldsList(fieldToShow);
                    Table LineTable = new Table();
                    LineTable.Width = 800;
                    foreach (int currentFieldsLine in LinesToShow)
                    {

                        TableRow tableRow1 = new TableRow();
                        TableRow tableRow2 = new TableRow();
                        List<Fields> fieldsInLine = GetFieldsByLineSortedByColumn(currentFieldsLine, fieldToShow);
                        foreach (Fields currentField in fieldsInLine)
                        {
                            TableCell tableCell1 = new TableCell(); //заголовок
                            Label cell1 = new Label();
                            cell1.Text = currentField.Name;
                            tableCell1.Controls.Add(cell1);
                            TableCell tableCell2 = new TableCell(); //само поле
                            Label cell2 = new Label();
                            cell2.Text = _common.GetFieldValueByCardVersionInstance(currentField.FieldID, cardId,
                                Version, currentInstance);
                            if (cell2.Text != "")
                            {
                                addThisInstance = true;
                                tableCell2.Controls.Add(cell2);
                                tableRow1.Cells.Add(tableCell1);
                                tableRow2.Cells.Add(tableCell2);
                            }
                        }
                        LineTable.Rows.Add(tableRow1);
                        LineTable.Rows.Add(tableRow2);
                    }

                    if (addThisInstance)
                    {
                        Label hrLabel = new Label();
                        hrLabel.Text = "<hr>";
                        panelToReturn.Controls.Add(hrLabel);
                        InstanceCell.Controls.Add(LineTable);
                        InstanceRow.Cells.Add(InstanceCell);
                        InstanceTable.Rows.Add(InstanceRow);
                        panelToReturn.Controls.Add(InstanceTable);
                    }
                }

            }

            return panelToReturn;
        }
    }
    public class CardCreateEdit
    {
        //  private int _userId = 1;
        private ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        private CardCommonFunctions _common = new CardCommonFunctions();
        public int CreateNewCardInRegister(int registerId, int fieldId)
        {
            CollectedCards newCard = new CollectedCards();
            newCard.FkRegister = registerId;
            newCard.MaInFieldID = fieldId;
            newCard.Active = true;
            chancDb.CollectedCards.InsertOnSubmit(newCard);
            chancDb.SubmitChanges();

            return newCard.CollectedCardID;
        }
        public int CreateNewFieldValueInCard(int cardId, int userId, int fieldId, int Version, int Instance,
            string textValue, bool IsDeleted)
        {
            int lastVersion = _common.GetLastVersionByCard(cardId); // FORPORT
            CollectedFieldsValues newField = new CollectedFieldsValues();
            newField.Active = true;
            newField.FkCollectedCard = cardId;
            newField.FkField = fieldId;
            newField.FkUser = userId;
            newField.CreateDateTime = DateTime.Now;
            newField.Version = lastVersion + 1;   // FORPOR

            newField.Instance = Instance;
            newField.ValueText = textValue;
            newField.IsDeleted = IsDeleted;
            chancDb.CollectedFieldsValues.InsertOnSubmit(newField);
            chancDb.SubmitChanges();
            return newField.CollectedFieldValueID;
        }
        public int SaveCard(int registerId, int cardId, List<TextBox> fieldsToSave, List<FileUpload> filesToSave)
        {
            int mainfieldId = 0;
            foreach (TextBox currentTextBox in fieldsToSave)
            {
                if (currentTextBox.Attributes["iamfieldid"] != null)
                {
                    if (currentTextBox.Attributes["iamfieldid"] == "1")
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
        public void SaveFieldsValues(List<TextBox> fieldsToSave, List<FileUpload> filesToSave, int cardId)
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
                            currentCollectedFieldId = CreateNewFieldValueInCard(cardId, userId, currentFieldId, lastVersion + 1,
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
                        currentCollectedFieldInstance, currentField.Text, false);
                }
                else
                {
                    string Value = _common.GetFieldValueByCardVersionInstance(currentFieldId, cardId, lastVersion,
                        currentCollectedFieldInstance);
                    if (Value != currentField.Text)
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
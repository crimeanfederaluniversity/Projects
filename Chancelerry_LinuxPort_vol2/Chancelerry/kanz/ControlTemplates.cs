using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using Microsoft.Ajax.Utilities;
using Npgsql;

namespace Chancelerry.kanz
{
    public class ControlTemplates
    {
        public class Template1
        {
            protected ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

            protected Table _resultTable;
            protected int _mainEndedGoodCnt = 0;
            protected int _mainEndedBadCnt = 0;
            protected int _mainNotEndedGoodCnt = 0;
            protected int _mainNotEndedBadCnt = 0;
            protected int _addEndedGoodCnt = 0;
            protected int _addEndedBadCnt = 0;
            protected int _addNotEndedGoodCnt = 0;
            protected int _addNotEndedBadCnt = 0;

            protected int GetCardMaxInstance(int cardId, List<CollectedFieldsValues> valuesList1,
                List<CollectedFieldsValues> valuesList2)
            {
                List<int> max1 = (from a in valuesList1
                    where a.FkCollectedCard == cardId
                          && a.Active
                    select a.Instance).ToList();
                List<int> max2 = (from a in valuesList2
                    where a.FkCollectedCard == cardId
                          && a.Active
                    select a.Instance).ToList();
                List<int> max = new List<int>();
                max.AddRange(max1);
                max.AddRange(max2);
                if (!max.Any())
                    return 0;
                return max.Max();
            }

            protected List<CollectedFieldsValues> GetRealValueInCardEndField(int cardId,
                List<CollectedFieldsValues> valuesList)
            {
                List<CollectedFieldsValues> listToReturn = new List<CollectedFieldsValues>();
                // listToReturn.Add(new CollectedFieldsValues() { ValueText = " " });
                List<CollectedFieldsValues> tmp = (from a in valuesList
                    where a.FkCollectedCard == cardId
                          && a.Active
                    select a).ToList();
                if (tmp.Count == 0) return listToReturn; // Почему оно ноль??
                int maxInstance = (from a in tmp select a.Instance).Max();
                for (int i = 0; i < maxInstance + 1; i++)
                {
                    List<CollectedFieldsValues> tmp1 =
                        (from a in tmp where a.Instance == i select a).OrderByDescending(mc => mc.Version).ToList();
                    CollectedFieldsValues tmp2 = new CollectedFieldsValues();
                    if (tmp1.Count > 0)
                    {
                        tmp2 = (from a in tmp1 select a).FirstOrDefault();
                    }
                    else
                    {
                        continue;
                    }
                    if (tmp2 == null)
                        continue;
                    if (tmp2.IsDeleted)
                        continue;
                    listToReturn.Add(tmp2);
                }
                return listToReturn;
            }

            protected List<CollectedFieldsValues> GetCollectedValuesInFieldInRegister(int fieldId)
            {
                return (from a in chancDb.CollectedCards
                    // найдем все значения для данного филда в данном регистре
                    join b in chancDb.CollectedFieldsValues
                        on a.CollectedCardID equals b.FkCollectedCard
                    where a.Active == true
                          && b.Active == true
                          && b.FkField == fieldId
                          && a.FkRegister == _registerId
                    select b).Distinct().ToList();
            }

            protected string GetCalculateDays(string compleatedDate, string cntrlEndDate, bool isMain)
            {
                string strToReturn = "";
                if (compleatedDate != null && cntrlEndDate != null)
                    if (compleatedDate.Any() && cntrlEndDate.Any()) // значит закрыт
                    {
                        DateTime tmlCntrlEndDate = DateTime.MinValue;
                        DateTime tmlCompleatedDate = DateTime.MinValue;
                        DateTime.TryParse(compleatedDate, out tmlCompleatedDate);
                        DateTime.TryParse(cntrlEndDate, out tmlCntrlEndDate);

                        int days = tmlCntrlEndDate.Subtract(tmlCompleatedDate).Days;
                        if (days < 0)
                        {
                            if (isMain)
                            {
                                _mainEndedBadCnt++;
                            }
                            else
                            {
                                _addEndedBadCnt++;
                            }
                            return "<div class='bad1'>" + days + "</div>";
                        }
                        else
                        {
                            if (isMain)
                            {
                                _mainEndedGoodCnt++;
                            }
                            else
                            {
                                _addEndedGoodCnt++;
                            }
                            return "<div class='good1'>" + days + "</div>";
                        }

                    }

                if (cntrlEndDate != null)
                    if (cntrlEndDate.Any()) // еще открыт
                    {
                        DateTime tmlCntrlEndDate = DateTime.MinValue;
                        DateTime.TryParse(cntrlEndDate, out tmlCntrlEndDate);

                        int days = tmlCntrlEndDate.Subtract(_compareDateTime).Days;
                        if (days < 0)
                        {
                            if (isMain)
                            {
                                _mainNotEndedBadCnt++;
                            }
                            else
                            {
                                _addNotEndedBadCnt++;
                            }
                            return "<div class='bad2'>" + days + "</div>";
                        }
                        else
                        {
                            if (isMain)
                            {
                                _mainNotEndedGoodCnt++;
                            }
                            else
                            {
                                _addNotEndedGoodCnt++;
                            }
                            return "<div class='good2'>" + days + "</div>";
                        }
                    }

                return strToReturn;
            }

            protected virtual List<int> GetCardsWhereDateFieldInRange()
            {
                StatisticsClass statisticsClass = new StatisticsClass();
                return statisticsClass.GetCardsWhereDateFieldInRange(_dateToFilterId, _registerId, _startDate, _endDate);
            }

            protected bool CreateTable(bool filterCntrlEndByDates)
            {
                _resultTable = new Table();
                _resultTable.CssClass = "resultTable";
                List<int> allCardsInRangeList = GetCardsWhereDateFieldInRange();
                List<CollectedFieldsValues> mainCntrlComletedDateFieldValues =
                    GetCollectedValuesInFieldInRegister(_mainCntrlComletedDateFieldId);
                List<CollectedFieldsValues> mainCntrlEndDateFieldValues =
                    GetCollectedValuesInFieldInRegister(_mainCntrlEndDateFieldId);
                List<CollectedFieldsValues> addCntrlCompletedDateFieldValues =
                    GetCollectedValuesInFieldInRegister(_addCntrlCompletedDateFieldId);
                List<CollectedFieldsValues> addCntrlEndDateFieldValues =
                    GetCollectedValuesInFieldInRegister(_addCntrlEndDateFieldId);
                List<CollectedFieldsValues> mustBeYesInFieldValues =
                    GetCollectedValuesInFieldInRegister(_mustBeYesInFieldId);
                _resultTable.Rows.Add(new TableHeaderRow()
                {
                    Cells =
                    {
                        new TableHeaderCell() {Text = "№ Документа"},
                        new TableHeaderCell() {Text = "Контроль"},
                        new TableHeaderCell() {Text = "Контр. Дата"},
                        new TableHeaderCell() {Text = "Дата. Исполн."},
                        new TableHeaderCell() {Text = "Разница дат"},
                        new TableHeaderCell() {Text = "Доп. Контр. Дата "},
                        new TableHeaderCell() {Text = "Доп. Дата. Исполн."},
                        new TableHeaderCell() {Text = "Доп. Разница дат"}
                    }
                });

                List<CollectedCards> allCards = (from a in chancDb.CollectedCards
                    where a.FkRegister == _registerId
                    select a).ToList();

                foreach (int currentCard in allCardsInRangeList)
                {
                    List<CollectedFieldsValues> tmpMainCntrlComletedDateFieldValues =
                        GetRealValueInCardEndField(currentCard, mainCntrlComletedDateFieldValues);
                    List<CollectedFieldsValues> tmpMainCntrlEndDateFieldValues = GetRealValueInCardEndField(
                        currentCard, mainCntrlEndDateFieldValues);
                    List<CollectedFieldsValues> tmpAddCntrlCompletedDateFieldValues =
                        GetRealValueInCardEndField(currentCard, addCntrlCompletedDateFieldValues);
                    List<CollectedFieldsValues> tmpAddCntrlEndDateFieldValues = GetRealValueInCardEndField(currentCard,
                        addCntrlEndDateFieldValues);
                    List<CollectedFieldsValues> tmpMustBeYesInFieldValues = GetRealValueInCardEndField(currentCard,
                        mustBeYesInFieldValues);


                    if ((from a in tmpMustBeYesInFieldValues where a.ValueText.ToLower().Contains("да") select a).Any()
                        || (from a in tmpMainCntrlComletedDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        || (from a in tmpAddCntrlEndDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        || (from a in tmpAddCntrlCompletedDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        || (from a in tmpMainCntrlEndDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        )
                    {
                        int maxInstanceInCard =
                            GetCardMaxInstance(currentCard, addCntrlCompletedDateFieldValues, addCntrlEndDateFieldValues) +
                            1;
                        for (int i = 0; i < maxInstanceInCard; i++)
                        {
                            TableRow currentRow = new TableRow();
                            if (i == 0)
                            {
                                currentRow.Cells.Add(new TableCell()
                                {
                                    Text =
                                        (from a in allCards where a.CollectedCardID == currentCard select a.MaInFieldID)
                                            .FirstOrDefault().ToString(),
                                    RowSpan = maxInstanceInCard
                                });

                                currentRow.Cells.Add(new TableCell()
                                {
                                    Text = (from a in tmpMustBeYesInFieldValues select a.ValueText).FirstOrDefault(),
                                    RowSpan = maxInstanceInCard
                                });
                                string mainCntrlEndDateStr =
                                    (from a in tmpMainCntrlEndDateFieldValues select a.ValueText).FirstOrDefault();
                                string mainCompleatedDateStr =
                                    (from a in tmpMainCntrlComletedDateFieldValues select a.ValueText).FirstOrDefault();


                                bool mainShowEmpty = true;

                                DateTime tmp = DateTime.MinValue;
                                if (DateTime.TryParse(mainCntrlEndDateStr, out tmp))
                                {
                                    if (tmp.Date >= _startDate.Date && tmp.Date <= _endDate.Date)
                                    {
                                        mainShowEmpty = false;
                                    }
                                }

                                if (mainShowEmpty && filterCntrlEndByDates)
                                {
                                    currentRow.Cells.Add(new TableCell() {RowSpan = maxInstanceInCard});
                                    currentRow.Cells.Add(new TableCell() {RowSpan = maxInstanceInCard});
                                    currentRow.Cells.Add(new TableCell() {RowSpan = maxInstanceInCard});
                                }
                                else
                                {
                                    currentRow.Cells.Add(new TableCell()
                                    {
                                        Text = mainCntrlEndDateStr,
                                        RowSpan = maxInstanceInCard
                                    });
                                    currentRow.Cells.Add(new TableCell()
                                    {
                                        Text = mainCompleatedDateStr,
                                        RowSpan = maxInstanceInCard
                                    });
                                    currentRow.Cells.Add(new TableCell()
                                    {
                                        Text = GetCalculateDays(mainCompleatedDateStr, mainCntrlEndDateStr, true),
                                        RowSpan = maxInstanceInCard
                                    });

                                }
                            }

                            string addCompleatedDateStr =
                                (from a in tmpAddCntrlCompletedDateFieldValues where a.Instance == i select a.ValueText)
                                    .FirstOrDefault();
                            string addCntrlEndDateStr =
                                (from a in tmpAddCntrlEndDateFieldValues where a.Instance == i select a.ValueText)
                                    .FirstOrDefault();


                            bool addShowEmpty = true;

                            DateTime tmp2 = DateTime.MinValue;
                            if (DateTime.TryParse(addCntrlEndDateStr, out tmp2))
                            {
                                if (tmp2.Date >= _startDate.Date && tmp2.Date <= _endDate.Date)
                                {
                                    addShowEmpty = false;
                                }
                            }

                            if (addShowEmpty && filterCntrlEndByDates)
                            {
                                currentRow.Cells.Add(new TableCell());
                                currentRow.Cells.Add(new TableCell());
                                currentRow.Cells.Add(new TableCell());
                            }
                            else
                            {
                                currentRow.Cells.Add(new TableCell() {Text = addCntrlEndDateStr});
                                currentRow.Cells.Add(new TableCell() {Text = addCompleatedDateStr});
                                currentRow.Cells.Add(new TableCell()
                                {
                                    Text = GetCalculateDays(addCompleatedDateStr, addCntrlEndDateStr, false)
                                });

                            }
                            _resultTable.Rows.Add(currentRow);
                        }
                    }
                }
                return true;
            }

            public virtual Table GetStatisticTable()
            {
                Table tableToReturn = new Table();
                int all = _mainEndedGoodCnt + _mainEndedBadCnt + _mainNotEndedGoodCnt + _mainNotEndedBadCnt +
                          _addEndedGoodCnt + _addEndedBadCnt + _addNotEndedGoodCnt + _addNotEndedBadCnt;
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = {new TableCell() {Text = "все"}, new TableCell() {Text = all.ToString()}}
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "основное без нарушения срока"},
                        new TableCell() {Text = _mainEndedGoodCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "основное с нарушением срока"},
                        new TableCell() {Text = _mainEndedBadCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "основное, незавершенное"},
                        new TableCell() {Text = _mainNotEndedGoodCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "основное, незавершенное, с нарушением срока"},
                        new TableCell() {Text = _mainNotEndedBadCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "дополнительное без нарушения срока"},
                        new TableCell() {Text = _addEndedGoodCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "дополнительное  с нарушением срока"},
                        new TableCell() {Text = _addEndedBadCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "дополнительное, незавершенное"},
                        new TableCell() {Text = _addNotEndedGoodCnt.ToString()}
                    }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells =
                    {
                        new TableCell() {Text = "дополнительное, незавершенное, с нарушением срока"},
                        new TableCell() {Text = _addNotEndedBadCnt.ToString()}
                    }
                });

                return tableToReturn;
            }
            public virtual Table GetResultTable()
            {
                CreateTable(false);
                return _resultTable;
            }
            public bool Initiate(int userId)
            {
                UserId = userId;
                return true;
            }
            public bool SetParams(DateTime startDate, DateTime endDate, DateTime compareDate, int viewType,
                int registerId)
            {
                SetStartEndDate(startDate, endDate);
                _compareDateTime = compareDate;
                _viewType = viewType;
                _registerId = registerId;
                return true;
            }
            protected int UserId { set; get; }
            protected readonly int _registerModelId = 1; //Входящие
            protected readonly int _dateToFilterId = 7; // Дата поступления
            protected DateTime _startDate;
            protected DateTime _endDate;
            protected DateTime _compareDateTime;
            protected int _viewType;
            protected int _registerId;

            protected void SetStartEndDate(DateTime startDate, DateTime endDate)
            {
                _startDate = startDate;
                _endDate = endDate;
            }

            protected int _mustBeYesInFieldId = 31; //тут должно быть да
            protected int _mainCntrlComletedDateFieldId = 32;
            protected int _mainCntrlEndDateFieldId = 37;
            protected int _addCntrlCompletedDateFieldId = 165;
            protected int _addCntrlEndDateFieldId = 39;

            public ListItem[] GetDropDownValues()
            {
                int userId = UserId;
                return (from a in chancDb.Registers
                    where a.Active == true
                    where a.FkRegistersModel == _registerModelId
                    join b in chancDb.RegistersUsersMap
                        on a.RegisterID equals b.FkRegister
                    where b.FkUser == userId
                    where b.Active
                    select a).ToList()
                    .Select(mc => new ListItem() {Value = mc.RegisterID.ToString(), Text = mc.Name})
                    .Distinct()
                    .ToArray();
            }
        }
        public class Template2 : Template1
        {

            protected override List<int> GetCardsWhereDateFieldInRange()
            {
                List<int> listToReturn = GetCardsWhereCntrlDateInRange();
                return listToReturn;
            }

            public override Table GetResultTable()
            {
                CreateTable(true);
                return _resultTable;
            }

            public List<int> GetCardsIdsWhereCntrlDateInRangeInAnyInstance(int fieldId)
            {
                List<int> listToReturn = new List<int>();
                List<CollectedFieldsValues> valuesList =
                    (from a in chancDb.CollectedCards
                        // найдем все значения для заданного филда в данном регистре
                        join b in chancDb.CollectedFieldsValues
                            on a.CollectedCardID equals b.FkCollectedCard
                        where a.Active == true
                              && b.Active == true
                              && b.FkField == fieldId
                              && a.FkRegister == _registerId
                        select b).Distinct().ToList();
                List<int> cardsIds = (from a in valuesList select a.FkCollectedCard).Distinct().ToList();
                foreach (int cardId in cardsIds) //пройдемся по каждой карточке чтобы найти есть ли нужная нам дата)
                {
                    List<CollectedFieldsValues> valuesInCard =
                        (from a in valuesList where a.FkCollectedCard == cardId select a).ToList();
                    // назходим все поля относящиеся только к текущей карте
                    if (valuesInCard.Count == 0) continue; // Почему оно ноль??
                    int maxInstance = (from a in valuesInCard select a.Instance).Max();
                    //находим сколько инстансов
                    for (int i = 0; i < maxInstance + 1; i++)
                    {
                        List<CollectedFieldsValues> tmp1 =
                            (from a in valuesInCard where a.Instance == i select a).OrderByDescending(mc => mc.Version)
                                .ToList();
                        //нашли все значения отсортированные по версии
                        CollectedFieldsValues tmp2;
                        if (tmp1.Count > 0)
                        {
                            tmp2 = (from a in tmp1 select a).FirstOrDefault();
                        }
                        else
                        {
                            continue;
                        }
                        if (tmp2 == null)
                            continue;
                        if (tmp2.IsDeleted)
                            continue;

                        DateTime tmp = DateTime.MinValue; // объявили так чтобы потом проверить)
                        if (DateTime.TryParse(tmp2.ValueText, out tmp))
                        {
                            if (tmp.Date >= _startDate.Date && tmp.Date <= _endDate.Date)
                            {
                                listToReturn.Add(cardId);
                            }

                        }
                    }
                }
                return listToReturn;
            }

            public List<int> GetCardsWhereCntrlDateInRange() // нестандартный список карточек по нескольким полям
            {
                List<int> list1 = GetCardsIdsWhereCntrlDateInRangeInAnyInstance(_mainCntrlEndDateFieldId);
                List<int> list2 = GetCardsIdsWhereCntrlDateInRangeInAnyInstance(_addCntrlEndDateFieldId);
                List<int> listToReturn = new List<int>();
                listToReturn.AddRange(list1);
                listToReturn.AddRange(list2);
                listToReturn = listToReturn.Distinct().ToList();
                return listToReturn;

            }
        }
        public class Template3 : Template2
        {

            public Template3()
            {
                MainIncomingDateId = 7;
                AddIncomingDateId = 24;
                Str1 = "Кол-во входящих c учетом движения документов";
                Str2 = "Кол-во входящих карточек";
            }

            protected string Str1;
            protected string Str2;
            protected int MainIncomingDateId;
            protected int AddIncomingDateId;

            public bool SetParams(DateTime startDate, DateTime endDate, int registerId)
            {
                SetStartEndDate(startDate, endDate);
                _registerId = registerId;
                return true;
            }

            private List<int> CountIncomingInRangeOnlyCards()
            {
                List<int> list1 = GetCardsIdsWhereCntrlDateInRangeInAnyInstance(MainIncomingDateId);
                list1.Sort();
                return list1;
            }

            private List<int> CountIncomingInRangeWithDocsMovement()
            {

                List<int> list1 = GetCardsIdsWhereCntrlDateInRangeInAnyInstance(MainIncomingDateId);
                List<int> list2 = GetCardsIdsWhereCntrlDateInRangeInAnyInstance(AddIncomingDateId);
                List<int> resList = new List<int>();
                resList.AddRange(list1);
                resList.AddRange(list2);
                resList.Sort();
                return resList;
            }

            public override Table GetStatisticTable()
            {
                List<int> l1 = CountIncomingInRangeWithDocsMovement();
                List<int> l2 = CountIncomingInRangeOnlyCards();

                Table resulTable = new Table();
                resulTable.Rows.Add(new TableRow()
                {
                    Cells = {new TableCell() {Text = Str1}, new TableCell() {Text = l1.Count.ToString()}}
                });
                resulTable.Rows.Add(new TableRow()
                {
                    Cells = {new TableCell() {Text = Str2}, new TableCell() {Text = l2.Count.ToString()}}
                });

                return resulTable;
            }
        }
        public class Template4 : Template3
        {
            public Template4()
            {
                MainIncomingDateId = 20;
                AddIncomingDateId = 29;
                Str1 = "Кол-во резолюций c учетом движения документов";
                Str2 = "Кол-во резолюций без движения документа";
            }

        }
        public class Template5 
        {
            
            protected ChancelerryDb chancDb =
               new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            private int registerModelId = 3;

            public int UserId { set; get; }
            private int dateFieldId = 79;
            private DateTime startDate;
            private DateTime endDate;
            private int registerId;
            private string valueWeNeed = "да";
            private int valueWeNeedFieldId = 170;
            private int dateToCheckFieldId = 100;



            protected virtual List<int> GetCardsWhereDateFieldInRange()
            {
                StatisticsClass statisticsClass = new StatisticsClass();
                return statisticsClass.GetCardsWhereDateFieldInRange(dateFieldId, registerId, startDate, endDate);
            }
            protected List<CollectedFieldsValues> GetCollectedValuesInFieldInRegister(int fieldId)
            {
                return (from a in chancDb.CollectedCards
                            // найдем все значения для данного филда в данном регистре
                        join b in chancDb.CollectedFieldsValues
                            on a.CollectedCardID equals b.FkCollectedCard
                        where a.Active == true
                              && b.Active == true
                              && b.FkField == fieldId
                              && a.FkRegister == registerId
                        select b).Distinct().ToList();
            }

            public void SetStartParams(DateTime stardate, DateTime enddate, int registerid)
            {
                startDate = stardate;
                endDate = enddate;
                registerId = registerid;
            }

            public Table GetResultTable()
            {
                Table resultTable = new Table();
                List<int> allCardsInDateRange = GetCardsWhereDateFieldInRange();
                List<CollectedFieldsValues> allFieldValues = GetCollectedValuesInFieldInRegister(valueWeNeedFieldId);
                List<int> allCardsInDateRangeWithNeededValue=new List<int>();
                foreach (int currentCardId in allCardsInDateRange)
                {
                    if (
                        (from a in allFieldValues where a.FkCollectedCard == currentCardId select a).OrderByDescending(
                            mc => mc.Version).FirstOrDefault().ValueText.ToLower().Contains(valueWeNeed))
                    {
                        allCardsInDateRangeWithNeededValue.Add(currentCardId);
                    }
                }
                List<int> onControlAllDone = new List<int>();
                List<int> onControlNoneDone = new List<int>();
                List<int> onControlSomeDone = new List<int>();

                List<CollectedFieldsValues> allNeededDateFieldValues = GetCollectedValuesInFieldInRegister(dateToCheckFieldId);
                foreach (int currentCardId in allCardsInDateRangeWithNeededValue)
                {
                    List<CollectedFieldsValues> allValuesInFieldRegisterCard =
                        (from a in allNeededDateFieldValues where a.FkCollectedCard == currentCardId select a).ToList();
                    int maxInstanceInCard = (from a in allValuesInFieldRegisterCard select a.Instance).Max();
                    bool allDone = true;
                    bool someDone = false;

                    for (int i = 0; i < maxInstanceInCard + 1; i++)
                    {
                        CollectedFieldsValues realValue = (from a in allValuesInFieldRegisterCard where a.Instance == i select a).OrderByDescending(
                            mc => mc.Version).FirstOrDefault();
                        if (realValue == null)
                            continue;
                        if (realValue.IsDeleted)
                            continue;
                        if (realValue.ValueText.IsNullOrWhiteSpace())
                        {
                            allDone = false;
                        }
                        else
                        {
                            someDone = true;
                        }
                    }
                    if (allDone)
                    {
                        onControlAllDone.Add(currentCardId);
                    }
                    else if (someDone)
                    {
                        onControlSomeDone.Add(currentCardId);
                    }
                    else
                    {
                        onControlNoneDone.Add(currentCardId);
                    }
                }

                resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = "Всего на контроле:" }, new TableCell() { Text = allCardsInDateRangeWithNeededValue.Count().ToString() } } });
                resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = "Все пункты исполнены:" }, new TableCell() { Text = onControlAllDone.Count().ToString() } } });
                resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = "Часть пунктов исполнена:" }, new TableCell() { Text = onControlSomeDone.Count().ToString() } } });
                resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = "Ни один пункт не исполнен:" }, new TableCell() { Text = onControlNoneDone.Count().ToString() } } });

                return resultTable;
            }


            public ListItem[] GetDropDownListItems()
            {
                return (from a in chancDb.Registers
                    where a.FkRegistersModel == registerModelId
                          && a.Active == true
                    join b in chancDb.RegistersUsersMap
                        on a.RegisterID equals b.FkRegister

                    where  b.Active == true
                    && b.FkUser == UserId
                        select a).Distinct().Select(mc => new ListItem() {Value = mc.RegisterID.ToString(), Text = mc.Name}).ToArray();
            }
        }
    }
}
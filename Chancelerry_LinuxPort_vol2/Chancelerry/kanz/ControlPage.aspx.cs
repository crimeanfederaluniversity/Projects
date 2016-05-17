using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Npgsql;
using NUnit.Framework.SyntaxHelpers;

namespace Chancelerry.kanz
{
    public partial class ControlPage : System.Web.UI.Page
    {

        public class Template1
        {
            private ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

            private Table _resultTable;



            int _allCnt = 0;
            int _mainEndedGoodCnt = 0;
            int _mainEndedBadCnt = 0;
            int _mainNotEndedGoodCnt = 0;
            int _mainNotEndedBadCnt = 0;
            int _addEndedGoodCnt = 0;
            int _addEndedBadCnt = 0;
            int _addNotEndedGoodCnt = 0;
            int _addNotEndedBadCnt = 0;


            private int GetCardMaxInstance(int cardId, List<CollectedFieldsValues> valuesList1, List<CollectedFieldsValues> valuesList2)
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

            private List<CollectedFieldsValues> GetRealValueInCardEndField(int cardId , List<CollectedFieldsValues> valuesList)
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
                    List<CollectedFieldsValues> tmp1 = (from a in tmp where a.Instance == i select a).OrderByDescending(mc => mc.Version).ToList();
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


            private List<CollectedFieldsValues> GetCollectedValuesInFieldInRegister(int fieldId)
            {
                return (from a in chancDb.CollectedCards // найдем все значения для данного филда в данном регистре
                              join b in chancDb.CollectedFieldsValues
                                  on a.CollectedCardID equals b.FkCollectedCard
                              where a.Active == true
                                    && b.Active == true
                                    && b.FkField == fieldId
                                    && a.FkRegister == _registerId
                              select b).Distinct().ToList();
            }


            private string GetCalculateDays(string compleatedDate, string cntrlEndDate, bool isMain)
            {
                string strToReturn="";
                if (compleatedDate!=null && cntrlEndDate != null)
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
                            { _mainEndedBadCnt++;}
                        else
                        {
                            _addEndedBadCnt++;
                        }
                            return "<div class='bad1'>" + days + "</div>";
                    }
                    else
                        {
                            if (isMain)
                            { _mainEndedGoodCnt++; }
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
                            { _mainNotEndedBadCnt++; }
                            else
                            {
                                _addNotEndedBadCnt++;
                            }
                            return "<div class='bad2'>" + days + "</div>";
                        }
                    else
                    {
                            if (isMain)
                            { _mainNotEndedGoodCnt++; }
                            else
                            {
                                _addNotEndedGoodCnt++;
                            }
                            return "<div class='good2'>" + days + "</div>";
                        }                                             
                }

                return strToReturn;
            }

            private bool CreateTable()
            {
                _resultTable = new Table();
                _resultTable.CssClass = "resultTable";
                //к этому моменту все переменные есть надо всего то собрать все в кучу
                //для начала нужно вытащить все нужные карточки
                //вытаскиваем только те которые вписываются в срок являются контрольными
                //дальше посмотрим
                StatisticsClass statisticsClass = new StatisticsClass();
                List<int> allCardsInRangeList = statisticsClass.GetCardsWhereDateFieldInRange(_dateToFilterId, _registerId, _startDate, _endDate);

                List<CollectedFieldsValues> mainCntrlComletedDateFieldValues = GetCollectedValuesInFieldInRegister(_mainCntrlComletedDateFieldId);
                List<CollectedFieldsValues> mainCntrlEndDateFieldValues = GetCollectedValuesInFieldInRegister(_mainCntrlEndDateFieldId);
                List<CollectedFieldsValues> addCntrlCompletedDateFieldValues = GetCollectedValuesInFieldInRegister(_addCntrlCompletedDateFieldId);
                List<CollectedFieldsValues> addCntrlEndDateFieldValues = GetCollectedValuesInFieldInRegister(_addCntrlEndDateFieldId);
                List<CollectedFieldsValues> mustBeYesInFieldValues = GetCollectedValuesInFieldInRegister(_mustBeYesInFieldId);

                List<int> allCntrlCardsInRange = new List<int>();


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
                    List<CollectedFieldsValues> tmpMainCntrlComletedDateFieldValues = GetRealValueInCardEndField(currentCard, mainCntrlComletedDateFieldValues);
                    List<CollectedFieldsValues> tmpMainCntrlEndDateFieldValues = GetRealValueInCardEndField(currentCard, mainCntrlEndDateFieldValues);
                    List<CollectedFieldsValues> tmpAddCntrlCompletedDateFieldValues = GetRealValueInCardEndField(currentCard, addCntrlCompletedDateFieldValues);
                    List<CollectedFieldsValues> tmpAddCntrlEndDateFieldValues = GetRealValueInCardEndField(currentCard, addCntrlEndDateFieldValues);
                    List<CollectedFieldsValues> tmpMustBeYesInFieldValues = GetRealValueInCardEndField(currentCard, mustBeYesInFieldValues);
                    

                    if ((from a in tmpMustBeYesInFieldValues where a.ValueText.ToLower().Contains("да") select a).Any()
                        || (from a in tmpMainCntrlComletedDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        || (from a in tmpAddCntrlEndDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        || (from a in tmpAddCntrlCompletedDateFieldValues where a.ValueText.Length > 4 select a).Any()
                        || (from a in tmpMainCntrlEndDateFieldValues where a.ValueText.Length>4 select a).Any()
                        )
                    {
                        int maxInstanceInCard = GetCardMaxInstance(currentCard, addCntrlCompletedDateFieldValues, addCntrlEndDateFieldValues)+1;
                        
                        

                        for (int i = 0; i < maxInstanceInCard; i++)
                        {
                            TableRow currentRow = new TableRow();
                            if (i == 0)
                            {
                                currentRow.Cells.Add(new TableCell() { Text = (from a in allCards where a.CollectedCardID == currentCard select a.MaInFieldID).FirstOrDefault().ToString(), RowSpan = maxInstanceInCard });
                                currentRow.Cells.Add(new TableCell() { Text = (from a in tmpMustBeYesInFieldValues select a.ValueText).FirstOrDefault(), RowSpan = maxInstanceInCard });
                                string mainCntrlEndDateStr =
                                    (from a in tmpMainCntrlEndDateFieldValues select a.ValueText).FirstOrDefault();
                                string mainCompleatedDateStr =
                                    (from a in tmpMainCntrlComletedDateFieldValues select a.ValueText).FirstOrDefault();

                                currentRow.Cells.Add(new TableCell() { Text = mainCntrlEndDateStr, RowSpan = maxInstanceInCard });
                                currentRow.Cells.Add(new TableCell() { Text = mainCompleatedDateStr, RowSpan = maxInstanceInCard });   
                                 
                                currentRow.Cells.Add(new TableCell() { Text = GetCalculateDays(mainCompleatedDateStr, mainCntrlEndDateStr, true), RowSpan = maxInstanceInCard });
                            }

                            string addCompleatedDateStr  =
                                (from a in tmpAddCntrlCompletedDateFieldValues where a.Instance == i select a.ValueText)
                                    .FirstOrDefault();
                            string addCntrlEndDateStr =
                                (from a in tmpAddCntrlEndDateFieldValues where a.Instance == i select a.ValueText)
                                    .FirstOrDefault();

                            currentRow.Cells.Add(new TableCell() { Text = addCntrlEndDateStr });
                            currentRow.Cells.Add(new TableCell() { Text = addCompleatedDateStr });

                            currentRow.Cells.Add(new TableCell() { Text = GetCalculateDays(addCompleatedDateStr, addCntrlEndDateStr, false) });

                            _resultTable.Rows.Add(currentRow);
                        }
                    }      
                }
                return true;
            }
        /*    int _allCnt = 0;
            int _mainEndedGoodCnt = 0;
            int _mainEndedBadCnt = 0;
            int _mainNotEndedGoodCnt = 0;
            int _mainNotEndedBadCnt = 0;
            int _addEndedGoodCnt = 0;
            int _addEndedBadCnt = 0;
            int _addNotEndedGoodCnt = 0;
            int _addNotEndedBadCnt = 0;
            */
            public Table GetStatisticTable()
            {
                Table tableToReturn = new Table();
                int all = _mainEndedGoodCnt + _mainEndedBadCnt + _mainNotEndedGoodCnt + _mainNotEndedBadCnt +
                          _addEndedGoodCnt + _addEndedBadCnt + _addNotEndedGoodCnt + _addNotEndedBadCnt;
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "все" }, new TableCell() { Text = all.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = {new TableCell() {Text = "основное без нарушения срока"}, new TableCell() {Text = _mainEndedGoodCnt.ToString()}}
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "основное с нарушением срока" }, new TableCell() { Text = _mainEndedBadCnt.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "основное, незавершенное" }, new TableCell() { Text = _mainNotEndedGoodCnt.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "основное, незавершенное, с нарушением срока" }, new TableCell() { Text = _mainNotEndedBadCnt.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "дополнительное без нарушения срока" }, new TableCell() { Text = _addEndedGoodCnt.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "дополнительное  с нарушением срока" }, new TableCell() { Text = _addEndedBadCnt.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "дополнительное, незавершенное" }, new TableCell() { Text = _addNotEndedGoodCnt.ToString() } }
                });
                tableToReturn.Rows.Add(new TableRow()
                {
                    Cells = { new TableCell() { Text = "дополнительное, незавершенное, с нарушением срока" }, new TableCell() { Text = _addNotEndedBadCnt.ToString() } }
                });

                return tableToReturn;
            }
            public Table GetResultTable()
            {
                CreateTable();
                return _resultTable;
            }
            public bool Initiate(int userId)
            {
                UserId = userId;
                return true;
            }
            public bool SetParams(DateTime startDate, DateTime endDate, DateTime compareDate, int viewType,int registerId)
            {
                SetStartEndDate(startDate, endDate);
                _compareDateTime = compareDate;
                _viewType = viewType;
                _registerId = registerId;
                return true;
            }
            private int UserId { set; get; }
            readonly private int _registerModelId = 1; //Входящие

            readonly private int _dateToFilterId = 7; // Дата поступления
            private DateTime _startDate;
            private DateTime _endDate;

            private DateTime _compareDateTime;

            private int _viewType;
            private int _registerId;
            private void SetStartEndDate(DateTime startDate, DateTime endDate)
            {
                _startDate = startDate;
                _endDate = endDate;
            }

            private int _mustBeYesInFieldId = 31; //тут должно быть да

            private int _mainCntrlComletedDateFieldId = 32;
            private int _mainCntrlEndDateFieldId = 37;
            private int _addCntrlCompletedDateFieldId = 165;
            private int _addCntrlEndDateFieldId = 39;
          
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
                                                select a).ToList().Select(mc => new ListItem() {Value = mc.RegisterID.ToString(), Text = mc.Name}).Distinct().ToArray();
            }
        }

        readonly Template1 template1 = new Template1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region T1
                int userId = 0;
                int.TryParse(Session["userID"].ToString(), out userId);

                if (userId == 0)
                {
                    Response.Redirect("~/Default.aspx");
                }
                template1.Initiate(userId);
                T1ListOfIncomingDropDownList.Items.AddRange(template1.GetDropDownValues());
                #endregion
            }
        }

        protected void T1CreateTableButton_Click(object sender, EventArgs e)
        {
            string startDateStr = T1StartDateTextBox.Text;
            string endDateStr = T1EndDateTextBox.Text;
            string compareDateStr = T1CompareDateTextBox.Text;
            string registerIdStr = T1ListOfIncomingDropDownList.SelectedValue;
            string viewTypeStr = T1RadioButtonList1.SelectedValue;

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            DateTime compareDate = DateTime.MaxValue;
            int registerId = 0;
            int viewType = 0;

            DateTime.TryParse(startDateStr, out startDate);
            DateTime.TryParse(endDateStr, out endDate);
            DateTime.TryParse(compareDateStr, out compareDate);
            Int32.TryParse(registerIdStr, out registerId);
            Int32.TryParse(viewTypeStr, out viewType);


            template1.SetParams(startDate,endDate,compareDate,viewType, registerId);
            T1ResultDiv.Controls.Clear();
            T1ResultDiv.Controls.Add(template1.GetResultTable());
            T1ResultDiv.Controls.Add(template1.GetStatisticTable());
            
        }
    }
}
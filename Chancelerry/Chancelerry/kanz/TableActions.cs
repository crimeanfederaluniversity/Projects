using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using Chancelerry.kanz;

namespace Chancelerry
{
    public class TableActions
    {
        private List<string> strList;
        private List<int> intList;
        private List<DateTime> dateList;
        private List<float> floatList;
        public static List<TextBox> SearchBoxsData = new List<TextBox>();
        public static Table DTable { get; set; }

        protected class DataOne
        {
            public string textValue { get; set; }
            public int instance { get; set; }
            public int version { get; set; }
            public bool deleted { get; set; }
            public int id { get; set; }

        }

        public class SearchValues
        {
            public int fieldId { get; set; }
            public string value { get; set; }
     
        }

        public class RenderCards
        {
            public int cardId { get; set; }
            public List<string> cardToRender { get; set; } 
        }

        private void RedirectToEdit(object sender, EventArgs e)
        {
            
            ImageButton thisButton = (ImageButton)sender;
            int currentCardId = Convert.ToInt32(thisButton.Attributes["_cardID"]);
            HttpContext.Current.Session["cardID"] = currentCardId; 
            HttpContext.Current.Session["version"] = 100500;
            HttpContext.Current.Session["canEdit"] = true;
            HttpContext.Current.Response.Redirect("~/kanz/CardEdit.aspx", true);
        }

        private void RedirectToView(object sender, EventArgs e)
        {
            ImageButton thisButton = (ImageButton)sender;
            int currentCardId = Convert.ToInt32(thisButton.Attributes["_cardID"]);
            HttpContext.Current.Session["cardID"] = currentCardId;
            HttpContext.Current.Session["version"] = 100500;
            HttpContext.Current.Session["canEdit"] = false;
            HttpContext.Current.Response.Redirect("~/kanz/CardEdit.aspx", true);
        }

        private void DeleteCard(object sender, EventArgs e)
        {
            ImageButton thisButton = (ImageButton)sender;
            int currentCardId = Convert.ToInt32(thisButton.Attributes["_cardID"]);
            HttpContext.Current.Session["cardID"] = currentCardId;

            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();

            var card = (from c in dataContext.CollectedCards
                        where
                            c.fk_register == (int) HttpContext.Current.Session["registerID"] &&
                            c.collectedCardID == currentCardId
                       select c).FirstOrDefault();

            if (card != null)
                card.active = false;

            dataContext.SubmitChanges();

            HttpContext.Current.Response.Redirect("~/kanz/RegisterView.aspx", true);
        }

        public TableRow AddRowFromList(List<string> list, int cardID)
        {
            TableRow row = new TableRow();
            row.BorderStyle = BorderStyle.Solid;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                cel.Text = elm;
                row.Cells.Add(cel);
            }

            TableCell cell = new TableCell();

            ImageButton buttonEdit = new ImageButton();
            buttonEdit.ImageUrl= "~/kanz/icons/editaddButtonIcon.gif";
            buttonEdit.Height = 20;
            buttonEdit.Width = 20;
            buttonEdit.Attributes.Add("_cardID", cardID.ToString());
            buttonEdit.Click += RedirectToEdit;

            ImageButton buttonView = new ImageButton();
            buttonView.Height = 20;
            buttonView.Width = 20;
            buttonView.ImageUrl = "~/kanz/icons/viewButtonIcon.png";
            buttonView.Attributes.Add("_cardID", cardID.ToString());
            buttonView.Click += RedirectToView;

            ImageButton buttonDelete = new ImageButton();
            buttonDelete.Height = 20;
            buttonDelete.Width = 20;
            buttonDelete.ImageUrl = "~/kanz/icons/delButtonIcon.jpg";
            buttonDelete.Attributes.Add("_cardID", cardID.ToString());
            buttonDelete.Click += DeleteCard;
            buttonDelete.OnClientClick = "return confirm('Вы уверены, что хотите удалить?');";


            row.Cells.Add(cell);

            cell.Controls.Add(buttonEdit);
            cell.Controls.Add(buttonView);
            cell.Controls.Add(buttonDelete);

            row.Controls.Add(cell);

            return row;
        }

        public TableRow AddRowFromList(List<int> list)
        {
            TableRow row = new TableRow();
            row.BorderStyle = BorderStyle.Solid;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                cel.Text = elm.ToString();
                row.Cells.Add(cel);
            }

            return row;
        }

        public TableHeaderRow AddHeaderRoFromList(List<string> list)
        {
            TableHeaderRow row = new TableHeaderRow();
            row.CssClass = "custom-th";
            row.BorderStyle = BorderStyle.Inset;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                var asd = elm;
                cel.Text = elm;
                row.Cells.Add(cel);
            }

            return row;
        }

        public TableHeaderRow AddSearchHeaderRoFromList(List<int> fieldID)
        {

            // Поиск

            TableHeaderRow row = new TableHeaderRow();
            row.BorderStyle = BorderStyle.Inset;

            foreach (var elm in fieldID)
            {
                TableCell cell = new TableCell();

                TextBox tb = new TextBox();
                tb.Attributes.Add("_fieldID4search", elm.ToString());
                tb.ID = "searchTb" + elm.ToString();
                tb.Attributes.Add("onkeypress", "return runScript(event);");
                tb.Attributes.Add("onkeydown", "return runScript(event);");
                tb.Attributes.Add("class", "search-field");
                cell.Controls.Add(tb);

                SearchBoxsData.Add(tb);
                row.Cells.Add(cell);
            }

            return row;
        }

        public void RefreshTable(ChancelerryDBDataContext dataContext, object userID, Registers register, object regId, Table dataTable, Dictionary<int, string> searchList)
        {
            dataTable.Rows.Clear();

            // Достаем поля для данного реестра и пользователя на основе RegisterView и прав пользователя RegistersUsersMap c сортировкой по весу
            var fieldsAll = (from regUsrMap in dataContext.RegistersUsersMap
                             join regView in dataContext.RegistersView on regUsrMap.registersUsersMapID equals regView.fk_registersUsersMap
                             join _fields in dataContext.Fields.OrderByDescending(n => n.fieldID == 1) on regView.fk_field equals _fields.fieldID
                             where regUsrMap.fk_user == Convert.ToInt32(userID) && regUsrMap.fk_register == register.registerID && regView.active
                             select new { _fields.name, _fields.fieldID, regView.weight }).OrderBy(w => w.weight).ToList();

            var fieldsName = (from f in fieldsAll select f.name).ToList();
            var fieldsId = (from f in fieldsAll select f.fieldID).ToList();


            // Поиск
            dataTable.Rows.Add(AddSearchHeaderRoFromList(fieldsId));

            // Заголовки
            dataTable.Rows.Add(AddHeaderRoFromList(fieldsName));

            // Карточки этого реестра + номера документов по которым фильтровать
            var cardsAllFull = (from card in dataContext.CollectedCards
                            join collected in dataContext.CollectedFieldsValues on card.collectedCardID equals collected.fk_collectedCard
                            join field in dataContext.Fields on collected.fk_field equals field.fieldID
                            where card.fk_register == (int)HttpContext.Current.Session["registerID"] && 
                                  card.active && 
                                  field.type == "autoIncrement"
                           select new DataOne(){ id = card.collectedCardID, textValue = collected.valueText}).ToList(); // LOG !!! {try catch в каком поле ошибка}

            // добавляем в version конвертированное значение номера документа из string в int для дальнейшего фильтра
            foreach (var itm in cardsAllFull)
            {
                int tmp = 0; // V
                Int32.TryParse(itm.textValue, out tmp); //V
                itm.version = tmp;
            }

            List<int> cardsToShow = new List<int>(); // сюда будем складывать карточки которые нужно показать

            if (searchList != null) //если фильтры есть
            {
                bool isFirst = true;
                foreach (int currentKey in searchList.Keys) // проходимся по каждому фильтру
                {
                    int fieldId = currentKey;
                    string fieldValue = "";
                    searchList.TryGetValue(fieldId, out fieldValue);  //достаем айдишник нашего филда

                    List<int> cardsWithValue = (from a in dataContext.CollectedFieldsValues
                                                where a.active == true && a.fk_field == fieldId && a.valueText.Contains(fieldValue)
                                                join b in dataContext.CollectedCards on a.fk_collectedCard equals b.collectedCardID
                                                where b.active == true
                                                select a.fk_collectedCard).Distinct().ToList(); // находим все карточки которые соответсвтуют
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
                // фильтруем по номерам документов и достаем только ID'шники карточек
                cardsToShow = (from a in cardsAllFull select a).OrderByDescending(n => n.version).ToList().Select(card => card.id).Distinct().ToList();
            }

            
            HttpContext.Current.Session["pageCount"] = (int)Math.Floor((double)cardsToShow.Count / 10) + 1; // количество страниц таблицы
            HttpContext.Current.Session["cardsCount"] = cardsToShow.Count;


            // Отрисовка //

            // по всем карточкам
            foreach (var card in cardsToShow.Skip((int)HttpContext.Current.Session["pageCntrl"] * 10).Take(10).ToList())
            {
                List<string> cardRow = new List<string>();

                foreach (var field in fieldsId) // проходим по каждому полю в карточке card
                {
                    StringBuilder fieldInstancesValue = new StringBuilder(); // сюда записываем все инстансы

                        // сотношение поля и  карточки в collected (получаем поле с его инстансами и версией)
                       var query = (from a in dataContext.CollectedFieldsValues
                                 where a.fk_field == field && a.fk_collectedCard == card
                                 select new DataOne()
                                 {
                                     textValue = a.valueText,
                                     instance = a.instance,
                                     version = a.version,
                                     deleted = a.isDeleted
                                 }).ToList();

                    // Список всех удаленных инстансов
                    var delInst = (from ins in query where ins.deleted select ins.instance).ToList();

                    // Получаем список всех инстансов для данного поля исключая удаленные
                    var instances = (from f in query
                                     where !f.deleted
                                     select f.instance).Distinct().ToList().Except(delInst.Distinct()).ToList();

                    var k = 1; // переменная для отображения порядкового номера инстанса

                    // Если инстанс 1 то отображаем без 1) и br
                    if (instances.Count <= 1)
                    {
                        fieldInstancesValue.Append(
                                (from vv in query
                                 where vv.instance == instances[0] && !vv.deleted
                                 select vv).OrderByDescending(v => v.version)
                                          .FirstOrDefault()?.textValue);
                    }
                    else
                    {
                        foreach (var instance in instances)
                        {
                            // Забираем максимальное значение(версия) textValue каждого инстанса
                            fieldInstancesValue.Append(
                                k.ToString() + ") " +
                                (from vv in query
                                 where vv.instance == instance && !vv.deleted
                                 select vv).OrderByDescending(v => v.version)
                                          .FirstOrDefault()?.textValue + "<br>"); //если не null за писываем в поле
                            k++;
                        }
                    }


                    // Добавляем значения в Row каждого поля
                    cardRow.Add(fieldInstancesValue.ToString());
                }

                    dataTable.Rows.Add(AddRowFromList(cardRow, card)); // Добавляем в таблицу Row
            }
            

        }


    }
}
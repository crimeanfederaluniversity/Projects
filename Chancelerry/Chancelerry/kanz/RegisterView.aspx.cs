using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class RegisterView : System.Web.UI.Page
    {

        public class DataOne
        {
            public string textValue { get; set; }
            public int instance { get; set; }
            public int version { get; set; }

            public bool deleted { get; set; }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            /////////////////////////////////////////////////////////////////////

            var regId = Session["registerID"];
            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();

            var register = 
                (from r in dataContext.Registers
                 where r.registerID == Convert.ToInt32(regId)
                 select r).FirstOrDefault();

            
            if (register != null)
            {
                RegisterNameLabel.Text = register.name;
       
                TableActions ta = new TableActions();

                // Достаем поля для данного реестра и пользователя на основе RegisterView и прав пользователя RegistersUsersMap c сортировкой по весу
                var fieldsAll = (from regUsrMap in dataContext.RegistersUsersMap
                                 join regView in dataContext.RegistersView on regUsrMap.registersUsersMapID equals regView.fk_registersUsersMap
                                 join _fields in dataContext.Fields on regView.fk_field equals _fields.fieldID
                                 where regUsrMap.fk_user == Convert.ToInt32(userID) && regUsrMap.fk_register == register.registerID
                                 select new { _fields.name, _fields.fieldID, regView.weight}).OrderBy(w=>w.weight).ToList();

                var fieldsName = (from f in fieldsAll select f.name).ToList();
                var fieldsId = (from f in fieldsAll select f.fieldID).ToList();



                // Заголовки
                dataTable.Rows.Add(ta.AddHeaderRoFromList(fieldsName));

                // Карточки этого реестра
                var cards = (from card in dataContext.CollectedCards
                    join r in dataContext.Registers on card.fk_register equals r.registerID
                    where r.registerID == Convert.ToInt32(regId)
                             select card.collectedCardID).ToList();


                // по всем карточкам
                foreach (var card in cards)
                {
                    List<string> cardRow = new List<string>();
                    GetValue(fieldsId, dataContext, card, cardRow); // функция добавления значения Row для каждой карточки и поля
                    dataTable.Rows.Add(ta.AddRowFromList(cardRow)); // Добавляем в таблицу Row
                }

              }
            }

        private static void GetValue(List<int> fieldsId, ChancelerryDBDataContext dataContext, int card, List<string> cardRow)
        {
            foreach (var field in fieldsId) // проходим по каждому полю в карточке card
            {

                StringBuilder fieldValue = new StringBuilder(); // результирующее значение Cell со всеми инстансами для добавления в Row

                // сотношение поля и  карточки в collected (получаем поле с его инстансами и версией)
                var query = (from a in dataContext.CollectedFieldsValues
                             where a.fk_field == field && a.fk_collectedCard == card
                             select new DataOne(){ textValue = a.valueText,
                                                   instance = a.instance,
                                                   version = a.version,
                                                   deleted = a.isDeleted }).ToList();

               //var instanceFilter = (from f in query where !f.deleted select new DataOne() { instance = f.instance, version = f.version, textValue = f.textValue, deleted = f.deleted }).OrderByDescending(ver => ver.version).ToList();

                // Список всех удаленных инстансов
                var delInst = (from ins in query where ins.deleted select ins.instance).ToList();

                // Получаем список всех инстансов для данного поля исключая удаленные
                var instances = (from f in query
                    where !f.deleted
                    select f.instance).Distinct().ToList().Except(delInst.Distinct()).ToList();

                var k = 1; // переменная для отображения порядкового номера инстанса

                foreach (var instance in instances)
                {
                    // Забираем максимальное значение(версия) textValue каждого инстанса
                    fieldValue.Append(
                        k.ToString() + ") " + (from vv in query where vv.instance == instance && !vv.deleted select vv).OrderByDescending(v => v.version)
                            .FirstOrDefault()?.textValue + "<br>"); //если не null за писываем в поле
                    k++;
                }
                // Добавляем значения в Row каждого поля
                cardRow.Add(fieldValue.ToString());

            }
        }
    }
}
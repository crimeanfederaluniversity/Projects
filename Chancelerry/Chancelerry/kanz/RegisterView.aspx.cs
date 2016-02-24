﻿using System;
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
                var regName = register.name;
                var regModel = register.fk_registersModel;

                RegisterNameLabel.Text = regName;

                TableActions ta = new TableActions();

                // Достаем поля для данного реестра и пользователя на основе RegisterView и прав пользователя RegistersUsersMap c сортировкой по весу
                var fieldsName = (from regUsrMap in dataContext.RegistersUsersMap
                              join regView in dataContext.RegistersView on regUsrMap.registersUsersMapID equals regView.fk_registersUsersMap
                              join _fields in dataContext.Fields on regView.fk_field equals _fields.fieldID
                              where regUsrMap.fk_user == Convert.ToInt32(userID) && regUsrMap.fk_register == register.registerID
                              select _fields.name).ToList();

                var fieldsId = (from regUsrMap in dataContext.RegistersUsersMap
                                  join regView in dataContext.RegistersView on regUsrMap.registersUsersMapID equals regView.fk_registersUsersMap
                                  join _fields in dataContext.Fields on regView.fk_field equals _fields.fieldID
                                  where regUsrMap.fk_user == Convert.ToInt32(userID) && regUsrMap.fk_register == register.registerID
                                  select _fields.fieldID).ToList();


                // Заголовки
                dataTable.Rows.Add(ta.AddHeaderRoFromList(fieldsName));

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

                // сортируем по Inst
                //var sortbyInst = query.OrderByDescending(inst => inst.instance).ThenByDescending(ver=>ver.version);

                // Узнаем количество Inst'ансов (максимальный инстанс)
               // var instMax = (from m in sortbyInst select m.instance).FirstOrDefault();

               // var instanceFilter = (from f in query where !f.deleted select new DataOne() { instance = f.instance, version = f.version, textValue = f.textValue, deleted = f.deleted}).OrderByDescending(ver=>ver.version).ToList(); // Список всех инстансов данного поля, которые не удалены.

                // Получаем список всех инстансов для данного поля
                var instances = (from f in query
                    where !f.deleted
                    select f.instance).Distinct().ToList();

                var k = 1;
                foreach (var instance in instances)
                {
                    // Забираем максимальное значение(версия) textValue каждого инстанса
                    fieldValue.Append(
                        k.ToString() + ") " + (from vv in query where vv.instance == instance select vv).OrderByDescending(v => v.version)
                            .FirstOrDefault()?.textValue + "<br>"); //если не null за писываем в поле
                    k++;
                }
                // Добавляем значения в Row каждого поля
                cardRow.Add(fieldValue.ToString());
            }
        }
    }
}
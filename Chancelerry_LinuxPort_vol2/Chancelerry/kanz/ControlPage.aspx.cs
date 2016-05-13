using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class ControlPage : System.Web.UI.Page
    {

        public class Template1
        {
            /// Логика такая
            /// Заходит человек
            /// Выбирает нужный реестр
            /// Выбирает за какой срок получить данные
            /// Выбирает галочками что посмотреть 
            /// 1) Неисполненные (срок прошел)
            /// 2) Неисполненные (срок не прошел)
            /// 3) Исполненные с нарушением срока
            /// 4) Исполненные без нарушения срока
            /// Жмет кнопку 
            /// 
            /// 

            private int _registerModelId = 1; //Входящие

            private int _dateToFilterId = 7; // Дата поступления
            private DateTime _startDate;
            private DateTime _endDate;
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


            /// Так вот
            /// Тут будет работа шаблона для определения во входящих 
            /// Нам нужно жестко указать к какой модели относится шаблон
            /// В данном случае к модели с id 1
            /// Дальше нам нужно определиться по какому Field мы фильтруем дату
            /// Переменные под значения начала и конца дат (включительно)
            /// Нужна еще функция для установки дат
            /// Поскольку мы должны рыскать только в контрольных документах
            /// нам нужно найти эти документы
            /// есть несколько признаков таких документов
            /// 1) у них стоит метка контроль да
            /// 2) у них есть дата контроля
            /// 3) у них есть дата доп контроля
            /// итого нужно проверить одно поле на наличие 'да' и несколько полей на наличие чего либо
            /// Нашли все активные контрольные карточки по периоду
            /// 
            /// как должна выглядеть таблица
            /// 
            ///Номер карты/////Дата поступления////Описание осно//////////////////////////////////////////////////////////////////////////////////////////
            ///             ///                 ///
            ///             ///                 ///
            ///             ///                 ///
            ///             ///                 ///
            ///             ///                 ///
            ///             ///                 ///
            ///             ///                 ///
            /////////////////////////////////////////////////////////////////////////////////////////////////////
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
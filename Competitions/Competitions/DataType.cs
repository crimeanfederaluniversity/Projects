using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Competitions
{
    public class DataType
    {
        private int dataTypeCount = 11;
        public string GetDataTypeName(int dataType)
        {
            switch (dataType)
            {
                case 0:
                {
                    return "Текст";
                }
                case 1:
                {
                    return "Дробное число";
                }
                case 2:
                {
                    return "Целое число";
                }
                case 3:
                {
                    return "Галочка";
                }
                case 4:
                {
                    return "По существующим таблицам (Выпадающий список)";
                }
                case 5:
                {
                    return "Дата";
                }
                case 6:
                {
                    return "По существующим таблицам (Все обязательно) (нет)";
                }
                case 7:
                {
                    return "Автоинкремент";
                }
                case 8:
                {
                    return "Сумма";
                }
                case 9:
                {
                    return "Константы(Выпадающий список)";
                }
                case 10:
                {                  
                    return "Константы(Все обязательно)";
                }
                default :
                {
                    return "error";
                }

            }
        }
        private ListItem GetDataTypeListItem(int dataType)
        {
            ListItem newListItem = new ListItem();
            newListItem.Text = GetDataTypeName(dataType);
            newListItem.Value = dataType.ToString();
            return newListItem;
        }
        public ListItem[] GetDataTypeListItemCollection()
        {
            ListItem[] newListItemAray = new ListItem[dataTypeCount];
            for (int i = 0; i < dataTypeCount; i++)
            {
                newListItemAray[i] =GetDataTypeListItem(i);
            }
            return newListItemAray;
        }
        public bool IsDataTypeDropDown(int dataType)
        {
            if (dataType == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeDate(int dataType)
        {
            if (dataType == 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeBit(int dataType)
        {
            if (dataType == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeFloat(int dataType)
        {
            if (dataType == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeInteger(int dataType)
        {
            if (dataType == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeText(int dataType)
        {
            if (dataType == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeNecessarilyShow(int dataType)
        {
            if (dataType == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeSum(int dataType)
        {
            if (dataType == 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeIterator(int dataType)
        {
            if (dataType == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeConstantDropDown(int dataType)
        {
            if (dataType == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeConstantNecessarilyShow(int dataType)
        {
            if (dataType == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
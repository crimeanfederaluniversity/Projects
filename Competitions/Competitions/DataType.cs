using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;

namespace Competitions
{
    public class DataType
    {
        private int dataTypeCount = 17;
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
                    return "По существующим таблицам (Все обязательно)";
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
                case 11:
                {
                    return "Минимальное значение";
                }
                case 12:
                {
                    return "Максимальное значение";
                }
                case 13:
                {
                    return  "Суммирование по критерию";
                }
                case 14:
                {
                    return "Название заявки";
                }
                case 15:
                {
                    return "Один к одному по критерию";
                }
                case 16:
                {
                    return "По существующим таблицам (Все обязательно) по критерию (CheckBox)";
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

        public bool DataTypeWithConnectionToCollected(int dataType)
        {
            if (IsDataTypeDropDown(dataType)
                || IsDataTypeSum(dataType)
                || IsDataTypeNecessarilyShow(dataType)
                || IsDataTypeMaxValue(dataType)
                || IsDataTypeMinValue(dataType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DataTypeWithConnectionToConstant(int dataType)
        {
            if (IsDataTypeConstantDropDown(dataType)
                || IsDataTypeConstantNecessarilyShow(dataType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DataTypeWithConnectionToColumnsWithParams(int dataType)
        {
            if ((IsDataTypeSymWithParam(dataType)) || (IsDataTypeOneToOneWithParams(dataType)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DataTypeDependOfBit(int dataType)
        {
            if (IsDataTypeNecessarilyShowWithParam(dataType))
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public bool IsDataTypeMinValue(int dataType)
        {
            if (dataType == 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeMaxValue(int dataType)
        {
            if (dataType == 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeSymWithParam(int dataType)
        {
            if (dataType == 13)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        public bool IsDataTypeNameOfApplication(int dataType)
        {
            if (dataType == 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeOneToOneWithParams(int dataType)
        {
            if (dataType == 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDataTypeNecessarilyShowWithParam(int dataType)
        {
            if (dataType == 16)
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
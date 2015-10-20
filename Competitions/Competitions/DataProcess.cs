﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;
using Competitions;

namespace Competitions
{
    public class ValidatorParams
    {
        public bool Enabled { set; get; }
        public bool RequireValidatorEnabled { set; get; }
        public double MinValue {set; get; }
        public double MaxValue {set; get; }
        public ValidationDataType ContentType {set; get; }
        public string ErrorMessage {set; get; }

        public TextBoxMode TextBoxTextMode { set; get; }
        public ValidatorParams(bool enabled, double minValue, double maxValue, ValidationDataType contenType,
            string errorMessage, TextBoxMode textBoxTextMode,bool enableRequireValidator)
        {
            Enabled = enabled;
            MinValue = minValue;
            MaxValue = maxValue;
            ContentType = contenType;
            ErrorMessage = errorMessage;
            TextBoxTextMode = textBoxTextMode;
            RequireValidatorEnabled = enableRequireValidator;
        }

        public ValidatorParams()
        {
            Enabled = false;
            RequireValidatorEnabled = false;
            MinValue = 0;
            MaxValue = 0;
            ContentType = ValidationDataType.String;
            ErrorMessage = "No error";
            TextBoxTextMode= TextBoxMode.SingleLine;
        }
    }
    public class DataProcess
    {
        private  ValidatorParams validator = new ValidatorParams();
        private double _toTotalUp = 0;

        public ValidatorParams GetValidatorParams()
        {
            return validator;
        }
        public void     ClearTotalUp()
        {
            _toTotalUp = 0;
        }
        public double   GetToTotalUpValue()
        {
            return _toTotalUp;
        }
        public string   GetStringValueFromCollectedData(int rowId, int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zColumnTable currentColumn = (from a in competitionDataBase.zColumnTable
                                          where a.ID == columnId
                                          select a).FirstOrDefault();
            DataType dataType = new DataType();
            if (columnId != null)
            {
                zCollectedDataTable currenCollectedDataTable = (from a in competitionDataBase.zCollectedDataTable
                                                                where a.Active == true
                                                                      && a.FK_ColumnTable == columnId
                                                                      && a.FK_CollectedRowsTable == rowId
                                                                select a).FirstOrDefault();

                if (dataType.IsDataTypeText(currentColumn.DataType))
                {
                    return currenCollectedDataTable.ValueText;
                }
              
            }
            return "";
        }
        public double   GetValueFromCollectedData(int rowId, int columnId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zColumnTable currentColumn = (from a in competitionDataBase.zColumnTable
                                          where a.ID == columnId
                                          select a).FirstOrDefault();
            DataType dataType = new DataType();
            if (columnId != null)
            {
                zCollectedDataTable currenCollectedDataTable = (from a in competitionDataBase.zCollectedDataTable
                                                                where a.Active == true
                                                                      && a.FK_ColumnTable == columnId
                                                                      && a.FK_CollectedRowsTable == rowId
                                                                select a).FirstOrDefault();

                if (dataType.IsDataTypeFloat(currentColumn.DataType))
                {
                    
                    return (double)currenCollectedDataTable.ValueDouble;
                }
                if (dataType.IsDataTypeInteger(currentColumn.DataType))
                {
                    return (double)currenCollectedDataTable.ValueInt;
                }
                if (dataType.IsDataTypeBit(currentColumn.DataType))
                {
                    if ((bool)currenCollectedDataTable.ValueBit)
                    {
                        return 1;
                    }
                }

            }
            return 0;
        }
        public string   GetReadWriteString(zColumnTable currentColumn, zCollectedDataTable currentCollectedData)
        {
            DataType dataType = new DataType();
            
            #region float

            if (dataType.IsDataTypeFloat(currentColumn.DataType))
            {
                validator.Enabled = true;
                validator.ContentType=ValidationDataType.Double;
                validator.ErrorMessage = "Только числовое значение!";
                validator.MinValue = -100000000000;
                validator.MaxValue = 100000000000;
                validator.RequireValidatorEnabled = true;
                validator.TextBoxTextMode = TextBoxMode.SingleLine;
                if (currentCollectedData.ValueDouble != null)
                {
                    double doubleValue = (double) currentCollectedData.ValueDouble;
                    _toTotalUp = doubleValue;
                    return doubleValue.ToString();
                }
            }

            #endregion
            #region Date

            if (dataType.IsDataTypeDate(currentColumn.DataType))
            {
                validator.Enabled = false;
                validator.ErrorMessage = "";
                validator.RequireValidatorEnabled = true;
                validator.TextBoxTextMode = TextBoxMode.Date;
                if (currentCollectedData.ValueDataTime != null)
                {
                    DateTime dateTime = (DateTime) currentCollectedData.ValueDataTime;
                    string tmpStr = dateTime.ToString().Split(' ')[0];
                    string[] tmpArray = tmpStr.Split('.');
                    string tmp2 = tmpArray[2] + "-" + tmpArray[1] + "-" + tmpArray[0];
                    return tmp2;
                }
            }

            #endregion
            #region int
            validator.Enabled = true;
            if (dataType.IsDataTypeInteger(currentColumn.DataType))
            {
                validator.ContentType = ValidationDataType.Integer;
                validator.ErrorMessage = "Только целочисленное значение!";
                validator.MinValue = Int32.MinValue;
                validator.MaxValue = Int32.MaxValue;
                validator.RequireValidatorEnabled = true;
                validator.TextBoxTextMode = TextBoxMode.SingleLine;
                if (currentCollectedData.ValueInt != null)
                {
                    int intValue = (int) currentCollectedData.ValueInt;
                    _toTotalUp = intValue;
                    return intValue.ToString();
                }
            }

            #endregion
            #region text
           
            if (dataType.IsDataTypeText(currentColumn.DataType))
            {
                validator.Enabled = false;
                validator.ContentType = ValidationDataType.String;
                validator.ErrorMessage = "Введите текст!";
                validator.MinValue = 1;
                validator.MaxValue = 50000;
                validator.RequireValidatorEnabled = true;
                validator.TextBoxTextMode = TextBoxMode.MultiLine;
                return currentCollectedData.ValueText;
            }

            #endregion
            return "0";
        }
        public bool     GetBoolDataValue(zColumnTable currentColumn, zCollectedDataTable currentCollectedData)
        {
            DataType dataType = new DataType();
            #region bit
            if (dataType.IsDataTypeBit(currentColumn.DataType))
            {
                if (currentCollectedData.ValueBit == null)
                {
                    return false;
                }
                else
                {
                    return (bool) currentCollectedData.ValueBit;
                }
            }
            #endregion
            return false;
        }
        public DateTime GetDateDataValue(zColumnTable currentColumn, zCollectedDataTable currentCollectedData)
        {
            DataType dataType = new DataType();

            #region date

            if (dataType.IsDataTypeDate(currentColumn.DataType))
            {
                if (currentCollectedData.ValueDataTime == null)
                {
                    return DateTime.Today;
                }
                else
                {
                    return ((DateTime) currentCollectedData.ValueDataTime);
                }
            }

            #endregion

            return DateTime.Today;
        }
        public string   GetReadOnlyString(zColumnTable currentColumn, zCollectedDataTable currentCollectedData, int applicationId, int currentRowId,int gvRowId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();
            #region iterator

            if (dataType.IsDataTypeIterator(currentColumn.DataType))
            {
                _toTotalUp = gvRowId;
                return (gvRowId).ToString();
            }

            #endregion
            #region sum

            if (dataType.IsDataTypeSum(currentColumn.DataType))
            {
                zColumnTable columnToSum = (from a in competitionDataBase.zColumnTable
                    where a.ID == currentColumn.FK_ColumnTable
                    select a).FirstOrDefault();
                double AllSum = 0;
                if (dataType.IsDataTypeInteger(columnToSum.DataType))
                {
                    AllSum = (int) (from a in competitionDataBase.zCollectedDataTable
                        where a.FK_ColumnTable == columnToSum.ID
                              && a.Active == true
                        join b in competitionDataBase.zCollectedRowsTable
                            on a.FK_CollectedRowsTable equals b.ID
                        where b.Active == true
                              && b.FK_ApplicationTable == applicationId
                        select a.ValueInt).Distinct().Sum();
                }
                if (dataType.IsDataTypeFloat(columnToSum.DataType))
                {
                    AllSum = (double)
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.FK_ColumnTable == columnToSum.ID
                                  && a.Active == true
                            join b in competitionDataBase.zCollectedRowsTable
                                on a.FK_CollectedRowsTable equals b.ID
                            where b.Active == true
                                  && b.FK_ApplicationTable == applicationId
                            select a.ValueDouble).Distinct().Sum();
                }
                if (dataType.IsDataTypeBit(columnToSum.DataType))
                {
                    AllSum =
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.FK_ColumnTable == columnToSum.ID
                                  && a.Active == true
                            join b in competitionDataBase.zCollectedRowsTable
                                on a.FK_CollectedRowsTable equals b.ID
                            where b.Active == true
                                  && b.FK_ApplicationTable == applicationId
                                  && a.ValueBit == true
                            select a).Distinct().Count();
                }
                _toTotalUp = AllSum;
                return AllSum.ToString();
            }

            #endregion
            #region ConstantNecessarily

            if (dataType.IsDataTypeConstantNecessarilyShow(currentColumn.DataType))
            {
                zCollectedDataTable getCollectedData =
                    (from a in competitionDataBase.zCollectedDataTable
                        where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                        select a).FirstOrDefault();
                return getCollectedData.ValueText;
            }

            #endregion
            #region MaxValue

            if (dataType.IsDataTypeMaxValue(currentColumn.DataType))
            {
                zColumnTable columnToFind = (from a in competitionDataBase.zColumnTable
                    where a.ID == currentColumn.FK_ColumnTable
                    select a).FirstOrDefault();
                string tmpResult = "";
                if (dataType.IsDataTypeInteger(columnToFind.DataType))
                {
                    _toTotalUp = (double) (from a in competitionDataBase.zCollectedDataTable
                        where a.FK_ColumnTable == columnToFind.ID
                              && a.Active == true
                        join b in competitionDataBase.zCollectedRowsTable
                            on a.FK_CollectedRowsTable equals b.ID
                        where b.Active == true
                              && b.FK_ApplicationTable == applicationId
                        select a.ValueInt).Distinct()
                        .OrderByDescending(param => param)
                        .FirstOrDefault();
                    tmpResult = _toTotalUp.ToString();
                }
                if (dataType.IsDataTypeFloat(columnToFind.DataType))
                {


                    _toTotalUp = (double) (from a in competitionDataBase.zCollectedDataTable
                        where a.FK_ColumnTable == columnToFind.ID
                              && a.Active == true
                        join b in competitionDataBase.zCollectedRowsTable
                            on a.FK_CollectedRowsTable equals b.ID
                        where b.Active == true
                              && b.FK_ApplicationTable == applicationId
                        select a.ValueDouble).Distinct()
                        .OrderByDescending(param => param)
                        .FirstOrDefault();
                    tmpResult = _toTotalUp.ToString();
                }

                if (dataType.IsDataTypeDate(columnToFind.DataType))
                {
                    tmpResult =
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.FK_ColumnTable == columnToFind.ID
                                  && a.Active == true
                            join b in competitionDataBase.zCollectedRowsTable
                                on a.FK_CollectedRowsTable equals b.ID
                            where b.Active == true
                                  && b.FK_ApplicationTable == applicationId
                            select a.ValueDataTime).Distinct()
                            .OrderByDescending(param => param)
                            .FirstOrDefault().ToString().Split(' ')[0];
                }

                return tmpResult;
            }

            #endregion
            #region MinValue

            if (dataType.IsDataTypeMinValue(currentColumn.DataType))
            {
                zColumnTable columnToFind = (from a in competitionDataBase.zColumnTable
                    where a.ID == currentColumn.FK_ColumnTable
                    select a).FirstOrDefault();
                string tmpResult = "";
                if (dataType.IsDataTypeInteger(columnToFind.DataType))
                {

                    _toTotalUp = (double)
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.FK_ColumnTable == columnToFind.ID
                                  && a.Active == true
                            join b in competitionDataBase.zCollectedRowsTable
                                on a.FK_CollectedRowsTable equals b.ID
                            where b.Active == true
                                  && b.FK_ApplicationTable == applicationId
                            select a.ValueInt).Distinct()
                            .OrderBy(param => param)
                            .FirstOrDefault();
                    tmpResult = _toTotalUp.ToString();
                }
                if (dataType.IsDataTypeFloat(columnToFind.DataType))
                {
                    _toTotalUp = (double)
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.FK_ColumnTable == columnToFind.ID
                                  && a.Active == true
                            join b in competitionDataBase.zCollectedRowsTable
                                on a.FK_CollectedRowsTable equals b.ID
                            where b.Active == true
                                  && b.FK_ApplicationTable == applicationId
                            select a.ValueDouble).Distinct()
                            .OrderBy(param => param)
                            .FirstOrDefault();
                    tmpResult = _toTotalUp.ToString();
                }

                if (dataType.IsDataTypeDate(columnToFind.DataType))
                {
                    tmpResult =
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.FK_ColumnTable == columnToFind.ID
                                  && a.Active == true
                            join b in competitionDataBase.zCollectedRowsTable
                                on a.FK_CollectedRowsTable equals b.ID
                            where b.Active == true
                                  && b.FK_ApplicationTable == applicationId
                            select a.ValueDataTime).Distinct()
                            .OrderBy(param => param)
                            .FirstOrDefault().ToString().Split(' ')[0];
                }
                return tmpResult;
            }

            #endregion
            #region CollectedNecessarily

            if (dataType.IsDataTypeNecessarilyShow(currentColumn.DataType))
            {
                zCollectedDataTable getCollectedData =
                    (from a in competitionDataBase.zCollectedDataTable
                        where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                        select a).FirstOrDefault();
                return getCollectedData.ValueText;
            }

            #endregion
            #region CollectedNecessarilyWithParams

            if (dataType.IsDataTypeNecessarilyShowWithParam(currentColumn.DataType))
            {
                if (currentCollectedData.ValueFK_CollectedDataTable != null)
                {
                    zCollectedDataTable getCollectedData =
                        (from a in competitionDataBase.zCollectedDataTable
                            where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                            select a).FirstOrDefault();
                    return getCollectedData.ValueText;
                }
            }

            #endregion
            #region sumWithParams

            if (dataType.IsDataTypeSymWithParam(currentColumn.DataType))
            {                                  
                zCollectedDataTable collectedDataFrom =
                    (from a in competitionDataBase.zCollectedDataTable
                        where a.FK_CollectedRowsTable == currentRowId
                              && a.FK_ColumnTable == currentColumn.FK_ColumnConnectFromTable
                        select a).FirstOrDefault();

                int iD = (int) collectedDataFrom.ValueFK_CollectedDataTable;
                //мы получили по сути ID мероприятия для которого считаем

                List<zCollectedRowsTable> collectedDataRowsToSum =
                    (from a in competitionDataBase.zCollectedDataTable
                        where a.Active == true
                              && a.FK_ColumnTable == currentColumn.FK_ColumnConnectToTable
                              && a.ValueFK_CollectedDataTable == iD
                        join b in competitionDataBase.zCollectedRowsTable
                            on a.FK_CollectedRowsTable equals b.ID
                        where b.Active == true
                        select b).Distinct().ToList();
                //получили список строк в таблице из которой берем числа для суммирования и только те строки которые нам нужны

                double sum = 0;
                foreach (zCollectedRowsTable currentRowToSum in collectedDataRowsToSum)
                {
                    sum += GetValueFromCollectedData(currentRowToSum.ID, (int) currentColumn.FK_ColumnTable);
                }
                _toTotalUp = sum;
                return sum.ToString();
            }

            #endregion
            #region aplikcationName

            if (dataType.IsDataTypeNameOfApplication(currentColumn.DataType))
            {

                return (from a in competitionDataBase.zApplicationTable
                    where a.ID == applicationId
                    select a.Name).FirstOrDefault();
            }

            #endregion
            #region oneToOneWithParams
            if (dataType.IsDataTypeOneToOneWithParams(currentColumn.DataType))
            {
                zColumnTable columnConnectetFrom = (from a in competitionDataBase.zColumnTable
                                                    where a.ID == currentColumn.FK_ColumnConnectFromTable
                                                    select a).FirstOrDefault();

                zColumnTable columnConnectetTo = (from a in competitionDataBase.zColumnTable
                                                    where a.ID == currentColumn.FK_ColumnConnectToTable
                                                    select a).FirstOrDefault();
                zColumnTable collumnToGetValue = (from a in competitionDataBase.zColumnTable
                    where a.ID == currentColumn.FK_ColumnTable
                    select a).FirstOrDefault();

                zCollectedDataTable collectedDataFrom = (from a in competitionDataBase.zCollectedDataTable
                    where a.Active == true
                          && a.FK_CollectedRowsTable == currentRowId
                          && a.FK_ColumnTable == columnConnectetFrom.ID
                    select a).FirstOrDefault(); // значение которое ссылается

                if ((columnConnectetFrom!=null)&&(columnConnectetTo!=null)&&(collumnToGetValue!=null))
                {
                    if (columnConnectetFrom.DataType == columnConnectetTo.DataType) // значит оба ссылаются на одного и того же// не тестировано
                    {
                       /* zCollectedRowsTable distatntRow = (from a in competitionDataBase.zCollectedRowsTable
                                                           where a.Active == true
                                                           join b in competitionDataBase.zCollectedDataTable
                                                               on a.ID equals b.FK_CollectedRowsTable
                                                           where b.ID == collectedDataFrom.ValueFK_CollectedDataTable
                                                           select a).FirstOrDefault();
                        
                        zCollectedDataTable distantRowValue = (from a in competitionDataBase.zCollectedDataTable
                                                               where a.Active == true
                                                                     && a.FK_CollectedRowsTable == distatntRow.ID
                                                                       && a.FK_ColumnTable == collumnToGetValue.ID
                                                               select a).FirstOrDefault();

                        if (distantRowValue != null)
                        {
                            if (IsCellReadWrite(collumnToGetValue.DataType))
                            {
                                return (GetReadWriteString(collumnToGetValue, distantRowValue));
                            }
                        }*/
                    }
                    else //наш ссылается на него
                    {
                        zCollectedRowsTable distatntRow = (from a in competitionDataBase.zCollectedRowsTable
                            where a.Active == true
                            join b in competitionDataBase.zCollectedDataTable
                                on a.ID equals b.FK_CollectedRowsTable
                            where b.ID == collectedDataFrom.ValueFK_CollectedDataTable
                            select a).FirstOrDefault();
                        //нащли далбнюю строку
                        if (distatntRow != null) //дальней строки нет
                        {
                            zCollectedDataTable distantRowValue = (from a in competitionDataBase.zCollectedDataTable
                                where a.Active == true
                                      && a.FK_CollectedRowsTable == distatntRow.ID
                                      && a.FK_ColumnTable == collumnToGetValue.ID
                                select a).FirstOrDefault();

                            if (distantRowValue != null)
                            {
                                if (IsCellReadWrite(collumnToGetValue.DataType))
                                {
                                    return (GetReadWriteString(collumnToGetValue, distantRowValue));
                                }
                            }
                        }
                    }
                }                         
            }
            #endregion
            return "";
        }

        public string GetDropDownSelectedValueString(zColumnTable currentColumn,  zCollectedDataTable currentCollectedData, int applicationId, int currentRowId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            DataType dataType = new DataType();

            #region ConstantDropDown

            if (dataType.IsDataTypeConstantDropDown(currentColumn.DataType))
            {
                zCollectedDataTable getCollectedData =
                    (from a in competitionDataBase.zCollectedDataTable
                     where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                     select a).FirstOrDefault();
                return getCollectedData.ValueText;
            }

            #endregion

            #region OtherTableDropDown

            if (dataType.IsDataTypeDropDown(currentColumn.DataType))
            {
                zCollectedDataTable getCollectedData =
                    (from a in competitionDataBase.zCollectedDataTable
                     where a.ID == currentCollectedData.ValueFK_CollectedDataTable
                     select a).FirstOrDefault();
                return getCollectedData.ValueText;
            }

            #endregion
            return "";
        }
        public bool IsCellDate(int dataTypeIndex)
        {
            DataType dataType = new DataType();
            return dataType.IsDataTypeDate(dataTypeIndex);
        }
        public bool IsCellCheckBox(int dataTypeIndex)
        {
            DataType dataType = new DataType();
            return dataType.IsDataTypeBit(dataTypeIndex);
        }
        public bool IsCellReadWrite(int dataTypeIndex)
        {
            DataType dataType = new DataType();
            if ((dataType.IsDataTypeText(dataTypeIndex)) || (dataType.IsDataTypeFloat(dataTypeIndex)) || (dataType.IsDataTypeInteger(dataTypeIndex))||
                (dataType.IsDataTypeDate(dataTypeIndex)))
            {
                return true;
            }
            return false;        
        }
        public bool IsCellReadOnly(int dataTypeIndex)
        {
            DataType dataType = new DataType();
            if ((dataType.IsDataTypeIterator(dataTypeIndex)) || (dataType.IsDataTypeSum(dataTypeIndex)) || (dataType.IsDataTypeConstantNecessarilyShow(dataTypeIndex))
                ||(dataType.IsDataTypeMaxValue(dataTypeIndex)) || (dataType.IsDataTypeMinValue(dataTypeIndex)) || (dataType.IsDataTypeNecessarilyShow(dataTypeIndex))
                ||(dataType.IsDataTypeSymWithParam(dataTypeIndex)) || (dataType.IsDataTypeNameOfApplication(dataTypeIndex)) || (dataType.IsDataTypeOneToOneWithParams(dataTypeIndex))
                || (dataType.IsDataTypeNecessarilyShowWithParam(dataTypeIndex)))
            {
                return true;
            }
            return false;
        }
        public bool IsCellDropDown(int dataTypeIndex)
        {
            DataType dataType = new DataType();
            if ((dataType.IsDataTypeConstantDropDown(dataTypeIndex)) || (dataType.IsDataTypeDropDown(dataTypeIndex)))
            {
                return true;
            }
            return false; 
        }
    }
}
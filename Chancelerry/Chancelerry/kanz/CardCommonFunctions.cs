using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public class CardCommonFunctions
    {
        ChancelerryDBDataContext chancDb = new ChancelerryDBDataContext();
        public Users GetUsersTableById(int userid)
        {
            return (from a in chancDb.Users where a.userID == userid && a.active select a).FirstOrDefault();
        }
        public RegistersModels GetRegisterModelById(int registerModelId)
        {
            return (from a in chancDb.RegistersModels where a.registerModelID == registerModelId && a.active select a).FirstOrDefault();
        }
        public List<FieldsGroups> GetFieldsGroupsInRegisterModelOrderByLine(int registerModelId)
        {
            return (from a in chancDb.FieldsGroups where a.fk_registerModel == registerModelId && a.active select a).Distinct().OrderBy(li => li.line).ToList();
        }
        public List<Fields> GetFieldsInFieldGroupOrderByLine(int fieldGroupId)
        {
            return (from a in chancDb.Fields where a.fk_fieldsGroup == fieldGroupId && a.active select a).Distinct().OrderBy(li => li.line).ToList();
        }
        public List<Fields> GetFieldsInRegisterModel(int registerModelId)
        {
            List<Fields> tmpFieldsList = new List<Fields>();
            List<FieldsGroups> tmpFieldsGroupsList = GetFieldsGroupsInRegisterModelOrderByLine(registerModelId);
            foreach (FieldsGroups currentFieldsGroup in tmpFieldsGroupsList)
            {
                List<Fields> tmpField = GetFieldsInFieldGroupOrderByLine(currentFieldsGroup.fieldsGroupID);
                foreach (Fields currentField in tmpField)
                {
                    tmpFieldsList.Add(currentField);
                }
            }
            return tmpFieldsList;
        }

        
    }
    public class DataTypes
    {
        public string GetDataTypeName (int type )
        {
            switch (type)
            {
                case 0: return "bit";
                case 1: return "int";
                case 2: return "float";
                case 3: return "text";
                case 4: return "date";
            }
            return "error";
        }
    }
    public class CardCreateView
    {
        CardCommonFunctions _common = new CardCommonFunctions();

        public List<Fields> GetFieldsByLineSortedByColumn (int line, List<Fields> sourseFieldsList)
        {
            List<Fields> fieldsByLineSorted = (from a in sourseFieldsList
                where a.line == line
                select a).OrderBy(col => col.columnNumber).ToList();
            return fieldsByLineSorted;
        }

        public List<int> GetDistinctedSordetLinesFromFieldsList(List<Fields> sourseFieldsList)
        {
            return (from a in sourseFieldsList select a.line).Distinct().OrderBy(li => li).ToList();
        }

        public Panel CreateViewByRegisterModel(int registerModelId)
        {
            Panel cardMainPanel = new Panel();

            cardMainPanel.GroupingText = "1234";
            List<FieldsGroups> fieldGroupsToShow = _common.GetFieldsGroupsInRegisterModelOrderByLine(registerModelId);
            foreach (FieldsGroups currentFieldGroup in fieldGroupsToShow)
            {
                Panel groupPanel = new Panel();
                groupPanel.GroupingText = currentFieldGroup.name;
                List<Fields> fieldToShow = _common.GetFieldsInFieldGroupOrderByLine(currentFieldGroup.fieldsGroupID);
                List<int> linesToShow = GetDistinctedSordetLinesFromFieldsList(fieldToShow);
                
                foreach (int currentFieldsline in linesToShow)
                {
                    Panel linePanel = new Panel();
                    List<Fields> fieldsInLine = GetFieldsByLineSortedByColumn(currentFieldsline, fieldToShow);
                    int leftPadding = 5;
                    Table lineTable = new Table();
                    TableRow tableRow1 = new TableRow();
                    TableRow tableRow2 = new TableRow();
                    foreach (Fields currentField in fieldsInLine)
                    {
                        TableCell tableCell1 = new TableCell();
                        TableCell tableCell2 = new TableCell();
                        



                        Label currentFieldTitle = new Label();
                        //currentFieldTitle.Style["Position"] = "relative";
                        //currentFieldTitle.Style["display"] = "inline";               
                        //currentFieldTitle.Style["Left"] = leftPadding + "px";
                        //currentFieldTitle.Style["Top"] = 0 + "px";
                        currentFieldTitle.Text = currentField.name;
                        //linePanel.Controls.Add(currentFieldTitle);
                        tableCell1.Controls.Add(currentFieldTitle);

                        TextBox currentFieldTextBox = new TextBox();                       
                        //currentFieldTextBox.Style["Position"] = "relative";
                        //currentFieldTextBox.Style["display"] = "inline";
                        currentFieldTextBox.Style["Height"] = currentField.height + "px"; 
                        currentFieldTextBox.Style["Width"] = currentField.width + "px";
                        //currentFieldTextBox.Style["Left"] = leftPadding + "px";
                        //currentFieldTextBox.Style["Top"] = 10 + "px";
                        //linePanel.Controls.Add(currentFieldTextBox);

                        tableCell2.Controls.Add(currentFieldTextBox);

                        leftPadding += 5;
                        leftPadding += currentField.width;

                        tableRow1.Cells.Add(tableCell1);
                        tableRow2.Cells.Add(tableCell2);
                    }
                    lineTable.Rows.Add(tableRow1);
                    lineTable.Rows.Add(tableRow2);
                    groupPanel.Controls.Add(lineTable);
                    groupPanel.Controls.Add(linePanel);
                }
                cardMainPanel.Controls.Add(groupPanel);
            }
            return cardMainPanel;
        }
    }
}
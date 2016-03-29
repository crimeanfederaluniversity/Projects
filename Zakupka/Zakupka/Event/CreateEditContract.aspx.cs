﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zakupka.Event
{
    public partial class CreateEditContract : System.Web.UI.Page
    {
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();        
        public List<ValueSaveClass> ValuesList = new List<ValueSaveClass>();

        public class ValueSaveClass
        {
            public TextBox Value { get; set; }
            public int FieldId { get; set; }
            public int ContractId { get; set; }
        }

        public string GetCollectedValue(int fieldId, int contractId)
        {
            //вытаскиваем значение если нету то создаем
            if (fieldId != null && contractId != null)
            {
                CollectedValues fk = (from a in zakupkaDB.CollectedValues
                                      where a.fk_contract == contractId
                                          && a.fk_field == fieldId
                                      select a).FirstOrDefault();
                if(fk == null)
                {
                    CollectedValues newvalue = new CollectedValues();
                    newvalue.active = true;
                    newvalue.value = "";
                    newvalue.fk_field = fieldId;
                    newvalue.fk_contract = contractId;
                    newvalue.createDateTime = DateTime.Now;
                    zakupkaDB.CollectedValues.InsertOnSubmit(newvalue);
                    zakupkaDB.SubmitChanges();
                }
                return fk.value;
            }
            return "0";
        }

        public Table CreateNewTable ()
        {
            int step = Convert.ToInt32(Session["step"]);
            int conId = Convert.ToInt32(Session["contractID"]);
            Table tableToReturn = new Table();
            List<Fields> allFields = (from a in zakupkaDB.Fields where a.active && a.step == step select a).ToList();
            int maxLine = (from a in allFields select a.line).OrderByDescending(mc => mc).FirstOrDefault();

            for (int i =0; i<maxLine+1; i++)
            {
                TableRow newRow = new TableRow();
                TableRow headerNewRow = new TableRow();
                List<Fields> fieldsInLine = (from a in allFields where a.line == i select a).OrderBy(mc=>mc.col).ToList();
                foreach (Fields field in fieldsInLine)
                {
                    TableCell newCell = new TableCell();
                    TableCell headerNewCell = new TableCell();
                    headerNewCell.Text = field.name;

                    TextBox newTextBox = new TextBox();
                    newTextBox.Width = field.width;
                    newTextBox.Height = field.height;
                    //newTextBox.ID = field.filedID;
                    int contractId = conId;// вытаскиваеш из сессии
                    newTextBox.Text = GetCollectedValue(field.filedID, contractId);

                    ValueSaveClass classTmp = new ValueSaveClass();
                    classTmp.Value = newTextBox;
                    classTmp.FieldId = field.filedID;
                    classTmp.ContractId = contractId;
                    ValuesList.Add(classTmp);

                    newCell.Controls.Add(newTextBox);

                    headerNewRow.Cells.Add(headerNewCell);
                    newRow.Cells.Add(newCell);
                }
                tableToReturn.Rows.Add(headerNewRow);
                tableToReturn.Rows.Add(newRow);
            }
            return tableToReturn;           
        }


        public void SaveCollectedValue (int fieldId, int contractId, string value)
        {
            // находиш в базе коллектед по филду и контракту и меняеш его значение
            if (fieldId != null && contractId != null)
            {
                CollectedValues fk = (from a in zakupkaDB.CollectedValues
                                      where a.fk_contract == contractId
                                          && a.fk_field == fieldId
                                      select a).FirstOrDefault();
                fk.value = value;
                zakupkaDB.SubmitChanges();
            }           
        }

        public void SaveAll()
        {
            foreach (ValueSaveClass tmp in ValuesList)
            {
                SaveCollectedValue(tmp.FieldId, tmp.ContractId, tmp.Value.Text);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            TableDiv.Controls.Add(CreateNewTable());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveAll();
            TableDiv.Controls.Add(CreateNewTable());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public DataValidator validator = new DataValidator();
        public class ValueSaveClass
        {
            public TextBox Value { get; set; }
            public int FieldId { get; set; }
            public int ContractId { get; set; }
        }

        public string GetCollectedValue(int fieldId, int contractId, int userId)
        {
            //вытаскиваем значение если нету то создаем
            if (fieldId != null && contractId != null && userId != null)
            {
                CollectedValues fk = (from a in zakupkaDB.CollectedValues
                                      where a.fk_contract == contractId
                                          && a.fk_field == fieldId && a.fk_user == userId 
                                      select a).FirstOrDefault();
                if(fk != null)
                {
                    if (fk.active == false)
                    {
                        fk.active = true;
                        zakupkaDB.SubmitChanges();
                    }                       
                }
                else
                {
                    fk  = new CollectedValues();
                    fk.active = true;
                    fk.value = "";
                    fk.fk_field = fieldId;
                    fk.fk_contract = contractId;
                    fk.fk_user = userId;
                    fk.createDateTime = DateTime.Now;
                    zakupkaDB.CollectedValues.InsertOnSubmit(fk);
                    zakupkaDB.SubmitChanges();
                }
                                
                return fk.value;
            }
            return "0";
        }

        public Table CreateNewTable ()
        {
            int step = Convert.ToInt32(Session["step"]);
            int userID = Convert.ToInt32(Session["userID"]);
            int conId = Convert.ToInt32(Session["contractID"]);    
            Contracts name = (from a in zakupkaDB.Contracts where a.contractID == conId select a).FirstOrDefault();
            Label1.Text = name.name.ToString();
            Table tableToReturn = new Table();
            List<Fields> allFields = (from a in zakupkaDB.Fields where a.active && a.step == step
                                     select a).ToList();
            int maxLine = (from a in allFields select a.line).OrderByDescending(mc => mc).FirstOrDefault();

            for (int i = 0; i < maxLine + 1; i++)
            {
                TableRow newRow = new TableRow();
                TableRow headerNewRow = new TableRow();
                List<Fields> fieldsInLine = (from a in allFields where a.line == i select a).OrderBy(mc => mc.col).ToList();
                foreach (Fields field in fieldsInLine)
                {
                    TableCell newCell = new TableCell();
                    TableCell headerNewCell = new TableCell();
                    headerNewCell.Text = field.name;

                    TextBox newTextBox = validator.GetTextBox(field.type);
                    newTextBox.Width = field.width;
                    newTextBox.Height = field.height;
                    newTextBox.ID = "textBox"+ field.filedID.ToString();
                    int contractId = conId;// вытаскиваеш из сессии
                    newTextBox.Text = GetCollectedValue(field.filedID, contractId, userID);

                    ValueSaveClass classTmp = new ValueSaveClass();
                    classTmp.Value = newTextBox;
                    classTmp.FieldId = field.filedID;
                    classTmp.ContractId = contractId;
                    ValuesList.Add(classTmp);

                    newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.filedID, "textBox" + field.filedID.ToString(), field.type));
                    newCell.Controls.Add(newTextBox);

                    newRow.Cells.Add(newCell);

                    headerNewRow.Cells.Add(headerNewCell);
                }
                tableToReturn.Rows.Add(headerNewRow);
                tableToReturn.Rows.Add(newRow);
            }
            Session["values"] = ValuesList;
            return tableToReturn;  
                     
        }


        public void SaveCollectedValue (int fieldId, int contractId, string value)
        {
            if (fieldId != null && contractId != null)
            {
                Fields fieldtype = (from a in zakupkaDB.Fields
                                    where a.filedID == fieldId
                                        && a.active == true
                                    select a).FirstOrDefault();
                DataValidator check = new DataValidator();
              
                TextBox currentFieldTextBox = check.GetTextBox(fieldtype.type);
                TableDiv.Controls.Add(check.GetRangeValidator("RangeValidator" + currentFieldTextBox.ID, fieldtype.filedID.ToString(), fieldtype.type));
                // находиш в базе коллектед по филду и контракту и меняеш его значение

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
            int step = Convert.ToInt32(Session["step"]);
            List<Fields> autosum = (from a in zakupkaDB.Fields
                                    where   a.active == true && a.step == step && a.autosum != null
                                    select a).ToList();
            List<Fields> autosumvalue = (from a in zakupkaDB.Fields
                                    where a.active == true && a.step == step && a.autosum != null && a.autocalculate == true
                                    select a).ToList();
            List<CollectedValues> valueforsum = new List<CollectedValues>();
            int conId = Convert.ToInt32(Session["contractID"]);

            foreach (var tmp in autosum)
            {
                CollectedValues value = (from a in zakupkaDB.CollectedValues
                                         where a.fk_contract == conId
                                             && a.fk_field == tmp.filedID
                                         select a).FirstOrDefault();
                valueforsum.Add(value);
            }  
            foreach(var tmp in autosumvalue)
            {
                CollectedValues value = (from a in zakupkaDB.CollectedValues
                                         where a.fk_contract == conId
                                             && a.fk_field == tmp.filedID
                                         select a).FirstOrDefault();
                if (tmp.autosum == 5)
                {
                    List<CollectedValues> val = (from a in zakupkaDB.CollectedValues
                                                 where a.active == true && a.fk_contract == conId
                                                 join b in zakupkaDB.Fields
                                                 on a.fk_field equals b.filedID
                                                 where b.active == true && b.autocalculate == false && b.autosum == tmp.autosum
                                                 select a).ToList();

                    Decimal sum = Convert.ToDecimal(val[0].value) - Convert.ToDecimal(val[1].value);                
                    value.value = sum.ToString();
                    zakupkaDB.SubmitChanges();
                }
                else
                {
                    List<CollectedValues> val = (from a in zakupkaDB.CollectedValues
                                                 where a.active == true && a.fk_contract == conId
                                                 join b in zakupkaDB.Fields
                                                 on a.fk_field equals b.filedID
                                                 where b.active == true && b.autocalculate == false && b.autosum == tmp.autosum
                                                 select a).ToList();

                    Decimal sum = 0;

                    foreach (var n in val)
                    {
                        sum = sum + Convert.ToDecimal(n.value);

                    }

                    value.value = sum.ToString();
                    zakupkaDB.SubmitChanges();
                }
            }
                     
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            ValuesList = (List<ValueSaveClass>) Session["values"];
            if (ValuesList == null)
                ValuesList = new List<ValueSaveClass>();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {                    
            SaveAll();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
            Response.Redirect("~/Event/ContractPage.aspx");

        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Event/ContractPage.aspx");
        }
    }
}
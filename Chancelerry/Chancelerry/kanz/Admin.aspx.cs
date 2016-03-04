using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Chancelerry.kanz
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public int addCard(int fkRegister)
        {
            ChancelerryDBDataContext chancDB = new ChancelerryDBDataContext();
            CollectedCards newCard = new CollectedCards();
            newCard.active = true;
            newCard.fk_register = fkRegister;
            chancDB.CollectedCards.InsertOnSubmit(newCard);
            chancDB.SubmitChanges();
            return newCard.collectedCardID;
        }

        public void createFieldValue (int fkCard,int fkField,string value, int instance)
        {
            ChancelerryDBDataContext chancDB = new ChancelerryDBDataContext();
            CollectedFieldsValues newValue = new CollectedFieldsValues();
            newValue.active = true;
            newValue.fk_collectedCard = fkCard;
            newValue.fk_field = fkField;
            newValue.fk_user = 1;
            newValue.valueText = value;
            newValue.createDateTime = DateTime.Now;
            newValue.isDeleted = false;
            newValue.version = 1;
            newValue.instance = instance;
            chancDB.CollectedFieldsValues.InsertOnSubmit(newValue);
            chancDB.SubmitChanges();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string wholeFile = File.ReadAllText("c:\\1\\1.txt");
            string[] rows = wholeFile.Split('%');
            foreach (string currentRow in rows)
            {
                //char[] charsToTrim1 = { ' ', '\r', '\n', '\t',};
                int cardId = addCard(-1);

                string[] fields = currentRow.Split('#');
                int i = 0;
                int instanseofaddresy = 0;
                foreach (string currentField in fields)
                {
                    if (i==0)
                    {
                        createFieldValue(cardId, 40, currentField.Trim(), 0);
                    }
                    if (i==1)
                    {
                        createFieldValue(cardId, 41, currentField.Trim(), 0);
                    }
                    if (i==2)
                    {
                        createFieldValue(cardId, 42, currentField.Trim(), 0);
                    }
                    if (i==3)
                    {

                        string date = currentField[7].ToString() + currentField[8].ToString()+currentField[9].ToString()+currentField[10].ToString()+"-"+ currentField[4].ToString()+ currentField[5].ToString()+"-"+ currentField[1].ToString()+ currentField[2].ToString();
                        createFieldValue(cardId, 43, date, 0);
                    }
                    if (i==4)
                    {
                        createFieldValue(cardId, 44, currentField.Trim(), 0);
                    }
                    if (i==5)
                    {
                        createFieldValue(cardId, 45, currentField.Trim(), 0);
                    }
                    if (i==6)
                    {
                        char[] charsToTrim = { ' ', '\r', '\n', '\t', '"' };
                        string tmp = currentField.Trim(charsToTrim);
                        if (tmp.Contains("\n"))
                        {
                            string[] structs = tmp.Split('\n');
                            if (structs.Length > 0)
                            {
                                int b = 0;
                                foreach (string currentStruct in structs)
                                {
                                    string newTmp = currentStruct.Replace("\"\"", "\"");
                                    createFieldValue(cardId, 46, newTmp, b);
                                    b++;
                                }
                            }
                        }
                    }
                    if (i == 7)
                    {
                        if (currentField.Length > 5)
                        {
                            string[] tmp = currentField.Split(',');
                            int b = 0;
                            foreach (string currentValue in tmp)
                            {

                                createFieldValue(cardId, 48, currentValue.Trim(), b);
                                instanseofaddresy++;
                                b++;
                            }
                        }
                    }
                    if (i == 8)
                    {
                        if (currentField.Length > 5)
                        {
                            createFieldValue(cardId, 48, currentField.Trim(), instanseofaddresy);
                        }
                    }
                    if (i == 9)
                    {
                        createFieldValue(cardId, 50, currentField.Trim(), 0);
                    }
                    if (i == 10)
                    {
                        createFieldValue(cardId, 72, currentField.Trim(), 0);
                    }

                    i++;
                }

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string wholeFile = File.ReadAllText("c:\\1\\2.txt");
            string[] rows = wholeFile.Split('z');
            foreach (string currentRow in rows)
            {
                int cardId = addCard(8);
                string[] fields = currentRow.Split('#');
                int i = 0;
                //createFieldValue(cardId, 58, "1", 0);
                //createFieldValue(cardId, 61, "2", 0);
                //createFieldValue(cardId, 62, "3", 0);
                //createFieldValue(cardId, 64, "4", 0);
                foreach (string currentField in fields)
                {
                    if (i == 0)
                    {
                        createFieldValue(cardId, 1, currentField.Trim(), 0);
                    }
                    if (i == 1)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 2)
                    {
                        createFieldValue(cardId, 6, currentField.Trim(), 0);
                    }
                    if (i == 3)
                    {                  
                        createFieldValue(cardId, 8, currentField.Trim(), 0);
                    }
                    if (i == 4)
                    {
                        string date = "";
                        if (currentField.Length>2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 7, date, 0);
                        //createFieldValue(cardId, 7, currentField.Trim(), 0);
                    }
                    if (i == 5)
                    {
                        createFieldValue(cardId, 11, currentField.Trim(), 0);
                    }
                    if (i == 6)
                    {
                        createFieldValue(cardId, 12, currentField.Trim(), 0);
                    }
                    if (i == 7)
                    {
                        createFieldValue(cardId, 13, currentField.Trim(), 0);
                    }
                    if (i == 8)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 15, date, 0);
                        //createFieldValue(cardId, 15, currentField.Trim(), 0);
                    }
                    if (i == 9)
                    {
                        createFieldValue(cardId, 16, currentField.Trim(), 0);
                    }
                    if (i == 10)
                    {
                        createFieldValue(cardId, 17, currentField.Trim(), 0);
                    }
                    if (i == 11)
                    {
                        createFieldValue(cardId, 18, currentField.Trim(), 0);
                    }
                    if (i == 12)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 20, date, 0);
                        //createFieldValue(cardId, 20, currentField.Trim(), 0);
                    }
                    if (i == 13)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 21, date, 0);
                        //createFieldValue(cardId, 21, currentField.Trim(), 0);
                    }
                    if (i == 14)
                    {
                        createFieldValue(cardId, 22, currentField.Trim(), 0);
                    }
                    if (i == 15)
                    {
                        createFieldValue(cardId, 23, currentField.Trim(), 0);
                    }
                    if (i == 16)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 24, date, 0);
                    }
                    if (i == 17)
                    {
                       
                        createFieldValue(cardId, 25, currentField.Trim(), 0);
                       // createFieldValue(cardId, 24, currentField.Trim(), 0);
                    }
                    if (i == 18)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 26, date, 0);
                    }
                    if (i == 19)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 37, date, 0);
                        //createFieldValue(cardId, 26, currentField.Trim(), 0);
                    }
                    if (i == 20)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 39, date, 0);
                        //createFieldValue(cardId, 37, currentField.Trim(), 0);
                    }
                    if (i == 21)
                    {
                        createFieldValue(cardId, 31, currentField.Trim(), 0);
                        //createFieldValue(cardId, 39, currentField.Trim(), 0);
                    }
                    if (i == 22)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();

                        createFieldValue(cardId, 32, date, 0);
                    }
                    if (i == 23)
                    {
                                             createFieldValue(cardId, 33, currentField.Trim(), 0);
                        //createFieldValue(cardId, 32, currentField.Trim(), 0);
                    }
                    if (i == 24)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();

                        createFieldValue(cardId, 103, date, 0);
                    }
                    if (i == 25)
                    {
                        string date = "";
                        if (currentField.Length > 2)
                            date = currentField[7].ToString() + currentField[8].ToString() + currentField[9].ToString() + currentField[10].ToString() + "-" + currentField[4].ToString() + currentField[5].ToString() + "-" + currentField[1].ToString() + currentField[2].ToString();
                        createFieldValue(cardId, 105, date, 0);
                        //createFieldValue(cardId, 103, currentField.Trim(), 0);
                    }
                    if (i == 26)
                    {
                        createFieldValue(cardId, 106, currentField.Trim(), 0);
                    }
                    if (i == 27)
                    {
                        createFieldValue(cardId, 76, currentField.Trim(), 0);
                    }
                    i++;
                }


            }
        }


        public int GetFieldIdForCard(CollectedCards card)
        {
            ChancelerryDBDataContext chancDB = new ChancelerryDBDataContext();
            int valueToreturn = 0;
            Fields mainFieldInRegister = (from a in chancDB.Fields
                join b in chancDB.FieldsGroups
                    on a.fk_fieldsGroup equals b.fieldsGroupID
                join c in chancDB.RegistersModels
                    on b.fk_registerModel equals c.registerModelID
                join d in chancDB.Registers
                    on c.registerModelID equals d.fk_registersModel
                where d.registerID == card.fk_register
                      && (a.type == "autoIncrementReadonly" || a.type == "autoIncrement")
                select a).FirstOrDefault();
            if (mainFieldInRegister != null)
            {
                CollectedFieldsValues collectedId = (from a in chancDB.CollectedFieldsValues
                    where a.fk_field == mainFieldInRegister.fieldID
                          && a.fk_collectedCard == card.collectedCardID
                    select a).OrderByDescending(mc => mc.version).FirstOrDefault();
                if (collectedId != null)
                {
                    Int32.TryParse(collectedId.valueText, out valueToreturn);
                }
            }
            return valueToreturn;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            ChancelerryDBDataContext chancDB = new ChancelerryDBDataContext();
            List<CollectedCards> collectedCards = (from a in chancDB.CollectedCards select a).ToList();
            foreach (CollectedCards currentCard in collectedCards)
            {
                currentCard.mainFieldId = GetFieldIdForCard(currentCard);
            }
            chancDB.SubmitChanges();
        }
    }
}
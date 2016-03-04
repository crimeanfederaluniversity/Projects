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

        /*
        public int addCard(int fkRegister)
        {
            ChancelerryDBDataContext chancDb = new ChancelerryDBDataContext();
            CollectedCards newCard = new CollectedCards();
            newCard.active = true;
            newCard.fk_register = fkRegister;
            chancDb.CollectedCards.InsertOnSubmit(newCard);
            chancDb.SubmitChanges();
            return newCard.collectedCardID;
        }

        public void createFieldValue (int fkCard,int fkField,string value, int instance)
        {
            ChancelerryDBDataContext chancDb = new ChancelerryDBDataContext();
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
            chancDb.CollectedFieldsValues.InsertOnSubmit(newValue);
            chancDb.SubmitChanges();
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
            string[] rows = wholeFile.Split('%');
            foreach (string currentRow in rows)
            {
                int cardId = addCard(4);
                string[] fields = currentRow.Split('#');
                int i = 0;
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
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 4)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 5)
                    {
                        createFieldValue(cardId, 6, currentField.Trim(), 0);
                    }
                    if (i == 6)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 7)
                    {
                        createFieldValue(cardId, 1, currentField.Trim(), 0);
                    }
                    if (i == 8)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 9)
                    {
                        createFieldValue(cardId, 6, currentField.Trim(), 0);
                    }
                    if (i == 10)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 10)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                    if (i == 12)
                    {
                        createFieldValue(cardId, 6, currentField.Trim(), 0);
                    }
                    if (i == 13)
                    {
                        createFieldValue(cardId, 4, currentField.Trim(), 0);
                    }
                }
            }
        }*/
    }
}
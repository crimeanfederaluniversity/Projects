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
                int cardId = addCard(5);

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
                        createFieldValue(cardId, 43, currentField.Trim(), 0);
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
    }
}
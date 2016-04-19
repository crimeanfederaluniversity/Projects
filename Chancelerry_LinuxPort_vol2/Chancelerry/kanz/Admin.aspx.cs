using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Configuration;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class Admin : System.Web.UI.Page
    {
        private ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));


        public int addCard(int fkRegister, int mainFieldId)
        {
            
            CollectedCards newCard = new CollectedCards();
            newCard.Active = true;
            newCard.FkRegister = fkRegister;
            newCard.MaInFieldID = mainFieldId;
            chancDb.CollectedCards.InsertOnSubmit(newCard);
            chancDb.SubmitChanges();
            return newCard.CollectedCardID;
        }

        public void createFieldValue(int fkCard, int fkField, string value, int instance)
        {
          
            CollectedFieldsValues newValue = new CollectedFieldsValues();
            newValue.Active = true;
            newValue.FkCollectedCard = fkCard;
            newValue.FkField = fkField;
            newValue.FkUser = 1;
            newValue.ValueText = value;
            newValue.CreateDateTime = DateTime.Now;
            newValue.IsDeleted = false;
            newValue.Version = 1;
            newValue.Instance = instance;
            chancDb.CollectedFieldsValues.InsertOnSubmit(newValue);
            chancDb.SubmitChanges();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string str = "";
            List<CollectedCards> allCards = (from a in chancDb.CollectedCards where a.Active == true && a.FkRegister == 1 select a).OrderByDescending(mc=>mc.MaInFieldID).ToList();
            int maxValue = allCards[0].MaInFieldID.Value;
            for (int i=0;i<maxValue;i++)
            {
                int cntTmp = (from a in allCards where a.MaInFieldID == i select a).Count();
                if (cntTmp == 0)
                {
                    str += i.ToString() + Environment.NewLine;
                }
                else if (cntTmp != 1)
                {
                    str += i.ToString() + " ERRROR" + Environment.NewLine;
                }
            }
            TextBox1.Text = str;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            /*
            string wholeFile = File.ReadAllText("c:\\1\\5.txt");
            string[] rows = wholeFile.Split('z');
            foreach (string currentRow in rows)
            {
                string[] fields = currentRow.Split('#');
                int cardId = addCard(-1, Convert.ToInt32(fields[0]));

                
                int i = 0;
                foreach (string currentField in fields)
                {
                    if (i == 0)
                    {
                        createFieldValue(cardId, 78, currentField.Trim(), 0);
                    }
                    if (i == 1)
                    {
                        string date = currentField[6].ToString() + currentField[7].ToString() +
                                     currentField[8].ToString() + currentField[9].ToString() + "-" +
                                     currentField[3].ToString() + currentField[4].ToString() + "-" +
                                     currentField[0].ToString() + currentField[1].ToString();
                        createFieldValue(cardId, 79, date, 0);
                    }
                    if (i == 2)
                    {
                        createFieldValue(cardId, 83, currentField.Trim(), 0);
                    }
                    if (i == 3)
                    {

                        createFieldValue(cardId, 82, currentField.Trim(), 0);
                    }
                    if (i == 4)
                    {

                        createFieldValue(cardId, 81, currentField.Trim(), 0);
                    }
                    i++;
                }
            }*/
        }
    }
}
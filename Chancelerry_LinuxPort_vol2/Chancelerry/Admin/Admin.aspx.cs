﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class Admin : System.Web.UI.Page
    {
        private readonly ChancelerryDb _chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));


        public int AddCard(int fkRegister, int mainFieldId)
        {
            
            CollectedCards newCard = new CollectedCards();
            newCard.Active = true;
            newCard.FkRegister = fkRegister;
            newCard.MaInFieldID = mainFieldId;
            _chancDb.CollectedCards.InsertOnSubmit(newCard);
            _chancDb.SubmitChanges();
            return newCard.CollectedCardID;
        }

        public void CreateFieldValue(int fkCard, int fkField, string value, int instance)
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
            _chancDb.CollectedFieldsValues.InsertOnSubmit(newValue);
            _chancDb.SubmitChanges();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                int userId = (int)userID;
                if (userId != 1)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            string str = "";
            List<CollectedCards> allCards = (from a in _chancDb.CollectedCards where a.Active == true && a.FkRegister == 1 select a).OrderByDescending(mc=>mc.MaInFieldID).ToList();
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
            string wholeFile = File.ReadAllText("c:\\1\\6.txt");
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
                        createFieldValue(cardId, 125, currentField.Trim(), 0);
                    }
                    if (i == 1)
                    {
                        createFieldValue(cardId, 126, currentField.Trim(), 0);
                    }
                    if (i == 2)
                    {
                        if (currentField.Any())
                        {
                            string date = currentField[6].ToString() + currentField[7].ToString() +
                                          currentField[8].ToString() + currentField[9].ToString() + "-" +
                                          currentField[3].ToString() + currentField[4].ToString() + "-" +
                                          currentField[0].ToString() + currentField[1].ToString();
                            createFieldValue(cardId, 127, date, 0);
                        }
                    }
                    if (i == 3)
                    {

                        createFieldValue(cardId, 128, currentField.Trim(), 0);
                    }
                    if (i == 4)
                    {
                        createFieldValue(cardId, 129, currentField.Trim(), 0);
                    }
                    if (i == 5)
                    {

                        createFieldValue(cardId, 130, currentField.Trim(), 0);
                    }
                    if (i == 6)
                    {

                        createFieldValue(cardId, 131, currentField.Trim(), 0);
                    }
                    if (i == 7)
                    {
                        createFieldValue(cardId, 132, currentField.Trim(), 0);
                    }
                    
                    if (i == 8)
                    {
                        createFieldValue(cardId, 133, currentField.Trim(), 0);
                    }
                    if (i == 9)
                    {

                        createFieldValue(cardId, 134, currentField.Trim(), 0);
                    }
                    if (i == 10)
                    {

                        createFieldValue(cardId, 135, currentField.Trim(), 0);
                    }
                    if (i == 11)
                    {
                        if (currentField.Any())
                        {
                            string date = currentField[6].ToString() + currentField[7].ToString() +
                                          currentField[8].ToString() + currentField[9].ToString() + "-" +
                                          currentField[3].ToString() + currentField[4].ToString() + "-" +
                                          currentField[0].ToString() + currentField[1].ToString();
                            createFieldValue(cardId, 136, date, 0);
                        }
                    }
                    if (i == 12)
                    {

                        createFieldValue(cardId, 137, currentField.Trim(), 0);
                    }
                    if (i == 13)
                    {

                        createFieldValue(cardId, 138, currentField.Trim(), 0);
                    }
                    if (i == 14)
                    {

                        createFieldValue(cardId, 139, currentField.Trim(), 0);
                    }
                    if (i == 15)
                    {

                        createFieldValue(cardId, 140, currentField.Trim(), 0);
                    }
                    if (i == 16)
                    {
                        createFieldValue(cardId, 141, currentField.Trim(), 0);
                    }
                    if (i == 17)
                    {
                        if (currentField.Any())
                        {
                            string date = currentField[6].ToString() + currentField[7].ToString() +
                                          currentField[8].ToString() + currentField[9].ToString() + "-" +
                                          currentField[3].ToString() + currentField[4].ToString() + "-" +
                                          currentField[0].ToString() + currentField[1].ToString();
                            createFieldValue(cardId, 142, date, 0);
                        }
                    }
                    if (i == 18)
                    {

                        createFieldValue(cardId, 144, currentField.Trim(), 0);
                    }
                    if (i == 19)
                    {
                        if (currentField.Any())
                        {
                            string date = currentField[6].ToString() + currentField[7].ToString() +
                                          currentField[8].ToString() + currentField[9].ToString() + "-" +
                                          currentField[3].ToString() + currentField[4].ToString() + "-" +
                                          currentField[0].ToString() + currentField[1].ToString();
                            createFieldValue(cardId, 147, date, 0);
                        }
                    }
                    if (i == 20)
                    {
                        if (currentField.Any())
                        {
                            string date = currentField[6].ToString() + currentField[7].ToString() +
                                          currentField[8].ToString() + currentField[9].ToString() + "-" +
                                          currentField[3].ToString() + currentField[4].ToString() + "-" +
                                          currentField[0].ToString() + currentField[1].ToString();
                            createFieldValue(cardId, 148, date, 0);
                        }
                    }
                    if (i == 21)
                    {

                        createFieldValue(cardId, 149, currentField.Trim(), 0);
                    }
                    if (i == 22)
                    {
                        createFieldValue(cardId, 150, currentField.Trim(), 0);
                    }
                    if (i == 23)
                    {

                        createFieldValue(cardId, 151, currentField.Trim(), 0);
                    }
                    if (i == 24)
                    {
                        if (currentField.Any())
                        {
                            string date = currentField[6].ToString() + currentField[7].ToString() +
                                          currentField[8].ToString() + currentField[9].ToString() + "-" +
                                          currentField[3].ToString() + currentField[4].ToString() + "-" +
                                          currentField[0].ToString() + currentField[1].ToString();
                            createFieldValue(cardId, 143, date, 0);
                        }
                    }
                    i++;
                }
            }*/
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
            List<CollectedCards> allCardsInRegister =
                (from a in _chancDb.CollectedCards where a.FkRegister == 3 select a).ToList();
            int cnt = 0;

            List<CollectedFieldsValues> fieldAllValues = (from a in _chancDb.CollectedFieldsValues
                where a.FkField == 101

                join b in _chancDb.CollectedCards
                    on a.FkCollectedCard equals b.CollectedCardID
                where b.FkRegister == 3
                select a).ToList();

            foreach (CollectedCards currentCard in allCardsInRegister)
            {
                if (!(from a in fieldAllValues where a.FkCollectedCard == currentCard.CollectedCardID select a).Any())
                {
                    
                    CollectedFieldsValues addinfoFieldValue = new CollectedFieldsValues();
                    addinfoFieldValue.FkCollectedCard = currentCard.CollectedCardID;
                    addinfoFieldValue.FkField = 101;
                    addinfoFieldValue.FkUser = 1;
                    addinfoFieldValue.Active = true;
                    addinfoFieldValue.CreateDateTime = DateTime.Now;
                    addinfoFieldValue.Instance = 0;
                    addinfoFieldValue.Version = 0;
                    addinfoFieldValue.IsDeleted = false;
                    addinfoFieldValue.ValueText = "";
                    _chancDb.CollectedFieldsValues.InsertOnSubmit(addinfoFieldValue);
                    
                    cnt++;
                }
            }

           //_chancDb.SubmitChanges();
            
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class ZapolnenieForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridviewApdate();
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Questions_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private void GridviewApdate() //Обновление гридвью
        {
            CompetitionDBDataContext questionTable = new CompetitionDBDataContext();
            int idbid = (int)Session["ID_Bid"];
            int idkon = (int)Session["ID_Konkurs"];
            
                List<Questions> questionslist = (from a in questionTable.Questions
                                                 join b in questionTable.Konkurs_QuestionMapingTable
                                                 on idkon equals b.FK_Konkurs
                                                 where b.FK_Question == a.ID_Question
                                                 && a.Active == true
                                                 select a).ToList();
                DataTable dataTableQuestions = new DataTable();
                dataTableQuestions.Columns.Add(new DataColumn("ID_Question", typeof(int)));
                dataTableQuestions.Columns.Add(new DataColumn("Question", typeof(string)));
                dataTableQuestions.Columns.Add(new DataColumn("Answer", typeof(string)));
                dataTableQuestions.Columns.Add(new DataColumn("Id", typeof(string)));

                CompetitionDBDataContext NewAnswer = new CompetitionDBDataContext();
                foreach (Questions q in questionslist)
                {
                    DataRow dataRow = dataTableQuestions.NewRow();
                    dataRow["ID_Question"] = q.ID_Question.ToString();
                    dataRow["Question"] = q.Question;
                    Answers answer = (from a in NewAnswer.Answers
                                      where a.Active == true
                                      join b in NewAnswer.Question_AnswerMapingTable
                                          on a.ID_Answer equals b.FK_Answer
                                      where b.FK_Question == q.ID_Question &&
                                          a.FK_Bid == idbid
                                      select a).FirstOrDefault();
                    if (answer != null)
                    {
                        dataRow["Answer"] = answer.Answer;
                    }
                    else
                    {
                        answer = new Answers();
                        answer.Active = true;
                        answer.Answer = "";
                        answer.FK_Bid = idbid;
                        NewAnswer.Answers.InsertOnSubmit(answer);
                        NewAnswer.SubmitChanges();
                        Question_AnswerMapingTable newlink = new Question_AnswerMapingTable();
                        newlink.FK_Question = q.ID_Question;
                        newlink.FK_Answer = answer.ID_Answer;
                        newlink.Active = true;
                        NewAnswer.Question_AnswerMapingTable.InsertOnSubmit(newlink);
                        NewAnswer.SubmitChanges();

                    }
                    dataRow["Id"] = answer.ID_Answer;
                    dataTableQuestions.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTableQuestions;
                GridView1.DataBind();
              
                CompetitionDBDataContext NewValue = new CompetitionDBDataContext();
                DataTable dataTableTarget = new DataTable();
                dataTableTarget.Columns.Add(new DataColumn("ID_TargetIndicator", typeof(int)));
                dataTableTarget.Columns.Add(new DataColumn("TargetIndicator", typeof(string)));
                dataTableTarget.Columns.Add(new DataColumn("PurchaseValue", typeof(double)));
                dataTableTarget.Columns.Add(new DataColumn("Id_Value", typeof(string)));
                List<TargetIndicators> targetlist = (from a in NewValue.TargetIndicators
                                                     join b in NewValue.Konkurs_TargetMapingTable
                                                         on idkon equals b.FK_Konkurs
                                                     where b.FK_Target == a.ID_TargetIndicator && b.FK_Konkurs == idkon
                                                     select a).ToList();
                foreach (TargetIndicators t in targetlist)
                {
                    DataRow dataRow = dataTableTarget.NewRow();
                    dataRow["ID_TargetIndicator"] = t.ID_TargetIndicator.ToString();
                    dataRow["TargetIndicator"] = t.TargetIndicator;
                    TargetIndicatorValue purchasevalue = (from a in NewValue.TargetIndicatorValue
                                                          join b in NewValue.TargetIndicators
                                                              on a.FK_TargetIndicator equals b.ID_TargetIndicator
                                                          where a.Active == true && a.FK_Bid == idbid
                                                          select a).FirstOrDefault();
                    if (purchasevalue != null)
                    {
                        dataRow["PurchaseValue"] = purchasevalue.PurchaseValue;
                    }
                    else
                    {
                        purchasevalue = new TargetIndicatorValue();
                        purchasevalue.Active = true;
                        purchasevalue.PurchaseValue = 0;
                        purchasevalue.FK_Bid = idbid;
                        purchasevalue.FK_TargetIndicator = t.ID_TargetIndicator;
                        NewValue.TargetIndicatorValue.InsertOnSubmit(purchasevalue);
                        NewValue.SubmitChanges();
                    }
                    dataRow["Id_Value"] = purchasevalue.ID_TargetIndicatorValue;
                    dataTableTarget.Rows.Add(dataRow);
                }
       
            }

        
                   
        protected void Button2_Click(object sender, EventArgs e)
        {
           
       
            CompetitionDBDataContext Newanswer = new CompetitionDBDataContext();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox textAnswer = (TextBox)GridView1.Rows[i].FindControl("Answer");
                Label Id_stat = (Label)GridView1.Rows[i].FindControl("Id");
                   if ((textAnswer !=null) && (Id_stat != null))
                
                   {
                   Answers current = (from a in Newanswer.Answers  
                                         where a.Active == true && a.ID_Answer == Convert.ToInt32(Id_stat.Text)select a).FirstOrDefault();
            
                       current.Answer = textAnswer.Text;
                       Newanswer.SubmitChanges();
                   }
            }
           
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены!');", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TargetIndicator.aspx");
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
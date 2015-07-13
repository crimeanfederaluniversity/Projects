using System;
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
           
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
<<<<<<< .mine
            if (!(Page.IsPostBack))
            {
                CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
                List<Konkursy> comp = (from a in newCompetition.Konkursies where a.Active == true select a).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите конкурс");
                foreach (Konkursy n in comp)
                {
                    dictionary.Add(n.ID_Konkurs, n.Name);
                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
              
                }
                
            }
=======
>>>>>>> .r382

        }

        protected void Questions_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
<<<<<<< .mine
            if (DropDownList1.SelectedIndex==1)
            {
            Response.Redirect("~/Competitions_Pages/FormSmeta.aspx");
            }
            else
                Response.Redirect("~/ZapolnenieForm.aspx");
           /* GridView1.Visible = true;
            CompetitionDBDataContext newField = new CompetitionDBDataContext();
            CompetitionDBDataContext newValue = new CompetitionDBDataContext();
            List<Fields> table = (from a in newField.Fields where a.Active == true select a).ToList();
=======
            CompetitionDBDataContext questionTable = new CompetitionDBDataContext();
           
            int idbid = (int)Session["ID_Bid"];
            int idkon = (int)Session["ID_Konkurs"];
>>>>>>> .r382

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
    }
}
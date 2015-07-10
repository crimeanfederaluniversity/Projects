using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(Page.IsPostBack))
            {
                CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
                List<Konkursy> comp = (from a in newCompetition.Konkursy where a.Active == true select a).ToList();
               
                foreach (Konkursy n in comp)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.Number;
                    TmpItem.Value = n.ID_Konkurs.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDBDataContext newQuestion = new CompetitionDBDataContext();
            Questions newquestion = new Questions();
            newquestion.Active = true;
            newquestion.Question = TextBox1.Text;
            newQuestion.Questions.InsertOnSubmit(newquestion);
            newQuestion.SubmitChanges();

            foreach (ListItem n in CheckBoxList1.Items )
            {
                if (n.Selected == true)
                {
                Konkurs_QuestionMapingTable newlink = new Konkurs_QuestionMapingTable();
                newlink.Active = true;
                newlink.FK_Konkurs = Convert.ToInt32(n.Value);
                newlink.FK_Question = newquestion.ID_Question;
                newQuestion.Konkurs_QuestionMapingTable.InsertOnSubmit(newlink);
                newQuestion.SubmitChanges();
                }              
            }            
            
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "Данные внесены", true);
        
         
         }
    }
}
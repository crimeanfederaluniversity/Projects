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
            foreach (ListItem n in CheckBoxList1.Items)
            {
                if (n.Selected == true)
                {

            Smeta newquestion = new Smeta();
            newquestion.Active = true;
            newquestion.Name_state = TextBox1.Text;
            newquestion.Uniqvalue = TextBox2.Text;
            newquestion.Type_state = false;
            newquestion.FK_Konkurs = Convert.ToInt32(n.Value);
            
            newQuestion.Smeta.InsertOnSubmit(newquestion);
            newQuestion.SubmitChanges();
                }              
            }            
            
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "Данные внесены", true);
        
         
         }
    }
}
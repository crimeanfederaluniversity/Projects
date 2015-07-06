using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class Vvod : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
            Competitions comp = new Competitions();
            comp.Name = TextBox1.Text;
            comp.Number = TextBox2.Text;
            comp.StartDate = Calendar1.SelectedDate;
            comp.EndDate = Calendar2.SelectedDate;
            comp.Curator = TextBox3.Text;
            newCompetition.Competitions.InsertOnSubmit(comp);
            newCompetition.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Конкурс создан');", true);
        }
    }
}
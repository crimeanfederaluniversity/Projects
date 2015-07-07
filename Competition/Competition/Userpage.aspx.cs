using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class Userpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
            List<Competitions> comp = (from a in newCompetition.Competitions where a.Active == true select a).ToList();
            GridView1.DataSource =  comp;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ZapolnenieForm.aspx");
        }
    }
}
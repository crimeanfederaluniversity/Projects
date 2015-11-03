using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class WatchUniqueMarksInCompetition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetUniqueMarksButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionData = new CompetitionDataContext();
            var sessionParam = Session["CompetitionID"];
            if (sessionParam == null)
            {
                //error
                Response.Redirect("ChooseCompetition.aspx");
            }
            int competitionId = (int)sessionParam;

            List<zSectionTable> sectionsListInCompetition = (from a in competitionData.zSectionTable
                where a.FK_CompetitionsTable == competitionId
                      && a.Active == true
                select a).ToList();

            foreach (zSectionTable currentSection in sectionsListInCompetition)
            {
                string lineForDocument = "";
                List<zColumnTable> columnsInCurrentSection = (from a in competitionData.zColumnTable
                    where a.FK_SectionTable == currentSection.ID
                          && a.Active == true
                    select a).ToList();
                
                if (columnsInCurrentSection.Count > 1)
                {   lineForDocument += "ZTablez";
                    foreach (zColumnTable currentColumn in columnsInCurrentSection)
                    {
                        lineForDocument += currentColumn.UniqueMark + "z";                     
                    }
                    lineForDocument=lineForDocument.Remove(lineForDocument.Length - 1);
                    lineForDocument += 'Z';
                }
                else
                {
                    lineForDocument += "ZLinez" + columnsInCurrentSection[0].UniqueMark + "Z";
                }
                UniqueMarksTextBox.Text += lineForDocument + Environment.NewLine;
            }
        }
    }
}
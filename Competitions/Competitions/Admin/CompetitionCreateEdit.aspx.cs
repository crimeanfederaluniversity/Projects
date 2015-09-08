using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class NewCompetition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sessionParam = Session["CompetitionID"];
                if (sessionParam == null)
                {
                    //error
                    Response.Redirect("ChooseCompetition.aspx");
                }
                else
                {
                    int iD = (int)sessionParam;
                    if (iD > 0)
                    {
                        CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                        zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                            where a.Active == true
                                  && a.ID == iD
                            select a).FirstOrDefault();
                        if (currentCompetition == null)
                        {
                            //error
                            Response.Redirect("ChooseCompetition.aspx");
                        }
                        else
                        {
                            NameTextBox.Text = currentCompetition.Name;
                            DescriptionTextBox.Text = currentCompetition.Number;
                        }
                    }
                }
            }
        }
        protected void CreateSaveButtonClick(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
             var sessionParam = Session["CompetitionID"];
            if (sessionParam == null)
            {
                //error
                Response.Redirect("ChooseCompetition.aspx");
            }
            else
            {
                int iD = (int) sessionParam;
                if (iD > 0)
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                            where a.Active == true
                                  && a.ID == iD
                            select a).FirstOrDefault();
                        if (currentCompetition == null)
                        {
                            //error
                            Response.Redirect("ChooseCompetition.aspx");
                        }
                        else
                        {
                            currentCompetition.Name = NameTextBox.Text;
                            currentCompetition.Number = DescriptionTextBox.Text;
                            competitionDataBase.SubmitChanges();
                        }
                    }
                }
                else
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zCompetitionsTable newCompetition = new zCompetitionsTable();
                        newCompetition.Name = NameTextBox.Text;
                        newCompetition.Number = DescriptionTextBox.Text;
                        newCompetition.Active = true;
                        newCompetition.OpenForApplications = false;
                        competitionDataBase.zCompetitionsTable.InsertOnSubmit(newCompetition);
                        competitionDataBase.SubmitChanges();
                    }
                }
            }
            Response.Redirect("ChooseCompetition.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseCompetition.aspx");
        }
    }
}
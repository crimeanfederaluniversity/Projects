using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class CompetitionExpertEdit : System.Web.UI.Page
    {
        private List<UsersTable> GetExpertsInCompetitionList(int competitionId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                                        where a.Active == true
                                              && a.AccessLevel == 5
                                        join b in CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable
                                            on a.ID equals b.FK_UsersTable
                                        where b.Active == true
                                              && b.FK_CompetitionsTable == competitionId
                                        select a).ToList();
            return experts;
        }
        private List<UsersTable> GetExpertsOutCompetitionList(int competitionId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> allExperts = (from a in CompetitionsDataBase.UsersTable
                                           where a.Active == true
                                                 && a.AccessLevel == 5
                                           select a).ToList();
            List<UsersTable> expertsInCompetition = GetExpertsInCompetitionList(competitionId);
            foreach (UsersTable currentUser in expertsInCompetition)
            {
                allExperts.Remove(currentUser);
            }
            return allExperts;
        }
        private DataTable GetFilledDataTable(List<UsersTable> expertsList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            foreach (UsersTable currentExpert in expertsList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = currentExpert.ID;
                dataRow["Name"] = currentExpert.Email;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var appIdTmp = Session["CompetitionID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int competitionId = Convert.ToInt32(appIdTmp);

                connectedExpertsGV.DataSource = GetFilledDataTable(GetExpertsInCompetitionList(competitionId));
                unconnectedExpertsGV.DataSource = GetFilledDataTable(GetExpertsOutCompetitionList(competitionId));

                connectedExpertsGV.DataBind();
                unconnectedExpertsGV.DataBind();

            }
        }
        protected void ExpertDeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var appIdTmp = Session["CompetitionID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("CompetitionExpertEdit.aspx");
                }
                int competitionId = Convert.ToInt32(appIdTmp);

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zExpertsAndCompetitionMappngTamplateTable  expertAndCompetitionConnection =
                    (from a in CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable
                     where a.FK_UsersTable == Convert.ToInt32(button.CommandArgument)
                           && a.Active == true
                           && a.FK_CompetitionsTable == competitionId
                     select a).FirstOrDefault();
                if (expertAndCompetitionConnection != null)
                {
                    expertAndCompetitionConnection.Active = false;
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("CompetitionExpertEdit.aspx");
        }
        protected void ExpertAddButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var appIdTmp = Session["CompetitionID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("ApllicationExpertEdit.aspx");
                }
                int competitionId = Convert.ToInt32(appIdTmp);

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zExpertsAndCompetitionMappngTamplateTable expertAndCompetitionConnection =
                    (from a in CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable
                     where a.FK_UsersTable == Convert.ToInt32(button.CommandArgument)
                           && a.FK_CompetitionsTable == competitionId
                     select a).FirstOrDefault();
                if (expertAndCompetitionConnection != null)
                {
                    expertAndCompetitionConnection.Active = true;
                    CompetitionsDataBase.SubmitChanges();
                }
                else
                {
                    expertAndCompetitionConnection = new zExpertsAndCompetitionMappngTamplateTable();
                    expertAndCompetitionConnection.Active = true;
                    expertAndCompetitionConnection.FK_CompetitionsTable = competitionId;
                    expertAndCompetitionConnection.FK_UsersTable = Convert.ToInt32(button.CommandArgument);
                    CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable.InsertOnSubmit(expertAndCompetitionConnection);
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("CompetitionExpertEdit.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseCompetition.aspx");
        }
    }
}
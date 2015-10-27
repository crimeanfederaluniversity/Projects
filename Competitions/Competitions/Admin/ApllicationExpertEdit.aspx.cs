using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ApllicationExpertEdit : System.Web.UI.Page
    {
        private List<UsersTable> GetSovetCompetitionExpertsList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<zExpertsAndCompetitionMappngTamplateTable> sovetExperts = (from a in CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable
                                                                            where a.Active == true
                                                                            join b in CompetitionsDataBase.zApplicationTable
                                                                            on a.FK_CompetitionsTable equals b.FK_CompetitionTable
                                                                            where b.ID == applicationId
                                                                            join c in CompetitionsDataBase.zCompetitionsTable
                                                                            on b.FK_CompetitionTable equals c.ID
                                                                            where c.Active == true
                                                                            select a).ToList();

            List<UsersTable> sovetexpertnames = new List<UsersTable>();
            foreach (var currentUser in sovetExperts)
            {
                UsersTable sovexp = (from a in CompetitionsDataBase.UsersTable
                                     where a.ID == currentUser.FK_UsersTable
                                     select a).FirstOrDefault();
                sovetexpertnames.Add(sovexp);
            }
            return sovetexpertnames;
        }
        private List<UsersTable> GetExpertsInApplicationList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                where a.Active == true
                      && a.AccessLevel == 5
                join b in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                    on a.ID equals b.FK_UsersTable
                where b.Active == true
                      && b.FK_ApplicationsTable == applicationId
                select a).ToList();
            return experts;
        }
        private List<UsersTable> GetExpertsOutApplicationList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> allExperts = (from a in CompetitionsDataBase.UsersTable
                where a.Active == true
                      && a.AccessLevel == 5           
                select a).ToList();
            List<UsersTable> expertsInApplication = GetExpertsInApplicationList(applicationId);
            List<UsersTable> SovetexpertApplication = GetSovetCompetitionExpertsList(applicationId);
            foreach (UsersTable currentUser in expertsInApplication)
            {                
                foreach (UsersTable currentsovetuser in SovetexpertApplication)
                {
                    allExperts.Remove(currentUser);
                    allExperts.Remove(currentsovetuser);
                }
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
                var appIdTmp = Session["ApplicationID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int applicationId = Convert.ToInt32(appIdTmp);

              connectedExpertsGV.DataSource=GetFilledDataTable(GetExpertsInApplicationList(applicationId));
              unconnectedExpertsGV.DataSource=GetFilledDataTable(GetExpertsOutApplicationList(applicationId));
                
                
                connectedExpertsGV.DataBind();
                unconnectedExpertsGV.DataBind();
                
                    
            }
        }
        protected void ExpertDeleteButtonClick (object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var appIdTmp = Session["ApplicationID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("ApllicationExpertEdit.aspx");
                }
                int applicationId = Convert.ToInt32(appIdTmp);

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zExpertsAndApplicationMappingTable expertAndApplicationConnection =
                    (from a in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                        where a.FK_UsersTable == Convert.ToInt32(button.CommandArgument)
                              && a.Active == true
                              && a.FK_ApplicationsTable == applicationId
                        select a).FirstOrDefault();
                if (expertAndApplicationConnection != null)
                {
                    expertAndApplicationConnection.Active = false;
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("ApllicationExpertEdit.aspx");
        }
        protected void ExpertAddButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var appIdTmp = Session["ApplicationID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("ApllicationExpertEdit.aspx");
                }
                int applicationId = Convert.ToInt32(appIdTmp);

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zExpertsAndApplicationMappingTable expertAndApplicationConnection = (from a in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                                                                                     where a.FK_UsersTable == Convert.ToInt32(button.CommandArgument)
                                                                                     && a.FK_ApplicationsTable == applicationId
                                                                                     select a).FirstOrDefault();
                if (expertAndApplicationConnection != null)
                {
                    expertAndApplicationConnection.Active = true;
                    CompetitionsDataBase.SubmitChanges();
                }
                else
                {
                    expertAndApplicationConnection = new zExpertsAndApplicationMappingTable();
                    expertAndApplicationConnection.Active = true;
                    expertAndApplicationConnection.FK_ApplicationsTable = applicationId;
                    expertAndApplicationConnection.FK_UsersTable = Convert.ToInt32(button.CommandArgument);
                    CompetitionsDataBase.zExpertsAndApplicationMappingTable.InsertOnSubmit(expertAndApplicationConnection);
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("ApllicationExpertEdit.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseApplication.aspx");
        }

       






    }
}
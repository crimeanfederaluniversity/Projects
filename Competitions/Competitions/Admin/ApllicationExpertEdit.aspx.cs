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

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<UsersTable> sovetExperts = (from d in CompetitionsDataBase.UsersTable
                                                 where d.Active == true 
                                                 join a in   CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable
                                                 on d.ID equals a.FK_UsersTable
                                                 where a.Active == true
                                                 join b in CompetitionsDataBase.zApplicationTable
                                                 on a.FK_CompetitionsTable equals b.FK_CompetitionTable
                                                 where b.ID == applicationId
                                                 join c in CompetitionsDataBase.zCompetitionsTable
                                                 on b.FK_CompetitionTable equals c.ID
                                                 where c.Active == true
                                                 select d).ToList();

                List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                                            where a.Active == true
                                                  && a.AccessLevel == 5
                                            join b in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                                                on a.ID equals b.FK_UsersTable
                                            where b.Active == true
                                                  && b.FK_ApplicationsTable == applicationId
                                            select a).ToList();
                if (sovetExperts != null)
                {
                    foreach (UsersTable current in sovetExperts)
                    {
                        if (experts.Contains(current))
                        {
                            experts.Remove(current);
                        }                     
                    }
                }
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add("ID", typeof(string));
                dataTable1.Columns.Add("Name", typeof(string));
                foreach (UsersTable currentExpert in experts)
                {
                    DataRow dataRow = dataTable1.NewRow();
                    dataRow["ID"] = currentExpert.ID;
                    dataRow["Name"] = currentExpert.Email;
                    dataTable1.Rows.Add(dataRow);
                }
                connectedExpertsGV.DataSource = dataTable1;
                connectedExpertsGV.DataBind();

                List<UsersTable> allExperts = (from a in CompetitionsDataBase.UsersTable
                                               where a.Active == true
                                                     && a.AccessLevel == 5
                                               select a).ToList();
                if (sovetExperts != null)
                {
                    foreach (UsersTable current in sovetExperts)
                    {
                        allExperts.Remove(current);
                    }
                    foreach (UsersTable n in experts)
                    {
                        allExperts.Remove(n);
                    }

                }
                
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add("ID", typeof(string));
                dataTable2.Columns.Add("Name", typeof(string));
                foreach (UsersTable currentExpert in allExperts)
                {
                    DataRow dataRow = dataTable2.NewRow();
                    dataRow["ID"] = currentExpert.ID;
                    dataRow["Name"] = currentExpert.Email;
                    dataTable2.Rows.Add(dataRow);
                }
                unconnectedExpertsGV.DataSource = dataTable2;
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
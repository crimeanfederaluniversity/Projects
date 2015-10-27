using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class ApplicationSovetexpertEdit : System.Web.UI.Page
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
 
                sovetExpertsGV.DataSource = GetFilledDataTable(GetSovetCompetitionExpertsList(applicationId));
                sovetExpertsGV.DataBind();

            }

        }
        protected void LinkExpertsButtonClick(object sender, EventArgs e)
        {
            var appid = Session["ApplicationID"];
            if (appid != null)
            {

                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                                                        where a.Active == true && a.Accept == false
                                                              && a.ID == Convert.ToInt32(appid) && a.Sended == true
                                                        select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    List<zExpertsAndCompetitionMappngTamplateTable> sovetexpertlist =
                        (from a in competitionDataBase.zExpertsAndCompetitionMappngTamplateTable
                         where a.Active == true && a.FK_CompetitionsTable == currentApplication.FK_CompetitionTable
                         select a).ToList();

                    if (sovetexpertlist.Count == 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "alert('К данному конкурсу не выбран экспертный совет! Вы действительно хотите продолжить?');",
                            true);
                    }
                    else
                    {
                        foreach (var SovetExperts in sovetexpertlist)
                        {
                            zExpertsAndApplicationMappingTable sovetexpertlink =
                                new zExpertsAndApplicationMappingTable();
                            sovetexpertlink.Active = true;
                            sovetexpertlink.FK_ApplicationsTable = currentApplication.ID;
                            sovetexpertlink.FK_UsersTable = SovetExperts.FK_UsersTable;
                            competitionDataBase.zExpertsAndApplicationMappingTable.InsertOnSubmit(sovetexpertlink);
                            competitionDataBase.SubmitChanges();
                        }
                    }
                }

            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseApplication.aspx");
        }
    }
}
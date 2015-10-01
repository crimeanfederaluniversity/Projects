using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Curator
{
    public partial class CuratorExpertPointPage : System.Web.UI.Page
    {
        private List<UsersTable> GetExpertsInApplicationList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                                        where a.Active == true  && a.AccessLevel == 5
                                        join b in CompetitionsDataBase.zExpertsAndApplicationMappingTables
                                        on a.ID equals b.FK_UsersTable
                                        where b.Active == true && b.FK_ApplicationsTable == applicationId
                                        join c in CompetitionsDataBase.zExpertPointsValue
                                        on a.ID equals c.FK_ExpertsTable 
                                        where c.Active == true && c.Sended == true
                                        select a).Distinct().ToList();
            return experts;

        }

        private DataTable GetFilledDataTable(List<UsersTable> expertsList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("AccessLevel", typeof(int));

            foreach (UsersTable currentExpert in expertsList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = currentExpert.ID;
                dataRow["Name"] = currentExpert.Email;
                dataRow["AccessLevel"] = currentExpert.AccessLevel;
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

                ExpertsPointGV.DataSource = GetFilledDataTable(GetExpertsInApplicationList(applicationId));
                ExpertsPointGV.DataBind();

            }
        }
        protected void ExpertDownloadButtonClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Функционал в разработке!');", true);
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuratorApplications.aspx");
        }
        
    }
}
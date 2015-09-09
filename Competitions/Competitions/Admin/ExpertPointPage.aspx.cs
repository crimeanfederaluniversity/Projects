using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class ExpertPointPage : System.Web.UI.Page
    {    
        private List<UsersTable> GetExpertsInApplicationList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                where a.Active == true
                      && a.AccessLevel == 5 
                join b in CompetitionsDataBase.zExpertsAndApplicationMappingTables
                    on a.ID equals b.FK_UsersTable
                where b.Active == true
                      && b.FK_ApplicationsTable == applicationId
                select a).ToList();
            return experts;
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

                ExpertsPointGV.DataSource = GetFilledDataTable(GetExpertsInApplicationList(applicationId));
                ExpertsPointGV.DataBind();
        
            }
        }
        protected void ExpertDownloadButtonClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Функционал в разработке');", true);
        }
         
    }
}
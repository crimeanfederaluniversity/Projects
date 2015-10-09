using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ChooseCompetition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                    where a.Active == true
                    select a).ToList();

                foreach (zCompetitionsTable currentCompetition in competitionsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentCompetition.ID;
                    dataRow["Name"] = currentCompetition.Name;
                    dataRow["Number"] = currentCompetition.Number;
                    if ((bool)currentCompetition.OpenForApplications)
                    {
                        dataRow["Status"] = "Открыт";
                    }
                    else
                    {
                        dataRow["Status"] = "Закрыт";
                    }
                    
                    dataTable.Rows.Add(dataRow);
                }
                CompetitionsGV.DataSource = dataTable;
                CompetitionsGV.DataBind();
            }
        }      
        protected void OpenButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["CompetitionID"] = iD;
                Response.Redirect("ChooseSection.aspx"); //
            }           
        }
        protected void ChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["CompetitionID"] = iD;
                Response.Redirect("CompetitionCreateEdit.aspx");
            }           
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCompetitionsTable competitionToDelete = (from a in competitionDataBase.zCompetitionsTable
                    where a.ID == iD
                    select a).FirstOrDefault();
                if (competitionToDelete!=null)
                {
                    competitionToDelete.Active = false;
                    competitionDataBase.SubmitChanges();
                }
                else
                {
                    //error
                }
            } 
        }
        protected void NewCompetitionButton_Click(object sender, EventArgs e)
        {
            Session["CompetitionID"] = 0;
            Response.Redirect("CompetitionCreateEdit.aspx");
        }
        protected void ExpertSovetButtonClick(object sender, EventArgs e)
        {
             Button button = (Button) sender;
             if (button != null)
             {
                 Session["CompetitionID"] = button.CommandArgument;
                 Response.Redirect("CompetitionExpertEdit.aspx");
             }
        }
        
        protected void StartStopButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCompetitionsTable competitionToChangeStatus = (from a in competitionDataBase.zCompetitionsTable
                                                          where a.ID == iD
                                                          select a).FirstOrDefault();
                if (competitionToChangeStatus != null)
                {
                    if (competitionToChangeStatus.OpenForApplications == null)
                    {
                        competitionToChangeStatus.OpenForApplications = true;
                    }
                    else
                    {
                        competitionToChangeStatus.OpenForApplications = !competitionToChangeStatus.OpenForApplications;
                    }
                    competitionDataBase.SubmitChanges();
                }
                else
                {
                    //error
                }
            }
            Response.Redirect("ChooseCompetition.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
    }
}
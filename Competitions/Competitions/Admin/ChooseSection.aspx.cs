using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ChooseSection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sessionParam = Session["CompetitionID"];            
            if (sessionParam == null)
            {
                //error
                Response.Redirect("ChooseCompetition.aspx");
            }
            int competitionId = (int)sessionParam;               
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                CompetitionNameLabel.Text = (from a in competitionDataBase.zCompetitionsTables
                    where a.ID == competitionId
                    select a.Name).FirstOrDefault();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Description", typeof(string)));

                List<zSectionTable> sectionList  = (from a in competitionDataBase.zSectionTable
                                                             where a.Active == true
                                                             && a.FK_CompetitionsTable == competitionId
                                                             select a).ToList();

                foreach (zSectionTable currentSection in sectionList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentSection.ID;
                    dataRow["Name"] = currentSection.Name;
                    dataRow["Description"] = currentSection.Description;
                    dataTable.Rows.Add(dataRow);
                }

                SectionGV.DataSource = dataTable;
                SectionGV.DataBind();
            }
        }
        protected void OpenButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["SectionID"] = iD;
                Response.Redirect("ChooseColumn.aspx"); //
            }
        }
        protected void ChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["SectionID"] = iD;
                Response.Redirect("SectionCreateEdit.aspx");
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zSectionTable sectionToDelete = (from a in competitionDataBase.zSectionTable
                                                          where a.ID == iD
                                                          select a).FirstOrDefault();
                if (sectionToDelete != null)
                {
                    sectionToDelete.Active = false;
                    competitionDataBase.SubmitChanges();
                }
                else
                {
                    //error
                }
                
            }
        }
        protected void NewSection_Click(object sender, EventArgs e)
        {
            Session["SectionID"] = 0;
            Response.Redirect("SectionCreateEdit.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseCompetition.aspx");
        }

        protected void GoToConstListManagmentButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseConstantList.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Curator
{
    public partial class NewCuratorTZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompetitionDataContext curator = new CompetitionDataContext();
                List<zActionPRManualTable> comp = (from a in curator.zActionPRManualTable where a.Active == true select a).ToList();
                foreach (zActionPRManualTable n in comp)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.ActionPR;
                    TmpItem.Value = n.ID.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }
            }
        }
        protected void CreateSaveButtonClick(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();          
                zCompetitionsTable newCompetition = new zCompetitionsTable();                
                newCompetition.Name = NameTextBox.Text;
                newCompetition.Number = DescriptionTextBox.Text;
                newCompetition.Budjet = Convert.ToDouble(BudjetTextBox.Text);
                // newCompetition.FK_Curator = user;
                newCompetition.StartDate = Calendar1.SelectedDate;
                newCompetition.EndDate = Calendar2.SelectedDate;
                newCompetition.Active = true;
                newCompetition.OpenForApplications = false;
                competitionDataBase.zCompetitionsTable.InsertOnSubmit(newCompetition);
                competitionDataBase.SubmitChanges();
                foreach (ListItem current in CheckBoxList1.Items)
                {
                    zActionsCompetitionsMappingTable actionlink = new zActionsCompetitionsMappingTable();
                    actionlink.FK_Competiton = newCompetition.ID;
                    actionlink.FK_ActionPR = Convert.ToInt32(current.Value);
                    actionlink.Active = current.Selected;
                    competitionDataBase.zActionsCompetitionsMappingTable.InsertOnSubmit(actionlink);
                    competitionDataBase.SubmitChanges();
                }
                Session["CompetitionID"] = newCompetition.ID;
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Готово!');", true);
            }
        
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuratorCompetition.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuratorFormCreate.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Curator
{
    public partial class CuratorTZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!Page.IsPostBack)

            {
                CompetitionDataContext curator = new CompetitionDataContext();
                  List<zActionPRManualTable> comp = (from a in curator.zActionPRManualTables where a.Active == true select a).ToList();              
                foreach (zActionPRManualTable n in comp)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.ActionPR;
                    TmpItem.Value = n.ID.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }

                var sessionParam = Session["CompetitionID"];
                if (sessionParam != null)
                {
                    int iD = (int)sessionParam;
                    if (iD > 0)
                    {
                        CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                        zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTables
                                                                 where a.Active == true && a.ID == iD
                                                                 select a).FirstOrDefault();
                        if (currentCompetition == null)
                        {                          
                            Response.Redirect("CuratorCompetition.aspx");
                        }
                        else
                        {
                            NameTextBox.Text = currentCompetition.Name;
                            DescriptionTextBox.Text = currentCompetition.Number;
                            BudjetTextBox.Text = currentCompetition.Budjet.ToString();

                            Calendar1.SelectedDate = Convert.ToDateTime(currentCompetition.StartDate);
                            Calendar2.SelectedDate = Convert.ToDateTime(currentCompetition.EndDate);
                        }
                    }             
                    
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка!');", true);
                }
            }
        }

        protected void CreateSaveButtonClick(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam = Session["CompetitionID"];
            var userId = Session["UserID"];
            if (sessionParam != null && userId != null)
            {
                int iD = (int)sessionParam;
                int user = (int)userId;
                if (iD > 0)
                {
                  zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTables
                                                                 where a.Active == true && a.ID == iD
                                                                 select a).FirstOrDefault();
                        if (currentCompetition == null)
                        {
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка!');", true); 
                        }
                        else
                        {
                            foreach (ListItem current in CheckBoxList1.Items)
                            {
                                zActionsCompetitionsMappingTable action = (from a in competitionDataBase.zActionsCompetitionsMappingTables
                                                                                     where a.FK_Competiton == iD
                                                                                     && a.FK_ActionPR == Convert.ToInt32(current.Value)
                                                                                     select a).FirstOrDefault();
                                if (action == null)
                                {
                                    action = new zActionsCompetitionsMappingTable();
                                    action.Active = current.Selected;
                                    action.FK_Competiton = iD;
                                    action.FK_ActionPR = Convert.ToInt32(current.Value);
                                    competitionDataBase.zActionsCompetitionsMappingTables.InsertOnSubmit(action);
                                    competitionDataBase.SubmitChanges();   
                                }
                                else
                                {
                                    action.Active = current.Selected;
                                    competitionDataBase.SubmitChanges(); 
                                }

                            }
                            currentCompetition.Name = NameTextBox.Text;
                            currentCompetition.Number = DescriptionTextBox.Text;
                            currentCompetition.Budjet = Convert.ToDouble(BudjetTextBox.Text);
                            currentCompetition.FK_Curator = user;
                            currentCompetition.StartDate = Calendar1.SelectedDate;
                            currentCompetition.EndDate = Calendar2.SelectedDate;                           
                            competitionDataBase.SubmitChanges();
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Готово!');", true); 
                        }
                    }
                }

                else
                {
                    zCompetitionsTable newCompetition = new zCompetitionsTable();
                    foreach (ListItem current in CheckBoxList1.Items)
                    {
                        zActionsCompetitionsMappingTable actionlink = new zActionsCompetitionsMappingTable();
                        actionlink.FK_Competiton = newCompetition.ID;
                        actionlink.FK_ActionPR = Convert.ToInt32(current.Value);
                        actionlink.Active = current.Selected;
                        competitionDataBase.zActionsCompetitionsMappingTables.InsertOnSubmit(actionlink);
                        competitionDataBase.SubmitChanges();
                    }                        
                        newCompetition.Name = NameTextBox.Text;
                        newCompetition.Number = DescriptionTextBox.Text;
                        newCompetition.Budjet = Convert.ToDouble(BudjetTextBox.Text);
                       // newCompetition.FK_Curator = user;
                        newCompetition.StartDate = Calendar1.SelectedDate;
                        newCompetition.EndDate = Calendar2.SelectedDate;
                        newCompetition.Active = true;
                        newCompetition.OpenForApplications = false;
                        competitionDataBase.zCompetitionsTables.InsertOnSubmit(newCompetition);
                        competitionDataBase.SubmitChanges();              
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Готово!');", true); 
                }
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
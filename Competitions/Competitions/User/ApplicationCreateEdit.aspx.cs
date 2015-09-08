using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.User
{
    public partial class ApplicationCreateEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sessionParam = Session["ApplicationID"];
                if (sessionParam == null)
                {
                    //error
                    Response.Redirect("ChooseApplication.aspx");
                }
                else
                {
                    CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                    int iD = (int)sessionParam;
                    if (iD > 0)
                    {
                       
                        zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTables
                            where a.Active == true
                                  && a.ID == iD
                            select a).FirstOrDefault();
                        if (currentApplication == null)
                        {
                            //error
                            Response.Redirect("ChooseApplication.aspx");
                        }
                        else
                        {
                            ApplicationNameTextBox.Text = currentApplication.Name;
                            string currenCompetitionName = (from a in competitionDataBase.zCompetitionsTables
                                where a.ID == currentApplication.FK_CompetitionTable
                                select a.Name).FirstOrDefault();
                            ChooseCompetitionDropDownList.Items.Add(currenCompetitionName);
                            ChooseCompetitionDropDownList.Enabled = false;
                        }
                    }
                    else
                    {
                        List<zCompetitionsTable> competitionList = (from a in competitionDataBase.zCompetitionsTables
                            where a.Active == true
                            && a.OpenForApplications == true
                            select a).ToList();
                        foreach (zCompetitionsTable currentCompetition in competitionList)
                        {
                            ListItem newListItem = new ListItem();
                            newListItem.Text = currentCompetition.Name;
                            newListItem.Value = currentCompetition.ID.ToString();
                            ChooseCompetitionDropDownList.Items.Add(newListItem);
                        }
                    }
                }
            }
            
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseApplication.aspx");
        }

        protected void CreateEditButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam = Session["ApplicationID"];
            var userIdParam = Session["UserID"];
            if (sessionParam == null)
            {
                //error
                Response.Redirect("ChooseApplication.aspx");
            }
            else
            {
                int iD = (int)sessionParam;
                int userId = (int)userIdParam;
                if (iD > 0)
                {
                    if (ApplicationNameTextBox.Text.Length > 0)
                    {
                        zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTables
                                                                 where a.Active == true
                                                                       && a.ID == iD
                                                                 select a).FirstOrDefault();
                        if (currentApplication == null)
                        {
                            //error
                            Response.Redirect("ChooseApplication.aspx");
                        }
                        else
                        {
                            currentApplication.Name = ApplicationNameTextBox.Text;
                            competitionDataBase.SubmitChanges();
                        }
                    }
                }
                else
                {
                    if (ApplicationNameTextBox.Text.Length > 0)
                    {
                        zApplicationTable newApplication = new zApplicationTable();
                        newApplication.Name = ApplicationNameTextBox.Text;
                        newApplication.FK_CompetitionTable = Convert.ToInt32(ChooseCompetitionDropDownList.SelectedValue);
                        newApplication.Active = true;
                        newApplication.FK_UsersTable = userId;
                        newApplication.CretaDateTime = DateTime.Now;
                        newApplication.Sended = false;
                        competitionDataBase.zApplicationTables.InsertOnSubmit(newApplication);
                        competitionDataBase.SubmitChanges();
                    }
                }
            }
            Response.Redirect("ChooseApplication.aspx");
        }
    }
}
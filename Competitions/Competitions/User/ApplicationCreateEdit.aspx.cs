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
                var konkursid = Session["ID"];

                if (sessionParam == null && konkursid == null)
                {
                    //error
                    Response.Redirect("ChooseApplication.aspx");
                }
                else
                {
                    CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                    int iD = (int)sessionParam;
                    int idkon = (int)konkursid;
                    if (iD > 0 && idkon > 0)
                    {                      
                        zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTables
                            where a.Active == true
                                  && a.ID == iD && a.FK_CompetitionTable == idkon
                            select a).FirstOrDefault();
                        if (currentApplication == null)
                        {
                            //error
                            Response.Redirect("ChooseApplication.aspx");
                        }
                        else
                        {
                            ApplicationNameTextBox.Text = currentApplication.Name;
                            string currenCompetitionName = (from a in competitionDataBase.zCompetitionsTable
                                where a.ID == currentApplication.FK_CompetitionTable
                                select a.Name).FirstOrDefault();                          
                        }
                    }
                    else
                    {
                        List<zCompetitionsTable> competitionList = (from a in competitionDataBase.zCompetitionsTable
                            where a.Active == true
                            && a.OpenForApplications == true
                            select a).ToList();
                        foreach (zCompetitionsTable currentCompetition in competitionList)
                        {
                            ListItem newListItem = new ListItem();
                            newListItem.Text = currentCompetition.Name;
                            newListItem.Value = currentCompetition.ID.ToString();
                            
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
                        var konkursid = Session["ID"];
                        int idkon = (int)konkursid;
                        zApplicationTable newApplication = new zApplicationTable();
                        newApplication.Name = ApplicationNameTextBox.Text;
                        newApplication.FK_CompetitionTable = idkon;
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
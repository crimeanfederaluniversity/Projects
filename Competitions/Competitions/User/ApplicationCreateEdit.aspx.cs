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
                var konkursid = Session["ID_Konkurs"];
                int iD = 0;
                if (sessionParam != null)
                {
                     iD = (int)sessionParam;
                }

                int idkon = 0;
                if (konkursid != null)
                {
                      idkon = (int)konkursid;
                }
               
                    CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                    
                    
                    if (iD > 0)
                    {                      
                        zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
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
                            string currenCompetitionName = (from a in competitionDataBase.zCompetitionsTables
                                where a.ID == currentApplication.FK_CompetitionTable
                                select a.Name).FirstOrDefault();                          
                        }
                    }
                    else
                    {
                        /*List<zCompetitionsTable> competitionList = (from a in competitionDataBase.zCompetitionsTable
                            where a.Active == true
                            && a.OpenForApplications == true
                            select a).ToList();
                        foreach (zCompetitionsTable currentCompetition in competitionList)
                        {
                            ListItem newListItem = new ListItem();
                            newListItem.Text = currentCompetition.Name;
                            newListItem.Value = currentCompetition.ID.ToString();
                            
                        }*/
                    
                }
            }
            
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserMainPage.aspx");
        }

        protected void CreateEditButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam = Session["ApplicationID"];
            var konkursid = Session["ID_Konkurs"];
            var userIdParam = Session["UserID"];

            int iD = 0;
            if (sessionParam != null)
            {
                iD = (int) sessionParam;
            }

            int idkon = 0;
            if (konkursid != null)
            {
                idkon = (int) konkursid;
            }


            int userId = (int) userIdParam;
            if (iD > 0)
            {
                if (ApplicationNameTextBox.Text.Length > 0)
                {
                    zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
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
                    newApplication.FK_CompetitionTable = idkon;
                    newApplication.Active = true;
                    newApplication.FK_UsersTable = userId;
                    newApplication.CretaDateTime = DateTime.Now;
                    newApplication.Sended = false;
                    competitionDataBase.zApplicationTable.InsertOnSubmit(newApplication);
                    competitionDataBase.SubmitChanges();
                }
            }

            Response.Redirect("ChooseApplication.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
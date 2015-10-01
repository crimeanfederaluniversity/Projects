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
                    iD = (int) sessionParam;
                }

                int idkon = 0;
                if (konkursid != null)
                {
                    idkon = (int) konkursid;
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
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        ApplicationNameTextBox.Text = currentApplication.Name;
                        string currenCompetitionName = (from a in competitionDataBase.zCompetitionsTables
                            where a.ID == currentApplication.FK_CompetitionTable
                            select a.Name).FirstOrDefault();
                    }
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
            zApplicationTable currentApplication;
            if (iD > 0)
            {
                if (ApplicationNameTextBox.Text.Length > 0)
                {
                    currentApplication = (from a in competitionDataBase.zApplicationTable
                        where a.Active == true
                              && a.ID == iD
                        select a).FirstOrDefault();
                    if (currentApplication == null)
                    {
                        //error
                        Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        currentApplication.Name = ApplicationNameTextBox.Text;
                        competitionDataBase.SubmitChanges();
                        Session["ApplicationID"] = currentApplication.ID;
                        Response.Redirect("ChooseApplicationAction.aspx");
                    }
                }
            }
            else
            {
                if (ApplicationNameTextBox.Text.Length > 0)
                {
                    currentApplication = new zApplicationTable();
                    currentApplication.Name = ApplicationNameTextBox.Text;
                    currentApplication.FK_CompetitionTable = idkon;
                    currentApplication.Active = true;
                    currentApplication.FK_UsersTable = userId;
                    currentApplication.CretaDateTime = DateTime.Now;
                    currentApplication.Sended = false;
                    currentApplication.Accept = false;
                    competitionDataBase.zApplicationTable.InsertOnSubmit(currentApplication);
                    competitionDataBase.SubmitChanges();
                    Session["ApplicationID"] = currentApplication.ID;
                    Response.Redirect("ChooseApplicationAction.aspx");
                }
            }
            Response.Redirect("~/Default.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Curator
{
    public partial class CuratorCompetition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            var userIdtmp = Session["UserID"];
            if (userIdtmp == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = (int) userIdtmp;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Budjet", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Sended", typeof(string)));

                List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                                                             where a.Active == true && a.FK_Curator == userId
                                                             select a).ToList();

                foreach (zCompetitionsTable currentCompetition in competitionsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentCompetition.ID;
                    dataRow["Name"] = currentCompetition.Name;
                    dataRow["Number"] = currentCompetition.Number;
                    dataRow["Budjet"] = Convert.ToInt32(currentCompetition.Budjet);
                    dataRow["StartDate"] = currentCompetition.StartDate.ToString().Split(' ')[0];
                    dataRow["EndDate"] = currentCompetition.EndDate.ToString().Split(' ')[0];
                    if (currentCompetition.Sended == false)
                    {
                        dataRow["Sended"] = "На рассмотрении";
                    }
                    else
                    {
                        dataRow["Sended"] = "Открыт для подачи заявок";
                    }
                   
                    dataTable.Rows.Add(dataRow);
                }
                CompetitionsGV.DataSource = dataTable;
                CompetitionsGV.DataBind();
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Button send = (Button)e.Row.FindControl("OpenButton");
            Button change = (Button)e.Row.FindControl("ChangeButton");

            if (send != null && change!= null)
            {
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zCompetitionsTable sended = (from a in CompetitionsDataBase.zCompetitionsTable
                                                   where a.Active == true
                                                   && a.ID == Convert.ToInt32(send.CommandArgument)                                                  
                                                   select a).FirstOrDefault();
                if (sended.Sended == true)
                {
                    send.Enabled = false;
                    change.Enabled = false;
                }
                else
                {
                    send.Enabled = true;
                    change.Enabled = true;
                }
            }
        }
        protected void NewCompetitionButton_Click(object sender, EventArgs e)
        {
             
            Response.Redirect("NewCuratorTZ.aspx");

        }
        protected void ChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["CompetitionID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("CuratorTZ.aspx");
            }
        }
        protected void OpenButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCompetitionsTable currentApplication = (from a in competitionDataBase.zCompetitionsTable
                                                        where a.Active == true
                                                              && a.ID == iD
                                                        select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    currentApplication.Sended = true;
                    //currentApplication.SendedDataTime = DateTime.Now;
                    competitionDataBase.SubmitChanges();
                }
            }
            Response.Redirect("CuratorCompetition.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuratorMainPage.aspx");
        }
    }
}
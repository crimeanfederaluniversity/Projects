﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class Applications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
             CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zApplicationTable> applicationsList = (from a in CompetitionsDataBase.zApplicationTables
                    where a.Active == true
                    select a).ToList();
                DataTable dataTable = new  DataTable();

                dataTable.Columns.Add("ID", typeof (string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Description", typeof(string));
                dataTable.Columns.Add("Autor", typeof(string));
                dataTable.Columns.Add("Competition", typeof(string));
                dataTable.Columns.Add("Experts", typeof(string));

                
                foreach (zApplicationTable currentApplication in applicationsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["Description"] = "";
                    dataRow["Competition"] = (from a in CompetitionsDataBase.zCompetitionsTable
                                                  where a.ID == currentApplication.FK_CompetitionTable
                                                  select  a.Name).FirstOrDefault();
                    dataRow["Autor"] = (from a in CompetitionsDataBase.UsersTable 
                                            where a.ID == currentApplication.FK_UsersTable
                                            select a.Email).FirstOrDefault();
                    List<UsersTable> expertsList = (from a in CompetitionsDataBase.UsersTable
                        join b in CompetitionsDataBase.zExpertsAndApplicationMappingTables
                            on a.ID equals b.FK_UsersTable
                        where a.AccessLevel == 5
                              && a.Active == true
                              && b.FK_ApplicationsTable == currentApplication.ID
                              && b.Active == true
                        select a).ToList();
                    string expertNamesList = "";
                    
                    foreach (UsersTable currentExpert in expertsList)
                    {
                        expertNamesList += currentExpert.Email + " \n";

                    }
                    dataRow["Experts"] = expertNamesList;
                    dataTable.Rows.Add(dataRow);
                }
                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }

        protected void ExpertChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            if (button != null)
            {
                Session["ApplicationID"] = button.CommandArgument;
                Response.Redirect("ApllicationExpertEdit.aspx");
            }
        }
    }
}
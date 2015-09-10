﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class SectionCreateEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sessionParam1 = Session["CompetitionID"];
                var sessionParam2 = Session["SectionID"];
                if ((sessionParam1 == null) || (sessionParam2 ==null))
                {
                    //error
                    Response.Redirect("ChooseSection.aspx");
                }
                else
                {
                    CompetitionDataContext newCompetition = new CompetitionDataContext();
                     
                    List<zBlockTable> blocks =
                        (from a in newCompetition.zBlockTable
                         where  a.Active == true
                         select a).ToList();

                    ListItem tmpItem2 = new ListItem();
                    tmpItem2.Text = "Выберите блок к которому относится форма";
                    tmpItem2.Value = "0";
                    DropDownList1.Items.Add(tmpItem2);

                    foreach (zBlockTable current in blocks)
                    {
                        ListItem tmpItem = new ListItem();
                        tmpItem.Text = current.BlockName;
                        tmpItem.Value = current.ID.ToString();
                        DropDownList1.Items.Add(tmpItem);
                    }

                    int competitionId = (int)sessionParam1;
                    int sectionId = (int)sessionParam2;
                    if (sectionId>0)
                    {
                        CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                        zSectionTable currentSection = (from a in competitionDataBase.zSectionTable
                                                                 where a.Active == true
                                                                       && a.ID == sectionId
                                                                       && a.FK_CompetitionsTable == competitionId
                                                                 select a).FirstOrDefault();
                        if (currentSection == null)
                        {
                            //error
                            Response.Redirect("ChooseSection.aspx");
                        }
                        else
                        {
                            NameTextBox.Text = currentSection.Name;
                            DescriptionTextBox.Text = currentSection.Description;
                            DropDownList1.SelectedIndex = Convert.ToInt32(currentSection.FK_BlockID);
                            MaxRowCountTextBox.Text = currentSection.ColumnMaxCount.ToString();
                        }
                    }
                }
            }
        }
        protected void CreateSaveButtonClick(object sender, EventArgs e)
        {
        CompetitionDataContext competitionDataBase = new CompetitionDataContext();
        var sessionParam1 = Session["CompetitionID"];
        var sessionParam2 = Session["SectionID"];
        if ((sessionParam1 == null) || (sessionParam2 == null))
        {
            //error
            Response.Redirect("ChooseSection.aspx");
        }
        else
            {
                int competitionId = (int)sessionParam1;
                int sectionId = (int)sessionParam2;
                if (sectionId > 0)
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zSectionTable currentSection = (from a in competitionDataBase.zSectionTable
                                                         where a.Active == true
                                                               && a.ID == sectionId
                                                               && a.FK_CompetitionsTable == competitionId
                                                         select a).FirstOrDefault();
                        if (currentSection == null)
                        {
                            //error
                            Response.Redirect("ChooseSection.aspx");
                        }
                        else
                        {
                            currentSection.Name = NameTextBox.Text;
                            currentSection.Description = DescriptionTextBox.Text;
                            currentSection.FK_BlockID = Convert.ToInt32(DropDownList1.SelectedIndex);
                            currentSection.ColumnMaxCount = Convert.ToInt32(MaxRowCountTextBox.Text);

                            competitionDataBase.SubmitChanges();
                        }
                    }
                }
                else
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zSectionTable newSection = new zSectionTable();
                        newSection.Name = NameTextBox.Text;
                        newSection.Description = DescriptionTextBox.Text;
                        newSection.FK_BlockID = Convert.ToInt32(DropDownList1.SelectedIndex);
                        newSection.ColumnMaxCount = Convert.ToInt32(MaxRowCountTextBox.Text);
                        newSection.Active = true;
                        newSection.FK_CompetitionsTable = competitionId;
                        competitionDataBase.zSectionTable.InsertOnSubmit(newSection);
                        competitionDataBase.SubmitChanges();
                    }
                }
            }
            Response.Redirect("ChooseSection.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseSection.aspx");
        }
    }
}
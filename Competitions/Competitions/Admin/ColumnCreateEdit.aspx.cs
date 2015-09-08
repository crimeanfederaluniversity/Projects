using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ColumnCreateEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {         
            if (!Page.IsPostBack)
            {
                DataType dataType = new DataType();
                DataTypeDropDownList.Items.AddRange(dataType.GetDataTypeListItemCollection());

                var sessionParam1 = Session["CompetitionID"];
                var sessionParam2 = Session["SectionID"];
                var sessionParam3 = Session["ColumnID"];
                if ((sessionParam1 == null) || (sessionParam2 == null) || (sessionParam3 == null))
                {
                    //error
                    Response.Redirect("ChooseSection.aspx");
                }
                else
                {
                    int competitionId = (int)sessionParam1;
                    int sectionId = (int)sessionParam2;
                    int columnId = (int)sessionParam3;

                    CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                    List <zColumnTable> columnList = (from a in competitionDataBase.zColumnTable
                        where a.Active
                        join b in competitionDataBase.zSectionTable
                            on a.FK_SectionTable equals b.ID
                        where b.Active == true
                              && b.ID != sectionId
                              && b.FK_CompetitionsTable == competitionId
                        select a).Distinct().ToList();
                    foreach (zColumnTable currentColumn in columnList)
                    {
                        ListItem newItem = new  ListItem();
                        newItem.Text = currentColumn.Name;
                        newItem.Value = currentColumn.ID.ToString();
                        FkToColumnDropDown.Items.Add(newItem);
                    }

                    List<zConstantListTable> constantList = (from a in competitionDataBase.zConstantListTable
                        where a.Active
                              && a.FK_CompetitionTable == competitionId
                        select a).ToList();

                    foreach (zConstantListTable currentConstantList in constantList)
                    {
                        ListItem newItem = new ListItem();
                        newItem.Text = currentConstantList.Name;
                        newItem.Value = currentConstantList.ID.ToString();
                        FkToConstantDropDown.Items.Add(newItem);
                    }
                                     


                    if (columnId > 0)
                    {
                        zColumnTable currentColum = (from a in competitionDataBase.zColumnTable
                                                        where a.Active == true
                                                              && a.ID == columnId
                                                              && a.FK_SectionTable == sectionId
                                                        select a).FirstOrDefault();
                        if (currentColum == null)
                        {
                            //error
                            Response.Redirect("ChooseColumn.aspx");
                        }
                        else
                        {
                            NameTextBox.Text = currentColum.Name;
                            DescriptionTextBox.Text = currentColum.Description;
                            DataTypeDropDownList.SelectedIndex = currentColum.DataType;
                            if (currentColum.FK_ColumnTable != null)
                            {
                                FkToColumnDropDown.Items.FindByValue(currentColum.FK_ColumnTable.ToString()).Selected=true;
                                ChooseColumnForDropDownDiv.Visible = true;
                            }
                            if (currentColum.FK_ConstantListsTable != null)
                            {
                                FkToConstantDropDown.Items.FindByValue(currentColum.FK_ConstantListsTable.ToString())
                                    .Selected = true;
                                ChooseConstantForDropDownDiv.Visible = true;
                            }
                        }
                    }
                }
            }
        }
        protected void DataTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataType dataType = new DataType();
            int chosenValue = Convert.ToInt32(DataTypeDropDownList.SelectedValue);

            if (dataType.IsDataTypeDropDown(chosenValue) || dataType.IsDataTypeSum(chosenValue) || dataType.IsDataTypeNecessarilyShow(chosenValue))
            {
                ChooseColumnForDropDownDiv.Visible = true;
            }
            else
            {
                ChooseColumnForDropDownDiv.Visible = false;
            }

            if (dataType.IsDataTypeConstantDropDown(chosenValue) ||
                dataType.IsDataTypeConstantNecessarilyShow(chosenValue))
            {
                ChooseConstantForDropDownDiv.Visible =true;
            }
            else
            {
                ChooseConstantForDropDownDiv.Visible = false;
            }           
        }
        protected void CreateSaveButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam1 = Session["CompetitionID"];
            var sessionParam2 = Session["SectionID"];
            var sessionParam3 = Session["ColumnID"];
            if ((sessionParam1 == null) || (sessionParam2 == null) || (sessionParam3 == null))
            {
                //error
                Response.Redirect("ChooseSection.aspx");
            }
            else
            {
                int dataTypeSelectedValue = Convert.ToInt32(Convert.ToInt32(DataTypeDropDownList.SelectedValue));
                DataType dataType = new DataType();
                int competitionId = (int)sessionParam1;
                int sectionId = (int)sessionParam2;
                int columnId = (int) sessionParam3;
                if (columnId > 0)
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zColumnTable currentColumn = (from a in competitionDataBase.zColumnTable
                                                         where a.Active == true
                                                               && a.ID == columnId
                                                               && a.FK_SectionTable == sectionId
                                                         select a).FirstOrDefault();
                        if (currentColumn == null)
                        {
                            //error
                            Response.Redirect("ChooseColumn.aspx");
                        }
                        else
                        {
                            currentColumn.Name = NameTextBox.Text;
                            currentColumn.Description = DescriptionTextBox.Text;
                            currentColumn.DataType =
                                Convert.ToInt32(DataTypeDropDownList.SelectedValue);
                            if (dataType.IsDataTypeDropDown(dataTypeSelectedValue) || dataType.IsDataTypeSum(dataTypeSelectedValue) || dataType.IsDataTypeNecessarilyShow(dataTypeSelectedValue))
                            {
                                currentColumn.FK_ColumnTable =Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                            }
                            if (dataType.IsDataTypeConstantDropDown(dataTypeSelectedValue) || dataType.IsDataTypeConstantNecessarilyShow(dataTypeSelectedValue))
                            {
                                currentColumn.FK_ConstantListsTable = Convert.ToInt32(FkToConstantDropDown.SelectedValue);
                            }

                            competitionDataBase.SubmitChanges();
                        }
                    }
                }
                else
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        
                        zColumnTable newColumn = new zColumnTable();
                        newColumn.Name = NameTextBox.Text;
                        newColumn.Description = DescriptionTextBox.Text;
                        newColumn.Active = true;
                        newColumn.FK_SectionTable = sectionId;
                        newColumn.DataType = dataTypeSelectedValue;

                        if (dataType.IsDataTypeDropDown(dataTypeSelectedValue) || dataType.IsDataTypeSum(dataTypeSelectedValue) || dataType.IsDataTypeNecessarilyShow(dataTypeSelectedValue))
                        {
                            newColumn.FK_ColumnTable = Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                        }

                        if (dataType.IsDataTypeConstantDropDown(dataTypeSelectedValue) || dataType.IsDataTypeConstantNecessarilyShow(dataTypeSelectedValue))
                        {
                            newColumn.FK_ConstantListsTable = Convert.ToInt32(FkToConstantDropDown.SelectedValue);
                        }
                        competitionDataBase.zColumnTable.InsertOnSubmit(newColumn);
                        competitionDataBase.SubmitChanges();
                    }
                }
            }
            Response.Redirect("ChooseColumn.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseColumn.aspx");
        }    
    }
}
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
        private ListItem[] GetColumnsInThisSection(int sectionId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zColumnTable> columnList = (from a in competitionDataBase.zColumnTables
                                             where a.Active
                                             join b in competitionDataBase.zSectionTable
                                                 on a.FK_SectionTable equals b.ID
                                             where b.Active == true
                                                   && b.ID == sectionId
                                             select a).Distinct().ToList();

            ListItem[] newListItemAray = new ListItem[columnList.Count];
            for (int i = 0; i < columnList.Count; i++)
            {
                ListItem newItem = new ListItem();
                newItem.Text = columnList[i].Name;
                newItem.Value = columnList[i].ID.ToString();            
                newListItemAray[i] = newItem;
            }
            return newListItemAray;
        }
        private ListItem[] GetColumnsInThisApplicationWithoutThisSection(int sectionId, int competitionId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zColumnTable> columnList = (from a in competitionDataBase.zColumnTables
                                             where a.Active
                                             join b in competitionDataBase.zSectionTable
                                                 on a.FK_SectionTable equals b.ID
                                             where b.Active == true
                                                   && b.ID != sectionId
                                                   && b.FK_CompetitionsTable == competitionId
                                             select a).Distinct().ToList();

            ListItem[] newListItemAray = new ListItem[columnList.Count];
            for (int i = 0; i < columnList.Count; i++)
            {
                ListItem newItem = new ListItem();
                newItem.Text = columnList[i].Name;
                newItem.Value = columnList[i].ID.ToString();
                newListItemAray[i] = newItem;
            }
            return newListItemAray;
        }
        private ListItem[] GetConstantList(int competitionId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zConstantListTable> constantList = (from a in competitionDataBase.zConstantListTable
                                                     where a.Active
                                                           && a.FK_CompetitionTable == competitionId
                                                     select a).ToList();

            ListItem[] newListItemAray = new ListItem[constantList.Count];
            for (int i = 0; i < constantList.Count; i++)
            {
                ListItem newItem = new ListItem();
                newItem.Text = constantList[i].Name;
                newItem.Value = constantList[i].ID.ToString();
                newListItemAray[i] = newItem;
            }
            return newListItemAray;
        }
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
                    FkToColumnDropDown.Items.AddRange(GetColumnsInThisApplicationWithoutThisSection(sectionId,competitionId));
                    FkToConstantDropDown.Items.AddRange(GetConstantList(competitionId));
                    Fk_ColumnConnectFromDropDown.Items.AddRange(GetColumnsInThisSection(sectionId));
                    Fk_ColumnConnectToDropDown.Items.AddRange(GetColumnsInThisApplicationWithoutThisSection(sectionId, competitionId));
                    if (columnId > 0)
                    {
                        zColumnTable currentColum = (from a in competitionDataBase.zColumnTables
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
                            UniqueMarkTextBox.Text = currentColum.UniqueMark;
                            if (currentColum.TotalUp!=null)
                            TotalUpCheckBox.Checked = (bool)currentColum.TotalUp;
                            DataTypeDropDownList.SelectedIndex = currentColum.DataType;
                            if (dataType.DataTypeWithConnectionToCollected(currentColum.DataType))
                            {
                                FkToColumnDropDown.Items.FindByValue(currentColum.FK_ColumnTable.ToString()).Selected=true;
                                ChooseColumnForDropDownDiv.Visible = true;
                                
                            }
                            if (dataType.DataTypeWithConnectionToConstant(currentColum.DataType))
                            {
                                FkToConstantDropDown.Items.FindByValue(currentColum.FK_ConstantListsTable.ToString())
                                    .Selected = true;
                                ChooseConstantForDropDownDiv.Visible = true;
                                TotalUpCheckBox.Visible = false;
                            }
                            if (dataType.DataTypeWithConnectionToColumnsWithParams(currentColum.DataType))
                            {
                                Fk_ColumnConnectFromDropDown.Items.FindByValue(currentColum.FK_ColumnConnectFromTable.ToString()).Selected = true;
                                Fk_ColumnConnectToDropDown.Items.FindByValue(currentColum.FK_ColumnConnectToTable.ToString()).Selected = true;
                                Panel1.Visible = true;
                                FkToColumnDropDown.Items.FindByValue(currentColum.FK_ColumnTable.ToString()).Selected = true;
                                ChooseColumnForDropDownDiv.Visible = true;
                                
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

            ChooseColumnForDropDownDiv.Visible = dataType.DataTypeWithConnectionToCollected(chosenValue);
            ChooseConstantForDropDownDiv.Visible = dataType.DataTypeWithConnectionToConstant(chosenValue);
            Panel1.Visible = dataType.DataTypeWithConnectionToColumnsWithParams(chosenValue);
            TotalUpCheckBox.Visible = !dataType.DataTypeWithConnectionToConstant(chosenValue);
            if (Panel1.Visible)
            {               
                ChooseColumnForDropDownDiv.Visible = true;
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
                        zColumnTable currentColumn = (from a in competitionDataBase.zColumnTables
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
                            currentColumn.TotalUp = TotalUpCheckBox.Checked;
                            currentColumn.UniqueMark = UniqueMarkTextBox.Text;
                            if (dataType.DataTypeWithConnectionToCollected(dataTypeSelectedValue))
                            {
                                currentColumn.FK_ColumnTable = Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                            }
                            if (dataType.DataTypeWithConnectionToConstant(dataTypeSelectedValue))
                            {
                                currentColumn.FK_ConstantListsTable = Convert.ToInt32(FkToConstantDropDown.SelectedValue);
                            }
                            if (dataType.DataTypeWithConnectionToColumnsWithParams(dataTypeSelectedValue))
                            {
                                currentColumn.FK_ColumnTable = Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                                currentColumn.FK_ColumnConnectFromTable = Convert.ToInt32(Fk_ColumnConnectFromDropDown.SelectedValue);
                                currentColumn.FK_ColumnConnectToTable = Convert.ToInt32(Fk_ColumnConnectToDropDown.SelectedValue);
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
                        newColumn.TotalUp = TotalUpCheckBox.Checked;
                        newColumn.UniqueMark = UniqueMarkTextBox.Text;
                        if (dataType.DataTypeWithConnectionToCollected(dataTypeSelectedValue))
                        {
                            newColumn.FK_ColumnTable = Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                        }
                        if (dataType.DataTypeWithConnectionToConstant(dataTypeSelectedValue))
                        {
                            newColumn.FK_ConstantListsTable = Convert.ToInt32(FkToConstantDropDown.SelectedValue);
                        }
                        if (dataType.DataTypeWithConnectionToColumnsWithParams(dataTypeSelectedValue))
                        {
                            newColumn.FK_ColumnTable = Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                            newColumn.FK_ColumnConnectFromTable = Convert.ToInt32(Fk_ColumnConnectFromDropDown.SelectedValue);
                            newColumn.FK_ColumnConnectToTable = Convert.ToInt32(Fk_ColumnConnectToDropDown.SelectedValue);
                        }
                        competitionDataBase.zColumnTables.InsertOnSubmit(newColumn);
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
        protected void FkToConstantDropDown_SelectedIndexChanged(object sender, EventArgs e) /// доделай чтобы во втором дроп дауне лишнего не было
        {
            DataType dataType = new DataType();

            if (dataType.IsDataTypeSymWithParam(Convert.ToInt32(DataTypeDropDownList.SelectedValue)))
            {
                
            }
        }    
    }
}
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
            List<zColumnTable> columnList = (from a in competitionDataBase.zColumnTable
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
            List<zColumnTable> columnList = (from a in competitionDataBase.zColumnTable
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
        private ListItem[] GetColumnsInThisApplicationWithBitDataType(int sectionId, int competitionId)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            List<zColumnTable> columnList = (from a in competitionDataBase.zColumnTable
                                             where a.Active
                                             join b in competitionDataBase.zSectionTable
                                                 on a.FK_SectionTable equals b.ID
                                             where b.Active == true
                                                   && b.ID != sectionId
                                                   && b.FK_CompetitionsTable == competitionId
                                             select a).Distinct().ToList();
            List<zColumnTable> onlyBitColumnList = new List<zColumnTable>();
            DataType dataType = new DataType();
            foreach (zColumnTable currentColumn in columnList)
            {
                if (dataType.IsDataTypeBit(currentColumn.DataType))
                {
                    onlyBitColumnList.Add(currentColumn);
                }
            }

            ListItem[] newListItemAray = new ListItem[onlyBitColumnList.Count];

            for (int i = 0; i < onlyBitColumnList.Count; i++)
            {             
                    ListItem newItem = new ListItem();
                    newItem.Text = onlyBitColumnList[i].Name;
                    newItem.Value = onlyBitColumnList[i].ID.ToString();
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
                    BitColumnsDropDown.Items.AddRange(GetColumnsInThisApplicationWithBitDataType(sectionId,competitionId));
                    LeftValueDropDown.Items.AddRange(GetColumnsInThisSection(sectionId));
                    RightValueDropDown.Items.AddRange(GetColumnsInThisSection(sectionId));
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
                            UniqueMarkTextBox.Text = currentColum.UniqueMark;
                            if (currentColum.SortBy != null)
                            SortByCheckBox.Checked = (bool) currentColum.SortBy;
                            if (currentColum.TotalUp!=null)
                            TotalUpCheckBox.Checked = (bool)currentColum.TotalUp;
                            if (currentColum.Visible != null)
                            VisibleCheckBox.Checked = (bool) currentColum.Visible;
                            DataTypeDropDownList.SelectedIndex = currentColum.DataType;
                            if (dataType.DataTypeWithConnectionToCollected(currentColum.DataType))
                            {
                                FkToColumnDropDown.Items.FindByValue(currentColum.FK_ColumnTable.ToString()).Selected=true;
                                //ChooseColumnForDropDownDiv.Visible = true;
                                
                            }
                            if (dataType.DataTypeWithConnectionToConstant(currentColum.DataType))
                            {
                                FkToConstantDropDown.Items.FindByValue(currentColum.FK_ConstantListsTable.ToString())
                                    .Selected = true;
                                //ChooseConstantForDropDownDiv.Visible = true;
                                //TotalUpCheckBox.Visible = false;
                            }
                            if (dataType.DataTypeWithConnectionToColumnsWithParams(currentColum.DataType))
                            {
                                Fk_ColumnConnectFromDropDown.Items.FindByValue(currentColum.FK_ColumnConnectFromTable.ToString()).Selected = true;
                                Fk_ColumnConnectToDropDown.Items.FindByValue(currentColum.FK_ColumnConnectToTable.ToString()).Selected = true;
                                //Panel1.Visible = true;
                                FkToColumnDropDown.Items.FindByValue(currentColum.FK_ColumnTable.ToString()).Selected = true;
                                //ChooseColumnForDropDownDiv.Visible = true;
                                
                            }
                            if (dataType.IsCellCalculateInRow(currentColum.DataType))
                            {
                                LeftValueDropDown.Items.FindByValue(currentColum.FK_ColumnConnectFromTable.ToString()).Selected = true;
                                RightValueDropDown.Items.FindByValue(currentColum.FK_ColumnConnectToTable.ToString()).Selected = true;
                            }
                            if (dataType.DataTypeDependOfBit(currentColum.DataType))
                            {
                                //FkToColumnDropDown.Items.FindByValue(currentColum.FK_ColumnTable.ToString()).Selected = true;
                                BitColumnsDropDown.Items.FindByValue(currentColum.FK_ColumnConnectToTable.ToString()).Selected = true;
                                //ChooseColumnForDropDownDiv.Visible = true;
                                //ChooseBitForDepend.Visible = true;
                            }
                            ShowOnlyAppropriateDivs();
                        }
                    }
                }
            }
        }
        protected void DataTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOnlyAppropriateDivs();
        }
        protected void ShowOnlyAppropriateDivs()
        {
            ChooseColumnForDropDownDiv.Visible = false;
            ChooseConstantForDropDownDiv.Visible = false;
            ChooseBitForDepend.Visible = false;
            Panel1.Visible = false;
            TotalUpCheckBox.Visible = false;
            
            DataType dataType = new DataType();
            int chosenValue = Convert.ToInt32(DataTypeDropDownList.SelectedValue);
            if (dataType.DataTypeWithConnectionToCollected(chosenValue))
            {
                ChooseColumnForDropDownDiv.Visible = true;
                TotalUpCheckBox.Visible = true;
            }
            if (dataType.DataTypeWithConnectionToConstant(chosenValue))
            {
                ChooseConstantForDropDownDiv.Visible = true;
            }
            if (dataType.DataTypeWithConnectionToColumnsWithParams(chosenValue))
            {
                ChooseColumnForDropDownDiv.Visible = true;
                Panel1.Visible = true;
                TotalUpCheckBox.Visible = true;
            }
            if (dataType.DataTypeDependOfBit(chosenValue))
            {
                ChooseBitForDepend.Visible = true;
            }
            if (dataType.IsCellCalculateInRow(chosenValue))
            {
                MathInRowPanel.Visible = true;
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

                zColumnTable currentColumn = null;
                bool isNewRowKInTable = false;
                if (columnId > 0)
                {
                    isNewRowKInTable = false;
                }
                else
                {
                    isNewRowKInTable = true;
                }

                if (isNewRowKInTable)
                {
                    currentColumn = new zColumnTable();
                }
                else
                {
                    currentColumn = (from a in competitionDataBase.zColumnTable
                                                         where a.Active == true
                                                               && a.ID == columnId
                                                               && a.FK_SectionTable == sectionId
                                                         select a).FirstOrDefault();
                }

                if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {                   
                        if (currentColumn == null)
                        {
                            //error
                            Response.Redirect("ChooseColumn.aspx");
                        }
                        else
                        {
                            currentColumn.Name = NameTextBox.Text;
                            currentColumn.Description = DescriptionTextBox.Text;
                            currentColumn.Active = true;
                            currentColumn.FK_SectionTable = sectionId;
                            currentColumn.DataType = dataTypeSelectedValue;
                            currentColumn.TotalUp = TotalUpCheckBox.Checked;
                            currentColumn.SortBy = SortByCheckBox.Checked;
                            currentColumn.UniqueMark = UniqueMarkTextBox.Text;
                            currentColumn.Visible = VisibleCheckBox.Checked;
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
                            if (dataType.DataTypeDependOfBit(dataTypeSelectedValue))
                            {
                                //currentColumn.FK_ColumnTable = Convert.ToInt32(FkToColumnDropDown.SelectedValue);
                                currentColumn.FK_ColumnConnectToTable = Convert.ToInt32(BitColumnsDropDown.SelectedValue);
                            }
                            if (isNewRowKInTable)
                            {
                                competitionDataBase.zColumnTable.InsertOnSubmit(currentColumn);
                            }

                            if (dataType.IsCellCalculateInRow(dataTypeSelectedValue))
                            {
                                currentColumn.FK_ColumnConnectFromTable = Convert.ToInt32(LeftValueDropDown.SelectedValue);
                                currentColumn.FK_ColumnConnectToTable = Convert.ToInt32(RightValueDropDown.SelectedValue);
                            }

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
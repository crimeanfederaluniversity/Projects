using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ConstantListCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sessionParam1 = Session["CompetitionID"];
            var sessionParam2 = Session["ConstantListID"];
            if ((sessionParam1 == null)||(sessionParam2 == null))
            {   //error
                Response.Redirect("ChooseConstantList.aspx");
            }
            int competitionId = (int)sessionParam1;
            int constantListId = (int)sessionParam2;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                zConstantListTable currenConstantList = (from a in competitionDataBase.zConstantListTable
                    where a.ID == constantListId
                    select a).FirstOrDefault();

                ConstantListNameTextBox.Text = currenConstantList.Name;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ConstValue", typeof(string)));

                List<zCollectedDataTable> constantCollectedDate = (from a in competitionDataBase.zCollectedDataTable
                    where a.Active == true
                    join b in competitionDataBase.zConstantListTable
                    on a.FK_ConstantListTable equals b.ID
                    where b.Active == true
                    && b.ID == constantListId
                    && b.FK_CompetitionTable == competitionId
                    select a).ToList();


                foreach (zCollectedDataTable currentCollectedData in constantCollectedDate)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentCollectedData.ID;
                    dataRow["ConstValue"] = currentCollectedData.ValueText;
                    dataTable.Rows.Add(dataRow);
                }

                ConstantListValuesGV.DataSource = dataTable;
                ConstantListValuesGV.DataBind();
            }
        }

        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCollectedDataTable collectedDataToDelete = (from a in competitionDataBase.zCollectedDataTable
                                                           where a.ID == iD
                                                           select a).FirstOrDefault();
                if (collectedDataToDelete != null)
                {
                    collectedDataToDelete.Active = false;
                    competitionDataBase.SubmitChanges();
                }
                else
                {
                    //error
                }

            }
            Response.Redirect("ConstantListCreate.aspx");
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseConstantList.aspx");
        }

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            var sessionParam1 = Session["CompetitionID"];
            var sessionParam2 = Session["ConstantListID"];
            if ((sessionParam1 == null)||(sessionParam2 == null))
            {   //error
                Response.Redirect("ChooseConstantList.aspx");
            }
            int competitionId =  (int)sessionParam1;
            int constantListId = (int)sessionParam2;
          
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zCollectedDataTable newCollectedConstant = new  zCollectedDataTable();
            newCollectedConstant.FK_ConstantListTable = constantListId;
            newCollectedConstant.Active = true;
            newCollectedConstant.CreateDateTime = DateTime.Now;
            newCollectedConstant.LastChangeDateTime = DateTime.Now;

            competitionDataBase.zCollectedDataTable.InsertOnSubmit(newCollectedConstant);
            competitionDataBase.SubmitChanges();
            Response.Redirect("ConstantListCreate.aspx");
        
        }

        protected void SaveAllButton_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam1 = Session["CompetitionID"];
            var sessionParam2 = Session["ConstantListID"];
            if ((sessionParam1 == null) || (sessionParam2 == null))
            {   //error
                Response.Redirect("ChooseConstantList.aspx");
            }
            int competitionId = (int)sessionParam1;
            int constantListId = (int)sessionParam2;

            DataType dataType = new DataType();
           
            foreach (GridViewRow currentRow in ConstantListValuesGV.Rows)
            {
                Label idLabel = (Label) currentRow.FindControl("ID");
                if (idLabel != null)
                {
                    zCollectedDataTable currentCollectedData = (from a in competitionDataBase.zCollectedDataTable
                        where a.ID == Convert.ToInt32(idLabel.Text)
                        select a).FirstOrDefault();

                    if (currentCollectedData != null)
                    {
                        TextBox currentTextBox = (TextBox) currentRow.FindControl("ConstValueTextBox");
                        currentCollectedData.ValueText = currentTextBox.Text;
                        competitionDataBase.SubmitChanges();                      
                    }
                }
            }
            zConstantListTable currentConstantList = (from a in competitionDataBase.zConstantListTable
                                                      where a.ID == constantListId
                select a).FirstOrDefault();
            currentConstantList.Name = ConstantListNameTextBox.Text;
            competitionDataBase.SubmitChanges();
        }
    }
}
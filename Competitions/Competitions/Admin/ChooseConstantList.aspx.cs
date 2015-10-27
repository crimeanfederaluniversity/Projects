using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ChooseConstantList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sessionParam1 = Session["CompetitionID"];
            if (sessionParam1 == null)
            {
                //error
                Response.Redirect("ChooseCompetition.aspx");
            }

            int competitionId = (int)sessionParam1;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
              
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                List<zConstantListTable> constantList = (from a in competitionDataBase.zConstantListTable
                                                 where a.Active == true
                                                 && a.FK_CompetitionTable == competitionId
                                                 select a).ToList();
                DataType newDataType = new DataType();
                foreach (zConstantListTable currentConstantList in constantList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentConstantList.ID;
                    dataRow["Name"] = currentConstantList.Name;
                    dataTable.Rows.Add(dataRow);
                }

                ConstantListsGV.DataSource = dataTable;
                ConstantListsGV.DataBind();
            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseSection.aspx");
        }
        protected void CreateNewConstanListButton_Click(object sender, EventArgs e)
        {
            var sessionParam1 = Session["CompetitionID"];
            if (sessionParam1 == null)
            {   //error
                Response.Redirect("ChooseConstantList.aspx");
            }
            int competitionId = (int)sessionParam1;

            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zConstantListTable newConstantList = new zConstantListTable();
            newConstantList.FK_CompetitionTable = competitionId;
            newConstantList.Active = true;
            newConstantList.Name = "";
            competitionDataBase.zConstantListTable.InsertOnSubmit(newConstantList);
            competitionDataBase.SubmitChanges();

            Session["ConstantListID"] = newConstantList.ID;
            Response.Redirect("ConstantListCreate.aspx");
        }
        protected void ChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["ConstantListID"] = iD;
                Response.Redirect("ConstantListCreate.aspx");
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zConstantListTable constantListToDelete = (from a in competitionDataBase.zConstantListTable
                                               where a.ID == iD
                                               select a).FirstOrDefault();
               List <zCollectedDataTable> constatvalue = (from a in competitionDataBase.zCollectedDataTable
                    where a.Active && a.FK_ConstantListTable == constantListToDelete.ID
                    select a).ToList();
                if (constantListToDelete != null)
                {
                    constantListToDelete.Active = false;
                    competitionDataBase.SubmitChanges();
                    foreach (zCollectedDataTable n in constatvalue)
                    {
                        n.Active = false;
                        competitionDataBase.SubmitChanges();
                    }
                    
                }
                else
                {
                    //error
                }

            }
            Response.Redirect("ChooseConstantList.aspx");
        }


    }
}
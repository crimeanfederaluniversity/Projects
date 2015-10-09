using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ChooseColumn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sessionParam1 = Session["CompetitionID"];
            var sessionParam2 = Session["SectionID"];
            if ((sessionParam1 == null) || (sessionParam2 == null))
            {
                //error
                Response.Redirect("ChooseSection.aspx");
            }

            int competitionId = (int)sessionParam1;
            int sectionId = (int)sessionParam2;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                CompetitionNameLabel.Text = (from a in competitionDataBase.zCompetitionsTable
                                             where a.ID == competitionId
                                             select a.Name).FirstOrDefault();
                SectionNameLeabel.Text = (from a in competitionDataBase.zSectionTable
                                             where a.ID == sectionId
                                             select a.Name).FirstOrDefault();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Description", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DataType", typeof (string)));
                List<zColumnTable> columnList = (from a in competitionDataBase.zColumnTable
                                                   where a.Active == true
                                                   && a.FK_SectionTable == sectionId
                                                   select a).ToList();
                DataType newDataType = new DataType();
                foreach (zColumnTable currentColumn in columnList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentColumn.ID;
                    dataRow["Name"] = currentColumn.Name;
                    dataRow["Description"] = currentColumn.Description;
                    dataRow["DataType"] = newDataType.GetDataTypeName(Convert.ToInt32(currentColumn.DataType));
                    dataTable.Rows.Add(dataRow);
                }

                ColumnGV.DataSource = dataTable;
                ColumnGV.DataBind();
            }
        }
        protected void NewColumn_Click(object sender, EventArgs e)
        {
            Session["ColumnID"] = 0;
            Response.Redirect("ColumnCreateEdit.aspx");
        }   
        protected void ChangeButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["ColumnID"] = iD;
                Response.Redirect("ColumnCreateEdit.aspx");
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zColumnTable columnToDelete = (from a in competitionDataBase.zColumnTable
                                                 where a.ID == iD
                                                 select a).FirstOrDefault();
                if (columnToDelete != null)
                {
                    columnToDelete.Active = false;
                    competitionDataBase.SubmitChanges();
                }
                else
                {
                    //error
                }

            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseSection.aspx");
        }
    }
}
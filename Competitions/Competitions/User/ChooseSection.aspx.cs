using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.User
{
    public partial class ChooseSection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sessionParam = Session["ApplicationID"];
            var userIdParam = Session["UserID"];

            if (!Page.IsPostBack)
            {
                if (sessionParam == null)
                {
                    //error
                    Response.Redirect("ChooseApplication.aspx");
                }
                else
                {
                    int iD = (int) sessionParam;
                    int userId = (int) userIdParam;

                    CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                    zApplicationTable currenApplication = (from a in competitionDataBase.zApplicationTable
                        where a.ID == iD
                        select a).FirstOrDefault();

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("Status", typeof (string)));

                    List<zSectionTable> sectionList = (from a in competitionDataBase.zSectionTable
                                                       where a.FK_CompetitionsTable == currenApplication.FK_CompetitionTable
                              && a.Active == true
                        select a).ToList();

                    foreach (zSectionTable zurrentSection in sectionList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = zurrentSection.ID;
                        dataRow["Name"] = zurrentSection.Name;
                        dataRow["Status"] = "Не заполнено/заполнено";
                        dataTable.Rows.Add(dataRow);
                    }

                    ApplicationGV.DataSource = dataTable;
                    ApplicationGV.DataBind();
                }
            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseApplicationAction.aspx");
        }
        protected void FillButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["SectionID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("FillSection.aspx");
            }

            //  Response.Redirect("ChooseApplication.aspx");
        }        
    }
}
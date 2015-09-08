using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Competitions.User
{
    public partial class UserMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));

                List<zCompetitionsTable> competitionsList = (from a in competitionDataBase.zCompetitionsTable
                                                             where a.Active == true
                                                             select a).ToList();

                foreach (zCompetitionsTable currentCompetition in competitionsList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentCompetition.ID;
                    dataRow["Name"] = currentCompetition.Name;
                    dataRow["Number"] = currentCompetition.Number;
                   
                    dataTable.Rows.Add(dataRow);
                }
                MainGV.DataSource = dataTable;
                MainGV.DataBind();
            }
        }

        protected void MyApplication_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/ChooseApplication.aspx");
        }
        protected void NewApplication_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (CompetitionDataContext newBid = new CompetitionDataContext())
                {
                    /*zCompetitionsTable id = (from a in newBid.zCompetitionsTable
                                   where a.ID == Convert.ToInt32(button.CommandArgument)
                                   select a).FirstOrDefault();
                    */
                    Session["ID_Konkurs"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/User/ApplicationCreateEdit.aspx");
                }
            }
        }
    }
}
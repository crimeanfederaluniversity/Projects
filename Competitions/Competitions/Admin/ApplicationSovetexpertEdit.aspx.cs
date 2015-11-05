using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class ApplicationSovetexpertEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zExpertGroup> groupList = (from a in CompetitionsDataBase.zExpertGroup
                    where a.Active == true
                    select a).ToList();
                if (groupList != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("ID", typeof (string));
                    dataTable.Columns.Add("Name", typeof (string));

                      foreach (zExpertGroup current in groupList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = current.ID;
                    dataRow["Name"] = current.Name;
                    dataTable.Rows.Add(dataRow);
                   
                      }
                    sovetExpertsGV.DataSource = dataTable;
                    sovetExpertsGV.DataBind();
                }

            }
        }

        protected void GroupButtonClick(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                if (button != null)
                {
                    Session["GroupID"] = button.CommandArgument;
                    Response.Redirect("CompetitionExpertEdit.aspx");
                }
            }


        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseCompetition.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            zExpertGroup newgrouplist = new zExpertGroup();
            newgrouplist.Active = true;
            newgrouplist.Name = TextBox1.Text;
            CompetitionsDataBase.zExpertGroup.InsertOnSubmit(newgrouplist);
            CompetitionsDataBase.SubmitChanges();

        }
 
    }
}
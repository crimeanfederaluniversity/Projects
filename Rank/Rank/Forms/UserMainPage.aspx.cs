using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class UserMainPage : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["UserID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = (int)userId;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));

            List<Rank_Parametrs> allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true select a).ToList();
            if (allparam != null)
            {
                foreach (var tmp in allparam)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
                    dataRow["Point"] = ""; // запрос на значения
                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["parametrID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Forms/UserArticlePage.aspx");
            }
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
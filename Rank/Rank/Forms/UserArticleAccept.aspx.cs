using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class UserArticleAccept : System.Web.UI.Page
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
            Refresh();
        }
        protected void Refresh()
        {
            var userId = Session["UserID"];
            int userID = (int)userId;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));

            List<Rank_Articles> authorList = (from b in ratingDB.Rank_Articles where b.Active == true
                                              join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_Article
                                              where a.Active == true && a.FK_User == userID && a.UserConfirm == false select b).ToList();
            foreach (Rank_Articles value in authorList)
            {
                Rank_DifficaltPoint point = (from b in ratingDB.Rank_DifficaltPoint where b.Active == true
                                             join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_point
                                             where a.FK_Article == value.ID select b).FirstOrDefault();
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = value.ID;
                dataRow["Name"] = value.Name;
                dataRow["Date"] = value.AddDate;
                dataRow["Point"] = point.Name;
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

        }
    }
}
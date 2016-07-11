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
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            if(rights.AccessLevel != 0)
            {
                Button3.Visible = true;
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));

            List<Rank_Parametrs> 
                allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true select a).ToList();
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
                var userId = Session["UserID"];
                int userID = (int)userId;
                Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == Convert.ToInt32(button.CommandArgument) select item).FirstOrDefault();
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                if (rights.AccessLevel == 9 && name.OneOrManyAuthor == false)
                {
                    Session["parametrID"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/Forms/FormCreateEditByUser.aspx");
                }
                else
                {
                    Session["parametrID"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/Forms/UserArticlePage.aspx");
                }
            }
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserArticleAccept.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/StructPointsForm.aspx");
        }
    }
}
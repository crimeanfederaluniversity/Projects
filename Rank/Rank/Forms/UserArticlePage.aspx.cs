using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class UserArticlePage : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var IdParam = Session["parametrID"];
            if (IdParam == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int paramId = (int)IdParam;

                Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
                Label1.Text = name.Name;
                var userId = Session["UserID"];
                if (userId == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int userID = (int)userId;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                List<Rank_Articles> userparamarticle = (from a in ratingDB.Rank_Articles
                                                        where a.Active == true && a.FK_parametr == paramId
                                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                                        where b.FK_User == userID && b.Active == true
                                                        select a).ToList();
                foreach (var tmp in userparamarticle)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Name"] = tmp.Name;
                    dataRow["Status"] = tmp.Status;
                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        

        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Session["articleID"] = Convert.ToInt32(button.CommandArgument);
            Response.Redirect("~/Forms/UserFillFormPage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var userId = Session["UserID"];           
            int userID = (int)userId;
            int paramId = Convert.ToInt32(Session["parametrID"]);
            Rank_Articles newValue = new Rank_Articles();
            newValue.Active = true;
            newValue.AddDate = DateTime.Now;
            newValue.FK_parametr = paramId;       
            newValue.Status = 0;
            ratingDB.Rank_Articles.InsertOnSubmit(newValue);
            ratingDB.SubmitChanges();
            Rank_UserArticleMappingTable newLink = new Rank_UserArticleMappingTable();
            newLink.Active = true;
            newLink.FK_Article = newValue.ID;
            newLink.UserConfirm = false;
            newLink.FK_User = userID;
            ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newLink);
            ratingDB.SubmitChanges();
            Session["articleID"] = Convert.ToInt32(newValue.ID);
            Response.Redirect("~/Forms/UserFillFormPage.aspx");
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class StructUserPointsForAccept : System.Web.UI.Page
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
            dataTable.Columns.Add(new DataColumn("User", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Color", typeof(string)));
            List<UsersTable> structusers = new List<UsersTable>();
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();         
                // надо в базе сделать аксес левел чтобы понятно у кого есть подчиненные и по его привязке к структуре вытаскивать всех под ним
                if ( rights.FK_FirstLevelSubdivisionTable != null &&  rights.FK_SecondLevelSubdivisionTable != null && rights.FK_ThirdLevelSubdivisionTable != null)
                {
                    structusers = (from a in ratingDB.UsersTable  where a.Active == true &&
                                    (a.FirstLevelSubdivisionTable == rights.FirstLevelSubdivisionTable
                                    && a.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable
                                    && a.FK_ThirdLevelSubdivisionTable == rights.FK_ThirdLevelSubdivisionTable)
                                   select a).ToList();
                }
                if (rights.FK_FirstLevelSubdivisionTable != null && rights.FK_SecondLevelSubdivisionTable != null )
                {
                    structusers = (from a in ratingDB.UsersTable
                                   where a.Active == true &&  (a.FirstLevelSubdivisionTable == rights.FirstLevelSubdivisionTable  && a.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable)
                                   select a).ToList();
                }
                if (rights.FK_FirstLevelSubdivisionTable != null)
                {
                    structusers = (from a in ratingDB.UsersTable   where a.Active == true &&  a.FirstLevelSubdivisionTable == rights.FirstLevelSubdivisionTable  select a).ToList();
                }
                if (rights.AccessLevel == 9)
                {
                    structusers = (from a in ratingDB.UsersTable where a.Active == true select a).ToList();
                }
          
            if (structusers != null)
            {
                int allsumm = 0;           
                foreach (var tmp in structusers)
                {
                    List<Rank_UserParametrValue> userrating = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == tmp.UsersTableID select a).ToList();
                    int sum = 0;
                    
                    foreach (var a in userrating)
                    {
                        if(a.Value != null )
                        sum = sum + Convert.ToInt32(a.Value.Value);
                    }
                    List<Rank_Articles> userarticles = new List<Rank_Articles>();
                    if (rights.AccessLevel == 1)
                    {
                        userarticles = (from a in ratingDB.Rank_Articles
                                        where a.Active == true && a.Status == 1
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.Active == true && b.FK_User == tmp.UsersTableID && b.UserConfirm == true && b.CreateUser == true
                                        join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                        where c.AccessLevel == 0
                                        select a).ToList();
                    }
                    if (rights.AccessLevel == 2)
                    {
                        userarticles = (from a in ratingDB.Rank_Articles
                                        where a.Active == true && a.Status == 1
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.Active == true && b.FK_User == tmp.UsersTableID && b.UserConfirm == true && b.CreateUser == true
                                        join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                        where c.AccessLevel == 1
                                        select a).ToList();
                    }
                    if (rights.AccessLevel == 4)
                    {
                        userarticles = (from a in ratingDB.Rank_Articles
                                        where a.Active == true && a.Status == 1
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.Active == true && b.FK_User == tmp.UsersTableID && b.UserConfirm == true && b.CreateUser == true
                                        join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                        where c.AccessLevel == 2
                                        select a).ToList();
                    }


                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.UsersTableID;
                    dataRow["User"] = tmp.Surname + " " + tmp.Name + " " + tmp.Patronimyc;
                    dataRow["Point"] = sum;
                    allsumm = allsumm + sum;
                    if(userarticles!= null && userarticles.Count != 0)
                    {
                        dataRow["Status"] = "Ожидает Вашего утверждения";
                        dataRow["Color"] = 1; // красный                       
                    }
                    else
                    {
                        dataRow["Status"] = "Не требует утверждения";
                        dataRow["Color"] = "";
                    }
           
                    dataTable.Rows.Add(dataRow);
                }
                Label1.Text = allsumm.ToString();
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void ShowButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["showuserID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Forms/HeadAcceptSecond.aspx");
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var lblColor = e.Row.FindControl("Color") as Label;
            if (lblColor != null)
            {
                if (lblColor.Text == "1") // красный 
                {
                    e.Row.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
                }
                if (lblColor.Text == "2") // желтый
                {
                    e.Row.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");
                }
                if (lblColor.Text == "3") // зеленый
                {
                    e.Row.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
                }
            }
        }
    }
}
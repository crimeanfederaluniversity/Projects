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
            var IdParam = Session["parametrID"];
            if (IdParam == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int paramId = (int)IdParam;
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
            var IdParam = Session["parametrID"];
            int paramId = (int)IdParam;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("User", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

            List<UsersTable> structusers;
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            if (rights.AccessLevel == 9)
            {
                structusers = (from a in ratingDB.UsersTable where a.Active == true select a).ToList();
            }
            else
            {
                // надо в базе сделать аксес левел чтобы понятно у кого есть подчиненные и по его привязке к структуре вытаскивать всех под ним
                structusers = (from a in ratingDB.UsersTable where a.Active == true && 
                               (a.FirstLevelSubdivisionTable == rights.FirstLevelSubdivisionTable 
                               && a.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable
                               && a.FK_ThirdLevelSubdivisionTable == rights.FK_ThirdLevelSubdivisionTable) select a).ToList();
            }
            
            if (structusers != null)
            {
                Rank_UserParametrValue point;
                foreach (var tmp in structusers)
                {
                    point = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == tmp.UsersTableID
                                                    && a.FK_parametr == paramId select a).FirstOrDefault();
                    if (point == null)
                    {
                        point = new Rank_UserParametrValue();
                        point.Active = true;
                        point.FK_parametr = paramId;
                        point.FK_user = tmp.UsersTableID;                      
                        ratingDB.Rank_UserParametrValue.InsertOnSubmit(point);
                        ratingDB.SubmitChanges();
                    }
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.UsersTableID;
                    dataRow["User"] = tmp.Surname + " " + tmp.Name + " " + tmp.Patronimyc;
                    if (point.Accept == true)
                        dataRow["Status"] = "Утвержден";
                    if (point.Accept == false)
                        dataRow["Status"] = "Не утвержден";
                    if (point != null)
                    {
                        dataRow["Point"] = point.Value;
                    }
                    else
                    {
                        dataRow["Point"] = "нед рассчитан";
                    }
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void ShowButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["userID"] = Convert.ToInt32(button.CommandArgument); // надо сделать чтобы с этиой сессией был только просмотр
                Response.Redirect("~/Forms/UserArticlePage.aspx");
            }
        }
        protected void AcceptButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                // окрасить в зеленый
                Rank_UserParametrValue accept = (from a in ratingDB.Rank_UserParametrValue where a.FK_user == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();
                accept.Accept = true;
                accept.AcceptDate = DateTime.Now;
                ratingDB.SubmitChanges();
                Refresh();
            }
        }
    }
}
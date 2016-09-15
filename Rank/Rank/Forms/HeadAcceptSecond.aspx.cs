using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class StructPointsForm : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            object str_userID =  Session["userID"] ?? String.Empty;
            bool isSet_userID = int.TryParse(str_userID.ToString(), out userId);

            if (!isSet_userID)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = (int)userId;

            int sotrudnik = 0;
            object str_showuserID =  Session["showuserID"] ?? String.Empty;
            bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out sotrudnik);

            UsersTable name = (from a in ratingDB.UsersTable where a.UsersTableID == sotrudnik select a).FirstOrDefault();
            Label1.Text = name.Surname + " " + name.Name + " " + name.Patronimyc;
            Refresh();
            List<Rank_Parametrs> allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true select a).ToList();
            foreach (var PAR in allparam)
            {
                List<Rank_Articles> userparamarticle = (from a in ratingDB.Rank_Articles
                                                  where a.Active == true && a.FK_parametr == PAR.ID && a.Status == 1
                                                  join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                                  where b.FK_User == userID && b.Active == true && b.UserConfirm == true
                                                  select a).ToList();
                foreach (var ART in userparamarticle)
                {
                    Calculate userpoints = new Calculate();
                 //   userpoints.CalculateStructParametrPoint(PAR.ID, userID);
                }
            }
        }
        protected void Refresh()
        {
            int userId = 0;
            object str_userID =  Session["userID"] ?? String.Empty;
            bool isSet_userID = int.TryParse(str_userID.ToString(), out userId);

            int userID = (int)userId;

            int userpoints = 0;
            object str_showuserID =  Session["showuserID"] ?? String.Empty;
            bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out userpoints);

            Rank_UserRatingPoints bal = (from item in ratingDB.Rank_UserRatingPoints where item.FK_User == userID select item).FirstOrDefault();
            if(bal != null && bal.Value.HasValue)
            {
                Label2.Text = bal.Value.Value.ToString();
            }
            else
            {
                Label2.Text = "0";
            }          
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Number", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Color", typeof(string)));
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Number", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Point", typeof(string)));
            List<Rank_Parametrs> allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.EditUserType == 1  select a).ToList();
            List<Rank_Parametrs> allparam2 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.EditUserType == 0 select a).ToList();
            if (allparam != null)
            {              
                foreach (Rank_Parametrs tmp in allparam)
                {
                    Rank_UserParametrValue point = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == userpoints
                                                    select a).FirstOrDefault();
  
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
                    dataRow["Number"] = tmp.Number;
                    UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                    List<Rank_Articles> userarticles = new List<Rank_Articles>();
                    if (rights.AccessLevel == 1)
                    {
                        userarticles = (from a in ratingDB.Rank_Articles
                                        where a.Active == true && a.Status == 1 && a.FK_parametr ==  tmp.ID
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.Active == true && b.FK_User == userpoints && b.UserConfirm == true && b.CreateUser == true
                                        join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                        where c.AccessLevel == 0 && c.FK_FirstLevelSubdivisionTable == rights.FK_FirstLevelSubdivisionTable && c.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable
                                        && c.FK_ThirdLevelSubdivisionTable == rights.FK_ThirdLevelSubdivisionTable
                                        select a).ToList();
                    }
                    if (rights.AccessLevel == 2)
                    {
                        userarticles = (from a in ratingDB.Rank_Articles
                                        where a.Active == true && a.Status == 1 && a.FK_parametr == tmp.ID
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.Active == true && b.FK_User == userpoints  && b.UserConfirm == true && b.CreateUser == true
                                        join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                        where c.AccessLevel == 1 && c.FK_FirstLevelSubdivisionTable == rights.FK_FirstLevelSubdivisionTable && c.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable
                                        
                                        select a).ToList();
                    }
                    if (rights.AccessLevel == 4)
                    {
                        userarticles = (from a in ratingDB.Rank_Articles
                                        where a.Active == true && a.Status == 1 && a.FK_parametr == tmp.ID
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.Active == true && b.FK_User == userpoints && b.UserConfirm == true && b.CreateUser == true
                                        join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                        where c.AccessLevel == 2 && c.FK_FirstLevelSubdivisionTable == rights.FK_FirstLevelSubdivisionTable  
                                        select a).ToList();
                    }
                    if (point != null)
                    {
                        dataRow["Point"] = point.Value;
                    }
                    else
                    {
                        dataRow["Point"] = "";
                    }
                    if (userarticles != null && userarticles.Count != 0)
                    {
                        dataRow["Status"] = "Ожидает верификации";
                        dataRow["Color"] = 1; // красный                       
                    }
                    else
                    {
                        dataRow["Status"] = "Верифицировано";
                        dataRow["Color"] = "";
                    }

                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
            if (allparam2 != null)
            {
                foreach (var tmp in allparam2)
                {
                    Rank_UserParametrValue point = (from a in ratingDB.Rank_UserParametrValue
                                                    where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == userpoints
                                                    select a).FirstOrDefault();

                    DataRow dataRow = dataTable2.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
                    dataRow["Number"] = tmp.Number;
                    UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                    List<Rank_Articles> userarticles = new List<Rank_Articles>();

                    if (point != null)
                    {
                        dataRow["Point"] = point.Value;
                    }
                    else
                    {
                        dataRow["Point"] = "";
                    }
                    dataTable2.Rows.Add(dataRow);
                }
                GridView2.DataSource = dataTable2;
                GridView2.DataBind();
            }

        }
        protected void ShowButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int sotrudnik = 0;
                object str_showuserID =  Session["showuserID"] ?? String.Empty;
                bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out sotrudnik);

                Session["showuserID"] = sotrudnik;

                int btnArg;
                int.TryParse(button.CommandArgument, out btnArg);
                Session["parametrID"] = btnArg;

                Response.Redirect("~/Forms/UserArticlePage.aspx");
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblColor = e.Row.FindControl("Color") as Label;
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
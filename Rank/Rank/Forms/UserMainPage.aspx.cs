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
            List<Rank_UserParametrValue> userrating = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == userID select a).ToList();
            int sum = 0;
            foreach (var a in userrating)
            { if(a.Value.HasValue)
                {
                    sum = sum + Convert.ToInt32(a.Value.Value);
                }               
            }
            Label1.Text = sum.ToString();
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();        
            List<Rank_Articles> authorList = (from b in ratingDB.Rank_Articles
                                              where b.Active == true
                                              join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_Article
                                              where a.Active == true && a.FK_User == userID && a.UserConfirm == false
                                              select b).ToList();
            if(authorList.Count != 0)
            {
                Button2.Text += "(" + authorList.Count.ToString() + ")";
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));

            List<Rank_Parametrs> allparam;  
          
            if (rights.AccessLevel == 9)
            {
                allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true && (a.EditUserType == 1 || a.EditUserType == 2) select a).ToList();
            }
            else
            {
                Button2.Visible = true;
                Label1.Visible = true;
                Label2.Visible = true;
                allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true select a).ToList();
            }
            if (allparam != null)
            {
                foreach (var tmp in allparam)
                {
                    Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                        where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == userID
                                                        select a).FirstOrDefault();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
                   
                   if(calculate!= null)
                    {
                        dataRow["Point"] = calculate.Value;
                    }
                    else
                    {
                        dataRow["Point"] = "нет данных";
                    }
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
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var userId = Session["UserID"];
                int userID = (int)userId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                if(rights.AccessLevel != 9)
                {
                    Button but = (e.Row.FindControl("EditButton") as Button);
                    Rank_Parametrs param = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.ID == Convert.ToUInt32(but.CommandArgument) select a).FirstOrDefault();
                   
                    if(param.EditUserType == 1 || param.EditUserType == 2 )
                    {
                        but.Text = "Редактировать";
                    }
                    else
                    {
                        but.Text = "Просмотреть";
                    }                    
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
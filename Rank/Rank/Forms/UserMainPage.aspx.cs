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
            int userId = 0;
            object str_UserID = Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);
            
            if (!isSet_UserID)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = (int)userId;
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();

            int showuser = 0;
            object str_showuserID = Session["showuserID"] ?? String.Empty;
            bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out showuser);
         
            if (rights.AccessLevel == 9)
            {
                Label3.Text = "Ввод рейтинговых данных научно-педагогических работников КФУ";
                Button2.Visible = false;
                Label1.Visible = false;
                    if (isSet_showuserID)
                    {
                        UsersTable user = (from item in ratingDB.UsersTable where item.UsersTableID == showuser select item).FirstOrDefault();
                        Label2.Text = user.Surname.ToString() + " " + user.Name.ToString() + " " + user.Patronimyc.ToString();
                    }
                    else
                    {
                        GridView1.Columns[3].Visible = false;
                        GridView2.Visible = false;              
                    }
                }
            
            else
            {
                Label1.Visible = true;
                Button2.Visible = true;               
            }
            
            Calculate userpoints = new Calculate();
              
            List<Rank_Articles> authorList = (from b in ratingDB.Rank_Articles
                                              where b.Active == true
                                              join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_Article
                                              where a.Active == true && a.FK_User == userID && a.UserConfirm == false
                                              select b).ToList();
            if(authorList.Count != 0)
            {
                Button2.Text += "(" + authorList.Count.ToString() + ")";
            }
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Number", typeof(string)));

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Number", typeof(string)));
            List<Rank_Parametrs> allparam1 = new List<Rank_Parametrs>();
            List<Rank_Parametrs> allparam2 = new List<Rank_Parametrs>();
            if (rights.TypeOfPosition == true)
            {
                allparam1 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.EditUserType == 0 select a).OrderBy(mc => mc.Number).ToList();
                allparam2 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.EditUserType == 1 select a).OrderBy(mc => mc.Number).ToList();
            }
            else
            {
                allparam1 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.UserType == 2 && a.EditUserType == 0 select a).OrderBy(mc => mc.Number).ToList();
                allparam2 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.UserType == 2 && a.EditUserType == 1 select a).OrderBy(mc => mc.Number).ToList();
            }

            List<Rank_Articles> allarticleList = (from b in ratingDB.Rank_Articles
                                                    where b.Active == true
                                                    join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_Article
                                                    where a.Active == true && a.FK_User == userID && a.UserConfirm == true
                                                    select b).ToList();

 
            List<Rank_UserParametrValue> userrating = new List<Rank_UserParametrValue>();
            if (rights.AccessLevel == 9)
            {
                if (isSet_showuserID)
                {
                    userrating = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == showuser select a).ToList();
                    int sum = 0;
                    foreach (var a in userrating)
                    {
                        if (a.Value.HasValue)
                        {
                            sum = sum + Convert.ToInt32(a.Value.Value);
                        }
                    }
                    Label1.Text = sum.ToString();
                }
            }
            else
            {                
                userrating = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == userID select a).ToList();
                int sum = 0;
                foreach (var a in userrating)
                {
                    if (a.Value.HasValue)
                    {
                        sum = sum + Convert.ToInt32(a.Value.Value);
                    }
                }
                Label1.Text = sum.ToString();
            }    
            if (allparam1 != null)
            {
                foreach (var tmp in allparam1)
                {
                    Rank_UserParametrValue calculate = new Rank_UserParametrValue();
                    if (rights.AccessLevel == 9 && isSet_showuserID)
                    {
                            calculate = (from a in ratingDB.Rank_UserParametrValue
                                         where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == showuser
                                         select a).FirstOrDefault();
                        userpoints.CalculateUserParametrPoint(tmp.ID, showuser);
                    }   
                    if(rights.AccessLevel != 9)
                    {
                        calculate = (from a in ratingDB.Rank_UserParametrValue
                                     where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == userID
                                     select a).FirstOrDefault();
                        userpoints.CalculateUserParametrPoint(tmp.ID, userID);
                    }
                    DataRow dataRow = dataTable1.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
                    dataRow["Number"] = tmp.Number;
                    if (calculate!= null)
                    {
                        dataRow["Point"] = calculate.Value;
                    }
                    else
                    {
                        dataRow["Point"] = "";
                    }
                    dataTable1.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable1;
                GridView1.DataBind();
            }
            if (allparam2 != null)
            {
                foreach (var tmp in allparam2)
                {
                    Rank_UserParametrValue calculate = new Rank_UserParametrValue();
                    if (rights.AccessLevel == 9 && isSet_showuserID)
                    {
                        int id = (int)showuser;
                        calculate = (from a in ratingDB.Rank_UserParametrValue
                                     where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == id
                                     select a).FirstOrDefault();
                        userpoints.CalculateUserParametrPoint(tmp.ID, id);
                    }
                    if (rights.AccessLevel != 9)
                    {
                        calculate = (from a in ratingDB.Rank_UserParametrValue
                                     where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == userID
                                     select a).FirstOrDefault();
                        userpoints.CalculateUserParametrPoint(tmp.ID, userID);
                    }
                    DataRow dataRow = dataTable2.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
                    dataRow["Number"] = tmp.Number;
                    if (calculate != null)
                    {
                        dataRow["Point"] = calculate.Value;
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

        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int userId = 0;
            object str_UserID = Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;

            int btnCmdArg = 0;
            object str_btnCmdArg = button.CommandArgument ?? String.Empty;
            bool isSet_btnCmdArg = int.TryParse(str_btnCmdArg.ToString(), out btnCmdArg);

            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == btnCmdArg select item).FirstOrDefault();
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
          
            Session["parametrID"] = btnCmdArg;

            Response.Redirect("~/Forms/UserArticlePage.aspx");
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int userId = 0;
                object str_UserID = Session["UserID"] ?? String.Empty;
                bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

                int userID = (int)userId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                Button but = (e.Row.FindControl("EditButton") as Button);
                if (rights.AccessLevel == 9)
                {
                but.Text = "Внести данные";
                    }
                    else
                    {
                        but.Text = "Просмотреть";
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
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
            var showuser = Session["showuserID"];
         
                if (rights.AccessLevel == 9)
            {
                Button2.Visible = false;
                Label1.Visible = false;
                    if (showuser != null)
                    {
                    int id = (int)showuser;
                        UsersTable user = (from item in ratingDB.UsersTable where item.UsersTableID == id select item).FirstOrDefault();
                        Label2.Text = user.Surname.ToString() + " " + user.Name.ToString() + " " + user.Patronimyc.ToString();
                    }
                    else
                    {
                        GridView1.Columns[2].Visible = false;
                        GridView2.Visible = false;
                        Label2.Text = "Индивидуальный рейтинг научно-педагогических работников, подразделений, СП(Ф) высшего образования и научных СП(Ф) и их руководителей ФГАОУ ВО «КФУ им. В.И.Вернадского» за 2016 год:";
                    }
                }
            
            else
            {
                Label1.Visible = true;
                Button2.Visible = true;
                Label2.Text = "Индивидуальный рейтинг научно-педагогических работников, подразделений, СП(Ф) высшего образования и научных СП(Ф) и их руководителей ФГАОУ ВО «КФУ им. В.И.Вернадского» за 2016 год:";
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

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable2.Columns.Add(new DataColumn("Point", typeof(string)));
       
            List<Rank_Parametrs> allparam1 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.EditUserType == 0 select a).ToList();
            List<Rank_Parametrs> allparam2 = (from a in ratingDB.Rank_Parametrs where a.Active == true && a.EditUserType == 1 select a).ToList();
       
                List<Rank_Articles> allarticleList = (from b in ratingDB.Rank_Articles
                                                      where b.Active == true
                                                      join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_Article
                                                      where a.Active == true && a.FK_User == userID && a.UserConfirm == true
                                                      select b).ToList();

            /*  foreach (var a in allparam)
              {
                  if (allarticleList.Count == 0)
                  {
                      List<Rank_UserParametrValue> clean = (from b in ratingDB.Rank_UserParametrValue
                                                                     where b.Active == true && b.FK_user == userID && b.FK_parametr == a.ID   select b).ToList();
                      foreach(var b in clean)
                      {
                          b.Value = 0;
                          ratingDB.SubmitChanges();
                      }

                  }
                  */
            List<Rank_UserParametrValue> userrating = new List<Rank_UserParametrValue>();
            if (rights.AccessLevel == 9)
            {
                if (showuser != null)
                {
                    int id = (int)showuser;
                    userrating = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == id select a).ToList();
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
                    if (rights.AccessLevel == 9 && showuser != null)
                    { 
                            int id = (int)showuser;
                            calculate = (from a in ratingDB.Rank_UserParametrValue
                                         where a.Active == true && a.FK_parametr == tmp.ID && a.FK_user == id
                                         select a).FirstOrDefault();
                        userpoints.CalculateUserParametrPoint(tmp.ID, id);
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
                   if(calculate!= null)
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
                    if (rights.AccessLevel == 9 && showuser != null)
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
            { 
                var userId = Session["UserID"];
                int userID = (int)userId;
                Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == Convert.ToInt32(button.CommandArgument) select item).FirstOrDefault();
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
          
                Session["parametrID"] = Convert.ToInt32(button.CommandArgument);
                    Response.Redirect("~/Forms/UserArticlePage.aspx");              
            }
           
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var userId = Session["UserID"];
                int userID = (int)userId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                Button but = (e.Row.FindControl("EditButton") as Button);
                if (rights.AccessLevel == 9)
                {
                but.Text = "Редактировать";
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
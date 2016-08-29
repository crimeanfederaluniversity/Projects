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
            Refresh();
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            Label1.Text = name.Name;
            var userId = Session["UserID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = (int)userId;
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            var edituserId = Session["showuserID"];
            if (edituserId != null )
            {
                int edituser = (int)edituserId;
                UsersTable username = (from item in ratingDB.UsersTable where item.UsersTableID == edituser select item).FirstOrDefault();
                Label2.Visible = true;
                Label2.Text = username.Surname + " " + username.Name + " " + username.Patronimyc;
                if (rights.AccessLevel == 9)
                {
                    TextBox1.Visible = true;
                    Button2.Visible = true;
                }                
            }
            else
            {              
                    if ((rights.AccessLevel == 9 && name.EditUserType != 0  && name.EditUserType != 2) || (rights.AccessLevel != 10 && name.EditUserType == 3)
                  || (rights.AccessLevel != 9 && rights.AccessLevel != 10 && name.EditUserType != 1 && name.EditUserType != 2) || (rights.AccessLevel != 9 &&  paramId == 19))
                {
                    TextBox1.Visible = false;
                    Label4.Visible = false;
                    Button2.Visible = false;
                }
                else
                {
                    TextBox1.Visible = true;
                    Label4.Visible = true;
                    Button2.Visible = true;
                }
            }

        }
      
        protected void Refresh()
        {           
            var IdParam = Session["parametrID"];
            int paramId = (int)IdParam;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Color", typeof(string)));
            List<Rank_Articles> userparamarticle = new List<Rank_Articles>(); 
            var userId = Session["UserID"];
            int userID = (int)userId;         
            var edituserId = Session["showuserID"];
            if (edituserId != null)
            {
                GridView1.Columns[6].Visible = false;
                int edituser = (int)edituserId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();                           
                if (rights.AccessLevel != 9 && rights.AccessLevel != 0)
                {                 
                    userparamarticle = (from a in ratingDB.Rank_Articles  where a.Active == true && a.FK_parametr == paramId && a.Status != 0
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.FK_User == edituser && b.Active == true && b.UserConfirm == true 
                                        select a).ToList();
                }

            }
            else
            {               
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                if (rights.AccessLevel == 10 )
                {
                    GridView1.Columns[4].Visible = false;
                    userparamarticle = (from a in ratingDB.Rank_Articles  where a.Active == true && a.FK_parametr == 16 select a).ToList();
                }
                if (rights.AccessLevel == 9)
                {
                   GridView1.Columns[4].Visible = false;
                    userparamarticle = (from a in ratingDB.Rank_Articles  where a.Active == true && a.FK_parametr == paramId
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.FK_User == userID && b.Active == true && b.CreateUser == true
                                        select a).ToList();
                }
                if (rights.AccessLevel != 10 && rights.AccessLevel != 9 )
                    {
                        userparamarticle = (from a in ratingDB.Rank_Articles
                                            where a.Active == true && a.FK_parametr == paramId
                                            join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                            where b.FK_User == userID && b.Active == true && b.UserConfirm == true
                                            select a).ToList();
                    
                    }
            }
            foreach (var tmp in userparamarticle)
            {
                Rank_UserArticleMappingTable userarticlepoint = new Rank_UserArticleMappingTable();
                if (edituserId != null)
                {
                    int edituser = (int)edituserId;
                    UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == edituser select item).FirstOrDefault();
                    if (rights.AccessLevel != 9 || rights.AccessLevel != 0)
                    {
                        userarticlepoint = (from a in ratingDB.Rank_UserArticleMappingTable
                                            where a.Active == true && a.FK_Article == tmp.ID && a.FK_User == edituser
                                            select a).FirstOrDefault();
                    }
                }
                else
                {
                    UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                    if (rights.AccessLevel == 10)
                    {
                        GridView1.Columns[4].Visible = false;
                    }
                    else
                    {
                        userarticlepoint = (from a in ratingDB.Rank_UserArticleMappingTable
                                            where a.Active == true && a.FK_Article == tmp.ID && a.FK_User == userID
                                            select a).FirstOrDefault();
                    }
                }

                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = tmp.ID;
                dataRow["Name"] = tmp.Name;
                dataRow["Date"] = tmp.AddDate;
                if (userarticlepoint != null)
                {
                    
                        dataRow["Point"] = userarticlepoint.ValuebyArticle;
                   
                }
              
                if (tmp.Status == 0)
                {
                    dataRow["Status"] = "Доступно для редактирования";
                }
                    
                if (tmp.Status == 1)
                {
                    if (edituserId != null)
                    {
                        dataRow["Color"] = 1; // красный     
                    }
                    else
                    {
                        dataRow["Color"] = "";
                    }
                    dataRow["Status"] = "Отправлено на рассмотрение";
                    GridView1.Columns[6].Visible = false;
                }
                if (tmp.Status == 2)
                {
                    dataRow["Color"] = 3;
                    dataRow["Status"] = "Утверждена руководителем";
                    GridView1.Columns[6].Visible = false;
                }
                if (tmp.Status == 3)
                {
                    GridView1.Columns[6].Visible = false;
                    dataRow["Status"] = "Добавлено ОМР";
                }
                if (tmp.Status == 4)
                {
                    dataRow["Status"] = "Утверждено ОМР";
                   
                }
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;          
            GridView1.DataBind();
        }  
        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            Session["articleID"] = Convert.ToInt32(button.CommandArgument);
            var userId = Session["UserID"];
            int showuser = 0;
            showuser = Convert.ToInt32(Session["showuserID"]);

            Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();
            Rank_UserArticleMappingTable edit = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_User == Convert.ToInt32(userId) && a.FK_Article == send.ID select a).FirstOrDefault();
  
                var IdParam = Session["parametrID"];
                int paramId = (int)IdParam;
               
                int userID = (int)userId;
                Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();        
                if (paramId == 19 && send.Status == 0)
                {
                    Response.Redirect("~/Forms/CreateEditForm.aspx");
                }
                    if ((rights.AccessLevel == 9 && name.EditUserType != 0 && name.EditUserType != 2 && name.EditUserType != 3)
                    || (send.Status == 1 || send.Status == 2) || (rights.AccessLevel != 10 && name.EditUserType == 3)
                    || (rights.AccessLevel != 9 && rights.AccessLevel != 10 && name.EditUserType != 1 && name.EditUserType != 2)
                    || (rights.AccessLevel != 10 && name.EditUserType == 3) || (showuser != 0 ) || (edit.CreateUser == false || edit.CreateUser == null))
                {
                    Response.Redirect("~/Forms/ViewArticleForm.aspx");
                }
                else
                {
                    Response.Redirect("~/Forms/CreateEditForm.aspx");
                }                                         
        }
        protected void DeleteButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int article = Convert.ToInt32(Session["articleID"]);
                int paramId = Convert.ToInt32(Session["parametrID"]);
                var userId = Session["UserID"];               
                int userID = (int)userId;
                Rank_UserArticleMappingTable delete = (from item in ratingDB.Rank_UserArticleMappingTable where item.FK_Article == Convert.ToInt32(button.CommandArgument)
                                                       && item.FK_User == userID && item.Active == true
                                                       join b in ratingDB.Rank_Articles on item.FK_Article equals b.ID
                                                       where b.Active == true  && b.Status == 0 select item).FirstOrDefault();
                if (delete != null)
                {
                    delete.Active = false;
                    ratingDB.SubmitChanges();
                    Refresh();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Вы не можете удалить данный пункт, т.к. он уже отправлен на утверждение!');", true);
                }
              
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                var userId = Session["UserID"];
                int userID = (int)userId;
                int paramId = Convert.ToInt32(Session["parametrID"]);
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                             
                Rank_Articles newValue = new Rank_Articles();
                newValue.Active = true;
                newValue.AddDate = DateTime.Now;
                newValue.FK_parametr = paramId;
                newValue.Name = TextBox1.Text;
                if (rights.AccessLevel == 9)
                {
                    newValue.Status = 3;
                }
                else
                {
                    newValue.Status = 0;
                }
                ratingDB.Rank_Articles.InsertOnSubmit(newValue);
                ratingDB.SubmitChanges();
                             
                            
                if(rights.AccessLevel == 9 || rights.AccessLevel == 10)
                {
                    var edituserId = Session["edituserID"];
                    if (edituserId != null)
                    {
                        int edituser = (int)edituserId;
                        Rank_UserArticleMappingTable newLink = new Rank_UserArticleMappingTable();
                        newLink.Active = true;
                        newLink.FK_Article = newValue.ID;
                        newLink.FK_User = edituser;
                        newLink.UserConfirm = true;
                        if(paramId == 19)
                        {
                            newLink.CreateUser = true;
                        }
                        ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newLink);
                        ratingDB.SubmitChanges();
                        Rank_UserArticleMappingTable newLink2 = new Rank_UserArticleMappingTable();
                        newLink2.Active = true;
                        newLink2.FK_Article = newValue.ID;
                        newLink2.FK_User = userID;
                        newLink2.CreateUser = true;
                        ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newLink2);
                        ratingDB.SubmitChanges();
                    }
                    else
                    {
                        Rank_UserArticleMappingTable newLink3 = new Rank_UserArticleMappingTable();
                        newLink3.Active = true;
                        newLink3.FK_Article = newValue.ID;
                        newLink3.FK_User = userID;
                        newLink3.CreateUser = true;
                        newLink3.UserConfirm = true;
                        ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newLink3);
                        ratingDB.SubmitChanges();

                    }
                }
                else
                {
                    Rank_UserArticleMappingTable newLink = new Rank_UserArticleMappingTable();
                    newLink.Active = true;
                    newLink.FK_Article = newValue.ID;
                    newLink.FK_User = userID;
                    newLink.UserConfirm = true;
                    newLink.CreateUser = true;
                    ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newLink);
                    ratingDB.SubmitChanges();
                }                           
                Session["articleID"] = Convert.ToInt32(newValue.ID);
                Response.Redirect("~/Forms/CreateEditForm.aspx");
            }         
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var userId = Session["UserID"];
                int userID = (int)userId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                if (rights.AccessLevel != 9)
                {
                    Button but = (e.Row.FindControl("DeleteButton") as Button);
                    Rank_Articles delete = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == Convert.ToUInt32(but.CommandArgument) select a).FirstOrDefault();

                    if (delete.Status !=0 )
                    {
                        but.Enabled = false;
                    }
                    else
                    {
                        but.Enabled = true;
                    }
                }

            }
           
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserMainPage.aspx");
        }
    }
}

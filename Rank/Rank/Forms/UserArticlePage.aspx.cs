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
            int IdParam = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out IdParam);

            if (!isSet_parametrID)
                Response.Redirect("~/Default.aspx");

            int paramId = (int)IdParam;

            Refresh();
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            Label1.Text = name.Name;
            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            if (!isSet_UserID)
                Response.Redirect("~/Default.aspx");

            int userID = (int)userId;
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            if((name.EditUserType == 1 && rights.AccessLevel == 9) || (name.EditUserType == 0 && rights.AccessLevel != 9)|| (name.ID == 37))
            {
                Button2.Visible = false;
            }
            else
            {
                Button2.Visible = true;
            }

            int edituserId = 0;
            object str_showuserID =  Session["showuserID"] ?? String.Empty;
            bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out edituserId);

            if (isSet_showuserID)
            {
 
                Button2.Visible = false;
                int edituser = (int)edituserId;
                UsersTable username = (from item in ratingDB.UsersTable where item.UsersTableID == edituser select item).FirstOrDefault();
                Label2.Visible = true;
                Label2.Text = username.Surname + " " + username.Name + " " + username.Patronimyc;
          
             }

        }
      
        protected void Refresh()
        {
            int parametrID = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out parametrID);

            int? paramId = parametrID;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("Point", typeof(string));
            dataTable.Columns.Add("Color", typeof(string));
            List<Rank_Articles> userparamarticle = new List<Rank_Articles>();
            int userId = 0;
            object str_UserID = Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int? userID = (int)userId;

            int showuserID = 0;
            object str_showuserID = Session["showuserID"] ?? String.Empty;
            bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out showuserID);

            /*int edituserId = 0;
            string sShowuserId = (string) Session["showuserID"] ?? String.Empty;
            bool isshowuserIDSet = Session["showuserID"] != null;
            if (isshowuserIDSet)
                isshowuserIDSet = int.TryParse(sShowuserId, out edituserId);*/

            if (isSet_showuserID)
            {       
                Button2.Visible = false;
                GridView1.Columns[6].Visible = false;
                int edituser = (int)showuserID;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();                           
                if (rights.AccessLevel != 0)
                {                 
                    userparamarticle = (from a in ratingDB.Rank_Articles  where a.Active == true && a.FK_parametr == paramId && a.Status != 0
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.FK_User == edituser && b.Active == true && b.UserConfirm == true 
                                        select a).ToList();
                }
            }
            else
            {
                Button2.Visible = true;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
             
                if (rights.AccessLevel == 9)
                {
                   GridView1.Columns[2].Visible = false;
                    userparamarticle = (from a in ratingDB.Rank_Articles  where a.Active == true && a.FK_parametr == paramId
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                        where b.FK_User == userID && b.Active == true && b.CreateUser == true
                                        select a).ToList();
                }
                if ( rights.AccessLevel != 9 )
                    {
                    userparamarticle = (from a in ratingDB.Rank_Articles
                                            where a.Active == true && a.FK_parametr == parametrID
                                        join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                            where b.FK_User == userId && b.Active == true && b.UserConfirm == true
                                            select a).ToList();
                    }
            }

            foreach (Rank_Articles tmp in userparamarticle)
            {
                Rank_UserArticleMappingTable userarticlepoint = new Rank_UserArticleMappingTable();
                if (isSet_showuserID)
                {
                    int edituser = (int)showuserID;
                    UsersTable rights =
                        (from item in ratingDB.UsersTable where item.UsersTableID == edituser select item)
                            .FirstOrDefault();
                    if (rights.AccessLevel != 9 || rights.AccessLevel != 0)
                    {
                        userarticlepoint = (from a in ratingDB.Rank_UserArticleMappingTable
                            where a.Active == true && a.FK_Article == tmp.ID && a.FK_User == edituser
                            select a).FirstOrDefault();
                    }
                }
                else
                {
                    UsersTable rights =
                        (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
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
                Rank_ArticleValues name = (from a in ratingDB.Rank_ArticleValues
                                           where a.Active == true && a.FK_Article == tmp.ID
                                           join b in ratingDB.Rank_Fields on a.FK_Field equals b.ID
                                           where b.Active == true && b.namefield == true
                                           select a).FirstOrDefault();

                if (name != null && name.Value != null)
                {
                    dataRow["Name"] = name.Value;
                }
                else
                {
                    dataRow["Name"] = "Нет названия";
                }
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
            if (isSet_showuserID)
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
            dataRow["Status"] = "Верифицировано руководителем";
            GridView1.Columns[6].Visible = false;
        }
        if (tmp.Status == 3)
        {
            GridView1.Columns[6].Visible = false;
            dataRow["Status"] = "Добавлено ОМР";
        }
        if (tmp.Status == 4)
        {
            dataRow["Status"] = "Верифицировано ОМР";

        }
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;          
            GridView1.DataBind();
        }  
        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int btnCmdArg = 0;
            string sbtnCmdArg = (string)button.CommandArgument ?? String.Empty;
            bool isbtnCmdArgSet = int.TryParse(sbtnCmdArg, out btnCmdArg);

            Session["articleID"] = btnCmdArg;

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;
            Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == btnCmdArg select a).FirstOrDefault();
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            if (Session["showuserID"]!= null)
            {
                if(rights.AccessLevel == 9)
                {
                    Response.Redirect("~/Forms/CreateEditForm.aspx");
                }
                else
                {
                    Response.Redirect("~/Forms/ViewArticleForm.aspx");
                }

            }
            else
            {
                int IdParam = 0;
                object str_parametrID =  Session["parametrID"] ?? String.Empty;
                int.TryParse(str_parametrID.ToString(), out IdParam);

                int paramId = (int)IdParam;
                Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
                Rank_UserArticleMappingTable edit = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_User == userId && a.FK_Article == send.ID select a).FirstOrDefault();
                if ((send.Status != 0 ) 
                    || (rights.AccessLevel != 9 && rights.AccessLevel != 10 && name.EditUserType != 1 )
                    || (rights.AccessLevel != 10 && name.EditUserType == 3) || (edit.CreateUser == false || edit.CreateUser == null))
                     {
                    Response.Redirect("~/Forms/ViewArticleForm.aspx");
                }
                else
                {
                    Response.Redirect("~/Forms/CreateEditForm.aspx");
                }
            }                                           
        }
        protected void DeleteButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int btnCmdArg = 0;
            object str_btnCmdArg = button.CommandArgument ?? String.Empty;
            bool isSet_btnCmdArg = int.TryParse(str_btnCmdArg.ToString(), out btnCmdArg);

            int userID = (int)userId;
            Calculate userpoints = new Calculate();
                
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            if (rights.AccessLevel == 9 || rights.AccessLevel == 10)
            {
                Rank_Articles deletearticle = (from item in ratingDB.Rank_Articles
                                                where item.Active == true && item.ID == btnCmdArg
                                                select item).FirstOrDefault();
                if (deletearticle != null)
                {
                    deletearticle.Active = false;
                    ratingDB.SubmitChanges();
                    List<Rank_UserArticleMappingTable> deletelist = (from item in ratingDB.Rank_UserArticleMappingTable
                                                            where  item.FK_Article == btnCmdArg
                                                            && item.Active == true select item).ToList();
                    foreach(Rank_UserArticleMappingTable a in deletelist)
                    {
                        userpoints.CalculateUserArticlePoint(paramId, btnCmdArg, a.FK_User.Value);
                    }
                }
            }
            else
            {
                Rank_UserArticleMappingTable delete = (from item in ratingDB.Rank_UserArticleMappingTable
                                                        where item.FK_Article == btnCmdArg
                                                            && item.FK_User == userID && item.Active == true
                                                        join b in ratingDB.Rank_Articles on item.FK_Article equals b.ID
                                                        where b.Active == true && b.Status == 0
                                                        select item).FirstOrDefault();
                if (delete != null)
                {
                    delete.Active = false;
                    ratingDB.SubmitChanges();
                    userpoints.CalculateUserArticlePoint(paramId, btnCmdArg, userID);
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
                int userId = 0;
                object str_UserID =  Session["UserID"] ?? String.Empty;
                bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

                int userID = (int)userId;

                int paramId = 0;
                object str_parametrID =  Session["parametrID"] ?? String.Empty;
                bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);
                
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                             
                Rank_Articles newValue = new Rank_Articles();
                newValue.Active = true;
                newValue.AddDate = DateTime.Now;
                newValue.FK_parametr = paramId;
                if (rights.AccessLevel == 9 && paramId!= 19)
                {
                    newValue.Status = 4;
                }
                else
                {
                    newValue.Status = 0;
                }
                ratingDB.Rank_Articles.InsertOnSubmit(newValue);
                ratingDB.SubmitChanges();
                List<Rank_DifficaltPoint> one = (from item in ratingDB.Rank_DifficaltPoint where item.fk_parametr == paramId select item).ToList();
               
                Rank_UserArticleMappingTable newLink3 = new Rank_UserArticleMappingTable();
                newLink3.Active = true;
                newLink3.FK_Article = newValue.ID;
                newLink3.FK_User = userID;

                if (one != null && one.Count == 1)
                {
                    foreach (Rank_DifficaltPoint a in one)
                    {
                        newLink3.FK_point = a.ID;
                    }
                }
                    newLink3.CreateUser = true;
                    newLink3.UserConfirm = true;
                    ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newLink3);
                    ratingDB.SubmitChanges();
                                    
                Session["articleID"] = Convert.ToInt32(newValue.ID); // TODO: mono
                Response.Redirect("~/Forms/CreateEditForm.aspx");
                     
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int userId = 0;
                object str_UserID =  Session["UserID"] ?? String.Empty;
                bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

                int userID = (int)userId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                if (rights.AccessLevel != 9)
                {
                    Button but = (e.Row.FindControl("DeleteButton") as Button);

                    int articleId = 0;
                    object str_articleId = but.CommandArgument ?? String.Empty;
                    bool isSet_articleId = int.TryParse(str_articleId.ToString(), out articleId);

                    Rank_Articles delete = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == articleId select a).FirstOrDefault();

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

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserMainPage.aspx");
        }
    }
}

﻿using System;
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

            Refresh();
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            if (rights.AccessLevel == 9 && name.EditUserType == 1)
            {
                var edituserId = Session["edituserID"];
                if (edituserId != null)
                {
                    int edituser = (int)edituserId;
                }
                TextBox1.Visible = false;
                Button2.Visible = false;
            }
            else
            {
                TextBox1.Visible = true;
                Button2.Visible = true;
            }
            if (rights.AccessLevel != 9 && name.EditUserType == 0)
            {
                TextBox1.Visible = false;
                Button2.Visible = false;
            }
            else
            {
                TextBox1.Visible = true;
                Button2.Visible = true;
            }

        }
        protected void Refresh()
        {
            var IdParam = Session["parametrID"];
            int paramId = (int)IdParam;
            var userId = Session["UserID"];
            int userID = (int)userId;
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

            List<Rank_Articles> userparamarticle;
            if (rights.AccessLevel == 9)
            {
                userparamarticle = (from a in ratingDB.Rank_Articles
                                    where a.Active == true && a.FK_parametr == paramId
                                    select a).ToList();
            }
            else
            {
                userparamarticle = (from a in ratingDB.Rank_Articles
                                    where a.Active == true && a.FK_parametr == paramId
                                    join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                    where b.FK_User == userID && b.Active == true && b.UserConfirm == true
                                    select a).ToList();
            }

            foreach (var tmp in userparamarticle)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = tmp.ID;
                dataRow["Name"] = tmp.Name;
                dataRow["Date"] = tmp.AddDate;
                if (tmp.Status == 0)
                    dataRow["Status"] = "Доступна для редактирования";
                if (tmp.Status == 1)
                    dataRow["Status"] = "Отправлена на рассмотрение";
                if (tmp.Status == 2)
                    dataRow["Status"] = "Утверждена";
                if (tmp.Status == 3)
                    dataRow["Status"] = "Возвращена на исправление";
                if (tmp.Status == 4)
                    dataRow["Status"] = "Возвращена соавтором на испраление";
                dataTable.Rows.Add(dataRow);
            }

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
    
        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Session["articleID"] = Convert.ToInt32(button.CommandArgument);

            var IdParam = Session["parametrID"];          
            int paramId = (int)IdParam;

            var userId = Session["UserID"];
            int userID = (int)userId;
    
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();           
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();

            if (rights.AccessLevel == 9 && name.EditUserType == 0)
            {
                var edituserId = Session["edituserID"];
                if (edituserId != null)
                {
                    int edituser = (int)edituserId;
                    Session["newuserID"] = edituser;
                }
            }           
            Response.Redirect("~/Forms/CreateEditForm.aspx");
        }
        protected void DeleteButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var userId = Session["UserID"];               
                int userID = (int)userId;
                Rank_UserArticleMappingTable delete = (from item in ratingDB.Rank_UserArticleMappingTable where item.FK_Article == Convert.ToInt32(button.CommandArgument)
                                                       && item.FK_User == userID  select item).FirstOrDefault();
                if (delete != null)
                {
                    delete.Active = false;
                    ratingDB.SubmitChanges();
                }
                Refresh();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != null)
            {
                var userId = Session["UserID"];
                int userID = (int)userId;
                int paramId = Convert.ToInt32(Session["parametrID"]);
                Rank_Articles newValue = new Rank_Articles();
                newValue.Active = true;
                newValue.AddDate = DateTime.Now;
                newValue.FK_parametr = paramId;
                newValue.Name = TextBox1.Text;
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
                Response.Redirect("~/Forms/CreateEditForm.aspx");
            }
          
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserMainPage.aspx");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class UserArticleAccept : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);
            
            if (!isSet_UserID)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = (int)userId;

            Refresh();
        }
        protected void Refresh()
        {
            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));

            List<Rank_Articles> authorList = (from b in ratingDB.Rank_Articles  where b.Active == true
                                              join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_Article
                                              where a.Active == true && a.FK_User == userID && a.UserConfirm == false
                                              select b).ToList();
            foreach (Rank_Articles value in authorList)
            {
                Rank_DifficaltPoint point = (from b in ratingDB.Rank_DifficaltPoint  where b.Active == true
                                             join a in ratingDB.Rank_UserArticleMappingTable on b.ID equals a.FK_point
                                             where a.FK_Article == value.ID && a.FK_User == userID && a.UserConfirm == false
                                             select b).FirstOrDefault();
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = value.ID;
                dataRow["Name"] = value.Name;
                dataRow["Date"] = value.AddDate;
                if (point != null)
                {
                    dataRow["Point"] = point.Name;
                }
                else
                {
                    dataRow["Point"] = "нет привязки";
                }
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

        }

        protected void AcceptButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;

            int fk_article;
            int.TryParse(button.CommandArgument, out fk_article);

            Rank_UserArticleMappingTable confirm = (from a in ratingDB.Rank_UserArticleMappingTable
                                                    where a.Active == true && a.FK_User == userID && a.UserConfirm == false
                                                    && a.FK_Article == fk_article
                                                    select a).FirstOrDefault();
            Rank_Articles param = (from a in ratingDB.Rank_Articles
                                   where a.Active == true && a.ID == fk_article
                                   select a).FirstOrDefault();

            GridViewRow row = (GridViewRow)button.Parent.Parent;
            DropDownList drop = (DropDownList)row.FindControl("ddlPoint");
  
            int fkpoint;
            Int32.TryParse(drop.SelectedValue, out fkpoint);
            if (fkpoint != 0)
            {
                confirm.FK_point = fkpoint;
                ratingDB.SubmitChanges();
            }
            else
            { 
            }

            int paramId = Convert.ToInt32(param.FK_parametr); // TODO: mono
            if (confirm != null)
            {
                confirm.UserConfirm = true;
                ratingDB.SubmitChanges();
            }
            Calculate userpoints = new Calculate();

            userpoints.CalculateUserArticlePoint(paramId, fk_article, userID);
            userpoints.CalculateUserParametrPoint(paramId, userID);

            Refresh();
            
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label id = (e.Row.FindControl("ID") as Label);

                int itemId;
                int.TryParse(id.Text, out itemId);
                Rank_Articles paramId = (from item in ratingDB.Rank_Articles where item.Active == true && item.ID == itemId select item).FirstOrDefault();

                DropDownList ddlPoint = (e.Row.FindControl("ddlPoint") as DropDownList);
                List<Rank_DifficaltPoint> points = (from item in ratingDB.Rank_DifficaltPoint where item.Active == true && item.fk_parametr == paramId.FK_parametr select item).ToList();
                Dictionary<int, string>  dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (Rank_DifficaltPoint item in points)
                    dictionary.Add(item.ID, item.Name);

                ddlPoint.DataSource = dictionary;
                ddlPoint.DataTextField = "Value";
                ddlPoint.DataValueField = "Key";
                ddlPoint.DataBind();
                string point = (e.Row.FindControl("Point") as Label).Text;
                Rank_DifficaltPoint findpoint = (from item in ratingDB.Rank_DifficaltPoint where item.Active == true && item.Name.Contains(point) select item).FirstOrDefault();
                if (findpoint != null)
                {
                    //ddlPoint.Items.FindByValue(findpoint.ID.ToString()).Selected = true;
                }
                else
                {
                    ddlPoint.SelectedIndex = 0;
                }
            }

        }

        protected void ddlPoint_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
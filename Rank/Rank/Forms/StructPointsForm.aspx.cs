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
            var userId = Session["UserID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = (int)userId;
            Refresh();
            List<Rank_Parametrs> allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true select a).ToList();
            foreach (var PAR in allparam)
            {
                List<Rank_Articles> userparamarticle = (from a in ratingDB.Rank_Articles
                                                  where a.Active == true && a.FK_parametr == PAR.ID
                                                  join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                                  where b.FK_User == userID && b.Active == true && b.UserConfirm == true
                                                  select a).ToList();
                foreach (var ART in userparamarticle)
                {
                    Calculate userpoints = new Calculate();
                    userpoints.CalculateStructParametrPoint(PAR.ID, ART.ID, userID);
                }
            }
        }
            protected void Refresh()
        {
            var userId = Session["UserID"];     
            int userID = (int)userId;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Point", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            
           
            List<Rank_Parametrs> allparam = (from a in ratingDB.Rank_Parametrs where a.Active == true select a).ToList();
            if (allparam != null)
            {
                Rank_StructPoints point;
                foreach (var tmp in allparam)
                {
                    UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                    if (rights.AccessLevel == 9)
                    {
                        // показатели для кфу
                        point = (from a in ratingDB.Rank_StructPoints
                                 where a.Active == true && a.FK_parametr == tmp.ID && (a.FK_firstlvl == null && a.FK_secondlvl == null && a.FK_thirdlvl == null)
                                 select a).FirstOrDefault();
                    }
                    else
                    {
                        // показатели структурного подразделения пользователя
                        point = (from a in ratingDB.Rank_StructPoints
                                 where a.Active == true && a.FK_parametr == tmp.ID &&
                                 (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                  && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                  && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                                 select a).FirstOrDefault();
                    }
                    if (point == null)
                    {
                        point = new Rank_StructPoints();
                        point.Active = true;
                        point.FK_parametr = tmp.ID;
                        point.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                        point.FK_secondlvl = rights.FK_SecondLevelSubdivisionTable;
                        point.FK_thirdlvl = rights.FK_ThirdLevelSubdivisionTable;
                        ratingDB.Rank_StructPoints.InsertOnSubmit(point);
                        ratingDB.SubmitChanges();
                    }
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = tmp.ID;
                    dataRow["Parametr"] = tmp.Name;
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
                        dataRow["Point"] = "не рассчитан";
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
                Session["parametrID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Forms/StructUserPointsForAccept.aspx");
            }
        }
        protected void AcceptButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var userId = Session["UserID"];
                int userID = (int)userId;
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                // окрасить в зеленый
                Rank_StructPoints accept = (from a in ratingDB.Rank_StructPoints where a.FK_parametr == Convert.ToInt32(button.CommandArgument) &&
                                               (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                    && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                                    && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                                            select a).FirstOrDefault();
                accept.Accept = true;
                accept.AcceptDate = DateTime.Now;
                ratingDB.SubmitChanges();
                Refresh();
            }
        }
    }
}
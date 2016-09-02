using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class FormUserPublication : System.Web.UI.Page
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
            UsersTable userTable =  (from a in ratingDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            } 
            if (!IsPostBack)
            {
                RefreshGrid();
            }
        }
        private void RefreshGrid()
        {
            DataTable dataTable = new DataTable();             
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("fio", typeof(string)));

            List<UsersTable> authorList;
            if (TextBox1.Text.Any())
            {
                authorList = (from a in ratingDB.UsersTable where a.Active == true && a.Surname.Contains(TextBox1.Text) select a).ToList();
            }
            else
            {
                authorList = (from a in ratingDB.UsersTable where a.Active == true select a).ToList();
            }
           
            foreach (UsersTable value in authorList)
            {
                DataRow dataRow = dataTable.NewRow();
           
                FirstLevelSubdivisionTable first = (from a in ratingDB.FirstLevelSubdivisionTable where a.Active == true && a.FirstLevelSubdivisionTableID == value.FK_FirstLevelSubdivisionTable select a).FirstOrDefault();
                SecondLevelSubdivisionTable second = (from a in ratingDB.SecondLevelSubdivisionTable where a.Active == true && a.SecondLevelSubdivisionTableID == value.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();
                ThirdLevelSubdivisionTable third = (from a in ratingDB.ThirdLevelSubdivisionTable where a.Active == true && a.ThirdLevelSubdivisionTableID == value.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
               
                dataRow["userid"] = value.UsersTableID;
                if (first != null)
                {
                    dataRow["firstlvl"] = first.Name;
                }
                else
                {
                    dataRow["firstlvl"] = "Нет привязки";
                }
                if (second != null)
                {
                    dataRow["secondlvl"] = second.Name;
                }
                else
                {
                    dataRow["secondlvl"] = "Нет привязки";
                }
                if (third != null)
                {
                    dataRow["thirdlvl"] = third.Name;
                }
                else
                {
                    dataRow["thirdlvl"] = "Нет привязки";
                }
                dataRow["fio"] = value.Surname + " " + value.Name + " " + value.Patronimyc;
             
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        protected void GoButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {           
                Session["edituserID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Forms/UserMainPage.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/OMRMainPage.aspx");
        }
    }
}

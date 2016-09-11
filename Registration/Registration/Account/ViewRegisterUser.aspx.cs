using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Registration.Account
{
    public partial class ViewRegisterUser : System.Web.UI.Page
    {
        UsersDBDataContext rating = new UsersDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

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
            dataTable.Columns.Add(new DataColumn("email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("position", typeof(string)));
            dataTable.Columns.Add(new DataColumn("stavka", typeof(string)));
            dataTable.Columns.Add(new DataColumn("degree", typeof(string)));
            dataTable.Columns.Add(new DataColumn("fio", typeof(string)));


            List<UsersTable> authorList = (from a in rating.UsersTable where a.Active == true select a).ToList();


            foreach (UsersTable value in authorList)
            {
                DataRow dataRow = dataTable.NewRow();

                FirstLevelSubdivisionTable first = (from a in rating.FirstLevelSubdivisionTable where a.Active == true && a.FirstLevelSubdivisionTableID == value.FK_FirstLevelSubdivisionTable select a).FirstOrDefault();
                SecondLevelSubdivisionTable second = (from a in rating.SecondLevelSubdivisionTable where a.Active == true && a.SecondLevelSubdivisionTableID == value.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();
                ThirdLevelSubdivisionTable third = (from a in rating.ThirdLevelSubdivisionTable where a.Active == true && a.ThirdLevelSubdivisionTableID == value.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
                dataRow["email"] = value.Email;
                dataRow["position"] = value.Position;
                dataRow["stavka"] = value.Stavka;
                dataRow["degree"] = value.Degree;
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

      
     
    }
    
}
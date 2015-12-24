using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.IO.Compression;

namespace PersonalPages
{
    public partial class StudentsTasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
            gridwievUpdate();
            }
        }
        protected void gridwievUpdate()
       {
           /*    Serialization UserSer = (Serialization)Session["UserID"];
             if (UserSer == null)
              {
                 Response.Redirect("~/Default.aspx");
            }

     int userID = UserSer.Id;
        */
   int userID = 1;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("WatchButton", typeof(string)));
            List<TasksTable> Tasks =
                (from a in PersonalPagesDB.TasksTable
                 join b in PersonalPagesDB.GroupsTable on a.FK_Group equals b.ID
                 join c in PersonalPagesDB.StudentsTable on b.ID equals c.FK_Group
                 where c.StudentsTableID==userID &&
                     a.Active == true && b.Active == true && c.Active == true
                 select a).ToList();
            foreach (var task in Tasks)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = task.ID;
                dataRow["Name"] = task.Name;
                dataRow["WatchButton"] = task.ID;
                dataRow["Date"] = Convert.ToString(task.Date).Remove(10);
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void WatchButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            int FileID = Convert.ToInt32(button.CommandArgument);//считываем название файла
            string DocName = (from a in PersonalPagesDB.TasksTable where a.ID == FileID select a.Task_FileName).FirstOrDefault();
            Response.Redirect(@"~/PersonalPages/Documents/" + DocName);
        }
    }
}
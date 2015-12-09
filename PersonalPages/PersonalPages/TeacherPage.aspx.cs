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
    public partial class TeacherPage : System.Web.UI.Page
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
          PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
              DataTable dataTable = new DataTable();
                        dataTable.Columns.Add(new DataColumn("ID", typeof (int)));
                        dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                        dataTable.Columns.Add(new DataColumn("WatchButton", typeof (string)));
            List<GroupsTable> Groups =
                (from a in PersonalPagesDB.GroupsTable
                 join b in PersonalPagesDB.ConnectGroup_And_Users on a.ID equals b.FK_GroupTable
                 join c in PersonalPagesDB.UsersTable on b.FK_UserTable equals c.UsersTableID where 
               //  b.FK_UserTableuserId &&
                 a.Active==true&&b.Active==true&& c.Active==true  
                 select a).ToList();            
             
          foreach (var group1 in Groups)
          {
              DataRow dataRow = dataTable.NewRow();
              dataRow["ID"] =group1.ID;
              dataRow["Name"] = group1.Name;
              dataRow["WatchButton"] = (from a in PersonalPagesDB.Personal_Documents 
                                            join b in PersonalPagesDB.GroupsTable on a.FK_Group equals b.ID where b.ID==group1.ID && a.Doc_Type==1 select a.ID).FirstOrDefault();
              dataTable.Rows.Add(dataRow);
          }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                    }
         
        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        protected void WatchButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
             PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            int FileID  = Convert.ToInt32(button.CommandArgument);//считываем название файла
            
            string DocName= (from a in PersonalPagesDB.Personal_Documents where a.ID==FileID select a.Doc_URL).FirstOrDefault();

                    Response.Redirect(@"~/PersonalPages/Documents/"+DocName);
            }
          
        

        protected void SaveFile()
        {
          //  Rank_IDClass Article_ID = (Rank_IDClass)Session["ArticleID"];
         //   int IDarticle = (int)Article_ID.ArticleID;
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Personal_Documents articleDoc = (from a in usersDB.Personal_Documents where a.Active == true select a).FirstOrDefault();
            String path = Server.MapPath(@"~/PersonalPages/Documents/");

            if (FileUpload2.PostedFiles.Count >0)
            {
                string DBLinkCombine = "";
                foreach (var file in FileUpload2.PostedFiles)
                {
                        file.SaveAs(path + articleDoc.ID.ToString() + "_" + file.FileName);
                    DBLinkCombine += articleDoc.ID.ToString() + "_" + file.FileName;
                    articleDoc.Doc_URL = DBLinkCombine;
                    articleDoc.Active = true;
                    articleDoc.Document_Name = DBLinkCombine;
                    articleDoc.FK_Group = (from a in usersDB.GroupsTable where a.Name == TextBox1.Text 
                                               && a.Active == true select a.ID).FirstOrDefault();
                    articleDoc.Doc_Type = 1;
                }
            }
            usersDB.Personal_Documents.InsertOnSubmit(articleDoc);
            usersDB.SubmitChanges();
        }      
    
    }
}
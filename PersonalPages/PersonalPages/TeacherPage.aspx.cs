using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

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
                        dataTable.Columns.Add(new DataColumn("AssessmentButton", typeof (string)));
                        dataTable.Columns.Add(new DataColumn("WatchButton", typeof (string)));
            List<GroupsTable> Groups =
                (from a in PersonalPagesDB.GroupsTables
                 join b in PersonalPagesDB.ConnectGroup_And_Users on a.ID equals b.FK_GroupTable
                 join c in PersonalPagesDB.UsersTables on b.FK_UserTable equals c.UsersTableID where 
               //  b.FK_UserTableuserId &&
                 a.Active==true&&b.Active==true&& c.Active==true  
                 select a).ToList();            
             
          foreach (var group in Groups)
          {
              DataRow dataRow = dataTable.NewRow();
              dataRow["ID"] =group.ID;
              dataRow["Name"] = group.Name;
              dataRow["AssessmentButton"] = group.ID;
              dataRow["WatchButton"] = group.ID;
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
        
        }
        protected void AssessmentButtonClick(object sender, EventArgs e)
        {

        }
        protected void SaveFile()
        {
          //  Rank_IDClass Article_ID = (Rank_IDClass)Session["ArticleID"];
         //   int IDarticle = (int)Article_ID.ArticleID;
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Personal_Document articleDoc = (from a in usersDB.Personal_Documents where a.Active == true select a).FirstOrDefault();
            String path = Server.MapPath(@"~/PersonalPages/Documents/");
            //  if (FileUpload1.HasFile)
            //  {
            if (FileUpload2.PostedFiles.Count > 1)
            {
                int AttachOK = 0;
                int AttachError = 0;
                string DBLinkCombine = "";
                foreach (var file in FileUpload2.PostedFiles)
                {
                    try
                    {
                        file.SaveAs(path + articleDoc.ID.ToString() + "_" + file.FileName);
                        if ((AttachOK + 1) != FileUpload2.PostedFiles.Count)
                        {
                            DBLinkCombine += articleDoc.ID.ToString() + "_" + file.FileName + "@";
                        }
                        else
                        {
                            DBLinkCombine += articleDoc.ID.ToString() + "_" + file.FileName;
                        }
                        AttachOK++;
                    }
                    catch (Exception ex)
                    {
                        AttachError++;
                    }
                    articleDoc.Doc_URL = DBLinkCombine;
                    usersDB.SubmitChanges();
                }
            }
            else
            {
                try
                {
                    FileUpload2.PostedFile.SaveAs(path + articleDoc.ID.ToString() + "_" + FileUpload2.FileName);
                    articleDoc.Doc_URL = articleDoc.ID.ToString() + "_" + FileUpload2.FileName;
                    usersDB.SubmitChanges();
                    // Label1.Text = "Файл загружен!";
                }
                catch (Exception ex)
                {
                    //Label1.Text = "Не удалось загрузить файл.";
                }
            }
            //    }
            // FileUpload1_Unload();
        }      
    
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class ViewArticleForm : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        public List<ValueSaveClass> ValuesList = new List<ValueSaveClass>();
        public DataValidator validator = new DataValidator();
        public class ValueSaveClass
        {
            public LinkButton Download { get; set; }
            public Label Value { get; set; }
            public int FieldId { get; set; }
            public int ArticleId { get; set; }
            public int ParamId { get; set; }
        }
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

            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
            Label1.Text = name.Name;
           
                List<Rank_Mark> marks = (from item in ratingDB.Rank_Mark where item.Active == true && item.fk_parametr == paramId select item).ToList();
            if (marks.Count == 1)
            {
                foreach (Rank_Mark a in marks)
                {
                    Label2.Text = a.Name;
                }
            }
            else
            {
                if (send.FK_mark == 1 || send.FK_mark == 6)
                {
                    if (send.FK_mark == 1)
                        Label2.Text = "- с грифом УМО - 70";
                    if (send.FK_mark == 6)
                        Label2.Text = "- с рекомендацией Ученого совета СП(Ф)  – 40";
                }
                else { 
                Rank_Mark mark = (from item in ratingDB.Rank_Mark where item.Active == true && item.ID == send.FK_mark select item).FirstOrDefault();
                    if(mark != null)
                    {
                        Label2.Text = mark.Name;
                    }
                    else
                    {
                        Label2.Text = "Нет значения";
                    }
               
            }
                }
            /*int view;
            int.TryParse(Session["showuserID"].ToString(), out view);*/

            int ruk = 0;
            object str_showuserID =  Session["showuserID"] ?? String.Empty;
            bool isSet_showuserID = int.TryParse(str_showuserID.ToString(), out ruk);

            Rank_Articles userarticles = new Rank_Articles();
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
          
            if (rights.AccessLevel == 1)
            {
                userarticles = (from a in ratingDB.Rank_Articles
                                where a.Active == true && a.Status == 1 && a.ID == article
                                join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                where b.Active == true && b.FK_User == ruk && b.UserConfirm == true && b.CreateUser == true
                                join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                where c.AccessLevel == 0
                                select a).FirstOrDefault();
            }
            if (rights.AccessLevel == 2)
            {
                userarticles = (from a in ratingDB.Rank_Articles
                                where a.Active == true && a.Status == 1 && a.ID == article
                                join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                where b.Active == true && b.FK_User == ruk && b.UserConfirm == true && b.CreateUser == true
                                join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                where c.AccessLevel == 1
                                select a).FirstOrDefault();
            }
            if (rights.AccessLevel == 4)
            {
                userarticles = (from a in ratingDB.Rank_Articles
                                where a.Active == true && a.Status == 1 && a.ID == article
                                join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                where b.Active == true && b.FK_User == ruk && b.UserConfirm == true && b.CreateUser == true
                                join c in ratingDB.UsersTable on b.FK_User equals c.UsersTableID
                                where c.AccessLevel == 2
                                select a).FirstOrDefault();
            }


            if (userarticles != null )
            {
                Button2.Visible = true;
            }
            if(rights.AccessLevel == 0)
            {
                Button2.Visible = false;
            }
            if (name.OneOrManyAuthor == true)
            {
                Refresh();
            }
            else
            {
                int edituserId = 0;
                str_showuserID = (string) Session["showuserID"] ?? String.Empty;
                isSet_showuserID = int.TryParse(str_showuserID.ToString(), out edituserId);

                if (isSet_showuserID)
                {
                   
                    int edituser = (int)edituserId;
                    UsersTable username = (from item in ratingDB.UsersTable where item.UsersTableID == edituser select item).FirstOrDefault();
                    Label3.Visible = true;
                    Label3.Text = username.Surname + " " + username.Name + " " + username.Patronimyc;
                }
            }
                TableDiv.Controls.Clear();
                TableDiv.Controls.Add(CreateNewTable());
            }
        
        protected void Refresh()
        {
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("fio", typeof(string)));
            dataTable.Columns.Add(new DataColumn("point", typeof(string)));

            List<Rank_UserArticleMappingTable> authorList = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_Article == article select a).ToList();
            foreach (Rank_UserArticleMappingTable value in authorList)
            {
                DataRow dataRow = dataTable.NewRow();
                UsersTable author = (from a in ratingDB.UsersTable where a.Active == true && a.UsersTableID == value.FK_User select a).FirstOrDefault();
                if (author.AccessLevel != 9)
                {

                    FirstLevelSubdivisionTable first = (from a in ratingDB.FirstLevelSubdivisionTable where a.Active == true && a.FirstLevelSubdivisionTableID == author.FK_FirstLevelSubdivisionTable select a).FirstOrDefault();
                    SecondLevelSubdivisionTable second = (from a in ratingDB.SecondLevelSubdivisionTable where a.Active == true && a.SecondLevelSubdivisionTableID == author.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();
                    ThirdLevelSubdivisionTable third = (from a in ratingDB.ThirdLevelSubdivisionTable where a.Active == true && a.ThirdLevelSubdivisionTableID == author.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
                    dataRow["ID"] = value.ID;
                    dataRow["userid"] = author.UsersTableID;
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
                    dataRow["fio"] = author.Surname + " " + author.Name + " " + author.Patronimyc;

                    if (value.FK_point != null)
                    {
                        dataRow["point"] = (from a in ratingDB.Rank_DifficaltPoint where a.Active == true && a.ID == value.FK_point select a.Name).FirstOrDefault().ToString();
                    }
                    else
                    {
                        dataRow["point"] = "";
                    }
                }
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

        }

        public string GetCollectedValue(int fieldId, int articleId, int paramid)
        {
            Rank_ArticleValues fk = (from a in ratingDB.Rank_ArticleValues
                                     where
                                        a.FK_Article == articleId
                                     && a.FK_Field == fieldId
                                     && a.FK_Param == paramid
                                     select a).FirstOrDefault();
            if (fk != null)
            {
                if (fk.Active == false)
                {
                    fk.Active = true;
                    ratingDB.SubmitChanges();
                }
            }
            else
            {
                Rank_Fields typeoffield = (from a in ratingDB.Rank_Fields where a.ID == fieldId where a.Active == true select a).FirstOrDefault();

                fk = new Rank_ArticleValues();
                fk.Active = true;
                fk.Value = "";
                fk.FK_Field = fieldId;
                fk.FK_Article = articleId;
                fk.FK_Param = paramid;
                ratingDB.Rank_ArticleValues.InsertOnSubmit(fk);
                ratingDB.SubmitChanges();
            }
            return fk.Value;
        }

        public Table CreateNewTable()
        {
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);
    
            Table tableToReturn = new Table();
            List<Rank_Fields> allFields = new List<Rank_Fields>();
        
                if (paramId == 5 || paramId == 6 || paramId == 7)
                {
                    Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
                    if (send.FK_mark != null)
                    {
                        allFields = (from a in ratingDB.Rank_Fields
                                     where a.Active == true && a.FK_parametr == paramId && a.FK_mark == send.FK_mark
                                     select a).ToList();
                    }
                }
                else
                {
                    allFields = (from a in ratingDB.Rank_Fields where a.Active == true && a.FK_parametr == paramId select a).ToList();
                }
            
            int maxLine = (from a in allFields select a.line).OrderByDescending(mc => mc).FirstOrDefault();
            for (int i = 0; i < maxLine + 1; i++)
            {
                TableRow newRow = new TableRow();
                TableRow headerNewRow = new TableRow();
                List<Rank_Fields> fieldsInLine = (from a in allFields where a.line == i select a).OrderBy(mc => mc.col).ToList();
                foreach (Rank_Fields field in fieldsInLine)
                {
                    TableCell newCell = new TableCell();
                    TableCell headerNewCell = new TableCell();
                    headerNewCell.Text = field.Name;
                    if (field.type.Contains("file"))
                    {
                        string value = GetCollectedValue(field.ID, article, paramId);
                        if (value == null || value == "")
                        { 

                        }
                        else
                        {
                            LinkButton filedownload = new LinkButton();
                            filedownload.Text = value;
                            filedownload.Width = field.width;
                            filedownload.Height = field.high;
                            filedownload.ID = "filedownload" + field.ID.ToString();
                            filedownload.Click += new EventHandler(filedownloadclick);
                            ValueSaveClass classTmp = new ValueSaveClass();
                            classTmp.Download = filedownload;
                            classTmp.FieldId = field.ID;
                            classTmp.ArticleId = article;
                            classTmp.ParamId = paramId;
                            ValuesList.Add(classTmp);                         
                            newCell.Controls.Add(filedownload);
                        }
                    }
                    else
                    { 
                        Label newTextBox = new Label();
                        newTextBox.Width = field.width;
                        newTextBox.Height = field.high;
                        newTextBox.ID = "textBox" + field.ID.ToString();
                        newTextBox.Text = GetCollectedValue(field.ID, article, paramId);

                        ValueSaveClass classTmp = new ValueSaveClass();
                        classTmp.Value = newTextBox;
                        classTmp.Value.Font.Bold = true;
                        classTmp.FieldId = field.ID;
                        classTmp.ArticleId = article;
                        classTmp.ParamId = paramId;

                        ValuesList.Add(classTmp);
                        newCell.Controls.Add(newTextBox);
                    }
                    newRow.Cells.Add(newCell);
                    headerNewRow.Cells.Add(headerNewCell);
                }
                tableToReturn.Rows.Add(headerNewRow);
                tableToReturn.Rows.Add(newRow);
            }
       
            return tableToReturn;
        }

        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }
        public void filedownloadclick(object sender, EventArgs e)
        {
            foreach (ValueSaveClass tmp in ValuesList)
            {
                Rank_Fields typeoffield = (from a in ratingDB.Rank_Fields where a.ID == tmp.FieldId where a.Active == true select a).FirstOrDefault();
                if (typeoffield.type.Contains("file"))
                {
                    string value = GetCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId);
                    if (value != null && value != "")
                    {
                        String path = value;
                        HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + value);
                        HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(path));
                        HttpContext.Current.Response.End();
                        Response.End();
                    }
                }
            }
        }
   
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserArticlePage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);
 
            Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
            send.Status = 2;
            ratingDB.SubmitChanges();
            Response.Redirect("~/Forms/UserArticlePage.aspx");

        }
    }
}
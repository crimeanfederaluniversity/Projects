﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class UserFillFormPage : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        public List<ValueSaveClass> ValuesList = new List<ValueSaveClass>();
        public DataValidator validator = new DataValidator();
        public class ValueSaveClass
        {
            public FileUpload File { get; set; }
            public LinkButton Download { get; set; }
            public DropDownList SelectedValue { get; set; }
            public TextBox Value { get; set; }
            public int FieldId { get; set; }
            public int ArticleId { get; set; }
            public int ParamId { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int paramId = Convert.ToInt32(Session["parametrID"]);
            var userId = Session["UserID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = (int)userId;
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            int article = Convert.ToInt32(Session["articleID"]);
            Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
                   
            Label1.Text = name.Name;             
            if (!IsPostBack)
            {
                if ((rights.AccessLevel == 9 && name.EditUserType != 0 && name.EditUserType != 2) || (send.Status == 1 || send.Status == 2))
                {
                    SaveButton.Visible = false;
                    SendButton.Visible = false;
                    Label11.Text = "Вы находитесь в режиме просмотра данных, редактирование невозможно!";
                    Label11.Visible = true;
                }
                else
                {
                    SaveButton.Visible = true;
                    SendButton.Visible = true;
                    Label11.Text = "Вы находитесь в режиме редактирования данных!";
                    Label11.Visible = true;
                }
                if ((rights.AccessLevel != 9 && name.EditUserType != 1 && name.EditUserType != 2) || (send.Status == 1 || send.Status == 2))
                {
                    SaveButton.Visible = false;
                    SendButton.Visible = false;
                    Label11.Text = "Вы находитесь в режиме просмотра данных, редактирование невозможно!";
                    Label11.Visible = true;
                }
                else
                {
                    SaveButton.Visible = true;
                    SendButton.Visible = true;
                    Label11.Text = "Вы находитесь в режиме редактирования данных!";
                    Label11.Visible = true;
                }
                List<FirstLevelSubdivisionTable> First_stageList = (from item in ratingDB.FirstLevelSubdivisionTable where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");
                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);
                DropDownList3.DataTextField = "Value";
                DropDownList3.DataValueField = "Key";
                DropDownList3.DataSource = dictionary;
                DropDownList3.DataBind();

                List<Rank_Mark> marks = (from item in ratingDB.Rank_Mark where item.Active == true && item.fk_parametr == paramId select item).ToList();
                if (marks.Count == 1)
                {
                    DropDownList2.Visible = false;
                }
                else
                {
                    var dictionary1 = new Dictionary<int, string>();
                    dictionary1.Add(0, "Выберите значение");

                    foreach (var item in marks)
                        dictionary1.Add(item.ID, item.Name);

                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    if(send.FK_mark != null)
                    {
                        DropDownList2.SelectedValue = send.FK_mark.ToString();
                    }
                    DropDownList2.DataSource = dictionary1;
                    DropDownList2.DataBind();
                }
            }
            if (name.OneOrManyAuthor == true)
            {
                Label13.Visible = true;
                Label2.Visible = true;
                DropDownList3.Visible = true;
                Label3.Visible = true;
                DropDownList4.Visible = true;
                Label4.Visible = true;
                DropDownList5.Visible = true;
                Label12.Visible = true;
                TextBox2.Visible = true;
                NewAuthorButton.Visible = true;
                Refresh();
            }
            if (paramId == 5 || paramId == 6 || paramId == 7)
            {
                if (send.FK_mark != null)
                {
                    TableDiv.Controls.Clear();
                    TableDiv.Controls.Add(CreateNewTable());
                }
            }
            else
            {
                TableDiv.Controls.Clear();
                TableDiv.Controls.Add(CreateNewTable());
            }
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int article = Convert.ToInt32(Session["articleID"]);
            Rank_Articles mark = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
            mark.FK_mark = Convert.ToInt32(DropDownList2.SelectedItem.Value);
            ratingDB.SubmitChanges();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
        }
        #region таблица с авторами
        protected void Refresh()
        {
            int article = Convert.ToInt32(Session["articleID"]);
            int paramId = Convert.ToInt32(Session["parametrID"]);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("fio", typeof(string)));
            dataTable.Columns.Add(new DataColumn("point", typeof(string)));

            List<Rank_UserArticleMappingTable> authorList = (from a in ratingDB.Rank_UserArticleMappingTable  where a.Active == true && a.FK_Article == article  select a).ToList();
            foreach (Rank_UserArticleMappingTable value in authorList)
            {
                DataRow dataRow = dataTable.NewRow();            
                UsersTable author = (from a in ratingDB.UsersTable where a.Active == true && a.UsersTableID == value.FK_User select a).FirstOrDefault();
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
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        
        }
        #endregion table
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {           
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int paramId = Convert.ToInt32(Session["parametrID"]);
                    DropDownList ddlPoint = (e.Row.FindControl("ddlPoint") as DropDownList);
                    List<Rank_DifficaltPoint> points = (from item in ratingDB.Rank_DifficaltPoint where item.Active == true && item.fk_parametr == paramId select item).ToList();
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(0, "Выберите значение");

                    foreach (var item in points)
                        dictionary.Add(item.ID, item.Name);
                    ddlPoint.DataSource = dictionary;
                    ddlPoint.DataTextField = "Value";
                    ddlPoint.DataValueField = "Key";
                    ddlPoint.DataBind();
                    string point = (e.Row.FindControl("point") as Label).Text;
                if (point != null && point != "")
                {
                    ddlPoint.Items.FindByText(point).Selected = true;
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
        public void SaveCollectedValue(int fieldId, int articleId, int paramid, string value)
        {
            Rank_Fields typeoffield = (from a in ratingDB.Rank_Fields where a.ID == fieldId where a.Active == true select a).FirstOrDefault();
         
                Rank_ArticleValues fk = (from a in ratingDB.Rank_ArticleValues
                                         where a.FK_Article == articleId && a.FK_Field == fieldId
                                        && a.FK_Param == paramid
                                         select a).FirstOrDefault();

                fk.Value = value;
                ratingDB.SubmitChanges();
                            
        }
    
        public Table CreateNewTable()
        {
            int article = Convert.ToInt32(Session["articleID"]);
            int paramId = Convert.ToInt32(Session["parametrID"]);
            var userId = Session["UserID"];           
            int userID = (int)userId;
            Table tableToReturn = new Table();
            List<Rank_Fields> allFields = new List<Rank_Fields>();
           
                UsersTable right = (from a in ratingDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
           if (right.AccessLevel == 9)
            {
                    allFields = (from a in ratingDB.Rank_Fields where a.Active == true && a.FK_parametr == paramId  select a).ToList();
            }  
           else
           {
                if (paramId == 5 || paramId == 6 || paramId == 7)
                {
                    Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
                    if (send.FK_mark != null)
                    {
                        allFields = (from a in ratingDB.Rank_Fields  where a.Active == true && a.FK_parametr == paramId   && a.FK_mark == send.FK_mark
                                     select a).ToList();
                    }
                }
                else
                {
                    allFields = (from a in ratingDB.Rank_Fields where a.Active == true && a.FK_parametr == paramId   select a).ToList();
                }         
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

                    if (field.type.Contains("file") )
                    {                      
                        string value = GetCollectedValue(field.ID, article, paramId);
                        
                            if (value != null && value != "")
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
                           //     newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.ID, "filedownload" + field.ID.ToString(), field.type));
                                newCell.Controls.Add(filedownload);                         
                        }
                        else
                        {
                            if (Label11.Text == "Вы находитесь в режиме редактирования данных!")
                            {
                                FileUpload filename = new FileUpload();
                                filename.Width = field.width;
                                filename.Height = field.high;
                                filename.ID = "fileupload" + field.ID.ToString();

                                ValueSaveClass classTmp = new ValueSaveClass();
                                classTmp.File = filename;
                                classTmp.FieldId = field.ID;
                                classTmp.ArticleId = article;
                                classTmp.ParamId = paramId;

                                ValuesList.Add(classTmp);
                         //       newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.ID, "fileupload" + field.ID.ToString(), field.type));
                                newCell.Controls.Add(filename);
                            }
                        }                    
                    }
                    if (field.type.Contains("drop"))
                    {
                        DropDownList ddList = new DropDownList();
                        ddList.Width = field.width;
                        ddList.Height = field.high;
                        ddList.ID = "dropdown" + field.ID.ToString();                      
                        string value = GetCollectedValue(field.ID, article, paramId);
                        List<Rank_DropDownValues> values = (from a in ratingDB.Rank_DropDownValues where a.Active == true
                                                                               join b in ratingDB.Rank_DropDown on a.FK_dropdown equals b.ID
                                                                               where b.Active == true && b.ID == field.FK_dropdown select a).ToList();
                        foreach (Rank_DropDownValues tmp in values)
                        {
                         ddList.Items.Add(new ListItem() { Value = tmp.Name, Text = tmp.Name });
                            if (value != null && value != "")
                            {
                                ddList.SelectedValue = value;
                            }
                        }
                         ValueSaveClass classTmp = new ValueSaveClass();
                         classTmp.SelectedValue = ddList;
                         classTmp.FieldId = field.ID;
                         classTmp.ArticleId = article;
                         classTmp.ParamId = paramId;
                         ValuesList.Add(classTmp);
                         newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.ID, "dropdown" + field.ID.ToString(), field.type));
                         newCell.Controls.Add(ddList);
                        }
                    if (field.type.Contains("string") || field.type.Contains("int") || field.type.Contains("date"))
                    {
                        TextBox newTextBox = validator.GetTextBox(field.type);
                        newTextBox.Width = field.width;
                        newTextBox.Height = field.high;
                        newTextBox.ID = "textBox" + field.ID.ToString();
                        newTextBox.Text = GetCollectedValue(field.ID, article, paramId);

                        ValueSaveClass classTmp = new ValueSaveClass();
                        classTmp.Value = newTextBox;
                        classTmp.FieldId = field.ID;
                        classTmp.ArticleId = article;
                        classTmp.ParamId = paramId;

                        ValuesList.Add(classTmp);

                        newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.ID, "textBox" + field.ID.ToString(), field.type));
                        newCell.Controls.Add(newTextBox);
                    }
                    newRow.Cells.Add(newCell);
                    headerNewRow.Cells.Add(headerNewCell);
                }
                tableToReturn.Rows.Add(headerNewRow);
                tableToReturn.Rows.Add(newRow);
            }
            Session["contractvalues"] = ValuesList;
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
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + value );
                        HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(path));
                        HttpContext.Current.Response.End();
                        Response.End();
                    }
                }
            }
            
        }
        public void SaveAll()
        {
            foreach (ValueSaveClass tmp in ValuesList)
            {
                Rank_Fields typeoffield = (from a in ratingDB.Rank_Fields where a.ID == tmp.FieldId where a.Active == true select a).FirstOrDefault();
                if (typeoffield.type.Contains("string") || typeoffield.type.Contains("int") || typeoffield.type.Contains("date"))
                {
                    SaveCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId, tmp.Value.Text);
                }
                if (typeoffield.FK_dropdown != null)
                {
                    SaveCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId, tmp.SelectedValue.SelectedValue);
                }
                if (typeoffield.type.Contains("file"))
                {
                    String path = Server.MapPath("~/userdocs");
                    Directory.CreateDirectory(path + "\\\\" + tmp.ArticleId.ToString());
                    if (tmp.File.HasFile)
                    {
                        tmp.File.PostedFile.SaveAs(path + "\\\\" + tmp.ArticleId.ToString() + "\\\\" +  tmp.File.FileName);
                        SaveCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId, path + "\\\\" + tmp.ArticleId.ToString() + "\\\\" +   tmp.File.FileName);
                    }
                }
            }              
        }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {          
            int SelectedValue = -1;
            if (int.TryParse(DropDownList3.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in ratingDB.SecondLevelSubdivisionTable 
                                                                      where item.Active == true && item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList4.Enabled = true;
                    DropDownList4.DataTextField = "Value";
                    DropDownList4.DataValueField = "Key";
                    DropDownList4.DataSource = dictionary;
                    DropDownList4.DataBind();
                }
            }
        }
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedValue = -1;
            if (int.TryParse(DropDownList4.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<ThirdLevelSubdivisionTable> third_stage = (from item in ratingDB.ThirdLevelSubdivisionTable
                                                                where item.Active == true && item.FK_SecondLevelSubdivisionTable == SelectedValue
                                                                select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();
                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList5.Enabled = true;
                    DropDownList5.DataTextField = "Value";
                    DropDownList5.DataValueField = "Key";
                    DropDownList5.DataSource = dictionary;
                    DropDownList5.DataBind();
                }
            }
        }
        protected void SaveButtonClick(object sender, EventArgs e)
        {
            SaveAll();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
            int paramId = Convert.ToInt32(Session["parametrID"]);
            var userId = Session["UserID"];         
            int userID = (int)userId;
            List<Rank_Mark> marks = (from item in ratingDB.Rank_Mark where item.Active == true && item.fk_parametr == paramId select item).ToList();
            if (marks.Count == 1)
            {
                int article = Convert.ToInt32(Session["articleID"]);
                Rank_Articles mark = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
                foreach(var a in marks)
                {
                    mark.FK_mark = a.ID;
                    ratingDB.SubmitChanges();
                }             
            }
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            if (name.OneOrManyAuthor == false) // если это единичная привязка
            {
                UsersTable rights = (from item in ratingDB.UsersTable where item.Active == true && item.UsersTableID == userID select item).FirstOrDefault();
              
                int article = Convert.ToInt32(Session["articleID"]);
                Rank_UserArticleMappingTable newValue = new Rank_UserArticleMappingTable();
                newValue.Active = true;
                newValue.FK_Article = article;
                if (rights.AccessLevel == 9 && name.EditUserType == 0) // если это вводит ОМР
                {
                    var editID = Session["newuserID"]; // вытаскиваем айди юзера из сессии и делаем связь
                    if (editID != null)
                    {
                        int edituserid = (int)editID; 
                        newValue.FK_User = edituserid;
                    }
                }
                else // если это сам юзер делаем связь по сессии юзера
                {
                    newValue.FK_User = userID;
                }                  
                newValue.UserConfirm = false;
                ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newValue);
                ratingDB.SubmitChanges();
            }
                Response.Redirect("~/Forms/UserArticlePage.aspx");
        }
        protected void PoiskRefresh()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("surname", typeof(string)));
            dataTable.Columns.Add(new DataColumn("name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("patronimyc", typeof(string)));
            dataTable.Columns.Add(new DataColumn("position", typeof(string)));
            List<UsersTable> poisk = (from a in ratingDB.UsersTable
                                      where a.Active == true && (a.FK_FirstLevelSubdivisionTable == Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value) || a.FK_FirstLevelSubdivisionTable == null) &&
                                      (a.FK_SecondLevelSubdivisionTable == Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value) || a.FK_SecondLevelSubdivisionTable == null) &&
                                      (a.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList5.Items[DropDownList5.SelectedIndex].Value) || a.FK_ThirdLevelSubdivisionTable == null)
                                      && a.Surname.Contains(TextBox2.Text)
                                      select a).ToList();
            if (poisk != null && poisk.Count != 0)
            {
                foreach (UsersTable author in poisk)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["userid"] = author.UsersTableID;
                    dataRow["surname"] = author.Surname;
                    dataRow["name"] = author.Name;
                    dataRow["patronimyc"] = author.Patronimyc;
                    dataRow["position"] = author.Position;
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                searchError.Visible = true;
            }
            GridView2.DataSource = dataTable;
            GridView2.DataBind();
        }
        protected void NewAuthorButtonClick(object sender, EventArgs e)
        {
            SaveAll();
            if (TextBox2.Text != null && DropDownList3.SelectedIndex != 0)
            {
                int article = Convert.ToInt32(Session["articleID"]);
                TableDiv.Controls.Clear();
                TableDiv.Controls.Add(CreateNewTable());
                searchError.Visible = false;
                PoiskRefresh();
            } 
        }
        protected void AddAutorButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int article = Convert.ToInt32(Session["articleID"]);
            Rank_UserArticleMappingTable newValue = new Rank_UserArticleMappingTable();
            newValue.Active = true;
            newValue.FK_Article = article;
            newValue.FK_User = Convert.ToInt32(button.CommandArgument);
            newValue.UserConfirm = false;
            ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newValue);
            ratingDB.SubmitChanges();
            Refresh();
        }
        protected void RankPointSaveButtonClik(object sender, EventArgs e)
        {            
            Button button = (Button)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            DropDownList drop = (DropDownList)row.FindControl("ddlPoint");
            
            int article = Convert.ToInt32(Session["articleID"]);
            Rank_UserArticleMappingTable savepoint = (from a in ratingDB.Rank_UserArticleMappingTable
                                                   where a.Active == true && a.FK_Article == article && a.FK_User == Convert.ToInt32(button.CommandArgument)
                                                   select a).FirstOrDefault();
            int fkpoint = 0;
            Int32.TryParse(drop.SelectedValue,out  fkpoint);
            if(fkpoint != 0)
            {
                savepoint.FK_point = fkpoint;
                ratingDB.SubmitChanges();
            }
            else
            {
                savepoint.FK_point = null;
                ratingDB.SubmitChanges();
            }
           
            Refresh();
        }     
        protected void Rank_DeleteAutorButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int article = Convert.ToInt32(Session["articleID"]);
            Rank_UserArticleMappingTable delete = (from a in ratingDB.Rank_UserArticleMappingTable
                                                   where a.Active == true && a.FK_Article == article && a.FK_User == Convert.ToInt32(button.CommandArgument)
                                                   select a).FirstOrDefault();
            if (delete != null)
            {
                delete.Active = false;
                ratingDB.SubmitChanges();
            }
            Refresh();
        }   
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserArticlePage.aspx");
        }

        protected void SendButtonClick(object sender, EventArgs e)
        {
            int article = Convert.ToInt32(Session["articleID"]);
            Rank_Articles send = (from a in ratingDB.Rank_Articles
                                  where a.Active == true && a.ID == article 
                                  select a).FirstOrDefault();
            send.Status = 1;
            ratingDB.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Отправлено на утверждение заведующему Вашего структурного подразделения!');", true);
        }

        protected void AddNotSystemUserButtonClick(object sender, EventArgs e)
        {
            if(CheckBox1.Checked)
            {

            }
            else
            {

            }
        }

       
    }
}
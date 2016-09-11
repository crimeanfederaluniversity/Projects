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
            int paramId = 0;
            object str_parametrID = Session["parametrID"] ?? String.Empty;
            bool isSet_parametrIDSet = int.TryParse(str_parametrID.ToString(), out paramId);

            int userId = 0;
            object str_UserID = Session["UserID"] ?? String.Empty;
            bool isSet_UserIDSet = int.TryParse(str_UserID.ToString(), out userId);

            if (!isSet_UserIDSet)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = (int)userId;

            int article = 0;
            object str_articleID = Session["articleID"] ?? String.Empty;
            bool isSet_articleIDSet = int.TryParse(str_articleID.ToString(), out article);

            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs join a in ratingDB.Rank_Articles on item.ID equals a.FK_parametr
                                   where a.ID == article
                                   select item).FirstOrDefault();
            Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();

            Label1.Text = name.Name;

            int viewuser = 0;
            object str_showuserID = Session["showuserID"] ?? String.Empty;
            bool isSet_showuserIDSet = int.TryParse(str_showuserID.ToString(), out viewuser);

            if (!Page.IsPostBack)
            {
                #region Коэффициент сложности          
                List<Rank_DifficaltPoint> points = (from item in ratingDB.Rank_DifficaltPoint where item.Active == true && item.fk_parametr == paramId select item).ToList();
                if (points.Count == 1)
                {
                    DropDownList6.Visible = false;
                    ddlPoint.Visible = false;
                }
                var dictionary2 = new Dictionary<int, string>();
                dictionary2.Add(0, "Выберите коэффициент сложности");
                foreach (var item in points)
                    dictionary2.Add(item.ID, item.Name);
                DropDownList6.DataSource = dictionary2;
                DropDownList6.DataTextField = "Value";
                DropDownList6.DataValueField = "Key";
                DropDownList6.DataBind();

                ddlPoint.DataSource = dictionary2;
                ddlPoint.DataTextField = "Value";
                ddlPoint.DataValueField = "Key";
                ddlPoint.DataBind();
                #endregion
                #region Баллы
                List<Rank_Mark> marks = (from item in ratingDB.Rank_Mark where item.Active == true && item.fk_parametr == paramId select item).ToList();
                if (marks.Count == 1 || paramId == 1 || paramId == 2)
                {
                    DropDownList2.Visible = false;
                    Label17.Visible = true;
                    if (paramId == 1)
                    {
                        Label17.Text = "- с грифом УМО - 70";
                    }
                    if (paramId == 2)
                    {
                        Label17.Text = "- гриф УМС СП(Ф)  – 40";
                    }
                    if (marks.Count == 1)
                    {
                        foreach (var a in marks)
                        {
                            Label17.Text = a.Name;
                        }
                    }
                }
                else
                {
                    var dictionary1 = new Dictionary<int, string>(); dictionary1.Add(0, "Выберите значение");
                    foreach (var item in marks) dictionary1.Add(item.ID, item.Name);
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    if (send != null && send.FK_mark != null)
                    {
                        DropDownList2.SelectedValue = send.FK_mark.ToString();
                    }
                    DropDownList2.DataSource = dictionary1;
                    DropDownList2.DataBind();
                }
                #endregion

                List<FirstLevelSubdivisionTable> First_stageList = (from item in ratingDB.FirstLevelSubdivisionTable where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите академию");
                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);
                DropDownList3.DataTextField = "Value";
                DropDownList3.DataValueField = "Key";
                DropDownList3.DataSource = dictionary;
                DropDownList3.DataBind();

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
            List<Rank_UserArticleMappingTable> authorList = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_Article == article
                                                             join b in ratingDB.UsersTable on a.FK_User equals b.UsersTableID where a.Active == true && b.AccessLevel != 9 && b.AccessLevel != 10 select a).ToList();
            if (name.OneOrManyAuthor == false && authorList.Count>=1)
            {
                Label13.Visible = false;
                Label2.Visible = false;
                Label14.Visible = false;
                Label15.Visible = false;
                Label16.Visible = false;
                DropDownList3.Visible = false;
                TextBox3.Visible = false;
                DropDownList4.Visible = false;      
                DropDownList5.Visible = false;
                DropDownList6.Visible = false;
                ddlPoint.Visible = false;
                Label12.Visible = false;
                TextBox2.Visible = false;
                NewAuthorButton.Visible = false;
                AddNotSystemUserButton.Visible = false;
                Refresh();
                NotSystmAuthorRefresh();
            }
            Refresh();
          
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleIDSet = int.TryParse(str_articleID.ToString(), out article);

            Rank_Articles mark = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
            if(DropDownList2.SelectedIndex != 0)
            {
                int value = 0;
                object str_ddl2Value =  Session["ddl2Value"] ?? String.Empty;
                bool isSet_ddl2ValueSet = int.TryParse(str_ddl2Value.ToString(), out value);

                mark.FK_mark = value;
            }
            ratingDB.SubmitChanges();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
        }
        #region таблица с авторами
        protected void Refresh()
        {
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleIDSet = int.TryParse(str_articleID.ToString(), out article);

            int paramId = 0;
            object sparametrID = Session["parametrID"] ?? String.Empty;
            bool isSet_parametrIDSet = int.TryParse(sparametrID.ToString(), out paramId);

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
                UsersTable author = (from a in ratingDB.UsersTable where a.Active == true && a.UsersTableID == value.FK_User select a).FirstOrDefault();
                if (author.AccessLevel == 9 || author.AccessLevel == 10)
                {

                }
                else { 

                    DataRow dataRow = dataTable.NewRow();
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
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        
        }
        #endregion table


        protected void SaveDropDownjChange(object sender, EventArgs e)
        {
            SaveAll();
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleIDSet = int.TryParse(str_articleID.ToString(), out article);

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserIDSet = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;

            Rank_UserArticleMappingTable user = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_Article == article && a.FK_User == userID select a).FirstOrDefault();

            DropDownList point = (DropDownList)sender;

            int selectedValue = 0;
            object str_selectedValue =  point.SelectedValue ?? String.Empty;
            bool isSet_selectedValueSet = int.TryParse(str_selectedValue.ToString(), out selectedValue);

            user.FK_point = selectedValue;

            ratingDB.SubmitChanges();

            Response.Redirect("~/Forms/CreateEditForm.aspx");
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int article = 0;
                object str_articleID =  Session["articleID"] ?? String.Empty;
                bool isSet_articleIDSet = int.TryParse(str_articleID.ToString(), out article);

                int paramId = 0;
                object str_parametrID =  Session["parametrID"] ?? String.Empty;
                bool isSet_parametrIDSet = int.TryParse(str_parametrID.ToString(), out paramId);

                Label userid = (e.Row.FindControl("userid") as Label);
                Label nopoint = (e.Row.FindControl("point") as Label);
                DropDownList ddlPoint = (e.Row.FindControl("ddlPoint") as DropDownList);

                int userId = 0;
                object str_userId = userid.Text ?? String.Empty;
                bool isSet_userIdSet = int.TryParse(str_userId.ToString(), out userId);

                Rank_UserArticleMappingTable point = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_Article == article && a.FK_User == userId select a).FirstOrDefault();
                if (point.FK_point == null)
                {
                    
                    ddlPoint.Visible = true;
                    ddlPoint.AutoPostBack = true;
                    ddlPoint.SelectedIndexChanged += SaveDropDownjChange;
                    List<Rank_DifficaltPoint> points = (from item in ratingDB.Rank_DifficaltPoint where item.Active == true && item.fk_parametr == paramId select item).ToList();
                    ddlPoint.ID = "dropDownId" + point.ID;
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(0, "Выберите значение");

                    foreach (var item in points)
                        dictionary.Add(item.ID, item.Name);
                    ddlPoint.DataSource = dictionary;
                    ddlPoint.DataTextField = "Value";
                    ddlPoint.DataValueField = "Key";
                    ddlPoint.DataBind();
           
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
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;
            Table tableToReturn = new Table();
            List<Rank_Fields> allFields = new List<Rank_Fields>();

            if (paramId == 5 || paramId == 6 || paramId == 7)
            {
                Rank_Articles send = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
                if (send.FK_mark != null)
                {
                    allFields = (from a in ratingDB.Rank_Fields  where a.Active == true && a.FK_parametr == paramId  && ( a.FK_mark == send.FK_mark || a.FK_mark == null)
                                    select a).ToList();
                }
            }
            else
            {
                allFields = (from a in ratingDB.Rank_Fields where a.Active == true && a.FK_parametr == paramId   select a).ToList();
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
                        if (value == null || value == "" ) 
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
                                //     newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.ID, "filedownload" + field.ID.ToString(), field.type));
                                newCell.Controls.Add(filedownload);                           
                        }                                    
                    }
                    if (field.type.Contains("drop"))
                    {
                        DropDownList ddList = new DropDownList();
                        ddList.Width = field.width;
                        ddList.Height = field.high;
                        ddList.ID = "dropdown" + field.ID.ToString();
                        string value = GetCollectedValue(field.ID, article, paramId);
                        List<Rank_DropDownValues> values = (from a in ratingDB.Rank_DropDownValues
                                                            where a.Active == true
                                                            join b in ratingDB.Rank_DropDown on a.FK_dropdown equals b.ID
                                                            where b.Active == true && b.ID == field.FK_dropdown
                                                            select a).ToList();
                        foreach (Rank_DropDownValues tmp in values)
                        {
                            ddList.Items.Add(new ListItem() { Value = tmp.Name, Text = tmp.Name });
                            if (value != null || value != "")
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
                    if (field.type.Contains("date"))
                    {                       
                        TextBox textBoxToReturn = new TextBox();
                        textBoxToReturn.Width = field.width;
                        textBoxToReturn.Height = field.high;
                        textBoxToReturn.ID = "date" + field.ID.ToString();
                        textBoxToReturn.Text = GetCollectedValue(field.ID, article, paramId);
                         
                       textBoxToReturn.Attributes.Add("onfocus", "this.select();lcs(this)");
                       textBoxToReturn.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");
                                 
                        ValueSaveClass classTmp = new ValueSaveClass();
                        classTmp.Value = textBoxToReturn;
                        classTmp.FieldId = field.ID;
                        classTmp.ArticleId = article;
                        classTmp.ParamId = paramId;
                        ValuesList.Add(classTmp);
                     //   newCell.Controls.Add(validator.GetRangeValidator("RangeValidator" + field.ID, "dropdown" + field.ID.ToString(), field.type));
                        newCell.Controls.Add(textBoxToReturn);
                    }

                    if (field.type.Contains("string") || field.type.Contains("int") )
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
                  
                        string value = GetCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId);
                        if (value == null || value == "")
                        {
                            if (tmp.File.HasFile)
                            {
                                String path = Server.MapPath("~/userdocs");
                                Directory.CreateDirectory(path + "\\\\" + tmp.ArticleId.ToString());
                                tmp.File.PostedFile.SaveAs(path + "\\\\" + tmp.ArticleId.ToString() + "\\\\" + tmp.File.FileName);
                                SaveCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId, path + "\\\\" + tmp.ArticleId.ToString() + "\\\\" + tmp.File.FileName);
                            }
                        
                    }
                }
            }              
        }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveAll();
            int SelectedValue = -1;
            if (int.TryParse(DropDownList3.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in ratingDB.SecondLevelSubdivisionTable 
                                                                      where item.Active == true && item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите факультет");
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
                    dictionary.Add(-1, "Выберите кафедру");
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
            Calculate userpoints = new Calculate();
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());

            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;
            UsersTable rights = (from item in ratingDB.UsersTable where item.Active == true && item.UsersTableID == userID select item).FirstOrDefault();
            List<Rank_Mark> marks = (from item in ratingDB.Rank_Mark where item.Active == true && item.fk_parametr == paramId select item).ToList();

            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            Rank_Articles mark = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == article select a).FirstOrDefault();
        
            if ((paramId == 1 || paramId == 2) && (rights.AccessLevel != 9))
            { 
                 if(paramId == 1)
                {
                    mark.FK_mark = 1;
                    ratingDB.SubmitChanges();
                }
                if (paramId == 2)
                {
                    mark.FK_mark = 6;
                    ratingDB.SubmitChanges();
                }
            }           
            if (marks.Count == 1)
            {           
                foreach(var a in marks)
                {
                    mark.FK_mark = a.ID;
                    ratingDB.SubmitChanges();
                }             
            }
            List<Rank_UserArticleMappingTable> authorList = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_Article == article select a).ToList();
            foreach(var a in authorList)
            {
                userpoints.CalculateUserArticlePoint(paramId, article, a.FK_User.Value);
            }        
            Response.Redirect("~/Forms/CreateEditForm.aspx");
        }
        protected void PoiskRefresh()
        { 
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("surname", typeof(string)));
            dataTable.Columns.Add(new DataColumn("name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("patronimyc", typeof(string)));
            dataTable.Columns.Add(new DataColumn("position", typeof(string)));
            List<UsersTable> poisk = new List<UsersTable>();
            if (TextBox2.Text != "")
            {
                int firstLevelSubdivisionTable;
                int.TryParse(DropDownList3.Items[DropDownList3.SelectedIndex].Value, out firstLevelSubdivisionTable);

                int secondLevelSubdivisionTable;
                int.TryParse(DropDownList4.Items[DropDownList4.SelectedIndex].Value, out secondLevelSubdivisionTable);

                int thirdLevelSubdivisionTable;
                int.TryParse(DropDownList5.Items[DropDownList5.SelectedIndex].Value, out thirdLevelSubdivisionTable);

                poisk = (from a in ratingDB.UsersTable
                                          where a.Active == true && (a.FK_FirstLevelSubdivisionTable == firstLevelSubdivisionTable || a.FK_FirstLevelSubdivisionTable == null) &&
                                          (a.FK_SecondLevelSubdivisionTable == secondLevelSubdivisionTable || a.FK_SecondLevelSubdivisionTable == null) &&
                                          (a.FK_ThirdLevelSubdivisionTable == thirdLevelSubdivisionTable || a.FK_ThirdLevelSubdivisionTable == null)
                                          && a.Surname.Contains(TextBox2.Text)
                                          select a).ToList();
            }
         
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
                if (TextBox2.Text != "")
                {
                    searchError.Visible = true;
                }
            }
            GridView2.DataSource = dataTable;
            GridView2.DataBind();
        }
        protected void NotSystmAuthorRefresh()
        {
            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("notsystemuserid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("notsystemuserfio", typeof(string)));
            dataTable.Columns.Add(new DataColumn("notsystemuserpoint", typeof(string)));
   
            List<Rank_NotSystemAuthors> poisk = (from a in ratingDB.Rank_NotSystemAuthors
                                                 where a.Active == true &&  a.FK_Article == article select a).ToList();
            if (poisk != null && poisk.Count != 0)
            {
                foreach (var author in poisk)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["notsystemuserid"] = author.ID;
                    dataRow["notsystemuserfio"] = author.FIO;
                    if (author.FK_Point != null)
                    {
                        dataRow["notsystemuserpoint"] = (from a in ratingDB.Rank_DifficaltPoint where a.Active == true && a.ID == author.FK_Point select a.Name).FirstOrDefault().ToString();
                    }
                    else
                    {
                        dataRow["notsystemuserpoint"] = "";
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }
      
            GridView3.DataSource = dataTable;
            GridView3.DataBind();
        }
        protected void NewAuthorButtonClick(object sender, EventArgs e)
        {
            SaveAll();
            if(ddlPoint.Visible == false)
            {
                if (TextBox2.Text != null && DropDownList3.SelectedIndex != 0)
                {
                    int article = 0;
                    object str_articleID =  Session["articleID"] ?? String.Empty;
                    bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

                    TableDiv.Controls.Clear();
                    TableDiv.Controls.Add(CreateNewTable());
                    searchError.Visible = false;
                    PoiskRefresh();
                }
            }
            else
            {
                if (TextBox2.Text != null && DropDownList3.SelectedIndex != 0 && ddlPoint.SelectedIndex !=0)
                {
                    //int article = Convert.ToInt32(Session["articleID"]);
                    TableDiv.Controls.Clear();
                    TableDiv.Controls.Add(CreateNewTable());
                    searchError.Visible = false;
                    PoiskRefresh();
                }
            }
           
        }
        protected void AddAutorButtonClik(object sender, EventArgs e)
        {
            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = userId;
            Button button = (Button)sender;

            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            int btnCmdArg = 0;
            object str_btnCmdArg = button.CommandArgument ?? String.Empty;
            bool isSet_btnCmdArg = int.TryParse(str_btnCmdArg.ToString(), out btnCmdArg);

            List<Rank_UserArticleMappingTable> authorList = (from a in ratingDB.Rank_UserArticleMappingTable where a.Active == true && a.FK_Article == article select a).ToList();
            List<int> addusers = new List<int>();
            foreach(var n in authorList)
            {
                // TODO: MAYBE NOT SAVE FOR MONO
                Int32 fk_user = Convert.ToInt32(n.FK_User);
                addusers.Add(fk_user);
            }
            if (addusers.Contains(btnCmdArg))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данный пользователь уже добавлен!');", true);
            }
            else
            {
                UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userID select item).FirstOrDefault();
                Rank_UserArticleMappingTable newValue = new Rank_UserArticleMappingTable();
                newValue.Active = true;
                newValue.FK_Article = article;
                newValue.FK_User = btnCmdArg;
                if (rights.AccessLevel == 9 || rights.AccessLevel == 10)
                {
                    newValue.UserConfirm = true;
                    if(paramId == 19)
                    {
                        newValue.CreateUser = true;
                    }
                    else
                    {
                        newValue.CreateUser = false;
                    }                  
                }
                else
                {
                    newValue.UserConfirm = false;
                }

                int selectedValue;
                int.TryParse(ddlPoint.SelectedValue, out selectedValue);

                newValue.FK_point = selectedValue;
                ratingDB.Rank_UserArticleMappingTable.InsertOnSubmit(newValue);
                ratingDB.SubmitChanges();
                DropDownList3.SelectedIndex = 0;
                DropDownList4.SelectedIndex = 0;
                DropDownList5.SelectedIndex = 0;
                TextBox2.Text = "";
                PoiskRefresh();
                Refresh();
            }
        }
      
        protected void Rank_DeleteAutorButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            int btnCmdArg;
            int.TryParse(button.CommandArgument, out btnCmdArg);

            Rank_UserArticleMappingTable delete = (from a in ratingDB.Rank_UserArticleMappingTable
                                                   where a.Active == true && a.FK_Article == article && a.FK_User == btnCmdArg
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
            List<Rank_Fields> allFields = new List<Rank_Fields>();

            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            List<Rank_ArticleValues> check = (from a in ratingDB.Rank_ArticleValues where a.Active == true && a.FK_Article == article select a).ToList();
            foreach (var n in check)
            {
                if(n.Value == null || n.Value == "")
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Пожалуйста, заполните все поля!');", true);
                    break;
                }
             
            }

            int paramId = 0;
            object str_parametrID =  Session["parametrID"] ?? String.Empty;
            bool isSet_parametrID = int.TryParse(str_parametrID.ToString(), out paramId);

            int userId = 0;
            object str_UserID =  Session["UserID"] ?? String.Empty;
            bool isSet_UserID = int.TryParse(str_UserID.ToString(), out userId);

            int userID = (int)userId;
            Rank_Articles send = (from a in ratingDB.Rank_Articles
                                  where a.Active == true && a.ID == article 
                                  select a).FirstOrDefault();
            send.Status = 1;
            ratingDB.SubmitChanges();
            Calculate userpoints = new Calculate();
            userpoints.CalculateUserParametrPoint(paramId, userID);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Отправлено на утверждение руководителю Вашего структурного подразделения! Баллы показателя пересчитаны с учетом новых данных.');", true);
            Response.Redirect("~/Forms/UserArticlePage.aspx");

        }
        protected void DeleteNotSystemAutorButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int btnCmdArg;
            int.TryParse(button.CommandArgument, out btnCmdArg);

            int article = 0;
            object str_articleID =  Session["articleID"] ?? String.Empty;
            bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

            Rank_NotSystemAuthors delete = (from a in ratingDB.Rank_NotSystemAuthors
                                            where a.Active == true && a.FK_Article == article && a.ID == btnCmdArg
                                                   select a).FirstOrDefault();
            if (delete != null)
            {
                delete.Active = false;
                ratingDB.SubmitChanges();
            }
            Refresh();
        }
        protected void AddNotSystemUserButtonClick(object sender, EventArgs e)
        {
            if(DropDownList6.Visible == true)
            {
                int dd6Value;
                int.TryParse(DropDownList6.SelectedItem.Value, out dd6Value);

                if (TextBox3.Text != null && dd6Value != 0)
                {
                    int article = 0;
                    object str_articleID =  Session["articleID"] ?? String.Empty;
                    bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

                    Rank_NotSystemAuthors newnotCFUauthor = new Rank_NotSystemAuthors();
                    newnotCFUauthor.Active = true;
                    newnotCFUauthor.FK_Article = article;

                    int value;
                    int.TryParse(DropDownList6.SelectedItem.Value, out value);
                    newnotCFUauthor.FK_Point = value;
                    newnotCFUauthor.FIO = TextBox3.Text;
                    ratingDB.Rank_NotSystemAuthors.InsertOnSubmit(newnotCFUauthor);
                    ratingDB.SubmitChanges();
                    NotSystmAuthorRefresh();
                }
            }
            else
            {
                if (TextBox3.Text != null )
                {
                    int article = 0;
                    object str_articleID =  Session["articleID"] ?? String.Empty;
                    bool isSet_articleID = int.TryParse(str_articleID.ToString(), out article);

                    Rank_NotSystemAuthors newnotCFUauthor = new Rank_NotSystemAuthors();
                    newnotCFUauthor.Active = true;
                    newnotCFUauthor.FK_Article = article;

                    int value;
                    int.TryParse(DropDownList6.SelectedItem.Value, out value);
                    newnotCFUauthor.FK_Point = value;

                    newnotCFUauthor.FIO = TextBox3.Text;
                    ratingDB.Rank_NotSystemAuthors.InsertOnSubmit(newnotCFUauthor);
                    ratingDB.SubmitChanges();
                    NotSystmAuthorRefresh();
                }

            }
               
        }
      
    }
}
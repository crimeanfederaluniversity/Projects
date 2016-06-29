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
        public List<ValueSaveClass> ValuesList = new List<ValueSaveClass>();
        public DataValidator validator = new DataValidator();
        public class ValueSaveClass
        {
            public DropDownList SelectedValue { get; set; }
            public TextBox Value { get; set; }
            public int FieldId { get; set; }
            public int ArticleId { get; set; }
            public int ParamId { get; set; }
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            int paramId = Convert.ToInt32(Session["parametrID"]);
            Rank_Parametrs name = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            Label1.Text = name.Name;
            TableDiv.Controls.Clear();
            TableDiv.Controls.Add(CreateNewTable());
            Refresh();
            if (!IsPostBack)
            {
                List<FirstLevelSubdivisionTable> First_stageList = (from item in ratingDB.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");
                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);
                DropDownList3.DataTextField = "Value";
                DropDownList3.DataValueField = "Key";
                DropDownList3.DataSource = dictionary;
                DropDownList3.DataBind();
            }
            List<Rank_Mark> marks = (from item in ratingDB.Rank_Mark where item.fk_parametr == paramId select item).ToList();
            var dictionary1 = new Dictionary<int, string>();
            dictionary1.Add(0, "Выберите значение");

            foreach (var item in marks)
                dictionary1.Add(item.ID, item.Name);

            DropDownList2.DataTextField = "Value";
            DropDownList2.DataValueField = "Key";
            DropDownList2.DataSource = dictionary1;
            DropDownList2.DataBind();
        }
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

            List<Rank_UserArticleMappingTable> authorList = (from a in ratingDB.Rank_UserArticleMappingTable
                                                             where a.Active == true && a.FK_Article == article
                                                             select a).ToList();
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
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int paramId = Convert.ToInt32(Session["parametrID"]);
                DropDownList ddlPoint = (e.Row.FindControl("ddlPoint") as DropDownList);
                List<Rank_DifficaltPoint> points = (from item in ratingDB.Rank_DifficaltPoint where item.fk_parametr == paramId select item).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in points)
                    dictionary.Add(item.ID, item.Name);
                ddlPoint.DataSource = dictionary;
                ddlPoint.DataTextField = "Value";
                ddlPoint.DataValueField = "Key";
                ddlPoint.DataBind();

                //     ddlPoint.Items.Insert(0, new ListItem("Выберите коэффициент"));

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

            Table tableToReturn = new Table();
            List<Rank_Fields> allFields = (from a in ratingDB.Rank_Fields where a.Active == true && a.FK_parametr == paramId select a).ToList();
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
                    /*
                                        if (field.FK_dropdown != null)
                                        {
                                            DropDownList ddList = new DropDownList();
                                            ddList.Width = field.width;
                                            ddList.Height = field.high;
                                            ddList.ID = "dropdown" + field.ID.ToString();                      
                                            string value = GetCollectedValue(field.ID, article, paramId);
                                            if(value != null && value != "")
                                            {
                                                ddList.SelectedItem.Value = value;
                                            }

                                            List<Rank_DropDownValues> values = (from a in ratingDB.Rank_DropDownValues where a.Active == true
                                                                               join b in ratingDB.Rank_DropDown on a.FK_dropdown equals b.ID
                                                                               where b.Active == true && b.ID == field.FK_dropdown select a).ToList();
                                            foreach (Rank_DropDownValues tmp in values)
                                            {
                                                ddList.Items.Add(new ListItem() { Value = tmp.Name, Text = tmp.Name });
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
                                        else
                                        */
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
        public void SaveAll()
        {
            foreach (ValueSaveClass tmp in ValuesList)
            {
                SaveCollectedValue(tmp.FieldId, tmp.ArticleId, tmp.ParamId, tmp.Value.Text);
            }
        }
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedValue = -1;
            if (int.TryParse(DropDownList3.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in ratingDB.SecondLevelSubdivisionTable
                                                                      where item.FK_FirstLevelSubdivisionTable == SelectedValue
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
                                                                where item.FK_SecondLevelSubdivisionTable == SelectedValue
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
            Response.Redirect("~/Forms/FormUchebniki.aspx");
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
            Int32.TryParse(drop.SelectedValue, out fkpoint);
            if (fkpoint != 0)
            {
                savepoint.FK_point = fkpoint;
            }
            else
            {
                savepoint.FK_point = null;
            }
            ratingDB.SubmitChanges();
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

        }

        protected void AddNotSystemUserButtonClick(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {

            }
            else
            {

            }
        }

        
    }
}

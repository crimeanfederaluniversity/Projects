using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class OMRAcceptArticlePage : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<FirstLevelSubdivisionTable> First_stageList = (from item in ratingDB.FirstLevelSubdivisionTable where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                Dictionary<int, string> dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");
                foreach (FirstLevelSubdivisionTable item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);
                DropDownList3.DataTextField = "Value";
                DropDownList3.DataValueField = "Key";
                DropDownList3.DataSource = dictionary;
                DropDownList3.DataBind();
                /*
                List<Rank_Parametrs> paramList = (from item in ratingDB.Rank_Parametrs where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
                dictionary2.Add(0, "Выберите значение");
                foreach (Rank_Parametrs item in paramList)
                    dictionary2.Add(item.ID, item.Name);
                DropDownList6.DataTextField = "Value";
                DropDownList6.DataValueField = "Key";
                DropDownList6.DataSource = dictionary2;
                DropDownList6.DataBind();
                */
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
                    Dictionary<int, string> dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");
                    foreach (SecondLevelSubdivisionTable item in second_stageList)
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
                    Dictionary<int, string> dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите значение");
                    foreach (ThirdLevelSubdivisionTable item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList5.Enabled = true;
                    DropDownList5.DataTextField = "Value";
                    DropDownList5.DataValueField = "Key";
                    DropDownList5.DataSource = dictionary;
                    DropDownList5.DataBind();
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblColor = e.Row.FindControl("Color") as Label;
            if (lblColor != null)
            {
                if (lblColor.Text == "1") // красный 
                {
                    e.Row.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
                }
                if (lblColor.Text == "2") // желтый
                {
                    e.Row.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");
                }
                if (lblColor.Text == "3") // зеленый
                {
                    e.Row.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            PoiskRefresh();
        }
        protected void EditButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            {
                Rank_Articles name = (from a in ratingDB.Rank_Articles where a.Active == true && a.ID == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();
                Session["articleID"] = Convert.ToInt32(button.CommandArgument);
                Session["parametrID"] = Convert.ToInt32(name.FK_parametr);
                Response.Redirect("~/Forms/CreateEditForm.aspx");
            }
            {
                Session["articleID"] = Convert.ToInt32(button.CommandArgument);
                Response.Redirect("~/Forms/CreateEditForm.aspx");
            }


            int btnArg;
            int.TryParse(button.CommandArgument, out btnArg);
            Session["articleID"] = btnArg;

            Response.Redirect("~/Forms/CreateEditForm.aspx");
        }

        protected void AcceptButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int itemId = 0;
            object str_itemId = button.CommandArgument ?? String.Empty;
            bool isSet_itemId = int.TryParse(str_itemId.ToString(), out itemId);

            Rank_Articles accept = (from item in ratingDB.Rank_Articles
                                    where item.ID == itemId
            && item.Active == true
                                    select item).FirstOrDefault();
            if (accept != null)
            {
                accept.Status = 4;
                ratingDB.SubmitChanges();
                PoiskRefresh();
            }
        }
        protected void PoiskRefresh()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Parametr", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Color", typeof(string)));

            int ddl3Value = 0;
            object str_ddl3Value = DropDownList3.SelectedItem.Value ?? String.Empty;
            bool isSet_ddl3Value = int.TryParse(str_ddl3Value.ToString(), out ddl3Value);

            int ddl4Value = 0;
            object str_ddl4Value = DropDownList4.SelectedItem == null ? String.Empty : DropDownList4.SelectedItem.Value;
            bool isSet_ddl4Value = int.TryParse(str_ddl4Value.ToString(), out ddl4Value);

            int ddl5Value = 0;
            object str_ddl5Value = DropDownList5.SelectedItem == null ? String.Empty : DropDownList5.SelectedItem.Value;
            bool isSet_ddl5Value = int.TryParse(str_ddl5Value.ToString(), out ddl5Value);

           
                int fk_FirstLevelSubdivisionTable = 0;
                object str_fk_FirstLevelSubdivisionTable = DropDownList3.SelectedIndex < 0 ? String.Empty : DropDownList3.Items[DropDownList3.SelectedIndex].Value;
                int.TryParse(str_fk_FirstLevelSubdivisionTable.ToString(), out fk_FirstLevelSubdivisionTable);

                int fk_SecondLevelSubdivisionTable = 0;
                object fkr_FK_SecondLevelSubdivisionTable = DropDownList4.SelectedIndex < 0 ? String.Empty : DropDownList4.Items[DropDownList4.SelectedIndex].Value;
                int.TryParse(fkr_FK_SecondLevelSubdivisionTable.ToString(), out fk_SecondLevelSubdivisionTable);

                int fk_ThirdLevelSubdivisionTable = 0;
                object fkr_FK_ThirdLevelSubdivisionTable = DropDownList5.SelectedIndex < 0 ? String.Empty : DropDownList5.Items[DropDownList5.SelectedIndex].Value;
                int.TryParse(fkr_FK_ThirdLevelSubdivisionTable.ToString(), out fk_ThirdLevelSubdivisionTable);

                List<UsersTable> poisk = new List<UsersTable>();

                if (ddl3Value == 0 && ddl4Value ==0 && ddl5Value == 0)
                {
                    poisk = (from a in ratingDB.UsersTable where a.Active == true select a).ToList();
                }
                if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable == 0 && fk_ThirdLevelSubdivisionTable == 0)
                {
                    poisk = (from a in ratingDB.UsersTable
                             where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                             a.FK_SecondLevelSubdivisionTable == null &&
                             a.FK_ThirdLevelSubdivisionTable == null
                             select a).ToList();
                }

                if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable == 0)
                {
                    poisk = (from a in ratingDB.UsersTable
                             where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                             a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable &&
                             a.FK_ThirdLevelSubdivisionTable == null
                             select a).ToList();
                }
                if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable != 0)
                {
                    poisk = (from a in ratingDB.UsersTable
                             where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                             a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable &&
                             a.FK_ThirdLevelSubdivisionTable == fk_ThirdLevelSubdivisionTable
                             select a).ToList();
                }


                if (poisk != null && poisk.Count != 0)
                {
                    List<Rank_Articles> structarticles = new List<Rank_Articles>();
                    foreach (UsersTable tmp in poisk)
                    {

                        List<Rank_Articles> userarticles = new List<Rank_Articles>();
                        if (CheckBox1.Checked == true)
                        {
                            userarticles = (from a in ratingDB.Rank_Articles
                                            where a.Active == true && a.Status == 1
                                            join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                            where b.Active == true && b.FK_User == tmp.UsersTableID && b.UserConfirm == true && b.CreateUser == true
                                            select a).ToList();
                        }
                        else
                        {
                            userarticles = (from a in ratingDB.Rank_Articles
                                            where a.Active == true && a.Status != 0 
                                            join b in ratingDB.Rank_UserArticleMappingTable on a.ID equals b.FK_Article
                                            where b.Active == true && b.FK_User == tmp.UsersTableID && b.UserConfirm == true && b.CreateUser == true
                                            select a).ToList();
                        }

                      //  int fk_parametr = 0;
                      //  object str_fk_parametr = DropDownList6.SelectedItem.Value ?? String.Empty;
                     //   bool isSet_fk_parametr = int.TryParse(str_fk_parametr.ToString(), out fk_parametr);

                        foreach (Rank_Articles a in userarticles)
                        {
                            structarticles.Add(a);
                        }
                    }
                    if (structarticles != null && structarticles.Count != 0)
                    { 
                        foreach (Rank_Articles article in structarticles)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = article.ID;
                            Rank_Parametrs paramname = (from a in ratingDB.Rank_Parametrs where a.ID == article.FK_parametr select a).FirstOrDefault();
                            dataRow["Parametr"] = paramname.Name;
                            Rank_ArticleValues name = (from a in ratingDB.Rank_ArticleValues
                                                       where a.Active == true && a.FK_Article == article.ID
                                                       join b in ratingDB.Rank_Fields on a.FK_Field equals b.ID
                                                       where b.Active == true && b.namefield == true
                                                       select a).FirstOrDefault();

                            if (name.Value != null)
                            {
                                dataRow["Name"] = name.Value;
                            }
                            else
                            {
                                dataRow["Name"] = "Нет названия";
                            }
                            dataRow["Date"] = article.AddDate;
                            if (article.Status == 0)
                                dataRow["Status"] = "Доступно для редактирования";
                            if (article.Status == 1)
                                dataRow["Status"] = "Отправлено на рассмотрение";
                            if (article.Status == 2)
                                dataRow["Status"] = "Верифицировано руководителем";
                            if (article.Status == 3)
                                dataRow["Status"] = "Добавлено ОМР";
                            if (article.Status == 4)
                            {
                                dataRow["Color"] = 3;
                                dataRow["Status"] = "Верифицировано ОМР";
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/OMRMainPage.aspx");
        }
    }
}
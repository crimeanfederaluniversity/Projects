using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class OMRUserRatingPage : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<FirstLevelSubdivisionTable> First_stageList = (from item in ratingDB.FirstLevelSubdivisionTable where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");
                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);
                DropDownList3.DataTextField = "Value";
                DropDownList3.DataValueField = "Key";
                DropDownList3.DataSource = dictionary;
                DropDownList3.DataBind();
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            PoiskRefresh();
        }
        protected void PoiskRefresh()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("fio", typeof(string)));
            dataTable.Columns.Add(new DataColumn("firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("position", typeof(string)));
            dataTable.Columns.Add(new DataColumn("point", typeof(string)));

            if (Convert.ToInt32(DropDownList3.SelectedItem.Value) != 0)
            {
                List<UsersTable> poisk = (from a in ratingDB.UsersTable
                                          where a.Active == true && (a.FK_FirstLevelSubdivisionTable == Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value) ) &&
                                          (a.FK_SecondLevelSubdivisionTable == Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value) || a.FK_SecondLevelSubdivisionTable == null) &&
                                          (a.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList5.Items[DropDownList5.SelectedIndex].Value) || a.FK_ThirdLevelSubdivisionTable == null)
                                          select a).ToList();

                if (poisk != null && poisk.Count != 0)
                {
                    foreach (UsersTable author in poisk)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        FirstLevelSubdivisionTable first = (from a in ratingDB.FirstLevelSubdivisionTable where a.Active == true && a.FirstLevelSubdivisionTableID == author.FK_FirstLevelSubdivisionTable select a).FirstOrDefault();
                        SecondLevelSubdivisionTable second = (from a in ratingDB.SecondLevelSubdivisionTable where a.Active == true && a.SecondLevelSubdivisionTableID == author.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();
                        ThirdLevelSubdivisionTable third = (from a in ratingDB.ThirdLevelSubdivisionTable where a.Active == true && a.ThirdLevelSubdivisionTableID == author.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
                        List<Rank_UserParametrValue> userrating = (from a in ratingDB.Rank_UserParametrValue where a.Active == true && a.FK_user == author.UsersTableID select a).ToList();
                        int sum = 0;
                        foreach (var a in userrating)
                        {
                            if(a.Value.HasValue)
                            {
                                sum = sum + Convert.ToInt32(a.Value.Value);
                            }
                            
                        }

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
                        dataRow["position"] = author.Position;
                        dataRow["point"] = sum.ToString();
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
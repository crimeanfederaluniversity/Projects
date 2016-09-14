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
            if (Convert.ToInt32(DropDownList6.SelectedItem.Value) == 1)
            {
                PPS.Visible = true;
                NR.Visible = false;
            }
            else
            {
                PPS.Visible = false;
                NR.Visible = true;
            }
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
            List<UsersTable> poisk = new List<UsersTable>();

            int ddl3Value = 0;
            object str_ddl3Value = DropDownList3.SelectedItem.Value ?? String.Empty;
            bool isSet_ddl3Value = int.TryParse(str_ddl3Value.ToString(), out ddl3Value);

            int ddl4Value = 0;
            object str_ddl4Value = DropDownList4.SelectedItem == null ? String.Empty : DropDownList4.SelectedItem.Value;
            bool isSet_ddl4Value = int.TryParse(str_ddl4Value.ToString(), out ddl4Value);

            int ddl5Value = 0;
            object str_ddl5Value = DropDownList5.SelectedItem == null ? String.Empty : DropDownList5.SelectedItem.Value;
            bool isSet_ddl5Value = int.TryParse(str_ddl5Value.ToString(), out ddl5Value);

            int type = 0;
            object str_type = DropDownList6.SelectedItem.Value ?? String.Empty;
            bool isSet_type = int.TryParse(str_type.ToString(), out type);

            int PPStype = 0;
            object str_pps = DropDownList6.SelectedItem.Text ?? String.Empty;
            bool isSet_pps = int.TryParse(str_pps.ToString(), out PPStype);

            int NRtype = 0;
            object str_nr = DropDownList6.SelectedItem.Text ?? String.Empty;
            bool isSet_nr = int.TryParse(str_nr.ToString(), out NRtype);

            int fk_FirstLevelSubdivisionTable = 0;
            object str_fk_FirstLevelSubdivisionTable = DropDownList3.SelectedIndex < 0 ? String.Empty : DropDownList3.Items[DropDownList3.SelectedIndex].Value;
            int.TryParse(str_fk_FirstLevelSubdivisionTable.ToString(), out fk_FirstLevelSubdivisionTable);

            int fk_SecondLevelSubdivisionTable = 0;
            object fkr_FK_SecondLevelSubdivisionTable = DropDownList4.SelectedIndex < 0 ? String.Empty : DropDownList4.Items[DropDownList4.SelectedIndex].Value;
            int.TryParse(fkr_FK_SecondLevelSubdivisionTable.ToString(), out fk_SecondLevelSubdivisionTable);

            int fk_ThirdLevelSubdivisionTable = 0;
            object fkr_FK_ThirdLevelSubdivisionTable = DropDownList5.SelectedIndex < 0 ? String.Empty : DropDownList5.Items[DropDownList5.SelectedIndex].Value;
            int.TryParse(fkr_FK_ThirdLevelSubdivisionTable.ToString(), out fk_ThirdLevelSubdivisionTable );

            if (fk_FirstLevelSubdivisionTable == 0 && type == 0 )
            {
                poisk = (from a in ratingDB.UsersTable where a.Active == true && a.Confirmed == true select a).ToList();
            }
            // Общий рейтинг ППсников
            if (fk_FirstLevelSubdivisionTable == 0 && type == 1 && PPStype == 0)
            {
                poisk = (from a in ratingDB.UsersTable where a.Active == true && a.TypeOfPosition == true select a).ToList();
            }
            //   Общий рейтинг научников
            if (fk_FirstLevelSubdivisionTable == 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable where a.Active == true && a.TypeOfPosition == false select a).ToList();
            }
            #region Поиск общий по структуре
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable == 0  && fk_ThirdLevelSubdivisionTable == 0  && type == 0)
            {
                poisk = (from a in ratingDB.UsersTable   where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == null &&  a.FK_ThirdLevelSubdivisionTable == null  select a).ToList();
            }

            if (fk_FirstLevelSubdivisionTable != 0  && fk_SecondLevelSubdivisionTable != 0  && fk_ThirdLevelSubdivisionTable == 0  && type == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable &&   a.FK_ThirdLevelSubdivisionTable == null     select a).ToList();
            }
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable != 0  && type == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable &&   a.FK_ThirdLevelSubdivisionTable == fk_ThirdLevelSubdivisionTable  select a).ToList();
            }
            #endregion

            #region Поиск ППСников  по структуре
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable == 0 && fk_ThirdLevelSubdivisionTable == 0 && type  == 1 && PPStype == 0)
            {
                poisk = (from a in ratingDB.UsersTable where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == null &&  a.FK_ThirdLevelSubdivisionTable == null  && a.TypeOfPosition == true select a).ToList();
            }

            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 1 && PPStype == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable &&   a.FK_ThirdLevelSubdivisionTable == null   && a.TypeOfPosition == true  select a).ToList();
            }
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable != 0 && type == 1 && PPStype == 0)
            {
                poisk = (from a in ratingDB.UsersTable   where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == fk_ThirdLevelSubdivisionTable  && a.TypeOfPosition == true
                         select a).ToList();
            }
            #endregion

            #region Поиск ППСников по должностям по структуре
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable == 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 1 && PPStype != 0)
            {
                poisk = (from a in ratingDB.UsersTable   where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                            a.FK_SecondLevelSubdivisionTable == null && a.FK_ThirdLevelSubdivisionTable == null && a.TypeOfPosition == true // && a.Position == PPStype
                         select a).ToList();
            }

            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 1 && PPStype != 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == null && a.TypeOfPosition == true  // && a.Position == PPStype
                         select a).ToList();
            }
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable != 0 && type == 1 && PPStype != 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == fk_ThirdLevelSubdivisionTable && a.TypeOfPosition == true // && a.Position == PPStype
                         select a).ToList();
            }
            #endregion

            #region Поиск научников  по структуре
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable == 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == null && a.FK_ThirdLevelSubdivisionTable == null && a.TypeOfPosition == false
                         select a).ToList();
            }

            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                         a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == null && a.TypeOfPosition == false
                         select a).ToList();
            }
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable != 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == fk_ThirdLevelSubdivisionTable && a.TypeOfPosition == false
                         select a).ToList();
            }
            #endregion

            #region Поиск научников по должностям по структуре
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable == 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == null && a.FK_ThirdLevelSubdivisionTable == null && a.TypeOfPosition == false // && a.Position == NRtype.
                         select a).ToList();
            }

            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable == 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable  where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == null && a.TypeOfPosition == false // && a.Position == NRtype.
                         select a).ToList();
            }
            if (fk_FirstLevelSubdivisionTable != 0 && fk_SecondLevelSubdivisionTable != 0 && fk_ThirdLevelSubdivisionTable != 0 && type == 2 && NRtype == 0)
            {
                poisk = (from a in ratingDB.UsersTable where a.Active == true && a.FK_FirstLevelSubdivisionTable == fk_FirstLevelSubdivisionTable &&
                        a.FK_SecondLevelSubdivisionTable == fk_SecondLevelSubdivisionTable && a.FK_ThirdLevelSubdivisionTable == fk_ThirdLevelSubdivisionTable && a.TypeOfPosition == false // && a.Position == NRtype.
                         select a).ToList();
            }
            #endregion


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
                    foreach (Rank_UserParametrValue a in userrating)
                    {
                        if (a.Value.HasValue)
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
   
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/OMRMainPage.aspx");
        }
    }
}
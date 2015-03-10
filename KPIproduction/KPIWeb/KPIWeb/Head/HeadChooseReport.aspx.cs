using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace KPIWeb.Head
{
    public partial class ChooseReport : System.Web.UI.Page
    {
        public int col_ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                Label3.Visible = false;
                Label2.Visible = false;
                Label4.Visible = false;
                DropDownList1.Visible = false;
                DropDownList2.Visible = false;
                DropDownList3.Visible = false;
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
                
                List<FirstLevelSubdivisionTable> First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
                //дропдаун заполнен
                List<ReportArchiveTable> reportsArchiveTablesTable = (
                                                from a in kpiWebDataContext.UsersTable
                                                join b in kpiWebDataContext.FirstLevelSubdivisionTable
                                                on a.FK_FirstLevelSubdivisionTable equals b.FirstLevelSubdivisionTableID
                                                join c in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId
                                                join d in kpiWebDataContext.ReportArchiveTable
                                                on c.FK_ReportArchiveTableId equals d.ReportArchiveTableID
                                                where a.UsersTableID == UserSer.Id
                                                && a.Active == true
                                                && b.Active == true
                                                && c.Active == true
                                                && d.Active == true
                                              //  && d.StartDateTime < DateTime.Now
                                              //  && d.EndDateTime > DateTime.Now
                                                select d).ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("info0", typeof(string)));
                List<int> conf_enable = new List<int>() ;
                foreach (ReportArchiveTable ReportRow in reportsArchiveTablesTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                    dataRow["ReportName"] = ReportRow.Name;
                    dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];
                    dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0];
                    int calcConf=(from a in kpiWebDataContext.CollectedCalculatedParametrs
                                        where a.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                                            && a.FK_UsersTable == userID
                                            && a.Confirmed ==true select a).ToList().Count();
                    int calcAll = (from a in kpiWebDataContext.CalculatedParametrs
                                        join b in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                        on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                                        join c in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                                        on a.CalculatedParametrsID equals c.FK_CalculatedParametrsTable
                                        where b.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                                         && c.FK_UsersTable == userID
                                         && c.CanConfirm == true
                                        select a).ToList().Count();

                    dataRow["info0"] =  calcConf+" из "+ calcAll;
                    if (calcAll==calcConf)
                    {
                        conf_enable.Add(0);
                    }
                    else
                    {
                        conf_enable.Add(1);
                    }
                    dataTable.Rows.Add(dataRow);
                }
                ViewState["confEnable"] = conf_enable;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данная функция находится на стадии разработки');", true);
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization;
                Serialization modeSer = new Serialization(1, null, null);
                Session["mode"] = modeSer;
                if (CheckBox1.Checked)
                {
                    int value = 0;
                    int l_1 = 0;
                    if (DropDownList1.SelectedIndex > 0)
                        l_1  = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                    int l_2 = 0;
                    if (DropDownList2.SelectedIndex > 0)
                        l_2 = Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
                    int l_3 = 0;
                    if (DropDownList3.SelectedIndex > 0)
                        l_3 = Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value);

                    Serialization level = new Serialization(1,l_1,l_2,l_3,0, 0);
                    Session["level"] = level;
                }
                else
                {
                    Serialization level = new Serialization(0, 0, 0, 0, 0, 0);
                    Session["level"] = level;
                }

                Response.Redirect("~/Head/HeadShowReportResult.aspx");
            }
        }
        protected void ButtonConfirmClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данная функция находится на стадии разработки');", true);
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization;
                Serialization modeSer = new Serialization(2, null, null);
                Session["mode"] = modeSer;
                if (CheckBox1.Checked)
                {
                    int value = 0;
                    int l_1 = 0;
                    if (DropDownList1.SelectedIndex > 0)
                        l_1 = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                    int l_2 = 0;
                    if (DropDownList2.SelectedIndex > 0)
                        l_2 = Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
                    int l_3 = 0;
                    if (DropDownList3.SelectedIndex > 0)
                        l_3 = Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value);

                    Serialization level = new Serialization(1, l_1, l_2, l_3, 0, 0);
                    Session["level"] = level;
                }
                else
                {
                    Serialization level = new Serialization(0, 0, 0, 0, 0, 0);
                    Session["level"] = level;
                }

                Response.Redirect("~/Head/HeadShowReportResult.aspx");
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                                      where item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.Enabled = true;
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                }
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));

            int SelectedValue = -1;

            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<ThirdLevelSubdivisionTable> third_stage = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                                                                where item.FK_SecondLevelSubdivisionTable == SelectedValue
                                                                select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();

                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");

                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList3.Enabled = true;
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                Label3.Visible = true;
                Label2.Visible = true;
                Label4.Visible = true;
                DropDownList1.Visible = true;
                DropDownList2.Visible = true;
                DropDownList3.Visible = true;
            }
            else
            {
                Label3.Visible = false;
                Label2.Visible = false;
                Label4.Visible = false;
                DropDownList1.Visible = false;
                DropDownList2.Visible = false;
                DropDownList3.Visible = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Color color;
            Color confirmedColor = System.Drawing.Color.LimeGreen;
            Color disableColor = System.Drawing.Color.LightGray;
            if (col_ == 0)
            {
                col_ = 1;
                color = System.Drawing.Color.FloralWhite;
            }
            else
            {
                col_ = 0;
                color = System.Drawing.Color.GhostWhite;
            }
            e.Row.BackColor = color;
            List<int> ConfEnable = (List<int>)ViewState["confEnable"];
            var ConfBut = e.Row.FindControl("ButtonConfirmReport") as Button;
            var info0 = e.Row.FindControl("info0") as Label;
            if ((e.Row.RowIndex >= 0) && e.Row.RowIndex < ConfEnable.Count())
            {
                if (ConfEnable[e.Row.RowIndex] == 1)
                {
                    ConfBut.Enabled = true;
                }
                else
                {
                    ConfBut.Enabled = false;
                    ConfBut.BackColor = disableColor;
                    DataControlFieldCell d = ConfBut.Parent as DataControlFieldCell;
                    d.BackColor = disableColor;
                    info0.BackColor = confirmedColor;
                    DataControlFieldCell f = info0.Parent as DataControlFieldCell;
                    f.BackColor = confirmedColor;

                }
            }

        }
    }
}
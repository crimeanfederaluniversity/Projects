using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.Rector
{
    public partial class RAnalitics : System.Web.UI.Page
    {
        public class Struct // класс описываюший структурные подразделения
        {
            public int Lv_0 { get; set; }
            public int Lv_1 { get; set; }
            public int Lv_2 { get; set; }
            public int Lv_3 { get; set; }
            public int Lv_4 { get; set; }
            public int Lv_5 { get; set; }

            public string Name { get; set; }

            public Struct(int lv0, int lv1, int lv2, int lv3, int lv4, int lv5, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = lv4;
                Lv_5 = lv5;
                Name = name;
            }
            public Struct(int lv0, int lv1, int lv2, int lv3, int lv4, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = lv4;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, int lv1, int lv2, int lv3, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, int lv1, int lv2, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = 0;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, int lv1, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = 0;
                Lv_3 = 0;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, string name)
            {
                Lv_0 = lv0;
                Lv_1 = 0;
                Lv_2 = 0;
                Lv_3 = 0;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }

            List<UsersTable> ProrectorList = (from a in kpiWebDataContext.UsersTable
                                              where a.Active == true
                                              && a.AccessLevel == 5
                                              select a).ToList();
            if (Page.IsPostBack)
            {

            }
            else
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ProrectorID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ProrectorName", typeof(string)));

                foreach (UsersTable Prorector in ProrectorList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ProrectorID"] = Prorector.UsersTableID;
                    dataRow["ProrectorName"] = Prorector.Email;
                    dataTable.Rows.Add(dataRow);
                }
                GridView2.DataSource = dataTable;
                GridView2.DataBind();
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                DataTable dataTable2 = new DataTable();
                dataTable2.Columns.Add(new DataColumn("IndicatorClassID", typeof(string)));
                dataTable2.Columns.Add(new DataColumn("IndicatorClassName", typeof(string)));
                List<IndicatorClass> IndicatorClassList = (from a in kpiWebDataContext.IndicatorClass
                                                           select a).ToList();
                foreach (IndicatorClass IndicatorClass in IndicatorClassList)
                {
                    DataRow dataRow = dataTable2.NewRow();
                    dataRow["IndicatorClassID"] = IndicatorClass.IndicatorClassID;
                    dataRow["IndicatorClassName"] = IndicatorClass.ClassName;
                    dataTable2.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable2;
                GridView1.DataBind();
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                List<IndicatorsTable> IndicatorsTableList = (from a in kpiWebDataContext.IndicatorsTable
                                                             where a.Active == true
                                                             select a).ToList();
                CheckBoxList1.Items.Clear();
                foreach (IndicatorsTable currentIndicator in IndicatorsTableList)
                {
                    ListItem Item1 = new ListItem();
                    Item1.Text = currentIndicator.Name;
                    Item1.Value = currentIndicator.IndicatorsTableID.ToString();
                    CheckBoxList1.Items.Add(Item1);
                }

            }
        }
        protected void ButtonClassClick(object sender, EventArgs e) // По типам индикаторов
        {
            LinkButton button = (LinkButton)sender;
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                List<int> IndicatorList = (from a in kpiWebDataContext.IndicatorsTable
                                           where a.Active == true
                                           join b in kpiWebDataContext.IndicatorClass
                                           on a.FK_IndicatorClass equals b.IndicatorClassID
                                           where
                                           b.IndicatorClassID == Convert.ToInt32(button.CommandArgument)
                                           select a.IndicatorsTableID).Distinct().ToList();
                if (IndicatorList.Count() > 0)
                {
                    RectorChartSession RectorChart = new RectorChartSession();
                    RectorChart.IndicatorsList = IndicatorList;
                    Session["RectorChart"] = RectorChart;
                    Response.Redirect("~/Rector/RShowChart.aspx");
                }
            }
        }
        protected void ButtonProrectorClick(object sender, EventArgs e) //По проректорам
        {
            LinkButton button = (LinkButton)sender;
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                List<int> IndicatorList = (from a in kpiWebDataContext.IndicatorsTable
                                           where a.Active == true
                                           join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                           on a.IndicatorsTableID equals b.FK_IndicatorsTable
                                           where b.Active == true
                                           && b.CanConfirm == true
                                           && b.FK_UsresTable == Convert.ToInt32(button.CommandArgument)
                                           select a.IndicatorsTableID).Distinct().ToList();
                if (IndicatorList.Count() > 0)
                {
                    RectorChartSession RectorChart = new RectorChartSession();
                    RectorChart.IndicatorsList = IndicatorList;
                    Session["RectorChart"] = RectorChart;
                    Response.Redirect("~/Rector/RShowChart.aspx");
                }
            }
        } 
        protected void Button4_Click(object sender, EventArgs e) // по всем показателям
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<int> IndicatorList = (from a in kpiWebDataContext.IndicatorsTable
                                       where a.Active == true
                                       select a.IndicatorsTableID).ToList();
            RectorChartSession RectorChart = new RectorChartSession();
            RectorChart.IndicatorsList = IndicatorList;
            Session["RectorChart"] = RectorChart;
            Response.Redirect("~/Rector/RShowChart.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e) // по отмеченным в чекбоксах галочкам
        {
            List<int> IndicatorList = new List<int>();
            foreach (ListItem Item in CheckBoxList1.Items)
            {
                if (Item.Selected)
                {
                    IndicatorList.Add(Convert.ToInt32(Item.Value));
                }
            }
            if (IndicatorList.Count()>0)
            {
                RectorChartSession RectorChart = new RectorChartSession();
                RectorChart.IndicatorsList = IndicatorList;
                Session["RectorChart"] = RectorChart;
                Response.Redirect("~/Rector/RShowChart.aspx");
            }
        }
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }       
    }
}
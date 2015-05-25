using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class ReportFilling : System.Web.UI.Page
    {
        public class Struct // класс описываюший структурные подразделения
        {
            public int ReportID { get; set; }
            public string Lv_1_Name { get; set; }
            public string Lv_2_Name { get; set; }
            public string Lv_3_Name { get; set; }
            public int UserID { get; set; }

            public Struct(int ReportID_, string Lv_1_Name_, string Lv_2_Name_, string Lv_3_Name_, int UserID_)
            {
                this.ReportID = ReportID_;
                this.Lv_1_Name = Lv_1_Name_;
                this.Lv_2_Name = Lv_2_Name_;
                this.Lv_3_Name = Lv_3_Name_;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }             
       
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int ReportArchiveID;
            ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);

            DataTable DataTableStatus = new DataTable();

            DataTableStatus.Columns.Add(new DataColumn("LV_1", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("LV_2", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("LV_3", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("Status", typeof(string)));            
            DataTableStatus.Columns.Add(new DataColumn("EmailEdit", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("EmailConfirm", typeof(string)));
            /*
            (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
             where
             a.ReportArchiveAndLevelMappingTableId == ReportArchiveID
             && a.Active == true
             join b in kPiDataContext.FirstLevelSubdivisionTable
             on a.FK_FirstLevelSubmisionTableId equals b.FirstLevelSubdivisionTableID
             where b.Active == true
             join d in kPiDataContext.SecondLevelSubdivisionTable
             on b.FirstLevelSubdivisionTableID equals d.FK_FirstLevelSubdivisionTable
             where d.Active == true
             join f in kPiDataContext.ThirdLevelSubdivisionTable
             on d.SecondLevelSubdivisionTableID equals f.FK_SecondLevelSubdivisionTable
             where f.Active == true
             join c in kPiDataContext.UsersTable
             on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubdivisionTable
             where c.Active == true
             select new { a.ReportArchiveAndLevelMappingTableId, b.Name, d.Name, f.Name, c.UsersTableID }
                 ).ToList();
            */
            //foreach (IndicatorsTable indicator in indicatorTable)
            {
                DataRow dataRow = DataTableStatus.NewRow();
                dataRow["LV_1"] = "q";
                dataRow["LV_2"] = "q";
                dataRow["LV_3"] = "q";
                dataRow["Status"] = "q";
                dataRow["EmailEdit"] = "q";
                dataRow["EmailConfirm"] = "q";
                DataTableStatus.Rows.Add(dataRow);                
            }
            GridWhoOws.DataSource = DataTableStatus;
            GridWhoOws.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Reports
{
    public partial class ChooseReport : System.Web.UI.Page
    {
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

            if (userTable.AccessLevel != 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                
                ////////////
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));         
                List<ReportArchiveTable> reportsArchiveTablesTable =  (
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
                                                && d.StartDateTime < DateTime.Now 
                                                && d.EndDateTime > DateTime.Now
                                                select d).ToList();
                ///тут мы получили список активных отччетов пользователя
                /// пользователь привязан к таблице первого подразделения
                /// таблица первого подразделения привязана к таблице отчетов(через таблицу связи)
                /// на данный момент отчет можно назначать только первому подразделению!!!    
                ///                        
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                
                foreach (ReportArchiveTable ReportRow in reportsArchiveTablesTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                                dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                                dataRow["ReportName"] = ReportRow.Name;
                                dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];//только дата// время обрезается сплитом
                                dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0]; ;
                                dataTable.Rows.Add(dataRow); 
                }           
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
                ///вывели все отчеты с параметрами в гридвью          
            }
        }

        protected void ButtonEditClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета               
                Serialization modeSer = new Serialization(0,null,null);
                Session["mode"] = modeSer;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета               
                Serialization modeSer = new Serialization(1, null, null);
                Session["mode"] = modeSer;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }
        protected void ButtonConfirmClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета               
                Serialization modeSer = new Serialization(2, null, null);
                Session["mode"] = modeSer;
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/SpecializationParametrs.aspx");            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
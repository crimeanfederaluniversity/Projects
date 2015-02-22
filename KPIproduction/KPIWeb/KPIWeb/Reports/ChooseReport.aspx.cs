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
                Response.Redirect("~/Account/Login.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
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
                /// 
                ///                 
                List<ThirdLevelSubdivisionTable> ThirdLevel = (
                    from item in kpiWebDataContext.ThirdLevelSubdivisionTable
                    join b in kpiWebDataContext.UsersTable
                    on item.FK_SecondLevelSubdivisionTable equals b.FK_SecondLevelSubdivisionTable
                    where b.UsersTableID == UserSer.Id 
                    select  item).ToList();



                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (ThirdLevelSubdivisionTable item in ThirdLevel)
                    dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
            }
        }

        protected void ButtonEditClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчета
                Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
        }

        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данная функция находится на стадии разработки');", true);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList1.Items.Clear();
             KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                                                                     select a).ToList();
                int i = 0;
                foreach (SpecializationTable spec in specializationTableData)
                {
                    CheckBoxList1.Items.Add(spec.Name);
                    CheckBoxList1.Items[i].Value = spec.SpecializationTableID.ToString();
                    CheckBoxList1.Items[i].Selected = ((from a in kPiDataContext.SpecializationTable
                        join b in kPiDataContext.FourthLevelSubdivisionTable
                            on a.SpecializationTableID equals b.FK_Specialization
                        where b.FK_ThirdLevelSubdivisionTable == SelectedValue
                        && b.Active == true
                        select a).ToList().Count) > 0
                        ? true
                        : false;
                }          
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                        SpecializationTable spec = (from a in kPiDataContext.SpecializationTable
                        join b in kPiDataContext.FourthLevelSubdivisionTable
                            on a.SpecializationTableID equals b.FK_Specialization
                        where b.FK_ThirdLevelSubdivisionTable == SelectedValue
                        && b.Active == true
                        select a).ToList().Count) > 0
                }
                {
                    

                }
            }
        }
    }
}
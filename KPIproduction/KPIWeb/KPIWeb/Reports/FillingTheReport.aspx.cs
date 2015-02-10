using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;

namespace KPIWeb.Reports
{
    public partial class FillingTheReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int UserID = (int) Session["UserID"];
                int ReportID;
                int RoleID;
                string param = Session["Params"].ToString();
                string[] tmp = param.Split('_');
                ReportID = Convert.ToInt32(tmp[0]);
                RoleID = Convert.ToInt32(tmp[1]);
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

                UsersTable user = (from usersTable in KPIWebDataContext.UsersTable
                    where usersTable.UsersTableID == UserID
                    select usersTable).FirstOrDefault();

                if (user == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                //KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                //Список ID всех активных кампаний для данного пользователя
                    /* List<int> ReportArchiveIDList = (from reportArchiveTables in KPIWebDataContext.ReportArchiveTables
                                                         join reportAndRolesMappings in KPIWebDataContext.ReportAndRolesMappings on
                                                         reportArchiveTables.ReportArchiveTableID equals reportAndRolesMappings.FK_ReportArchiveTable
                                                         where reportAndRolesMappings.FK_RolesTable == user.FK_RolesTable &&
                                                         reportArchiveTables.Active == true &&
                                                         reportArchiveTables.StartDateTime < DateTime.Now &&
                                                         reportArchiveTables.EndDateTime > DateTime.Now
                                                         select reportArchiveTables.ReportArchiveTableID).ToList();

                        int currentReportArchiveID = ReportArchiveIDList.FirstOrDefault();
                      */
                    ViewState["CurrentReportArchiveID"] = ReportID;
                 

                //Список всех базовых параметров "принадлежащих" данному пользователю
                List<BasicParametersTable> basicParametersTable =
                    (from basicParametersTables in KPIWebDataContext.BasicParametersTables
                        join basicParametersAndRolesMappingTables in
                            KPIWebDataContext.BasicParametersAndRolesMappingTables on
                            basicParametersTables.BasicParametersTableID equals
                            basicParametersAndRolesMappingTables.FK_BasicParametersTable
                        where basicParametersAndRolesMappingTables.FK_RolesTable == RoleID
                        select basicParametersTables).ToList();

                //Список ранее введенных пользователем данных для данной кампании (отчета)
                List<CollectedBasicParametersTable> сollectedBasicParametersTable =
                    (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTable
                        where
                            (from item in basicParametersTable select item.BasicParametersTableID).ToList()
                                .Contains((int) collectedBasicParameters.FK_BasicParametersTable) &&
                            collectedBasicParameters.FK_UsersTable == UserID &&
                            collectedBasicParameters.FK_ReportArchiveTable == ReportID
                        select collectedBasicParameters).ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CollectedValue", typeof (string)));

                //Добавляем недостающие строки в базу CollectedBasicParametersTable с пустыми значениями в столбце "CollectedValue"
                //+ заполняем обьект dataTable
                foreach (BasicParametersTable basicParameter in basicParametersTable)
                {
                    CollectedBasicParametersTable сollectedBasicParameter = (from item in сollectedBasicParametersTable
                        where item.FK_BasicParametersTable == basicParameter.BasicParametersTableID
                        select item).FirstOrDefault();

                    DataRow dataRow = dataTable.NewRow();

                    dataRow["CurrentReportArchiveID"] = ReportID;
                    dataRow["BasicParametersTableID"] = basicParameter.BasicParametersTableID;
                    dataRow["Name"] = basicParameter.Name;

                    if (сollectedBasicParameter == null)
                    {
                        string localIP =
                            Dns.GetHostEntry(Dns.GetHostName())
                                .AddressList.Where(
                                    ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                .Select(ip => ip.ToString())
                                .FirstOrDefault() ?? "";

                        CollectedBasicParametersTable newItem = new CollectedBasicParametersTable();
                        newItem.Active = true;
                        newItem.FK_UsersTable = UserID;
                        newItem.FK_ReportArchiveTable = ReportID;
                        newItem.FK_BasicParametersTable = basicParameter.BasicParametersTableID;
                        newItem.UserIP = localIP;
                        newItem.LastChangeDateTime = DateTime.Now;

                        dataRow["CollectedBasicParametersTableID"] = string.Empty;
                        dataRow["CollectedValue"] = string.Empty;

                        KPIWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(newItem);
                    }
                    else
                    {
                        dataRow["CollectedBasicParametersTableID"] =
                            сollectedBasicParameter.CollectedBasicParametersTableID;
                        dataRow["CollectedValue"] = сollectedBasicParameter.CollectedValue;
                    }

                    dataTable.Rows.Add(dataRow);
                }

                KPIWebDataContext.SubmitChanges();

                ViewState["CollectedBasicParametersTable"] = dataTable;

                GridviewCollectedBasicParameters.DataSource = dataTable;
                GridviewCollectedBasicParameters.DataBind();
            }
        }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            StringCollection sc = new StringCollection();

            if (ViewState["CollectedBasicParametersTable"] != null && ViewState["CurrentReportArchiveID"] != null)
            {
                int currentReportArchiveID = (int)ViewState["CurrentReportArchiveID"];

                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
               // user = (UsersTable)Session["user"];

                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();

                DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];

                if (collectedBasicParametersTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= collectedBasicParametersTable.Rows.Count; i++)
                    {
                        TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("TextBoxCollectedValue");
                        Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("LabelCollectedBasicParametersTableID");

                        if (textBox != null && label != null)
                        {
                            double collectedValue = -1;
                            if (double.TryParse(textBox.Text, out collectedValue) && collectedValue > -1)
                            {
                                int collectedBasicParametersTableID = -1;
                                if (int.TryParse(label.Text, out collectedBasicParametersTableID) && collectedBasicParametersTableID > -1)
                                    tempDictionary.Add(collectedBasicParametersTableID, collectedValue);
                            }
                        }
                        rowIndex++;
                    }
                }

                if (tempDictionary.Count > 0)
                {
                    //Список ранее введенных пользователем данных для данной кампании (отчета)
                    List<CollectedBasicParametersTable> сollectedBasicParametersTable = (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTable
                                                                                         where (from item in tempDictionary select item.Key).ToList().Contains((int)collectedBasicParameters.CollectedBasicParametersTableID)
                                                                                         select collectedBasicParameters).ToList();

                    string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";

                    foreach (var сollectedBasicParameter in сollectedBasicParametersTable)
                    {
                        сollectedBasicParameter.CollectedValue = (from item in tempDictionary where item.Key == сollectedBasicParameter.CollectedBasicParametersTableID select item.Value).FirstOrDefault();
                        сollectedBasicParameter.LastChangeDateTime = DateTime.Now;
                        сollectedBasicParameter.UserIP = localIP;
                    }

                    KPIWebDataContext.SubmitChanges();

                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены');", true);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            Dictionary<int, double> tempDictionary = new Dictionary<int, double>();
            DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];
            if (collectedBasicParametersTable.Rows.Count > 0)
            {
                for (int i = 1; i <= collectedBasicParametersTable.Rows.Count; i++)
                {
                    TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("TextBoxCollectedValue");
                    Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("LabelCollectedBasicParametersTableID");

                    if (textBox != null && label != null)
                    {
                        double collectedValue = -1;
                        if (double.TryParse(textBox.Text, out collectedValue) && collectedValue > -1)
                        {
                            int collectedBasicParametersTableID = -1;
                            if (int.TryParse(label.Text, out collectedBasicParametersTableID) && collectedBasicParametersTableID > -1)
                                tempDictionary.Add(collectedBasicParametersTableID, collectedValue);
                        }
                    }
                    rowIndex++;
                }
            }
            DataTable dt = collectedBasicParametersTable;
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range rng = null;
            DataRow dRow;
            DataColumn dCol;
            for (int _row = 0; _row < dt.Rows.Count; _row++)
            {
                for (int _col = 3; _col < dt.Columns.Count; _col++)//костыль
                {
                    dRow = dt.Rows[_row];
                    dCol = dt.Columns[_col];
                    rng = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[_row + 1, _col -2]; //костыль
                    rng.Value2 = dRow[dCol.ColumnName].ToString();
                }
            }
            excelApp.Visible = true;
        }       
    }
}
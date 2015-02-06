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

namespace KPIWeb.Reports
{
    public partial class FillingTheReport : System.Web.UI.Page
    {
        UsersTable user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                UsersTable user = (from usersTable in KPIWebDataContext.UsersTable
                                   where usersTable.Login == "user11" &&
                                   usersTable.Password == "user11"
                                   select usersTable).FirstOrDefault();
                Session["user"] = user;

                user = (UsersTable)Session["user"];

                if (user == null)
                    Response.Redirect("Login.aspx");
                else
                {
                    //KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

                    //Список ID всех активных кампаний для данного пользователя

                    
                    List<int> ReportArchiveIDList = (from reportArchiveTables in KPIWebDataContext.ReportArchiveTables
                                                     join reportAndRolesMappings in KPIWebDataContext.ReportAndRolesMappings on
                                                     reportArchiveTables.ReportArchiveTableID equals reportAndRolesMappings.FK_ReportArchiveTable
                                                     where reportAndRolesMappings.FK_RolesTable == user.FK_RolesTable &&
                                                     reportArchiveTables.Active == true &&
                                                     reportArchiveTables.StartDateTime < DateTime.Now &&
                                                     reportArchiveTables.EndDateTime > DateTime.Now
                                                     select reportArchiveTables.ReportArchiveTableID).ToList();

                    int currentReportArchiveID = ReportArchiveIDList.FirstOrDefault();
                    ViewState["CurrentReportArchiveID"] = currentReportArchiveID;

                    //Список всех базовых параметров "принадлежащих" данному пользователю
                    List<BasicParametersTable> basicParametersTable = (from basicParametersTables in KPIWebDataContext.BasicParametersTables
                                                                       join basicParametersAndRolesMappingTables in KPIWebDataContext.BasicParametersAndRolesMappingTables on
                                                                       basicParametersTables.BasicParametersTableID equals basicParametersAndRolesMappingTables.FK_BasicParametersTable
                                                                       where basicParametersAndRolesMappingTables.FK_RolesTable == user.FK_RolesTable
                                                                       select basicParametersTables).ToList();

                    //Список ранее введенных пользователем данных для данной кампании (отчета)
                    List<CollectedBasicParametersTable> сollectedBasicParametersTable = (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTables
                                                                                         where (from item in basicParametersTable select item.BasicParametersTableID).ToList().Contains((int)collectedBasicParameters.FK_BasicParametersTable) &&
                                                                                         collectedBasicParameters.FK_UsersTable == user.UsersTableID &&
                                                                                         collectedBasicParameters.FK_ReportArchiveTable == currentReportArchiveID
                                                                                         select collectedBasicParameters).ToList();

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CollectedValue", typeof(string)));

                    //Добавляем недостающие строки в базу CollectedBasicParametersTable с пустыми значениями в столбце "CollectedValue"
                    //+ заполняем обьект dataTable
                    foreach (BasicParametersTable basicParameter in basicParametersTable)
                    {
                        CollectedBasicParametersTable сollectedBasicParameter = (from item in сollectedBasicParametersTable
                                                                                 where item.FK_BasicParametersTable == basicParameter.BasicParametersTableID
                                                                                 select item).FirstOrDefault();

                        DataRow dataRow = dataTable.NewRow();

                        dataRow["CurrentReportArchiveID"] = currentReportArchiveID;
                        dataRow["BasicParametersTableID"] = basicParameter.BasicParametersTableID;
                        dataRow["Name"] = basicParameter.Name;

                        if (сollectedBasicParameter == null)
                        {
                            string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";

                            CollectedBasicParametersTable newItem = new CollectedBasicParametersTable();
                            newItem.Active = true;
                            newItem.FK_UsersTable = user.UsersTableID;
                            newItem.FK_ReportArchiveTable = currentReportArchiveID;
                            newItem.FK_BasicParametersTable = basicParameter.BasicParametersTableID;
                            newItem.UserIP = localIP;
                            newItem.LastChangeDateTime = DateTime.Now;

                            dataRow["CollectedBasicParametersTableID"] = string.Empty;
                            dataRow["CollectedValue"] = string.Empty;

                            KPIWebDataContext.CollectedBasicParametersTables.InsertOnSubmit(newItem);
                        }
                        else
                        {
                            dataRow["CollectedBasicParametersTableID"] = сollectedBasicParameter.CollectedBasicParametersTableID;
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
                user = (UsersTable)Session["user"];

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
                    List<CollectedBasicParametersTable> сollectedBasicParametersTable = (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTables
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
    }
}

//http://www.aspsnippets.com/Articles/Save-and-Retrieve-Dynamic-TextBox-values-in-GridView-to-SQL-Server-Database.aspx
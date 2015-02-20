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
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

namespace KPIWeb.Reports
{
    public partial class FillingTheReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            /////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                List<BasicParametersTable> basicParams =
                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                        join b in kpiWebDataContext.BasicParametersTable
                            on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                        join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                            on b.BasicParametersTableID equals c.FK_ParametrsTable
                        where a.FK_ReportArchiveTable == ReportArchiveID
                              && c.FK_UsersTable == UserID
                              && a.Active == true 
                              && c.CanEdit == true
                              && c.Active == true
                        select b).ToList();
                /////нашли все нужные базовые показатели по( пользователь и отчет)           
                UsersTable user = (from a in kpiWebDataContext.UsersTable
                    where a.UsersTableID == UserID
                    select a).FirstOrDefault();

                int l_0 = user.FK_ZeroLevelSubdivisionTable   == null ? 0 : (int)user.FK_ZeroLevelSubdivisionTable;
                int l_1 = user.FK_FirstLevelSubdivisionTable  == null ? 0 : (int)user.FK_FirstLevelSubdivisionTable;
                int l_2 = user.FK_SecondLevelSubdivisionTable == null ? 0 : (int)user.FK_SecondLevelSubdivisionTable;
                int l_3 = user.FK_ThirdLevelSubdivisionTable  == null ? 0 : (int)user.FK_ThirdLevelSubdivisionTable;
                int l_4 = user.FK_FourthLevelSubdivisionTable == null ? 0 : (int)user.FK_FourthLevelSubdivisionTable;
                int l_5 = user.FK_FifthLevelSubdivisionTable  == null ? 0 : (int)user.FK_FifthLevelSubdivisionTable;
                ///узнали все о пользователе
                List<SpecializationTable> specializationList = (from a in kpiWebDataContext.SpecializationTable
                    join b in kpiWebDataContext.SpecializationAndLevelMappingTable
                        on a.SpecializationTableID equals b.FK_SpecializationTable
                    where ((b.FK_ZeroLevelSubcisionTable      == l_0) || (l_0 == 0))
                          && ((b.FK_ZeroLevelSubcisionTable   == l_1) || (l_1 == 0))
                          && ((b.FK_SecondLevelSubcisionTable == l_2) || (l_2 == 0))
                          && ((b.FK_ThirdLevelSubcisionTable  == l_3) || (l_3 == 0))
                          && ((b.FK_FourthLevelSubcisionTable == l_4) || (l_4 == 0))
                          && ((b.FK_FifthLevelSubcisionTable  == l_5) || (l_5 == 0))
                    select a).ToList();
                // узнали все специальности под пользователем
                foreach (SpecializationTable specialization in specializationList)
                {

                    foreach (BasicParametersTable basicParam in basicParams) // создадим строки для ввода данных которых нет
                    {
                        CollectedBasicParametersTable collectedTemp =
                            (from a in kpiWebDataContext.CollectedBasicParametersTable
                             where a.FK_UsersTable == UserID
                                   && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                   && a.FK_ReportArchiveTable == ReportArchiveID
                                   && a.FK_SpecializationTable == specialization.SpecializationTableID
                             select a).FirstOrDefault();
                        if (collectedTemp == null) // надо создать
                        {
                            collectedTemp = new CollectedBasicParametersTable();
                            collectedTemp.Active = true;
                            collectedTemp.FK_UsersTable = UserID;
                            collectedTemp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                            collectedTemp.FK_ReportArchiveTable = ReportArchiveID;
                            collectedTemp.CollectedValue = 0;
                            collectedTemp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                            collectedTemp.LastChangeDateTime = DateTime.Now;
                            collectedTemp.SavedDateTime = DateTime.Now;
                            collectedTemp.FK_SpecializationTable = specialization.SpecializationTableID;
                            kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedTemp);
                            kpiWebDataContext.SubmitChanges();
                        }
                    }
                }
                ///база данных готово к выводу
                
                //создаем дататейбл
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                int i = 0;
                
                foreach (SpecializationTable specialization in specializationList)
                {
                    dataTable.Columns.Add(new DataColumn("Value"+i.ToString(), typeof(string)));
                    i++;
                }
                for (int k = i; k <= 8; k++)
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                }

                foreach (BasicParametersTable basicParam in basicParams) // создадим строки для ввода данных которых нет
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                    dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;            
                    dataRow["Name"] = basicParam.Name;
                    /*
                    dataRow["CollectedBasicParametersTableID"] = collectedBasic.CollectedBasicParametersTableID;
                    dataRow["CollectedValue"] = collectedBasic.CollectedValue;
                    */
                    int j = 0;
                    
                    foreach (SpecializationTable specialization in specializationList)
                    {
                        dataRow["Value"+j.ToString()] =
                            (from a in kpiWebDataContext.CollectedBasicParametersTable
                                where a.FK_UsersTable == UserID
                                      && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                      && a.FK_ReportArchiveTable == ReportArchiveID
                                      && a.FK_SpecializationTable == specialization.SpecializationTableID
                                select a.CollectedValue).FirstOrDefault();
                        j++;
                    }
                    dataTable.Rows.Add(dataRow);
                }

                ViewState["CollectedBasicParametersTable"] = dataTable;
                ViewState["CurrentReportArchiveID"] = ReportArchiveID;
                GridviewCollectedBasicParameters.DataSource = dataTable;

                for (int j=0; j < i; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j+4].Visible = true;
                }              
                GridviewCollectedBasicParameters.DataBind();
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
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MainFunctions mainFunctions = new MainFunctions();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userId);
            if (userTable.AccessLevel != 10 && userTable.AccessLevel != 8)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                KPIWebDataContext kpiWebData = new KPIWebDataContext();
                List<ReportArchiveTable> reportsList =
                    (from a in kpiWebData.ReportArchiveTable where a.Active == true select a).ToList();
                foreach (ReportArchiveTable currentReport in reportsList)
                {
                    ListItem newListIntem = new ListItem();
                    newListIntem.Value = currentReport.ReportArchiveTableID.ToString();
                    newListIntem.Text = currentReport.Name;
                    ReportsDropDown.Items.Add(newListIntem);
                }
            }
        }

        protected void ButtonStatusChangeClick (object sender, EventArgs e)
        {

        }

        protected void ButtonDeleteRowClick(object sender, EventArgs e)
        {

        }

        protected void ButtobDeleteRowFromDbClick(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebData = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            int reportId = Convert.ToInt32(ReportsDropDown.SelectedValue);

            #region
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ResponsibleProrector", typeof(string)));
            dataTable.Columns.Add(new DataColumn("CollectedId", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SubmitComment", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SubmitDate", typeof(string)));
            #endregion

            if (ParamTypeDropDown.SelectedValue == "1")
            {
                List<IndicatorsTable> allIndicatorsInReport = mainFunctions.GetIndicatorsInReport(reportId);
                foreach (IndicatorsTable currentIndicator in allIndicatorsInReport)
                {
                    DataRow newDataRow = dataTable.NewRow();
                    newDataRow["Name"] = currentIndicator.Name;
                    newDataRow["SubmitComment"] = "";
                    newDataRow["SubmitDate"] = "";
                    newDataRow["ResponsibleProrector"] =
                        mainFunctions.GetResponsibleProrectorPositionForIndicator(currentIndicator.IndicatorsTableID);
                    CollectedIndocators currentCollected =
                        mainFunctions.GetCollectedIndicatorInReport(currentIndicator.IndicatorsTableID, reportId);
                    if (currentCollected == null)
                    {
                        newDataRow["Status"] = "Данных нет";
                        newDataRow["CollectedId"] = "0";
                        newDataRow["Value"] = "Значение отсутствует";
                        if (Filter1DropDown.SelectedValue=="2") continue;

                    }
                    else
                    {
                        if (currentCollected.Confirmed == true)
                        {
                            newDataRow["Status"] = "Утвержден";
                            if (Filter1DropDown.SelectedValue == "3") continue;
                        }
                        else
                        {
                            newDataRow["Status"] = "Не утвержден";
                            if (Filter1DropDown.SelectedValue == "2") continue;
                        }

                        newDataRow["CollectedId"] = currentCollected.CollectedIndocatorsID;
                        newDataRow["Value"] = currentCollected.CollectedValue.ToString();
                        ConfirmationHistory confirmData =
                            mainFunctions.GetConfirmationHistoryLine(currentIndicator.IndicatorsTableID, 0, reportId);
                        if (confirmData != null)
                        {
                            newDataRow["SubmitComment"] = confirmData.Comment;
                            newDataRow["SubmitDate"] = confirmData.Date;
                        }
                    }
                    dataTable.Rows.Add(newDataRow);
                }
            }
            else
            {
                List<CalculatedParametrs> allCalculatedInReport = mainFunctions.GetCalculatedParametrsInReport(reportId);
                foreach (CalculatedParametrs currentCalculated in allCalculatedInReport)
                {
                    DataRow newDataRow = dataTable.NewRow();
                    newDataRow["Name"] = currentCalculated.Name;
                    newDataRow["SubmitComment"] = "";
                    newDataRow["SubmitDate"] = "";
                    newDataRow["ResponsibleProrector"] =
                        mainFunctions.GetResponsibleProrectorPositionForCalculated(currentCalculated.CalculatedParametrsID);
                    CollectedCalculatedParametrs currentCollected =
                        mainFunctions.GetCollectedCalculatedInRepost(currentCalculated.CalculatedParametrsID, reportId);
                    if (currentCollected == null)
                    {
                        newDataRow["Status"] = "Данных нет";
                        newDataRow["CollectedId"] = "0";
                        newDataRow["Value"] = "Значение отсутствует";
                        if (Filter1DropDown.SelectedValue == "2") continue;

                    }
                    else
                    {
                        if (currentCollected.Confirmed == true)
                        {
                            newDataRow["Status"] = "Утвержден";
                            if (Filter1DropDown.SelectedValue == "3") continue;
                        }
                        else
                        {
                            newDataRow["Status"] = "Не утвержден";
                            if (Filter1DropDown.SelectedValue == "2") continue;
                        }
                        newDataRow["Status"] = currentCollected.Confirmed == true ? "Утвержден" : "Не утвержден";
                        newDataRow["CollectedId"] = currentCollected.CollectedCalculatedParametrsID;
                        newDataRow["Value"] = currentCollected.CollectedValue.ToString();
                        ConfirmationHistory confirmData =
                            mainFunctions.GetConfirmationHistoryLine(0, currentCalculated.CalculatedParametrsID, reportId);
                        if (confirmData != null)
                        {
                            newDataRow["SubmitComment"] = confirmData.Comment;
                            newDataRow["SubmitDate"] = confirmData.Date;
                        }
                    }
                    dataTable.Rows.Add(newDataRow);
                }
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
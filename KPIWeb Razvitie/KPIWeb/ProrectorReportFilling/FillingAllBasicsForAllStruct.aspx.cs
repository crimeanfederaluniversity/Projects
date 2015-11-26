using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class FillingAllBasicsForAllStruct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            RangeValidatorFunctions rangeValidatorFunctions = new RangeValidatorFunctions();
            CollectedDataStatusProcess collectedDataStatusProcess = new CollectedDataStatusProcess();
            ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userId);
            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            ViewState["login"] = userTable.Email;
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int reportId = Convert.ToInt32(mySession.ReportArchiveID);         
            ReportArchiveTable report = mainFunctions.GetReportById(reportId);
          
            ReportNameLabel.Text = report.Name;
            if (!Page.IsPostBack)
            {
                List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                /////создаем дататейбл
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Comment", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CommentEnabled", typeof (string)));

                    dataTable.Columns.Add(new DataColumn("Value0", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("CollectId0" , typeof (string)));
                    dataTable.Columns.Add(new DataColumn("NotNull0" , typeof (string)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorEnabled0" , typeof (bool)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorMinValue0", typeof (double)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorMaxValue0" , typeof (double)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorType0" ,
                        typeof (ValidationDataType)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorMessage0", typeof (string)));
                    dataTable.Columns.Add(new DataColumn("TextBoxReadOnly0", typeof (bool)));

                    List<BasicParametersTable> basicParametersToFillList = (from a in kpiWebDataContext.BasicParametersTable
                                                                            where a.Active == true

                                                                            join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                                on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                            where b.Active == true
                                                                                  && b.FK_UsersTable == userId
                                                                                  && b.CanEdit == true

                                                                            join c in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                                on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                            where c.Active == true
                                                                                  && c.FK_ReportArchiveTable == reportId

                                                                            select a).Distinct().ToList();
                bool canConfirm = true;
                bool allConfirmed = true;
                TmpProrectorFillFunctions tmpProrectorFillFunctions = new TmpProrectorFillFunctions();

                    foreach (BasicParametersTable currentBasic in basicParametersToFillList)
                    {
                        CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
                        if (tmpProrectorFillFunctions.IsBasicNotToShow(currentBasic.BasicParametersTableID))
                        {
                            CollectedBasicParametersTable currentCollectedData =
                                collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                    currentBasic.BasicParametersTableID, 0, 1,
                                    true,0,userId);

                        }
                        else
                        {

                            #region filling

                            DataRow dataRow = dataTable.NewRow();
                            dataRow["Name"] = currentBasic.Name;
                            dataRow["CurrentReportArchiveID"] = reportId;
                            dataRow["BasicParametersTableID"] = currentBasic.BasicParametersTableID;
                            dataRow["Comment"] =
                                mainFunctions.GetCommentForBasicInReport(currentBasic.BasicParametersTableID, reportId);;
                            BasicParametrAdditional basicParametrAdditional =
                                (from a in kpiWebDataContext.BasicParametrAdditional
                                    where a.BasicParametrAdditionalID == currentBasic.BasicParametersTableID
                                    select a).FirstOrDefault();
                            if (basicParametrAdditional == null)
                            {
                                Response.Redirect("~/Default.aspx");
                            }
                            int dataType = (int) basicParametrAdditional.DataType;
                            int columnId = 0;
                           

                            CollectedBasicParametersTable currentCollectedData =
                                collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                    currentBasic.BasicParametersTableID, 0, 1,
                                    true, null,
                                    userId);
                            if (currentCollectedData.Status != 5)
                            {
                                allConfirmed = false;
                            }
                            if (currentCollectedData.CollectedValue == null)
                            {
                                canConfirm = false;
                            }
                            //  if (currentCollectedData.Status > 4) reportIsConfirmed = true;
                            dataRow["Value0"] = currentCollectedData.CollectedValue.ToString();
                            dataRow["CollectId0"] =
                                currentCollectedData.CollectedBasicParametersTableID.ToString();
                            dataRow["TextBoxReadOnly0"] = currentCollectedData.Status == 5 ? true : false;

                            dataRow["NotNull0"] = 1.ToString();

                            dataRow["RangeValidatorEnabled0"] =
                                rangeValidatorFunctions.GetValidateEnabledForDataType(dataType);
                            dataRow["RangeValidatorMinValue0"] =
                                rangeValidatorFunctions.GetMinValueForDataType(dataType);
                            dataRow["RangeValidatorMaxValue0"] =
                                rangeValidatorFunctions.GetMaxValueForDataType(dataType);
                            dataRow["RangeValidatorType0"] =
                                rangeValidatorFunctions.GetValidateTypeForDataType(dataType);
                            dataRow["RangeValidatorMessage0"] =
                                rangeValidatorFunctions.GetValidateErrorTextForDataType(dataType);

                            dataTable.Rows.Add(dataRow);

                            #endregion
                        }
                    }
                    SendButton.Enabled = canConfirm;
                    if (allConfirmed)
                    {
                        SendButton.Enabled = false;
                        SaveButton.Enabled = false;
                    }
                    GridviewCollectedBasicParameters.DataSource = dataTable;         
                    GridviewCollectedBasicParameters.DataBind();
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseReport.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            foreach ( GridViewRow currentRow in GridviewCollectedBasicParameters.Rows)
            {
                Label idLabel = currentRow.FindControl("CollectId0") as Label;
                TextBox valueTextBox = currentRow.FindControl("Value0") as TextBox;
                if ((idLabel != null) && (valueTextBox !=null))
                {
                    if (idLabel.Text.Any())
                    {
                        int iD = Convert.ToInt32(idLabel.Text);
                        CollectedBasicParametersTable currentCollected =
                            (from a in kpiWebDataContext.CollectedBasicParametersTable
                                where a.CollectedBasicParametersTableID == iD
                                select a).FirstOrDefault();
                        if (currentCollected != null)
                        {
                            if (valueTextBox.Text.Any())
                            {
                                currentCollected.CollectedValue = Convert.ToDouble(valueTextBox.Text);
                                kpiWebDataContext.SubmitChanges();
                            }
                        }
                    }
                }
            }
            Response.Redirect("FillingAllBasicsForAllStruct.aspx");
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
           
            MainFunctions mainFunctions = new MainFunctions();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userId);
            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            ViewState["login"] = userTable.Email;
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int reportId = Convert.ToInt32(mySession.ReportArchiveID);         
            ReportArchiveTable report = mainFunctions.GetReportById(reportId);
          
            List<BasicParametersTable> basicParametersToFillList = (from a in kpiWebDataContext.BasicParametersTable
                                                                            where a.Active == true

                                                                            join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                                on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                            where b.Active == true
                                                                                  && b.FK_UsersTable == userId
                                                                                  && b.CanEdit == true

                                                                            join c in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                                on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                            where c.Active == true
                                                                                  && c.FK_ReportArchiveTable == reportId

                                                                            select a).Distinct().ToList();

                TmpProrectorFillFunctions tmpProrectorFillFunctions = new TmpProrectorFillFunctions();

                    foreach (BasicParametersTable currentBasic in basicParametersToFillList)
                    {
                        CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
                            CollectedBasicParametersTable currentCollectedData =
                                collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                    currentBasic.BasicParametersTableID, 0, 1,
                                    true,0,userId);
                            CollectedBasicParametersTable currentCollectedData2 = (from a in kpiWebDataContext.CollectedBasicParametersTable
                            where a.CollectedBasicParametersTableID == currentCollectedData.CollectedBasicParametersTableID
                            select a).FirstOrDefault();
                            currentCollectedData2.Status = 5;

                        kpiWebDataContext.SubmitChanges();
                    }

            Response.Redirect("ChooseReport.aspx");
        }
    }
}
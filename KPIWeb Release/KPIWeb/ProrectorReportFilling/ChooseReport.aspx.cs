using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPIWeb.Rector;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class ChooseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            CollectedDataStatusProcess collectedDataStatusProcess = new CollectedDataStatusProcess();

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
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                List<ReportArchiveTable> reportsArchiveTablesTable = null;
                reportsArchiveTablesTable = (from a in kpiWebDataContext.ReportArchiveTable
                                             join b in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                             on a.ReportArchiveTableID equals b.FK_ReportArchiveTable
                                             where a.Active == true
                                             && b.Active == true
                                             join c in kpiWebDataContext.IndicatorsAndUsersMapping
                                             on b.FK_IndicatorsTable equals c.FK_IndicatorsTable
                                             where c.Active == true
                                             && c.CanView == true
                                             && c.FK_UsresTable == userId
                                             && a.OnlyForProrector == true
                                             select a).Distinct().ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ConfirmButtonEnabled", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("ViewButtonEnabled", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("ViewButtonEnabledForAllStruct", typeof(bool)));          
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                TmpProrectorFillFunctions tmpProrectorFillFunctions = new TmpProrectorFillFunctions();
                foreach (ReportArchiveTable reportRow in reportsArchiveTablesTable)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ReportArchiveID"] = reportRow.ReportArchiveTableID.ToString();
                    dataRow["ReportName"] = reportRow.Name;
                    dataRow["StartDate"] = reportRow.StartDateTime.ToString().Split(' ')[0];
                    dataRow["EndDate"] = reportRow.EndDateTime.ToString().Split(' ')[0];
                    dataRow["Status"] = "Нет данных";
                    if ( tmpProrectorFillFunctions.CanProrectorFillReportByStruct(reportRow.ReportArchiveTableID,userId))
                    {
                        dataRow["Status"] = collectedDataStatusProcess.GetStatusNameForStructInReportByStructIdNLevel(1, 0,
                       reportRow.ReportArchiveTableID, userId, true);
                    }
                    if (tmpProrectorFillFunctions.CanProrectorFillReportForAllStruct(reportRow.ReportArchiveTableID,
                        userId))
                    {
                        dataRow["Status"] = tmpProrectorFillFunctions.GetStatusNameForStructOnly(1,null,null,null,null,null,userId,reportRow.ReportArchiveTableID);
                    }
                   


                    dataRow["ConfirmButtonEnabled"] =
                        !collectedDataStatusProcess.DoesAnyCollectedHaveNullValue( 0, 1,
                            reportRow.ReportArchiveTableID, userId,true);

                    

                    if (!mainFunctions.CanUserEditAnyInReport(userId,
                        reportRow.ReportArchiveTableID))
                    {
                        dataRow["ConfirmButtonEnabled"] = false;
                        dataRow["ViewButtonEnabled"] = false;
                        dataRow["ViewButtonEnabledForAllStruct"] = false;
                    }
                    else
                    {
                        dataRow["ViewButtonEnabled"] = tmpProrectorFillFunctions.CanProrectorFillReportByStruct(reportRow.ReportArchiveTableID,userId);
                        dataRow["ViewButtonEnabledForAllStruct"] = tmpProrectorFillFunctions.CanProrectorFillReportForAllStruct(reportRow.ReportArchiveTableID, userId);                        dataRow["ConfirmButtonEnabled"] = true;
                    }

                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                CheckBoxesToShow checkBoxesToShow = new CheckBoxesToShow();
                Serialization newSession = new Serialization();
                MainFunctions mainFunctions = new MainFunctions();
                newSession.ReportArchiveID = Convert.ToInt32(button.CommandArgument);
                Session["ProrectorFillingSession"] = newSession;
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Prorector " + (string)ViewState["login"] + " pereshel k zapolneniyu otcheta, ID = " + button.CommandArgument);

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

                if (checkBoxesToShow.CanUserEditCheckBoxNetwork(userTable.UsersTableID))
                    Response.Redirect("FillOnlyCheckBoxes.aspx");

                Response.Redirect("ChooseAcademy.aspx");
            }            
        }
        protected void ButtonConfirmClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
               // MainFunctions mainFunctions = new MainFunctions();
                CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
                ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
                Serialization userSer = (Serialization)Session["UserID"];
                if (userSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int userId = userSer.Id;
                int reportId = Convert.ToInt32(button.CommandArgument);

                    List<ThirdLevelSubdivisionTable> thirdLevelListToFillWithZero =
                        toGetOnlyNeededStructAutoFilter.GetAllThirdLevelList(reportId, userId);

                    foreach (ThirdLevelSubdivisionTable currentThirdLevel in thirdLevelListToFillWithZero)
                    {
                        collectedDataProcess.SetStatusToAllCollected(reportId, 3, currentThirdLevel.ThirdLevelSubdivisionTableID,userId,5);
                        /*
                        ThirdLevelParametrs currentThirdParam = (from a in kPiDataContext.ThirdLevelParametrs
                            where a.ThirdLevelParametrsID == currentThirdLevel.ThirdLevelSubdivisionTableID
                            select a).FirstOrDefault();
                        List<BasicParametersTable> basicsForThirdInReportForUser =
                            (from a in kPiDataContext.BasicParametersTable
                                where a.Active == true

                                join b in kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                                    on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                                where b.Active == true
                                && b.FK_SubdivisionClassTable == currentThirdParam.FK_SubdivisionClassTable

                                join c in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                    on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                where c.Active == true
                                      && c.FK_ReportArchiveTable == reportId

                                join d in kPiDataContext.BasicParametrsAndUsersMapping
                                    on a.BasicParametersTableID equals d.FK_ParametrsTable
                                where d.Active == true
                                      && d.FK_UsersTable == userId
                                      && d.CanEdit == true

                                select a).Distinct().ToList();
                        foreach (BasicParametersTable currentBasic in basicsForThirdInReportForUser)
                        {
                            ////CONFIRMATION
                            collectedDataProcess.ConfirmCollectedBasic(reportId,
                                currentBasic.BasicParametersTableID, 3, currentThirdLevel.ThirdLevelSubdivisionTableID);
                        }       */               
                    }
            }  
            Response.Redirect("ChooseReport.aspx");
        }                
        protected void ButtonZeroToAllNullReportClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                MainFunctions mainFunctions = new MainFunctions();
                ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
                Serialization userSer = (Serialization)Session["UserID"];
                if (userSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int userId = userSer.Id;
                int reportId = Convert.ToInt32(button.CommandArgument);

                    List<ThirdLevelSubdivisionTable> thirdLevelListToFillWithZero =
                        toGetOnlyNeededStructAutoFilter.GetAllThirdLevelList(reportId, userId);
                    CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
                    foreach (ThirdLevelSubdivisionTable currentThirdLevel in thirdLevelListToFillWithZero)
                    {
                        ThirdLevelParametrs currentThirdParam = (from a in kPiDataContext.ThirdLevelParametrs
                            where a.ThirdLevelParametrsID == currentThirdLevel.ThirdLevelSubdivisionTableID
                            select a).FirstOrDefault();
                        List<BasicParametersTable> basicsForThirdInReportForUser =
                            (from a in kPiDataContext.BasicParametersTable
                                where a.Active == true

                                join b in kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                                    on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                                where b.Active == true
                                && b.FK_SubdivisionClassTable == currentThirdParam.FK_SubdivisionClassTable

                                join c in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                    on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                where c.Active == true
                                      && c.FK_ReportArchiveTable == reportId

                                join d in kPiDataContext.BasicParametrsAndUsersMapping
                                    on a.BasicParametersTableID equals d.FK_ParametrsTable
                                where d.Active == true
                                      && d.FK_UsersTable == userId
                                      && d.CanEdit == true

                                select a).Distinct().ToList();
                        foreach (BasicParametersTable currentBasic in basicsForThirdInReportForUser)
                        {
                            collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                currentBasic.BasicParametersTableID, 3, currentThirdLevel.ThirdLevelSubdivisionTableID,
                                true,0, userId);
                        }                      
                    }
            }            
        }
        protected void ButtonFillReportForAllStruct(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                CheckBoxesToShow checkBoxesToShow = new CheckBoxesToShow();
                Serialization newSession = new Serialization();
                MainFunctions mainFunctions = new MainFunctions();
                newSession.ReportArchiveID = Convert.ToInt32(button.CommandArgument);
                Session["ProrectorFillingSession"] = newSession;
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Prorector " + (string)ViewState["login"] + " pereshel k zapolneniyu otcheta po vsemy CFU, ID = " + button.CommandArgument);

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

                Response.Redirect("FillingAllBasicsForAllStruct.aspx");
            }   
        }
    }
}
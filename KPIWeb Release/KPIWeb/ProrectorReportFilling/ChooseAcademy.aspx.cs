using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class ChooseAcademy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            CollectedDataStatusProcess collectedDataStatusProcess = new CollectedDataStatusProcess();
            ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userID);
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
            int reportID = Convert.ToInt32(mySession.ReportArchiveID);
            ReportArchiveTable Report = mainFunctions.GetReportById(reportID);
            ReportNameLabel.Text = Report.Name;

            if (!Page.IsPostBack)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("StructName", typeof (string)));
                dataTable.Columns.Add(new DataColumn("StructDataStatus", typeof (string)));
                dataTable.Columns.Add(new DataColumn("StructID", typeof (string)));


                List<FirstLevelSubdivisionTable> firstLevelList =
                    toGetOnlyNeededStructAutoFilter.GetFirstLevelList(reportID, userID);
           
                                                
                foreach (FirstLevelSubdivisionTable currentFirstLevel in firstLevelList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = currentFirstLevel.Name;
                    dataRow["StructID"] = currentFirstLevel.FirstLevelSubdivisionTableID;
                    dataRow["StructDataStatus"] =
                        collectedDataStatusProcess.GetStatusNameForStructInReportByStructIdNLevel(
                            currentFirstLevel.FirstLevelSubdivisionTableID,
                            1, reportID, userID);
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
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


        protected void ButtonStructDeeperClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization newSession = (Serialization) Session["ProrectorFillingSession"];
                newSession.l1 = Convert.ToInt32(button.CommandArgument);
                Session["ProrectorFillingSession"] = newSession;
                LogHandler.LogWriter.WriteLog(LogCategory.INFO,
                    "Prorector " + (string) ViewState["login"] + " pereshel k zapolneniyu dlya academy, ID = " +
                    button.CommandArgument);
                Response.Redirect("ChooseFaculty.aspx");
            }
        }
        
    }
}
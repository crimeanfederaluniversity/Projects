using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class ChooseFaculty : System.Web.UI.Page
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
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userID);
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 6, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            ViewState["login"] = userTable.Email;
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int reportID = Convert.ToInt32(mySession.ReportArchiveID);
            int firstLevelId = Convert.ToInt32((mySession.l1));
            FirstLevelSubdivisionTable firstLevel = mainFunctions.GetFirstLevelById(firstLevelId);
            ReportArchiveTable Report = mainFunctions.GetReportById(reportID);
            ReportNameLabel.Text = Report.Name;
            FirstLevelLabel.Text = firstLevel.Name;

            if (!Page.IsPostBack)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("StructName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StructDataStatus", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StructID", typeof (string)));

                List<SecondLevelSubdivisionTable> secondLevelList =
                    toGetOnlyNeededStructAutoFilter.GetSecondLevelList(reportID, userID, firstLevelId);

                foreach (SecondLevelSubdivisionTable currentSecondLevel in secondLevelList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = currentSecondLevel.Name;
                    dataRow["StructID"] = currentSecondLevel.SecondLevelSubdivisionTableID;
                    dataRow["StructDataStatus"] = collectedDataStatusProcess.GetStatusNameForStructInReportByStructIdNLevel(
                            currentSecondLevel.SecondLevelSubdivisionTableID,
                            2, reportID, userID,false); ;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseAcademy.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void ButtonStructDeeperClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization newSession = (Serialization)Session["ProrectorFillingSession"];
                newSession.l2 = Convert.ToInt32(button.CommandArgument);
                Session["ProrectorFillingSession"] = newSession;
                LogHandler.LogWriter.WriteLog(LogCategory.INFO,
                    "Prorector " + (string)ViewState["login"] + " pereshel k zapolneniyu dlya faculty, ID = " +
                    button.CommandArgument);
                /*

               */
                Response.Redirect("ChooseStruct.aspx");
            }
        }
        
    }
}
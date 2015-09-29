using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class ChooseStruct : System.Web.UI.Page
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
            int firstLevelId = Convert.ToInt32((mySession.l1));
            int secondLevelId = Convert.ToInt32(mySession.l2);
            SecondLevelSubdivisionTable secondLevel = mainFunctions.GetSecondLevelById(secondLevelId);
            FirstLevelSubdivisionTable firstLevel = mainFunctions.GetFirstLevelById(firstLevelId);
            ReportArchiveTable Report = mainFunctions.GetReportById(reportID);
            ReportNameLabel.Text = Report.Name;
            FirstLevelNameLabel.Text = firstLevel.Name;
            SecondLevelNameLabel.Text = secondLevel.Name;
            if (!Page.IsPostBack)
            {
                //мы берем кафедры
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("StructName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StructID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
               
                List<ThirdLevelSubdivisionTable> noKafedra = toGetOnlyNeededStructAutoFilter.GetThirdLevelList(
                    reportID, userID, secondLevelId, 2);

                List<ThirdLevelSubdivisionTable> OnlyKafedras = toGetOnlyNeededStructAutoFilter.GetThirdLevelList(
                    reportID, userID, secondLevelId, 1);

                foreach (ThirdLevelSubdivisionTable CurrentThird in noKafedra)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = CurrentThird.Name;
                    if (CurrentThird.Name == "Деканат")
                    {
                        dataRow["StructName"] = "Данные по контингенту студентов";
                    }
                    dataRow["StructID"] = CurrentThird.ThirdLevelSubdivisionTableID;


                    CollectedBasicParametersTable tmp = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                         where a.FK_ReportArchiveTable == secondLevelId
                                                         && a.Active == true
                                                         && a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                         select a).FirstOrDefault();
                    dataRow["Status"] =
                        collectedDataStatusProcess.GetStatusNameForStructInReportByStructIdNLevel(
                            CurrentThird.ThirdLevelSubdivisionTableID, 3, reportID, userID);
                    dataTable.Rows.Add(dataRow);
                }

                if (OnlyKafedras.Count() > 0)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = "Другое";
                    dataRow["StructID"] = 0;
                    List<int> tmpArrayOfIds = (List<int>)(from a in OnlyKafedras select a.ThirdLevelSubdivisionTableID).ToList();
                    dataRow["Status"] =
                        collectedDataStatusProcess.GetStatusNameForStructListInReportByStructIdListnLevel(
                            tmpArrayOfIds, 3, reportID, userID);            
                    dataTable.Rows.Add(dataRow);
                }

                                
                GridView1.DataSource = dataTable;
                
                GridView1.DataBind();
                if (GridView1.Rows.Count == 1)
                {
                    Button button = GridView1.Rows[0].FindControl("ButtonOpenFillingPage") as Button;
                    OpenFillingPage(Convert.ToInt32(button.CommandArgument));
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {


            Response.Redirect("ChooseFaculty.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        private void OpenFillingPage(int structId)
        {
            Serialization newSession = (Serialization)Session["ProrectorFillingSession"];
            newSession.l3 = structId;
            Session["ProrectorFillingSession"] = newSession;
            LogHandler.LogWriter.WriteLog(LogCategory.INFO,
                "Prorector " + (string)ViewState["login"] + " pereshel k zapolneniyu dlya 3-rd level, ID = " + structId);

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            CheckBoxesToShow checkBoxesToShow = new CheckBoxesToShow();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userID);
            if (newSession.l3 == 0)
            {
                Response.Redirect("FillingPage.aspx");
            }

            if (checkBoxesToShow.ShowUserChecBoxPage(userID))
            {
                Response.Redirect("SpecParamCheckBoxes.aspx");
            }
            Response.Redirect("FillingPage.aspx");
        }
        protected void ButtonOpenFillingPageClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                OpenFillingPage(Convert.ToInt32(button.CommandArgument));
            }
        }       
    }
}
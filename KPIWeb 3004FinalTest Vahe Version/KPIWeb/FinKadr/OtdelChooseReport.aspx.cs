using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace KPIWeb.FinKadr
{
    public partial class OtdelChooseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            
            ViewState["login"] =
                     (from a in kPiDataContext.UsersTable
                      where a.UsersTableID == userID
                      select a.Email).FirstOrDefault();

            if (userTable.AccessLevel != 3)
            {
                Response.Redirect("~/Default.aspx");
            }

            OtdelHistorySession OtdelHistory = (OtdelHistorySession)Session["OtdelHistory"];
            if (OtdelHistory == null)
            {
                GoForwardButton.Enabled = false;
            }

            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                ParametrType paramType = new ParametrType(1);
                if (paramType == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (paramType.paramType == 0) //смотрим индикацелевой показатель
                {
                    PageName.Text = "Работа с целевыми показателями";
                }
                else
                {
                    PageName.Text = "Работа с первичными данными";
                }

                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();                
                List<ReportArchiveTable> reportsArchiveTablesTable = null;            
                reportsArchiveTablesTable = (from a in kpiWebDataContext.ReportArchiveTable
                select a).ToList();
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
                    dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];
                    dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0];                            
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }





        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                ParametrType paramType = new ParametrType(1);
                if (paramType == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (paramType.paramType == 0) //смотрим индцелевой показатель
                {
                    OtdelResult.Struct mainStruct = new OtdelResult.Struct(1, "");
                    OtdelSession OtdelResultSession = new OtdelSession(mainStruct, 1, 0, 0,
                        Convert.ToInt32(button.CommandArgument), 0);
                    OtdelHistorySession OtdelHistory = new OtdelHistorySession();
                    OtdelHistory.SessionCount = 1;
                    OtdelHistory.CurrentSession = 0;
                    OtdelHistory.Visible = false;
                    OtdelHistory.OtdelSession[OtdelHistory.CurrentSession] = OtdelResultSession;
                    Session["OtdelHistory"] = OtdelHistory;
                    LogHandler.LogWriter.WriteLog(LogCategory.INFO, "OCR0: Проректор " + (string)ViewState["login"] + " перешел к работе с отчетом, ID = " + button.CommandArgument);
                    Response.Redirect("~/FinKadr/OtdelResult.aspx");
                }
                else // смотрим рассчетные
                {
                    OtdelResult.Struct mainStruct = new OtdelResult.Struct(1, "");
                    OtdelSession OtdelResultSession = new OtdelSession(mainStruct, 1, 0, 1,
                        Convert.ToInt32(button.CommandArgument), 0);
                    OtdelHistorySession OtdelHistory = new OtdelHistorySession();
                    OtdelHistory.SessionCount = 1;
                    OtdelHistory.CurrentSession = 0;
                    OtdelHistory.Visible = false;
                    OtdelHistory.OtdelSession[OtdelHistory.CurrentSession] = OtdelResultSession;
                    Session["OtdelHistory"] = OtdelHistory;
                    LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0OCR1: Проректор " + (string)ViewState["login"] + " перешел к работе с отчетом, ID = " + button.CommandArgument);
                    Response.Redirect("~/FinKadr/OtdelResult.aspx");
                }

            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
           // Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FinKadr/OtdelResult.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RectorChooseReport : System.Web.UI.Page
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

            /*
             * if (userTable.AccessLevel != 3)
            {
                Response.Redirect("~/Default.aspx");
            }*/


            RectorHistorySession RectorHistory = (RectorHistorySession) Session["rectorHistory"];
            if (RectorHistory == null)
            {
                GoForwardButton.Enabled = false;
            }

            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                //ParametrType paramType = new ParametrType(1);;
                ParametrType paramType = (ParametrType)Session["paramType"];
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
               /* reportsArchiveTablesTable = (from a in kpiWebDataContext.UsersTable
                                             where a.Active == true 
                                             && a.UsersTableID == userID
                                             join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                             on a.UsersTableID equals b.FK_UsresTable
                                             where b.Active == true
                                             && b.CanView == true

                                             where c.Active == true select c).ToList();
                */

                reportsArchiveTablesTable = (from a in kpiWebDataContext.ReportArchiveTable
                                             join b in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                             on a.ReportArchiveTableID equals b.FK_ReportArchiveTable
                                             where a.Active == true
                                             && b.Active == true
                                             join c in kpiWebDataContext.IndicatorsAndUsersMapping
                                             on b.FK_IndicatorsTable equals c.FK_IndicatorsTable
                                             where c.Active == true
                                             && c.CanView == true
                                             && c.FK_UsresTable == userID
                                             select a).Distinct().ToList();

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
                ParametrType paramType = (ParametrType)Session["paramType"];
                if (paramType == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                if (paramType.paramType == 0) //смотрим индцелевой показатель
                {
                    ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, "");
                    RectorSession rectorResultSession = new RectorSession(mainStruct, 1, 0, 0,
                        Convert.ToInt32(button.CommandArgument), 0);
                    RectorHistorySession RectorHistory = new RectorHistorySession();
                    RectorHistory.SessionCount = 1;
                    RectorHistory.CurrentSession = 0;
                    RectorHistory.Visible = false;
                    RectorHistory.RectorSession[RectorHistory.CurrentSession] = rectorResultSession;
                    Session["rectorHistory"] = RectorHistory;
                    LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RCR0: Prorector " + (string)ViewState["login"] + " pereshel k rabote s othetom, ID = " + button.CommandArgument);
                    Response.Redirect("~/Rector/Result.aspx");
                }
                else // смотрим рассчетные
                {
                    ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, "");
                    RectorSession rectorResultSession = new RectorSession(mainStruct, 1, 0, 1,
                        Convert.ToInt32(button.CommandArgument), 0);
                    RectorHistorySession RectorHistory = new RectorHistorySession();
                    RectorHistory.SessionCount = 1;
                    RectorHistory.CurrentSession = 0;
                    RectorHistory.Visible = false;
                    RectorHistory.RectorSession[RectorHistory.CurrentSession] = rectorResultSession;
                    Session["rectorHistory"] = RectorHistory;
                    LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RCR1: Prorector " + (string)ViewState["login"] + " pereshel k rabote s othetom, ID = " + button.CommandArgument);
                    Response.Redirect("~/Rector/Result.aspx");
                }

            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/Result.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
        }
    }
}
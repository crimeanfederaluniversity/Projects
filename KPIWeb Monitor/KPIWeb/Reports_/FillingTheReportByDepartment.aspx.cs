using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.DepartmentFilling
{
    public partial class FillingReport : System.Web.UI.Page
    {
        public int col_ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initialization n Sessions
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
            int SecondLevel = paramSerialization.l2;
            int ThirdLevel = paramSerialization.l3;

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                      (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            ViewState["login"] = (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a.Email).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 9, 0, 0)) // 1
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }          
            ViewState["userTableID"] = (int)userTable.UsersTableID;
            #endregion
            if (!Page.IsPostBack)
            {             
                    #region GetUserInfo

                    Serialization modeSer = (Serialization)Session["mode"];
                    if (modeSer == null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                    }
                    int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем //4 зашел директор
                    ////////////////
                    int l_0 = userTable.FK_ZeroLevelSubdivisionTable == null ? 0 : (int)userTable.FK_ZeroLevelSubdivisionTable;
                    int l_1 = userTable.FK_FirstLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FirstLevelSubdivisionTable;
                    int l_2 = userTable.FK_SecondLevelSubdivisionTable == null? 0: (int)userTable.FK_SecondLevelSubdivisionTable;
                    int l_3 = userTable.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)userTable.FK_ThirdLevelSubdivisionTable;
                    int l_4 = userTable.FK_FourthLevelSubdivisionTable == null? 0: (int)userTable.FK_FourthLevelSubdivisionTable;
                    int l_5 = userTable.FK_FifthLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FifthLevelSubdivisionTable;
                    int userLevel = 5;
                    userLevel = l_5 == 0 ? 4 : userLevel;
                    userLevel = l_4 == 0 ? 3 : userLevel;
                    userLevel = l_3 == 0 ? 2 : userLevel;
                    userLevel = l_2 == 0 ? 1 : userLevel;
                    userLevel = l_1 == 0 ? 0 : userLevel;
                    userLevel = l_0 == 0 ? -1 : userLevel;

                    ////ранги пользователя
                    /// -1 никто ниоткуда/// 0 с Кфу /// 1 с Академии/// 2 с Факультета/// 3 с кафедры/// 4 с специализация/// 5 с под специализацией,пока нет

                    #endregion
                    #region DataTableCreate

                    List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                    List<string> basicNames = new List<string>(); // сюда названия параметров для excel
                    int columnCount = 0;
                    /////создаем дататейбл
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("Struct1", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Struct2", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Struct3", typeof(string)));
                    for (int k = 0; k <= 40; k++) //создаем кучу полей
                    {
                        dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                        dataTable.Columns.Add(new DataColumn("CollectId" + k.ToString(), typeof(string)));
                        dataTable.Columns.Add(new DataColumn("NotNull" + k.ToString(), typeof(string)));
                    }
                    #endregion
                    #region GetThirdLevelAndBasics
                    List<ThirdLevelSubdivisionTable> thirdLevelInReport =
                    (from a in kPiDataContext.ThirdLevelSubdivisionTable
                        where a.Active == true
                        join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                            on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                        where b.Active == true
                              && b.FK_ReportArchiveTableId == ReportArchiveID
                        select a).ToList();

                List<BasicParametersTable> basicParametersInReport =
                    (from a in kPiDataContext.BasicParametersTable
                        where a.Active == true
                        join b in kPiDataContext.BasicParametrsAndUsersMapping
                            on a.BasicParametersTableID equals b.FK_ParametrsTable
                        where b.Active == true
                              && b.FK_UsersTable == userTable.UsersTableID
                              && (((b.CanEdit == true) && mode == 0)
                                  || ((b.CanView == true) && mode == 1)
                                  || ((b.CanConfirm == true) && mode == 2))
                        join c in kPiDataContext.BasicParametrAdditional
                            on a.BasicParametersTableID equals c.BasicParametrAdditionalID                       
                        where c.Calculated == false
                     join d in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                     on a.BasicParametersTableID equals  d.FK_BasicParametrsTable
                     where d.Active == true
                     && d.FK_ReportArchiveTable == ReportArchiveID
                     select a).ToList();
                    #endregion

                foreach (BasicParametersTable currentBasic in basicParametersInReport)
                {
                    columnNames.Add(currentBasic.Name);

                }
               
                foreach (ThirdLevelSubdivisionTable currentThird in thirdLevelInReport)
                {
                    
                    SecondLevelSubdivisionTable currentSecond = (from a in kPiDataContext.SecondLevelSubdivisionTable
                        where a.SecondLevelSubdivisionTableID == currentThird.FK_SecondLevelSubdivisionTable
                        select a).FirstOrDefault();
                    FirstLevelSubdivisionTable currentFirst = (from a in kPiDataContext.FirstLevelSubdivisionTable
                        join b in kPiDataContext.SecondLevelSubdivisionTable on a.FirstLevelSubdivisionTableID equals
                            b.FK_FirstLevelSubdivisionTable
                        where b.SecondLevelSubdivisionTableID == currentThird.FK_SecondLevelSubdivisionTable
                        select a).FirstOrDefault();

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Struct1"] = currentFirst.Name;
                    dataRow["Struct2"] = currentSecond.Name;
                    dataRow["Struct3"] = currentThird.Name;;

                    int i = 0;
                    foreach (BasicParametersTable currentBasic in basicParametersInReport)
                    {
                        #region GetCollectedIdCreateIfNeeded
                                                //сначала проверим есть для этого показателя на этом третьем уровне в этом отчете Collected
                                                CollectedBasicParametersTable currentCollected =
                                                    (from a in kPiDataContext.CollectedBasicParametersTable
                                                        where a.Active == true
                                                              && a.FK_ThirdLevelSubdivisionTable == currentThird.ThirdLevelSubdivisionTableID
                                                              && a.FK_BasicParametersTable == currentBasic.BasicParametersTableID
                                                              && a.FK_ReportArchiveTable == ReportArchiveID
                                                        select a).FirstOrDefault();
                                                if (currentCollected == null)
                                                {
                                                    currentCollected = new CollectedBasicParametersTable();
                                                    currentCollected.Active = true;
                                                    currentCollected.FK_UsersTable = userTable.UsersTableID;
                                                    currentCollected.FK_BasicParametersTable = currentBasic.BasicParametersTableID;
                                                    currentCollected.FK_ReportArchiveTable = ReportArchiveID;
                                                    currentCollected.CollectedValue = null;
                                                    currentCollected.UserIP =
                                                        Dns.GetHostEntry(Dns.GetHostName())
                                                            .AddressList.Where(
                                                                ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                                            .Select(ip => ip.ToString())
                                                            .FirstOrDefault() ?? "";
                                                    currentCollected.LastChangeDateTime = DateTime.Now;
                                                    currentCollected.SavedDateTime = DateTime.Now;
                                                    currentCollected.FK_ZeroLevelSubdivisionTable = currentFirst.FK_ZeroLevelSubvisionTable;
                                                    currentCollected.FK_FirstLevelSubdivisionTable = currentFirst.FirstLevelSubdivisionTableID;
                                                    currentCollected.FK_SecondLevelSubdivisionTable = currentSecond.SecondLevelSubdivisionTableID;
                                                    currentCollected.FK_ThirdLevelSubdivisionTable = currentThird.ThirdLevelSubdivisionTableID;
                                                    currentCollected.Status = 0;
                                                    kPiDataContext.CollectedBasicParametersTable.InsertOnSubmit(currentCollected);
                                                    kPiDataContext.SubmitChanges();
                                                }
                        #endregion
                        dataRow["Value" + i] =  currentCollected.CollectedValue.ToString();
                        dataRow["CollectId" + i] = currentCollected.CollectedBasicParametersTableID.ToString();
                        dataRow["NotNull" + i] = 1.ToString();
                        i++;
                    }                   
                    dataTable.Rows.Add(dataRow);
                }
                columnCount = basicParametersInReport.Count;
                ViewState["ValueColumnCnt"] = columnCount;
                GridviewCollectedBasicParameters.DataSource = dataTable;
                for (int j = 0; j < columnCount; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j + 3].Visible = true;
                    GridviewCollectedBasicParameters.Columns[j + 3].HeaderText = columnNames[j];
                }
                GridviewCollectedBasicParameters.DataBind();

            }
        }

        protected void GridviewCollectedBasicParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                #region Color
                                Color color;
                                Color confirmedColor = System.Drawing.Color.LimeGreen;
                                Color disableColor = System.Drawing.Color.LightGray;
                                if (col_ == 0)
                                {
                                    col_ = 1;
                                    color = System.Drawing.Color.FloralWhite;
                                }
                                else
                                {
                                    col_ = 0;
                                    color = System.Drawing.Color.GhostWhite;
                                }
                                e.Row.BackColor = color;
                #endregion  
                #region getSessionInfo

                    Serialization modeSer = (Serialization)Session["mode"];
                    if (modeSer == null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                    }
                    int mode = modeSer.mode;

                    int columnCnt = (int)ViewState["ValueColumnCnt"];
                #endregion               
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                int rowIndex = 0;
                for (int i = 1; i <= columnCnt; i++)
                {
                    {
                        var lblMinutes = e.Row.FindControl("Value" + rowIndex) as TextBox;
                        var NotNullLbl = e.Row.FindControl("NotNull" + rowIndex) as Label;
                        if (NotNullLbl != null)
                        {
                            if (NotNullLbl.Text.Count() == 0)
                            {
                                lblMinutes.Visible = false;
                                if (e.Row.RowType == DataControlRowType.DataRow)
                                {
                                    DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                    d.BackColor = disableColor;
                                }
                            }
                            else
                            {
                                Label lbl = e.Row.FindControl("CollectId" + rowIndex) as Label;
                                RangeValidator Validator = e.Row.FindControl("Validate" + rowIndex) as RangeValidator;
                                int Status = Convert.ToInt32((from a in kpiWebDataContext.CollectedBasicParametersTable
                                                              where a.CollectedBasicParametersTableID == Convert.ToInt32(lbl.Text)
                                                              select a.Status).FirstOrDefault());

                                if (mode == 0) // редактировать
                                {
                                    #region edit
                                    int type = Convert.ToInt32((from a in kpiWebDataContext.CollectedBasicParametersTable
                                                                join b in kpiWebDataContext.BasicParametrAdditional
                                                                    on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                                where a.CollectedBasicParametersTableID == Convert.ToInt32(lbl.Text)
                                                                select b.DataType).FirstOrDefault());

                                    if (Status == 4) // данные подтверждены первым уровнем
                                    {
                                        lblMinutes.ReadOnly = true;
                                        DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                        d.BackColor = confirmedColor;
                                        lblMinutes.BackColor = confirmedColor;
                                        if (Validator != null)
                                        {
                                            Validator.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                        d.BackColor = color;
                                        lblMinutes.BackColor = color;
                                        #region validator choose
                                        if (Validator != null)
                                        {
                                            if (type == 0)
                                            {
                                                Validator.MinimumValue = "0";
                                                Validator.MaximumValue = "1";
                                                Validator.Type = ValidationDataType.Integer;
                                                Validator.Text = "Только 0 или 1";
                                            }
                                            if (type == 1)
                                            {
                                                Validator.MinimumValue = "0";
                                                Validator.MaximumValue = "1000000";
                                                Validator.Type = ValidationDataType.Integer;
                                                Validator.Text = "Только целочисленное значение";
                                            }
                                            if (type == 2)
                                            {
                                                Validator.MinimumValue = "0";
                                                Validator.MaximumValue = "1000000000000";
                                                Validator.Type = ValidationDataType.Double;
                                                Validator.Text = "Только цифры и запятая";
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                else if (mode == 1) //смотреть
                                {
                                    #region view
                                    lblMinutes.ReadOnly = true;
                                    lblMinutes.BackColor = color;
                                   
                                    if (Validator != null)
                                    {
                                        Validator.Enabled = false;
                                    }
                                    #endregion
                                }
                                else if (mode == 2) // утверждать
                                {
                                    #region confirm
                                    lblMinutes.ReadOnly = true;
                                    Validator.Enabled = false;
                                    lblMinutes.BackColor = color;                                  
                                    #endregion
                                }
                            }
                        }

                        rowIndex++;
                    }
                }
            }
        }

    }
}
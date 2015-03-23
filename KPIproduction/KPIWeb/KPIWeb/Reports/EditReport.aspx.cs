using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.Threading;
using System.Web.WebPages;

namespace KPIWeb.Reports
{
    public partial class EditReport : System.Web.UI.Page
    {
        UsersTable user;


        protected void FillGridVIews(int reportID)
        {   
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////                
            List<IndicatorsTable> indicatorTable =
            (from item in kPiDataContext.IndicatorsTable where item.Active == true select item).ToList();
            DataTable dataTableIndicator = new DataTable();

            dataTableIndicator.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
            dataTableIndicator.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
            dataTableIndicator.Columns.Add(new DataColumn("IndicatorCheckBox", typeof(bool)));
            foreach (IndicatorsTable indicator in indicatorTable)
            {
                DataRow dataRow = dataTableIndicator.NewRow();
                dataRow["IndicatorID"] = indicator.IndicatorsTableID.ToString();
                dataRow["IndicatorName"] = indicator.Name;
                dataRow["IndicatorCheckBox"] = ((from a in kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                                                 where a.Active == true
                                                 && a.FK_IndicatorsTable == indicator.IndicatorsTableID
                                                 && a.FK_ReportArchiveTable == reportID
                                                 select a).Count() > 0) ? true : false; 
                dataTableIndicator.Rows.Add(dataRow);                
            }            
            IndicatorsTable.DataSource = dataTableIndicator;
            IndicatorsTable.DataBind();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<CalculatedParametrs> calcParams =
            (from item in kPiDataContext.CalculatedParametrs where item.Active == true select item).ToList();
            DataTable dataTableCalc = new DataTable();

            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsID", typeof(string)));
            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsName", typeof(string)));
            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsCheckBox", typeof(bool)));
            foreach (CalculatedParametrs calcParam in calcParams)
            {
                DataRow dataRow = dataTableCalc.NewRow();
                dataRow["CalculatedParametrsID"] = calcParam.CalculatedParametrsID.ToString();
                dataRow["CalculatedParametrsName"] = calcParam.Name;
                dataRow["CalculatedParametrsCheckBox"] = ((from a in kPiDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                                           where a.Active == true
                                                           && a.FK_CalculatedParametrsTable == calcParam.CalculatedParametrsID
                                                           && a.FK_ReportArchiveTable == reportID
                                                           select a).Count() > 0) ? true : false; 
                dataTableCalc.Rows.Add(dataRow);
            }
            CalculatedParametrsTable.DataSource = dataTableCalc;
            CalculatedParametrsTable.DataBind();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<BasicParametersTable> basicParams =
          (from item in kPiDataContext.BasicParametersTable where item.Active == true select item).ToList();
            DataTable dataTableBasic = new DataTable();

            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsID", typeof(string)));
            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsName", typeof(string)));
            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsCheckBox", typeof(bool)));
            foreach (BasicParametersTable basic in basicParams)
            {
                DataRow dataRow = dataTableBasic.NewRow();
                dataRow["BasicParametrsID"] = basic.BasicParametersTableID.ToString();
                dataRow["BasicParametrsName"] = basic.Name;
                dataRow["BasicParametrsCheckBox"] = ((from a in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                      where a.Active == true
                                                      && a.FK_BasicParametrsTable == basic.BasicParametersTableID
                                                      && a.FK_ReportArchiveTable == reportID
                                                      select a).Count() > 0) ? true : false; 
                dataTableBasic.Rows.Add(dataRow);
            }
            BasicParametrsTable.DataSource = dataTableBasic;
            BasicParametrsTable.DataBind();
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            ViewState["BasicDataTable"] = dataTableBasic;
            ViewState["CalculateDataTable"] = dataTableCalc;
            ViewState["IndicatorDataTable"] = dataTableIndicator;

            /////////////////////////////////////////////////////////////////////

            for (int i = 0; i < dataTableBasic.Rows.Count; i++)
            {
                CheckBox chekBox = (CheckBox)BasicParametrsTable.Rows[i].FindControl("BasicParametrsCheckBox");
                if (chekBox.Checked == true)
                {
                    chekBox.Enabled = false;
                }
            }

            for (int i = 0; i < dataTableCalc.Rows.Count; i++)
            {
                CheckBox chekBox = (CheckBox)CalculatedParametrsTable.Rows[i].FindControl("CalculatedParametrsCheckBox");
                if (chekBox.Checked == true)
                {
                    chekBox.Enabled = false;
                }
            }

            for (int i = 0; i < dataTableIndicator.Rows.Count; i++)
            {
                CheckBox chekBox = (CheckBox)IndicatorsTable.Rows[i].FindControl("IndicatorCheckBox");
                if (chekBox.Checked == true)
                {
                    chekBox.Enabled = false;
                }
            }
        }

        protected List<int> getCalcByIndicator(List<int> indArr)
        {
            List<int> calcListTemp = new List<int>();
            StringBuilder AllInOne = new StringBuilder();
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            foreach (int tmp in indArr)
            {
                IndicatorsTable indTable = (from a in kpiWebDataContext.IndicatorsTable
                                            where a.IndicatorsTableID == tmp
                                            select a).FirstOrDefault();
                AllInOne.Append(indTable.Formula + "*");
            }

            string[] abbArr = AllInOne.ToString().Split('/', '^', '+', '-', '(', ')', '*', ' ', '\n', '\r');
            string strArr = "";
            foreach (string str in abbArr)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        int tmpp = Convert.ToInt32((from a in kpiWebDataContext.CalculatedParametrs
                                                    where a.AbbreviationEN == str
                                                    select a.CalculatedParametrsID).FirstOrDefault());
                        if (tmpp == 0)
                        {
                            //ERROR//нужно записать в лог str// это аббревиатура
                        }
                        else
                        {
                            calcListTemp.Add(tmpp);
                        }
                        
                    }
                }
            }
            return calcListTemp;           
        }

        protected List<int> getBasicByCalc(List<int> calcArr)
        {
            List<int> basicListTemp = new List<int>();
            StringBuilder AllInOne = new StringBuilder();
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            foreach (int tmp in calcArr)
            {
                CalculatedParametrs calcTable = (from a in kpiWebDataContext.CalculatedParametrs
                                                 where a.CalculatedParametrsID == tmp
                                                 select a).FirstOrDefault();
                AllInOne.Append(calcTable.Formula + "*");
            }

            string[] abbArr = AllInOne.ToString().Split('/', '^', '+', '-', '(', ')', '*', ' ','\n','\r');
            string strArr = "";

            foreach (string str in abbArr)
            {
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        int tmpp = Convert.ToInt32((from a in kpiWebDataContext.BasicParametersTable
                            where a.AbbreviationEN == str
                            select a.BasicParametersTableID).FirstOrDefault());
                        if (tmpp==0)
                        {
                            //ERROR//нужно записать в лог str
                        }
                        else
                        {
                            basicListTemp.Add(tmpp);
                        }
                        
                    }
                }
            }

            return basicListTemp;
        }

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

            if ((userTable.AccessLevel != 10)&&(userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }
            ////////////////////////////////////////////////////////////////////////////
            Serialization ReportId = (Serialization)Session["ReportArchiveTableID"];

            if (!Page.IsPostBack)
            {
                if (ReportId.ReportArchiveID != 0)
                {
                    int reportArchiveTableID = ReportId.ReportArchiveID;

                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    ReportArchiveTable ReportArchiveTable = (from item in KPIWebDataContext.ReportArchiveTable
                                                             where item.ReportArchiveTableID == reportArchiveTableID
                                                             select item).FirstOrDefault();
                    if (ReportArchiveTable != null)
                    {
                        CheckBoxActive.Checked = ReportArchiveTable.Active;
                        CheckBoxCalculeted.Checked = ReportArchiveTable.Calculeted;
                        CheckBoxSent.Checked = ReportArchiveTable.Sent;
                        CheckBoxRecipientConfirmed.Checked = ReportArchiveTable.RecipientConfirmed;
                        
                        TextBoxName.Text = ReportArchiveTable.Name;

                        if (ReportArchiveTable.StartDateTime != null)
                            CalendarStartDateTime.SelectedDate = (DateTime)ReportArchiveTable.StartDateTime;

                        if (ReportArchiveTable.EndDateTime != null)
                            CalendarEndDateTime.SelectedDate = (DateTime)ReportArchiveTable.EndDateTime;

                        if (ReportArchiveTable.DateToSend != null)
                            CalendarDateToSend.SelectedDate = (DateTime)ReportArchiveTable.DateToSend;

                        if (ReportArchiveTable.SentDateTime != null)
                            CalendarSentDateTime.SelectedDate = (DateTime)ReportArchiveTable.SentDateTime;

                        if (ReportArchiveTable.RecivedDateTime != null)
                            CalendarReportRecived.SelectedDate = (DateTime)ReportArchiveTable.RecivedDateTime;
                    }
                    ////заполнили поля
                    List<FirstLevelSubdivisionTable> academies =
                        (from item in KPIWebDataContext.FirstLevelSubdivisionTable
                         where item.Active==true
                            select item).ToList();
                    int i = 0;

                    foreach (FirstLevelSubdivisionTable academy in academies)
                    {
                        CheckBoxList1.Items.Add(academy.Name);
                        int tmp = (from item in KPIWebDataContext.ReportArchiveAndLevelMappingTable
                            where item.FK_FirstLevelSubmisionTableId == academy.FirstLevelSubdivisionTableID
                                  && item.FK_ReportArchiveTableId == reportArchiveTableID
                            select item).Count();
                        CheckBoxList1.Items[i].Selected = tmp > 0 ? true : false; 
                        CheckBoxList1.Items[i].Value = academy.FirstLevelSubdivisionTableID.ToString();
                        i++;
                    }            
                    //////////////////////////////////////////////////////////////////////////////////
                    FillGridVIews(reportArchiveTableID);
                }
                else //создаем новый отчет
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    ButtonSave.Text="Сохранить новую кампанию";

                    List<FirstLevelSubdivisionTable> academies =
                        (from item in KPIWebDataContext.FirstLevelSubdivisionTable
                         where item.Active == true
                         select item).ToList();
                    int i = 0;

                    foreach (FirstLevelSubdivisionTable academy in academies)
                    {
                        CheckBoxList1.Items.Add(academy.Name);
                        CheckBoxList1.Items[i].Value = academy.FirstLevelSubdivisionTableID.ToString();
                        i++;
                    }        
                    FillGridVIews(0);
                }
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxName.Text.Length < 4)
            {
             Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                           "alert('Введите корректное название отчета');", true);         
            }
            else if (!(CalendarStartDateTime.SelectedDate > DateTime.MinValue))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                           "alert('Введите дату начала отчета');", true);
            }
            else if (!(CalendarEndDateTime.SelectedDate > DateTime.MinValue))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                           "alert('Введите дату конца отчета');", true);
            }
            else if (CalendarEndDateTime.SelectedDate < CalendarStartDateTime.SelectedDate)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                          "alert('Неправильно указаны даты');", true);
            }

            else
            {

                Serialization ReportId = (Serialization) Session["ReportArchiveTableID"];
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                ReportArchiveTable reportArchiveTable = new ReportArchiveTable();

                #region //запись в базу нового отчета или внесение изменений в старый

                int reportArchiveTableID = 0;
                if (ReportId.ReportArchiveID == 0) // создаем новую запись в БД и узнаем ей айди
                {
                    reportArchiveTable.Active = CheckBoxActive.Checked;
                    reportArchiveTable.Calculeted = CheckBoxCalculeted.Checked;
                    reportArchiveTable.Sent = CheckBoxSent.Checked;
                    reportArchiveTable.RecipientConfirmed = CheckBoxRecipientConfirmed.Checked;
                    reportArchiveTable.Name = TextBoxName.Text;

                    if (CalendarStartDateTime.SelectedDate > DateTime.MinValue)
                        reportArchiveTable.StartDateTime = CalendarStartDateTime.SelectedDate;

                    if (CalendarEndDateTime.SelectedDate > DateTime.MinValue)
                        reportArchiveTable.EndDateTime = CalendarEndDateTime.SelectedDate;

                    if (CalendarDateToSend.SelectedDate > DateTime.MinValue)
                        reportArchiveTable.DateToSend = CalendarDateToSend.SelectedDate;

                    if (CalendarSentDateTime.SelectedDate > DateTime.MinValue)
                        reportArchiveTable.SentDateTime = CalendarSentDateTime.SelectedDate;

                    if (CalendarReportRecived.SelectedDate > DateTime.MinValue)
                        reportArchiveTable.RecivedDateTime = CalendarReportRecived.SelectedDate;

                    kpiWebDataContext.ReportArchiveTable.InsertOnSubmit(reportArchiveTable);
                    kpiWebDataContext.SubmitChanges();

                    reportArchiveTableID = reportArchiveTable.ReportArchiveTableID;
                }

                else //если запись в бд уже есть
                {
                    reportArchiveTableID = ReportId.ReportArchiveID;
                    reportArchiveTable = (from item in kpiWebDataContext.ReportArchiveTable
                        where item.ReportArchiveTableID == reportArchiveTableID
                        select item).FirstOrDefault();
                }

                int rowIndex = 0;
                if (reportArchiveTable == null)
                    reportArchiveTable = new ReportArchiveTable();

                reportArchiveTable.Active = CheckBoxActive.Checked;
                reportArchiveTable.Calculeted = CheckBoxCalculeted.Checked;
                reportArchiveTable.Sent = CheckBoxSent.Checked;
                reportArchiveTable.RecipientConfirmed = CheckBoxRecipientConfirmed.Checked;
                reportArchiveTable.Name = TextBoxName.Text;

                if (CalendarStartDateTime.SelectedDate > DateTime.MinValue)
                    reportArchiveTable.StartDateTime = CalendarStartDateTime.SelectedDate;

                if (CalendarEndDateTime.SelectedDate > DateTime.MinValue)
                    reportArchiveTable.EndDateTime = CalendarEndDateTime.SelectedDate;

                if (CalendarDateToSend.SelectedDate > DateTime.MinValue)
                    reportArchiveTable.DateToSend = CalendarDateToSend.SelectedDate;

                if (CalendarSentDateTime.SelectedDate > DateTime.MinValue)
                    reportArchiveTable.SentDateTime = CalendarSentDateTime.SelectedDate;

                if (CalendarReportRecived.SelectedDate > DateTime.MinValue)
                    reportArchiveTable.RecivedDateTime = CalendarReportRecived.SelectedDate;

                #endregion

                #region //связь отчета со структурным подразд 1 уровня создание или изменение

                foreach (ListItem checkItem in CheckBoxList1.Items)
                {
                    if (checkItem.Selected)
                    {
                        ReportArchiveAndLevelMappingTable repNRole =
                            (from item in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                where item.FK_FirstLevelSubmisionTableId == Convert.ToInt32(checkItem.Value)
                                      && item.FK_ReportArchiveTableId == reportArchiveTableID
                                select item).FirstOrDefault();
                        if (repNRole != null)
                        {
                            repNRole.Active = true;
                        }
                        else
                        {
                            repNRole = new ReportArchiveAndLevelMappingTable();
                            repNRole.Active = true;
                            repNRole.FK_FirstLevelSubmisionTableId = Convert.ToInt32(checkItem.Value);
                            repNRole.FK_ReportArchiveTableId = reportArchiveTableID;
                            kpiWebDataContext.ReportArchiveAndLevelMappingTable.InsertOnSubmit(repNRole);
                        }
                    }
                    else
                    {
                        ReportArchiveAndLevelMappingTable repNRole =
                            (from item in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                where item.FK_FirstLevelSubmisionTableId == Convert.ToInt32(checkItem.Value)
                                      && item.FK_ReportArchiveTableId == reportArchiveTableID
                                select item).FirstOrDefault();
                        if (repNRole != null)
                        {
                            repNRole.Active = false; /// // / лучше просто удалить эту запись из БД
                        }
                    }
                }
                kpiWebDataContext.SubmitChanges();

                #endregion

                ////////////////////////////////////////////////////////пора записать данные в таблицы связей 

                if ((ViewState["BasicDataTable"] != null) && (ViewState["CalculateDataTable"] != null) &&
                    (ViewState["IndicatorDataTable"] != null))
                {
                    DataTable dt_basic = (DataTable) ViewState["BasicDataTable"];
                    DataTable dt_calculate = (DataTable) ViewState["CalculateDataTable"];
                    DataTable dt_indicator = (DataTable) ViewState["IndicatorDataTable"];
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////
                    for (int i = 0; i < dt_basic.Rows.Count; i++)
                    {
                        CheckBox chekBox = (CheckBox) BasicParametrsTable.Rows[i].FindControl("BasicParametrsCheckBox");
                        Label label = (Label) BasicParametrsTable.Rows[i].FindControl("BasicParametrsID");
                        if (chekBox.Checked == true)
                        {
                            ReportArchiveAndBasicParametrsMappingTable basicParam =
                                (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                    where a.FK_BasicParametrsTable == Convert.ToInt32(label.Text)
                                          && a.FK_ReportArchiveTable == reportArchiveTableID
                                    select a).FirstOrDefault();
                            if (basicParam != null)
                            {
                                basicParam.Active = true;
                            }
                            else
                            {
                                basicParam = new ReportArchiveAndBasicParametrsMappingTable();
                                basicParam.Active = true;
                                basicParam.FK_BasicParametrsTable = Convert.ToInt32(label.Text);
                                basicParam.FK_ReportArchiveTable = reportArchiveTableID;
                                kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable.InsertOnSubmit(basicParam);
                            }
                            kpiWebDataContext.SubmitChanges();
                        }
                        else
                        {
                            ReportArchiveAndBasicParametrsMappingTable basicParam =
                                (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                    where a.FK_BasicParametrsTable == Convert.ToInt32(label.Text)
                                          && a.FK_ReportArchiveTable == reportArchiveTableID
                                    select a).FirstOrDefault();
                            if (basicParam != null)
                            {
                                basicParam.Active = false;
                                kpiWebDataContext.SubmitChanges();
                            }
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////
                    List<int> calcId = new List<int>();

                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    for (int i = 0; i < dt_calculate.Rows.Count; i++)
                    {
                        CheckBox chekBox =
                            (CheckBox) CalculatedParametrsTable.Rows[i].FindControl("CalculatedParametrsCheckBox");
                        Label label = (Label) CalculatedParametrsTable.Rows[i].FindControl("CalculatedParametrsID");
                        if (chekBox.Checked == true)
                        {
                            calcId.Add(Convert.ToInt32(label.Text));
                            ReportArchiveAndCalculatedParametrsMappingTable calcParam =
                                (from a in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                    where a.FK_CalculatedParametrsTable == Convert.ToInt32(label.Text)
                                          && a.FK_ReportArchiveTable == reportArchiveTableID
                                    select a).FirstOrDefault();
                            if (calcParam != null)
                            {
                                calcParam.Active = true;
                            }
                            else
                            {
                                calcParam = new ReportArchiveAndCalculatedParametrsMappingTable();
                                calcParam.Active = true;
                                calcParam.FK_CalculatedParametrsTable = Convert.ToInt32(label.Text);
                                calcParam.FK_ReportArchiveTable = reportArchiveTableID;
                                kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable.InsertOnSubmit(
                                    calcParam);
                            }
                            kpiWebDataContext.SubmitChanges();
                        }
                        else
                        {
                            ReportArchiveAndCalculatedParametrsMappingTable calcParam =
                                (from a in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                    where a.FK_CalculatedParametrsTable == Convert.ToInt32(label.Text)
                                          && a.FK_ReportArchiveTable == reportArchiveTableID
                                    select a).FirstOrDefault();
                            if (calcParam != null)
                            {
                                calcParam.Active = false;
                                kpiWebDataContext.SubmitChanges();
                            }
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////
                    List<int> indId = new List<int>();
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    for (int i = 0; i < dt_indicator.Rows.Count; i++)
                    {
                        CheckBox chekBox = (CheckBox) IndicatorsTable.Rows[i].FindControl("IndicatorCheckBox");
                        Label label = (Label) IndicatorsTable.Rows[i].FindControl("IndicatorID");
                        if (chekBox.Checked == true)
                        {
                            indId.Add(Convert.ToInt32(label.Text));
                            ReportArchiveAndIndicatorsMappingTable indicators =
                                (from a in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                    where a.FK_IndicatorsTable == Convert.ToInt32(label.Text)
                                          && a.FK_ReportArchiveTable == reportArchiveTableID
                                    select a).FirstOrDefault();
                            if (indicators != null)
                            {
                                indicators.Active = true;
                            }
                            else
                            {
                                indicators = new ReportArchiveAndIndicatorsMappingTable();
                                indicators.Active = true;
                                indicators.FK_IndicatorsTable = Convert.ToInt32(label.Text);
                                indicators.FK_ReportArchiveTable = reportArchiveTableID;
                                kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable.InsertOnSubmit(indicators);
                            }
                            kpiWebDataContext.SubmitChanges();
                        }
                        else
                        {
                            ReportArchiveAndIndicatorsMappingTable indicators =
                                (from a in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                    where a.FK_IndicatorsTable == Convert.ToInt32(label.Text)
                                          && a.FK_ReportArchiveTable == reportArchiveTableID
                                    select a).FirstOrDefault();
                            if (indicators != null)
                            {
                                indicators.Active = false;
                                kpiWebDataContext.SubmitChanges();
                            }
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///нужнополучить список айдишников нужных базовых показателей
                    List<int> CalcID = getCalcByIndicator(indId);
                    foreach (int tmp in CalcID)
                    {
                        calcId.Add(tmp);
                    }

                    #region

                    foreach (int CalcParamID in calcId)
                    {
                        ReportArchiveAndCalculatedParametrsMappingTable calcParam =
                            (from a in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                where a.FK_CalculatedParametrsTable == CalcParamID
                                      && a.FK_ReportArchiveTable == reportArchiveTableID
                                select a).FirstOrDefault();
                        if (calcParam != null)
                        {
                            calcParam.Active = true;
                        }
                        else
                        {
                            calcParam = new ReportArchiveAndCalculatedParametrsMappingTable();
                            calcParam.Active = true;
                            calcParam.FK_CalculatedParametrsTable = CalcParamID;
                            calcParam.FK_ReportArchiveTable = reportArchiveTableID;
                            kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable.InsertOnSubmit(calcParam);
                        }
                        kpiWebDataContext.SubmitChanges();
                    }

                    #endregion

                    List<int> BaseID = getBasicByCalc(calcId);

                    #region

                    foreach (int baseID in BaseID)
                    {
                        ReportArchiveAndBasicParametrsMappingTable basicParam =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                where a.FK_BasicParametrsTable == baseID
                                      && a.FK_ReportArchiveTable == reportArchiveTableID
                                select a).FirstOrDefault();
                        if (basicParam != null)
                        {
                            basicParam.Active = true;
                        }
                        else
                        {
                            basicParam = new ReportArchiveAndBasicParametrsMappingTable();
                            basicParam.Active = true;
                            basicParam.FK_BasicParametrsTable = baseID;
                            basicParam.FK_ReportArchiveTable = reportArchiveTableID;
                            kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable.InsertOnSubmit(basicParam);
                        }
                        kpiWebDataContext.SubmitChanges();
                    }

                    #endregion

                }
                Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");
            }
        }

        protected void CalendarStartDateTime_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void BasicParametrsTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {   
            Serialization ReportId = (Serialization) Session["ReportArchiveTableID"];
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            var deleteBasic = from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     where a.FK_ReportArchiveTable == ReportId.ReportArchiveID select a;
            var deleteCalc = from a in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                     where a.FK_ReportArchiveTable == ReportId.ReportArchiveID select a;
            var deleteIndicator = from a in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                     where a.FK_ReportArchiveTable == ReportId.ReportArchiveID select a;
            foreach (var delB in deleteBasic)
            {
                kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable.DeleteOnSubmit(delB);
            }
            foreach (var delC in deleteCalc)
            {
                kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable.DeleteOnSubmit(delC);
            }
            foreach (var delI in deleteIndicator)
            {
                kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable.DeleteOnSubmit(delI);
            }
            kpiWebDataContext.SubmitChanges();
            Response.Redirect("~/StatisticsDepartment/ReportViewer.aspx");                     
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IndicatorsTable.Rows.Count; i++)
            {
                CheckBox chekBox = (CheckBox)IndicatorsTable.Rows[i].FindControl("IndicatorCheckBox");
                chekBox.Checked = true;
            }

            for (int i = 0; i < CalculatedParametrsTable.Rows.Count; i++)
            {
                CheckBox chekBox = (CheckBox)CalculatedParametrsTable.Rows[i].FindControl("CalculatedParametrsCheckBox");
                chekBox.Checked = true;
            }

            for (int i = 0; i < BasicParametrsTable.Rows.Count; i++)
            {
                CheckBox chekBox = (CheckBox)BasicParametrsTable.Rows[i].FindControl("BasicParametrsCheckBox");
                chekBox.Checked = true;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
        }
    }
}
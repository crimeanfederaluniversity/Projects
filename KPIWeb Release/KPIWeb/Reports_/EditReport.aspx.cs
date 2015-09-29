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
        [Serializable]
        public class Struct // класс описываюший структурные подразделения
        {
            public int Lv_0 { get; set; }
            public int Lv_1 { get; set; }
            public int Lv_2 { get; set; }
            public int Lv_3 { get; set; }
            public int Lv_4 { get; set; }
            public int Lv_5 { get; set; }
            public Struct(int lv0, int lv1, int lv2, int lv3, int lv4, int lv5)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = lv4;
                Lv_5 = lv5;
            }
        }
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked) // Рекурсивно проставляем галочки
        {
            foreach (TreeNode node in treeNode.ChildNodes)
            {
                node.Checked = nodeChecked;
                if (node.ChildNodes.Count > 0)
                {
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        public class MyObject
        {
            public int Id;
            public int ParentId;
            public string Name;
            public bool Checked;
        }
        private void BindTree(IEnumerable<MyObject> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));

            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.Name, node.Id.ToString());
                newNode.Checked = node.Checked;
                newNode.SelectAction = TreeNodeSelectAction.None;
                if (parentNode == null)
                {
                    TreeView1.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTree(list, newNode);
            }
        }
        public bool CheckIfChecked(int lv0, int Lv_1, int Lv_2, int Lv_3, int Lv_4, int Lv_5, int ReportID)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            ReportArchiveAndLevelMappingTable CurrentConnection
           = (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
              where a.FK_ReportArchiveTableId == ReportID
                 && (a.FK_FirstLevelSubmisionTableId == Lv_1 || (Lv_1 == 0 && a.FK_FirstLevelSubmisionTableId == null))
                 && (a.FK_SecondLevelSubdivisionTable == Lv_2 || (Lv_2 == 0 && a.FK_SecondLevelSubdivisionTable == null))
                 && (a.FK_ThirdLevelSubdivisionTable == Lv_3 || (Lv_3 == 0 && a.FK_ThirdLevelSubdivisionTable == null))
                 && (a.FK_FourthLevelSubdivisionTable == Lv_4 || (Lv_4 == 0 && a.FK_FourthLevelSubdivisionTable == null))
                 && (a.FK_FifthLevelSubdivisionTable == Lv_5 || (Lv_5 == 0 && a.FK_FifthLevelSubdivisionTable == null))
              select a).FirstOrDefault();
            if (CurrentConnection == null)
            {
                return false;
            }
            if (CurrentConnection.Active == false)
            {
                return false;
            }
            return true;
        }
        protected void FillGridVIews(int reportID)
        {   
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////                
            List<IndicatorsTable> indicatorTable =
            (from item in kPiDataContext.IndicatorsTable where item.Active == true select item).OrderBy(c => c.SortID).ToList();
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
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                  int reportArchiveTableID = 0;
                if (ReportId.ReportArchiveID != 0)
                {
                    reportArchiveTableID  = ReportId.ReportArchiveID;

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

                        if (ReportArchiveTable.ConfirmEndDay != null)
                            CalendarConfirmEndDay.SelectedDate = (DateTime)ReportArchiveTable.ConfirmEndDay;

                        if (ReportArchiveTable.DaysBeforeToCalcForRector != null)
                            DaysBeforeToCalcForRector.Text = ReportArchiveTable.DaysBeforeToCalcForRector.ToString();
                    }

                    
                    
                    ////заполнили поля
                    List<FirstLevelSubdivisionTable> academies =
                        (from item in KPIWebDataContext.FirstLevelSubdivisionTable
                         where item.Active == true
                         select item).ToList();
                   // int i = 0;

                    FillGridVIews(reportArchiveTableID);
                }
                else //создаем новый отчёт
                {
                    
                    ButtonSave.Text="Сохранить новую кампанию";

                    List<FirstLevelSubdivisionTable> academies =
                        (from item in KPIWebDataContext.FirstLevelSubdivisionTable
                         where item.Active == true
                         select item).ToList();
                  //  int i = 0;            
                    FillGridVIews(0);
                }

                {
                    TreeView1.Attributes.Add("onclick", "postBackByObject()");
                    TreeView1.DataSource = null;
                    TreeView1.DataBind();
                    List<MyObject> list = new List<MyObject>();
                    Dictionary<int, Struct> CollectedStruct = new Dictionary<int, Struct>();
                    int i = 1;
                    string tmp2;
                    #region get zero leve list

                    List<ZeroLevelSubdivisionTable> zeroLevelList = (from a in kPiDataContext.ZeroLevelSubdivisionTable
                                                                     where a.Active == true
                                                                     select a).ToList();
                    #endregion
                    foreach (ZeroLevelSubdivisionTable zeroLevelItem in zeroLevelList)//по каждому университету
                    {
                        #region get first level list
                        List<FirstLevelSubdivisionTable> firstLevelList = (from b in kPiDataContext.FirstLevelSubdivisionTable

                                                                           where b.FK_ZeroLevelSubvisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                                                                                 && b.Active == true
                                                                           select b).ToList();
                        #endregion
                        i++;
                        //  CollectedStruct.Add(i, new Struct(zeroLevelItem.ZeroLevelSubdivisionTableID, 0, 0, 0, 0, 0));

                        int par0 = i;
                        tmp2 = zeroLevelItem.Name;
                        if (tmp2 != null)
                            list.Add(new MyObject() { Id = i, ParentId = 0, Name = tmp2, Checked = false });
                        foreach (FirstLevelSubdivisionTable firstLevelItem in firstLevelList)//по каждой академии
                        {
                            #region get second level list
                            List<SecondLevelSubdivisionTable> secondLevelList =
                                (from d in kPiDataContext.SecondLevelSubdivisionTable
                                 where d.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                 && d.Active == true
                                 select d).ToList();
                            #endregion
                            i++;
                            CollectedStruct.Add(i, new Struct(zeroLevelItem.ZeroLevelSubdivisionTableID,
                                firstLevelItem.FirstLevelSubdivisionTableID, 0, 0, 0, 0));

                            int par1 = i;
                            tmp2 = firstLevelItem.Name;
                            if (tmp2 != null)
                                list.Add(new MyObject() { Id = i, ParentId = par0, Name = tmp2, Checked = CheckIfChecked(zeroLevelItem.ZeroLevelSubdivisionTableID, firstLevelItem.FirstLevelSubdivisionTableID, 0, 0, 0, 0, reportArchiveTableID) });
                            foreach (SecondLevelSubdivisionTable secondLevelItem in secondLevelList)//по каждому факультету
                            {
                                #region get third level list
                                List<ThirdLevelSubdivisionTable> thirdLevelList =
                                    (from f in kPiDataContext.ThirdLevelSubdivisionTable
                                     where f.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                     && f.Active == true
                                     select f).ToList();
                                #endregion
                                i++;
                                CollectedStruct.Add(i, new Struct(zeroLevelItem.ZeroLevelSubdivisionTableID,
                                    firstLevelItem.FirstLevelSubdivisionTableID, secondLevelItem.SecondLevelSubdivisionTableID, 0, 0, 0));

                                int par2 = i;
                                tmp2 = secondLevelItem.Name;
                                if (tmp2 != null)
                                    list.Add(new MyObject() { Id = i, ParentId = par1, Name = tmp2, Checked = CheckIfChecked(zeroLevelItem.ZeroLevelSubdivisionTableID, firstLevelItem.FirstLevelSubdivisionTableID, secondLevelItem.SecondLevelSubdivisionTableID, 0, 0, 0, reportArchiveTableID) });
                                foreach (ThirdLevelSubdivisionTable thirdLevelItem in thirdLevelList)//по кафедре
                                {
                                    i++;
                                    CollectedStruct.Add(i, new Struct(zeroLevelItem.ZeroLevelSubdivisionTableID,
                                        firstLevelItem.FirstLevelSubdivisionTableID, secondLevelItem.SecondLevelSubdivisionTableID,
                                        thirdLevelItem.ThirdLevelSubdivisionTableID, 0, 0));
                                    int par3 = i;
                                    tmp2 = thirdLevelItem.Name;
                                    if (tmp2 != null)
                                        list.Add(new MyObject() { Id = i, ParentId = par2, Name = tmp2, Checked = CheckIfChecked(zeroLevelItem.ZeroLevelSubdivisionTableID, firstLevelItem.FirstLevelSubdivisionTableID, secondLevelItem.SecondLevelSubdivisionTableID, thirdLevelItem.ThirdLevelSubdivisionTableID, 0, 0, reportArchiveTableID) });
                                }
                            }
                        }
                    }
                    BindTree(list, null);
                    TreeView1.CollapseAll();
                    ViewState["CollectedStruct"] = CollectedStruct;
                }
                ////заполнили дерево
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBoxName.Text.Length < 4)
            {
             Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                           "alert('Введите корректное название отчёта');", true);         
            }
            else if (!(CalendarStartDateTime.SelectedDate > DateTime.MinValue))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                           "alert('Введите дату начала отчёта');", true);
            }
            else if (!(CalendarEndDateTime.SelectedDate > DateTime.MinValue))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                           "alert('Введите дату конца отчёта');", true);
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
                

                #region //запись в базу нового отчёта или внесение изменений в старый
                ReportArchiveTable reportArchiveTable;
                int reportArchiveTableID = 0;
                if (ReportId.ReportArchiveID == 0) // создаем новую запись в БД и узнаем ей айди
                {
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
                   
                    if (CalendarConfirmEndDay.SelectedDate > DateTime.MinValue)
                        reportArchiveTable.ConfirmEndDay = CalendarConfirmEndDay.SelectedDate;

                    if (DaysBeforeToCalcForRector.Text != "")
                        reportArchiveTable.DaysBeforeToCalcForRector = Convert.ToInt32(DaysBeforeToCalcForRector.Text);

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

                if (CalendarConfirmEndDay.SelectedDate > DateTime.MinValue)
                    reportArchiveTable.ConfirmEndDay = CalendarConfirmEndDay.SelectedDate;

                if (DaysBeforeToCalcForRector.Text != "")
                    reportArchiveTable.DaysBeforeToCalcForRector =  Convert.ToInt32(DaysBeforeToCalcForRector.Text);

                #endregion

                //соединяем отчет и струтуру
                List<ReportArchiveAndLevelMappingTable> ExistingList = (from a in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                                        where a.FK_ReportArchiveTableId == reportArchiveTableID
                                                                        select a).ToList();
                foreach (ReportArchiveAndLevelMappingTable currentConnect in ExistingList)
                {
                    currentConnect.Active = false;
                }
                kpiWebDataContext.SubmitChanges();

                Dictionary<int, Struct> CollectedStruct = (Dictionary<int, Struct>)ViewState["CollectedStruct"];

                foreach (TreeNode Node in TreeView1.CheckedNodes)
                {
                    Struct CurrentStruct;
                    if (CollectedStruct.TryGetValue(Convert.ToInt32(Node.Value), out CurrentStruct))
                    {
                        ReportArchiveAndLevelMappingTable CurrentConnection
                       = (from a in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                          where a.FK_ReportArchiveTableId == reportArchiveTableID
                             && (a.FK_FirstLevelSubmisionTableId == CurrentStruct.Lv_1 || (CurrentStruct.Lv_1 == 0 && a.FK_FirstLevelSubmisionTableId == null))
                             && (a.FK_SecondLevelSubdivisionTable == CurrentStruct.Lv_2 || (CurrentStruct.Lv_2 == 0 && a.FK_SecondLevelSubdivisionTable == null))
                             && (a.FK_ThirdLevelSubdivisionTable == CurrentStruct.Lv_3 || (CurrentStruct.Lv_3 == 0 && a.FK_ThirdLevelSubdivisionTable == null))
                             && (a.FK_FourthLevelSubdivisionTable == CurrentStruct.Lv_4 || (CurrentStruct.Lv_4 == 0 && a.FK_FourthLevelSubdivisionTable == null))
                             && (a.FK_FifthLevelSubdivisionTable == CurrentStruct.Lv_5 || (CurrentStruct.Lv_5 == 0 && a.FK_FifthLevelSubdivisionTable == null))
                          select a).FirstOrDefault();
                        if (CurrentConnection == null)
                        {
                            CurrentConnection = new ReportArchiveAndLevelMappingTable();
                            CurrentConnection.FK_ReportArchiveTableId = reportArchiveTableID;
                            CurrentConnection.Active = true;
                            if (CurrentStruct.Lv_1 != 0)
                                CurrentConnection.FK_FirstLevelSubmisionTableId = CurrentStruct.Lv_1;
                            if (CurrentStruct.Lv_2 != 0)
                                CurrentConnection.FK_SecondLevelSubdivisionTable = CurrentStruct.Lv_2;
                            if (CurrentStruct.Lv_3 != 0)
                                CurrentConnection.FK_ThirdLevelSubdivisionTable = CurrentStruct.Lv_3;
                            if (CurrentStruct.Lv_4 != 0)
                                CurrentConnection.FK_FourthLevelSubdivisionTable = CurrentStruct.Lv_4;
                            if (CurrentStruct.Lv_5 != 0)
                                CurrentConnection.FK_FifthLevelSubdivisionTable = CurrentStruct.Lv_5;
                            kpiWebDataContext.ReportArchiveAndLevelMappingTable.InsertOnSubmit(CurrentConnection);
                            kpiWebDataContext.SubmitChanges();
                        }
                        else
                        {
                            CurrentConnection.Active = true;
                            kpiWebDataContext.SubmitChanges();
                        }
                    }
                }
                //соединяем отчет и струтуру

                /*
                #region //связь отчёта со структурным подразд 1 уровня создание или изменение

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
                */
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

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.ChildNodes.Count > 0)
            {
                this.CheckAllChildNodes(e.Node, e.Node.Checked);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        public int nodeChecked(string Value, TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.ChildNodes)
            {
                if (node.Value == Value)
                {
                    if (node.Checked)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (node.ChildNodes.Count > 0)
                    {
                        int i = nodeChecked(Value, node);
                        if (i != 0)
                        {
                            return i;
                        }
                    }
                    return 0;
                }

            }
            return 0;
        }

    }
}
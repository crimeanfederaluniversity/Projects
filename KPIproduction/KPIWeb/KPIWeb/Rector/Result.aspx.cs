using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class Result : System.Web.UI.Page
    {
        [Serializable]
        public class Struct // класс описываюший структурные подразделения
        {
            public int Lv_0 { get; set; }
            public int Lv_1 { get; set; }
            public int Lv_2 { get; set; }
            public int Lv_3 { get; set; }
            public int Lv_4 { get; set; }
            public int Lv_5 { get; set; }

            public string Name { get; set; }
            
            public Struct(int lv0, int lv1, int lv2, int lv3, int lv4, int lv5, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = lv4;
                Lv_5 = lv5;
                Name = name;
            }
            public Struct(int lv0, int lv1, int lv2, int lv3, int lv4, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = lv4;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, int lv1, int lv2, int lv3, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = lv3;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, int lv1, int lv2, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = lv2;
                Lv_3 = 0;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, int lv1, string name)
            {
                Lv_0 = lv0;
                Lv_1 = lv1;
                Lv_2 = 0;
                Lv_3 = 0;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
            public Struct(int lv0, string name)
            {
                Lv_0 = lv0;
                Lv_1 = 0;
                Lv_2 = 0;
                Lv_3 = 0;
                Lv_4 = 0;
                Lv_5 = 0;
                Name = name;
            }
        } 
        public List<Struct> GetChildStructList (Struct ParentStruct) 
        {
            List<Struct> tmpStrucList = new List<Struct>();
            int Level = 5;
            Level = ParentStruct.Lv_5 == 0 ? 4 : Level;
            Level = ParentStruct.Lv_4 == 0 ? 3 : Level;
            Level = ParentStruct.Lv_3 == 0 ? 2 : Level;
            Level = ParentStruct.Lv_2 == 0 ? 1 : Level;
            Level = ParentStruct.Lv_1 == 0 ? 0 : Level;
            Level = ParentStruct.Lv_0 == 0 ? -1 : Level;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            switch (Level)
            {
                case -1: // возвращаем все нулевого уровня, хотя там должен быть только КФУ
                {
                    tmpStrucList = (from a in kpiWebDataContext.ZeroLevelSubdivisionTable
                        where a.Active == true
                                    select new Struct(1,"") { 
                                        Lv_0 = (int)a.ZeroLevelSubdivisionTableID,  
                                        Lv_1 = 0,
                                        Lv_2 = 0,
                                        Lv_3 = 0,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = a.Name
                                    }).ToList();
                    break;
                }
                case 0: // возвращаем все универы
                {
                    tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    where a.Active == true
                                    && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                    select new Struct(1,"")
                                    {
                                        Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                        Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                        Lv_2 = 0,
                                        Lv_3 = 0,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = a.Name
                                    }).ToList();
                    break;
                }
                case 1: // возвращаем все факультеты
                {
                    tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                    on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                    where a.Active == true
                                    && b.Active == true
                                    && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                    && b.FK_FirstLevelSubdivisionTable == ParentStruct.Lv_1
                                    select new Struct(1,"")
                                    {
                                        Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                        Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                        Lv_2 = (int)b.SecondLevelSubdivisionTableID,
                                        Lv_3 = 0,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = b.Name
                                    }).ToList();
                    break;
                }
                case 2: // возвращаем все кафедры
                {
                    tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                    on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                    join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                    on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                    where a.Active == true
                                    && b.Active == true
                                    && c.Active == true
                                    && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                    && b.FK_FirstLevelSubdivisionTable == ParentStruct.Lv_1
                                    && c.FK_SecondLevelSubdivisionTable == ParentStruct.Lv_2
                                    select new Struct(1,"")
                                    {
                                        Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                        Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                        Lv_2 = (int)b.SecondLevelSubdivisionTableID,
                                        Lv_3 = (int)c.ThirdLevelSubdivisionTableID,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = c.Name
                                    }).ToList();
                    break;
                }
                default:
                {
                    //error не будем раскладывать до специальностей
                    break;
                }
            }

            return tmpStrucList;
        }
        public float GetCalculatedWithParams(Struct StructToCalcFor, int ParamType, int ParamID,int ReportID, int SpecID) // читает показатель
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
          
            float result = 0;
            if (ParamType == 0) // считаем индикатор
            {
                IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                    where a.IndicatorsTableID == ParamID
                    select a).FirstOrDefault();
                    return
                        (float) CalculateAbb.CalculateForLevel(1, Abbreviature.CalculatedAbbToFormula(Indicator.Formula)
                            , ReportID, SpecID, StructToCalcFor.Lv_0
                            , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4,
                            StructToCalcFor.Lv_5, 0);
            }
            else if (ParamType == 1) // считаем рассчетный
            {
                CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                                             where a.CalculatedParametrsID == ParamID
                                             select a).FirstOrDefault();
                return (float)CalculateAbb.CalculateForLevel(1, Calculated.Formula, ReportID, SpecID , StructToCalcFor.Lv_0
                        , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4,
                        StructToCalcFor.Lv_5, 0);
            }
            else if (ParamType == 2) // суммируем базовый
            {
                    return (float) CalculateAbb.SumForLevel(ParamID, ReportID,SpecID, StructToCalcFor.Lv_0
                        , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4,
                        StructToCalcFor.Lv_5);
            }
            else
            {
                //error
            }
            return result;
        }
        public int StructDeepness(Struct CurrentStruct)
        {
            int tmp=0;
            if (CurrentStruct.Lv_0 != 0) tmp++;
            if (CurrentStruct.Lv_1 != 0) tmp++;
            if (CurrentStruct.Lv_2 != 0) tmp++;
            if (CurrentStruct.Lv_3 != 0) tmp++;
            if (CurrentStruct.Lv_4 != 0) tmp++;
            if (CurrentStruct.Lv_5 != 0) tmp++;
            return tmp;
        }
        public Struct StructDeeper(Struct parentStruct, int nextID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            int lv0 = parentStruct.Lv_0;
            int lv1 = parentStruct.Lv_1;
            int lv2 = parentStruct.Lv_2;
            int lv3 = parentStruct.Lv_3;
            int lv4 = parentStruct.Lv_4;
            int lv5 = parentStruct.Lv_5;
            string name = parentStruct.Name;

            Struct tmp = new Struct(lv0,lv1,lv2,lv3,lv4,lv5,name);

            if (tmp.Lv_0 == 0)
            {
                tmp.Lv_0 = nextID;
                tmp.Name = (from a in kpiWebDataContext.ZeroLevelSubdivisionTable
                    where a.ZeroLevelSubdivisionTableID == nextID
                    select a.Name).FirstOrDefault();
                return tmp;
            }

            if (tmp.Lv_1 == 0)
            {
                tmp.Lv_1 = nextID;
                tmp.Name = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == nextID
                            select a.Name).FirstOrDefault();
                return tmp;
            }

            if (tmp.Lv_2 == 0)
            {
                tmp.Lv_2 = nextID;
                tmp.Name = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.SecondLevelSubdivisionTableID == nextID
                            select a.Name).FirstOrDefault();
                return tmp;
            }

            if (tmp.Lv_3 == 0)
            {
                tmp.Lv_3 = nextID;
                tmp.Name = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                            where a.ThirdLevelSubdivisionTableID == nextID
                            select a.Name).FirstOrDefault();
                return tmp;
            }
            if (tmp.Lv_4 == 0)
            {
                tmp.Lv_4 = nextID;
                tmp.Name = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                            where a.FourthLevelSubdivisionTableID == nextID
                            select a.Name).FirstOrDefault();
                return tmp;
            }
            if (tmp.Lv_5 == 0)
            {
                tmp.Lv_5 = nextID;
                tmp.Name = (from a in kpiWebDataContext.FifthLevelSubdivisionTable
                            where a.FifthLevelSubdivisionTableID == nextID
                            select a.Name).FirstOrDefault();
                return tmp;
            }
            return tmp;
        } //добавляет ID к первому в структуре нулю
        public int GetLastID(Struct currentStruct)
        {
            if (currentStruct.Lv_0 == 0)
            {
                return 0;
            }

            if (currentStruct.Lv_1 == 0)
            {
                return currentStruct.Lv_0;
            }

            if (currentStruct.Lv_2 == 0)
            {
                return currentStruct.Lv_1;
            }

            if (currentStruct.Lv_3 == 0)
            {
                return currentStruct.Lv_2;
            }

            if (currentStruct.Lv_4 == 0)
            {
                return currentStruct.Lv_3;
            }
            return 0;
        }  //определяет последнее не нулевое значение в структуре
        protected void Page_Load(object sender, EventArgs e)
        {
            #region get user data
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
        
            UsersTable userTable_ =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable_.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            #endregion
            if (!IsPostBack)
            {
                #region session
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                #region check for get
                string val = this.Request.QueryString["HLevel"];//hisoty level сова придумал)
                if (val != null)
                {
                    rectorHistory.CurrentSession = Convert.ToInt32(val);
                    Session["rectorHistory"] = rectorHistory;
                }
                #endregion

                if ((rectorHistory.SessionCount-rectorHistory.CurrentSession)<2)
                {
                    GoForwardButton.Enabled = false;
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;             
                #endregion
                #region DataTable init
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Abb", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Value", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Title", typeof(string)));

                dataTable.Columns.Add(new DataColumn("CanConfirm", typeof(bool)));
                

                #endregion 
                #region global page settings
                ReportTitle.Text = (from a in kpiWebDataContext.ReportArchiveTable
                    where a.ReportArchiveTableID == ReportID
                    select a.Name).FirstOrDefault().ToString();
                #endregion
                if (ViewType == 0) // просмотр для структурных подразделений
                {
                    #region преднастройка страницы                    
                    string title="";
                    if (mainStruct.Lv_1 == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "По университетам КФУ");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, mainStruct.Name);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }                   
                    //задади имя текущей сессии
                    if (SpecID != 0)
                    {
                        SpecName.Visible = true;
                        SpecName.Text = "Для специальности " + (from a in kpiWebDataContext.SpecializationTable
                                                                where a.SpecializationTableID == SpecID 
                                                                select a.Name).FirstOrDefault();
                    }

                    if (ParamType==0)
                    {
                        PageName.Text = "Значения для индикатора; \"";
                        PageName.Text += (from a in kpiWebDataContext.IndicatorsTable
                                          where a.IndicatorsTableID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\";";
                    }
                    else if (ParamType == 1)
                    {
                        PageName.Text = "Значения для расчетного показателя: \"";
                        PageName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                                          where a.CalculatedParametrsID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\";";
                    }
                    else if (ParamType == 2)
                    {
                        PageName.Text = "Значения для базового показателя: \"";
                        PageName.Text += (from a in kpiWebDataContext.BasicParametersTable
                                          where a.BasicParametersTableID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\";";
                    }
                    #region useless switch
                    int tmpLevel = StructDeepness(mainStruct);
                    switch (tmpLevel)
                    {
                        case 0:
                        {
                            title = "Подразделения";
                            break;
                        }
                        case 1:
                        {
                            title = "Подразделения";
                            break;
                        }
                        case 2:
                        {
                            title = "Подразделения";
                            break;
                        }
                        case 3:
                        {
                            title = "Подразделения";
                            break;
                        }
                        case 4:
                        {
                            title = "Подразделения";
                            break;
                        }
                        case 5:
                        {
                            title = "Подразделения";
                            break;
                        }
                        default:
                        {
                            title = "Подразделения";
                            break;
                        }
                    }
                    #endregion

                    #endregion
                    #region fill grid
                        List<Struct> currentStructList = GetChildStructList(mainStruct);
                        foreach (Struct currentStruct in currentStructList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = currentStruct.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Value"] = GetCalculatedWithParams(currentStruct, ParamType, ParamID, ReportID,SpecID).ToString();
                            dataTable.Rows.Add(dataRow);
                        }                                          
                    #endregion
                    #region DataGridBind
                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = title;
                    Grid.DataBind();
                    #endregion
                    #region постнастройка страницы

                    Grid.Columns[10].Visible = false;
                    Grid.Columns[9].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;
                    if (StructDeepness(mainStruct) > 2) // дальше углубляться нельзя
                    {
                        Grid.Columns[8].Visible = false;
                    }
                    #endregion
                }
                else if (ViewType == 1) // просмотр для показателей (верхние 3 шт)
                {
                    #region преднастройка страницы
                    string title="";
                    if (ParamType==0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "Все индикаторы по КФУ");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                        PageName.Text = "Значения индикторов для КФУ";
                        title = "Индикаторы";                      
                    }
                    else if (ParamType == 1)
                    {
                        string tmp = (from a in kpiWebDataContext.IndicatorsTable
                                      where a.IndicatorsTableID == ParamID
                                      select a.Name).FirstOrDefault();

                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "Расчетные показалели для индикатора: " + tmp);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageName.Text = "Значения расчетных показателей используемых для расчета индикатора: \"";
                        PageName.Text += tmp;
                        PageName.Text += "\" для КФУ";
                        title = "Расчетные показатели";
                        FormulaLable.Text = (from a in kpiWebDataContext.IndicatorsTable
                            where a.IndicatorsTableID == ParamID
                            select a.Formula).FirstOrDefault();
                        FormulaLable.Visible = true;
                    }
                    else if (ParamType == 2)
                    {
                        string tmp = (from a in kpiWebDataContext.CalculatedParametrs
                                      where a.CalculatedParametrsID == ParamID
                                      select a.Name).FirstOrDefault();
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"Базовые показатели для расчетного: "+ tmp);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageName.Text = "Значения базовых показателей используемых для расчета расчетного показателя\"";
                        PageName.Text += tmp;
                        PageName.Text += "\" для КФУ";
                        title = "Базовые показатели";
                        FormulaLable.Text = (from a in kpiWebDataContext.CalculatedParametrs
                                             where a.CalculatedParametrsID == ParamID
                                             select a.Formula).FirstOrDefault();
                        FormulaLable.Visible = true;
                    }

                    #endregion
                    #region fill grid
                    if (ParamType == 0)//считаем индикатор
                    {
                        List<IndicatorsTable> Indicators = (
                            from a in kpiWebDataContext.IndicatorsTable
                            join b in kpiWebDataContext.IndicatorsAndUsersMapping
                            on a.IndicatorsTableID equals b.FK_IndicatorsTable
                            where 
                            a.Active == true
                            && b.CanView == true
                            && b.FK_UsresTable == userID
                            select a).ToList();
                        
                        //нашли все индикаторы привязанные к пользователю
                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentIndicator.IndicatorsTableID;//GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrentIndicator.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            #region user can edit
                            #endregion 
                            #region get calculated if confirmed; calculate if not confirmed
                            CollectedIndocators collected = (from a in kpiWebDataContext.CollectedIndocators
                                                                 where a.FK_ReportArchiveTable == ReportID
                                                                 && a.FK_Indicators == CurrentIndicator.IndicatorsTableID
                                                                 select a).FirstOrDefault();
                            if (collected == null)
                            {
                                collected = new CollectedIndocators();
                                collected.FK_Indicators = CurrentIndicator.IndicatorsTableID;
                                collected.FK_ReportArchiveTable = ReportID;
                                collected.FK_UsersTable = userID;
                                collected.Confirmed = false;
                                collected.LastChangeDateTime = DateTime.Now;
                                collected.Active = true;
                                collected.CollectedValue = 12;
                                kpiWebDataContext.CollectedIndocators.InsertOnSubmit(collected);
                                kpiWebDataContext.SubmitChanges();
                            }
                            if (collected.Confirmed == true)
                            {
                                dataRow["CanConfirm"] = false;
                                dataRow["Value"] = collected.CollectedValue;
                            }
                            else
                            {
                                dataRow["CanConfirm"] = true;
                                dataRow["Value"] =GetCalculatedWithParams(mainStruct, ParamType, CurrentIndicator.IndicatorsTableID, ReportID, SpecID).ToString();
                            }
                            #endregion
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    if (ParamType == 1) //показываем рассчетный входящий в ID Индикатора
                    {
                        //ID  - это айди Индиктора
                        List<CalculatedParametrs> CalculatedList;
                        if (ParamID != 0)
                        {
                            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                where a.IndicatorsTableID == ParamID
                                select a).FirstOrDefault();
                             CalculatedList = Abbreviature.GetCalculatedList(Indicator.Formula);
                        }
                        else
                        {
                            CalculatedList = (from a in kpiWebDataContext.CalculatedParametrs
                                join b in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                                    on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                                where a.Active == true
                                      && b.CanView == true
                                      && b.FK_UsersTable == userID
                                select a).ToList();
                        }
                        foreach (CalculatedParametrs CurrentCalculated in CalculatedList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentCalculated.CalculatedParametrsID;//GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrentCalculated.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Abb"] = CurrentCalculated.AbbreviationEN;

                            #region get calculated if confirmed; calculate if not confirmed
                            CollectedCalculatedParametrs collected = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                                             where a.FK_ReportArchiveTable == ReportID
                                                             && a.FK_CalculatedParametrs == CurrentCalculated.CalculatedParametrsID
                                                             select a).FirstOrDefault();
                            if (collected == null)
                            {
                                collected = new CollectedCalculatedParametrs();
                                collected.FK_CalculatedParametrs = CurrentCalculated.CalculatedParametrsID;
                                collected.FK_ReportArchiveTable = ReportID;
                                collected.FK_UsersTable = userID;
                                collected.Confirmed = false;
                                collected.LastChangeDateTime = DateTime.Now;
                                collected.Active = true;
                                collected.CollectedValue = 11;
                                kpiWebDataContext.CollectedCalculatedParametrs.InsertOnSubmit(collected);
                                kpiWebDataContext.SubmitChanges();
                            }
                            if (collected.Confirmed == true)
                            {
                                dataRow["CanConfirm"] = false;
                                dataRow["Value"] = collected.CollectedValue;
                            }
                            else
                            {
                                dataRow["CanConfirm"] = true;
                                dataRow["Value"] = GetCalculatedWithParams(mainStruct, ParamType, CurrentCalculated.CalculatedParametrsID, ReportID, SpecID).ToString();
                            }
                            #endregion                            
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    if (ParamType == 2)//
                    {
                        //ID - Рассчетного айдишник
                        CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a).FirstOrDefault();
                        List <BasicParametersTable> BasicList = Abbreviature.GetBasicList(Calculated.Formula);

                        foreach (BasicParametersTable CurrebtBasic in BasicList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrebtBasic.BasicParametersTableID;//GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrebtBasic.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Abb"] = CurrebtBasic.AbbreviationEN; 
                            dataRow["Value"] = GetCalculatedWithParams(mainStruct, ParamType, CurrebtBasic.BasicParametersTableID, ReportID,SpecID).ToString();
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    #endregion 
                    #region DataGridBind
                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = title;
                    Grid.DataBind();
                    #endregion
                    #region постнастройки страницы
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[1].Visible = false;

                    if (ParamType == 0)
                    {
                        Grid.Columns[2].Visible = false;
                        Grid.Columns[8].Visible = false;//
                        Grid.Columns[10].Visible = false;//
                    }

                    if (ParamType == 1)
                    {
                        Grid.Columns[8].Visible = false;//
                        Grid.Columns[10].Visible = false;//
                    }

                    if (ParamType == 2) // дальше углубляться нельзя
                    {
                        Grid.Columns[7].Visible = false;
                        Grid.Columns[9].Visible = false;
                    }
                    #endregion
                }
                else if (ViewType == 2) // просмотр по специальностям
                {
                    #region преднастройка страницы


                    if (ParamType == 0)
                    {

                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "Индикатор для специальностей");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageName.Text = "Значения для индикатора; \"";
                        PageName.Text += (from a in kpiWebDataContext.IndicatorsTable
                                          where a.IndicatorsTableID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\";";
                    }
                    else if (ParamType == 1)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "Расчетный для специальностей");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageName.Text = "Значения для расчетного показателя: \"";
                        PageName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                                          where a.CalculatedParametrsID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\";";
                    }
                    else if (ParamType == 2)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "Базовый для специальностей");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageName.Text = "Значения для базового показателя: \"";
                        PageName.Text += (from a in kpiWebDataContext.BasicParametersTable
                                          where a.BasicParametersTableID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\";";
                    }

                    string title = "Специальности";
                    #endregion
                    #region fill grid
                    List<SpecializationTable> SpecTable = (from a in kpiWebDataContext.SpecializationTable
                        join b in kpiWebDataContext.FourthLevelSubdivisionTable
                            on a.SpecializationTableID equals b.FK_Specialization
                        where a.Active == true
                              && b.Active == true
                        select a).OrderBy(mc => mc.SpecializationTableID).ToList();
                    
                    //взяли все специальности которые привязаны к кафедрам
                    int old = 0;
                    foreach (SpecializationTable currentSpec in SpecTable)
                    {
                        if (currentSpec.SpecializationTableID != old)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = currentSpec.SpecializationTableID; //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = currentSpec.Name; //currentStruct.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Value"] =
                                GetCalculatedWithParams(mainStruct, ParamType, ParamID, ReportID,
                                    currentSpec.SpecializationTableID).ToString();
                            dataTable.Rows.Add(dataRow);
                        }
                        else
                        {
                            
                        }
                        old = currentSpec.SpecializationTableID;
                    }

                    #endregion
                    #region DataGridBind
                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = title;
                    Grid.DataBind();
                    #endregion
                    #region постнастройка страницы
                    Grid.Columns[10].Visible = false;
                    Grid.Columns[9].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[1].Visible = false;
                    #endregion
                }
                else
                {
                    //error // wrong ViewType
                }                    
                #region history 2

                string between = "--->";
                for (int i = 0; i < rectorHistory.SessionCount; i++)
                {
                    RectorSession curSesion = rectorHistory.RectorSession[i];
                    switch (i)
                    {
                        case 0:
                            {

                                if (rectorHistory.CurrentSession != 0)
                                {
                                    HistoryLable.Text = "<a href=\"Result?&HLevel=0\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text = curSesion.sesName;
                                }

                                HistoryLable.Visible = true;
                                break;
                            }
                        case 1:
                        {
                            HistoryLable.Text += between;
                                if (rectorHistory.CurrentSession != 1)
                                {
                                    HistoryLable.Text += "<a href=\"Result?&HLevel=1\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text += curSesion.sesName;
                                }
                                break;
                            }
                        case 2:
                            {
                                HistoryLable.Text += between;
                                if (rectorHistory.CurrentSession != 2)
                                {
                                    HistoryLable.Text += "<a href=\"Result?&HLevel=2\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text += curSesion.sesName;
                                }
                                break;
                            }
                        case 3:
                            {
                                HistoryLable.Text += between;
                                if (rectorHistory.CurrentSession != 3)
                                {
                                    HistoryLable.Text += "<a href=\"Result?&HLevel=3\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text += curSesion.sesName;
                                }                                
                                break;
                            }
                        case 4:
                            {
                                HistoryLable.Text += between;
                                if (rectorHistory.CurrentSession != 4)
                                {
                                    HistoryLable.Text += "<a href=\"Result?&HLevel=4\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text += curSesion.sesName;
                                }
                                break;
                            }
                        case 5:
                            {
                                HistoryLable.Text += between;
                                if (rectorHistory.CurrentSession != 5)
                                {
                                    HistoryLable.Text += "<a href=\"Result?&HLevel=5\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text += curSesion.sesName;
                                }
                                break;
                            }
                        case 6:
                            {
                                HistoryLable.Text += between;
                                if (rectorHistory.CurrentSession != 6)
                                {
                                    HistoryLable.Text += "<a href=\"Result?&HLevel=6\">" + curSesion.sesName + "</a>";
                                }
                                else
                                {
                                    HistoryLable.Text += curSesion.sesName;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }



                #endregion
            }
        }

        protected void ButtonConfirmClick(object sender, EventArgs e)
        {
            
        }
        protected void Button1Click(object sender, EventArgs e) //по структуре
        {
            Button button = (Button)sender;
            {
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"");

                if (currentRectorSession.sesViewType == 1)
                {//впервые перешли на разложение по структуре сразу после показателя
                    currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 0;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
                }
                else if (currentRectorSession.sesViewType == 2)
                {//впервые перешли на разложение по структуре после выбора специальности
                    currentRectorSession.sesSpecID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 0;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
                }
                else
                {
                    currentRectorSession.sesStruct = StructDeeper(currentRectorSession.sesStruct, Convert.ToInt32(button.CommandArgument));               
                }       
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button2Click(object sender, EventArgs e) // по составляющим
        {
            Button button = (Button)sender;
            {


                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"");
                //RectorSession currentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                currentRectorSession.sesParamType++;       
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button3Click(object sender, EventArgs e)//по специальности
        {
            Button button = (Button)sender;
            {              
                RectorHistorySession rectorHistory = (RectorHistorySession) Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;
                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"");

                currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                currentRectorSession.sesViewType = 2;      
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (rectorHistory.CurrentSession == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            rectorHistory.CurrentSession--;
            Session["rectorHistory"] = rectorHistory;
            Response.Redirect("~/Rector/Result.aspx");
        }
        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (rectorHistory.CurrentSession < rectorHistory.SessionCount) // есть куда переходить
            {
                rectorHistory.CurrentSession++;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }

        }
    }
}
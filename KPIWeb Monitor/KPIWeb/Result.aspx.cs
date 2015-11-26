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
        public float GetCalculatedWithParams(Struct StructToCalcFor, int ParamType, int ParamID,int ReportID) // читает показатель
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            
            float result = 0;
            if (ParamType == 0) // считаем индикатор
            {
                IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                    where a.IndicatorsTableID == ParamID
                    select a).FirstOrDefault();

                return (float)CalculateAbb.CalculateForLevel(1, Abbreviature.CalculatedAbbToFormula(Indicator.Formula)
                    , ReportID, StructToCalcFor.Lv_0
                    , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4, StructToCalcFor.Lv_5, 0);
            
            
            }
            else if (ParamType == 1) // считаем рассчетный
            {
                CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                                             where a.CalculatedParametrsID == ParamID
                                             select a).FirstOrDefault();
                return (float)CalculateAbb.CalculateForLevel(1, Calculated.Formula, ReportID, StructToCalcFor.Lv_0
                    , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4, StructToCalcFor.Lv_5, 0);
            }
            else if (ParamType == 2) // суммируем базовый
            {
                return (float)CalculateAbb.SumForLevel(ParamID,ReportID,StructToCalcFor.Lv_0
                    , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4, StructToCalcFor.Lv_5);
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
            Struct tmp = parentStruct;
            if (tmp.Lv_0 == 0)
            {
                tmp.Lv_0 = nextID; 
                return tmp;
            }

            if (tmp.Lv_1 == 0)
            {
                tmp.Lv_1 = nextID;
                return tmp;
            }

            if (tmp.Lv_2 == 0)
            {
                tmp.Lv_2 = nextID;
                return tmp;
            }

            if (tmp.Lv_3 == 0)
            {
                tmp.Lv_3 = nextID;
                return tmp;
            }
            if (tmp.Lv_4 == 0)
            {
                tmp.Lv_4 = nextID;
                return tmp;
            }
            if (tmp.Lv_5 == 0)
            {
                tmp.Lv_5 = nextID;
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
            if (!IsPostBack)
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                //Принимаемые через сессию параметры
                RectorSession rectorResultSession = (RectorSession) Session["rectorResultSession"];
                if (rectorResultSession == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                Struct mainStruct = rectorResultSession.sesStruct;
                int ViewType = rectorResultSession.sesViewType;
                int ParamID = rectorResultSession.sesParamID;
                int ParamType = rectorResultSession.sesParamType;
                int ReportID = rectorResultSession.sesReportID;
                ///////////////////////
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Value", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Title", typeof(string)));

                ReportTitle.Text = (from a in kpiWebDataContext.ReportArchiveTable
                    where a.ReportArchiveTableID == ReportID
                    select a.Name).FirstOrDefault().ToString();

                if (ViewType == 0) // просмотр для структурных подразделений
                {
                    #region преднастройка страницы
                    string title="";
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
                        dataRow["Value"] =  GetCalculatedWithParams(currentStruct, ParamType, ParamID, ReportID).ToString();
                        dataTable.Rows.Add(dataRow);
                    }

                    #endregion

                    Grid.DataSource = dataTable;
                    Grid.Columns[2].HeaderText = title;
                    Grid.DataBind();

                    #region постнастройка страницы

                    Grid.Columns[8].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[3].Visible = false;
                    Grid.Columns[1].Visible = false;
                    Grid.Columns[0].HeaderText = title;
                    if (StructDeepness(currentStructList[0]) > 3) // дальше углубляться нельзя
                    {
                        Grid.Columns[6].Visible = false;
                    }
                    #endregion

                }
                else if (ViewType == 1) // просмотр для показателей (верхние 3 шт)
                {
                    #region преднастройка страницы

                    string title="";
                    if (ParamType==0)
                    {
                        PageName.Text = "Значения индикторов для КФУ";
                        title = "Индикаторы";
                    }
                    else if (ParamType == 1)
                    {
                        PageName.Text = "Значения расчетных показателей используемых для расчета индикатора: \"";
                        PageName.Text += (from a in kpiWebDataContext.IndicatorsTable
                                                                      where a.IndicatorsTableID == ParamID
                                                                      select a.Name).FirstOrDefault();
                        PageName.Text += "\" для КФУ";
                        title = "Расчетные показатели";
                    }
                    else if (ParamType == 2)
                    {
                        PageName.Text = "Значения базовых показателей используемых для расчета расчетного показателя\"";
                        PageName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                                          where a.CalculatedParametrsID == ParamID
                                          select a.Name).FirstOrDefault();
                        PageName.Text += "\" для КФУ";
                        title = "Базовые показатели";
                    }

                    #endregion

                    #region main
                    if (ParamType == 0)//считаем индикатор
                    {
                        List<IndicatorsTable> Indicators = (from a in kpiWebDataContext.IndicatorsTable
                            where a.Active == true
                            select a).ToList();
                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentIndicator.IndicatorsTableID;//GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrentIndicator.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Value"] = GetCalculatedWithParams(mainStruct, ParamType, CurrentIndicator.IndicatorsTableID, ReportID).ToString();
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    if (ParamType == 1) //показываем рассчетный входящий в ID Индикатора
                    {
                        //ID  - это айди Индиктора
                        IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                            where a.IndicatorsTableID == ParamID
                            select a).FirstOrDefault();
                        List<CalculatedParametrs> CalculatedList = Abbreviature.GetCalculatedList(Indicator.Formula);
                        foreach (CalculatedParametrs CurrentCalculated in CalculatedList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentCalculated.CalculatedParametrsID;//GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrentCalculated.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Value"] = GetCalculatedWithParams(mainStruct, ParamType, CurrentCalculated.CalculatedParametrsID, ReportID).ToString();
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
                            dataRow["Value"] = GetCalculatedWithParams(mainStruct, ParamType, CurrebtBasic.BasicParametersTableID, ReportID).ToString();
                            dataTable.Rows.Add(dataRow);
                        }
                    }

                    #endregion 

                    Grid.DataSource = dataTable;
                    Grid.Columns[2].HeaderText = title;
                    Grid.DataBind();

                    #region постнастройки страницы
                    Grid.Columns[8].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[3].Visible = false;
                    Grid.Columns[1].Visible = false;
                    
                    if (ParamType == 2) // дальше углубляться нельзя
                    {
                        Grid.Columns[7].Visible = false;
                    }
                    #endregion
                }
                else
                {
                    //error // wrong ViewType
                }

            }

        }
        protected void Button1Click(object sender, EventArgs e) //по структуре
        {
            Button button = (Button)sender;
            {
                RectorSession rectorResultSession = (RectorSession) Session["rectorResultSession"];
                if (rectorResultSession == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                //rectorResultSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                //rectorResultSession.sesViewType = 0;
                if (rectorResultSession.sesViewType == 1)
                {//впервые перешли на разложение по структуре
                    rectorResultSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    rectorResultSession.sesViewType = 0;
                    rectorResultSession.sesStruct.Lv_0 = 1;
                    rectorResultSession.sesStruct.Lv_1 = 0;
                    rectorResultSession.sesStruct.Lv_2 = 0;
                    rectorResultSession.sesStruct.Lv_3 = 0;
                    rectorResultSession.sesStruct.Lv_4 = 0;
                    rectorResultSession.sesStruct.Lv_5 = 0;
                }
                else
                {
                    rectorResultSession.sesStruct = StructDeeper(rectorResultSession.sesStruct, Convert.ToInt32(button.CommandArgument));
                }                      
                Session["rectorResultSession"] = rectorResultSession;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button2Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                RectorSession rectorResultSession = (RectorSession)Session["rectorResultSession"];
                if (rectorResultSession == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                //rectorResultSession.sesStruct = StructDeeper(rectorResultSession.sesStruct, Convert.ToInt32(button.CommandArgument));
                rectorResultSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                rectorResultSession.sesParamType++;
                Session["rectorResultSession"] = rectorResultSession;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button3Click(object sender, EventArgs e)
        {

        }
    }
}
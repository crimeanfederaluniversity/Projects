using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Net;
using Microsoft.Ajax.Utilities;
using Button = System.Web.UI.WebControls.Button;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;

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
        public List<Struct> GetChildStructList(Struct ParentStruct)
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
                                        select new Struct(1, "")
                                        {
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
                                        select new Struct(1, "")
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
                                        select new Struct(1, "")
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
                                        select new Struct(1, "")
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
                case 3: // возвращаем все специальности
                    {
                        tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                        join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                        on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                        join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                        on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                        join d in kpiWebDataContext.FourthLevelSubdivisionTable
                                        on c.ThirdLevelSubdivisionTableID equals d.FK_ThirdLevelSubdivisionTable
                                        where a.Active == true
                                        && b.Active == true
                                        && c.Active == true
                                        && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                        && b.FK_FirstLevelSubdivisionTable == ParentStruct.Lv_1
                                        && c.FK_SecondLevelSubdivisionTable == ParentStruct.Lv_2
                                        && d.FK_ThirdLevelSubdivisionTable == ParentStruct.Lv_3
                                        select new Struct(1, "")
                                        {
                                            Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                            Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                            Lv_2 = (int)b.SecondLevelSubdivisionTableID,
                                            Lv_3 = (int)c.ThirdLevelSubdivisionTableID,
                                            Lv_4 = (int)d.FourthLevelSubdivisionTableID,
                                            Lv_5 = 0,
                                            Name = d.Name
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
        public List<Struct> GetChildStructList(Struct ParentStruct, int SpecID) 
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
                                    }).OrderBy(x => x.Lv_0).ToList();
                    break;
                }
                case 0: // возвращаем все универы
                {
                    tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                    on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                    join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                    on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                    join d in kpiWebDataContext.FourthLevelSubdivisionTable
                                    on c.ThirdLevelSubdivisionTableID equals d.FK_ThirdLevelSubdivisionTable
                                    where a.Active == true
                                    && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                    && ((SpecID == 0) || (SpecID == d.FK_Specialization))
                                    select new Struct(1,"")
                                    {
                                        Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                        Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                        Lv_2 = 0,
                                        Lv_3 = 0,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = a.Name
                                    }).OrderBy(x => x.Lv_1).ToList();
                    break;
                }
                case 1: // возвращаем все факультеты
                {
                    tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                    on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                    join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                    on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                    join d in kpiWebDataContext.FourthLevelSubdivisionTable
                                    on c.ThirdLevelSubdivisionTableID equals d.FK_ThirdLevelSubdivisionTable
                                    where a.Active == true
                                    && b.Active == true
                                    && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                    && b.FK_FirstLevelSubdivisionTable == ParentStruct.Lv_1
                                   && ((SpecID == 0) || (SpecID == d.FK_Specialization))
                                    select new Struct(1,"")
                                    {
                                        Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                        Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                        Lv_2 = (int)b.SecondLevelSubdivisionTableID,
                                        Lv_3 = 0,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = b.Name
                                    }).OrderBy(x => x.Lv_2).ToList();
                    break;
                }
                case 2: // возвращаем все кафедры
                {
                    tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                    on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                    join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                    on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable                                  
                                    join e in kpiWebDataContext.FourthLevelSubdivisionTable
                                    on c.ThirdLevelSubdivisionTableID equals e.FK_ThirdLevelSubdivisionTable
                                    where a.Active == true
                                    && b.Active == true
                                    && c.Active == true
                                    && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                    && b.FK_FirstLevelSubdivisionTable == ParentStruct.Lv_1
                                    && c.FK_SecondLevelSubdivisionTable == ParentStruct.Lv_2
                                    && ((SpecID == 0) || (SpecID == e.FK_Specialization))
                                    select new Struct(1,"")
                                    {
                                        Lv_0 = (int)a.FK_ZeroLevelSubvisionTable,
                                        Lv_1 = (int)a.FirstLevelSubdivisionTableID,
                                        Lv_2 = (int)b.SecondLevelSubdivisionTableID,
                                        Lv_3 = (int)c.ThirdLevelSubdivisionTableID,
                                        Lv_4 = 0,
                                        Lv_5 = 0,
                                        Name = c.Name
                                    }).OrderBy(x => x.Lv_3).ToList();
                    break;
                }
                default:
                {
                    //error не будем раскладывать до специальностей
                    break;
                }
            }

            List<Struct> uniqeStruct = new List<Struct>();
            foreach (Struct curStruct in tmpStrucList)
            {
                if (uniqeStruct.Count == 0)
                {
                    uniqeStruct.Add(curStruct);
                }
                else
                {
                    if ((uniqeStruct[uniqeStruct.Count - 1].Lv_0 != curStruct.Lv_0)||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_1 != curStruct.Lv_1)||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_2 != curStruct.Lv_2)||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_3 != curStruct.Lv_3)||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_4 != curStruct.Lv_4))
                    {
                        uniqeStruct.Add(curStruct);
                    }
                }
            }
            return uniqeStruct;
        }
        public class MyObject
        {
            public int Id;
            public int ParentId;
            public string Name;
            public string UrlAddr;
            public int Active;
        }
        public float GetCalculatedWithParams(Struct StructToCalcFor, int ParamType, int ParamID,int ReportID, int SpecID) // читает показатель
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
          
            float result = 0;
            if (ParamType == 0) // считаем целевой показатель
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
        public float CalculatedForDB(float input)
        {
            float tmp = (float) input;

            if ((tmp < -(float)1E+20) || (tmp > (float)1E+20)
                || (tmp == null) || (float.IsNaN(tmp))
                || (float.IsInfinity(tmp)) || (float.IsNegativeInfinity(tmp))
                || (float.IsPositiveInfinity(tmp)) || (!tmp.ToString().IsFloat()))
            {
                tmp = (float)1E+20;
            }
            return tmp;
        }
        protected void Page_Load(object sender, EventArgs e)
        {          
            #region get user data

            Panel5.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
            Panel7.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
            Panel6.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");

            string parameter = Request["__EVENTARGUMENT"];
            if (parameter != null)
            {
                int ParamId = -1;
                if (int.TryParse(parameter, out ParamId) && ParamId > 0)
                {
                    DoConfirm(ParamId);
                }
            }

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
        
            UsersTable userTable_ =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["login"] = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a.Email).FirstOrDefault();

            if (userTable_.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }

            #endregion
            if (!IsPostBack)
            {
                #region session

                RectorHistorySession rectorHistory = (RectorHistorySession) Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                ShowUnConfirmed unConfirmed = (ShowUnConfirmed) Session["unConfirmed"];
                bool ShowUnconfirmed = true;
                if (unConfirmed == null)
                {
                    ShowUnconfirmed = false;
                }
                else
                {
                    if (unConfirmed.DoShowUnConfirmed == false)
                    {
                        ShowUnconfirmed = false;
                    }
                    else
                    {
                        Button7.Enabled = false;
                        unConfirmed.DoShowUnConfirmed = false;
                        Session["unConfirmed"] = unConfirmed;
                    }
                }



                #region check for get

                string val = this.Request.QueryString["HLevel"]; //hisoty level сова придумал)
                if (val != null)
                {
                    rectorHistory.CurrentSession = Convert.ToInt32(val);
                    Session["rectorHistory"] = rectorHistory;
                }

                #endregion

                if ((rectorHistory.SessionCount - rectorHistory.CurrentSession) < 2)
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
                dataTable.Columns.Add(new DataColumn("Abb", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Value", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Title", typeof (string)));
                dataTable.Columns.Add(new DataColumn("PlannedValue", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Progress", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Color", typeof (string)));

                ///color table
                /// 0 - no color  // can't confirm
                /// 1 - green (confirmed)
                /// 2 - red (unconfirmed but calculated)
                /// 3 - orange (can confirm)
                /// 
                dataTable.Columns.Add(new DataColumn("CanWatchWhoOws", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("CanConfirm", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("ShowLable", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("LableText", typeof (string)));
                dataTable.Columns.Add(new DataColumn("LableColor", typeof (string)));

                #endregion
                #region global page settings

                ReportArchiveTable ReportTable = (from a in kpiWebDataContext.ReportArchiveTable
                    where a.ReportArchiveTableID == ReportID
                    select a).FirstOrDefault();

                double daysLeft = ((DateTime) ReportTable.EndDateTime - DateTime.Now).TotalDays;

                ReportTitle.Text = ReportTable.Name + " " + ReportTable.StartDateTime.ToString().Split(' ')[0] + " - " +
                                   ReportTable.EndDateTime.ToString().Split(' ')[0];

                #endregion
                #region Show Uncinfirmed Button 

                Button7.Visible = false;
                if ((daysLeft < ReportTable.DaysBeforeToCalcForRector) &&
                    (ReportTable.DaysBeforeToCalcForRector != 0))
                {
                    Button7.Enabled = true;
                }
                else
                {
                    Button7.Enabled = false;
                }

                if (ShowUnconfirmed == true)
                {
                    Button7.Enabled = false;
                }

                #endregion
                if (ViewType == 0) // просмотр для структурных подразделений
                {
                    #region преднастройка страницы                    

                    string title = "";
                    PageFullName.Text = "";
                    if (ParamType == 0)
                    {
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.IndicatorsTable
                            where a.IndicatorsTableID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>";
                    }
                    else if (ParamType == 1)
                    {
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>";
                    }
                    else if (ParamType == 2)
                    {
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.BasicParametersTable
                            where a.BasicParametersTableID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>"; 
                    }

                    int Deep = StructDeepness(mainStruct);
                    if (Deep == 1) 
                    {
                    }
                    if (Deep == 2)
                    {
                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    if (Deep == 3)
                    {           
                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                              where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                                              select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    if (Deep == 4)
                    {

                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                              where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                                              select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                              where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                                              select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                              where a.ThirdLevelSubdivisionTableID == mainStruct.Lv_3
                                              select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }

                    if (SpecID != 0)
                    {
                        PageFullName.Text += "Направление подготовки \"" + (from a in kpiWebDataContext.SpecializationTable
                            where a.SpecializationTableID == SpecID
                            select a.Name).FirstOrDefault() + "\" </br>";
                    }
                    if (mainStruct.Lv_1 == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "КФУ");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, mainStruct.Name);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    //задади имя текущей сессии


                    title = "Подразделения";
                    if (StructDeepness(mainStruct) > 3)
                    {
                        title = "Направления подготовки";
                    }


                    #endregion
                    #region fill grid

                    int BasicParamLevel = 0;
                    if (ParamType == 2)
                    {
                            BasicParamLevel  = (int) (from a in kpiWebDataContext.BasicParametrAdditional
                            where a.BasicParametrAdditionalID == ParamID
                            select a.SubvisionLevel).FirstOrDefault();

                    }
                    List<Struct> currentStructList = new List<Struct>();
                    if (SpecID != 0)
                    {
                        currentStructList = GetChildStructList(mainStruct, SpecID);
                    }
                    else
                    {
                        currentStructList = GetChildStructList(mainStruct);
                    }
                    foreach (Struct currentStruct in currentStructList)
                    {
                        DataRow dataRow = dataTable.NewRow();

                        dataRow["ID"] = GetLastID(currentStruct).ToString();
                        dataRow["Number"] = "num";
                        dataRow["Name"] = currentStruct.Name;
                        dataRow["StartDate"] = "nun";
                        dataRow["EndDate"] = "nun";

                        dataRow["CanConfirm"] = true;
                        dataRow["ShowLable"] = false;
                        dataRow["CanWatchWhoOws"] = false;
                        
                        dataRow["LableText"] = "";
                        dataRow["LableColor"] = "#000000";

                        dataRow["Value"] =
                            GetCalculatedWithParams(currentStruct, ParamType, ParamID, ReportID, SpecID).ToString();
                        dataTable.Rows.Add(dataRow);
                    }

                    #endregion
                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = title;
                    Grid.DataBind();

                    #endregion
                    #region постнастройка страницы

                    Grid.Columns[12].Visible = false;
                    Grid.Columns[11].Visible = false;
                    Grid.Columns[9].Visible = false;
                    Grid.Columns[8].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;
                    if ((StructDeepness(mainStruct) > (BasicParamLevel-1))||
                        (StructDeepness(mainStruct) > 2 )&&(SpecID!=0)) // дальше углубляться нельзя
                    {
                        Grid.Columns[10].Visible = false;
                    }                    
                    #endregion
                }
                else if (ViewType == 1) // просмотр для показателей (верхние 3 шт)
                {
                    #region преднастройка страницы

                    string name_text = "";
                    string value_text = "";
                    string progress_text = "";
                    string confirm_text = "";
                    string detalize_text = "";

                    if (ParamType == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Значения целевых показателей для КФУ");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                        PageFullName.Text = "Значения целевых показателей (ЦП) для КФУ";
                        //PageName.Text = "Значения индикторов для КФУ";
                        name_text = "Название ЦП";
                        value_text = "Значение ЦП";
                        progress_text = "Степень готовности первичных данных";
                        confirm_text = "Утвердить ЦП";
                        detalize_text = "Просмотреть первичные данные для ЦП";
                    }
                    else if (ParamType == 1)
                    {
                        if (ParamID == 0)
                        {
                            RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                                SpecID, "Значения первичных данных для КФУ");
                            rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                            Session["rectorHistory"] = rectorHistory;
                            PageFullName.Text = "Значения первичных данных для КФУ";
                        }
                        else
                        {
                            string tmp = (from a in kpiWebDataContext.IndicatorsTable
                                where a.IndicatorsTableID == ParamID
                                select a.Name).FirstOrDefault();

                            RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                                SpecID, "Первичные данные для целевого показателя: " + tmp);
                            rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                            Session["rectorHistory"] = rectorHistory;

                            PageFullName.Text = "Первичные данные (ПД) целевого показателя <b> \"" + tmp + "\"</b>  для КФУ";
                            FormulaLable.Text = (from a in kpiWebDataContext.IndicatorsTable
                                where a.IndicatorsTableID == ParamID
                                select a.Formula).FirstOrDefault();
                            FormulaLable.Visible = true;
                        }
                        name_text = "Названия ПД";
                        value_text = "Значение ПД";
                        progress_text = "Степень готовности базовых показателей";
                        confirm_text = "Утвердить ПД";
                        detalize_text = "Просмотреть базовые показатели для ПД";
                    }
                    else if (ParamType == 2)
                    {
                        string tmp = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Name).FirstOrDefault();
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Базовые показатели для первич: " + tmp);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageFullName.Text = "Базовые показатели первичного показателя <b>  \"" + tmp + "\"</b>  для КФУ";

                        name_text = "Названия БП";
                        value_text = "Значение БП";
                        //progress_text = "Степень готовности базовых показателей";
                        //confirm_text = "Утвердить ПД";
                        //Ыdetalize_text = "Просмотреть базовые показатели для ПД";

                        FormulaLable.Text = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Formula).FirstOrDefault();
                        FormulaLable.Visible = true;
                    }

                    #endregion
                    #region fill grid
                    if (ParamType == 0) //считаем целевой показатель
                    {
                        #region indicator
                        List<IndicatorsTable> Indicators = (
                            from a in kpiWebDataContext.IndicatorsTable
                            join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                on a.IndicatorsTableID equals b.FK_IndicatorsTable
                            where
                                a.Active == true
                                && b.CanView == true
                                && b.FK_UsresTable == userID
                            select a).ToList();

                        //нашли все целевой показатель привязанные к пользователю
                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentIndicator.IndicatorsTableID; //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrentIndicator.Name;
                            dataRow["CanWatchWhoOws"] = false;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";

                            PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                      && a.Date > DateTime.Now
                                //Q1
                                select a).OrderBy(x => x.Date).FirstOrDefault();

                            if (plannedValue != null)
                            {
                                dataRow["PlannedValue"] = plannedValue.Value;
                            }
                            else
                            {
                                dataRow["PlannedValue"] = "Не определено";
                            }

                            #region user can confirm

                            bool canConfirm = (bool) (from a in kpiWebDataContext.IndicatorsAndUsersMapping
                                where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                      && a.FK_UsresTable == userID
                                select a.CanConfirm).FirstOrDefault();

                            #endregion

                            # region are calculated confirmed 

                            List<CalculatedParametrs> CalculatedList =
                                Abbreviature.GetCalculatedList(CurrentIndicator.Formula);

                            bool CalcAreConfirmed = true;

                            int AllCalculated = 0;
                            int AllConfirmedCalculated = 0;
                            foreach (CalculatedParametrs CurrentCalculated in CalculatedList)
                            {
                                AllCalculated++;
                                CollectedCalculatedParametrs tmp_ =
                                    (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                        where
                                            a.FK_CalculatedParametrs == CurrentCalculated.CalculatedParametrsID
                                            && a.FK_ReportArchiveTable == ReportID
                                        select a).FirstOrDefault();
                                if (tmp_ == null)
                                {
                                    CalcAreConfirmed = false;
                                }
                                else
                                {
                                    if (tmp_.Confirmed == null)
                                    {
                                        CalcAreConfirmed = false;
                                    }
                                    else if (tmp_.Confirmed == false)
                                    {
                                        CalcAreConfirmed = false;
                                    }
                                    else
                                    {
                                        AllConfirmedCalculated++;
                                    }
                                }
                            }
                            dataRow["Progress"] = AllConfirmedCalculated.ToString() + " из " + AllCalculated.ToString();

                            #endregion

                            #region get calculated if confirmed; calculate if not confirmed

                            string value_ = "";

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
                                collected.CollectedValue = 0;
                                kpiWebDataContext.CollectedIndocators.InsertOnSubmit(collected);
                                kpiWebDataContext.SubmitChanges();
                            }

                            if (collected.Confirmed == true) // данные подтверждены
                            {
                                dataRow["CanConfirm"] = false;
                                dataRow["ShowLable"] = true;
                                dataRow["LableText"] = "Утверждено";
                                dataRow["Color"] = "1"; // confirmed
                                value_ = ((float) collected.CollectedValue).ToString("0.00");
                            }
                            else // данные уже есть но еще не подтверждены
                            {
                                if (canConfirm == false)
                                {
                                    dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = " "; // нет права утверждать
                                    dataRow["LableColor"] = "#101010";
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        /// 0 - no color  // can't confirm
                                        /// 1 - green (confirmed)
                                        /// 2 - red (unconfirmed but calculated)
                                        /// 3 - orange (can confirm)
                                        /// 
                                        float tmp =
                                            CalculatedForDB(GetCalculatedWithParams(mainStruct, ParamType,
                                                CurrentIndicator.IndicatorsTableID, ReportID, SpecID));

                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.00");
                                        }
                                    }
                                }
                                else if (!CalcAreConfirmed)
                                {
                                    /*dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = "Не все расчетные утверждены";
                                    */
                                    dataRow["CanConfirm"] = true;
                                    dataRow["ShowLable"] = false;
                                    dataRow["LableText"] = "";

                                    dataRow["LableColor"] = "#101010";
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        float tmp =
                                            CalculatedForDB(GetCalculatedWithParams(mainStruct, ParamType,
                                                CurrentIndicator.IndicatorsTableID, ReportID, SpecID));
                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.00");
                                        }
                                    }
                                }
                                else
                                {
                                    dataRow["CanConfirm"] = true;
                                    dataRow["ShowLable"] = false;
                                    dataRow["LableText"] = "";
                                    dataRow["LableColor"] = "#FFFFFF";
                                    dataRow["Color"] = "3";
                                    collected.CollectedValue =
                                        CalculatedForDB(GetCalculatedWithParams(mainStruct, ParamType,
                                            CurrentIndicator.IndicatorsTableID, ReportID, SpecID));
                                        //12;                               
                                    kpiWebDataContext.SubmitChanges();
                                    value_ = ((float) collected.CollectedValue).ToString("0.00");
                                }
                            }
                            dataRow["Value"] = value_;

                            #endregion

                            dataTable.Rows.Add(dataRow);
                        }

                        #endregion indicator
                    }
                    if (ParamType == 1) //показываем рассчетный входящий в ID целевой показатель
                    {
                        #region calculated

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
                            dataRow["ID"] = CurrentCalculated.CalculatedParametrsID;
                                //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrentCalculated.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";

                         //   dataRow["CanWatchWhoOws"] = "false";
                         //   dataRow["CanConfirm"] = "true";
                         //   dataRow["ShowLable"] = "false";
                           
                            dataRow["Abb"] = CurrentCalculated.AbbreviationEN;

                            #region get calculated if confirmed; calculate if not confirmed

                            #region user can edit

                            bool canConfirm = (bool) (from a in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                                where a.FK_CalculatedParametrsTable == CurrentCalculated.CalculatedParametrsID
                                      && a.FK_UsersTable == userID
                                select a.CanConfirm).FirstOrDefault();

                            #endregion

                            #region check if all users confirmed basics

                            List<BasicParametersTable> BasicList = Abbreviature.GetBasicList(CurrentCalculated.Formula);
                            int AllBasicsUsersCanEdit = 0;
                            int AllConfirmedBasics = 0;
                            foreach (BasicParametersTable Basic in BasicList)
                            {
                                /*
                                AllBasicsUsersCanEdit += (from a in kpiWebDataContext.BasicParametrsAndUsersMapping
                                    join b in kpiWebDataContext.UsersTable
                                        on a.FK_UsersTable equals b.UsersTableID
                                    where a.CanEdit == true
                                          && b.Active == true
                                          && a.FK_ParametrsTable == Basic.BasicParametersTableID
                                    select a).Count();
                                AllConfirmedBasics += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                    where a.FK_ReportArchiveTable == ReportID
                                          && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                          && a.Status == 4
                                    select a).Count();*/
                                AllBasicsUsersCanEdit += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                          where a.FK_ReportArchiveTable == ReportID
                                                                && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                                          select a).Count();

                                AllConfirmedBasics += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                       where a.FK_ReportArchiveTable == ReportID
                                                             && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                                             && a.Status == 4
                                                       select a).Count();
                            }
                            bool BasicsAreConfirmed = true;
                            if (AllBasicsUsersCanEdit != AllConfirmedBasics)
                            {
                                BasicsAreConfirmed = false;
                            }

                            #endregion

                            dataRow["Progress"] =
                                ((((float) AllConfirmedBasics)*100)/((float) AllBasicsUsersCanEdit)).ToString("0.0") +
                                "%";
                            string value_ = "";

                            CollectedCalculatedParametrs collected =
                                (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                    where a.FK_ReportArchiveTable == ReportID
                                          && a.FK_CalculatedParametrs == CurrentCalculated.CalculatedParametrsID
                                    select a).FirstOrDefault();
                            if (collected == null) // данных нет
                            {
                                collected = new CollectedCalculatedParametrs();
                                collected.FK_CalculatedParametrs = CurrentCalculated.CalculatedParametrsID;
                                collected.FK_ReportArchiveTable = ReportID;
                                collected.FK_UsersTable = userID;
                                collected.Confirmed = false;
                                collected.LastChangeDateTime = DateTime.Now;
                                collected.Active = true;
                                collected.CollectedValue = GetCalculatedWithParams(mainStruct, ParamType,
                                    CurrentCalculated.CalculatedParametrsID, ReportID, SpecID); //11;
                                kpiWebDataContext.CollectedCalculatedParametrs.InsertOnSubmit(collected);
                                kpiWebDataContext.SubmitChanges();
                            }

                            if (collected.Confirmed == true) //данные подтверждены
                            {
                                dataRow["CanWatchWhoOws"] = false;
                                dataRow["CanConfirm"] = false;
                                dataRow["ShowLable"] = true;
                                dataRow["LableText"] = "Утверждено";
                                dataRow["LableColor"] = Color.LawnGreen;
                                dataRow["Color"] = "1";
                                value_ = ((float) collected.CollectedValue).ToString("0.00");
                            }
                            else // данные есть но не подтверждены
                            {
                                if (canConfirm == false) //
                                {
                                    dataRow["CanWatchWhoOws"] = false;
                                    dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = " ";
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        float tmp =
                                            CalculatedForDB(GetCalculatedWithParams(mainStruct, ParamType,
                                                CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.00");
                                        }
                                    }
                                }
                                else if (BasicsAreConfirmed == false)
                                {
                                    dataRow["CanWatchWhoOws"] = true;
                                    dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = false;
                                    dataRow["LableText"] = "";
                                    /*
                                    dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = "Не все базовые показатели утверждены";*/
                                    dataRow["LableColor"] = Color.LightBlue;
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        float tmp =
                                            CalculatedForDB(GetCalculatedWithParams(mainStruct, ParamType,
                                                CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.00");
                                        }
                                    }
                                }
                                else
                                {
                                    dataRow["CanConfirm"] = true;
                                    dataRow["CanWatchWhoOws"] = false;
                                    dataRow["ShowLable"] = false;
                                    dataRow["LableText"] = "";
                                    dataRow["Color"] = "3";
                                    dataRow["LableColor"] = "#000000";
                                    collected.CollectedValue =
                                        CalculatedForDB(GetCalculatedWithParams(mainStruct, ParamType,
                                            CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                    kpiWebDataContext.SubmitChanges();
                                    value_ = ((float) collected.CollectedValue).ToString("0.00");
                                }
                            }
                            dataRow["Value"] = value_;

                            #endregion

                            dataTable.Rows.Add(dataRow);
                        }

                        #endregion
                    }
                    if (ParamType == 2) //
                    {
                        #region basic

                        //ID - Рассчетного айдишник
                        CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a).FirstOrDefault();
                        List<BasicParametersTable> BasicList = Abbreviature.GetBasicList(Calculated.Formula);

                        foreach (BasicParametersTable CurrebtBasic in BasicList)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrebtBasic.BasicParametersTableID; //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            dataRow["Name"] = CurrebtBasic.Name;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Abb"] = CurrebtBasic.AbbreviationEN;
                            dataRow["CanWatchWhoOws"] = false;
                            dataRow["CanConfirm"] = true;
                            dataRow["ShowLable"] = false;
                            dataRow["LableText"] = "";
                            dataRow["LableColor"] = "#000000";

                            dataRow["Value"] =
                                GetCalculatedWithParams(mainStruct, ParamType, CurrebtBasic.BasicParametersTableID,
                                    ReportID, SpecID).ToString();
                            dataTable.Rows.Add(dataRow);
                        }

                        #endregion
                    }

                    #endregion
                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = name_text;
                    Grid.Columns[6].HeaderText = value_text;
                    Grid.Columns[8].HeaderText = progress_text;
                    Grid.Columns[9].HeaderText = confirm_text;
                    Grid.Columns[11].HeaderText = detalize_text;
                    Grid.DataBind();

                    #endregion
                    #region постнастройки страницы

                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[1].Visible = false;

                    if (ParamType == 0)
                    {
                        Grid.Columns[2].Visible = false;
                        Grid.Columns[10].Visible = false; //
                        Grid.Columns[12].Visible = false; //
                        Button7.Visible = true;
                    }

                    if (ParamType == 1)
                    {
                        Button7.Visible = true;
                        if (ParamID == 0)
                        {
                            Grid.Columns[2].Visible = false;
                        }
                        Grid.Columns[10].Visible = false; //
                        Grid.Columns[12].Visible = false; //
                        Grid.Columns[7].Visible = false;
                    }

                    if (ParamType == 2) // дальше углубляться нельзя
                    {
                        Grid.Columns[7].Visible = false;
                        Grid.Columns[9].Visible = false;
                        Grid.Columns[8].Visible = false;
                        Grid.Columns[11].Visible = false;
                    }

                    #endregion
                }
                else if (ViewType ==  2) // просмотр по специальностям
                {
                    #region преднастройка страницы
                    if (ParamType == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Целевой показатель (ЦП) для направления подготовки");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else if (ParamType == 1)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Первичные данные (ПД) для направления подготовки");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else if (ParamType == 2)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Базовый показатель (БП) для направления подготовки");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                        string tmp = (from a in kpiWebDataContext.BasicParametersTable
                            where a.BasicParametersTableID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text = "Значения базового показателя (БП) <b>  \"" + tmp + "\" </b>  по направлениям подготовки для КФУ";
                    }

                    string title = "Направления подготовки";

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
                            dataRow["CanWatchWhoOws"] = false;
                            dataRow["CanConfirm"] = true;
                            dataRow["ShowLable"] = false;
                            dataRow["LableText"] = "";
                            dataRow["LableColor"] = "#000000";

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

                    Grid.Columns[12].Visible = false;
                    Grid.Columns[11].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[9].Visible = false;
                    Grid.Columns[8].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;

                    #endregion
                }
                else if (ViewType == 3)
                {
                    #region

                        PageFullName.Text = "";
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>";

                        int Deep = StructDeepness(mainStruct);
                        if (Deep == 1)
                        {
                        }
                        if (Deep == 2)
                        {
                            PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                  where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                                                  select a.Name).FirstOrDefault();
                            PageFullName.Text += "</br>";
                        }
                        if (Deep == 3)
                        {
                            PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                  where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                                                  select a.Name).FirstOrDefault();
                            PageFullName.Text += "</br>";

                            PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                  where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                                                  select a.Name).FirstOrDefault();
                            PageFullName.Text += "</br>";
                        }
                        if (Deep == 4)
                        {

                            PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                  where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                                                  select a.Name).FirstOrDefault();
                            PageFullName.Text += "</br>";

                            PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                  where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                                                  select a.Name).FirstOrDefault();
                            PageFullName.Text += "</br>";

                            PageFullName.Text += (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                  where a.ThirdLevelSubdivisionTableID == mainStruct.Lv_3
                                                  select a.Name).FirstOrDefault();
                            PageFullName.Text += "</br>";
                        }

                    #endregion
                    #region fill grid
                    CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                                                      where a.CalculatedParametrsID == ParamID
                                                      select a).FirstOrDefault();

                    List<BasicParametersTable> BasicList = Abbreviature.GetBasicList(Calculated.Formula);
                    List<Struct> currentStructList = new List<Struct>();
                    currentStructList = GetChildStructList(mainStruct);

                    foreach (Struct currentStruct in currentStructList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = GetLastID(currentStruct).ToString();
                        dataRow["Number"] = "num";
                        dataRow["Name"] = currentStruct.Name;
                        dataRow["StartDate"] = "nun";
                        dataRow["EndDate"] = "nun";
                        dataRow["CanConfirm"] = false;
                        dataRow["ShowLable"] = false;
                        dataRow["CanWatchWhoOws"] = false;
                        dataRow["LableText"] = "";
                        dataRow["LableColor"] = "#000000";
                        dataRow["Value"] = "nun";
                        #region check if all users confirmed basics

                        int AllBasicsUsersCanEdit = 0;
                        int AllConfirmedBasics = 0;
                        
                        foreach (BasicParametersTable Basic in BasicList)
                        {
                            /*
                            List<UsersTable> UserTableList = (from a in kpiWebDataContext.UsersTable
                                join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                on a.UsersTableID equals b.FK_UsersTable
                                join c in kpiWebDataContext.BasicParametrAdditional 
                                on b.FK_ParametrsTable equals c.BasicParametrAdditionalID
                                where a.Active == true
                                      && c.Calculated == false
                                      && b.Active == true
                                      && b.CanEdit == true
                                      && b.FK_ParametrsTable == Basic.BasicParametersTableID

                                      && ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) || (currentStruct.Lv_0 == 0))
                                      && ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) || (currentStruct.Lv_1 == 0))
                                      && ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) || (currentStruct.Lv_2 == 0))
                                      && ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) || (currentStruct.Lv_3 == 0))
                                    //  && ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) || (currentStruct.Lv_4 == 0))

                                select a).ToList();

                            int SpecCnt = 0;

                            BasicParametrAdditional basicAdditional =
                                (from a in kpiWebDataContext.BasicParametrAdditional
                                    where a.BasicParametrAdditionalID == Basic.BasicParametersTableID
                                    select a).FirstOrDefault();
                            if (basicAdditional.SubvisionLevel != 4)
                            {
                                AllBasicsUsersCanEdit += UserTableList.Count();
                            }
                            else
                            {
                                foreach (UsersTable CurUSer in UserTableList)
                                {
                                    SpecCnt += (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                        where a.FK_ThirdLevelSubdivisionTable == CurUSer.FK_ThirdLevelSubdivisionTable
                                        && a.Active == true
                                        select a).Count();
                                }
                                AllBasicsUsersCanEdit += (UserTableList.Count() * SpecCnt);
                            }
                       */
                            AllBasicsUsersCanEdit += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                   where a.FK_ReportArchiveTable == ReportID
                                                         && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                                         && ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) || (currentStruct.Lv_0 == 0))
                                                            && ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) || (currentStruct.Lv_1 == 0))
                                                            && ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) || (currentStruct.Lv_2 == 0))
                                                            && ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) || (currentStruct.Lv_3 == 0))
                                                            && ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) || (currentStruct.Lv_4 == 0))
                                                   select a).Count();

                            AllConfirmedBasics += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                   where a.FK_ReportArchiveTable == ReportID
                                                         && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                                         && a.Status == 4
                                                         && ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) || (currentStruct.Lv_0 == 0))
                                                            && ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) || (currentStruct.Lv_1 == 0))
                                                            && ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) || (currentStruct.Lv_2 == 0))
                                                            && ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) || (currentStruct.Lv_3 == 0))
                                                            && ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) || (currentStruct.Lv_4 == 0))
                                                   select a).Count();
                        }
                        bool BasicsAreConfirmed = true;
                        if (AllBasicsUsersCanEdit != AllConfirmedBasics)
                        {
                            BasicsAreConfirmed = false;
                        }

                        #endregion

                        if (AllBasicsUsersCanEdit == 0)
                        {
                            dataRow["Progress"] = "";
                        }
                        else
                        {
                            dataRow["Progress"] =
                                ((((float) AllConfirmedBasics)*100)/((float) AllBasicsUsersCanEdit)).ToString("0.0") +"%";
                            dataTable.Rows.Add(dataRow);
                        }


                       // dataTable.Rows.Add(dataRow);
                    }
                    #endregion
                    #region DataGridBind
                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = "Подразделения";
                    if (StructDeepness(mainStruct) > 3)
                    {
                        Grid.Columns[3].HeaderText = "Направления подготовки";
                    }
                    Grid.DataBind();
                    #endregion
                    #region постнастройка страницы
                    Grid.Columns[12].Visible = false;
                    Grid.Columns[11].Visible = false;
                    Grid.Columns[9].Visible = false;
                  //  Grid.Columns[8].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[6].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;
                    if (StructDeepness(mainStruct) >2) 
                    {
                        Grid.Columns[10].Visible = false;
                    }
                    #endregion
                }
                else 
                {
                    //error // wrong ViewType
                }
                RefreshHistory();

                if ((ViewType == 1) && ((ParamType == 0) || (ParamType == 1)))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowLegend", "ShowLegend()", true);
                }
            }
        }
        protected void ButtonConfirmClick(object sender, EventArgs e)
        {            
            /*
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                int ParamType = CurrentRectorSession.sesParamType;

                if (ParamType == 0) // indicator
                {
                    CollectedIndocators Indicator = (from a in kpiWebDataContext.CollectedIndocators
                        where a.FK_Indicators == Convert.ToInt32(button.CommandArgument)
                        select a).FirstOrDefault();
                    Indicator.Confirmed = true;
                    kpiWebDataContext.SubmitChanges();
                    Response.Redirect("~/Rector/Result.aspx");
                }
                else if (ParamType == 1) // calculated
                {
                    CollectedCalculatedParametrs Calculated = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                                     where a.FK_CalculatedParametrs == Convert.ToInt32(button.CommandArgument)
                                                     select a).FirstOrDefault();
                    Calculated.Confirmed = true;
                    kpiWebDataContext.SubmitChanges();
                    Response.Redirect("~/Rector/Result.aspx");
                }
            }*/
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
                else if (currentRectorSession.sesViewType == 3)
                {
                    currentRectorSession.sesStruct = StructDeeper(currentRectorSession.sesStruct, Convert.ToInt32(button.CommandArgument));  
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
        protected void Button3Click(object sender, EventArgs e) //по специальности
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
                Response.Redirect("~/Rector/RectorChooseReport.aspx");
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
        protected void Button4_Click(object sender, EventArgs e)
        {           
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        private void BindTree(IEnumerable<MyObject> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));
            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.Name, node.Id.ToString());
                
                if (node.Active == 1)
                {
                    newNode.NavigateUrl = node.UrlAddr;
                }
                else
                {
                    newNode.SelectAction = TreeNodeSelectAction.None;
                }
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
        public void RefreshHistory()
        {
            /*
            #region history            
            RectorHistorySession rectorHistory_ = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory_ == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (rectorHistory_.Visible == true)
            {
                Button6.Text = "Скрыть историю";
                TreeView1.Visible = true;
                List<MyObject> list = new List<MyObject>();
                for (int i = 0; i < rectorHistory_.SessionCount; i++)
                {
                    RectorSession curSesion = rectorHistory_.RectorSession[i];
                    int tmp = rectorHistory_.CurrentSession == i ? 0:1;
                    list.Add(new MyObject() { Id = i+1, Name = curSesion.sesName, ParentId = i, UrlAddr = "Result?&HLevel="+i,Active=tmp });                       
                }
                BindTree(list, null);
                TreeView1.ExpandAll();
            }
            else
            {
                Button6.Text = "Показать историю";
                TreeView1.Visible = false;
                TreeView1.Nodes.Clear();
                TreeView1.DataBind();
            }
            #endregion
             */
        }
        protected void Button6_Click(object sender, EventArgs e)
        {

            RectorHistorySession rectorHistory_ = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory_ == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (rectorHistory_.Visible == true)
            {
                rectorHistory_.Visible = false;
            }
            else
            {
                rectorHistory_.Visible = true;
            }
            Session["rectorHistory"] = rectorHistory_;

            RefreshHistory();
        }
        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            ShowUnConfirmed unConfirmed =new ShowUnConfirmed(true);
            Session["unConfirmed"] = unConfirmed;
            Response.Redirect("~/Rector/Result.aspx");
        }
        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            var ColorLable = e.Row.FindControl("Color") as Label;
            var PageConfirmButton = e.Row.FindControl("ConfirmButton") as Button;
            var PageButton2 = e.Row.FindControl("Button2") as Button;

            //// костыль 0%
            var Button1_ = e.Row.FindControl("Button1") as Button;                       
            var PLable_ = e.Row.FindControl("ProgressLable") as Label;
            if ((Button1_ != null) && (PLable_ != null))
            {
                if (PLable_.Text == "0,0%")
                {
                    Button1_.Enabled = false;
                }
            }
            

            //end костыль 0%

            if (ColorLable != null)
            {

                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                if ((CurrentRectorSession.sesViewType==1)||(CurrentRectorSession.sesParamType==0))
                {

                    PageButton2.Enabled = true;
                }
                else
                {
                    PageButton2.Enabled = false;
                }

                PageConfirmButton.Enabled = false;

                int ColorNumber = -1;
                if (int.TryParse(ColorLable.Text, out ColorNumber) && ColorNumber > -1)
                {
                    switch (ColorNumber)
                    {
                        case 0:
                        {
                         
                         break;   
                        }
                        case 1: // утверждено
                        {
                            e.Row.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
                            PageButton2.Enabled = true;
                            break;
                        }
                        case 2: // можно утвердить
                        {
                            e.Row.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
                            PageButton2.Enabled = true;
                            break;
                        }
                        case 3: // рассчитано на неутвержденных данных
                        {
                            e.Row.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");
                            PageConfirmButton.Enabled = true;
                            PageButton2.Enabled = true;
                            break;
                        }
                        default:
                        {                            
                            break;
                        }
                    }                    
                }



            }
#endregion
            var ConfirmButton = e.Row.FindControl("ConfirmButton") as Button;
            if (ConfirmButton != null)
            {
                ConfirmButton.OnClientClick = "javascript:return showCommentSection(" + ConfirmButton.CommandArgument+ ");";
            }

        }
        protected void Button8_Click(object sender, EventArgs e)
        {         

        }
        public void DoConfirm(int ParamId)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
            int ParamType = CurrentRectorSession.sesParamType;

            if (ParamType == 0) // indicator
            {
                CollectedIndocators Indicator = (from a in kpiWebDataContext.CollectedIndocators
                                                 where a.FK_Indicators == ParamId
                                                 select a).FirstOrDefault();
                Indicator.Confirmed = true;
                kpiWebDataContext.SubmitChanges();
                #region save params in DB with comment
                ConfirmationHistory ConfirmParam = new ConfirmationHistory();
                ConfirmParam.Date = DateTime.Now;
                ConfirmParam.FK_IndicatorsTable = ParamId;
                ConfirmParam.FK_ReportTable = CurrentRectorSession.sesReportID;
                ConfirmParam.FK_UsersTable = userID;
                ConfirmParam.Name = "Подтверждение ЦП проректором";
                ConfirmParam.Comment = TextBox1.Text;
                kpiWebDataContext.ConfirmationHistory.InsertOnSubmit(ConfirmParam);
                kpiWebDataContext.SubmitChanges();
                #endregion
                Response.Redirect("~/Rector/Result.aspx");
            }
            else if (ParamType == 1) // calculated
            {
                CollectedCalculatedParametrs Calculated = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                                           where a.FK_CalculatedParametrs == ParamId
                                                           select a).FirstOrDefault();
                Calculated.Confirmed = true;
                kpiWebDataContext.SubmitChanges();
                #region save params in DB with comment
                ConfirmationHistory ConfirmParam = new ConfirmationHistory();
                ConfirmParam.Date = DateTime.Now;
                ConfirmParam.FK_CalculatedParamTable = ParamId;
                ConfirmParam.FK_ReportTable = CurrentRectorSession.sesReportID;
                ConfirmParam.FK_UsersTable = userID;
                ConfirmParam.Name = "Подтверждение ПД проректором";
                ConfirmParam.Comment = TextBox1.Text;
                kpiWebDataContext.ConfirmationHistory.InsertOnSubmit(ConfirmParam);
                kpiWebDataContext.SubmitChanges();
                #endregion
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void ButtonOweClick(object sender, EventArgs e)
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

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "");

                    currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 3;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
           
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }       
    }
}
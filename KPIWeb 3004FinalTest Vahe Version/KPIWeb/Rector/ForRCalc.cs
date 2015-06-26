using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;



namespace KPIWeb.Rector
{
    public class ForRCalc
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
        public static List<Struct> GetAllSecondLevel()
        {
            List<Struct> tmpStrucList = new List<Struct>();
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();          

                        tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                        join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                        on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                        where a.Active == true
                                        && b.Active == true
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
    

            return tmpStrucList;
        }
        public static List<Struct> GetChildStructList(Struct ParentStruct, int ReportID)
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
                                        join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                        on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubmisionTableId

                                        where a.Active == true
                                        && b.FK_ReportArchiveTableId == ReportID
                                        && b.Active == true
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
        public static List<Struct> GetChildStructList(Struct ParentStruct, int ReportID, int SpecID)
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
                                        }).OrderBy(x => x.Lv_0).ToList();
                        break;
                    }
                case 0: // возвращаем все универы
                    {

                        tmpStrucList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                        join z in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                            on a.FirstLevelSubdivisionTableID equals z.FK_FirstLevelSubmisionTableId
                                        join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                        on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                        join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                        on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                        join d in kpiWebDataContext.FourthLevelSubdivisionTable
                                        on c.ThirdLevelSubdivisionTableID equals d.FK_ThirdLevelSubdivisionTable
                                        where a.Active == true
                                        && a.FK_ZeroLevelSubvisionTable == ParentStruct.Lv_0
                                        && z.FK_ReportArchiveTableId == ReportID
                                            && z.Active == true
                                        && ((SpecID == 0) || (SpecID == d.FK_Specialization))
                                        select new Struct(1, "")
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
                                        select new Struct(1, "")
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
                                        select new Struct(1, "")
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
                    if ((uniqeStruct[uniqeStruct.Count - 1].Lv_0 != curStruct.Lv_0) ||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_1 != curStruct.Lv_1) ||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_2 != curStruct.Lv_2) ||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_3 != curStruct.Lv_3) ||
                    (uniqeStruct[uniqeStruct.Count - 1].Lv_4 != curStruct.Lv_4))
                    {
                        uniqeStruct.Add(curStruct);
                    }
                }
            }
            return uniqeStruct;
        }
        public static float GetCalculatedWithParams(Struct StructToCalcFor, int ParamType, int ParamID, int ReportID, int SpecID) // читает показатель
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            float result = 0;
            if (ParamType == 0) // считаем целевой показатель
            {
                IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                             where a.IndicatorsTableID == ParamID
                                             select a).FirstOrDefault();
                return
                    (float)CalculateAbb.CalculateForLevel(1, Abbreviature.CalculatedAbbToFormula(Indicator.Formula)
                        , ReportID, SpecID, StructToCalcFor.Lv_0
                        , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4,
                        StructToCalcFor.Lv_5, 0);
            }
            else if (ParamType == 1) // считаем рассчетный
            {
                CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                                                  where a.CalculatedParametrsID == ParamID
                                                  select a).FirstOrDefault();
                return (float)CalculateAbb.CalculateForLevel(1, Calculated.Formula, ReportID, SpecID, StructToCalcFor.Lv_0
                        , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4,
                        StructToCalcFor.Lv_5, 0);
            }
            else if (ParamType == 2) // суммируем базовый
            {
                return (float)CalculateAbb.SumForLevel(ParamID, ReportID, SpecID, StructToCalcFor.Lv_0
                    , StructToCalcFor.Lv_1, StructToCalcFor.Lv_2, StructToCalcFor.Lv_3, StructToCalcFor.Lv_4,
                    StructToCalcFor.Lv_5);
            }
            else
            {
                //error
            }
            return result;
        }
        public static int StructDeepness(Struct CurrentStruct)
        {
            int tmp = 0;
            if (CurrentStruct.Lv_0 != 0) tmp++;
            if (CurrentStruct.Lv_1 != 0) tmp++;
            if (CurrentStruct.Lv_2 != 0) tmp++;
            if (CurrentStruct.Lv_3 != 0) tmp++;
            if (CurrentStruct.Lv_4 != 0) tmp++;
            if (CurrentStruct.Lv_5 != 0) tmp++;
            return tmp;
        }
        public static Struct StructDeeper(Struct parentStruct, int nextID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            int lv0 = parentStruct.Lv_0;
            int lv1 = parentStruct.Lv_1;
            int lv2 = parentStruct.Lv_2;
            int lv3 = parentStruct.Lv_3;
            int lv4 = parentStruct.Lv_4;
            int lv5 = parentStruct.Lv_5;
            string name = parentStruct.Name;

            Struct tmp = new Struct(lv0, lv1, lv2, lv3, lv4, lv5, name);

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
        public static int GetLastID(Struct currentStruct)
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
        public static float CalculatedForDB(float input)
        {
            float tmp = (float)input;

            if ((tmp < -(float)1E+20) || (tmp > (float)1E+20)
                || (tmp == null) || (float.IsNaN(tmp))
                || (float.IsInfinity(tmp)) || (float.IsNegativeInfinity(tmp))
                || (float.IsPositiveInfinity(tmp)) || (!tmp.ToString().IsFloat()))
            {
                tmp = (float)1E+20;
            }
            return tmp;
        }

        public static ChartOneValue GetCalculatedIndicator(int ReportID, IndicatorsTable Indicator, FirstLevelSubdivisionTable Academy, SecondLevelSubdivisionTable Faculty) // academyID == null && facultyID==null значит для всего КФУ
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            float Planned_Value = 0;
            string Name_ = "";
            float Value_ = 0;
            #region plannedIndicator
            PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                             where a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                                                   && a.Date > DateTime.Now
                                             select a).OrderBy(x => x.Date).FirstOrDefault();
            if (plannedValue != null)
            {
                Planned_Value = (float)plannedValue.Value;
            }
            #endregion
            #region Name
            if ((Academy == null) && (Faculty == null))
            {
                if (Indicator.Measure != null)
                {
                    if (Indicator.Measure.Length > 0)
                    {
                        Name_ = Indicator.Name + " (" + Indicator.Measure + ")";
                    }
                    else
                    {
                        Name_ = Indicator.Name;
                    }
                }
                else
                {
                    Name_ = Indicator.Name;
                }
            }
            else if (Faculty != null)
            {
                Name_ = Faculty.Name;
            }
            else if (Academy != null)
            {
                Name_ = Academy.Name;
            }



            #endregion
            #region
            //ForRCalc.Struct mainStruct = mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
            CollectedIndicatorsForR collected = new CollectedIndicatorsForR();
            if ((Academy == null) && (Faculty == null))
            {
                //mainStruct = new ForRCalc.Struct(1, 0, 0, 0, 0, "N");
                collected = (from a in kpiWebDataContext.CollectedIndicatorsForR
                             where a.FK_ReportArchiveTable == ReportID
                             && a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                             && a.FK_FirstLevelSubdivisionTable == null
                             && a.FK_SecondLevelSubdivisionTable == null
                             select a).FirstOrDefault();
            }
            else if (Faculty != null)
            {
                //mainStruct = new ForRCalc.Struct(1, Faculty.FK_FirstLevelSubdivisionTable, Faculty.SecondLevelSubdivisionTableID, 0, 0, "N");
                collected = (from a in kpiWebDataContext.CollectedIndicatorsForR
                             where a.FK_ReportArchiveTable == ReportID
                             && a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                             && a.FK_FirstLevelSubdivisionTable == Faculty.FK_FirstLevelSubdivisionTable
                             && a.FK_SecondLevelSubdivisionTable == Faculty.SecondLevelSubdivisionTableID
                             select a).FirstOrDefault();
            }
            else if (Academy != null)
            {
                //mainStruct = new ForRCalc.Struct(1, Academy.FirstLevelSubdivisionTableID, 0, 0, 0, "N");
                collected = (from a in kpiWebDataContext.CollectedIndicatorsForR
                             where a.FK_ReportArchiveTable == ReportID
                             && a.FK_IndicatorsTable == Indicator.IndicatorsTableID
                             && a.FK_FirstLevelSubdivisionTable == Academy.FirstLevelSubdivisionTableID
                             && a.FK_SecondLevelSubdivisionTable == null
                             select a).FirstOrDefault();
            }
            /*    
        float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0, Indicator.IndicatorsTableID, ReportID, 0));

        if (tmp == (float)1E+20)
        {
            Value_ = 0;
        }
        else
        {
            Value_ = tmp;
        }
        */
            if (collected == null)
            {
                Value_ = 0;
            }
            else
                if (collected.Value == null)
                {
                    Value_ = 0;
                }
                else
                {
                    Value_ = (float)collected.Value;
                }
            #endregion
            ChartOneValue DataRowForChart = new ChartOneValue(Name_, Value_, Planned_Value);
            return DataRowForChart;
        }
  
    }
}
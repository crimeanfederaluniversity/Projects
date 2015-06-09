using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RAnalitics : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            List<IndicatorsTable> Indicators = (
                            from a in kpiWebDataContext.IndicatorsTable
                            join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                on a.IndicatorsTableID equals b.FK_IndicatorsTable
                            join c in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                            on a.IndicatorsTableID equals c.FK_IndicatorsTable
                            where
                                a.Active == true
                                && b.CanView == true
                                && c.FK_ReportArchiveTable == 1
                            select a).OrderBy(mc => mc.IndicatorsTableID).ToList();

                        ChartValueArray Chart1 = new ChartValueArray(Indicators.Count(), "График достижения плановых значений целевых показателей");
                        int i=0;
                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                                             where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                                                   && a.Date > DateTime.Now
                                                             select a).OrderBy(x => x.Date).FirstOrDefault();

                            string Name_="";
                            float Planned_Value = 0;
                            float Value_ = 0;     
               
                            if (plannedValue != null)
                            {
                                Planned_Value = (float) plannedValue.Value;
                            }
                            if (CurrentIndicator.Measure != null)
                            {
                                if (CurrentIndicator.Measure.Length > 2)
                                {
                                    Name_ = CurrentIndicator.Name + " (" + CurrentIndicator.Measure + ")";
                                }
                                else 
                                {
                                    Name_ = CurrentIndicator.Name;
                                }
                                
                            }
                            else
                            {
                                Name_ = CurrentIndicator.Name;
                            }
                          
                            ForRCalc.Struct mainStruct = new ForRCalc.Struct(1,0,0,0,0,"N");
                            float tmp = ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, 0,CurrentIndicator.IndicatorsTableID, 1, 0));

                            if (tmp == (float)1E+20)
                            {
                                Value_ = 0;
                            }
                            else
                            {
                                Value_ = tmp;
                            }
                            Chart1.ChartValues[i] = new ChartOneValue(Name_, Value_, Planned_Value);
                            i++;
                        }
        }                
    }
}
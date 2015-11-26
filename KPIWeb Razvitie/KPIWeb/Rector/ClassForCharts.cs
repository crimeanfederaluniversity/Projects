using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb.Rector
{
    [Serializable]
    public class ChartOneValue
    {
        public string name { get; set; }
        public float value { get; set; }
        public float planned { get; set; }

        public ChartOneValue(String name_, float value_, float planned_)
        {
            this.name = name_;
            this.value = value_;
            this.planned = planned_;
        }
        public ChartOneValue(String name_, float value_)
        {
            this.name = name_;
            this.value = value_;
        }
    }

    public class ChartValueArray
    {
        public string chartName;
        public List<ChartOneValue> ChartValues;

        public ChartValueArray(string chartName_)
        {
            this.chartName = chartName_;
            ChartValues = new List<ChartOneValue>();
        }
        public ChartValueArray(List<ChartOneValue> ChartValues_, string chartName_ )
        {
            this.chartName = chartName_;
            this.ChartValues = ChartValues_;
        }
    }
    public class ClassForCharts
    {
       

    }

    public class ChartPlannedValue
    {
        public float PlannedValue;
        public float RealValue;
        public DateTime Date;
    }
    public class ChartValueWithAllPlanned
    {
        public int IndicatorID;
        public string IndicatorName;
        public List<ChartPlannedValue> PlannedAndRealValuesList;     
    }
}
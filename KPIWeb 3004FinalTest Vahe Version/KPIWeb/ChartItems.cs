using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using KPIWeb.Rector;

namespace KPIWeb
{
    public class ChartItems
    {
        public class DataItem
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        private List<DataItem> dataItems;
        public ChartItems()
        {
            dataItems = new List<DataItem>();
        }
        public List<DataItem> GetDataSource()
        {
            return dataItems;
        }
        public void AddChartItem(string name, double value)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            IndicatorsTable Indicator = (from a in kPiDataContext.IndicatorsTable
                                         where a.Name == name
                                         select a).FirstOrDefault();
            dataItems.Add(new DataItem() { Name = name, Value = Math.Round(value, 1) });
            
        }
        public List<ChartOneValue> ReturnTopFive(List<ChartOneValue> collection)
        {
            collection.Sort(delegate(ChartOneValue value1, ChartOneValue value2)
            { return value1.value.CompareTo(value2.value); });

            collection.Reverse();
            var sort = collection.Take(5);

            List<ChartOneValue> newList = sort.ToList();
            newList.Reverse();

            return newList;
        }
        public List<ChartOneValue> Sort(List<ChartOneValue> collection)
        {
            collection.Sort(delegate(ChartOneValue value1, ChartOneValue value2)
            { return value1.value.CompareTo(value2.value); });

            return collection;
        }

        public List<ChartOneValue> SortReverse(List<ChartOneValue> collection)
        {
            collection.Sort(delegate(ChartOneValue value1, ChartOneValue value2)
            { return value1.value.CompareTo(value2.value); });

            collection.Reverse();

            return collection;
        }

    }
}
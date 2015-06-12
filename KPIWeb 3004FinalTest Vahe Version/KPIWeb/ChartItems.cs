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
        private DataTable dataTable;
        private DataRow dataRow;

        public ChartItems()
        {
            dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Name"));
            dataTable.Columns.Add(new DataColumn("Value"));
        }
        public void AddChartItem(string name, double value)
        {
            dataRow = dataTable.NewRow();
            dataRow["Name"] = name;
            dataRow["Value"] = value;
            dataTable.Rows.Add(dataRow);
        }

        public DataTable GetDataSource()
        {
            return dataTable;
        }

        public List<ChartOneValue> ReturnTopFive(List<ChartOneValue> collection)
        {
            List<ChartOneValue> newList = new List<ChartOneValue>();

            collection.Sort(delegate(ChartOneValue value1, ChartOneValue value2)
            { return value1.value.CompareTo(value2.value); });
            collection.Reverse();

            var sort = collection.Take(5);

            foreach (ChartOneValue item in sort)
            {
                newList.Add(item);
            }

            newList.Reverse();
            return newList;
        }

    }
}
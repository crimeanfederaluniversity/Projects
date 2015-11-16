using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public class RGetCalculated
    {
        
    }

    public class RectorChooseReportClass
    {
        public int GetNewestReportId()
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            CollectedIndicatorsForR lastReport = (from a in kPiDataContext.CollectedIndicatorsForR
                where a.Active == true
                && a.FK_ReportArchiveTable !=null
                select a).OrderByDescending(mc => mc.FK_ReportArchiveTable).FirstOrDefault();
            if (lastReport == null)
                return 0;
            int lastReportId = Convert.ToInt32( lastReport.FK_ReportArchiveTable);
            return lastReportId;       
        }
        public ListItem[] GetListItemCollectionWithReports()
        {
            
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int i = 0;
            List<ReportArchiveTable> reportsList = (from a in kPiDataContext.ReportArchiveTable
                where a.ReportArchiveTableID != 1
                      && a.ReportArchiveTableID != 3
                      && a.Active == true
                select a).ToList();
            ListItem[] myListItemCollection = new ListItem[reportsList.Count+1];
            /*
            {
                ListItem newItem = new ListItem();
                newItem.Text = "Текущее значение";
                newItem.Value = "0";
                myListItemCollection[i] = (newItem);
                i++;
            }
            */
            {
                ListItem newItem = new ListItem();
                newItem.Text = "Расчет нулевых значений целевых показателей.";
                newItem.Value = "100500";
                myListItemCollection[i] = (newItem);
                i++;
            }

            foreach (ReportArchiveTable currentReport in reportsList)
            {
                ListItem newItem = new ListItem();
                newItem.Text = currentReport.Name;
                newItem.Value = currentReport.ReportArchiveTableID.ToString();
                myListItemCollection[i] = (newItem);
                i++;
            }

            return myListItemCollection;
        }
    }
}
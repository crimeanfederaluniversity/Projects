using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    public class CalculateIndicator
    {
        public static double? Calculate(int IndicatorsTableID, int ReportArchiveTableID)
        {
            double? returnValue = -1;

            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

            switch (IndicatorsTableID)
            {
                case 1:
                    double? a = (from item in KPIWebDataContext.CollectedBasicParametersTables
                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                             item.FK_BasicParametersTable == 15
                             select item.CollectedValue).Sum();

                    double? b = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 8
                                 select item.CollectedValue).Sum();

                    double? c = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 35
                                 select item.CollectedValue).Sum();

                    double? d = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 10
                                 select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double M_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTables
                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                             item.FK_BasicParametersTable == 15
                             select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 8
                                 select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 35
                                 select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 10
                                 select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double A_pk =  (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTables
                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                             item.FK_BasicParametersTable == 15
                             select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 8
                                 select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 35
                                 select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTables
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 10
                                 select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double B_pk =  (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTables
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 15
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTables
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 8
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTables
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 35
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTables
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 10
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double S_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    returnValue = (M_pk + A_pk) / (B_pk + S_pk + M_pk + A_pk) * 100;
                    break;
                case 2:
                    //
                    break;
                case 3:
                    //
                    break;
                case 4:
                    //
                    break;
                case 5:
                    //
                    break;
                case 6:
                    //
                    break;
                default:
                    returnValue = -1;
                    break;
            }
            return returnValue;
        }
    }
}
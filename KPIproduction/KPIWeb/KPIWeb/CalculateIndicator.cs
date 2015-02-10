using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    public class CalculateIndicator
    {
        public static double Calculate(int IndicatorsTableID, int ReportArchiveTableID)
        {
            double? returnValue = -1;


            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();

            switch (IndicatorsTableID)
            {
                case 1:
                    double? a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 15
                                 select item.CollectedValue).Sum();

                    double? b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 21
                                 select item.CollectedValue).Sum();

                    double? c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 28
                                 select item.CollectedValue).Sum();

                    double? d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 35
                                 select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double? M_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 2
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 5
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 8
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 11
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double? A_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 63
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 69
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 75
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 81
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double? B_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 42
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 47
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 52
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 57
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    double? S_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    returnValue = (M_pk + A_pk) / (B_pk + S_pk + M_pk + A_pk) * 100;

                    break;








                case 2:

                    double? SBB_EG = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 108
                                      select item.CollectedValue).Sum();

                    double? SBB_EG_DI = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                         item.FK_BasicParametersTable == 109
                                         select item.CollectedValue).Sum();

                    double? ChB_EG_DI = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                         item.FK_BasicParametersTable == 113
                                         select item.CollectedValue).Sum();

                    double? ChB_EG = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 112
                                      select item.CollectedValue).Sum();
                    double? ChS_EG = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 119
                                      select item.CollectedValue).Sum();
                    double? ChS_EG_DI = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                         item.FK_BasicParametersTable == 120
                                         select item.CollectedValue).Sum();

                    if (SBB_EG == null) SBB_EG = 0;
                    if (SBB_EG_DI == null) SBB_EG_DI = 0;
                    if (ChB_EG_DI == null) ChB_EG_DI = 0;
                    if (ChB_EG == null) ChB_EG = 0;
                    if (ChS_EG == null) ChS_EG = 0;
                    if (ChS_EG_DI == null) ChS_EG_DI = 0;







                    returnValue = ((SBB_EG * (ChB_EG + ChS_EG) + SBB_EG_DI * (ChB_EG + ChS_EG_DI)) / (ChB_EG + ChS_EG + ChB_EG_DI + ChS_EG_DI));
                    break;










                case 3:

                    double? a_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                          where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                          item.FK_BasicParametersTable == 16
                                          select item.CollectedValue).Sum();

                    double? b_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                          where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                          item.FK_BasicParametersTable == 23
                                          select item.CollectedValue).Sum();

                    double? c_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                          where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                          item.FK_BasicParametersTable == 30
                                          select item.CollectedValue).Sum();

                    double? d_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                          where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                          item.FK_BasicParametersTable == 37
                                          select item.CollectedValue).Sum();

                    if (a_M_dvo_pk == null) a_M_dvo_pk = 0;
                    if (b_M_dvo_pk == null) b_M_dvo_pk = 0;
                    if (c_M_dvo_pk == null) c_M_dvo_pk = 0;
                    if (d_M_dvo_pk == null) d_M_dvo_pk = 0;

                    double M_dvo_pk = (double)a_M_dvo_pk + ((double)b_M_dvo_pk * 0.25) + (((double)c_M_dvo_pk + (double)d_M_dvo_pk) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 2
                                  select item.CollectedValue).Sum();

                    b_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 5
                                  select item.CollectedValue).Sum();

                    c_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 8
                                  select item.CollectedValue).Sum();

                    d_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 22
                                  select item.CollectedValue).Sum();

                    if (a_M_dvo_pk == null) a_M_dvo_pk = 0;
                    if (b_M_dvo_pk == null) b_M_dvo_pk = 0;
                    if (c_M_dvo_pk == null) c_M_dvo_pk = 0;
                    if (d_M_dvo_pk == null) d_M_dvo_pk = 0;

                    double А_pk = (double)a_M_dvo_pk + ((double)b_M_dvo_pk * 0.25) + (((double)c_M_dvo_pk + (double)d_M_dvo_pk) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 4
                                  select item.CollectedValue).Sum();

                    b_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 7
                                  select item.CollectedValue).Sum();

                    c_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 10
                                  select item.CollectedValue).Sum();

                    d_M_dvo_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 13
                                  select item.CollectedValue).Sum();

                    if (a_M_dvo_pk == null) a_M_dvo_pk = 0;
                    if (b_M_dvo_pk == null) b_M_dvo_pk = 0;
                    if (c_M_dvo_pk == null) c_M_dvo_pk = 0;
                    if (d_M_dvo_pk == null) d_M_dvo_pk = 0;

                    double А_dvo_pk = (double)a_M_dvo_pk + ((double)b_M_dvo_pk * 0.25) + (((double)c_M_dvo_pk + (double)d_M_dvo_pk) * 0.1);


                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 2
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 5
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 8
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 11
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    A_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);


                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 15
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 21
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 28
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 35
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    M_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    returnValue = (M_dvo_pk + А_dvo_pk) / (M_pk + A_pk) * 100;

                    break;













                case 4:
                    double? a_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                             item.FK_BasicParametersTable == 65
                                             select item.CollectedValue).Sum();

                    double? b_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                             item.FK_BasicParametersTable == 71
                                             select item.CollectedValue).Sum();

                    double? c_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                             item.FK_BasicParametersTable == 77
                                             select item.CollectedValue).Sum();

                    double? d_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                             where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                             item.FK_BasicParametersTable == 83
                                             select item.CollectedValue).Sum();

                    if (a_B_dco_pk_oz == null) a_B_dco_pk_oz = 0;
                    if (b_B_dco_pk_oz == null) b_B_dco_pk_oz = 0;
                    if (c_B_dco_pk_oz == null) c_B_dco_pk_oz = 0;
                    if (d_B_dco_pk_oz == null) d_B_dco_pk_oz = 0;

                    double? B_dco_pk_oz = (double)a_B_dco_pk_oz + ((double)b_B_dco_pk_oz * 0.25) + (((double)c_B_dco_pk_oz + (double)d_B_dco_pk_oz) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 43
                                     select item.CollectedValue).Sum();

                    b_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 48
                                     select item.CollectedValue).Sum();

                    c_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 53
                                     select item.CollectedValue).Sum();

                    d_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 58
                                     select item.CollectedValue).Sum();

                    if (a_B_dco_pk_oz == null) a_B_dco_pk_oz = 0;
                    if (b_B_dco_pk_oz == null) b_B_dco_pk_oz = 0;
                    if (c_B_dco_pk_oz == null) c_B_dco_pk_oz = 0;
                    if (d_B_dco_pk_oz == null) d_B_dco_pk_oz = 0;

                    double? S_dco_pk_oz = (double)a_B_dco_pk_oz + ((double)b_B_dco_pk_oz * 0.25) + (((double)c_B_dco_pk_oz + (double)d_B_dco_pk_oz) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 17
                                     select item.CollectedValue).Sum();

                    b_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 24
                                     select item.CollectedValue).Sum();

                    c_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 31
                                     select item.CollectedValue).Sum();

                    d_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 38
                                     select item.CollectedValue).Sum();

                    if (a_B_dco_pk_oz == null) a_B_dco_pk_oz = 0;
                    if (b_B_dco_pk_oz == null) b_B_dco_pk_oz = 0;
                    if (c_B_dco_pk_oz == null) c_B_dco_pk_oz = 0;
                    if (d_B_dco_pk_oz == null) d_B_dco_pk_oz = 0;

                    double? M_dco_pk_oz = (double)a_B_dco_pk_oz + ((double)b_B_dco_pk_oz * 0.25) + (((double)c_B_dco_pk_oz + (double)d_B_dco_pk_oz) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 66
                                     select item.CollectedValue).Sum();

                    b_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 72
                                     select item.CollectedValue).Sum();

                    c_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 78
                                     select item.CollectedValue).Sum();

                    d_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 84
                                     select item.CollectedValue).Sum();

                    if (a_B_dco_pk_oz == null) a_B_dco_pk_oz = 0;
                    if (b_B_dco_pk_oz == null) b_B_dco_pk_oz = 0;
                    if (c_B_dco_pk_oz == null) c_B_dco_pk_oz = 0;
                    if (d_B_dco_pk_oz == null) d_B_dco_pk_oz = 0;

                    double? B_pk_oz = (double)a_B_dco_pk_oz + ((double)b_B_dco_pk_oz * 0.25) + (((double)c_B_dco_pk_oz + (double)d_B_dco_pk_oz) * 0.1);


                    //---------------------------------------------------------------------------------------------------------

                    a_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 44
                                     select item.CollectedValue).Sum();

                    b_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 49
                                     select item.CollectedValue).Sum();

                    c_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 54
                                     select item.CollectedValue).Sum();

                    d_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 59
                                     select item.CollectedValue).Sum();

                    if (a_B_dco_pk_oz == null) a_B_dco_pk_oz = 0;
                    if (b_B_dco_pk_oz == null) b_B_dco_pk_oz = 0;
                    if (c_B_dco_pk_oz == null) c_B_dco_pk_oz = 0;
                    if (d_B_dco_pk_oz == null) d_B_dco_pk_oz = 0;

                    double? S_pk_oz = (double)a_B_dco_pk_oz + ((double)b_B_dco_pk_oz * 0.25) + (((double)c_B_dco_pk_oz + (double)d_B_dco_pk_oz) * 0.1);



                    //---------------------------------------------------------------------------------------------------------

                    a_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 18
                                     select item.CollectedValue).Sum();

                    b_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 25
                                     select item.CollectedValue).Sum();

                    c_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 32
                                     select item.CollectedValue).Sum();

                    d_B_dco_pk_oz = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 39
                                     select item.CollectedValue).Sum();

                    if (a_B_dco_pk_oz == null) a_B_dco_pk_oz = 0;
                    if (b_B_dco_pk_oz == null) b_B_dco_pk_oz = 0;
                    if (c_B_dco_pk_oz == null) c_B_dco_pk_oz = 0;
                    if (d_B_dco_pk_oz == null) d_B_dco_pk_oz = 0;

                    double? M_pk_oz = (double)a_B_dco_pk_oz + ((double)b_B_dco_pk_oz * 0.25) + (((double)c_B_dco_pk_oz + (double)d_B_dco_pk_oz) * 0.1);



                    returnValue = (B_dco_pk_oz + S_dco_pk_oz + M_dco_pk_oz) / (B_pk_oz + S_pk_oz + M_pk_oz) * 100;
                    break;






                case 5:

                    double? SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 1
                                      select item.CollectedValue).Sum();

                    double? CHS_PPS_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                          where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                          item.FK_BasicParametersTable == 124
                                          select item.CollectedValue).Sum();

                    double? CHS_PPS_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                            where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                            item.FK_BasicParametersTable == 123
                                            select item.CollectedValue).Sum();

                    double? CHS_NR_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                         item.FK_BasicParametersTable == 122
                                         select item.CollectedValue).Sum();

                    double? CHS_NR_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                           where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                           item.FK_BasicParametersTable == 121
                                           select item.CollectedValue).Sum();


                    if (SCHNPR == null) SCHNPR = 0;
                    if (CHS_PPS_sh == null) CHS_PPS_sh = 0;
                    if (CHS_PPS_vnsm == null) CHS_PPS_vnsm = 0;
                    if (CHS_NR_sh == null) CHS_NR_sh = 0;
                    if (CHS_NR_vnsm == null) CHS_NR_vnsm = 0;


                    // КП / СЧНПР * (ЧС_ППС_ш+ЧС_ППС_внсм+ЧС_НР_ш+ЧС_НР_внсм)
                    returnValue = (double)SCHNPR / ((double)CHS_PPS_sh + (double)CHS_PPS_vnsm + (double)CHS_NR_sh + (double)CHS_NR_vnsm);
                    break;







                case 6:

                    double? KP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 96
                                  select item.CollectedValue).Sum();

                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();

                    CHS_PPS_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 124
                                  select item.CollectedValue).Sum();

                    CHS_PPS_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 123
                                    select item.CollectedValue).Sum();

                    CHS_NR_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 122
                                 select item.CollectedValue).Sum();

                    CHS_NR_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 121
                                   select item.CollectedValue).Sum();

                    if (KP == null) KP = 0;
                    if (SCHNPR == null) SCHNPR = 0;
                    if (CHS_PPS_sh == null) CHS_PPS_sh = 0;
                    if (CHS_PPS_vnsm == null) CHS_PPS_vnsm = 0;
                    if (CHS_NR_sh == null) CHS_NR_sh = 0;
                    if (CHS_NR_vnsm == null) CHS_NR_vnsm = 0;


                    // КП / СЧНПР * (ЧС_ППС_ш+ЧС_ППС_внсм+ЧС_НР_ш+ЧС_НР_внсм)
                    returnValue = (double)KP / (double)SCHNPR * ((double)CHS_PPS_sh + (double)CHS_PPS_vnsm + (double)CHS_NR_sh + (double)CHS_NR_vnsm);
                    break;





                case 7:

                    double? KPS5 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 98
                                    select item.CollectedValue).Sum();

                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();

                    CHS_PPS_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 124
                                  select item.CollectedValue).Sum();

                    CHS_PPS_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 123
                                    select item.CollectedValue).Sum();

                    CHS_NR_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 122
                                 select item.CollectedValue).Sum();

                    CHS_NR_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 121
                                   select item.CollectedValue).Sum();

                    if (KPS5 == null) KPS5 = 0;
                    if (SCHNPR == null) SCHNPR = 0;
                    if (CHS_PPS_sh == null) CHS_PPS_sh = 0;
                    if (CHS_PPS_vnsm == null) CHS_PPS_vnsm = 0;
                    if (CHS_NR_sh == null) CHS_NR_sh = 0;
                    if (CHS_NR_vnsm == null) CHS_NR_vnsm = 0;

                    // КПС5 / СЧНПР * (ЧС_ППС_ш+ЧС_ППС_внсм+ЧС_НР_ш+ЧС_НР_внсм)


                    returnValue = KPS5 / SCHNPR * (CHS_PPS_sh + CHS_PPS_vnsm + CHS_NR_sh + CHS_NR_vnsm);
                    break;






                case 8:
                    double? NYOKR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 99
                                     select item.CollectedValue).Sum();

                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();

                    CHS_PPS_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 124
                                  select item.CollectedValue).Sum();

                    CHS_PPS_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 123
                                    select item.CollectedValue).Sum();

                    CHS_NR_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 122
                                 select item.CollectedValue).Sum();

                    CHS_NR_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 121
                                   select item.CollectedValue).Sum();

                    if (NYOKR == null) NYOKR = 0;
                    if (SCHNPR == null) SCHNPR = 0;
                    if (CHS_PPS_sh == null) CHS_PPS_sh = 0;
                    if (CHS_PPS_vnsm == null) CHS_PPS_vnsm = 0;
                    if (CHS_NR_sh == null) CHS_NR_sh = 0;
                    if (CHS_NR_vnsm == null) CHS_NR_vnsm = 0;


                    // КП / СЧНПР * (ЧС_ППС_ш+ЧС_ППС_внсм+ЧС_НР_ш+ЧС_НР_внсм)
                    returnValue = (double)NYOKR / (double)SCHNPR * ((double)CHS_PPS_sh + (double)CHS_PPS_vnsm + (double)CHS_NR_sh + (double)CHS_NR_vnsm);
                    break;








                case 9:

                    double? a_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 62
                                        select item.CollectedValue).Sum();

                    double? b_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 68
                                        select item.CollectedValue).Sum();

                    double? c_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 74
                                        select item.CollectedValue).Sum();

                    double? d_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 80
                                        select item.CollectedValue).Sum();

                    if (a_YSB_pr == null) a_YSB_pr = 0;
                    if (b_YSB_pr == null) b_YSB_pr = 0;
                    if (c_YSB_pr == null) c_YSB_pr = 0;
                    if (d_YSB_pr == null) d_YSB_pr = 0;

                    double? YSB_pr = (double)a_YSB_pr + ((double)b_YSB_pr * 0.25) + (((double)c_YSB_pr + (double)d_YSB_pr) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 45
                                select item.CollectedValue).Sum();

                    b_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 50
                                select item.CollectedValue).Sum();

                    c_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 55
                                select item.CollectedValue).Sum();

                    d_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 60
                                select item.CollectedValue).Sum();

                    if (a_YSB_pr == null) a_YSB_pr = 0;
                    if (b_YSB_pr == null) b_YSB_pr = 0;
                    if (c_YSB_pr == null) c_YSB_pr = 0;
                    if (d_YSB_pr == null) d_YSB_pr = 0;

                    double? YYS_pr = (double)a_YSB_pr + ((double)b_YSB_pr * 0.25) + (((double)c_YSB_pr + (double)d_YSB_pr) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 14
                                select item.CollectedValue).Sum();

                    b_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 22
                                select item.CollectedValue).Sum();

                    c_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 29
                                select item.CollectedValue).Sum();

                    d_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 36
                                select item.CollectedValue).Sum();

                    if (a_YSB_pr == null) a_YSB_pr = 0;
                    if (b_YSB_pr == null) b_YSB_pr = 0;
                    if (c_YSB_pr == null) c_YSB_pr = 0;
                    if (d_YSB_pr == null) d_YSB_pr = 0;

                    double? YSM_pr = (double)a_YSB_pr + ((double)b_YSB_pr * 0.25) + (((double)c_YSB_pr + (double)d_YSB_pr) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 67
                                select item.CollectedValue).Sum();

                    b_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 73
                                select item.CollectedValue).Sum();

                    c_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 79
                                select item.CollectedValue).Sum();

                    d_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 85
                                select item.CollectedValue).Sum();

                    if (a_YSB_pr == null) a_YSB_pr = 0;
                    if (b_YSB_pr == null) b_YSB_pr = 0;
                    if (c_YSB_pr == null) c_YSB_pr = 0;
                    if (d_YSB_pr == null) d_YSB_pr = 0;

                    double? B_pk_ys = (double)a_YSB_pr + ((double)b_YSB_pr * 0.25) + (((double)c_YSB_pr + (double)d_YSB_pr) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 46
                                select item.CollectedValue).Sum();

                    b_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 51
                                select item.CollectedValue).Sum();

                    c_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 56
                                select item.CollectedValue).Sum();

                    d_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 61
                                select item.CollectedValue).Sum();

                    if (a_YSB_pr == null) a_YSB_pr = 0;
                    if (b_YSB_pr == null) b_YSB_pr = 0;
                    if (c_YSB_pr == null) c_YSB_pr = 0;
                    if (d_YSB_pr == null) d_YSB_pr = 0;

                    double? S_pk_ys = (double)a_YSB_pr + ((double)b_YSB_pr * 0.25) + (((double)c_YSB_pr + (double)d_YSB_pr) * 0.1);

                    //---------------------------------------------------------------------------------------------------------

                    a_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 19
                                select item.CollectedValue).Sum();

                    b_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 26
                                select item.CollectedValue).Sum();

                    c_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 33
                                select item.CollectedValue).Sum();

                    d_YSB_pr = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                item.FK_BasicParametersTable == 40
                                select item.CollectedValue).Sum();

                    if (a_YSB_pr == null) a_YSB_pr = 0;
                    if (b_YSB_pr == null) b_YSB_pr = 0;
                    if (c_YSB_pr == null) c_YSB_pr = 0;
                    if (d_YSB_pr == null) d_YSB_pr = 0;

                    double? M_pk_ys = (double)a_YSB_pr + ((double)b_YSB_pr * 0.25) + (((double)c_YSB_pr + (double)d_YSB_pr) * 0.1);

                    //(ИСБ_пр+ИСС_пр+ИСМ_пр) / (Б_пк_ис+С_пк_ис+М_пк_ис) * 100
                    returnValue = (YSM_pr + YYS_pr + YSB_pr) / (B_pk_ys + S_pk_ys + M_pk_ys) * 100;

                    break;





                case 10:
                    // "=" (Численность зарубежных ведущих профессоров, преподавателей и исследователей, работающих в университете не менее 1 семестра)
                    returnValue = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 114
                                   select item.CollectedValue).Sum();
                    break;




                case 11:
                    double? OOS = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 104
                                   select item.CollectedValue).Sum();
                    double? OS_VNB = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 105
                                      select item.CollectedValue).Sum();

                    if (OOS == null) OOS = 0;
                    if (OS_VNB == null) OS_VNB = 0;

                    // (ОС_ВНБ / ООС) * 100

                    returnValue = ((OS_VNB / OOS) * 100);
                    break;

                case 12:

                    OOS = (from item in KPIWebDataContext.CollectedBasicParametersTable
                           where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                           item.FK_BasicParametersTable == 104
                           select item.CollectedValue).Sum();
                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();
                    CHS_PPS_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 124
                                  select item.CollectedValue).Sum();
                    CHS_PPS_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 123
                                    select item.CollectedValue).Sum();
                    CHS_NR_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 122
                                 select item.CollectedValue).Sum();
                    CHS_NR_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 121
                                   select item.CollectedValue).Sum();

                    if (OOS == null) OOS = 0;
                    if (SCHNPR == null) SCHNPR = 0;
                    if (CHS_PPS_sh == null) CHS_PPS_sh = 0;
                    if (CHS_PPS_vnsm == null) CHS_PPS_vnsm = 0;
                    if (CHS_NR_sh == null) CHS_NR_sh = 0;
                    if (CHS_NR_vnsm == null) CHS_NR_vnsm = 0;

                    //ООС / СЧНПР * (ЧС_ППС_ш+ЧС_ППС_внсм+ЧС_НР_ш+ЧС_НР_внсм)

                    returnValue = OOS / SCHNPR * (CHS_PPS_sh + CHS_PPS_vnsm + CHS_NR_sh + CHS_NR_vnsm);
                    break;



                case 13:
                    OS_VNB = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 111
                              select item.CollectedValue).Sum();

                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();

                    double? SZP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 124
                                   select item.CollectedValue).Sum();


                    if (OS_VNB == null) OS_VNB = 0;
                    if (SCHNPR == null) SCHNPR = 0;
                    if (SZP == null) SZP = 0;



                    // (ФзпНПР(OS_VNB) / СЧНПР) / 12 / СЗП * 100
                    returnValue = (OS_VNB / SCHNPR) / 12 / SZP * 100;
                    break;


                case 14:

                    double? b_OOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 100
                                     select item.CollectedValue).Sum();

                    double? d_OOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 101
                                     select item.CollectedValue).Sum();

                    double? m_OOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 102
                                     select item.CollectedValue).Sum();

                    double? s_OOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 103
                                     select item.CollectedValue).Sum();

                    if (b_OOP == null) b_OOP = 0;
                    if (d_OOP == null) d_OOP = 0;
                    if (m_OOP == null) m_OOP = 0;
                    if (s_OOP == null) s_OOP = 0;

                    double? OOP = b_OOP + d_OOP + m_OOP + s_OOP;

                    //---------------------------------------------------------------------------------------------------------

                    double? b_CHOOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 115
                                       select item.CollectedValue).Sum();

                    double? d_CHOOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 116
                                       select item.CollectedValue).Sum();

                    double? m_CHOOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 117
                                       select item.CollectedValue).Sum();

                    double? s_CHOOP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 118
                                       select item.CollectedValue).Sum();

                    if (b_CHOOP == null) b_CHOOP = 0;
                    if (d_CHOOP == null) d_OOP = 0;
                    if (m_CHOOP == null) m_CHOOP = 0;
                    if (s_CHOOP == null) s_CHOOP = 0;

                    double? CHOOP = b_CHOOP + d_CHOOP + m_CHOOP + s_CHOOP;

                    // (ООП_ОП/ ЧООП)*100
                    returnValue = (OOP / CHOOP) * 100;
                    break;

                case 15:
                    double? b_OP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 89
                                    select item.CollectedValue).Sum();

                    double? s_OP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 91
                                    select item.CollectedValue).Sum();

                    double? m_OP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 90
                                    select item.CollectedValue).Sum();

                    double? d_OP = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 87
                                    select item.CollectedValue).Sum();

                    if (b_OP == null) b_OP = 0;
                    if (s_OP == null) s_OP = 0;
                    if (m_OP == null) m_OP = 0;
                    if (d_OP == null) d_OP = 0;

                    // "сумма"

                    returnValue = b_OP + s_OP + m_OP + d_OP;
                    break;

                case 16:

                    double? OOP_100 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 100
                                       select item.CollectedValue).Sum();

                    double? OOP_103 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 103
                                       select item.CollectedValue).Sum();

                    double? OOP_102 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 102
                                       select item.CollectedValue).Sum();

                    double? OOP_101 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                       where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                       item.FK_BasicParametersTable == 101
                                       select item.CollectedValue).Sum();

                    if (OOP_100 == null) OOP_100 = 0;
                    if (OOP_101 == null) OOP_101 = 0;
                    if (OOP_102 == null) OOP_102 = 0;
                    if (OOP_103 == null) OOP_103 = 0;

                    double? buf_OOPSUM = (OOP_101 + OOP_102 + OOP_103 + OOP_100);


                    OOP_100 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                               where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                               item.FK_BasicParametersTable == 115
                               select item.CollectedValue).Sum();

                    OOP_103 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                               where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                               item.FK_BasicParametersTable == 118
                               select item.CollectedValue).Sum();

                    OOP_102 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                               where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                               item.FK_BasicParametersTable == 117
                               select item.CollectedValue).Sum();

                    OOP_101 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                               where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                               item.FK_BasicParametersTable == 116
                               select item.CollectedValue).Sum();

                    if (OOP_100 == null) OOP_100 = 0;
                    if (OOP_101 == null) OOP_101 = 0;
                    if (OOP_102 == null) OOP_102 = 0;
                    if (OOP_103 == null) OOP_103 = 0;

                    double? buf_OOPSUM_2 = (OOP_101 + OOP_102 + OOP_103 + OOP_100);

                    //(ООП_СОТ/ ЧООП)*100

                    returnValue = ((buf_OOPSUM / buf_OOPSUM_2) * 100);


                    break;

                case 17:

                    double? KP_65 = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                     where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                     item.FK_BasicParametersTable == 97
                                     select item.CollectedValue).Sum();

                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();

                    CHS_PPS_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                  where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                  item.FK_BasicParametersTable == 124
                                  select item.CollectedValue).Sum();

                    CHS_PPS_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                    where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                    item.FK_BasicParametersTable == 123
                                    select item.CollectedValue).Sum();

                    CHS_NR_sh = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                 where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                 item.FK_BasicParametersTable == 122
                                 select item.CollectedValue).Sum();

                    CHS_NR_vnsm = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 121
                                   select item.CollectedValue).Sum();

                    if (KP_65 == null) KP_65 = 0;
                    if (SCHNPR == null) SCHNPR = 0;
                    if (CHS_PPS_sh == null) CHS_PPS_sh = 0;
                    if (CHS_PPS_vnsm == null) CHS_PPS_vnsm = 0;
                    if (CHS_NR_sh == null) CHS_NR_sh = 0;
                    if (CHS_NR_vnsm == null) CHS_NR_vnsm = 0;

                    // КП / СЧНПР * (ЧС_ППС_ш+ЧС_ППС_внсм+ЧС_НР_ш+ЧС_НР_внсм)

                    returnValue = KP_65 / SCHNPR * (CHS_PPS_sh + CHS_PPS_vnsm + CHS_NR_sh + CHS_NR_vnsm);

                    break;

                case 18:

                    double? kol_MM = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 88
                                      select item.CollectedValue).Sum();

                    if (kol_MM == null) kol_MM = 0;
                    // "="
                    returnValue = kol_MM;
                    break;

                case 19:

                    SCHNPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                              where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                              item.FK_BasicParametersTable == 1
                              select item.CollectedValue).Sum();

                    double? PK_NPR = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                      where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                      item.FK_BasicParametersTable == 106
                                      select item.CollectedValue).Sum();

                    if (PK_NPR == null) PK_NPR = 0;
                    if (SCHNPR == null) SCHNPR = 0;


                    // (ПК_НПР /  СЧНПР) *100

                    returnValue = (PK_NPR / SCHNPR) * 100;

                    break;


                case 20:

                    //Б_Р_пк 
                    double? a_B_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 64
                                        select item.CollectedValue).Sum();

                    double? b_B_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 70
                                        select item.CollectedValue).Sum();

                    double? c_B_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 76
                                        select item.CollectedValue).Sum();

                    double? d_B_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 82
                                        select item.CollectedValue).Sum();
                    if (a_B_R_pk == null) a_B_R_pk = 0;
                    if (b_B_R_pk == null) b_B_R_pk = 0;
                    if (c_B_R_pk == null) c_B_R_pk = 0;
                    if (d_B_R_pk == null) d_B_R_pk = 0;

                    double? B_R_pk = (double)a_B_R_pk + ((double)b_B_R_pk * 0.25) + (((double)c_B_R_pk + (double)d_B_R_pk) * 0.1);

                    //М_Р_пк

                    double? a_M_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 20
                                        select item.CollectedValue).Sum();

                    double? b_M_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 20
                                        select item.CollectedValue).Sum();

                    double? c_M_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 20
                                        select item.CollectedValue).Sum();

                    double? d_M_R_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 20
                                        select item.CollectedValue).Sum();

                    if (a_M_R_pk == null) a_M_R_pk = 0;
                    if (b_M_R_pk == null) b_M_R_pk = 0;
                    if (c_M_R_pk == null) c_M_R_pk = 0;
                    if (d_M_R_pk == null) d_M_R_pk = 0;

                    double? M_R_pk = (double)a_M_R_pk + ((double)b_M_R_pk * 0.25) + (((double)c_M_R_pk + (double)d_M_R_pk) * 0.1);
                    //А_Р_пк 

                    double? a_A_P_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 3
                                        select item.CollectedValue).Sum();

                    double? b_A_P_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 6
                                        select item.CollectedValue).Sum();

                    double? c_A_P_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 9
                                        select item.CollectedValue).Sum();

                    double? d_A_P_pk = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                        where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                        item.FK_BasicParametersTable == 12
                                        select item.CollectedValue).Sum();

                    if (a_A_P_pk == null) a_A_P_pk = 0;
                    if (b_A_P_pk == null) b_A_P_pk = 0;
                    if (c_A_P_pk == null) c_A_P_pk = 0;
                    if (d_A_P_pk == null) d_A_P_pk = 0;

                    double? A_R_pk = (double)a_A_P_pk + ((double)b_A_P_pk * 0.25) + (((double)c_A_P_pk + (double)d_A_P_pk) * 0.1);
                    //M_пк

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 15
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 21
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 28
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 35
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    M_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //Б_пк

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 63
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 69
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 75
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 81
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    B_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);


                    //А_пк

                    a = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 2
                         select item.CollectedValue).Sum();

                    b = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 5
                         select item.CollectedValue).Sum();

                    c = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 8
                         select item.CollectedValue).Sum();

                    d = (from item in KPIWebDataContext.CollectedBasicParametersTable
                         where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                         item.FK_BasicParametersTable == 11
                         select item.CollectedValue).Sum();

                    if (a == null) a = 0;
                    if (b == null) b = 0;
                    if (c == null) c = 0;
                    if (d == null) d = 0;

                    A_pk = (double)a + ((double)b * 0.25) + (((double)c + (double)d) * 0.1);

                    //((Б_Р_пк+М_Р_пк+А_Р_пк) / (Б_пк+М_пк+А_пк)) * 100

                    returnValue = (B_R_pk + M_R_pk + A_R_pk) / (B_pk + M_pk + A_pk);
                    break;


                case 21:
                    // Количество базовых кафедр, открытых на предприятиях и в организациях реального сектора экономики
                    returnValue = (from item in KPIWebDataContext.CollectedBasicParametersTable
                                   where item.FK_ReportArchiveTable == ReportArchiveTableID &&
                                   item.FK_BasicParametersTable == 86
                                   select item.CollectedValue).Sum();
                    break;



                default:
                    returnValue = -1;
                    break;



            }
           /* double? tmp;
           /* tmp = Math.Round(returnValue, 3);//Convert.ToString(returnValue.ToString());
           */
            return (double)Math.Round((double)returnValue, 3);
        }
    }
}
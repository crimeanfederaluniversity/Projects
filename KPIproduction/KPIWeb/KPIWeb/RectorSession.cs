using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using KPIWeb.Rector;

namespace KPIWeb
{
    [Serializable]
    public class RectorSession
    {
        public Result.Struct sesStruct { get; set; }
        public int sesViewType { get; set; } // тип просмотра // 0 - просмотр для структурных подразделений // 1 - просмотр 
     //   public int sesLevelID { get; set; }  // ID структуры
        public int sesParamID { get; set; }  // ID параметра
        public int sesParamType { get; set; }// Тип параметра // 0 - индикатор / 1 - расчетный показатель / 2 - базовый показатель
        public int sesReportID { get; set; }

        public string sesName { get; set; }

        public RectorSession(Result.Struct _sesStruct, int _sesViewType,int _sesParamID, int _sesParamType, int _sesReportID)
        {
            this.sesStruct = _sesStruct;
            this.sesViewType = _sesViewType;
            this.sesParamID = _sesParamID;
            this.sesParamType = _sesParamType;
            this.sesReportID = _sesReportID;

        }
        public RectorSession(Result.Struct _sesStruct, int _sesViewType,int _sesParamID, int _sesParamType, int _sesReportID,string _sesName)
        {
            this.sesStruct = _sesStruct;
            this.sesViewType = _sesViewType;
            this.sesParamID = _sesParamID;
            this.sesParamType = _sesParamType;
            this.sesReportID = _sesReportID;
            this.sesName = _sesName;
        }
    }

    [Serializable]
    public class RectorHistorySession
    {
        public RectorSession[] RectorSession = new RectorSession[10];//{ get; set; }
        public int count { get; set; }
    }
}
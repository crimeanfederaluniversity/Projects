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
        public int sesParamType { get; set; }// Тип параметра // 0 - целевой показатель / 1 - расчетный показатель / 2 - базовый показатель
        public int sesReportID { get; set; }
        public string sesName { get; set; }
        public int sesSpecID { get; set; }
        public RectorSession(Result.Struct _sesStruct, int _sesViewType, int _sesParamID, int _sesParamType, int _sesReportID, int _sesSpecID)
        {
            this.sesStruct = _sesStruct;
            this.sesViewType = _sesViewType;
            this.sesParamID = _sesParamID;
            this.sesParamType = _sesParamType;
            this.sesReportID = _sesReportID;
            this.sesSpecID = _sesSpecID;
        }
        public RectorSession(Result.Struct _sesStruct, int _sesViewType,int _sesParamID, int _sesParamType, int _sesReportID,int _sesSpecID,string _sesName)
        {
            this.sesStruct = _sesStruct;
            this.sesViewType = _sesViewType;
            this.sesParamID = _sesParamID;
            this.sesParamType = _sesParamType;
            this.sesReportID = _sesReportID;
            this.sesName = _sesName;
            this.sesSpecID = _sesSpecID;
        }
    }

    [Serializable]
    public class RectorHistorySession
    {
        public RectorSession[] RectorSession = new RectorSession[10];//{ get; set; }
        public int SessionCount { get; set; }
        public int CurrentSession { get; set; }
        public bool Visible { get; set; }
    }

    [Serializable]
    public class ParametrType
    {
        public int paramType { set; get; }

        public ParametrType(int _paramType)
        {
            this.paramType = _paramType;
        }
    }

    [Serializable]
    public class ShowUnConfirmed
    {
        public bool DoShowUnConfirmed { set; get; }
        public ShowUnConfirmed(bool DoShowUnConfirmed_)
        {
            this.DoShowUnConfirmed = DoShowUnConfirmed_;
        }
    }
}
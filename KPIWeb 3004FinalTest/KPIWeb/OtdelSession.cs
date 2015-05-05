using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using KPIWeb.FinKadr;
using KPIWeb.Rector;

namespace KPIWeb
{
    [Serializable]
    public class OtdelSession
    {
        public OtdelResult.Struct sesStruct { get; set; }
        public int sesViewType { get; set; } // тип просмотра // 0 - просмотр для структурных подразделений // 1 - просмотр 
     //   public int sesLevelID { get; set; }  // ID структуры
        public int sesParamID { get; set; }  // ID параметра
        public int sesParamType { get; set; }// Тип параметра // 0 - целевой показатель / 1 - расчетный показатель / 2 - базовый показатель
        public int sesReportID { get; set; }
        public string sesName { get; set; }
        public int sesSpecID { get; set; }
        public OtdelSession(OtdelResult.Struct _sesStruct, int _sesViewType, int _sesParamID, int _sesParamType, int _sesReportID, int _sesSpecID)
        {
            this.sesStruct = _sesStruct;
            this.sesViewType = _sesViewType;
            this.sesParamID = _sesParamID;
            this.sesParamType = _sesParamType;
            this.sesReportID = _sesReportID;
            this.sesSpecID = _sesSpecID;
        }
        public OtdelSession(OtdelResult.Struct _sesStruct, int _sesViewType, int _sesParamID, int _sesParamType, int _sesReportID, int _sesSpecID, string _sesName)
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
    public class OtdelHistorySession
    {
        public OtdelSession[] OtdelSession = new OtdelSession[10];//{ get; set; }
        public int SessionCount { get; set; }
        public int CurrentSession { get; set; }
        public bool Visible { get; set; }
    }

    [Serializable]
    public class OtdelParametrType
    {
        public int paramType { set; get; }

        public OtdelParametrType(int _paramType)
        {
            this.paramType = _paramType;
        }
    }

    [Serializable]
    public class OtdelShowUnConfirmed
    {
        public bool DoShowUnConfirmed { set; get; }
        public OtdelShowUnConfirmed(bool DoShowUnConfirmed_)
        {
            this.DoShowUnConfirmed = DoShowUnConfirmed_;
        }
    }
}
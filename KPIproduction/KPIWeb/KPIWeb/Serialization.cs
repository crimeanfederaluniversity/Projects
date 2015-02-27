using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    [Serializable]
    public class Serialization
    {
        public int Id { get; set; }
        public int ReportArchiveID { get; set; }
        public string ReportStr { get; set; }

        public int mode { get; set; }
        public Serialization(int IdTmp)
        {
            this.Id = IdTmp;
        }
        public Serialization(string StrTmp)
        {
            this.ReportStr = StrTmp;
        }
        
        public Serialization(int ArchiveIdTmp,object obj)
        {
            this.ReportArchiveID = ArchiveIdTmp;
        }

        public Serialization(int mode, object obj, object obj2)
        {
            this.mode = mode;
        }
    }

}
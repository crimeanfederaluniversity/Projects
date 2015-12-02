using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalPages
{
     [Serializable]
    public class Serialization
    {
        public int Id { get; set; }
        public int ReportArchiveID { get; set; }
        public string ReportStr { get; set; }

        public int mode { get; set; }
        public int l0 { get; set; }
        public int l1 { get; set; }
        public int l2 { get; set; }
        public int l3 { get; set; }
        public int l4 { get; set; }
        public int l5 { get; set; }
        public Serialization(int IdTmp)
        {
            this.Id = IdTmp;
        }
        public Serialization(string StrTmp)
        {
            this.ReportStr = StrTmp;
        }

        public Serialization(int ArchiveIdTmp, object obj)
        {
            this.ReportArchiveID = ArchiveIdTmp;
        }

        public Serialization(int mode, object obj, object obj2)
        {
            this.mode = mode;
        }
        public Serialization(int l0_, int l1_, int l2_, int l3_, int l4_, int l5_)
        {
            this.l0 = l0_;
            this.l1 = l1_;
            this.l2 = l2_;
            this.l3 = l3_;
            this.l4 = l4_;
            this.l5 = l5_;
        }
    }

     [Serializable]
     public class IsMaster
     {
         public string MPassword { get; set; }
     }
}
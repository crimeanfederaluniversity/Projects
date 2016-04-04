using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDM.edm
{
    public class LogHandler
    {
        
        public void AddInfo(string message)
        {
            using (EDMdbDataContext dc = new EDMdbDataContext())
            {
                Log log = new Log
                {
                    active = true,
                    type = "info",
                    fk_user = (int)HttpContext.Current.Session["userID"],
                    userip = HttpContext.Current.Request.UserHostAddress,
                    page = HttpContext.Current.Request.Url.AbsolutePath,
                    date = DateTime.Now,
                    message = message
                };
                dc.Log.InsertOnSubmit(log);
                dc.SubmitChanges();
            }
        }

        public void AddError(string message)
        {
            using (EDMdbDataContext dc = new EDMdbDataContext())
            {
                Log log = new Log
                {
                    active = true,
                    type = "error",
                    fk_user = (int)HttpContext.Current.Session["userID"],
                    userip = HttpContext.Current.Request.UserHostAddress,
                    page = HttpContext.Current.Request.Url.AbsolutePath,
                    date = DateTime.Now,
                    message = message
                };


                dc.Log.InsertOnSubmit(log);
                dc.SubmitChanges();
            }
        }

        public void AddCustom(string type, int user, string ip, string page, string message)
        {
            using (EDMdbDataContext dc = new EDMdbDataContext())
            {
                Log log = new Log();

                log.active = true;
                log.type = type;
                log.fk_user = user;
                log.userip = ip;
                log.page = page;
                log.date = DateTime.Now;
                log.message = message;

                dc.Log.InsertOnSubmit(log);
                dc.SubmitChanges();
            }
        }
    }
}
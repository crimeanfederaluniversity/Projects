using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    public class CommonCode
    {
        public static String GetUserById(int UserID)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            if (UserID == null)
                return "0";
            UsersTable User = (from a in kpiWebDataContext.UsersTable
                               where a.UsersTableID == UserID
                               select a).FirstOrDefault();
            if (User == null)
                return "0";
            if (User.Position == null)
                return User.Email;
            if (User.Position.Length > 2)
                return User.Position;
            return User.Email;
        }
    }
}
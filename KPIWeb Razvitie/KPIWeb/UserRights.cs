using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Asn1.Mozilla;

namespace KPIWeb
{
    public class UserRights
    {
        public bool CanUserSeeThisPage(int userId, int pageGroupId1, int pageGroupId2,int pageGroupId3)
        {
            KPIWebDataContext kpiWeb = new KPIWebDataContext();
            UsersAndUserGroupMappingTable userRight = (from a in kpiWeb.UsersAndUserGroupMappingTable
                                                       where (a.FK_GroupTable == pageGroupId1
                                                       || a.FK_GroupTable == pageGroupId2
                                                       || a.FK_GroupTable == pageGroupId3)
                      && a.FK_UserTable == userId
                      && a.Active == true
                select a).FirstOrDefault();
            if (userRight == null)
                return false;
            return true;
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalPages;

namespace KPIWeb
{
    public partial class _Default : Page
    {
        UsersTable user;
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
                UsersTable user1 =
                (from a in KPIWebDataContext.UsersTable where a.UsersTableID == userIdFromGet select a).FirstOrDefault();
                if (user1 != null)
                    FormsAuthentication.SetAuthCookie(user1.Email, true);
            }
            // автологин
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
              
               Response.Redirect("http://preview2.cfu-portal.ru");
            }
         
                UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                                   where usersTables.UsersTableID == UserSer.Id
                                   select usersTables).FirstOrDefault();
      
            if (user != null)
            {
                List<UsersAndUserGroupMappingTable> admin = (from a in KPIWebDataContext.UsersAndUserGroupMappingTable
                                                             where a.FK_GroupTable == 19 && a.FK_UserTable == user.UsersTableID && a.Active == true && a.Confirmed == true 
                                                             select a).ToList();
                Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("http://preview2.cfu-portal.ru");
            }
        }
    }
}
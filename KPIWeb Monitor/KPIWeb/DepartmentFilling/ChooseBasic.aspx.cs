using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.DepartmentFilling
{
    public partial class ChooseBasic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 9, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                //мы знаем в каком отчете
            }
        }
        protected void ButtonEditClick(object sender, EventArgs e)
        {
            
        }
    }
}
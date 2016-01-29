using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PersonalPages;

namespace KPIWeb.Rector
{
    public partial class RectorMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
                UsersTable user =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userIdFromGet select a).FirstOrDefault();
                if (user != null)
                    FormsAuthentication.SetAuthCookie(user.Email, true);
            }

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;
            
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            var login =
                     (from a in kPiDataContext.UsersTable
                      where a.UsersTableID == (int)ViewState["LocalUserID"]
                      select a.Email).FirstOrDefault();

            ViewState["login"] = login;

            if (!IsPostBack)
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RM0: Prorector " + (string)ViewState["login"] + " moved to page (RectorMain)");


            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 6, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            ParametrType paramType = (ParametrType)Session["paramType"];
            if (paramType == null)
            {
                GoForwardButton.Enabled=false;
            }           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ParametrType paramType = new ParametrType(0);
            Session["paramType"] = paramType;
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RM1: Prorector " + (string)ViewState["login"] + " vibral raboty s Celevimi Pokazatelyami i pereshel na stranicy vibora otcheta");
            Response.Redirect("~/Rector/RectorChooseReport.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            ParametrType paramType = new ParametrType(1);
            Session["paramType"] = paramType;
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RM1: Prorector " + (string)ViewState["login"] + " vibral raboty s Celevimi Pokazatelyami i pereshel na stranicy vibora otcheta");
            Response.Redirect("~/Rector/RectorChooseReport.aspx");
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
        }
        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorChooseReport.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RM11: Prorector " + (string)ViewState["login"] + " pereshel k samostoyatelnomu zapolneniyu otcheta");
            Response.Redirect("~/ProrectorReportFilling/ChooseReport.aspx");
        }
    }
}
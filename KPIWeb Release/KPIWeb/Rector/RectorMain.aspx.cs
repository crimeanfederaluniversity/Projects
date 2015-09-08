using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class RectorMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            var login =
                     (from a in kPiDataContext.UsersTable
                      where a.UsersTableID == (int)ViewState["LocalUserID"]
                      select a.Email).FirstOrDefault();

            ViewState["login"] = login;

            if (!IsPostBack)
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RM0: Prorector " + (string)ViewState["login"] + " moved to page (RectorMain)");


            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
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
            LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RM2: Prorector " + (string)ViewState["login"] + " vibral raboty s Pervichnimi dannimy i pereshel na stranicy vibora otcheta");
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
    }
}
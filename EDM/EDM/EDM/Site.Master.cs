using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDM.edm;
using Microsoft.AspNet.Identity;

namespace EDM
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userSer = Session["userID"];

            if (userSer != null)
            {
                int userID;
                int.TryParse(Session["userID"].ToString(), out userID);

                ProcessMainFucntions main = new ProcessMainFucntions();
                GoToStarterPageButton.Visible = main.IsUserStarter(userID);
                GoToSubmitterButton.Visible = main.IsUserSubmitter(userID);
                GoToTemplatesButton.Visible = main.CanUserDoTemplate(userID);
                GoToSlavesHistory.Visible = main.IsUserHead(userID);
                top_panel2.Visible = true;
            }
            else
            {
                top_panel2.Visible = false;
            }
        }

        protected void goBackButton_Click(object sender, EventArgs e)
        {

        }


        protected void GoToStarterPageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edm/ProcessStarterPage.aspx");
        }
        protected void GoToTemplatesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edm/TemplatesList.aspx");
        }

        protected void GoToSlavesHistory_Click(object sender, EventArgs e)
        {
            Session["searchName"] = string.Empty;
            Session["dateStartSearch"] = string.Empty;
            Response.Redirect("~/edm/Subordinate.aspx");
        }

        protected void GoToSubmitterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edm/SubmittedPage.aspx");
        }
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
        protected void LinkButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/ResetPassword.aspx");
        }

        protected void md5CheckButton_OnClick(object sender, EventArgs e)
        {
            OtherFuncs of = new OtherFuncs();
            of.Md5Check(this.Page, FileUpload1);
        }

    }

}
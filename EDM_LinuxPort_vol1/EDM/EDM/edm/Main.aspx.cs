using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session["userID"];
            if (userId == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////////

            if (!Page.IsPostBack)
            {
                EDMdbDataContext dataContext = new EDMdbDataContext();

                var userIsNewParticipant =
                    (from a in dataContext.Participants where a.active && a.fk_user == (int)userId && a.isNew==true select a.fk_process)
                        .Distinct().ToList();

                var newProcessCount = (from p in userIsNewParticipant
                                      join proc in dataContext.Processes on p equals proc.processID
                                      where proc.active && proc.status == 0
                                      select proc).Count();
                if (newProcessCount > 0)
                {
                    Button2.Text += " ("+ newProcessCount+ ")";
                } 
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["direction"] = 1;
            Response.Redirect("Dashboard.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["direction"] = 0;
            Response.Redirect("Dashboard.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["direction"] = 2;
            Response.Redirect("Dashboard.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["processID"] = 0;
            Response.Redirect("ProcessEdit.aspx");
        }
    }
}
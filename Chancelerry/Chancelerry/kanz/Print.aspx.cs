using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            CardCreateView cardCreateView = new CardCreateView();
            var registerIdSes = Session["registerID"];
            var versionSes = Session["version"];
            var cardIdSes = Session["cardID"];
            if (registerIdSes != null && versionSes != null && cardIdSes != null)
            {
                PrintMainDiv.Controls.Add(cardCreateView.GetPrintVersion((int)registerIdSes, (int)cardIdSes, (int)versionSes));
            }
            
        }
    }
}
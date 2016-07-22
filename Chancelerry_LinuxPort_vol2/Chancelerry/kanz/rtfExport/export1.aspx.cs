using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Chancelerry.kanz.rtfExport
{
    public partial class export1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            rtfExporter rtf = new rtfExporter();
            string cardIdStr = Request.QueryString["card"];
            string instIdStr = Request.QueryString["inst"];
            string registerIdStr = Request.QueryString["reg"];
            int cardId = 0;
            int instance = 0;
            int regId = 0;
            Int32.TryParse(cardIdStr, out cardId);
            Int32.TryParse(instIdStr, out instance);
            Int32.TryParse(registerIdStr, out regId);
            string st;
            if (regId == 3)
            {
                st = rtf.Export(Server.MapPath("export1.rtf"), Server.MapPath("tmp/"), cardId, userId, 1, instance);
            }
            else
            {
                st =  rtf.Export(Server.MapPath("export2.rtf"),  Server.MapPath("tmp/"), cardId, userId, 2, instance);
            }
            Label1.Text = st.ToString();
        }
    }
}
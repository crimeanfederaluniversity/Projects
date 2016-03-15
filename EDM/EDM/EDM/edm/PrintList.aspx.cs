using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class PrintList : System.Web.UI.Page
    {
        EDMdbDataContext _edmDb = new EDMdbDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            HttpContext.Current.Session["processID"].ToString();

            

           
        }
    }
}
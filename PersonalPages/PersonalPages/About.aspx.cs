using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            List<ManualTable> man = (from a in PersonalPagesDB.ManualTable where a.Active == true select a).ToList();

            LinksLable.Text = "";
            foreach (ManualTable link in man)
            {
                LinksLable.Text += "<a href=\"" + ConfigurationManager.AppSettings.Get("SiteName") + "/manuals/" + link.ManualLink + "\" target=\"_blank\"  >" + link.ManualName + "</a> </br>";
            }

        }
    }
}
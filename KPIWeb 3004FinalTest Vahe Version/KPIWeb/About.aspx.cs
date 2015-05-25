using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<ManualTable> man = (from a in kPiDataContext.ManualTable where a.Active == true select a).ToList();
                 
                LinksLable.Text = "";
                foreach(ManualTable link in man)
                {
                    LinksLable.Text += "<a href=\"../manuals/" + link.ManualLink + "\">" + link.ManualName + "</a> </br>";
                }

              
            }
        }
         
}
}
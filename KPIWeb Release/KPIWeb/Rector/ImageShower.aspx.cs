using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector.NewInt
{
    public partial class ImageShower : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string path = Request.QueryString["url"];
            mainImage.ImageUrl = path;

        }
    }
}
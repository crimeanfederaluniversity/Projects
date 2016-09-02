﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class OMRMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
                Response.Redirect("~/Forms/UserMainPage.aspx");
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/OMRAcceptArticlePage.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/OMRUserRatingPage.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/FormCreateEditByUser.aspx");
        }
    }
}
﻿using System;
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

            int regId;
            int vers;
            int cardId;

            int.TryParse(Session["registerID"].ToString(), out regId);
            int.TryParse(Session["version"].ToString(), out vers);
            int.TryParse(Session["cardID"].ToString(), out cardId);

            CardCreateView cardCreateView = new CardCreateView();
            var registerIdSes = regId;
            var versionSes = vers;
            var cardIdSes = cardId;

           PrintMainDiv.Controls.Add(cardCreateView.GetPrintVersion((int)registerIdSes, (int)cardIdSes, (int)versionSes));
         
            
        }
    }
}
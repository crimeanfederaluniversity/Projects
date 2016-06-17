﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Rank
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userSer = Session["userID"];

            if (userSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
            RankDBDataContext ratingDB = new RankDBDataContext();
            var user = (from u in ratingDB.UsersTable
                        where u.UsersTableID == (int)userSer
                        select u).FirstOrDefault();

            if (user != null)
            {
                Response.Redirect("~/Forms/UserMainPage.aspx");
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}
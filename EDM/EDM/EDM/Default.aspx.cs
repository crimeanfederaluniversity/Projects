﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDM.Models;

namespace EDM
{
    public partial class _Default : Page
    {
        public void Directions(Users user)
        {
            FormsAuthentication.SetAuthCookie(user.name, true);
            Response.Redirect("~/edm/Main.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var userSer = Session["userID"];

            if (userSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }

            EDMdbDataContext dataContext = new EDMdbDataContext();
            var user = (from u in dataContext.Users
                        where u.userID == (int)userSer
                        select u).FirstOrDefault();

            if (user != null)
            {
                Directions(user);
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
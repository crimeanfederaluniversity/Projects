﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class _1CForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            string question = TextBox2.Text.ToString();
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplications newquestion = new Aplications();
            newquestion.Active = true;
            newquestion.FK_ApplicationType = 7;
            newquestion.FK_UserAdd = userID;
            newquestion.Date = DateTime.Now;
            newquestion.TelephoneNumber = TextBox1.Text;
            newquestion.Text = question;
            usersDB.Aplications.InsertOnSubmit(newquestion);
            usersDB.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваш запрос успешно отправлен!');", true);
            Response.Redirect("~/UserMainPage.aspx");
        }
    }
}
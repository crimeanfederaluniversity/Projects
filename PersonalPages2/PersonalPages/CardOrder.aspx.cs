﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class CardOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            if (!Page.IsPostBack)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications aplication = (from a in usersDB.Aplications where a.Active == true && a.FK_ApplicationType == 4 select a).FirstOrDefault();
                if (aplication.Confirmed == 0)
                {
                    Label1.Visible = true;
                    Label1.Text = "Ваша заявка находится на рассмотрении";
                    Button1.Text = "Подать новую заявку";
                }
                if (aplication.Confirmed == 1)
                {
                    Label1.Visible = true;
                    Label1.Text = "Ваша заявка отклонена";
                    Button1.Text = "Подать новую заявку";
                }
                if (aplication.Confirmed == 2)
                {
                    Label1.Visible = true;
                    Button1.Text = "Подать новую заявку";
                    Label1.Text = "Ваша заявка принята";
                }
            }
            }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            if (FileUpload1.HasFile) 
            {
                if (TextBox5.Text != null && TextBox4.Text != null)
                {
                    PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                    Aplications neworder = new Aplications();
                    neworder.Active = true;
                    neworder.FK_ApplicationType = 4;
                    neworder.FK_UserAdd = userID;
                    neworder.Date = DateTime.Now;
                    neworder.TelephoneNumber = TextBox5.Text;
                    neworder.Text = TextBox4.Text.ToString();
                    neworder.Confirmed = 0;
                    String path = Server.MapPath("http://cfu-portal.ru/AplicationFiles");
                    Directory.CreateDirectory(path + "\\\\" + userID.ToString());
                    FileUpload1.PostedFile.SaveAs(path + "\\\\" + userID.ToString() + "\\\\" + FileUpload1.FileName);
                    neworder.FileURL = "/AplicationFiles" + "\\\\" + userID.ToString() + "\\\\" + FileUpload1.FileName;
                    usersDB.Aplications.InsertOnSubmit(neworder);
                    usersDB.SubmitChanges();
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Запрос отправлен!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Не все поля заполнены!');", true);
                }
                       
            }         
        }
       
    }
}
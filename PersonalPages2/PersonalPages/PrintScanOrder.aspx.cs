using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class PrintScanOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            if (!Page.IsPostBack)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications aplication = (from a in usersDB.Aplications where a.Active == true && a.FK_ApplicationType == 5 select a).FirstOrDefault();
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
            if (TextBox5.Text != null && TextBox4.Text != null && TextBox6.Text != null)
            {
                string print = "Количество копий:" + TextBox4.Text.ToString() + " " + "Формат печати:" + TextBox5.Text.ToString() + " " + "Cтраницы для печати:" + TextBox6.Text.ToString();
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications newprint = new Aplications();
                newprint.Active = true;
                newprint.FK_ApplicationType = 5;
                newprint.FK_UserAdd = userID;
                newprint.Date = DateTime.Now;
                newprint.Text = print;
                newprint.Confirmed = 0;
                String path = Server.MapPath("~/AplicationFiles");
                Directory.CreateDirectory(path + "\\\\" + userID.ToString());
                FileUpload1.PostedFile.SaveAs(path + "\\\\" + userID.ToString() + "\\\\" + FileUpload1.FileName);
                newprint.FileURL = "~/AplicationFiles" + "\\\\" + userID.ToString() + "\\\\" + FileUpload1.FileName;
                usersDB.Aplications.InsertOnSubmit(newprint);
                usersDB.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка на печать отправлена!');", true);

            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Не все поля заполнены!');", true);
            }
            }
        }
    }

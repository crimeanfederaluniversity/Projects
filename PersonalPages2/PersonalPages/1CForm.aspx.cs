using System;
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
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplications aplication = (from a in usersDB.Aplications where a.Active == true select a).FirstOrDefault();
            if (aplication.Confirmed == 0)
            {
                Label1.Visible = true;
                Label1.Text="Ваша заявка находится на рассмотрении";
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
                Label1.Text = "Ваша заявка выполнена";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (TextBox1.Text != null)
            {
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
                newquestion.Confirmed = 0;
                usersDB.Aplications.InsertOnSubmit(newquestion);
                usersDB.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваш запрос успешно отправлен!');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Не все поля заполнены!');", true);
            }
        }
    }
}
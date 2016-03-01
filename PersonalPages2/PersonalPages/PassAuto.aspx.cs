using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class PassAuto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            if (!Page.IsPostBack)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplication aplication = (from a in usersDB.Aplications where a.Active == true && a.FK_ApplicationType == 3 select a).FirstOrDefault();
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

            if (TextBox3.Text != null)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplication newpass = new Aplication();
                newpass.Active = true;
                newpass.FK_ApplicationType = 3;
                newpass.FK_UserAdd = userID;
                newpass.Date = DateTime.Now;
                newpass.TelephoneNumber = TextBox3.Text;
                newpass.Text = TextBox4.Text.ToString();
                newpass.Confirmed = 0;
                usersDB.Aplications.InsertOnSubmit(newpass);
                usersDB.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваш запрос отправлен!');", true);
            }
            else
            {
                Response.Redirect("~/MasterServises/AdminServices.aspx");
            }
        }
    }
}
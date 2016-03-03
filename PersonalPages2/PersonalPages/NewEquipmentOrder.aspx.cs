using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class NewEquipmentOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            if (!Page.IsPostBack)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications aplication = (from a in usersDB.Aplications where a.Active == true && a.FK_ApplicationType == 8 select a).FirstOrDefault();
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
            if (TextBox5.Text != null && TextBox4.Text != null && TextBox6.Text != null && TextBox7.Text != null)
            {
                string order = "Необходимая техника или расходные материалы:" + TextBox4.Text.ToString() + " " + "Цель использования:" + TextBox5.Text.ToString() + " " + "Кто будет использовать:" + TextBox6.Text.ToString() + " " + "Получатель (ответственный):" + TextBox7.Text.ToString();
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications newequipment = new Aplications();
                newequipment.Active = true;
                newequipment.FK_ApplicationType = 8;
                newequipment.FK_UserAdd = userID;
                newequipment.Date = DateTime.Now;
                newequipment.Text = order;
                newequipment.Confirmed = 0;
                usersDB.Aplications.InsertOnSubmit(newequipment);
                usersDB.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка отправлена!');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Не все поля заполнены!');", true);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class EquipmentWriteOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            if (!Page.IsPostBack)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications aplication = (from a in usersDB.Aplications where a.Active == true && a.FK_ApplicationType == 9 select a).FirstOrDefault();
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
            string order = "Название оборудования:" + TextBox4.Text.ToString() + " " + "Инвентарный номер:" + TextBox5.Text.ToString() + " " +
            "Ответственный:" + " " + TextBox6.Text.ToString() +   "Причина списания:" + TextBox7.Text.ToString();
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplications equipment = new Aplications();
            equipment.Active = true;
            equipment.FK_ApplicationType = 9;
            equipment.FK_UserAdd = userID;
            equipment.Date = DateTime.Now;
            equipment.Text = order;
            equipment.Confirmed = 0;
            usersDB.Aplications.InsertOnSubmit(equipment);
            usersDB.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваша заявка принята на рассмотрение!');", true);
        }
        else
    {
        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Не все поля заполнены!');", true);
    }
        }
    }
}
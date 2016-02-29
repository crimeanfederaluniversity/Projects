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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            string order = "Название оборудования:" + TextBox4.Text.ToString() + " " + "Инвентарный номер:" + TextBox5.Text.ToString() + " " +
            "Ответственный:" + " " + TextBox6.Text.ToString() +   "Причина списания:" + TextBox7.Text.ToString();
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplication equipment = new Aplication();
            equipment.Active = true;
            equipment.FK_ApplicationType = 9;
            equipment.FK_UserAdd = userID;
            equipment.Date = DateTime.Now;
            equipment.Text = order;
            equipment.Confirmed = 0;
            usersDB.Aplications.InsertOnSubmit(equipment);
            usersDB.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваша заявка на списание оборудования принята на рассмотрение!');", true);
            Response.Redirect("~/UserMainPage.aspx");
        }
    }
}
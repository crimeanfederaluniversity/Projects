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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            string order = TextBox4.Text.ToString() + "/" + TextBox5.Text.ToString() + "/" + TextBox6.Text.ToString() + "/" + TextBox7.Text.ToString();
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplications newequipment = new Aplications();
            newequipment.Active = true;
            newequipment.FK_ApplicationType = 8;
            newequipment.FK_UserAdd = userID;
            newequipment.Date = DateTime.Now;
            newequipment.Text = order;
            usersDB.Aplications.InsertOnSubmit(newequipment);
            usersDB.SubmitChanges();
        }
    }
}
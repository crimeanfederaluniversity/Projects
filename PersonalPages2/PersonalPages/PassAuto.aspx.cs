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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplications newpass = new Aplications();
            newpass.Active = true;
            newpass.FK_ApplicationType = 3;
            newpass.FK_UserAdd = userID;
            newpass.Date = DateTime.Now;
            newpass.TelephoneNumber = TextBox3.Text; 
            newpass.Text = TextBox4.Text.ToString();

            usersDB.Aplications.InsertOnSubmit(newpass);
            usersDB.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваш запрос на карту успешно отправлен!');", true);
            Response.Redirect("~/MasterServises/AdminServices.aspx");
        }
    }
}
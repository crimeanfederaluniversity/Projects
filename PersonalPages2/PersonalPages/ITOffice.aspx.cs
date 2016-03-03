using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class ITOffice : System.Web.UI.Page
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
            if (TextBox1.Text != null)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications newquestion = new Aplications();
                newquestion.Active = true;
                newquestion.FK_ApplicationType = 6;
                newquestion.FK_UserAdd = userID;
                newquestion.Date = DateTime.Now;
                newquestion.Text = TextBox1.Text;
                newquestion.Confirmed = 0;
                newquestion.TelephoneNumber = TextBox3.Text;

                usersDB.Aplications.InsertOnSubmit(newquestion);
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
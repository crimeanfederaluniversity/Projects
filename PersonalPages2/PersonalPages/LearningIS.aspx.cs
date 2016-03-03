using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class LearningIS : System.Web.UI.Page
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
                Aplications newlerning = new Aplications();
                newlerning.Active = true;
                newlerning.FK_ApplicationType = 11;
                newlerning.FK_UserAdd = userID;
                newlerning.Date = DateTime.Now;
                newlerning.Text = TextBox1.Text;
                newlerning.Confirmed = 0;
                newlerning.TelephoneNumber = TextBox3.Text;

                usersDB.Aplications.InsertOnSubmit(newlerning);
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
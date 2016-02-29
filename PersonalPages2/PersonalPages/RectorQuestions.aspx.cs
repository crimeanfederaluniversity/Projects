using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace PersonalPages
{
    public partial class RectorQustions : System.Web.UI.Page
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
            if (TextBox1.Text != null && TextBox2.Text != null)
            {
                string question = "Oбращение к ректору:" + TextBox1.Text.ToString() + " " + "Pуководители, к которым обращались по данному вопросу:" + TextBox2.Text.ToString();
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications newquestion = new Aplications();
                newquestion.Active = true;
                newquestion.FK_ApplicationType = 2;
                newquestion.FK_UserAdd = userID;
                newquestion.Date = DateTime.Now;
                newquestion.TelephoneNumber = TextBox3.Text;
                newquestion.Text = question;
                newquestion.Confirmed = 0;
                usersDB.Aplications.InsertOnSubmit(newquestion);
                usersDB.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ваш вопрос отправлен на рассмотрение!');", true);
            }
            else
            {
                Response.Redirect("~/MasterServises/AdminServices.aspx");
            }
          
        }
    }
}
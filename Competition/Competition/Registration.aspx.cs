using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDBDataContext newuser = new CompetitionDBDataContext();
            Users userreg = new Users();
            userreg.Active = true;
            userreg.Role = 0;
            userreg.Name = TextBox1.Text;
            userreg.Post = TextBox2.Text;
            userreg.PlaceofWork = TextBox3.Text;
            userreg.E_mail = TextBox4.Text;
            userreg.Pass = TextBox5.Text;

            newuser.Users.InsertOnSubmit(userreg);
            newuser.SubmitChanges();
            Session["ID_User"] = userreg.ID_User;
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Вы успешно зарегистрированы!');", true);
            Response.Redirect("~/Userpage.aspx");
                
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class PrintScanOrder : System.Web.UI.Page
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
            if (FileUpload1.HasFile)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications newprint = new Aplications();
                newprint.Active = true;
                newprint.FK_ApplicationType = 4;
                newprint.FK_UserAdd = userID;
                newprint.Date = DateTime.Now;
                newprint.Text = TextBox4.Text.ToString();
                usersDB.Aplications.InsertOnSubmit(newprint);
                usersDB.SubmitChanges();
                Response.Redirect("PrintScanOrder.aspx");
            }
        }
    }
}
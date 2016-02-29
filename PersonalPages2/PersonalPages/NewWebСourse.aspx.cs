using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class NewWebСourse : System.Web.UI.Page
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
            string print = "Название курса:" + TextBox1.Text.ToString() + " " + "Руководитель курса:" + TextBox2.Text.ToString() + " " + "Преподаватель курса:" + TextBox4.Text.ToString();
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Aplications newcourse = new Aplications();
            newcourse.Active = true;
            newcourse.FK_ApplicationType = 10;
            newcourse.FK_UserAdd = userID;
            newcourse.Date = DateTime.Now;
            newcourse.Text = print;
            newcourse.Confirmed = 0;
            newcourse.TelephoneNumber = TextBox3.Text;
            String path = Server.MapPath("~/AplicationFiles");
            Directory.CreateDirectory(path + "\\\\" + userID.ToString());
            FileUpload1.PostedFile.SaveAs(path + "\\\\" + userID.ToString() + "\\\\" + FileUpload1.FileName);
            newcourse.FileURL = "~/AplicationFiles" + "\\\\" + userID.ToString() + "\\\\" + FileUpload1.FileName;
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка на создание электронного курса отправлена!');", true);
            usersDB.Aplications.InsertOnSubmit(newcourse);
            usersDB.SubmitChanges();
            Response.Redirect("~/UserMainPage.aspx");

        }
    }
}
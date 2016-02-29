using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class CardOrder : System.Web.UI.Page
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
            Aplication neworder = new Aplication();
            neworder.Active = true;
            neworder.FK_ApplicationType = 4;
            neworder.FK_UserAdd = userID;
            neworder.Date = DateTime.Now;
            neworder.TelephoneNumber = TextBox5.Text;
            neworder.Text = TextBox4.Text.ToString();
            neworder.Confirmed = 0;
            String path = Server.MapPath("http://cfu-portal.ru/AplicationFiles"); 
            Directory.CreateDirectory(path + "\\\\" + userID.ToString());
            FileUpload1.PostedFile.SaveAs(path +"\\\\" + userID.ToString()  + "\\\\" + FileUpload1.FileName);        
            neworder.FileURL = "/AplicationFiles" + "\\\\" +  userID.ToString()  + "\\\\" + FileUpload1.FileName;
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Запрос отправлен!');", true);
            usersDB.Aplications.InsertOnSubmit(neworder);
            usersDB.SubmitChanges();            
            Response.Redirect("~/CardOrder.aspx");
                
            }         
        }
       
    }
}
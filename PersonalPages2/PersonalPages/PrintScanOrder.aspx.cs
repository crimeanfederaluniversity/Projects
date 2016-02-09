using System;
using System.Collections.Generic;
using System.IO;
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
            string print = TextBox4.Text.ToString() + "/" + TextBox5.Text.ToString() + "/" + TextBox6.Text.ToString();
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications newprint = new Aplications();
                newprint.Active = true;
                newprint.FK_ApplicationType = 5;
                newprint.FK_UserAdd = userID;
                newprint.Date = DateTime.Now;
                newprint.Text = print;
              String path = Server.MapPath("~/AplicationFiles"); 
              Directory.CreateDirectory(path + "\\\\" + userID.ToString());
              FileUpload1.PostedFile.SaveAs(path +"\\\\" + userID.ToString()  + "\\\\" + FileUpload1.FileName);        
              newprint.FileURL = "~/AplicationFiles" + "\\\\" +  userID.ToString()  + "\\\\" + FileUpload1.FileName;
              Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка на печать отправлена!');", true);
                usersDB.Aplications.InsertOnSubmit(newprint);
                usersDB.SubmitChanges();
                Response.Redirect("PrintScanOrder.aspx");
            }
        }
    }

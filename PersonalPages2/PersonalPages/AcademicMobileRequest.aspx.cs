using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class AcademicMobileRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshGridView();
        }
        
        private void RefreshGridView()
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable userTable =
               (from a in PersonalPagesDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            StudentsTable studTable =
                (from a in PersonalPagesDB.StudentsTable where a.StudentsTableID == userID select a).FirstOrDefault();
                      

                if (userTable == null && studTable != null)
                {
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label6.Visible = true;
                Label10.Visible = true;
                Label7.Visible = false;
                TextBox7.Visible = true;
                TextBox8.Visible = true;
                TextBox9.Visible = true;
                TextBox10.Visible = true;
                TextBox13.Visible = true;
                TextBox11.Visible = false;
 
                    Label2.Text = studTable.Surname;
                    Label8.Text = studTable.Name;
                    Label9.Text = studTable.Patronimyc;
                  
                    }              
       
                if (userTable != null && studTable == null)
                {
                    Label2.Text = userTable.Surname;
                    Label8.Text = userTable.Name;
                    Label9.Text = userTable.Patronimyc;
                    Label3.Visible = false;
                    Label4.Visible = false;
                    Label5.Visible = false;
                    Label6.Visible = false;
                    Label10.Visible = false;
                    Label7.Visible = true;
                    TextBox7.Visible = false;
                    TextBox8.Visible = false;
                    TextBox9.Visible = false;
                    TextBox13.Visible = false;
                    TextBox10.Visible = false;
                    TextBox11.Visible = true;
     
                }

            }

  
        protected void AddRowButton_Click(object sender, EventArgs e)
        {

        }

        protected void SaveFIOButton_Click(object sender, EventArgs e)
        {

        }

        protected void DeleteButtonClick (object sender, EventArgs e)
        {

        }

      
    }
}
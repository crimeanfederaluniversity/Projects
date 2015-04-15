using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextBox1.Text = "";

            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["ID"] = userID;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ////////////////////////////////////////////////////////////////////////////
            if (!IsPostBack)
            {
                Label1.Text += userTable.Login;
                Label2.Text += userTable.Email;
                Label3.Text = (from zero in kPiDataContext.ZeroLevelSubdivisionTable
                    where zero.ZeroLevelSubdivisionTableID == userTable.FK_ZeroLevelSubdivisionTable
                    select zero.Name).FirstOrDefault();

                Label4.Text = (from b in kPiDataContext.FirstLevelSubdivisionTable
                    where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable
                    select b.Name).FirstOrDefault();

                Label5.Text = (from c in kPiDataContext.SecondLevelSubdivisionTable
                    where c.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable
                    select c.Name).FirstOrDefault();
                Label6.Text = (from d in kPiDataContext.ThirdLevelSubdivisionTable
                    where d.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable
                    select d.Name).FirstOrDefault();

                Label7.Text = (from f in kPiDataContext.FourthLevelSubdivisionTable
                    where f.FourthLevelSubdivisionTableID == userTable.FK_FourthLevelSubdivisionTable
                    select f.Name).FirstOrDefault();

                Label8.Text = (from g in kPiDataContext.FifthLevelSubdivisionTable
                    where g.FifthLevelSubdivisionTableID == userTable.FK_FifthLevelSubdivisionTable
                    select g.Name).FirstOrDefault();

                /*TextBox1.Text += "Ваш email " + userTable.Email + Environment.NewLine;
            TextBox1.Text += (from zero in kPiDataContext.ZeroLevelSubdivisionTable where zero.ZeroLevelSubdivisionTableID == userTable.FK_ZeroLevelSubdivisionTable select zero.Name).FirstOrDefault() + Environment.NewLine;
            TextBox1.Text += (from b in kPiDataContext.FirstLevelSubdivisionTable where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable select b.Name).FirstOrDefault() + Environment.NewLine; 
            TextBox1.Text += (from c in kPiDataContext.SecondLevelSubdivisionTable where c.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable select c.Name).FirstOrDefault() + Environment.NewLine; 
            TextBox1.Text += (from d in kPiDataContext.ThirdLevelSubdivisionTable where d.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable select d.Name).FirstOrDefault() + Environment.NewLine; 
            TextBox1.Text += (from f in kPiDataContext.FourthLevelSubdivisionTable where f.FourthLevelSubdivisionTableID == userTable.FK_FourthLevelSubdivisionTable select f.Name).FirstOrDefault() + Environment.NewLine; 
            TextBox1.Text += (from g in kPiDataContext.FifthLevelSubdivisionTable where g.FifthLevelSubdivisionTableID == userTable.FK_FifthLevelSubdivisionTable select g.Name).FirstOrDefault() + Environment.NewLine; */
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            Label9.Visible = true;
            Label10.Visible = true;
            Label11.Visible = true;

            TextBox1.Visible = true;
            TextBox2.Visible = true;
            TextBox3.Visible = true;

            Button2.Visible = true;

            SetFocus(Button2);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable user = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == (int)ViewState["ID"] select a).FirstOrDefault();

            if ( (user!= null) && (TextBox1.Text.Equals(user.Password)) && (TextBox2.Text.Any()) && (TextBox2.Text.Equals(TextBox3.Text)) )
            {
                user.Password = TextBox2.Text;
                kpiWebDataContext.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "alert('Пароль успешно изменен!');", true);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "alert('Произошла ошибка, проверьте правильность данных!');", true);
            }
        }


    }
}
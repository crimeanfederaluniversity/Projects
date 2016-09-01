using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Account
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        RankDBDataContext rating = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
        var  UserSer = Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = Convert.ToInt32(UserSer);  
            UsersTable userTable =
                (from a in rating.UsersTable where a.Active== true && a.UsersTableID == userID select a).FirstOrDefault();

           string first =  (from b in rating.FirstLevelSubdivisionTable
             where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable
             select b.Name).FirstOrDefault();
           string second = (from c in rating.SecondLevelSubdivisionTable
             where c.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable
             select c.Name).FirstOrDefault();
            string third = (from d in rating.ThirdLevelSubdivisionTable
                            where d.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable
                            select d.Name).FirstOrDefault();

            if (!IsPostBack)
            {
                Label1.Text += userTable.Surname.ToString() + userTable.Name.ToString() + userTable.Patronimyc.ToString();
                Label2.Text += userTable.Email;
                Label3.Text = first + " " + second + " " + third;

                Label4.Text += userTable.Position.ToString();

                Label5.Text += userTable.Stavka.ToString();
                Label6.Text += "Добавить поле в базу";
                Label7.Text += "Дабавить поле для ввода";
               

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
        
            UsersTable user = (from a in rating.UsersTable where a.UsersTableID == (int)ViewState["ID"] select a).FirstOrDefault();

            if ( (user!= null) && (TextBox1.Text.Equals(user.Password)) && (TextBox2.Text.Any()) && (TextBox2.Text.Equals(TextBox3.Text)) )
            {
                user.Password = TextBox2.Text;
                rating.SubmitChanges();
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
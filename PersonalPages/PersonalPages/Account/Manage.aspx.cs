using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using  System.Data;

namespace PersonalPages.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["ID"] = userID;

            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();

            UsersTable userTable =
                (from a in PersonalPagesDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            StudentsTable studTable =
                (from a in PersonalPagesDB.StudentsTable where a.StudentsTableID == userID select a).FirstOrDefault();

            if (userTable != null && studTable == null)
            {
                if (!IsPostBack)
                {
                    Label1.Text += userTable.Surname;
                    Label2.Text += userTable.Name;
                    Label3.Text += userTable.Patronimyc;
                    Label4.Text += userTable.Position;
                    Label5.Text += userTable.Email;
                    Label6.Text = (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                        where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable
                        select b.Name).FirstOrDefault();
                    if (userTable.FK_SecondLevelSubdivisionTable != null)
                    {
                        Label7.Text = (from c in PersonalPagesDB.SecondLevelSubdivisionTable
                            where c.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable
                            select c.Name).FirstOrDefault();
                    }
                    if (userTable.FK_ThirdLevelSubdivisionTable != null)
                    {
                        Label8.Text = (from d in PersonalPagesDB.ThirdLevelSubdivisionTable
                            where d.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable
                            select d.Name).FirstOrDefault();
                    }
                    if (!IsPostBack)
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                        dataTable.Columns.Add(new DataColumn("FIO", typeof (string)));

                        List<TypeOfWritingFIO> fio =
                            (from a in PersonalPagesDB.TypeOfWritingFIO
                                where a.FK_UserTableID == userID && a.Active == true
                                select a).ToList();

                        
                        GridView1.DataSource = fio;
                        GridView1.DataBind();
                    }

                }
            }
            if (userTable == null && studTable != null)
            {
                GridView1.Visible = false;
                SaveFIOButton.Visible = false;
                AddRowButton.Visible = false;
                Label1.Text += studTable.Surname;
                Label2.Text += studTable.Name;
                Label3.Text += studTable.Patronimyc;
                Label4.Text += studTable.Email;

                Label5.Text = (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                    where b.FirstLevelSubdivisionTableID == studTable.FK_FirstLevelSubdivision
                    select b.Name).FirstOrDefault();

                Label6.Text = (from c in PersonalPagesDB.SecondLevelSubdivisionTable
                    where c.SecondLevelSubdivisionTableID == studTable.FK_SecondLevelSubdivision
                    select c.Name).FirstOrDefault();
                Label7.Text += studTable.YearOfEnter;
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext())
                {
                    var check =
                    (from a in PersonalPagesDB.TypeOfWritingFIO
                     where
                         a.ID == Convert.ToInt32(button.CommandArgument)
                     select a)
                        .FirstOrDefault();

                    check.Active = false;

                    PersonalPagesDB.SubmitChanges();
                    Response.Redirect("Manage.aspx");

                }
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
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable user =
                (from a in PersonalPagesDB.UsersTable where a.UsersTableID == (int) ViewState["ID"] select a)
                    .FirstOrDefault();

            if ((user != null) && (TextBox1.Text.Equals(user.Password)) && (TextBox2.Text.Any()) &&
                (TextBox2.Text.Equals(TextBox3.Text)))
            {
                user.Password = TextBox2.Text;
                PersonalPagesDB.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                    "alert('Пароль успешно изменен!');", true);
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                    "alert('Произошла ошибка, проверьте правильность данных!');", true);
            }
        }

        protected void AddRowButton_Click(object sender, EventArgs e)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            TypeOfWritingFIO newRow = new TypeOfWritingFIO();
            newRow.Active = true;
            newRow.FK_UserTableID = userID;
            PersonalPagesDB.TypeOfWritingFIO.InsertOnSubmit(newRow);
            PersonalPagesDB.SubmitChanges();
            Response.Redirect("Manage.aspx");
        }

        protected void SaveFIOButton_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow currentRow in GridView1.Rows)
            {
                Label idLabel = (Label)currentRow.FindControl("LabelID");
                    TextBox FIOText = (TextBox) currentRow.FindControl("FIO");
                    if (idLabel != null)
                    {                   
                        PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                        TypeOfWritingFIO newfio =(from a in PersonalPagesDB.TypeOfWritingFIO
                            where a.ID == Convert.ToInt32(idLabel.Text) && a.Active == true
                            select a).FirstOrDefault();
                        if (newfio != null)
                        {
                            newfio.FIO = FIOText.Text;
                            PersonalPagesDB.SubmitChanges();
                        }
                        
                    }
            }
            Response.Redirect("Manage.aspx");
        }
    }
}
    
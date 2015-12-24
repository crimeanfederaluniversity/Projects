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
            if (userTable.Data_Status == false)
            {
                Label12.Text = "Учетные данные ожидают подтверждения";
            }
            if (userTable != null && studTable == null)
            {
                if (!IsPostBack)
                {
                    Text1.Text += userTable.Surname;
                    Text2.Text += userTable.Name;
                    Text3.Text += userTable.Patronimyc;
                    Text4.Text += userTable.Email;
                    DropDownList1.SelectedItem.Text = (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                        where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable
                        select b.Name).FirstOrDefault();
                    if (userTable.FK_SecondLevelSubdivisionTable != null)
                    {
                        DropDownList2.SelectedItem.Text = (from c in PersonalPagesDB.SecondLevelSubdivisionTable
                            where c.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable
                            select c.Name).FirstOrDefault();
                    }
                    if (userTable.FK_ThirdLevelSubdivisionTable != null)
                    {
                        DropDownList3.SelectedItem.Text = (from d in PersonalPagesDB.ThirdLevelSubdivisionTable
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
                Text1.Text = studTable.Surname;
                Text2.Text = studTable.Name;
                Text3.Text = studTable.Patronimyc;
                Text4.Text = studTable.Email;
                Text5.Text ="Академическая група" + (from a in PersonalPagesDB.GroupsTable where a.ID == studTable.FK_Group 
                                  && a.Active == true select a.Name).FirstOrDefault();
                DropDownList1.SelectedItem.Text = (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                    where b.FirstLevelSubdivisionTableID == studTable.FK_FirstLevelSubdivision
                    select b.Name).FirstOrDefault();
                DropDownList2.SelectedItem.Text = (from c in PersonalPagesDB.SecondLevelSubdivisionTable
                    where c.SecondLevelSubdivisionTableID == studTable.FK_SecondLevelSubdivision
                    select c.Name).FirstOrDefault();
                Text6.Text = "Год поступления" + studTable.YearOfEnter;
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
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();

            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            int SelectedValue = -1;

            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in PersonalPagesDB.SecondLevelSubdivisionTable
                                                                      where item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.Enabled = true;
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
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

        protected void Button3_Click(object sender, EventArgs e)
        {
             Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable newfio = (from a in PersonalPagesDB.UsersTable
                                       where a.UsersTableID == userID  && a.Active == true
                                       select a).FirstOrDefault();
            newfio.Data_Status = false;
            PersonalPagesDB.SubmitChanges(); 
        }
    }
}
    
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
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            ViewState["ID"] = userID;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable userTable = (from a in PersonalPagesDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (!IsPostBack)
            {    
              if (userTable != null)
                { // общие поля
                    Label14.Text = userTable.Email;
                    Label16.Text = userTable.Name;
                    Label15.Text = userTable.Surname;
                    Label17.Text = userTable.Patronimyc;
                    Name.Text = userTable.Name;
                    Surname.Text = userTable.Surname;
                    Patronimyc.Text = userTable.Patronimyc;
                    Email.Text = userTable.Email;
                    
                    if (userTable.Data_Status == false)
                    {
                        Label2.Visible = true;
                        Label2.Text = "Запрос на изменение учетных данных отправлен администратору системы. Для утверждения данных, пожалуйста, предоставьте документы, подтверждающие изменения в кабинет 131 корпус А!";

                    }
                    if (userTable.Data_Status == true)
                    {
                        Label2.Visible = true;
                        Label2.Text = "Ваши учетные данные утверждены!";
                    }
                    List<FirstLevelSubdivisionTable> First_stageList = (from item in PersonalPagesDB.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                    List<SecondLevelSubdivisionTable> Second_stageList = (from item in PersonalPagesDB.SecondLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();

                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите академию");
                    var dictionary2 = new Dictionary<int, string>();
                    dictionary2.Add(-1, "Выберите факультет");
                    foreach (var item in First_stageList)
                    {
                        dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);
                        DropDownList1.DataTextField = "Value";
                        DropDownList1.DataValueField = "Key";
                        DropDownList1.DataSource = dictionary;
                        DropDownList1.DataBind();
                    }
                    foreach (var item in Second_stageList)
                    {
                        dictionary2.Add(item.SecondLevelSubdivisionTableID, item.Name);
                        DropDownList2.DataTextField = "Value";
                        DropDownList2.DataValueField = "Key";
                        DropDownList2.DataSource = dictionary2;
                        DropDownList2.DataBind();
                    }
                  // поля только для cотрудника
      if (userTable.AccessLevel != 11 && userTable.AccessLevel != 12 && userTable.AccessLevel != 13 && userTable.AccessLevel != 14)
                    { 
                        Label12.Text = "Должность";
                        Label13.Text = "Ученая степень";                     
                        Label18.Text = userTable.Position;
                        Label19.Text = userTable.AcademicDegree;
                        DegreeYear.Text = userTable.AcademicDegree;
                        PositionKurs.Text = userTable.Position;
                        if(userTable.FK_FirstLevelSubdivisionTable != null)
                        { Label20.Text = (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                                   where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable && b.Active == true
                                                   select b.Name).FirstOrDefault().ToString(); }
                        else  { Label20.Text = ""; }
                        if(userTable.FK_SecondLevelSubdivisionTable != null)
                        {  Label21.Text = (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                    where b.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable && b.Active == true
                                                    select b.Name).FirstOrDefault().ToString(); }  
                        else  {  Label21.Text = ""; }
                         if(userTable.FK_ThirdLevelSubdivisionTable != null)
                        { Label22.Text = (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                                                    where b.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable && b.Active == true
                                                    select b.Name).FirstOrDefault().ToString();  }  
                         else { Label22.Text = ""; }
                                   
                            List<ThirdLevelSubdivisionTable> Third_stageList = (from item in PersonalPagesDB.ThirdLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();

                            var dictionary3 = new Dictionary<int, string>();
                            dictionary3.Add(-1, "Выберите кафедру");

                            foreach (var item in Third_stageList)
                            {
                                dictionary3.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                                DropDownList3.DataTextField = "Value";
                                DropDownList3.DataValueField = "Key";
                                DropDownList3.DataSource = dictionary3;
                                DropDownList3.DataBind();
                            }                                                    
                            Label23.Visible = true;
                            AddRowButton.Visible = true;
                            SaveFIOButton.Visible = false;
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                            dataTable.Columns.Add(new DataColumn("FIO", typeof(string)));
                            List<TypeOfWritingFIO> fio = (from a in PersonalPagesDB.TypeOfWritingFIO
                                                          where a.FK_UserTableID == userID && a.Active == true
                                                          select a).ToList();
                            if (fio != null)
                            {
                                SaveFIOButton.Visible = true;
                                GridView1.DataSource = fio;
                                GridView1.DataBind();
                            }                          
                        }
        else // поля только для студента 
                    {
                        Label12.Text = "Курс";
                        Label13.Text = "Год поступления";
                        Label18.Text = userTable.Kurs.ToString(); ;
                        Label19.Text = userTable.YearEnter.ToString(); ;
                        DegreeYear.Text = userTable.AcademicDegree;
                        PositionKurs.Text = userTable.Position;        
                           
                            if (userTable.FK_FirstLevelSubdivisionTable != null)
                            {
                                Label20.Text = (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                                where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable && b.Active == true
                                                select b.Name).FirstOrDefault().ToString();
                            }
                            else { Label20.Text = ""; }
                            if (userTable.FK_SecondLevelSubdivisionTable != null)
                            {
                                Label21.Text = (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                where b.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable && b.Active == true
                                                select b.Name).FirstOrDefault().ToString();
                            }
                            else { Label21.Text = ""; }
                            if (userTable.FK_StudentGroup != null)
                            {
                               // Label22.Text = (from b in PersonalPagesDB.StudentGroupsTable  where b.ID == userTable.FK_StudentGroup && b.Active == true  select b.Name).FirstOrDefault().ToString();
                            }
                            else { Label22.Text = ""; }

                            List<StudentGroupsTable> Third_stageList = (from item in PersonalPagesDB.StudentGroupsTable select item).OrderBy(mc => mc.Name).ToList();

                            var dictionary3 = new Dictionary<int, string>();
                            dictionary3.Add(-1, "Выберите группу");

                            foreach (var item in Third_stageList)
                            {
                                dictionary3.Add(item.ID, item.Name);
                                DropDownList3.DataTextField = "Value";
                                DropDownList3.DataValueField = "Key";
                                DropDownList3.DataSource = dictionary3;
                                DropDownList3.DataBind();
                            }
                        }
                    }
                }
            }      
        protected void Button5_Click(object sender, EventArgs e)
        {
            Name.Visible = true;
            Surname.Visible = true;
            Patronimyc.Visible = true;
            Email.Visible = true;
            DegreeYear.Visible = true;
            PositionKurs.Visible = true;
            DegreeYear.Visible = true;
            PositionKurs.Visible = true;
            DropDownList1.Visible = true;
            DropDownList2.Visible = true;
            DropDownList3.Visible = true;
            SendChange.Visible = true;
            Label14.Visible = false;
            Label16.Visible = false;
            Label15.Visible = false;
            Label17.Visible = false;
            Label18.Visible = false;
            Label19.Visible = false;
            Label20.Visible = false;
            Label21.Visible = false;
            Label22.Visible = false;
            Button5.Enabled = false;
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

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();

            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();

            int SelectedValue = -1;

            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["ID"] = userID;
 
            UsersTable userTable =
                (from a in PersonalPagesDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable.AccessLevel != 11 && userTable.AccessLevel != 12 && userTable.AccessLevel != 13 && userTable.AccessLevel != 14)
            {

                List<ThirdLevelSubdivisionTable> third_stage = (from item in PersonalPagesDB.ThirdLevelSubdivisionTable
                                                                where item.FK_SecondLevelSubdivisionTable == SelectedValue
                                                                select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();
                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");

                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);

                    DropDownList3.Enabled = true;
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
            }
            else
            {
                List<StudentGroupsTable> group_stage = (from item in PersonalPagesDB.StudentGroupsTable
                                                                where item.FK_SecondLevel == SelectedValue
                                                                select item).OrderBy(mc => mc.ID).ToList();
                if (group_stage != null && group_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");

                    foreach (var item in group_stage)
                        dictionary.Add(item.ID, item.Name);

                    DropDownList3.Enabled = true;
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
            }           
          }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
          
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
                    Response.Redirect("~/Accont/Manage.aspx");

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
            Response.Redirect("~/Account/Manage.aspx");
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
            Response.Redirect("~/Account/Manage.aspx");
        }

        private void ChangeUserParam(UsersTable user,int paramIdToChange,string textMessage,string oldValue,string newValue)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UserDataChangeHistory UserHistory = new UserDataChangeHistory();
            UserHistory.Active = true;
            UserHistory.ChangeDate = DateTime.Now;
            UserHistory.FK_User = user.UsersTableID;
            UserHistory.ID_Param_ToChange = paramIdToChange;//Имя
            UserHistory.Name = textMessage;
            UserHistory.OldValue = oldValue;
            UserHistory.NewValue = newValue;
            UserHistory.Status = 0;
            PersonalPagesDB.UserDataChangeHistory.InsertOnSubmit(UserHistory);
            PersonalPagesDB.SubmitChanges();
        }

       protected void SendChange_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            int userID = UserSer.Id;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable user = (from a in PersonalPagesDB.UsersTable where a.Active == true && a.UsersTableID == userID select a).FirstOrDefault();
            if (Name.Text != user.Name)
            {
                if (!((Name.Text == "") && (user.Name == null)))
                    ChangeUserParam(user, 1, "Изменение имени", user.Name, Name.Text);
            }
            if (Surname.Text != user.Surname)
            {
                if (!((Surname.Text == "") && (user.Surname == null)))
                    ChangeUserParam(user, 2, "Изменение фамилии", user.Surname, Surname.Text);
            }
            if (Patronimyc.Text != user.Patronimyc)
            {
                if (!((Patronimyc.Text == "") && (user.Patronimyc == null)))
                    ChangeUserParam(user, 3, "Изменение отчества", user.Patronimyc, Patronimyc.Text);
            }
            if (Email.Text != user.Email)
            {
                if (!((Email.Text == "") && (user.Email == null)))
                    ChangeUserParam(user, 4, "Изменение почтового адреса", user.Email, Email.Text);
            }
            if (user.FK_FirstLevelSubdivisionTable != null)
                if (DropDownList1.SelectedItem.Text != (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                                        where b.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeUserParam(user, 5, "Изменение академии", (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                                                    where b.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                                                    select b.Name).FirstOrDefault(), DropDownList1.SelectedItem.Text);

                }
            if (user.FK_SecondLevelSubdivisionTable != null)
                if (DropDownList2.SelectedItem.Text != (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                        where b.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeUserParam(user, 6, "Изменение факультета", (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                                      where b.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                                                      select b.Name).FirstOrDefault(), DropDownList2.SelectedItem.Text);
                }
           // для сотрудников
            if (user.AccessLevel != 11 && user.AccessLevel != 12 && user.AccessLevel != 13 && user.AccessLevel != 14)
            {
                if (PositionKurs.Text != user.Position)
                {
                    if (!((PositionKurs.Text == "") && (user.Position == null)))
                        ChangeUserParam(user, 8, "Изменение должности", user.Name, Name.Text);
                }
                if (DegreeYear.Text != user.AcademicDegree)
                {
                    if (!((DegreeYear.Text == "") && (user.AcademicDegree == null)))
                        ChangeUserParam(user, 9, "Изменение научной степени", user.Name, Name.Text);
                }
               
                if (user.FK_ThirdLevelSubdivisionTable != null)
                if (DropDownList3.SelectedItem.Text != (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                                                        where b.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeUserParam(user, 7, "Изменение кафедры" , (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                                                                    where b.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                                                       select b.Name).FirstOrDefault() , DropDownList3.SelectedItem.Text); 
                }               
        
            }
            else  // для студентов
            {
                if (PositionKurs.Text != user.Kurs.ToString())
                {
                    if (!((PositionKurs.Text == "") && (user.Kurs == null)))
                        ChangeUserParam(user, 10, "Изменение курса", user.Name, Name.Text);
                }
                if (DegreeYear.Text != user.YearEnter.ToString())
                {
                    if (!((DegreeYear.Text == "") && (user.YearEnter == null)))
                        ChangeUserParam(user, 11, "Изменение года поступления", user.Name, Name.Text);
                }
                if (user.FK_StudentGroup != null)
                if (DropDownList3.SelectedItem.Text != (from b in PersonalPagesDB.StudentGroupsTable
                                                        where b.ID == user.FK_StudentGroup
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeUserParam(user, 7,
                        "Изменение группы студента" , (from b in PersonalPagesDB.StudentGroupsTable
                                                       where b.ID == user.FK_StudentGroup
                                                          select b.Name).FirstOrDefault() , DropDownList3.SelectedItem.Text);     
                }      
            }
            user.Data_Status = false;
            PersonalPagesDB.SubmitChanges();
            Response.Redirect("~/Account/Manage.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChangeAccess.aspx");
        }
  
    }
}
    
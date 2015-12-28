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
                    if (userTable.Confirmed == false)
                    {
                        Label2.Text = "Учетные данные ожидают утверждения";
                    }
                  
                    List<FirstLevelSubdivisionTable> First_stageList = (from item in PersonalPagesDB.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                    List<SecondLevelSubdivisionTable> Second_stageList = (from item in PersonalPagesDB.SecondLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                    List<ThirdLevelSubdivisionTable> Third_stageList = (from item in PersonalPagesDB.ThirdLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                    List<GroupsTable> Group_stageList = (from item in PersonalPagesDB.GroupsTable where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                    var dictionary = new Dictionary<int, string>();
                    var dictionary2 = new Dictionary<int, string>();
                    var dictionary3 = new Dictionary<int, string>();
                    
                    if (userTable!=null)
                    {
                        dictionary.Add(0, (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                           where b.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable && b.Active == true
                                           select b.Name).FirstOrDefault());
                        dictionary2.Add(0, (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                           where b.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable && b.Active == true
                                           select b.Name).FirstOrDefault());
                        dictionary3.Add(0, (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                                           where b.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable && b.Active == true
                                           select b.Name).FirstOrDefault());
                    }
                    if(studTable!=null)
                    {
                        dictionary.Add(0, (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                           where b.FirstLevelSubdivisionTableID == studTable.FK_FirstLevelSubdivision && b.Active == true
                                           select b.Name).FirstOrDefault());
                        dictionary2.Add(0, (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                            where b.SecondLevelSubdivisionTableID == studTable.FK_SecondLevelSubdivision && b.Active == true
                                            select b.Name).FirstOrDefault());
                        dictionary3.Add(0, (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                                            where b.ThirdLevelSubdivisionTableID == studTable.FK_Group && b.Active == true
                                            select b.Name).FirstOrDefault());
                    
                    }
                   
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
                        if (userTable != null)
                        {
                            foreach (var item in Third_stageList)
                            {
                                dictionary3.Add(item.ThirdLevelSubdivisionTableID, item.Name);

                                DropDownList3.DataTextField = "Value";
                                DropDownList3.DataValueField = "Key";
                                DropDownList3.DataSource = dictionary3;
                                DropDownList3.DataBind();
                            }

                        }
                        else if (studTable!=null)
                        {
                            foreach (var item in Group_stageList)
                            {
                                dictionary3.Add(item.ID, item.Name);
                                DropDownList3.DataTextField = "Value";
                                DropDownList3.DataValueField = "Key";
                                DropDownList3.DataSource = dictionary3;
                                DropDownList3.DataBind();
                            }                             
                        }
                    Text1.Text = userTable.Name;
                    Text2.Text = userTable.Surname;
                    Text3.Text = userTable.Patronimyc;
                    Text4.Text += userTable.Email;

                    if (userTable.FK_FirstLevelSubdivisionTable == null)
                    {
                        DropDownList1.SelectedItem.Text = "КФУ";
                    }
                    if (userTable.FK_SecondLevelSubdivisionTable == null)
                    {
                        DropDownList2.SelectedItem.Text = "";
                    }
                    if (userTable.FK_ThirdLevelSubdivisionTable == null)
                    {
                        DropDownList3.SelectedItem.Text =  "";
                    }
                    
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
            if (userTable == null && studTable != null)
            {
                if (studTable.Data_Status == false)
                {
                    Label2.Text = "Учетные данные ожидают утверждения";
                }
                GridView1.Visible = false;
                SaveFIOButton.Visible = false;
                AddRowButton.Visible = false;
                Text1.Text = studTable.Name;
                Text2.Text = studTable.Surname;
                Text3.Text = studTable.Patronimyc;
                Text4.Text = studTable.Email;
                if (userTable.FK_FirstLevelSubdivisionTable == null)
                {
                    DropDownList1.SelectedItem.Text = "КФУ";
                }
                if (userTable.FK_SecondLevelSubdivisionTable == null)
                {
                    DropDownList2.SelectedItem.Text = "Факультет не выбран";
                }
                if (userTable.FK_ThirdLevelSubdivisionTable == null)
                {
                    DropDownList3.SelectedItem.Text = "Группа не выбрана ";
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


        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();

            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();

            int SelectedValue = -1;

            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
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

        private void ChangeStudentParam(StudentsTable student, int paramIdToChange, string textMessage, string oldValue, string newValue)
        {
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            StudentChangeDataHistoryTable StudentHistory = new StudentChangeDataHistoryTable();
            StudentHistory.Active = true;
            StudentHistory.ChangeDate = DateTime.Now;
            StudentHistory.FK_StudentTable = student.StudentsTableID;
            StudentHistory.ID_Param_ToChange = paramIdToChange;
            StudentHistory.Name = textMessage;
            StudentHistory.OldValue = oldValue;
            StudentHistory.NewValue = newValue;
            StudentHistory.Status = 0;
            PersonalPagesDB.StudentChangeDataHistoryTable.InsertOnSubmit(StudentHistory);
            PersonalPagesDB.SubmitChanges();
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            int userID = UserSer.Id;
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable user = (from a in PersonalPagesDB.UsersTable where a.Active == true && a.UsersTableID == userID select a).FirstOrDefault();
            StudentsTable student =(from a in PersonalPagesDB.StudentsTable where a.Active == true && a.StudentsTableID == userID select a).FirstOrDefault();
            if (user != null)
            {                
                if (Text1.Text != user.Name)
                {
                    if (!((Text1.Text == "") && (user.Name == null)))
                    ChangeUserParam(user, 1, "Изменение имени" ,user.Name,Text1.Text);
                }
                if (Text2.Text != user.Surname)
                {
                    if (!((Text2.Text == "") && (user.Surname == null)))
                    ChangeUserParam(user, 2, "Изменение фамилии", user.Surname, Text2.Text);
                }
                if (Text3.Text != user.Patronimyc)
                {
                    if (!((Text3.Text == "") && (user.Patronimyc == null)))
                    ChangeUserParam(user, 3, "Изменение отчества",user.Patronimyc , Text3.Text);          
                }
                if (Text4.Text != user.Email)
                {
                    if (!((Text4.Text == "") && (user.Email == null)))
                    ChangeUserParam(user, 4, "Изменение почтового адреса", user.Email, Text4.Text);  
                }
                if (user.FK_FirstLevelSubdivisionTable!=null)
                if (DropDownList1.SelectedItem.Text != (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                        where b.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                        select b.Name).FirstOrDefault())
                {
                    ChangeUserParam(user, 5,"Изменение академии", (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                                                  where b.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                                                       select b.Name).FirstOrDefault(), DropDownList1.SelectedItem.Text);

                }
                if (user.FK_SecondLevelSubdivisionTable != null)
                if (DropDownList2.SelectedItem.Text != (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                        where b.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeUserParam(user, 6, "Изменение факультета",(from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                        where b.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                                                          select b.Name).FirstOrDefault() ,DropDownList2.SelectedItem.Text);
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
                user.Data_Status = false;               
            }
            else if (student != null) ////////Для студентов
            {        
                if (Text1.Text != student.Name)
                {
                    if (!((Text1.Text == "") && (student.Name == null)))
                    ChangeStudentParam(student, 1, "Изменение имени" , student.Name , Text1.Text);
                }
                if (Text2.Text != student.Surname)
                {
                    if (!((Text2.Text == "") && (student.Surname == null)))
                    ChangeStudentParam(student, 2, "Изменение фамилии" , student.Surname , Text2.Text);              
                }
                if (Text3.Text != student.Patronimyc)
                {
                    if (!((Text3.Text == "") && (student.Patronimyc == null)))
                    ChangeStudentParam(student, 3, "Изменение отчества" , student.Patronimyc , Text3.Text);         
                }
                if (Text4.Text != student.Email)
                {
                    if (!((Text4.Text == "") && (student.Email == null)))
                    ChangeStudentParam(student, 4, "Изменение почтового адреса" , student.Email , Text4.Text);   
                }
                if (student.FK_FirstLevelSubdivision != null)
                if (DropDownList1.SelectedItem.Text != (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                                                        where b.FirstLevelSubdivisionTableID == student.FK_FirstLevelSubdivision
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeStudentParam(student, 5,
                        "Изменение академии" , (from b in PersonalPagesDB.FirstLevelSubdivisionTable
                            where b.FirstLevelSubdivisionTableID == student.FK_SecondLevelSubdivision
                                                   select b.Name).FirstOrDefault(), DropDownList1.SelectedItem.Text);
                }
                if (student.FK_SecondLevelSubdivision != null)
                if (DropDownList2.SelectedItem.Text != (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                                                        where b.SecondLevelSubdivisionTableID == student.FK_SecondLevelSubdivision
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeStudentParam(student, 6,
                        "Изменение факультета", (from b in PersonalPagesDB.SecondLevelSubdivisionTable
                            where b.SecondLevelSubdivisionTableID == student.FK_SecondLevelSubdivision
                                                     select b.Name).FirstOrDefault(), DropDownList2.SelectedItem.Text);       
                }
                if (student.FK_Group != null)
                if (DropDownList3.SelectedItem.Text != (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                                                        where b.ThirdLevelSubdivisionTableID == student.FK_Group
                                                        select b.Name).FirstOrDefault())
                {
                    ChangeStudentParam(student, 7,
                        "Изменение группы студента" , (from b in PersonalPagesDB.ThirdLevelSubdivisionTable
                            where b.ThirdLevelSubdivisionTableID == student.FK_Group
                                                          select b.Name).FirstOrDefault() , DropDownList3.SelectedItem.Text);     
                }
                student.Data_Status = false;
            }   
            PersonalPagesDB.SubmitChanges();
        }
    }
}
    
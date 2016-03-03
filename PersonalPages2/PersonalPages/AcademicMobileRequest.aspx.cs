using System;
using System.Collections.Generic;
using System.IO;
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
            if (!Page.IsPostBack)
            {
                PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                Aplications aplication = (from a in usersDB.Aplications where a.Active == true && a.FK_ApplicationType == 7 select a).FirstOrDefault();
                if (aplication.Confirmed == 0)
                {
                    Label11.Visible = true;
                    Label11.Text = "Ваша заявка находится на рассмотрении";
                    Button2.Text = "Подать новую заявку";
                }
                if (aplication.Confirmed == 1)
                {
                    Label11.Visible = true;
                    Label11.Text = "Ваша заявка отклонена";
                    Button2.Text = "Подать новую заявку";
                }
                if (aplication.Confirmed == 2)
                {
                    Label11.Visible = true;
                    Button2.Text = "Подать новую заявку";
                    Label11.Text = "Ваша заявка принята";
                }
                RefreshGridView();
            }            
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
                                           
                if (userTable != null )
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
 

        protected void Button2_Click(object sender, EventArgs e)
        {

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
    
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable userTable = (from a in PersonalPagesDB.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
         

            if (userTable != null )
            {
                if (FileUpload1.HasFile && FileUpload2.HasFile && FileUpload3.HasFile && FileUpload4.HasFile && FileUpload5.HasFile && FileUpload6.HasFile && FileUpload7.HasFile)
                {
                    if (userTable.AccessLevel == 11 || userTable.AccessLevel == 12 || userTable.AccessLevel == 13 || userTable.AccessLevel == 14)
                    {
                        string aspirant = "Название сети:" + TextBox3.Text.ToString() + "Срок академической мобильности c " + Calendar2.SelectedDate.ToString().Split(' ')[0] + " до " + Calendar3.SelectedDate.ToString().Split(' ')[0]
                     + " " + "Партнерская организация(в порядке приоритетности):" + TextBox14.Text.ToString() + " " + "Обоснование целей и мотивирование реализации академической мобильности:" + TextBox5.Text.ToString()
                     + "  " + "Ожидаемые результаты академической и научной активности (участие в конференциях, публикации, сертификаты и т.д.):" + TextBox6.Text.ToString() + " " + "План работы в принимающей организации(задача и период выполнения): " + TextBox15.Text.ToString()
                     + "*" + "Дата рождения:" + TextBox12.Text.ToString() + " " + "Постоянный почтовый адрес(адрес регистрации):" + TextBox2.Text.ToString() + " " + "Уровень образования:" + TextBox7.Text.ToString() + " " + "Напрвление подготовки:" + TextBox8.Text.ToString() + " "
                     + "Направленность:" + TextBox9.Text.ToString() + " " + "Средний балл:" + TextBox10.Text.ToString() + " " + "Кафедра:" + TextBox13.Text.ToString();

                        PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                        Aplications newacademrequest = new Aplications();
                        newacademrequest.Active = true;
                        newacademrequest.FK_ApplicationType = 1;
                        newacademrequest.FK_UserAdd = userID;
                        newacademrequest.Date = DateTime.Now;
                        newacademrequest.TelephoneNumber = TextBox1.Text;
                        newacademrequest.Text = aspirant;
                        newacademrequest.Confirmed = 0;
                        usersDB.Aplications.InsertOnSubmit(newacademrequest);
                        usersDB.SubmitChanges();
                        newacademrequest.FileURL = "~/AplicationFiles" + "\\\\" + newacademrequest.ID.ToString();
                        String path = Server.MapPath("~/AplicationFiles");
                        Directory.CreateDirectory(path + "\\\\" + userID.ToString());
                        String newpath = Server.MapPath("~/AplicationFiles" + "\\\\" + userID.ToString());
                        Directory.CreateDirectory(newpath + "\\\\" + newacademrequest.ID.ToString());
                        
                        FileUpload1.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload1.FileName);
                        FileUpload2.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload2.FileName);
                        FileUpload3.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload3.FileName);
                        FileUpload4.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload4.FileName);
                        FileUpload5.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload5.FileName);
                        FileUpload6.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload6.FileName);
                        FileUpload7.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload7.FileName);
                        
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка отправлена!');", true);

                    }
                    else
                    {
                        string rabotnik = "Название сети:" + TextBox3.Text.ToString() + "Срок академической мобильности c " + Calendar2.SelectedDate.ToString().Split(' ')[0] + " до " + Calendar3.SelectedDate.ToString().Split(' ')[0]
                     + " " + "Партнерская организация(в порядке приоритетности):" + TextBox14.Text.ToString() + " " + "Обоснование целей и мотивирование реализации академической мобильности:" + TextBox5.Text.ToString()
                     + "  " + "Ожидаемые результаты академической и научной активности (участие в конференциях, публикации, сертификаты и т.д.):" + TextBox6.Text.ToString() + " " + "План работы в принимающей организации(задача и период выполнения): " + TextBox15.Text.ToString()
                     + "*" + "Дата рождения:" + TextBox12.Text.ToString() + " " + "Постоянный почтовый адрес(адрес регистрации):" + TextBox2.Text.ToString() + " " + "Сфера научных интересов:" + TextBox11.Text.ToString();

                        PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
                        Aplications newacademrequest = new Aplications();
                        newacademrequest.Active = true;
                        newacademrequest.FK_ApplicationType = 1;
                        newacademrequest.FK_UserAdd = userID;
                        newacademrequest.Date = DateTime.Now;
                        newacademrequest.TelephoneNumber = TextBox1.Text;
                        newacademrequest.Text = rabotnik;
                        newacademrequest.Confirmed = 0;
                        usersDB.Aplications.InsertOnSubmit(newacademrequest);
                        usersDB.SubmitChanges();
                        newacademrequest.FileURL = "~/AplicationFiles" + "\\\\" + newacademrequest.ID.ToString();
                        String path = Server.MapPath("~/AplicationFiles");
                        Directory.CreateDirectory(path + "\\\\" + userID.ToString());
                        String newpath = Server.MapPath("~/AplicationFiles" + "\\\\" + userID.ToString());
                        Directory.CreateDirectory(newpath + "\\\\" + newacademrequest.ID.ToString());
                        
                        FileUpload1.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload1.FileName);
                        FileUpload2.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload2.FileName);
                        FileUpload3.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload3.FileName);
                        FileUpload4.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload4.FileName);
                        FileUpload5.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload5.FileName);
                        FileUpload6.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload6.FileName);
                        FileUpload7.PostedFile.SaveAs(newpath + "\\\\" + newacademrequest.ID.ToString() + "\\\\" + FileUpload7.FileName);
                        
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка отправлена!');", true);
                    }
               }
            
                else{ Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Вы прикрепили недостаточное количество документов!');", true); }
            }
            
        }

      
    }
}
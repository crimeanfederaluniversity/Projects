using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PersonalPages
{
    public partial class _Default : Page
    {
        private UsersTable user;
        private StudentsTable student;
        public void Directions(UsersTable user)
        {
            if (user.Position == null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
            }
            else if (user.Position.Length > 2)
            {
                FormsAuthentication.SetAuthCookie(user.Position, true);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
            }

            if (student != null || user!= null)
            {            
                Response.Redirect("UserMainPage.aspx");           
            }
            else //если входим сюда то что то не так) скорей всего пользователю не присвоен уровень в UsersTable
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable user = (from usersTables in PersonalPagesDB.UsersTable
                where usersTables.UsersTableID == UserSer.Id
                select usersTables).FirstOrDefault();
            StudentsTable student = (from studTables in PersonalPagesDB.StudentsTable
                                     where studTables.StudentsTableID == UserSer.Id
                                     select studTables).FirstOrDefault();
            if (user != null || student != null)
            {

                if (user != null && student == null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);              
                    Response.Redirect("UserMainPage.aspx");
                }
                if (user == null && student != null)
                {
                    FormsAuthentication.SetAuthCookie(student.Email, true);
                    Response.Redirect("UserMainPage.aspx");
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/UserLogin.aspx");
            }
        }
    }
}

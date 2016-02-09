using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;

namespace PersonalPages
{
    public partial class _Default : Page
    {
        private UsersTable user;

       

        public void UserDirections(UsersTable user)
        {
            if ( user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);                               
                Response.Redirect("UserMainPage.aspx");           
            }
            
            else //если входим сюда то что то не так) скорей всего пользователю не присвоен уровень в UsersTable
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
            }
        
            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }

            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            UsersTable user = (from usersTables in PersonalPagesDB.UsersTable
                where usersTables.UsersTableID == UserSer.Id
                select usersTables).FirstOrDefault();
           
            if (user != null)
            {
                                                    
                    UserDirections(user);
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}

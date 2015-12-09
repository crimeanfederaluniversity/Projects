using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PersonalPages
{
    public partial class UserMainPage : System.Web.UI.Page
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
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();

            UsersTable user = (from usersTables in usersDB.UsersTable
                               where usersTables.UsersTableID==userID
                               && usersTables.Active == true
                               select usersTables).FirstOrDefault();
            if (user.RatingCheck == false || user.RatingCheck == null)
            {
                Button1.Enabled = false;
            }
            if (user.CompetitionCheck == false || user.CompetitionCheck == null)
            {
                Button2.Enabled = false;
            }
            if (user.IndicatorCheck == false || user.IndicatorCheck == null)
            {
                Button3.Enabled = false;
            }
            if (user.MonitorCheck == false || user.MonitorCheck == null)
            {
                Button4.Enabled = false;
            }
            if (user.DocumentCheck == false || user.DocumentCheck == null)
            {
                Button5.Enabled = false;
            }
            if (user.LibraryCheck == false || user.LibraryCheck == null)
            {
                Button6.Enabled = false;
            }
            if (user.RepozitiryCheck == false || user.RepozitiryCheck == null)
            {
                Button7.Enabled = false;
            }
            Label1.Text =user.AcademicDegree + " "+ user.Surname + " " + user.Name.Remove(2) + ". " + user.Patronimyc.Remove(2) + ". " ;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("TeacherPage.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Metodist.aspx");
        }
    }
}
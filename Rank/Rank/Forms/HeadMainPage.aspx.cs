using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rank.Forms
{
    public partial class HeadMainPage : System.Web.UI.Page
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            object str_userID =  Session["userID"] ?? String.Empty;
            bool isSet_userID = int.TryParse(str_userID.ToString(), out userId);

            if (!isSet_userID)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = (int)userId;

            Calculate userpoints = new Calculate();
            userpoints.CalculateHeadParametrPoint(userID);
            Rank_UserRatingPoints point = (from a in ratingDB.Rank_UserRatingPoints
                                           where a.Active == true && a.FK_User == userID
                                          select a).FirstOrDefault();
            if(point!= null)
            {
                Label1.Text = point.Value.ToString();
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/UserMainPage.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/HeadAcceptFirst.aspx");
        }
    }
}
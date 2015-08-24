using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Competition
{
    public partial class TeamUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                 if (!IsPostBack)
            {
                GridviewApdate();
            }
        }
        private void GridviewApdate() //Обновление гридвью
        {
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newUser = new CompetitionDBDataContext();
            DataTable teamuser = new DataTable();
            teamuser.Columns.Add(new DataColumn("Name", typeof(string)));
            teamuser.Columns.Add(new DataColumn("Function", typeof(string)));
            teamuser.Columns.Add(new DataColumn("PayPerHour", typeof(string)));
            List<Users> users = (from a in newUser.Users
                                 join b in newUser.User_BidMapingTable 
                                 on idbid equals b.FK_Bid
                                 where a.ID_User == b.FK_User && a.Active == true select a).ToList();
            
            foreach (Users n in users)
            {
                
                DataRow dataRow = teamuser.NewRow();
                dataRow["Name"] = n.Name;
                dataRow["Function"] = n.Function;
                dataRow["PayPerHour"] = n.PayPerHour;
                teamuser.Rows.Add(dataRow);              
            }
            GridView1.DataSource = teamuser;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e) // сохранение данных из гридвью в базу
        {
            CompetitionDBDataContext newUser = new CompetitionDBDataContext();
            Users param = new Users();
            DataTable pay = (DataTable)ViewState["PayPerHour"];
            DataTable fun = (DataTable)ViewState["Function"];
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox name = (TextBox)GridView1.Rows[i].FindControl("Name");
                TextBox function = (TextBox)GridView1.Rows[i].FindControl("Function");
                TextBox payperhour = (TextBox)GridView1.Rows[i].FindControl("PayPerHour");
                Label Name = (Label)GridView1.Rows[i].FindControl("Name");
                Users Newfunpay = (from a in newUser.Users where a.Name ==  Name.Text && a.Active == true
                                      select a).FirstOrDefault();

                Newfunpay.Name  = name.Text;
                Newfunpay.Function = function.Text;
                Newfunpay.PayPerHour = payperhour.Text;
                newUser.SubmitChanges();                
            }
        }
        protected void Button2_Click(object sender, EventArgs e) //добавление новой строки в гридвью
        {
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newUser = new CompetitionDBDataContext();
            Users newuser = new Users();
            User_BidMapingTable newlink = new User_BidMapingTable();
            newuser.Active = true;
            newuser.Name = null;
            newuser.Function = null;
            newuser.PayPerHour = null;
             
            newUser.Users.InsertOnSubmit(newuser);
            newUser.SubmitChanges();

            newlink.Active = true;
            newlink.Access = 0;
            newlink.FK_Bid = idbid;
            newlink.FK_User = newuser.ID_User;
            GridviewApdate();
      
        }
        
    }
}
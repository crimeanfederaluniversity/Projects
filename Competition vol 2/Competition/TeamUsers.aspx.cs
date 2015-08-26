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
            teamuser.Columns.Add(new DataColumn("PartnerName", typeof(string)));
            teamuser.Columns.Add(new DataColumn("Functions", typeof(string)));
            teamuser.Columns.Add(new DataColumn("PayPerHour", typeof(int)));
            List<Partners> users = (from a in newUser.Partners where a.FK_Bid == idbid
                                  && a.Active == true select a).ToList();

            foreach (Partners n in users)
            {
                
                DataRow dataRow = teamuser.NewRow();
                dataRow["PartnerName"] = n.PartnerName;
                dataRow["Functions"] = n.Functions;
                dataRow["PayPerHour"] = n.PayPerHour;
                teamuser.Rows.Add(dataRow);              
            }
            GridView1.DataSource = teamuser;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e) // сохранение данных из гридвью в базу
        {
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newUser = new CompetitionDBDataContext();
            Partners param = new Partners();
            DataTable pay = (DataTable)ViewState["PayPerHour"];
            DataTable fun = (DataTable)ViewState["Functions"];
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox name = (TextBox)GridView1.Rows[i].FindControl("PartnerName");
                TextBox function = (TextBox)GridView1.Rows[i].FindControl("Functions");
                TextBox payperhour = (TextBox)GridView1.Rows[i].FindControl("PayPerHour");
                //Label Name = (Label)GridView1.Rows[i].FindControl("PartnerName");
               // Partners Newfunpay = (from a in newUser.Partners
               //                       where a.PartnerName == name.Text && a.Active == true
               //                       select a).FirstOrDefault();
                Partners Newfunpay = new Partners();
                Newfunpay.PartnerName  = name.Text;
                Newfunpay.Functions = function.Text;
                Newfunpay.PayPerHour = Convert.ToInt32(payperhour.Text);
                Newfunpay.FK_Bid = idbid;
                newUser.SubmitChanges();                
            }
        }
        protected void Button2_Click(object sender, EventArgs e) //добавление новой строки в гридвью
        {
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newUser = new CompetitionDBDataContext();
            Partners newuser = new Partners();
            
            newuser.Active = true;
            newuser.PartnerName = null;
            newuser.Functions = null;
            newuser.PayPerHour = null;
            newuser.FK_Bid = idbid;
             
            newUser.Partners.InsertOnSubmit(newuser);
            newUser.SubmitChanges();
 
            GridviewApdate();
      
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CalendarPlan.aspx");
        }
        
    }
}
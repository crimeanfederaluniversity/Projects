using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competition
{
    public partial class PurchasePlan1 : System.Web.UI.Page
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
            CompetitionDBDataContext newPlan = new CompetitionDBDataContext();
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];

            DataTable PlanData = new DataTable();
            PlanData.Columns.Add(new DataColumn("ID_Purchase", typeof(int)));
            PlanData.Columns.Add(new DataColumn("Purchase", typeof(string)));
            PlanData.Columns.Add(new DataColumn("Unit", typeof(string)));
            PlanData.Columns.Add(new DataColumn("Amount", typeof(string)));
            PlanData.Columns.Add(new DataColumn("Price", typeof(string)));
            PlanData.Columns.Add(new DataColumn("Sum", typeof(string)));
          

            List<PurchasePlan> plan = (from a in newPlan.PurchasePlan
                                       where
                                           a.FK_Bid == idbid &&
                                           a.Active == true
                                       select a).ToList();

            foreach (PurchasePlan n in plan)
            {

                DataRow dataRow = PlanData.NewRow();
                dataRow["ID_Purchase"] = n.ID_Purchase;
                dataRow["Purchase"] = n.Purchase;
                dataRow["Unit"] = n.Unit;
                dataRow["Amount"] = n.Amount;
                dataRow["Price"] = n.Price;
                dataRow["Sum"] = n.Sum;
                
                PlanData.Rows.Add(dataRow);
            }
            GridView1.DataSource = PlanData;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e) // сохранение данных из гридвью в базу
        {
            CompetitionDBDataContext newPlan = new CompetitionDBDataContext();
            PurchasePlan plan = new PurchasePlan();

            DataTable ID_Purchase = (DataTable)ViewState["ID_Purchase"];
            DataTable Purchase = (DataTable)ViewState["Purchase"];
            DataTable Unit = (DataTable)ViewState["Unit"];
            DataTable Amount = (DataTable)ViewState["Amount"];
            DataTable Price = (DataTable)ViewState["Price"];
            DataTable Sum = (DataTable)ViewState["Sum"];
 

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox Purchase_ = (TextBox)GridView1.Rows[i].FindControl("Purchase");
                TextBox Unit_ = (TextBox)GridView1.Rows[i].FindControl("Unit");
                TextBox Amount_ = (TextBox)GridView1.Rows[i].FindControl("Amount");
                TextBox Price_ = (TextBox)GridView1.Rows[i].FindControl("Price");
                TextBox Sum_ = (TextBox)GridView1.Rows[i].FindControl("Sum");
                Label ID_Purchase_ = (Label)GridView1.Rows[i].FindControl("ID_Purchase");

                PurchasePlan calplan = (from a in newPlan.PurchasePlan
                                        where a.ID_Purchase == Convert.ToInt32(ID_Purchase_.Text)
                                              && a.Active == true
                                        select a).FirstOrDefault();

                calplan.Purchase = Purchase_.Text;
                calplan.Unit =  Unit_.Text;
                calplan.Amount = Convert.ToInt32(Amount_.Text);
                calplan.Price = Convert.ToInt32(Price_.Text);
                calplan.Sum = Convert.ToInt32(Sum_.Text);              
                newPlan.SubmitChanges();
            }
        }

        protected void Button2_Click(object sender, EventArgs e) //добавление новой строки в гридвью
        {
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newPlan = new CompetitionDBDataContext();
            PurchasePlan newplan = new PurchasePlan();
            newplan.Active = true;
            newplan.Purchase = null;
            newplan.Unit = null;
            newplan.Amount = null;
            newplan.Price = null;
            newplan.Sum = null;             
            newplan.FK_Bid = idbid;

            newPlan.PurchasePlan.InsertOnSubmit(newplan);
            newPlan.SubmitChanges();

            GridviewApdate();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TeamUsers.aspx");
        }
    }
}
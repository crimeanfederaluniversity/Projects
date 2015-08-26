using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competition
{
    public partial class CalendarPlan1 : System.Web.UI.Page
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
            PlanData.Columns.Add(new DataColumn("ID_Event", typeof(int)));
            PlanData.Columns.Add(new DataColumn("Event", typeof(string)));
            PlanData.Columns.Add(new DataColumn("Period", typeof(int)));
            PlanData.Columns.Add(new DataColumn("StartDate", typeof(string)));
            PlanData.Columns.Add(new DataColumn("EndDate", typeof(string)));
            PlanData.Columns.Add(new DataColumn("Cost1000", typeof(int)));
            PlanData.Columns.Add(new DataColumn("SourceNull", typeof(string)));
            PlanData.Columns.Add(new DataColumn("TimeNull", typeof(string)));

            List<CalendarPlan> plan = (from a in newPlan.CalendarPlan
                                  where
                                      a.FK_Bid == idbid &&
                                      a.Active == true
                                  select a).ToList();

            foreach (CalendarPlan n in plan)
            {
                 
                DataRow dataRow = PlanData.NewRow();
                dataRow["ID_Event"] = n.ID_Event;
                dataRow["Event"] = n.Event;
                dataRow["Period"] = n.Period;
                dataRow["StartDate"] = n.StartDate;
                dataRow["EndDate"] = n.EndDate;
                dataRow["Cost1000"] = n.Cost1000;
                dataRow["SourceNull"] = n.SourceNull;
                dataRow["TimeNull"] = n.TimeNull;

                PlanData.Rows.Add(dataRow);
            }
            GridView1.DataSource = PlanData;
            GridView1.DataBind();
        }
       
        protected void Button1_Click(object sender, EventArgs e) // сохранение данных из гридвью в базу
        {
            CompetitionDBDataContext newPlan = new CompetitionDBDataContext();
            CalendarPlan plan = new CalendarPlan();
 
            DataTable Event = (DataTable)ViewState["Event"];
            DataTable Period = (DataTable)ViewState["Period"];
            DataTable StartDate = (DataTable)ViewState["StartDate"];
            DataTable EndDate = (DataTable)ViewState["EndDate"];
            DataTable Cost1000 = (DataTable)ViewState["Cost1000"];
            DataTable SourceNull = (DataTable)ViewState["SourceNull"];
            DataTable TimeNull = (DataTable)ViewState["TimeNull"];

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox Event_ = (TextBox)GridView1.Rows[i].FindControl("Event");
                TextBox Period_ = (TextBox)GridView1.Rows[i].FindControl("Period");
                TextBox StartDate_ = (TextBox)GridView1.Rows[i].FindControl("StartDate");
                TextBox EndDate_ = (TextBox)GridView1.Rows[i].FindControl("EndDate");
                TextBox Cost1000_ = (TextBox)GridView1.Rows[i].FindControl("Cost1000");
                TextBox SourceNull_ = (TextBox)GridView1.Rows[i].FindControl("SourceNull");
                TextBox TimeNull_ = (TextBox)GridView1.Rows[i].FindControl("TimeNull");
                Label ID_Event_ = (Label)GridView1.Rows[i].FindControl("ID_Event");

                CalendarPlan calplan = (from a in  newPlan.CalendarPlan                                    
                                      where a.ID_Event == Convert.ToInt32(ID_Event_.Text)
                                            && a.Active == true
                                      select a).FirstOrDefault();

                calplan.Event = Event_.Text;
                calplan.Period = Convert.ToInt32(Period_.Text);
                calplan.StartDate = Convert.ToDateTime(StartDate_.Text);
                calplan.EndDate = Convert.ToDateTime(EndDate_.Text);
                calplan.Cost1000 = Convert.ToDouble(Cost1000_.Text);
                calplan.SourceNull = SourceNull_.Text;
                calplan.TimeNull = TimeNull_.Text;

                newPlan.SubmitChanges();               
            }
        }

        protected void Button2_Click(object sender, EventArgs e) //добавление новой строки в гридвью
        {
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newPlan = new CompetitionDBDataContext();
            CalendarPlan newplan = new CalendarPlan();
            newplan.Active = true;
            newplan.Event = null;
            newplan.Period = 0;
            newplan.StartDate = null;
            newplan.EndDate = null;
            newplan.Cost1000 = 0;
            newplan.SourceNull = null;
            newplan.TimeNull = null;
            newplan.FK_Bid = idbid;
            
            newPlan.CalendarPlan.InsertOnSubmit(newplan);
            newPlan.SubmitChanges();             

            GridviewApdate();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PurchasePlan.aspx");
        }

    
    }
}
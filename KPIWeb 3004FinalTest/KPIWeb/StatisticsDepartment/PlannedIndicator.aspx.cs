using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
//using Microsoft.Office.Interop.Excel;
using Page = System.Web.UI.Page;
namespace KPIWeb
{
    public partial class PlannedIndicator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!(Page.IsPostBack))
            {
                List<IndicatorsTable> indicatorList = (from item in kPiDataContext.IndicatorsTable
                                                       where item.Active == true
                                                       select item).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите целевой показатель");
                foreach (IndicatorsTable item in indicatorList)
                    dictionary.Add(item.IndicatorsTableID, item.Name);
                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
            }
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

                DataTable dataTable = new DataTable();            
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("PlanedIndicatorID", typeof(int)));
                dataTable.Columns.Add(new DataColumn("FK_IndicatorsTable", typeof(int)));
                dataTable.Columns.Add(new DataColumn("Value", typeof(int)));
                dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Active", typeof(string)));

                List<PlannedIndicator> plannedList = (from a in kPiDataContext.PlannedIndicator 
                                                  where a.Active == true  select a  ).ToList();

                
                foreach (PlannedIndicator PlanRow in plannedList)
                {
                                DataRow dataRow = dataTable.NewRow();
                            
                                dataRow["Name"] = (from a  in kPiDataContext.IndicatorsTable where a.IndicatorsTableID == PlanRow.FK_IndicatorsTable
                                                       select a.Name).FirstOrDefault();
                                dataRow["PlanedIndicatorID"] = PlanRow.PlanedIndicatorID.ToString();
                                dataRow["FK_IndicatorsTable"] = PlanRow.FK_IndicatorsTable.ToString();
                                dataRow["Value"] = PlanRow.Value.ToString();
                                dataRow["Date"] =  PlanRow.Date.ToString();
                                dataRow["Active"] = PlanRow.Active.ToString();
                                     dataTable.Rows.Add(dataRow); 
                }           
           
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
             PlannedIndicator indicator = (from item in kPiDataContext.PlannedIndicator
                                                where item.Active==true && item.FK_IndicatorsTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                                                select item).FirstOrDefault();
             if (indicator != null)
             {
                 

                 if (CheckBox1.Checked) indicator.Active = true;
                 else indicator.Active = false;
                 indicator.FK_IndicatorsTable = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                 indicator.Value = Convert.ToDouble(IndicatorMeasure.Text);
                 indicator.Date = Calendar1.SelectedDate;
 
                 kPiDataContext.SubmitChanges();
                 Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Целевой показатель изменен');", true);
             }
        
             else
             {
                 PlannedIndicator indicators = new PlannedIndicator();
                 if (CheckBox1.Checked) indicators.Active = true;
                 else indicators.Active = false;

                 indicators.FK_IndicatorsTable = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                 indicators.Value = Convert.ToDouble(IndicatorMeasure.Text);
                 indicators.Date = Calendar1.SelectedDate;
                 kPiDataContext.PlannedIndicator.InsertOnSubmit(indicators);
                 kPiDataContext.SubmitChanges();
                 Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Целевого показателя создан');", true);

             }
        }



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         
        
          protected void  DeleteButtonClick (object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    PlannedIndicator value = (from a in kPiDataContext.PlannedIndicator
                                              where a.PlanedIndicatorID == Convert.ToInt32(button.CommandArgument)
                                              select a).FirstOrDefault();

                    value.Active = false;
                    kPiDataContext.SubmitChanges();
                }
                Response.Redirect("~/StatisticsDepartment/PlannedIndicator.aspx"); 
            }
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                "alert('Значение удалено');", true);

        }

    }
}
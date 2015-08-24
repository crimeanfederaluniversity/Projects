using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competition
{
    public partial class TargetIndicator : System.Web.UI.Page
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
            int idkon = (int)Session["ID_Konkurs"];
            CompetitionDBDataContext NewValue = new CompetitionDBDataContext();
            DataTable dataTableTarget = new DataTable();
            dataTableTarget.Columns.Add(new DataColumn("ID_TargetIndicator", typeof(int)));
            dataTableTarget.Columns.Add(new DataColumn("TargetIndicator", typeof(string)));
            dataTableTarget.Columns.Add(new DataColumn("PurchaseValue", typeof(double)));
            dataTableTarget.Columns.Add(new DataColumn("Id_Value", typeof(string)));
            List<TargetIndicators> targetlist = (from a in NewValue.TargetIndicators
                                                 join b in NewValue.Konkurs_TargetMapingTable
                                                     on idkon equals b.FK_Konkurs
                                                 where b.FK_Target == a.ID_TargetIndicator && b.FK_Konkurs == idkon
                                                 select a).ToList();
            foreach (TargetIndicators t in targetlist)
            {
                DataRow dataRow = dataTableTarget.NewRow();
                dataRow["ID_TargetIndicator"] = t.ID_TargetIndicator.ToString();
                dataRow["TargetIndicator"] = t.TargetIndicator;
                TargetIndicatorValue purchasevalue = (from a in NewValue.TargetIndicatorValue
                                                      join b in NewValue.TargetIndicators
                                                          on a.FK_TargetIndicator equals b.ID_TargetIndicator
                                                      where a.Active == true && a.FK_Bid == idbid
                                                      select a).FirstOrDefault();
                if (purchasevalue != null)
                {
                    dataRow["PurchaseValue"] = purchasevalue.PurchaseValue;
                }
                else
                {
                    purchasevalue = new TargetIndicatorValue();
                    purchasevalue.Active = true;
                    purchasevalue.PurchaseValue = 0;
                    purchasevalue.FK_Bid = idbid;
                    purchasevalue.FK_TargetIndicator = t.ID_TargetIndicator;
                    NewValue.TargetIndicatorValue.InsertOnSubmit(purchasevalue);
                    NewValue.SubmitChanges();
                }
                dataRow["Id_Value"] = purchasevalue.ID_TargetIndicatorValue;
                dataTableTarget.Rows.Add(dataRow);
            }
            GridView2.DataSource = dataTableTarget;
            GridView2.DataBind();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CompetitionDBDataContext Newtarget = new CompetitionDBDataContext();
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox targetvalue = (TextBox)GridView2.Rows[i].FindControl("PurchaseValue");
                Label Id_stat = (Label)GridView2.Rows[i].FindControl("Id_Value");
                if ((targetvalue != null) && (Id_stat != null))
                {
                    TargetIndicatorValue current = (from a in Newtarget.TargetIndicatorValue
                                                    where a.Active == true && a.ID_TargetIndicatorValue == Convert.ToInt32(Id_stat.Text)
                                                    select a).FirstOrDefault();

                    current.PurchaseValue = Convert.ToInt32(targetvalue.Text);
                    Newtarget.SubmitChanges();
                }
            }
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены!');", true);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SmetaForm.aspx");
        }
    }
}
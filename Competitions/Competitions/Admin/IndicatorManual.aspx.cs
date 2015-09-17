using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class IndicatorManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridviewApdate();
            }
        }

        private void GridviewApdate() //Обновление гридвью
        {
                TextBox1.Text = null;
                TextBox2.Text = null;
                CompetitionDataContext manual = new CompetitionDataContext();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("IndicatorValue", typeof(int)));
                List<zIndicatorManualTable> List = (from a in manual.zIndicatorManualTables
                                                 where a.Active == true
                                                 select a).ToList();
                foreach (zIndicatorManualTable current in List)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = current.ID;
                    dataRow["IndicatorName"] = current.IndicatorName;
                    dataRow["IndicatorValue"] = Convert.ToInt32(current.IndicatorValue);
                    dataTable.Rows.Add(dataRow);
                }
                IndicatorGV.DataSource = dataTable;
                IndicatorGV.DataBind();
            }
        

        protected void Button1_Click1(object sender, EventArgs e)
        {
            CompetitionDataContext manual = new CompetitionDataContext();
            zIndicatorManualTable newindicator = new zIndicatorManualTable();
            newindicator.Active = true;
            newindicator.IndicatorName = TextBox1.Text;
            newindicator.IndicatorValue = Convert.ToInt32(TextBox2.Text);
            manual.zIndicatorManualTables.InsertOnSubmit(newindicator);
            manual.SubmitChanges();
            GridviewApdate();
        }
        
    }
}
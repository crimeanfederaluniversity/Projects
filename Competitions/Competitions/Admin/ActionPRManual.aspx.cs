using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class ActionPRManual : System.Web.UI.Page
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
                CompetitionDataContext manual = new CompetitionDataContext();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ActionPR", typeof(string)));
                List<zActionPRManualTable> List = (from a in manual.zActionPRManualTable
                                                             where a.Active == true
                                                             select a).ToList();
                foreach (zActionPRManualTable current in List)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = current.ID;
                    dataRow["ActionPR"] = current.ActionPR;

                    dataTable.Rows.Add(dataRow);
                }
                ActionGV.DataSource = dataTable;
                ActionGV.DataBind();
            }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDataContext manual = new CompetitionDataContext();
            zActionPRManualTable newaction = new zActionPRManualTable();
            newaction.Active = true;
            newaction.ActionPR = TextBox1.Text;
            manual.zActionPRManualTable.InsertOnSubmit(newaction);
            manual.SubmitChanges();
            GridviewApdate();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManualPage.aspx");
        }
    }
}
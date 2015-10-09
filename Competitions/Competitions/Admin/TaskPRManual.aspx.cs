using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class TaskPRManual : System.Web.UI.Page
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
                dataTable.Columns.Add(new DataColumn("TaskPR", typeof(string)));
                List<zTaskPRManualTable> List = (from a in manual.zTaskPRManualTable
                                                   where a.Active == true
                                                   select a).ToList();
                foreach (zTaskPRManualTable current in List)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = current.ID;
                    dataRow["TaskPR"] = current.TaskPR;

                    dataTable.Rows.Add(dataRow);
                }
                TaskGV.DataSource = dataTable;
                TaskGV.DataBind();
            }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDataContext manual = new CompetitionDataContext();
            zTaskPRManualTable newtask = new zTaskPRManualTable();
            newtask.Active = true;
            newtask.TaskPR = TextBox1.Text;
            manual.zTaskPRManualTable.InsertOnSubmit(newtask);
            manual.SubmitChanges();
            GridviewApdate();
        }
    }
}
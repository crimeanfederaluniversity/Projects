using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.StatisticsDepartment
{
    public partial class ReportComentAdd : System.Web.UI.Page
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
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
             var Tmp = Session["ReportID"];
                if (Tmp == null)
                {
                      Response.Redirect("StatisticsHomePage.aspx");
                }
            int reportid = Convert.ToInt32(Tmp);
            DataTable dataTable = new DataTable();         
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("CommentId", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
            List <BasicParametersTable> basic = (from a in kpiWebDataContext.BasicParametersTable
                                 where a.Active == true 
                                join b in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                                          where b.FK_ReportArchiveTable == reportid
                                          && b.Active == true
                                select a).Distinct().ToList();
            foreach (BasicParametersTable current in basic)
            {
                CommetntForBasicInReport coment = (from a in kpiWebDataContext.CommetntForBasicInReports
                                                          where 
                                                          a.Active == true 
                                                          && a.FK_Report == reportid
                                                          && a.FK_BasickParamets == current.BasicParametersTableID
                                                          select a).FirstOrDefault();
                if (coment == null)
                {
                    coment = new CommetntForBasicInReport();
                    coment.FK_BasickParamets = current.BasicParametersTableID;
                    coment.FK_Report = reportid;
                    coment.Active = true;
                    kpiWebDataContext.CommetntForBasicInReports.InsertOnSubmit(coment);
                    kpiWebDataContext.SubmitChanges();
                }

                DataRow dataRow = dataTable.NewRow();
                dataRow["CommentId"] = coment.ID;
                dataRow["Name"] = current.Name;
                dataRow["Comment"] = coment.Comment;

                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
         

        protected void SaveButton_Click(object sender, EventArgs e)
        {
             var Tmp = Session["ReportID"];
                if (Tmp == null)
                {
                      Response.Redirect("StatisticsHomePage.aspx");
                }
            int reportid = Convert.ToInt32(Tmp);
        
             KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
             for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Label commentId = (Label)GridView1.Rows[i].FindControl("Label2");
                TextBox Comment_ = (TextBox)GridView1.Rows[i].FindControl("ComentTextBox");
                if ((commentId != null))
                  {
                      CommetntForBasicInReport current = (from a in kpiWebDataContext.CommetntForBasicInReports
                                                          where a.ID == Convert.ToInt32(commentId.Text)
                                                      select a).FirstOrDefault();
                      if (current != null)
                      {
                          current.Comment = Comment_.Text;
                          kpiWebDataContext.SubmitChanges();
                      }
                  }
                  else
                  {
                     // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка!');", true);
                  }
            }
             GridviewApdate();
         }
        
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class BasicParametrsComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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

                if (userTable.AccessLevel != 10)
                {
                    Response.Redirect("~/Default.aspx");
                }

               
                List<ReportArchiveTable> RepList = (from a in kPiDataContext.ReportArchiveTable
                                                    where a.Active == true
                                                    select a).ToList();
                foreach (ReportArchiveTable rep in RepList)
                {
                    ListItem Item = new ListItem();
                    Item.Text = (string)rep.Name;
                    Item.Value = (rep.ReportArchiveTableID).ToString();
                    DropDownList1.Items.Add(Item);
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("BasicParamIdValue", typeof(string)));
            dataTable.Columns.Add(new DataColumn("BasicParamName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("CommentValue", typeof(string)));
            dataTable.Columns.Add(new DataColumn("CommentIdValue", typeof(string)));

            int reportID = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);

            List<BasicParametersTable> basicParametersInReport =
                (from a in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                    where a.FK_ReportArchiveTable == reportID
                          && a.Active == true
                          join b in kPiDataContext.BasicParametersTable
                          on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                          where b.Active == true
                    select b).Distinct().ToList();

            foreach (BasicParametersTable currentBasic in basicParametersInReport)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["BasicParamIdValue"] = currentBasic.BasicParametersTableID;
                dataRow["BasicParamName"] = currentBasic.Name;


                CommetntForBasicInReport currentBasicReportComment = (from a in kPiDataContext.CommetntForBasicInReport
                    where a.FK_BasickParamets == currentBasic.BasicParametersTableID
                          && a.FK_Report == reportID
                          && a.Active == true
                    select a).FirstOrDefault();
                if (currentBasicReportComment == null)
                {
                    currentBasicReportComment = new CommetntForBasicInReport();
                    currentBasicReportComment.FK_BasickParamets = currentBasic.BasicParametersTableID;
                    currentBasicReportComment.FK_Report = reportID;
                    currentBasicReportComment.Active = true;
                    kPiDataContext.CommetntForBasicInReport.InsertOnSubmit(currentBasicReportComment);
                    kPiDataContext.SubmitChanges();
                }

                dataRow["CommentIdValue"] = currentBasicReportComment.ID;
                dataRow["CommentValue"] = currentBasicReportComment.Comment;
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            for (int i = 0; i <= GridView1.Rows.Count-1; i++) //в каждой строчке
            {
                Label label = (Label)GridView1.Rows[i].FindControl("CommentId");
                TextBox textBox = (TextBox)GridView1.Rows[i].FindControl("Comment");

                if (label != null && textBox != null)
                {
                    int commentId = -1;
                    if (int.TryParse(label.Text, out commentId) && commentId > -1)
                    {
                        CommetntForBasicInReport currentBasicReportComment =
                            (from a in kPiDataContext.CommetntForBasicInReport
                             where a.ID == commentId
                             select a).FirstOrDefault();
                        if (currentBasicReportComment != null)
                        {
                            if (currentBasicReportComment.Comment != textBox.Text)
                            {
                                currentBasicReportComment.Comment = textBox.Text;
                                kPiDataContext.SubmitChanges();
                            }
                        }
                        
                    }
                }
            }
            Response.Redirect("~/AutomationDepartment/BasicParametrsComments.aspx");
        }
    }
}
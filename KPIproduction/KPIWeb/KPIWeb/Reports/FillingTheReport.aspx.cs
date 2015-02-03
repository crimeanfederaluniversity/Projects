using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;

namespace KPIWeb.Reports
{
    public class DataSource
    {
        public int BasicParametersTableID {get; set;}
        public string Name { get; set; }
        public double? CollectedValue { get; set; }
    }
    public partial class FillingTheReport : System.Web.UI.Page
    {
        UsersTable user;
        private bool isEditMode = false;

        protected bool IsInEditMode
        {
            get { return this.isEditMode; }
            set { this.isEditMode = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                user = (UsersTable)Session["user"];

                if (user == null)
                    Response.Redirect("Login.aspx");
                else
                {
                    KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                    List<int> ReportArchiveIDList = (from reportArchiveTables in KPIWebDataContext.ReportArchiveTables
                                                     join reportAndRolesMappings in KPIWebDataContext.ReportAndRolesMappings on reportArchiveTables.ReportArchiveTableID equals reportAndRolesMappings.FK_ReportArchiveTable
                                                     where reportAndRolesMappings.FK_RolesTable == user.FK_RolesTable &&
                                                     reportArchiveTables.Active == true &&
                                                     reportArchiveTables.StartDateTime < DateTime.Now &&
                                                     reportArchiveTables.EndDateTime > DateTime.Now
                                                     select reportArchiveTables.ReportArchiveTableID).ToList();

                    int ReportArchiveID = ReportArchiveIDList.FirstOrDefault();

                    var TempDataSource = (from basicParametersTables in KPIWebDataContext.BasicParametersTables
                                          join basicParametersAndRolesMappingTables in KPIWebDataContext.BasicParametersAndRolesMappingTables on basicParametersTables.BasicParametersTableID equals basicParametersAndRolesMappingTables.FK_BasicParametersTable
                                          where basicParametersAndRolesMappingTables.FK_RolesTable == user.FK_RolesTable
                                          select new
                                          {
                                              basicParametersTables.BasicParametersTableID,
                                              basicParametersTables.Name,
                                              CollectedValue = string.Empty
                                          }).ToList();

                    List<DataSource> dataSourceList = new List<DataSource>();
                    foreach (var item in TempDataSource)
                    {
                        DataSource dataSource = new DataSource();
                        double? ppp = (from item2 in KPIWebDataContext.CollectedBasicParametersTables
                                       where item2.FK_BasicParametersTable == item.BasicParametersTableID &&
                                       item2.FK_ReportArchiveTable == ReportArchiveID
                                       select item2.CollectedValue).FirstOrDefault();

                        dataSource.BasicParametersTableID = item.BasicParametersTableID;
                        dataSource.Name = item.Name;
                        if (ppp.HasValue)
                            dataSource.CollectedValue = ppp.Value;

                        dataSourceList.Add(dataSource);
                    }
                    gv1.DataSource = dataSourceList;
                    gv1.DataBind();
                }
            }
            //this.dataGridView1.Columns[index].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        protected void gv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            isEditMode = true;
        }

        protected void gv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //foreach (GridViewRow row in gv1.Rows)
            //      { 

            //      } 

            for (int i = 0; i <= gv1.Rows.Count - 1; i++)
            {
                int sno = Convert.ToInt32(gv1.Rows[i].Cells[1].Text);

                String pram = ((TextBox)gv1.Rows[i].FindControl("Textbox2")).Text;
                String unit = ((TextBox)gv1.Rows[i].FindControl("Textbox3")).Text;
                String obtval = ((TextBox)gv1.Rows[i].FindControl("Textbox6")).Text;


                //int Fnoseats = Convert.ToInt32(nose);

                //string constr1;
                //constr1 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr1);
                //con.Open();
                //string sql = "UPDATE [cqac] SET  parameters='" + pram + "', unit='" + unit + "',[spval] = '" + spval + "',torval='" + torval + "',obtval='" + obtval + "'  WHERE [Ssno] =" + sno + "";
                //SqlCommand cmd = new SqlCommand(sql, con);
                //int res;
                //res = cmd.ExecuteNonQuery();
                //res = 1;
                //con.Close();

            }

        }
    }
}
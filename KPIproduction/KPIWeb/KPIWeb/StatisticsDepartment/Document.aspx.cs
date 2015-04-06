using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.StatisticsDepartment
{
    public partial class Document : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<DocumentTable> docs = (from a in kPiDataContext.DocumentTable where a.Active == true select a).ToList();


                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("DocumentName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
                // dataTable.Columns.Add(new DataColumn("DeleteButton", typeof(bool)));
                GridView1.DataSource = docs;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                DocumentTable doc = new DocumentTable();

                doc.DocumentName = TextBox2.Text;
                doc.DocumentLink = TextBox1.Text;
                doc.Active = true;
                kPiDataContext.DocumentTable.InsertOnSubmit(doc);
                kPiDataContext.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ сохранен');" + "document.location = 'Document.aspx';", true);
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Введите точное имя файла');", true);
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    var check =
                    (from a in kPiDataContext.DocumentTable
                     where
                         a.DocumentID == Convert.ToInt32(button.CommandArgument)
                     select a)
                        .FirstOrDefault();

                    check.Active = false;


                    kPiDataContext.SubmitChanges();
                    Response.Redirect("~/StatisticsDepartment/Document.aspx");
 
                }
            }
        }
    }
}
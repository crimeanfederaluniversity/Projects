using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace KPIWeb.Rector
{
    public partial class ViewDocument : System.Web.UI.Page
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

                GridView1.DataSource = docs;
                GridView1.DataBind();
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            
            Button button = (Button)sender;
            string s = button.CommandArgument.ToString();
            // var n = Directory.GetCurrentDirectory();
          // if (n.File.Exists("s") == true)
           //  {
            //if (Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "document.location = 'docs/" + s + "';" ))
           // {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "document.location = 'docs/" + s + "';", true);
          // }
          //  else
          //  {
          //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ не найден');", true);
          //  }
                }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
     }
}
    
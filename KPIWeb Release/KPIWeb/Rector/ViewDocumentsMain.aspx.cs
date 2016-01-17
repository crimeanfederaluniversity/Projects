using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public partial class ViewDocumentsMain : System.Web.UI.Page
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
            if (userTable.AccessLevel != 7)
            {
                Response.Redirect("~/Default.aspx");
            }


            if (!IsPostBack)
            {
                /*
                List<DocumentTypes> docsTypes = (from a in kPiDataContext.DocumentTypes where a.Active == true select a).ToList();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("DocTypeName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DocTypeId", typeof(string)));

                foreach (DocumentTypes docType in docsTypes)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["DocTypeName"] = docType.TypeName;
                    dataRow["DocTypeId"] = docType.DocumentTypeId;
                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();*/
            }
        }

        protected void ViewDocumentClick(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            Response.Redirect("~/Rector/ViewDocumentsSub.aspx?doctype=" + button.CommandArgument);
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }

        protected void GoBackButton_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class Manual : System.Web.UI.Page
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

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                List<ManualTable> manuals = (from a in kPiDataContext.ManualTable where a.Active == true select a).ToList();


                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ManualName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ManualLink", typeof(string)));

                GridView1.DataSource = manuals;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                ManualTable man = new ManualTable();

                man.ManualName = TextBox2.Text;
                man.ManualLink = TextBox1.Text;
                man.Active = true;
                kPiDataContext.ManualTable.InsertOnSubmit(man);
                kPiDataContext.SubmitChanges();
                string savepath = Server.MapPath("//manuals//");
                string fileName = TextBox1.Text;
                if (FileUpload1.HasFile)
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("//manuals//" + fileName));
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ не прикреплен');" + "document.location = 'Manual.aspx';", true);
                }

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Документ сохранен');" + "document.location = 'Manual.aspx';", true);
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
                    (from a in kPiDataContext.ManualTable
                     where
                         a.ManualID == Convert.ToInt32(button.CommandArgument)
                     select a)
                        .FirstOrDefault();

                    check.Active = false;


                    kPiDataContext.SubmitChanges();
                    Response.Redirect("~/StatisticsDepartment/Manual.aspx");

                }
            }
        }
    }
}
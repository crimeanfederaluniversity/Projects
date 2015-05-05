using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class Confirm : System.Web.UI.Page
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
                List<ReportArchiveTable> report =
                  (from item in kPiDataContext.ReportArchiveTable
                   where item.Active == true
                   select item).OrderBy(mc => mc.Name).ToList();

            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Выберите значение");

            foreach (var item in report)
                dictionary.Add(item.ReportArchiveTableID, item.Name);

            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();

                List<ConfirmationHistory> confirms = (from a in kPiDataContext.ConfirmationHistory select a).ToList();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
                GridView1.DataSource = confirms;
                GridView1.DataBind();
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            }
         

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            }
            
             
        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<ConfirmationHistory> confirms = 
                (from a in kPiDataContext.ConfirmationHistory
                 where a.FK_ReportTable == Convert.ToInt32(DropDownList1.SelectedValue) 
                 && a.FK_UsersTable == Convert.ToInt32(DropDownList2.SelectedValue)
                 select a).ToList();
            
            GridView1.DataSource = confirms;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<UsersTable> user =
                    (from item in kPiDataContext.UsersTable
                     join a in kPiDataContext.IndicatorsAndUsersMapping
                     on item.UsersTableID equals a.FK_UsresTable
                     where a.CanConfirm == true
                     select item ).ToList();
          
            if (user != null && user.Count() > 0)
            {
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(-1, "Выберите значение");
                foreach (var item in user)
                {
                    if (!dictionary.ContainsKey(item.UsersTableID))
                    {
                        dictionary.Add(item.UsersTableID, item.Email);
                    }
                }
                DropDownList2.DataTextField = "Value";
                DropDownList2.DataValueField = "Key";
                DropDownList2.DataSource = dictionary;
                DropDownList2.DataBind();
            }

           // DropDownList2.DataSource = new HashSet<string>(user);
            //DropDownList2.DataBind();
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<UsersTable> user =
                    (from item in kPiDataContext.UsersTable
                     join a in kPiDataContext.CalculatedParametrsAndUsersMapping
                     on item.UsersTableID equals a.FK_UsersTable
                     where a.CanConfirm == true
                     select item ).ToList();
            if (user != null && user.Count() > 0)
            {
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(-1, "Выберите значение");
                foreach (var item in user)
                {
                    if (!dictionary.ContainsKey(item.UsersTableID))
                    {
                        dictionary.Add(item.UsersTableID, item.Email);
                    }
                }
                DropDownList2.DataTextField = "Value";
                DropDownList2.DataValueField = "Key";
                DropDownList2.DataSource = dictionary;
                DropDownList2.DataBind();
            }
           // DropDownList2.DataSource = new HashSet<string>(user);
           // DropDownList2.DataBind();
        }
    }
}
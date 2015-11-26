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

                List<UsersTable> users =
                 (from item in kPiDataContext.UsersTable
                  where item.Active == true
                  && item.AccessLevel == 5
                  select item).OrderBy(mc => mc.Email).ToList();

                //var dictionary = new Dictionary<int, string>();
                dictionary.Clear();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in users)
                    dictionary.Add(item.UsersTableID, item.Email);

                DropDownList2.DataTextField = "Value";
                DropDownList2.DataValueField = "Key";
                DropDownList2.DataSource = dictionary;
                DropDownList2.DataBind();
            }

                
               

            
                    List<ConfirmationHistory> confirms = 
                        (from a in kPiDataContext.ConfirmationHistory
                         where 
                         ((a.FK_ReportTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value))
                         || (Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)==0))
                         && 
                         ((a.FK_UsersTable == Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value))
                         || (Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value)==0))
                         select a).ToList();
                
                

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Date", typeof(string)));

                dataTable.Columns.Add(new DataColumn("ParamName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ConfirmUser", typeof(string)));

                foreach (ConfirmationHistory ConfHist in confirms)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Name"] = ConfHist.Name;
                    dataRow["Comment"] = ConfHist.Comment;
                  //  dataRow["Name"] =
                    dataRow["Date"] = ConfHist.Date.ToString();
                    if (ConfHist.FK_BasicParamTable !=null)
                    {
                        dataRow["ParamName"] = (from a in kPiDataContext.BasicParametersTable where a.BasicParametersTableID == ConfHist.FK_BasicParamTable select a.Name).FirstOrDefault();
                    }
                    else if (ConfHist.FK_CalculatedParamTable!=null)
                    {
                        dataRow["ParamName"] = (from a in kPiDataContext.CalculatedParametrs where a.CalculatedParametrsID == ConfHist.FK_CalculatedParamTable select a.Name).FirstOrDefault();
                    }
                    else if (ConfHist.FK_IndicatorsTable !=null)
                    {
                        dataRow["ParamName"] = (from a in kPiDataContext.IndicatorsTable where a.IndicatorsTableID == ConfHist.FK_IndicatorsTable select a.Name).FirstOrDefault();
                    }
                    else
                    {
                        continue;
                    }
                    if (ConfHist.FK_UsersTable != null)
                    {
                        dataRow["ConfirmUser"] = (from a in kPiDataContext.UsersTable where a.UsersTableID == ConfHist.FK_UsersTable select a.Email).FirstOrDefault();

                    }
                    else
                    {
                        continue;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            

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
    }
}
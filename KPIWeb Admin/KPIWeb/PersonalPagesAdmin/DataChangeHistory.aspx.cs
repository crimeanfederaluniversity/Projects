using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class DataChangeHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization ser = (Serialization)Session["userIdChangeHistory"];
            if (ser == null)
            {
                Response.Redirect("Default.aspx");
            }
            int userToChangeId = ser.Id;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("UserChangeDataID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ID_Param_ToChange", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ChangeDate", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UserDataChangeHistory> users;
                {
                    users = (from a in kpiWebDataContext.UserDataChangeHistory
                             where a.Active == true && a.FK_User == userToChangeId                        
                             select a).ToList();
                }

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["UserChangeDataID"] = user.UserChangeDataID;
                   
                    if (user.ID_Param_ToChange == 1)
                    {
                        dataRow["ID_Param_ToChange"] = "Имя";
                    }
                    if (user.ID_Param_ToChange == 2)
                    {
                        dataRow["ID_Param_ToChange"] = "Фамилия";
                    }
                    if (user.ID_Param_ToChange == 3)
                    {
                        dataRow["ID_Param_ToChange"] = "Отчество";
                    }
                    if (user.ID_Param_ToChange == 4)
                    {
                        dataRow["ID_Param_ToChange"] = "Электронный адрес";
                    }
                    if (user.ID_Param_ToChange == 5)
                    {
                        dataRow["ID_Param_ToChange"] = "Академия";
                    }
                    if (user.ID_Param_ToChange == 6)
                    {
                        dataRow["ID_Param_ToChange"] = "Факультет";
                    }
                    if (user.ID_Param_ToChange == 7)
                    {
                        dataRow["ID_Param_ToChange"] = "Кафедра";
                    }
                    dataRow["Name"] = user.Name;
                    dataRow["ChangeDate"] = user.ChangeDate;
                    if (user.Status == false)
                    {
                        dataRow["Status"] = "не подтверждено";
                    }
                    if (user.Status == true)
                    {
                        dataRow["Status"] = "подтверждено";
                    }  
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
    }
}
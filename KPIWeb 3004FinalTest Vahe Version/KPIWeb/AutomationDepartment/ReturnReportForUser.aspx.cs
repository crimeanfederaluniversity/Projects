﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class ReturnReportForUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["User"] = userTable.Email;

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }
            ////////////////////////////////////////////////////////

            var reports = (from a in kPiDataContext.ReportArchiveTable where a.Active select a);

            var dictionary = new Dictionary<int, string>();
            dictionary.Add(-1, "Выберите значение");
            foreach (var obj in reports)
            {
                dictionary.Add(obj.ReportArchiveTableID, obj.Name);
            }

            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();

            var dictionary2 = new Dictionary<int, string>();
            dictionary2.Add(1, "Утвердить данные");
            dictionary2.Add(0, "Разутвердить данные");

            DropDownList2.DataTextField = "Value";
            DropDownList2.DataValueField = "Key";
            DropDownList2.DataSource = dictionary2;
            DropDownList2.DataBind();

            DropDownList2.SelectedIndex = 1;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            var user =
                (from a in kPiDataContext.UsersTable where a.Email.Equals(TextBox1.Text) && a.Active select a).FirstOrDefault();

            if (user != null && (user.FK_ThirdLevelSubdivisionTable != null || user.FK_ThirdLevelSubdivisionTable != 0))
            {
                var lvl = (from a in kPiDataContext.ReportArchiveAndLevelMappingTable
                    where a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                    select a).FirstOrDefault();
                if (lvl != null)
                {
                    var collected4user = (from a in kPiDataContext.CollectedBasicParametersTable
                        where a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable && a.FK_ReportArchiveTable == DropDownList1.SelectedIndex
                        select a).ToList();

                    foreach (var item in collected4user)
                    {
                        if ( DropDownList2.SelectedIndex == 0 )
                            item.Status = 0;
                        if ( DropDownList2.SelectedIndex == 1 )
                            item.Status = 4;
                    }
                    kPiDataContext.SubmitChanges();

                    DisplayAlert("Операция: \" " + DropDownList2.Items[DropDownList2.SelectedIndex].Value.ToString()+" \" успешно выполнена для пользователя: "+user.Email.ToString()+ " в отчете \"" + DropDownList1.Items[DropDownList1.SelectedIndex].Value.ToList() + "\"");
                }
                else DisplayAlert("Данный email не относится к выбранному отчету");
            }
            else DisplayAlert("Данный email не зарегистрирован в системе");
        }

        private void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }
    }
}
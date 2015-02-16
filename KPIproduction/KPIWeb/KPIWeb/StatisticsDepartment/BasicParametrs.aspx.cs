using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace KPIWeb.StatisticsDepartment
{
    public partial class BasicParametrs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));            
            List<BasicParametersTable> basicTable = (from item in kPiDataContext.BasicParametersTable select item).ToList();
            string tmpStr = "0";
            string str;
            int idTmp;
            foreach (BasicParametersTable tableRow in basicTable)
            {
                idTmp = tableRow.BasicParametersTableID;
                str = tableRow.AbbreviationEN;
                if ((str != null) && (str != " ") && (!str.IsEmpty()))
                {
                    if (!str.IsFloat())
                    {
                        int a = (from basic in kPiDataContext.BasicParametersTable
                            where
                                basic.AbbreviationEN == str
                            select basic).Count();
                        if (a > 1)
                        {
                            tmpStr += "\r\n" + "ID=" + idTmp.ToString() + " " + a.ToString() +
                                      " включений аббревиатуры " + str;
                        }
                        else if (a < 1)
                        {
                            tmpStr += "\r\n" + "ID=" + idTmp.ToString() + " " + str +
                                      " такой аббревиатеры не существует, это странно";
                        }
                    }
                    else
                    {
                        tmpStr += "\r\n" + "ID=" + idTmp.ToString() + " Аббревиатура не может быть числом " + str;
                    }
                }
                else
                {
                    tmpStr += "\r\n" + "ID=" + idTmp.ToString() + " Поле аббревиатуры должно быть заполнено " + str;
                }
            }
            if (tmpStr != "0")
            {
                TextBox1.Text = tmpStr;
            }
        }
    }
}
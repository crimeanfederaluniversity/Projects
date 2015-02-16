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
            else
            {
                TextBox1.Text = "Базовые показатели введены корректно";
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            BasicParametersTable basicParametr = (from item in kPiDataContext.BasicParametersTable where  item.BasicParametersTableID== Convert.ToInt32(TextBox8.Text) select item).FirstOrDefault();
            TextBox2.Text = basicParametr.BasicParametersTableID.ToString();
            if (basicParametr.Active)
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }
            TextBox4.Text = basicParametr.Name;
            TextBox5.Text = basicParametr.AbbreviationEN;
            TextBox6.Text = basicParametr.AbbreviationRU;
            TextBox7.Text = basicParametr.Measure;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            BasicParametersTable basicParametr = (from item in kPiDataContext.BasicParametersTable where item.BasicParametersTableID == Convert.ToInt32(TextBox8.Text) select item).FirstOrDefault();
           // basicParametr.BasicParametersTableID = Convert.ToInt32(TextBox2.Text);             
            if (CheckBox1.Checked==true)
            {
                basicParametr.Active = true;
            }
            else
            {
                basicParametr.Active = false;
            }

            basicParametr.Name=TextBox4.Text ;
            basicParametr.AbbreviationEN=TextBox5.Text ;
            basicParametr.AbbreviationRU=TextBox6.Text ;
            basicParametr.Measure=TextBox7.Text ;
            kPiDataContext.SubmitChanges();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
           
            string tmpStr;
            tmpStr = TextBox9.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                BasicParametersTable basicParametr = new BasicParametersTable();
                string[] strArrf = tmp.Split('#');
                basicParametr.Name = strArrf[0];
                basicParametr.AbbreviationEN = strArrf[1];
                basicParametr.AbbreviationRU = strArrf[2];
                basicParametr.Measure = strArrf[3];
                kPiDataContext.BasicParametersTable.InsertOnSubmit(basicParametr);
            }
            kPiDataContext.SubmitChanges();
        }      
    }
}
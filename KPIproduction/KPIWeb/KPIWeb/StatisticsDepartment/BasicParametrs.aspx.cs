using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.StatisticsDepartment
{
    public partial class BasicParametrs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Default.aspx");
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
            if (basicParametr!=null)
            {
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
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Базового показателя с таким ID не существует');", true);    
            }          
        }

        protected void Button3_Click(object sender, EventArgs e)
        {  
            if (TextBox8.Text!="")
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
                BasicParametersTable basicParametr = (from item in kPiDataContext.BasicParametersTable
                                                      where item.BasicParametersTableID == Convert.ToInt32(TextBox8.Text)
                                                      select item).FirstOrDefault();
                if (CheckBox1.Checked == true)
                {
                    basicParametr.Active = true;
                }
                else
                {
                    basicParametr.Active = false;
                }

                basicParametr.Name = TextBox4.Text;
                basicParametr.AbbreviationEN = TextBox5.Text;
                basicParametr.AbbreviationRU = TextBox6.Text;
                basicParametr.Measure = TextBox7.Text;
                kPiDataContext.SubmitChanges();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Нужно загрузить базовый показатель');", true);    
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
           
            string tmpStr;
            tmpStr = TextBox9.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            int i = 0;
           
            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                if (((tmp.Split('#').Length - 1) != 5) && (tmp != ""))            
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка в строке"+i.ToString()+"');", true);
                    i = 0;
                    break;
                }
                i++;
            }
            if (i>0)
            {
                foreach (string tmpStrf in tmpStrArr)
                {
                    if (tmpStrf.Length > 10)
                    {
                        string tmp = tmpStrf.Replace("\n", "");
                        BasicParametersTable basicParametr = new BasicParametersTable();
                        string[] strArrf = tmp.Split('#');
                        strArrf[0] = strArrf[0].TrimEnd();
                        strArrf[0] = strArrf[0].TrimStart();
                        strArrf[1] = strArrf[1].TrimEnd();
                        strArrf[1] = strArrf[1].TrimStart();
                        strArrf[2] = strArrf[2].TrimEnd();
                        strArrf[2] = strArrf[2].TrimStart();
                        strArrf[3] = strArrf[3].TrimEnd();
                        strArrf[3] = strArrf[3].TrimStart();
                        strArrf[4] = strArrf[4].TrimEnd();
                        strArrf[4] = strArrf[4].TrimStart();
                        strArrf[5] = strArrf[5].TrimEnd();
                        strArrf[5] = strArrf[5].TrimStart();
                        basicParametr.Active = true;
                        basicParametr.Name = strArrf[0];
                        basicParametr.AbbreviationEN = strArrf[1];
                        basicParametr.AbbreviationRU = strArrf[2];
                        basicParametr.Measure = strArrf[3];
                        basicParametr.SubvisionLevel = Convert.ToInt32(strArrf[4]);
                        basicParametr.ForeignStudents = Convert.ToInt32(strArrf[5]);
                        kPiDataContext.BasicParametersTable.InsertOnSubmit(basicParametr);
                    }
                }
            }
            kPiDataContext.SubmitChanges();
        }      
    }
}
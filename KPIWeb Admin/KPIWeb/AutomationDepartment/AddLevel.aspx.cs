using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.AutomationDepartment
{
    public partial class AddLevel : System.Web.UI.Page
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

            if (userTable.AccessLevel != 10 && userTable.AccessLevel != 9)
            {
                Response.Redirect("~/Default.aspx");
            }
            ////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                List<FirstLevelSubdivisionTable> First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox4.Text = "";
            CheckBox1.Checked = false;
            TextBox5.Text = "";
            CheckBox2.Checked = false;
            TextBox6.Text = "";
            CheckBox3.Checked = false;
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int SelectedValue = -1;

            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                Button6.Enabled = true;
                CheckBox1.Enabled = true;
                Button9.Enabled = true;
                List<SecondLevelSubdivisionTable> second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                                      where item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.Enabled = true;
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                }
            }
             
        }


        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox5.Text = "";
            CheckBox2.Checked = false;
            TextBox6.Text = "";
            CheckBox3.Checked = false;
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            int SelectedValue = -1;

            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                Button7.Enabled = true;
                CheckBox2.Enabled = true;
                Button10.Enabled = true;
                List<ThirdLevelSubdivisionTable> third_stage = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                                                                where item.FK_SecondLevelSubdivisionTable == SelectedValue
                                                                select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();

                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");

                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList3.Enabled = true;
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button8.Enabled = true;
            CheckBox3.Enabled = true;
            Button11.Enabled = true;
            TextBox6.Text = "";
            CheckBox3.Checked = false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = TextBox1.Text;
            string[] lines = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            foreach (string line in lines)
            {
                string t1 = line.TrimEnd(' ');
                string t2 = t1.TrimStart(' ');
                if (t2.Length > 2)
                {
                    FirstLevelSubdivisionTable fs = new FirstLevelSubdivisionTable();
                    fs.Active = true;
                    fs.Name = t2;
                    fs.FK_ZeroLevelSubvisionTable = 1;

                    kPiDataContext.FirstLevelSubdivisionTable.InsertOnSubmit(fs);
                }
            }
            kPiDataContext.SubmitChanges();
            TextBox1.Text = "";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Изменения внесены');", true);
            clearall();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Text.Equals("Выберите значение"))
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Выберите сначала Академию!');", true);
            else
            {
                int SelectedValue = -1;
                if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
                {
                    string s = TextBox2.Text;
                    string[] lines = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext();
                    foreach (string line in lines)
                    {
                        string t1 = line.TrimEnd(' ');
                        string t2 = t1.TrimStart(' ');
                        if (t2.Length > 2)
                        {
                            SecondLevelSubdivisionTable ss = new SecondLevelSubdivisionTable();
                            ss.Active = true;
                            ss.Name = t2;
                            ss.FK_FirstLevelSubdivisionTable = SelectedValue;
                            kPiDataContext.SecondLevelSubdivisionTable.InsertOnSubmit(ss);
                        }
                    }
                    kPiDataContext.SubmitChanges();
                    TextBox2.Text = "";
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Изменения внесены');", true);
                clearall();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedItem == null || (DropDownList2.SelectedItem != null && DropDownList2.SelectedItem.Text.Equals("Выберите значение")))
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                    "alert('Выберите сначала Академию/Факультет!');", true);
            else
            {
                int SelectedValue = -1;
                if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
                {
                    string s = TextBox3.Text;
                    string[] lines = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext();
                    foreach (string line in lines)
                    {
                        string t1 = line.TrimEnd(' ');
                        string t2 = t1.TrimStart(' ');
                        if (t2.Length > 2)
                        {
                            ThirdLevelSubdivisionTable ts = new ThirdLevelSubdivisionTable();
                            ts.Active = true;
                            ts.Name = t2;
                            ts.FK_SecondLevelSubdivisionTable = SelectedValue;
                            kPiDataContext.ThirdLevelSubdivisionTable.InsertOnSubmit(ts);
                            kPiDataContext.SubmitChanges();

                            ThirdLevelParametrs tp = new ThirdLevelParametrs();
                            tp.Active = true;
                            tp.CanGraduate = true;
                            tp.ThirdLevelParametrsID = ts.ThirdLevelSubdivisionTableID;
                            kPiDataContext.ThirdLevelParametrs.InsertOnSubmit(tp);
                        }
                    }
                    kPiDataContext.SubmitChanges();
                    TextBox3.Text = "";
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Изменения внесены');", true);
                clearall();
            }
        }

        protected void clearall()
        {
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<FirstLevelSubdivisionTable> First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Выберите значение");

            foreach (var item in First_stageList)
                dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AutomationDepartment/Main.aspx");
        }
     

        protected void Button6_Click1(object sender, EventArgs e)
        {
            
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                FirstLevelSubdivisionTable First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable 
                                                              where item.FirstLevelSubdivisionTableID == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                                                              select item).FirstOrDefault();

                if (First_stageList != null)
                {
                    TextBox4.Text = First_stageList.Name.ToString();

                    if (First_stageList.Active = true)
                    {
                        CheckBox1.Checked = true;
                    }
                    else
                    {
                        CheckBox1.Checked = false;
                    }
                    
            }
                
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            if (TextBox4.Text != "")
            {
               
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                FirstLevelSubdivisionTable First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable
                                                              where item.FirstLevelSubdivisionTableID == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                                                              select item).FirstOrDefault();
                List<SecondLevelSubdivisionTable> delete_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                                     where item.FK_FirstLevelSubdivisionTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                                                                     select item).ToList();
                List<ThirdLevelSubdivisionTable> delete_list = (from item in kPiDataContext.ThirdLevelSubdivisionTable join item2 in kPiDataContext.SecondLevelSubdivisionTable
                                                               on item.FK_SecondLevelSubdivisionTable equals item2.SecondLevelSubdivisionTableID
                                                                where item2.FK_FirstLevelSubdivisionTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                                                                select item).ToList();
                First_stageList.Name = TextBox4.Text;
                if (CheckBox1.Checked == true)
                {
                    First_stageList.Active = true;
                }
                else
                {
                    First_stageList.Active = false;
                    foreach (SecondLevelSubdivisionTable m in delete_stageList)
                    {
                        m.Active = false;
                    }
                        foreach (ThirdLevelSubdivisionTable n in delete_list)
                        {
                            n.Active = false;
                        }
                    
                }
                kPiDataContext.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Изменения внесены');" + "document.location = 'AddLevel.aspx';", true);
                
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ничего не введено');", true);
                clearall();
            }       
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            SecondLevelSubdivisionTable Second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                          where item.SecondLevelSubdivisionTableID == Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value)
                                                          select item).FirstOrDefault();

            if (Second_stageList != null)
            {
                TextBox5.Text = Second_stageList.Name.ToString();

                if (Second_stageList.Active == true)
                {
                    CheckBox2.Checked = true;
                }
                else
                {
                    CheckBox2.Checked = false;
                }
            
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            if (TextBox5.Text != "")
            {

                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                SecondLevelSubdivisionTable Second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                                where item.SecondLevelSubdivisionTableID == Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value)
                                                                select item).FirstOrDefault();

                List<ThirdLevelSubdivisionTable> delete_stageList = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                                                                     where item.FK_SecondLevelSubdivisionTable == Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value)
                                                                     select item).ToList();

                Second_stageList.Name = TextBox5.Text;
                if (CheckBox2.Checked == true)
                {
                    Second_stageList.Active = true;
                }
                else
                {
                    Second_stageList.Active = false;
                    foreach (ThirdLevelSubdivisionTable n in delete_stageList)
                    {
                        n.Active = false;
                    }

                }

                kPiDataContext.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Изменения внесены');" + "document.location = 'AddLevel.aspx';", true);
            }
            else
            {

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ничего не введено');", true);
                clearall();
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            ThirdLevelSubdivisionTable Third_stageList = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                                                           where item.ThirdLevelSubdivisionTableID == Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value)
                                                           select item).FirstOrDefault();

            if (Third_stageList != null)
            {
                TextBox6.Text = Third_stageList.Name.ToString();

                if (Third_stageList.Active = true)
                {
                    CheckBox3.Checked = true;
                }
                else
                {
                    CheckBox3.Checked = false;
                }
            
            }
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            if (TextBox6.Text != "")
            {

                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                ThirdLevelSubdivisionTable Third_stageList = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                                                              where item.ThirdLevelSubdivisionTableID == Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value)
                                                              select item).FirstOrDefault();
                Third_stageList.Name = TextBox6.Text;
                if (CheckBox3.Checked == true)
                {
                    Third_stageList.Active = true;
                }
                else
                {
                    Third_stageList.Active = false;
                }



                kPiDataContext.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Изменения внесены');" + "document.location = 'AddLevel.aspx';", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ничего не введено');", true);
                clearall();
            }
        }
    }
}
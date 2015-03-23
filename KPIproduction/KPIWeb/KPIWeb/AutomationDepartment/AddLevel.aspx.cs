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
            KPIWebDataContext kPiDataContext = new KPIWebDataContext( );
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
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

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext( );
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
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
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext( );

            int SelectedValue = -1;

            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
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
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = TextBox1.Text;
            string[] lines = s.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            KPIWebDataContext kPiDataContext = new KPIWebDataContext( );
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
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script", "alert('Выберите сначала Академию!');", true);
            else
            {
                int SelectedValue = -1;
                if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
                {
                    string s = TextBox2.Text;
                    string[] lines = s.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext( );
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
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script", "alert('Изменения внесены');", true);
                clearall();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedItem == null || (DropDownList2.SelectedItem != null && DropDownList2.SelectedItem.Text.Equals("Выберите значение")))
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                    "alert('Выберите сначала Академию/Факультет!');", true);
            else
            {
                int SelectedValue = -1;
                if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
                {
                    string s = TextBox3.Text;
                    string[] lines = s.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext( );
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
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script", "alert('Изменения внесены');", true);
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

            KPIWebDataContext kPiDataContext = new KPIWebDataContext( );
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
      
        protected void Button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}
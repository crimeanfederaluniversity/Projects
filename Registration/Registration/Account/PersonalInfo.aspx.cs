using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Registration.Account
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        UsersDBDataContext rating = new UsersDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            UsersTable author = null;
            if (Session["edituser"] != null)
            {
                var id = Convert.ToInt32(Session["edituser"]);
                author = (from a in rating.UsersTable where a.Active == false && a.UsersTableID == id select a).FirstOrDefault();
            }
            /*
            if (author != null )
            {
                TextBox1.Text = author.Email;
                TextBox2.Text = author.Surname;
                TextBox3.Text = author.Name;
                TextBox4.Text = author.Patronimyc;
                TextBox6.Text = author.PublicationFIO;
                if (author.TypeOfPosition == true)
                {
                 //   DropDownList6.SelectedIndex = 0;
                    PPS.Visible = true;
                //    PPS.Items.FindByText(author.Position).Selected = true;            
                }
                else
                {
                 //   DropDownList6.SelectedIndex = 1;
                    NR.Visible = true;
                 //   NR.Items.FindByText(author.Position).Selected = true;
                }
                stavka.Items.FindByText(author.Stavka.ToString()).Selected = true;
                if (author.Degree != null)
                {
               //     degree.Items.FindByText(author.Degree).Selected = true;
                }
            }       
            */        
                if (Convert.ToInt32(DropDownList6.SelectedItem.Value) == 0)
                {
                    PPS.Visible = true;
                    NR.Visible = false;
                }
                else
                {
                    PPS.Visible = false;
                    NR.Visible = true;
                }
                if (!IsPostBack)
            {
                 
                List<FirstLevelSubdivisionTable> First_stageList = (from item in rating.FirstLevelSubdivisionTable where item.Active == true select item).OrderBy(mc => mc.Name).ToList();
                    var dictionary = new Dictionary<int, string>();              
                    dictionary.Add(0, "Выберите академию");
                    foreach (var item in First_stageList)
                        dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);              
                    DropDownList1.DataTextField = "Value";
                    DropDownList1.DataValueField = "Key";
                    DropDownList1.DataSource = dictionary;
                    DropDownList1.DataBind();
                /*
                if (author != null)
                {
                    DropDownList1.SelectedValue = author.FK_FirstLevelSubdivisionTable.ToString();
                }
               
                */
            }         
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsersTable author = null;
            if (Session["edituser"] != null)
            {
                var id = Convert.ToInt32(Session["edituser"]);
                author = (from a in rating.UsersTable where a.Active == false && a.UsersTableID == id select a).FirstOrDefault();
            }
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in rating.SecondLevelSubdivisionTable
                                                                      where item.Active == true && item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите факультет");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.Enabled = true;
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                    /*
                    if (author.FK_SecondLevelSubdivisionTable != null)
                    {
                        DropDownList2.Items.FindByValue(author.FK_SecondLevelSubdivisionTable.ToString()).Selected = true;
                    }
                    */
                }
            }
        }
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsersTable author = null;
            if (Session["edituser"] != null)
            {
                var id = Convert.ToInt32(Session["edituser"]);
                author = (from a in rating.UsersTable where a.Active == false && a.UsersTableID == id select a).FirstOrDefault();
            }
            int SelectedValue = -1;         
            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<ThirdLevelSubdivisionTable> third_stage = (from item in rating.ThirdLevelSubdivisionTable
                                                                where item.Active == true && item.FK_SecondLevelSubdivisionTable == SelectedValue
                                                                select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();
                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите кафедру");
                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList3.Enabled = true;
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                    /*
                    if (author.FK_ThirdLevelSubdivisionTable != null)
                    {
                        SelectedValue = author.FK_ThirdLevelSubdivisionTable.Value;
                    }
                    */
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != null && TextBox2.Text.Any() && TextBox3.Text.Any() && TextBox4.Text.Any() && TextBox6.Text.Any() && DropDownList1.SelectedIndex != 0)
            {
                UsersTable newuser = new UsersTable();
                newuser.Active = false;
                newuser.Email = TextBox1.Text;
                newuser.Surname = TextBox2.Text;
                newuser.Name = TextBox3.Text;
                newuser.Patronimyc = TextBox4.Text;
                if (degree.SelectedIndex != 2)
                {
                    newuser.Degree = degree.SelectedItem.Text;
                }
                if (PPS.Visible == true)
                {
                    newuser.AccessLevel = Convert.ToInt32(PPS.SelectedItem.Value);
                    newuser.Position = PPS.SelectedItem.Text;
                }
                else if (NR.Visible == true)
                {
                    newuser.AccessLevel = Convert.ToInt32(NR.SelectedItem.Value);
                    newuser.Position = NR.SelectedItem.Text;
                }
                newuser.FK_FirstLevelSubdivisionTable = Convert.ToInt32(DropDownList1.SelectedItem.Value);
                int selectedValue = -1;
                if (int.TryParse(DropDownList2.SelectedValue, out selectedValue) && selectedValue > 0)
                    newuser.FK_FirstLevelSubdivisionTable = selectedValue;

                selectedValue = -1;
                if (int.TryParse(DropDownList3.SelectedValue, out selectedValue) && selectedValue > 0)
                    newuser.FK_FirstLevelSubdivisionTable = selectedValue;

                newuser.Stavka = Convert.ToDouble(stavka.SelectedItem.Value);
                newuser.PublicationFIO = TextBox6.Text;
                newuser.Password = "";
                if (Convert.ToInt32(DropDownList6.SelectedItem.Value) == 0)
                {
                    newuser.TypeOfPosition = true;
                }
                else if (Convert.ToInt32(DropDownList6.SelectedItem.Value) == 1)
                {
                    newuser.TypeOfPosition = false;
                }
                newuser.Confirmed = false;
                rating.UsersTable.InsertOnSubmit(newuser);
                rating.SubmitChanges();
                Response.Redirect("~/Account/FinishPage.aspx");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Пожалуйста заполните все обязательные поля.');", true);
            }             
        }
    }
}
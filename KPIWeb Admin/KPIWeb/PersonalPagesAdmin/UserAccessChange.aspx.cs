using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class UserAccessChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            if(!Page.IsPostBack)
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add(new DataColumn("UsersTableId", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Pass", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Surname", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Patronimyc", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Firstlvl", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Secondlvl", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Thirdlvl", typeof(string)));
            dataTable1.Columns.Add(new DataColumn("Acceslvl", typeof(string)));
           // dataTable1.Columns.Add(new DataColumn("Position", typeof(string)));
           // dataTable1.Columns.Add(new DataColumn("Kurs", typeof(string))); 
            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<UsersTable> users;
                {
                    users = (from a in kpiWebDataContext.UsersTable where a.Active == true select a).ToList();
                }

                foreach (var user in users)
                {
                    DataRow dataRow = dataTable1.NewRow();
                    dataRow["UsersTableId"] = user.UsersTableID;
                    dataRow["Email"] = user.Email;
                    dataRow["Pass"] = user.Password;
                    dataRow["Surname"] = user.Surname;
                    dataRow["Name"] = user.Name;
                    dataRow["Patronimyc"] = user.Patronimyc;
                    dataRow["Firstlvl"] = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                           where a.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();
                    dataRow["Secondlvl"] = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                            where a.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                            select a.Name).FirstOrDefault();
                    dataRow["Thirdlvl"] = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                           where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();
                    dataRow["Acceslvl"] = user.AccessLevel;
                   // dataRow["Position"] = user.Position;
                   // dataRow["Kurs"] = user.Kurs; 
                    dataTable1.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable1;
                GridView1.DataBind();
            }
        }
         protected void SaveUserButtonClick(object sender, EventArgs e)
        {           
                int rowIndex = 0;
                Button button = (Button)sender;
                {
                    if (ViewState["GridviewUsers"] != null)
                    {
                        DataTable dataTable = (DataTable)ViewState["GridviewUsers"];

                        if (dataTable.Rows.Count > 0)
                        {
                            for (int i = 1; i <= dataTable.Rows.Count; i++)
                            {
                                Label TextBoxUser = (Label)GridView1.Rows[rowIndex].FindControl("UsersTableId");
                                if (TextBoxUser.Text.Equals(button.CommandArgument.ToString()))
                                {                                  
                                    TextBox TextBoxEmail = (TextBox)GridView1.Rows[rowIndex].FindControl("Email");
                                    TextBox TextBoxPass = (TextBox)GridView1.Rows[rowIndex].FindControl("Pass");
                                    TextBox TextBoxSurname = (TextBox)GridView1.Rows[rowIndex].FindControl("Surname");
                                    TextBox TextBoxName = (TextBox)GridView1.Rows[rowIndex].FindControl("Name");                              
                                    TextBox TextBoxPatronimyc = (TextBox)GridView1.Rows[rowIndex].FindControl("Patronimyc");
 
                                   
                                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                                    {
                                        UsersTable user = (from a in kPiDataContext.UsersTable  where a.UsersTableID == Convert.ToInt32(button.CommandArgument)  select a).FirstOrDefault();

                                        if (TextBoxEmail.Text.Any())
                                            user.Email = TextBoxEmail.Text;
                                        if (TextBoxPass.Text.Any())
                                            user.Password = TextBoxPass.Text;
                                        if (TextBoxPatronimyc.Text.Any())
                                            user.Patronimyc = TextBoxPatronimyc.Text;
                                        if (TextBoxSurname.Text.Any())
                                            user.Surname = TextBoxSurname.Text;
                                        if (TextBoxName.Text.Any())
                                            user.Name = TextBoxName.Text;                          
                                                                                                                                                           
                                        kPiDataContext.SubmitChanges();                                      
                                    }
                                }
                                rowIndex++;
                            }
                        }
                    }
                }
         }               
        protected void ChangeUserButtonClick(object sender, EventArgs e)
        {
             
            Button button = (Button)sender;
            {
                Serialization ser = new Serialization(Convert.ToInt32(button.CommandArgument));
                Session["userIdforChange"] = ser;
                Response.Redirect("~/PersonalPagesAdmin/ChangeUserAccessLevel.aspx");
            }
        }
        protected void DeleteUserButtonClick(object sender, EventArgs e)
        {
            if (!CheckBox2.Checked)
            {
                Button button = (Button)sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        UsersTable user =
                            (from a in kPiDataContext.UsersTable
                             where a.UsersTableID == Convert.ToInt32(button.CommandArgument)
                             select a).FirstOrDefault();

                        user.Active = false;
                        kPiDataContext.SubmitChanges();
                         
                    }
                    RefreshGrid();

                }
            }
            else
            {
                DisplayAlert("Снимите предохранитель");
            }
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("UsersTableId", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Pass", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Surname", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Patronimyc", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Acceslvl", typeof(string)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                if (TextBox2.Text.Any())
                {
                    List<UsersTable> users =
                        (from a in kpiWebDataContext.UsersTable
                         where a.Active == true && a.Email.Contains(TextBox2.Text)
                         select a).ToList();

                    foreach (var user in users)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["UsersTableId"] = user.UsersTableID;
                        dataRow["Email"] = user.Email;
                        dataRow["Pass"] = user.Password;
                        dataRow["Surname"] = user.Surname;
                        dataRow["Name"] = user.Name;
                        dataRow["Patronimyc"] = user.Patronimyc;
                        dataRow["Firstlvl"] = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                               where a.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                               select a.Name).FirstOrDefault();
                        dataRow["Secondlvl"] = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                where a.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                                select a.Name).FirstOrDefault();
                        dataRow["Thirdlvl"] = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                               where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                               select a.Name).FirstOrDefault();
                        dataRow["Acceslvl"] = user.AccessLevel;
                        dataTable.Rows.Add(dataRow);
                    }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        }

        protected void Button2_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
        }
 
        }
     
    }
 
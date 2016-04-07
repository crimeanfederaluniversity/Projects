using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edmAdmin
{
    public partial class ChangeUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Id = Session["userAdmin"];
            if (Id == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                grid1Update();
            }
            
        }
        protected void ChangeUserButtonClick(object sender, EventArgs e)
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            Button button = (Button)sender;
            Session["userID"] = Convert.ToInt32(button.CommandArgument);
            Response.Redirect("~/edmAdmin/ChangeUser.aspx");
        }
        protected void SaveUserButtonClick(object sender, EventArgs e)
        {
            int rowIndex = 0;
            Button button = (Button)sender;
            {
                    DataTable dataTable = (DataTable)ViewState["GridviewUsers"];

                    if (dataTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dataTable.Rows.Count; i++)
                        {
                            Label TextBoxFourthlvlId = (Label)GridView1.Rows[rowIndex].FindControl("ID");
                            if (TextBoxFourthlvlId.Text.Equals(button.CommandArgument.ToString()))
                            {
                                TextBox TextBoxLogin = (TextBox)GridView1.Rows[rowIndex].FindControl("Login");
                                TextBox TextBoxPassword = (TextBox)GridView1.Rows[rowIndex].FindControl("Password");
                                TextBox TextBoxEmail = (TextBox)GridView1.Rows[rowIndex].FindControl("Email");
                                Label SecondLevel = (Label)GridView1.Rows[rowIndex].FindControl("SecondLevel");
                                Label Structure = (Label)GridView1.Rows[rowIndex].FindControl("Structure");
                                Label TextBoxThirdlvl = (Label)GridView1.Rows[rowIndex].FindControl("SaveUser");
                            CheckBox CanIniate = (CheckBox)GridView1.Rows[rowIndex].FindControl("CanIniate");
                            CheckBox CanCreate = (CheckBox)GridView1.Rows[rowIndex].FindControl("CanCreate");
                            CheckBox CanIniateCustom = (CheckBox)GridView1.Rows[rowIndex].FindControl("CanIniateCustom");
                            CheckBox CanPrint = (CheckBox)GridView1.Rows[rowIndex].FindControl("CanPrint");

                            using (EDMdbDataContext edmDb = new EDMdbDataContext())
                                {
                                    Users user =
                                        (from a in edmDb.Users
                                         where a.userID == Convert.ToInt32(button.CommandArgument)
                                         select a).FirstOrDefault();
                                    user.login = TextBoxLogin.Text;
                                    user.password= TextBoxPassword.Text;
                                    user.email = TextBoxEmail.Text;
                                    user.@struct = Structure.Text;
                                user.canInitiate = CanIniate.Checked;
                                user.canCreateTemplate = CanCreate.Checked;
                                user.canInitiateCustom = CanIniateCustom.Checked;
                                user.canPrintResult = CanPrint.Checked;
                                edmDb.SubmitChanges();
                       
                                }
                            }
                            rowIndex++;
                        }
                    }            
            }
        }
        protected void DeleteUserButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (EDMdbDataContext edmDb = new EDMdbDataContext())
                {
                    Users user =
                        (from a in edmDb.Users
                         where a.userID == Convert.ToInt32(button.CommandArgument)
                         select a).FirstOrDefault();

                    user.active = false;
                    edmDb.SubmitChanges();
                }
                grid1Update();
            }
        }
        protected void grid1Update()
        {        
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Login", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Password", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Structure", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SecondLevel", typeof(string)));
            dataTable.Columns.Add(new DataColumn("DeleteUser", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SaveUser", typeof(string)));
            dataTable.Columns.Add(new DataColumn("ChangeUser", typeof(string)));
            dataTable.Columns.Add(new DataColumn("CanIniate", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("CanCreate", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("CanIniateCustom", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("CanPrint", typeof(bool)));

            using (EDMdbDataContext edmDb = new EDMdbDataContext())
            {
                List<Users> users = (from a in edmDb.Users where a.active == true select a).ToList();
            
                foreach (var user in users)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = user.userID;
                    dataRow["Login"] = user.login;
                    dataRow["Password"] = user.password;
                    dataRow["Email"] = user.email;
                    dataRow["Structure"] = user.@struct;
                    dataRow["SecondLevel"] = (from a in edmDb.Struct where a.active == true && a.structID == user.fk_struct select a.name).FirstOrDefault();
                    dataRow["DeleteUser"] = user.userID;
                    dataRow["SaveUser"] = user.userID;
                    dataRow["ChangeUser"] = user.userID;
                    dataRow["CanIniate"] = user.canInitiate;
                    dataRow["CanCreate"] = user.canCreateTemplate;
                    dataRow["CanIniateCustom"] = user.canInitiateCustom;
                    dataRow["CanPrint"] = user.canPrintResult;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();            
        }

    }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/EditUser.aspx");
        }
    }
}

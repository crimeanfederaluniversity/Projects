using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PersonalPages
{
    public partial class CreateStudGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;  
        
                var IdTmp = Session["GroupID"];
                if (IdTmp == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int groupid = Convert.ToInt32(IdTmp);
             
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Columns.Add("ID", typeof(string));
                    dataTable1.Columns.Add("Surname", typeof(string));
                    dataTable1.Columns.Add("Name", typeof(string));
                    dataTable1.Columns.Add("Patronimyc", typeof(string));
                    dataTable1.Columns.Add("Kurs", typeof(string));
                    PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                    List<StudentsTable> studInThisGroup = (from a in PersonalPagesDB.StudentsTable
                                                           where a.Active == true && a.FK_Group == groupid                                                            
                                                           select a).ToList();
                    foreach (StudentsTable current in studInThisGroup)
                    {
                        DataRow dataRow1 = dataTable1.NewRow();
                        dataRow1["ID"] = current.StudentsTableID;
                        dataRow1["Surname"] = current.Surname;
                        dataRow1["Name"] = current.Name;
                        dataRow1["Patronimyc"] = current.Patronimyc;
                        dataRow1["Kurs"] = current.Kurs;
                        dataTable1.Rows.Add(dataRow1);

                    }
                    StudInGroupGV.DataSource = dataTable1;
                    StudInGroupGV.DataBind();
 
                    List<StudentsTable> studTable = (from a in PersonalPagesDB.StudentsTable
                                                     where a.Active == true && a.FK_Group == null
                                                     join b in PersonalPagesDB.UsersTable
                                                     on a.FK_SecondLevelSubdivision equals b.FK_SecondLevelSubdivisionTable
                                                     where b.UsersTableID == userID
                                                     select a).ToList();
                
                    DataTable dataTable2 = new DataTable();
                    dataTable2.Columns.Add("ID", typeof(string));
                    dataTable2.Columns.Add("Surname", typeof(string));
                    dataTable2.Columns.Add("Name", typeof(string));
                    dataTable2.Columns.Add("Patronimyc", typeof(string));
                    dataTable2.Columns.Add("Kurs", typeof(string));

                    foreach (StudentsTable current in studTable)
                    {
                        DataRow dataRow2 = dataTable2.NewRow();
                        dataRow2["ID"] = current.StudentsTableID;
                        dataRow2["Surname"] = current.Surname;
                        dataRow2["Name"] = current.Name;
                        dataRow2["Patronimyc"] = current.Patronimyc;
                        dataRow2["Kurs"] = current.Kurs;
                        dataTable2.Rows.Add(dataRow2);

                    }
                    StudOutGroupGV.DataSource = dataTable2;
                    StudOutGroupGV.DataBind();
                }

            }
        protected void StudentDeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var IdTmp = Session["GroupID"];
                if (IdTmp == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int groupid = Convert.ToInt32(IdTmp);

                PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                StudentsTable studentdelete =
                    (from a in PersonalPagesDB.StudentsTable
                     where a.Active == true && a.StudentsTableID == Convert.ToInt32(button.CommandArgument)
                     select a).FirstOrDefault();
                if (studentdelete != null)
                {
                    studentdelete.FK_Group = null;
                    PersonalPagesDB.SubmitChanges();
                }
            }
            Response.Redirect("~/CreateStudGroup.aspx");

        }
        protected void StudentAddButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var IdTmp = Session["GroupID"];
                if (IdTmp == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int groupid = Convert.ToInt32(IdTmp);

                PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                StudentsTable grouptadd =
                     (from a in PersonalPagesDB.StudentsTable
                      where a.Active == true && a.StudentsTableID == Convert.ToInt32(button.CommandArgument)                          
                      select a).FirstOrDefault();

                if (grouptadd != null)
                {
                    if (grouptadd.FK_Group == null)
                    {
                        grouptadd.FK_Group = groupid;
                        PersonalPagesDB.SubmitChanges();
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Этот студент уже привязан к другой группе!');", true);
                    }
                }               
            }
            Response.Redirect("~/CreateStudGroup.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AllStudGroups.aspx");
        }      
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edmAdmin
{
    public partial class StructureForm : System.Web.UI.Page
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
                DropdownStructUpdate();
            }
        }
        protected void DropdownStructUpdate()
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            List<Struct> structList =
                    (from a in edmDb.Struct
                     where a.active == true
                     select a).OrderBy(mc => mc.structID).ToList();
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(-1, "Выберите значение");
            foreach (var item in structList)
                dictionary.Add(item.structID, item.name);
            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();
        }

        protected void DeleteStructure_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                if (DropDownList1.SelectedItem.Text != "Выберите значение")
                {
                    using (EDMdbDataContext edmDb = new EDMdbDataContext())
                    {
                        Struct structure =
                            (from a in edmDb.Struct
                             where a.name == DropDownList1.SelectedItem.Text
                             select a).FirstOrDefault();
                        List<Struct> struc = (from a in edmDb.Struct where a.fk_parent == structure.structID select a).ToList();
                        foreach (var a in struc)
                        {
                            a.active = false;
                        }
                        structure.active = false;
                        edmDb.SubmitChanges();
                        DisplayAlert("Структурное подразделение было удалено вместе со всеми подчиненными подразделениями");
                    }
                    DropdownStructUpdate();
                }
                else
                {
                    DisplayAlert("Выберите структурное подразделение");
                }
            }
        }

        protected void AddNewStructure_Click(object sender, EventArgs e)
        {
            EDMdbDataContext edmDb = new EDMdbDataContext();
            Struct structure = new Struct();
            structure.active = true;
            structure.name = NewStructureBox.Text;
            if (DropDownList1.SelectedItem.Text == "Выберите значение")
            {
                structure.fk_parent = 2;
            }
            else
            {
                structure.fk_parent = (from a in edmDb.Struct where a.name == DropDownList1.SelectedItem.Text select a.structID).FirstOrDefault();
            }   
            edmDb.Struct.InsertOnSubmit(structure);
            edmDb.SubmitChanges();
            DropdownStructUpdate();
            DisplayAlert("Структурное подразделение добавлено");
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

        protected void AddNewName_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Text != "Выберите значение")
            { 
            using (EDMdbDataContext edmDb = new EDMdbDataContext())
            {
                Struct structure =
                    (from a in edmDb.Struct
                     where a.name == DropDownList1.SelectedItem.Text
                     select a).FirstOrDefault();
                DisplayAlert("Название структурного подразделения " + structure.name + " было изменено на " + NewNameBox.Text);
                structure.name = NewNameBox.Text;                             
                edmDb.SubmitChanges();
            }
            DropdownStructUpdate();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/edmAdmin/AdminMain.aspx");
        }
    }
}
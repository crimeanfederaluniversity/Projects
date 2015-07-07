using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class Vvod_form : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CompetitionDBDataContext newField = new CompetitionDBDataContext();
            if(!Page.IsPostBack)
            {
                 List<Forms> checklist = (from a in newField.Forms where a.Active == true  select a).ToList();

                foreach (Forms n in checklist)
                {
                    ListItem item = new  ListItem();
                    item.Text = n.Name;
                    item.Value = n.ID_Form.ToString();
                    CheckBoxList1.Items.Add(item);
                }
                CheckBoxList1.DataBind();
            }
           
        }

     /*   protected void Button1_Click(object sender, EventArgs e)
        {
            CompetitionDBDataContext newField = new CompetitionDBDataContext();
            Fields pole = new Fields(); 
            pole.Text = TextBox1.Text;
            newField.Fields.InsertOnSubmit(pole);
            newField.SubmitChanges();

            foreach(ListItem currentItem in CheckBoxList1.Items)
            {
                if (currentItem.Selected == true )
                {
                    Form_CompetitionMapingTable formcomp = new Form_CompetitionMapingTable();
                formcomp.FK_Competition = Convert.ToInt32(currentItem.Value);
                formcomp.FK_Form = formcomp.ID_Form;
                newCompetition.Form_CompetitionMapingTable.InsertOnSubmit(formcomp);
                }
            }
            
            newCompetition.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Форма создана');", true);
        }
        */
        

        
    }
}
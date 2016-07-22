using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz.cntrlTemplates
{
    public partial class cntrl5 : System.Web.UI.Page
    {
        ControlTemplates.Template5 templ = new ControlTemplates.Template5();
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            templ.UserId=(userId);
            T5ListOfPrikazDropDownList.Items.AddRange(templ.GetDropDownListItems());
        }

        protected void T5CreateTableButton_Click(object sender, EventArgs e)
        {
            string startDateStr = T5StartDateTextBox.Text;
            string endDateStr = T5EndDateTextBox.Text;
            string registerIdStr = T5ListOfPrikazDropDownList.SelectedValue;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            int registerId = 0;
            DateTime.TryParse(startDateStr, out startDate);
            DateTime.TryParse(endDateStr, out endDate);
            Int32.TryParse(registerIdStr, out registerId);
            templ.SetStartParams(startDate, endDate, registerId);          
            T5ResultDiv.Controls.Clear();
            T5ResultDiv.Controls.Add(templ.GetResultTable());
        }
    }
}
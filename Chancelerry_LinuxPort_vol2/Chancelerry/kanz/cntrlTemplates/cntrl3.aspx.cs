using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz.cntrlTemplates
{
    public partial class cntrl3 : System.Web.UI.Page
    {
        readonly ControlTemplates.Template3 _template3 = new ControlTemplates.Template3();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int userId = 0;
                int.TryParse(Session["userID"].ToString(), out userId);

                if (userId == 0)
                {
                    Response.Redirect("~/Default.aspx");
                }
                _template3.Initiate(userId);
                T3ListOfIncomingDropDownList.Items.AddRange(_template3.GetDropDownValues());
            }
        }

        protected void T3CreateTableButton_Click(object sender, EventArgs e)
        {
            string startDateStr = T3StartDateTextBox.Text;
            string endDateStr = T3EndDateTextBox.Text;
            string registerIdStr = T3ListOfIncomingDropDownList.SelectedValue;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            int registerId = 0;
            DateTime.TryParse(startDateStr, out startDate);
            DateTime.TryParse(endDateStr, out endDate);
            Int32.TryParse(registerIdStr, out registerId);
            _template3.SetParams(startDate, endDate, registerId);
            T3ResultDiv.Controls.Clear();
            T3ResultDiv.Controls.Add(_template3.GetStatisticTable());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz.cntrlTemplates
{
    public partial class cntrl4 : System.Web.UI.Page
    {
        readonly ControlTemplates.Template4 _template4 = new ControlTemplates.Template4();
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
                _template4.Initiate(userId);
                T4ListOfIncomingDropDownList.Items.AddRange(_template4.GetDropDownValues());
            }
        }

        protected void T4CreateTableButton_Click(object sender, EventArgs e)
        {
            string startDateStr = T4StartDateTextBox.Text;
            string endDateStr = T4EndDateTextBox.Text;
            string registerIdStr = T4ListOfIncomingDropDownList.SelectedValue;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            int registerId = 0;
            DateTime.TryParse(startDateStr, out startDate);
            DateTime.TryParse(endDateStr, out endDate);
            Int32.TryParse(registerIdStr, out registerId);
            _template4.SetParams(startDate, endDate, registerId);
            T4ResultDiv.Controls.Clear();
            T4ResultDiv.Controls.Add(_template4.GetStatisticTable());
        }
    }
}
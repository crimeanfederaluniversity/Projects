using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz.cntrlTemplates
{
    public partial class cntrl2 : System.Web.UI.Page
    {
        readonly ControlTemplates.Template1 _template1 = new ControlTemplates.Template1();

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
                _template1.Initiate(userId);
                T1ListOfIncomingDropDownList.Items.AddRange(_template1.GetDropDownValues());
            }
        }

        protected void T1CreateTableButton_Click(object sender, EventArgs e)
        {
            string startDateStr = T1StartDateTextBox.Text;
            string endDateStr = T1EndDateTextBox.Text;
            string compareDateStr = T1CompareDateTextBox.Text;
            string registerIdStr = T1ListOfIncomingDropDownList.SelectedValue;

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            DateTime compareDate = DateTime.MaxValue;
            int registerId = 0;
            int viewType = 0;

            DateTime.TryParse(startDateStr, out startDate);
            DateTime.TryParse(endDateStr, out endDate);
            DateTime.TryParse(compareDateStr, out compareDate);
            Int32.TryParse(registerIdStr, out registerId);


            _template1.SetParams(startDate, endDate, compareDate, viewType, registerId);
            T1ResultDiv.Controls.Clear();
            T1ResultDiv.Controls.Add(_template1.GetResultTable());
            T1ResultDiv.Controls.Add(_template1.GetStatisticTable());
        }
    }
}
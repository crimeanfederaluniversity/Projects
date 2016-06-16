using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz.cntrlTemplates
{
    public partial class cntrl1 : System.Web.UI.Page
    {
        readonly ControlTemplates.Template2 _template2 = new ControlTemplates.Template2();

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
                _template2.Initiate(userId);
                T2ListOfIncomingDropDownList.Items.AddRange(_template2.GetDropDownValues());
            }
        }

        protected void T2CreateTableButton_Click(object sender, EventArgs e)
        {
            string startDateStr = T2CntlFilterStartDateTextBox.Text;
            string endDateStr = T2CntrlFilterEndDateTextBox.Text;
            string compareDateStr = T2CompareDateTextBox.Text;
            string registerIdStr = T2ListOfIncomingDropDownList.SelectedValue;
     
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            DateTime compareDate = DateTime.MaxValue;
            int registerId = 0;
            int viewType = 0;

            DateTime.TryParse(startDateStr, out startDate);
            DateTime.TryParse(endDateStr, out endDate);
            DateTime.TryParse(compareDateStr, out compareDate);
            Int32.TryParse(registerIdStr, out registerId);



            _template2.SetParams(startDate, endDate, compareDate, viewType, registerId);
            T2ResultDiv.Controls.Clear();
            T2ResultDiv.Controls.Add(_template2.GetResultTable());
            T2ResultDiv.Controls.Add(_template2.GetStatisticTable());
        }
    }
}
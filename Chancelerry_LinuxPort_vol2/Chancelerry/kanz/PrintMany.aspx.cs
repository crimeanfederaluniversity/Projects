using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class PrintMany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<PrintManyParams> allParams = (List<PrintManyParams>) Session["PrintManyParams"];
            CardCreateView cardCreateView = new CardCreateView();
            int i = 0;
            foreach (PrintManyParams page in allParams)
            {               
                PrintMainDiv.Controls.Add(cardCreateView.GetPrintVersion(page.RegisterId,page.CardId, page.Version, Convert.ToInt32(DropDownList1.SelectedValue)));
                if (++i<allParams.Count)
                    PrintMainDiv.Controls.Add(new Panel() {CssClass = "pagebreak" });
            }        
        }
    }
}
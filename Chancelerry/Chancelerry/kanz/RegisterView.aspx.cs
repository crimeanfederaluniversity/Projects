using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class RegisterView : System.Web.UI.Page
    {

        public class DataOne
        {
            public string textValue { get; set; }
            public int instance { get; set; }
            public int version { get; set; }
            public bool deleted { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            /////////////////////////////////////////////////////////////////////

            var regId = Session["registerID"];
            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();
            TableActions ta = new TableActions();

            var register = 
                (from r in dataContext.Registers
                 where r.registerID == Convert.ToInt32(regId)
                 select r).FirstOrDefault();

            
            if (register != null)
            {
                RegisterNameLabel.Text = register.name;
                ta.RefreshTable(dataContext, userID, register, regId, dataTable);
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["cardID"] = 0;
            Session["version"] = 100500;
            Response.Redirect("CardEdit.aspx");
        }
    }
}
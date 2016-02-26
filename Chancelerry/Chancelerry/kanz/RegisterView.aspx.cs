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


        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                ViewState["userID"] = userID;
            }

            /////////////////////////////////////////////////////////////////////

            var regId = Session["registerID"];
            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();
            TableActions ta = new TableActions();

            var register = 
                (from r in dataContext.Registers
                 where r.registerID == Convert.ToInt32(regId)
                 select r).FirstOrDefault();


            if (!Page.IsPostBack)
            {
                if (register != null)
                {
                    RegisterNameLabel.Text = register.name;
                    ta.RefreshTable(dataContext, userID, register, regId, dataTable, new List<TableActions.SearchValues>());
                    TableActions.DTable = dataTable;
                }
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["cardID"] = 0;
            Session["version"] = 100500;
            Response.Redirect("CardEdit.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            Table table = TableActions.DTable;
            List<TableActions.SearchValues> searchList = new List<TableActions.SearchValues>();
        
            // Проходимся по таблице и ищем "поисковые" TextBox'ы 
            foreach (TableRow tr in table.Rows)
            {
                foreach (Control c in from TableCell tc in tr.Cells from Control c in tc.Controls where c.GetType() == typeof (TextBox) select c)
                {
                    var tbox = (TextBox) c;
                    // Делаем запрос, и если  в этом текстбохе в Text что-то есть
                    if (Request.Form[((TextBox) c).UniqueID].Any())
                    {
                        // добавляем объект поиска со значениями id поля (берем из аттрибута TextBox'а и само значение Text)
                        searchList.Add(new TableActions.SearchValues()
                        {
                            fieldId = Convert.ToInt32(tbox.Attributes["_fieldID4search"]),
                            value = Request.Form[((TextBox) c).UniqueID]
                        });
                    }
                }
            }


            var regId = Session["registerID"];
            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();
            TableActions ta = new TableActions();

            var register =
                (from r in dataContext.Registers
                    where r.registerID == Convert.ToInt32(regId)
                    select r).FirstOrDefault();

            ta.RefreshTable(dataContext, Convert.ToInt32(ViewState["userID"]), register, regId, dataTable,
                searchList);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("TableSettings.aspx");
        }
    }
    
}
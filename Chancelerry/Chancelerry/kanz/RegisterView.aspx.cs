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
        bool vVersion = true;
        int page = 0;
        private void RedirectToEdit(object sender, EventArgs e)
        {

            ImageButton thisButton = (ImageButton)sender;
            int currentCardId = Convert.ToInt32(thisButton.Attributes["_cardID"]);
            HttpContext.Current.Response.Redirect("https://www.ya.ru/", true);
        }

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


            if (register != null)
            {
                RegisterNameLabel.Text = register.name;


                if (vVersion)
                {
                    int size = 100;
                    if (Request.QueryString["page"] != null)
                        Int32.TryParse(Request.QueryString["page"], out page);

                    Dictionary<int, string> vSearchList = (Dictionary<int, string>)Session["vSearchList"];
                    CardCommonFunctions cardCommonFunctions = new CardCommonFunctions();

                    string sum = cardCommonFunctions.FastSearch(vSearchList, register.registerID, Convert.ToInt32(userID), dataTable, page * size, (page + 1) * size);
                    Button5.Visible = true;
                    Button6.Visible = true;
                    Button7.Visible = false;
                    Button8.Visible = false;
                    BottomButton9.Visible = false;
                    BottomButton10.Visible = true;
                    BottomButton11.Visible = true;
                    BottomButton12.Visible = false;
                    string page_info = sum;
                    PageNumberLabel.Text = page_info;
                    BottomPageNumberLabel.Text = page_info;

                }
                else
                {
                    ta.RefreshTable(dataContext, Convert.ToInt32(ViewState["userID"]), register, regId, dataTable, (Dictionary<int, string>)Session["vSearchList"]);
                    TableActions.DTable = dataTable;


                    string page_info = "Cтраница: " + ((int)Session["pageCntrl"] + 1).ToString() + "/" + (int)Session["pageCount"] + ". Всего: " + (int)Session["cardsCount"];
                    PageNumberLabel.Text = page_info;
                    BottomPageNumberLabel.Text = page_info;

                }
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
           // Session["pageCntrl"] = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["canEdit"] = true;
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
            if (vVersion)
            {
                table = dataTable;
            }
            List<TableActions.SearchValues> searchList = new List<TableActions.SearchValues>();
            Dictionary<int,string> vSearchDict = new Dictionary<int, string>();
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
                        vSearchDict.Add(Convert.ToInt32(tbox.Attributes["_fieldID4search"]), Request.Form[((TextBox)c).UniqueID]);

                        /*
                        searchList.Add(new TableActions.SearchValues()
                        {
                            fieldId = Convert.ToInt32(tbox.Attributes["_fieldID4search"]),
                            value = Request.Form[((TextBox) c).UniqueID]
                        });
                        */
                    }
                }
            }

            // По сессии передаем searchList и перезагружаем страницу
            //Session["searchList"] = searchList;
            Session["vSearchList"] = vSearchDict;
            Response.Redirect("RegisterView.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("TableSettings.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["vSearchList"] = null;
            Session["searchList"] = new List<TableActions.SearchValues>();
            Response.Redirect("RegisterView.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (vVersion)
            {
                Response.Redirect("RegisterView.aspx?page=" + (page + 1));
            }
            Session["pageCntrl"] = (int)Session["pageCntrl"]+1;
            Response.Redirect("RegisterView.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (vVersion)
            {
                if (page > 0)
                {
                    Response.Redirect("RegisterView.aspx?page=" + (page - 1));
                }
            }
            if ((int) Session["pageCntrl"] > 0)
            {
                Session["pageCntrl"] = (int) Session["pageCntrl"] - 1;
                Response.Redirect("RegisterView.aspx");
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (vVersion)
            {
                Response.Redirect("RegisterView.aspx?page=0");
            }
            Session["pageCntrl"] = 0;
            Response.Redirect("RegisterView.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            if (vVersion)
            {
                if (page > 0)
                {
                    Response.Redirect("RegisterView.aspx?page=" + (page - 1));
                }
            }
            Session["pageCntrl"] = (int)Session["pageCount"]-1;
            Response.Redirect("RegisterView.aspx");
        }
    }
    
}
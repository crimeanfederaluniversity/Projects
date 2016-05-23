using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class RegisterView : System.Web.UI.Page
    {
        public int page = 0;
        public int size = 10;
        public int sortFieldId = 0;
        ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected ListItem[] GetListOfColumns(int registerId, int userId)
        {
            List<ListItem> listToReturn = new List<ListItem>();
            listToReturn.Add(new ListItem() { Text = "По умолчанию", Value = "0" });
            listToReturn.AddRange(
                (from a in dataContext.Fields
                 where a.Active == true
                 join b in dataContext.RegistersView
                     on a.FieldID equals b.FkField
                 where b.Active == true
                 join c in dataContext.RegistersUsersMap
                     on b.FkRegistersUsersMap equals c.RegistersUsersMapID
                 where c.FkUser == userId && c.FkRegister == registerId && c.Active
                 select a).Distinct().ToList().Select(mc => new ListItem() { Value = mc.FieldID.ToString(), Text = mc.Name }).ToList());
            return listToReturn.ToArray();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            #region инициализация сессии пользователя и выбранного регистра
            //timeStampsLabel.Text = " 0_" + DateTime.Now.TimeOfDay;
            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                ViewState["userID"] = userId;
            }
            /////////////////////////////////////////////////////////////////////
            int regId;
            Int32.TryParse(Session["registerID"].ToString(), out regId);
            TableActions ta = new TableActions();
            var register =
                (from r in dataContext.Registers
                 where r.RegisterID == regId
                 select r).FirstOrDefault();
            #endregion
            if (register != null)
            {
                #region инициализация сессий поисков и get и списков
                if (!Page.IsPostBack)
                {
                    ChooseSortFieldIdDropDownList.Items.AddRange(GetListOfColumns(register.RegisterID, userId));
                }
                RegisterNameLabel.Text = register.Name;
                if (Request.QueryString["page"] != null)
                    Int32.TryParse(Request.QueryString["page"], out page);
                if (Request.QueryString["size"] != null)
                    Int32.TryParse(Request.QueryString["size"], out size);
                if (Request.QueryString["sortFieldId"] != null)
                    Int32.TryParse(Request.QueryString["sortFieldId"], out sortFieldId);

                Dictionary<int, string> vSearchList = (Dictionary<int, string>)Session["vSearchList"];
                string searchAll = (string)Session["vSearchAll"];
                string searchCardId = (string)Session["vSearchById"];
                string searchAllExtended = (string)Session["vSearchAllWithParams"];
                #endregion
                CardCommonFunctions cardCommonFunctions = new CardCommonFunctions();
                int totalCnt = 0;
                string sum = cardCommonFunctions.FastSearch(sortFieldId , searchAllExtended, searchCardId, vSearchList, searchAll, register.RegisterID, Convert.ToInt32(userId), dataTable, page * size, (page + 1) * size, out totalCnt);
                #region инициализация кнопок и инфо
                //timeStampsLabel.Text += cardCommonFunctions.timeStamps;
                int pagesCnt = totalCnt / size;
                string pageNumbers = "";

                if (totalCnt % size > 0) pagesCnt++;

                int startNumber = page - 5;
                startNumber = startNumber < 0 ? 0 : startNumber;
                
                for (int i = startNumber; i < 10 + startNumber; i++)
                {
                    if (i > pagesCnt - 1)
                        continue;
                    if (i == page)
                    {
                        pageNumbers += "<font color='red'> " + (i + 1).ToString() + " </font>";
                    }
                    else

                    {
                        pageNumbers += "<a href=\"?page=" + i + "&size=" + size + "\"> " + (i + 1).ToString() + " </a>";
                    }

                }
                
                GoToFirstTop.OnClientClick = "window.open(location.origin+location.pathname+'?page=0&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                GoToFirstBottom.OnClientClick = "window.open(location.origin+location.pathname+'?page=0&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                if (page == 0)
                {
                    GoToPreviousTop.Enabled = false;
                    GoToPreviousBottom.Enabled = false;
                }
                else
                {
                    GoToPreviousTop.OnClientClick = "window.open(location.origin+location.pathname+'?page=" + (page - 1) + "&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                    GoToPreviousBottom.OnClientClick = "window.open(location.origin+location.pathname+'?page=" + (page - 1) + "&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                }
                if (page == pagesCnt - 1)
                {
                    GoToNextTop.Enabled = false;
                    GoToNextBottom.Enabled = false;
                }
                else
                {
                    GoToNextTop.OnClientClick = "window.open(location.origin+location.pathname+'?page=" + (page + 1) + "&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                    GoToNextBottom.OnClientClick = "window.open(location.origin+location.pathname+'?page=" + (page + 1) + "&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                }
                GoToLastTop.OnClientClick = "window.open(location.origin+location.pathname+'?page=" + (pagesCnt - 1) + "&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";
                GoToLastBottom.OnClientClick = "window.open(location.origin+location.pathname+'?page=" + (pagesCnt - 1) + "&size=" + size + "&sortFieldId=" + sortFieldId + "','_self'); return false;";

                string page_info = sum;

                PagesListTop.Text = pageNumbers;
                PagesListBottom.Text = pageNumbers;

                PageInfoTop.Text = page_info;
                PageInfoBottom.Text = page_info;
                #endregion
            }
            //timeStampsLabel.Text += " 7_" + DateTime.Now.TimeOfDay;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string searchAll = (string)Session["vSearchAll"];
            SearchAllTextBox.Text = searchAll;

            string searchById = (string)Session["vSearchById"];
            SearchByIdTextbox.Text = searchById;

            string searchAllWithParams = (string)Session["vSearchAllWithParams"];
            SearchAllTextBoxExtended.Text = searchAllWithParams;

            ChooseSortFieldIdDropDownList.SelectedValue = sortFieldId.ToString();
            CardsOnPageDropDownList.SelectedValue = size.ToString();

           // timeStampsLabel.Text += " 8_" + DateTime.Now.TimeOfDay;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["canEdit"] = true;
            Session["cardID"] = 0;
            Session["version"] = 200500;
            Response.Redirect("CardEdit.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)

        {
            Search();
        }
        private void Search()
        {
            Table table = TableActions.DTable;
            table = dataTable;
            List<TableActions.SearchValues> searchList = new List<TableActions.SearchValues>();
            Dictionary<int, string> vSearchDict = new Dictionary<int, string>();
            // Проходимся по таблице и ищем "поисковые" TextBox'ы 
            foreach (TableRow tr in table.Rows)
            {
                foreach (Control c in from TableCell tc in tr.Cells from Control c in tc.Controls where c.GetType() == typeof(TextBox) select c)
                {
                    var tbox = (TextBox)c;
                    // Делаем запрос, и если  в этом текстбохе в Text что-то есть
                    if (Request.Form[((TextBox)c).UniqueID].Any())
                    {
                        // добавляем объект поиска со значениями id поля (берем из аттрибута TextBox'а и само значение Text)
                        vSearchDict.Add(Convert.ToInt32(tbox.Attributes["_fieldID4search"]), Request.Form[((TextBox)c).UniqueID]);
                    }
                }
            }

            // По сессии передаем searchList и перезагружаем страницу
            //Session["searchList"] = searchList;
            Session["vSearchById"] = null;
            Session["vSearchAll"] = null;
            Session["vSearchAllWithParams"] = null;
            SearchAllTextBox.Text = "";
            Session["vSearchList"] = vSearchDict;
            Response.Redirect("RegisterView.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("TableSettings.aspx");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["vSearchById"] = null;
            Session["vSearchList"] = null;
            Session["vSearchAll"] = null;
            Session["vSearchAllWithParams"] = null;
            Session["searchList"] = new List<TableActions.SearchValues>();
            Response.Redirect("RegisterView.aspx");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterView.aspx?page=" + (page + 1) + "&size=" + size + "&sortFieldId=" + sortFieldId);
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                Response.Redirect("RegisterView.aspx?page=" + (page - 1) + "&size=" + size + "&sortFieldId=" + sortFieldId);
            }

        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterView.aspx?page=0" + "&size=" + size + "&sortFieldId=" + sortFieldId);
        }
        protected void Button8_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                Response.Redirect("RegisterView.aspx?page=" + (page - 1) + "&size=" + size + "&sortFieldId=" + sortFieldId);
            }
        }
        protected void SearchAllButton_Click(object sender, EventArgs e)
        {
            Session["vSearchById"] = null;
            Session["vSearchList"] = null;
            Session["vSearchAllWithParams"] = null;
            Session["vSearchAll"] = SearchAllTextBox.Text;
            Response.Redirect("RegisterView.aspx");
        }
        protected void SearchById_Click(object sender, EventArgs e)
        {
            Session["vSearchById"] = SearchByIdTextbox.Text;
            Session["vSearchList"] = null;
            Session["vSearchAll"] = null;
            Session["vSearchAllWithParams"] = null;
            Response.Redirect("RegisterView.aspx");
        }
        protected void OpenByIdButton_Click(object sender, EventArgs e)
        {
            string textboxValue = OpenByIdTextBox.Text;
            if (textboxValue != null)
            {
                if (textboxValue.Length > 0)
                {
                    int cardId = 0;
                    Int32.TryParse(textboxValue, out cardId);
                    if (cardId != 0)
                    {
                        ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
                        int regId;
                        Int32.TryParse(Session["registerID"].ToString(), out regId);
                        CollectedCards cardIdReal = (from a in dataContext.CollectedCards
                                                     where a.FkRegister == regId
                                                           && a.MaInFieldID == cardId
                                                           && a.Active
                                                     select a).FirstOrDefault();
                        if (cardIdReal != null)
                        {
                            Session["canEdit"] = true;
                            Session["cardID"] = cardIdReal.CollectedCardID;
                            Session["version"] = 200500;
                            Response.Redirect("CardEdit.aspx");
                        }
                    }
                }
            }
        }
        protected void SearchAllExtendedButton_Click(object sender, EventArgs e)
        {
            Session["vSearchById"] = null;
            Session["vSearchList"] = null;
            Session["vSearchAllWithParams"] = SearchAllTextBoxExtended.Text;
            Session["vSearchAll"] = null;
            Response.Redirect("RegisterView.aspx");
        }
        protected void ChooseSortFieldIdDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32.TryParse(ChooseSortFieldIdDropDownList.SelectedValue, out sortFieldId);
            Response.Redirect("RegisterView.aspx?page=" + page + "&size=" + size + "&sortFieldId=" + sortFieldId);
        }
        protected void CardsOnPageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32.TryParse(CardsOnPageDropDownList.SelectedValue, out size);
            Response.Redirect("RegisterView.aspx?page=0&size=" + size + "&sortFieldId=" + sortFieldId);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Chancelerry.Account;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class StatisticsMain : System.Web.UI.Page
    {
        CardCommonFunctions main = new CardCommonFunctions();
        private ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId=0;
            int.TryParse(Session["userID"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                EndDateTextBox.Attributes.Add("onfocus", "this.select();lcs(this)");
                EndDateTextBox.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");

                StartDateTextBox.Attributes.Add("onfocus", "this.select();lcs(this)");
                StartDateTextBox.Attributes.Add("onclick", "event.cancelBubble=true;this.select();lcs(this)");

                List<Registers> allRegisters = main.GetAllRegistersForUser(userId);
                foreach (Registers reg in allRegisters)
                {
                    RegistersDropoDownList.Items.Add(new ListItem() { Value = reg.RegisterID.ToString(), Text = reg.Name });
                }
            }

        }
        protected void SumByRegistersDropoDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiedsDropDownList.Items.Clear();
            FilterFiedsDropDownList.Items.Clear();
            DateForFilter.Items.Clear();
            SumByResultLabel.Text = "";
            int selectedId = 0;
            Int32.TryParse(RegistersDropoDownList.SelectedValue, out selectedId);
            Registers currentRegister = main.GetRegisterById(selectedId);
            List<FieldsGroups> fieldsGroupInRegister =
                main.GetFieldsGroupsInRegisterModelOrderByLine(currentRegister.FkRegistersModel);
            List<Fields> allFields = new List<Fields>();
            foreach (FieldsGroups fieldGroup in fieldsGroupInRegister)
            {
                List<Fields> tmp = main.GetFieldsInFieldGroupOrderByLine(fieldGroup.FieldsGroupID);
                foreach (Fields curTmp in tmp)
                {
                    allFields.Add(curTmp);
                }
            }
            foreach (Fields field in allFields)
            {
                FiedsDropDownList.Items.Add(new ListItem() {Value = field.FieldID.ToString(),Text = field.Name});
                FilterFiedsDropDownList.Items.Add(new ListItem() { Value = field.FieldID.ToString(), Text = field.Name });
            }
            foreach (Fields field in allFields)
            {
                if ((field.Type == "date" || field.Type == "autoDate") && field.Multiple==false && (from a in fieldsGroupInRegister where a.FieldsGroupID == field.FkFieldsGroup select a).FirstOrDefault().Multiple==false)
                DateForFilter.Items.Add(new ListItem() { Value = field.FieldID.ToString(), Text = field.Name });
            }

            FilterFiedsDropDownList.Enabled = true;
            FiedsDropDownList.Enabled = true;
            DateForFilter.Enabled = true;
            SumByButton.Enabled = true;
            CreateTableButton.Enabled = true;

        }       
        protected void SumByButton_Click(object sender, EventArgs e)
        {
            int datefieldId = 0;
            Int32.TryParse(DateForFilter.SelectedValue, out datefieldId);

            int fieldId = 0;
            Int32.TryParse(FiedsDropDownList.SelectedValue, out fieldId);

            int fieldToSerachIn = 0;
            Int32.TryParse(FilterFiedsDropDownList.SelectedValue, out fieldToSerachIn);

            DateTime startDate = DateTime.MinValue;
            DateTime.TryParse(StartDateTextBox.Text, out startDate);

            DateTime endDate = DateTime.MaxValue;
            DateTime.TryParse(EndDateTextBox.Text, out endDate);

            string valueToSearch = "";
            valueToSearch = SearchInFieldTextBox.Text;

            int registerId = 0;
            Int32.TryParse(RegistersDropoDownList.SelectedValue, out registerId);
            StatisticsClass stClass = new StatisticsClass();
            List<CollectedFieldsValues> resultList = stClass.GetAllCollectedForFieldInDateRange(registerId,fieldId, datefieldId , startDate, endDate, fieldToSerachIn, valueToSearch);

            decimal sum = 0;
            foreach (CollectedFieldsValues current in resultList)
            {
                decimal tmp = 0;
                decimal.TryParse(current.ValueText, out tmp);
                sum += tmp;
            }
            SumByResultLabel.Text = "Сумма: "+sum.ToString();
        }
        protected void CreateTableButton_Click(object sender, EventArgs e)
        {
            int datefieldId = 0;
            Int32.TryParse(DateForFilter.SelectedValue, out datefieldId);

            int fieldId = 0;
            Int32.TryParse(FiedsDropDownList.SelectedValue, out fieldId);

            int fieldToSerachIn = 0;
            Int32.TryParse(FilterFiedsDropDownList.SelectedValue, out fieldToSerachIn);

            DateTime startDate = DateTime.MinValue;
            DateTime.TryParse(StartDateTextBox.Text, out startDate);

            DateTime endDate = DateTime.MaxValue;
            DateTime.TryParse(EndDateTextBox.Text, out endDate);

            string valueToSearch = "";
            valueToSearch = SearchInFieldTextBox.Text;

            int registerId = 0;
            Int32.TryParse(RegistersDropoDownList.SelectedValue, out registerId);
            //Получили все параметры со страницы
            StatisticsClass stClass = new StatisticsClass();
            List<CollectedFieldsValues> resultList = stClass.GetAllCollectedForFieldInDateRange(registerId, fieldId, datefieldId, startDate, endDate,fieldToSerachIn,valueToSearch);

            List<int> cardIds = (from a in resultList select a.FkCollectedCard).Distinct().ToList();
            Session["cardsIds"] = cardIds;
            List<string> uniqueValues = (from a in resultList orderby a.ValueText.Trim() select a.ValueText.Trim()).Distinct().ToList();

            Table resultTable = new Table() {CssClass = "resultTable" };
            resultTable.Rows.Add(new TableRow() { Cells = {new TableCell() {Text = "Название"}, new TableCell() {Text = "Кол-во" }, new TableCell() { Text = "Список номеров"  } } });
            int sum = 0;

            

            foreach (string current in uniqueValues)
            {
                List<CollectedFieldsValues> res = (from a in resultList where a.ValueText.Trim() == current select a).ToList();
                //List<string> = res.Select(mc=>mc.)
                List<CollectedCards> cards = new List<CollectedCards>();
                foreach (CollectedFieldsValues tmp in res)
                {
                    cards.Add(main.GetCardById(tmp.FkCollectedCard));
                }
                string cardIdsText = String.Join(",", cards.Select(mc => mc.MaInFieldID).ToList());
                int count = res.Count();
                sum += count;
                resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = current }, new TableCell() { Text = count.ToString()}, new TableCell() { Text = cardIdsText } } });
            }
            resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = "Итого" }, new TableCell() { Text = sum.ToString() } } });
            resultDiv.Controls.Add(resultTable);
        }

        protected void PrintFinded_Click(object sender, EventArgs e)
        {
            List<int> allCards = (List<int>) Session["cardsIds"];
            if (allCards == null)
                return;
            List<PrintManyParams> printParams = new List<PrintManyParams>();
            foreach (int cardId in allCards)
            {
                CollectedCards card = main.GetCardById(cardId);              
                printParams.Add(new PrintManyParams() {CardId = cardId,RegisterId = card.FkRegister,Version = main.GetLastVersionByCard(cardId)});
            }
            Session["PrintManyParams"] = printParams;
            Response.Redirect("PrintMany.aspx");
        }
    }
}
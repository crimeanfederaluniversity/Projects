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
            }
            foreach (Fields field in allFields)
            {
                if ((field.Type == "date" || field.Type == "autoDate") && field.Multiple==false && (from a in fieldsGroupInRegister where a.FieldsGroupID == field.FkFieldsGroup select a).FirstOrDefault().Multiple==false)
                DateForFilter.Items.Add(new ListItem() { Value = field.FieldID.ToString(), Text = field.Name });
            }
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

            DateTime startDate = DateTime.MinValue;
            DateTime.TryParse(StartDateTextBox.Text, out startDate);

            DateTime endDate = DateTime.MaxValue;
            DateTime.TryParse(EndDateTextBox.Text, out endDate);

            int registerId = 0;
            Int32.TryParse(RegistersDropoDownList.SelectedValue, out registerId);
            StatisticsClass stClass = new StatisticsClass();
            List<CollectedFieldsValues> resultList = stClass.GetAllCollectedForFieldInDateRange(registerId,fieldId, datefieldId , startDate, endDate);

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

            DateTime startDate = DateTime.MinValue;
            DateTime.TryParse(StartDateTextBox.Text, out startDate);

            DateTime endDate = DateTime.MaxValue;
            DateTime.TryParse(EndDateTextBox.Text, out endDate);

            int registerId = 0;
            Int32.TryParse(RegistersDropoDownList.SelectedValue, out registerId);
            StatisticsClass stClass = new StatisticsClass();
            List<CollectedFieldsValues> resultList = stClass.GetAllCollectedForFieldInDateRange(registerId, fieldId, datefieldId, startDate, endDate);

            List<string> uniqueValues = (from a in resultList orderby a.ValueText.Trim() select a.ValueText.Trim()).Distinct().ToList();

            Table resultTable = new Table();
            resultTable.Rows.Add(new TableRow() { Cells = {new TableCell() {Text = "Название"}, new TableCell() {Text = "Кол-во"}} });

            foreach (string current in uniqueValues)
            {
                resultTable.Rows.Add(new TableRow() { Cells = { new TableCell() { Text = current }, new TableCell() { Text = (from a in resultList where a.ValueText.Trim() == current select a).Count().ToString() } } });
            }
            resultDiv.Controls.Add(resultTable);
        }
    }
}
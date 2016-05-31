using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class ChooseDictionary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                ChancelerryDb chancDb =
                 new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
                List<Dictionarys> dictionaryse = (from a in chancDb.Dictionarys
                    where a.Active == true
                    select a).ToList();
                DictionarysList.Items.Clear();
                foreach (Dictionarys currentDictionary in dictionaryse)
                {
                    ListItem tmpListItem = new ListItem();
                    tmpListItem.Text = currentDictionary.Name;
                    tmpListItem.Value = currentDictionary.DictionaryID.ToString();
                    DictionarysList.Items.Add(tmpListItem);
                }
            }
            divForTable.Controls.Clear();
            divForTable.Controls.Add(CreateTable(Convert.ToInt32(DictionarysList.SelectedValue)));
            
        }
        
        public void DelValueButtonClick(object sender, EventArgs e)
        {
            
            ImageButton delButton = (ImageButton) sender;
            int id = Convert.ToInt32(delButton.CommandArgument);
            ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

            DictionarysValues currentValues = (from a in chancDb.DictionarysValues
                where a.DictionaryValueID == id
                select a).FirstOrDefault();
            if (currentValues != null)
            {
                currentValues.Active = false;
                chancDb.SubmitChanges();
            }
            divForTable.Controls.Clear();
            divForTable.Controls.Add(CreateTable(Convert.ToInt32(DictionarysList.SelectedValue)));
        }
        public void SaveValueButtonClick(object sender, EventArgs e)
        {
            ImageButton saveButton = (ImageButton)sender;
            int id = Convert.ToInt32(saveButton.CommandArgument);
            ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            DictionarysValues currentValues = (from a in chancDb.DictionarysValues
                                               where a.DictionaryValueID == id
                                               select a).FirstOrDefault();
            if (currentValues != null)
            {
                TextBox myTextBox = (TextBox)divForTable.FindControl("dictValue_" + id);
                currentValues.Value = myTextBox.Text;
                chancDb.SubmitChanges();
            }
            divForTable.Controls.Clear();
            divForTable.Controls.Add(CreateTable(Convert.ToInt32(DictionarysList.SelectedValue)));
        }

        protected void AddValue_Click(object sender, ImageClickEventArgs e)
        {
            ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            DictionarysValues newValue = new DictionarysValues();
            newValue.Active = true;
            newValue.FkDictionary = Convert.ToInt32(DictionarysList.SelectedValue);
            newValue.Value = " ";
            chancDb.DictionarysValues.InsertOnSubmit(newValue);
            chancDb.SubmitChanges();
            divForTable.Controls.Clear();
            divForTable.Controls.Add(CreateTable(Convert.ToInt32(DictionarysList.SelectedValue)));
        }

        public Table CreateTable(int dictionaryId)
        {
            Table tableToReturn = new Table();
            ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            List<DictionarysValues> allValues = (from a in chancDb.DictionarysValues
                where a.Active == true
                      && a.FkDictionary == dictionaryId
                select a).Distinct().ToList();
            foreach (DictionarysValues currentValue in allValues)
            {
                TableRow currentTableRow = new TableRow();
                {
                    TableCell currentTableCell1 = new TableCell();
                    {
                        currentTableCell1.Width = 600;
                        TextBox valueTextBox = new TextBox();                    
                        valueTextBox.ID = "dictValue_" + currentValue.DictionaryValueID.ToString();
                        valueTextBox.Attributes.Add("name", valueTextBox.ID);
                        valueTextBox.Width = 600;      
                        valueTextBox.Style.Add("max-width", "100%");           
                        valueTextBox.Text = currentValue.Value;
                        currentTableCell1.Controls.Add(valueTextBox);
                    }
                    TableCell currentTableCell2 = new TableCell();
                    {
                        ImageButton deleteButton = new ImageButton();
                        deleteButton.ImageUrl = "~/kanz/icons/delButtonIcon.jpg";
                        deleteButton.Height = 20;
                        deleteButton.Width = 20;
                        deleteButton.CommandArgument = currentValue.DictionaryValueID.ToString();
                        deleteButton.Click += DelValueButtonClick;
                        currentTableCell2.Controls.Add(deleteButton);

                    }
                    TableCell currentTableCell3 = new TableCell();
                    {
                        ImageButton saveButton = new ImageButton();
                        saveButton.ImageUrl = "~/kanz/icons/saveButtonIcon.jpg";
                        saveButton.Height = 20;
                        saveButton.Width = 20;
                        saveButton.CommandArgument = currentValue.DictionaryValueID.ToString();
                        saveButton.Click += SaveValueButtonClick;
                        currentTableCell3.Controls.Add(saveButton);
                    }
                    currentTableRow.Cells.Add(currentTableCell1);
                    currentTableRow.Cells.Add(currentTableCell2);
                    currentTableRow.Cells.Add(currentTableCell3);
                }
                tableToReturn.Rows.Add(currentTableRow);
            }

            return tableToReturn;
        } 


        protected void DictionarysList_SelectedIndexChanged(object sender, EventArgs e)
        {
            divForTable.Controls.Clear();
            divForTable.Controls.Add(CreateTable(Convert.ToInt32(DictionarysList.SelectedValue)));
        }
        
    }
}
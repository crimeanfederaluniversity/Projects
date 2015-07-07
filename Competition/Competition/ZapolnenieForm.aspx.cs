using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace Competition
{
    public partial class ZapolnenieForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack))
            {
                CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
                List<Competitions> comp = (from a in newCompetition.Competitions where a.Active == true select a).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите конкурс");
                foreach (Competitions n in comp)
                {
                dictionary.Add(n.ID_Competition, n.Name);
                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
              
                }
                
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.Visible = true;
            CompetitionDBDataContext newField = new CompetitionDBDataContext();
            CompetitionDBDataContext newValue = new CompetitionDBDataContext();
            List<Fields> table = (from a in newField.Fields where a.Active == true select a).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Наименование статей", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Сумма", typeof(string)));

            foreach (var n in table)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["Наименование статей"] = n.Text;

                dataTable.Rows.Add(dataRow);
            }
             GridView1.DataSource = dataTable;
             GridView1.DataBind();
            }


              protected void GridviewValues_RowDataBound(object sender, GridViewRowEventArgs e)
              {
              }
           
        }
    }

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class ShowRegisteAndPrint : System.Web.UI.Page
    {
        readonly CardCommonFunctions main = new CardCommonFunctions();
       // readonly ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                RegistersDropoDownList.Items.Add(new ListItem() { Value = "-1", Text = "Выберите регистр" });
                List<Registers> allRegisters = main.GetAllRegistersForUser(userId);
                foreach (Registers reg in allRegisters)
                {
                    RegistersDropoDownList.Items.Add(new ListItem() { Value = reg.RegisterID.ToString(), Text = reg.Name });
                }
            }
        }

        public Table CreateTableWithListOfFields(int registerModelId)
        {
            Table tableToReturn = new Table();
          /*  List<Fields> allActiveFields = (from a in dataContext.Fields
                where a.Active == true
                join b in dataContext.FieldsGroups
                    on a.FkFieldsGroup equals b.FieldsGroupID
                where b.Active == true
                      && b.FkRegisterModel == registerModelId
                select a).Distinct().ToList();*/
            return tableToReturn;
        }

        protected void RegistersDropoDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GenerateResultTableButton_Click(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class Dashboard : System.Web.UI.Page
    {

        public class DataForGv
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////////
            
            ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            // Забираем только прикрепленные реестры в  RegUsrMap для данного пользователя и проверяем на активность в Registers.
             List<DataForGv> registers = (from rum in dataContext.RegistersUsersMap
                             join reg in dataContext.Registers on rum.FkRegister equals reg.RegisterID
                             where reg.Active && rum.Active && rum.FkUser == (int)userID
                             select new DataForGv(){ Id = reg.RegisterID, Name = reg.Name }).ToList() ;

            GridViewRegisters.DataSource = registers;

            BoundField boundField = new BoundField();
            boundField.DataField = "Id";
            boundField.HeaderText = "ID";
            boundField.Visible = false;
            GridViewRegisters.Columns.Add(boundField);

            ButtonField coluButtonField = new ButtonField();
            coluButtonField.DataTextField = "Name";
            coluButtonField.HeaderText = "Реестры документов";
            coluButtonField.ButtonType = ButtonType.Link;
            coluButtonField.CommandName = "Link";
            
            GridViewRegisters.Columns.Add(coluButtonField);
            DataBind();
        }

        protected void GridViewRegisters_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            int userId;
            Int32.TryParse(Session["userID"].ToString(), out userId); // Port

            switch (e.CommandName)
            {
                case "Link":
                {
                     List<DataForGv> registers = (from rum in dataContext.RegistersUsersMap
                                         join reg in dataContext.Registers on rum.FkRegister equals reg.RegisterID

                                         where reg.Active && rum.Active && rum.FkUser == userId
                                                  select new DataForGv() { Id = reg.RegisterID, Name = reg.Name }).ToList();

                        // CommandArgument - номер строки.
                            Session["registerID"] = registers[Convert.ToInt32(e.CommandArgument)].Id;
                    Session["searchList"] = new List<TableActions.SearchValues>();
                        Session["vSearchList"] = null;
                        Response.Redirect("RegisterView.aspx");
                }
                    break;
            }
        }

        protected void DictionaryEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseDictionary.aspx");
        }

        protected void GoToStatistics_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatisticsMain.aspx");
        }

        protected void GoToControl_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseCntrlTempl.aspx");
        }

        protected void GoToShowResterAndPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowRegisteAndPrint.aspx");
        }
    }
}
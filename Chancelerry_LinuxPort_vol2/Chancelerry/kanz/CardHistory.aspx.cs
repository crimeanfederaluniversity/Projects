using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class CardHistory : System.Web.UI.Page
    {
        readonly ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

        protected void Page_Load(object sender, EventArgs e)
        {
            #region инициализация сессии пользователя и выбранного регистра         
            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);
            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            int regId = -1;
            Int32.TryParse(Session["registerID"].ToString(), out regId);
            int mainFieldId = -1;
            Int32.TryParse(Session["mainFieldId"].ToString(), out mainFieldId);
            if (regId<0 || mainFieldId < 0)
                return;
            bool showAdminLog = false;

            List<CollectedFieldsValues> valuesInCardByMainFieldIdAndRegister = (from a in dataContext.CollectedCards
                where a.FkRegister == regId && a.MaInFieldID == mainFieldId
                join b in dataContext.CollectedFieldsValues on a.CollectedCardID equals b.FkCollectedCard
                where (b.FkUser != 1 || showAdminLog == true)
                select b).OrderBy(mc => mc.CollectedFieldValueID).Distinct().ToList();

            ResultTable.Rows.Add(new TableRow()
            {
                Cells =
                {
                    new TableCell() {BorderWidth = 1,Text = "Пользователь"},
                    new TableCell() {BorderWidth = 1,Text = "Дата"},
                    new TableCell() {BorderWidth = 1,Text = "Поле"},
                    new TableCell() {BorderWidth = 1,Text = "Значение"},
                    new TableCell() {BorderWidth = 1,Text = "instance"},
                    new TableCell() {BorderWidth = 1,Text = "del instance"}
                }
            });

            foreach (CollectedFieldsValues currentValue in valuesInCardByMainFieldIdAndRegister)
            {
                ResultTable.Rows.Add(new TableRow()
                {
                    Cells =
                {
                    new TableCell() {BorderWidth = 1,Text = (from a in dataContext.Users where a.UserID == currentValue.FkUser select a.Name).FirstOrDefault()},
                    new TableCell() {BorderWidth = 1,Text = currentValue.CreateDateTime.Date.ToString().Split(' ')[0]},
                    new TableCell() {BorderWidth = 1,Text = (from a in dataContext.Fields where a.FieldID == currentValue.FkField select a.Name +" "+ a.FieldID ).FirstOrDefault() },
                    new TableCell() {BorderWidth = 1,Text = currentValue.ValueText},
                    new TableCell() {BorderWidth = 1,Text = currentValue.Instance.ToString()},
                    new TableCell() {BorderWidth = 1,Text = currentValue.IsDeleted?"удален":""}
                }
                });
            }

            #endregion
        }
    }
}
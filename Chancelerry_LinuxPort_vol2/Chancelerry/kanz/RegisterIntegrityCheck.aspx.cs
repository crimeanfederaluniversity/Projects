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
    public partial class RegisterIntegrityCheck : System.Web.UI.Page
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
            int regId;
            Int32.TryParse(Session["registerID"].ToString(), out regId);
            var register =
                (from r in dataContext.Registers
                    where r.RegisterID == regId
                    select r).FirstOrDefault();

            #endregion

            if (register != null)
            {
                List<int> cardsWithCopy = new List<int>();
                List<int> cardsNotExist = new List<int>();
                string str = "";
                List<CollectedCards> allCards = (from a in dataContext.CollectedCards where a.Active == true && a.FkRegister == regId select a).OrderByDescending(mc => mc.MaInFieldID).ToList();
                if (allCards.Count <1)
                    return;
                int maxValue = allCards[0].MaInFieldID.Value;
                for (int i = 1; i < maxValue; i++)
                {
                    int cntTmp = (from a in allCards where a.MaInFieldID == i select a).Count();
                    if (cntTmp == 0)
                    {
                        cardsNotExist.Add(i);
                        //str += i.ToString() + Environment.NewLine;
                    }
                    else if (cntTmp != 1)
                    {
                        cardsWithCopy.Add(i);
                       // str += i.ToString() + " ERRROR" + Environment.NewLine;
                    }
                }
                cardsWithCopy = cardsWithCopy.Distinct().ToList();
                TableRow headerRow = new TableRow() {Cells = { new TableCell() {Text = "Номера отсутствующих"},new TableCell() {Text = "Номера дублируемых карт"} }};
                TableRow resultRow = new TableRow() { Cells = { new TableCell() { Text = cardsNotExist.Count!=0?string.Join(",",cardsNotExist):"Нет" }, new TableCell() { Text = cardsWithCopy.Count != 0 ? string.Join(",", cardsWithCopy):"Нет" } } };
                ResultTable.Rows.Add(headerRow);
                ResultTable.Rows.Add(resultRow);

            }
        }
    }
}
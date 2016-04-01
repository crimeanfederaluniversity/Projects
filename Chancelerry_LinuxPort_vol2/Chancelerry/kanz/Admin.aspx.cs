using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Configuration;
using Npgsql;

namespace Chancelerry.kanz
{
    public partial class Admin : System.Web.UI.Page
    {
        private ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "";
            List<CollectedCards> allCards = (from a in chancDb.CollectedCards where a.Active == true && a.FkRegister == 1 select a).OrderByDescending(mc=>mc.MaInFieldID).ToList();
            int maxValue = allCards[0].MaInFieldID.Value;
            for (int i=0;i<maxValue;i++)
            {
                int cntTmp = (from a in allCards where a.MaInFieldID == i select a).Count();
                if (cntTmp == 0)
                {
                    str += i.ToString() + Environment.NewLine;
                }
                else if (cntTmp != 1)
                {
                    str += i.ToString() + " ERRROR" + Environment.NewLine;
                }
            }
            TextBox1.Text = str;
        }
    }
}
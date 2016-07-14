using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace Chancelerry.kanz.rtfExport
{
    public partial class export1 : System.Web.UI.Page
    {

        private string GetDataFromDb(List<CollectedFieldsValues> allCollectedFieldsValues,  int instance,int fieldId) // if instance == -1 то любой сойдет
        {
            string tmp = (from a in allCollectedFieldsValues
                where a.FkField == fieldId
                      && (a.Instance == instance || instance == -1)
                select a).OrderByDescending(mc => mc.Version).Select(mc => mc.ValueText).FirstOrDefault();
            if (tmp == null)
                return "";
            return tmp;
        }

        private Dictionary<string, string> GetDataEndExport(int cardId, int instance )
        {
            ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));

            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);
            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            Users user = (from a in chancDb.Users where a.UserID == userId select a).FirstOrDefault();
            if (user == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            List<CollectedFieldsValues> allCollectedInCard = (from a in chancDb.CollectedFieldsValues
                                                         where a.FkCollectedCard == cardId
                                                         select a).ToList();
            Dictionary<string, string> newTmpDict = new Dictionary<string, string>();
            newTmpDict.Add("#markForWhoomStruct#", GetDataFromDb(allCollectedInCard, instance, 92));
            newTmpDict.Add("#markForWhoomName#", GetDataFromDb(allCollectedInCard, instance, 94));
            newTmpDict.Add("#markDocDate#", GetDataFromDb(allCollectedInCard, -1, 79));
            newTmpDict.Add("#markDocNumber#", GetDataFromDb(allCollectedInCard, -1, 78));
            newTmpDict.Add("#markDocContent#", GetDataFromDb(allCollectedInCard, -1, 81));
            newTmpDict.Add("#markItemNumber#", GetDataFromDb(allCollectedInCard, instance, 84));
            newTmpDict.Add("#markItemCnrtlDate#", GetDataFromDb(allCollectedInCard, instance,87 ));
            newTmpDict.Add("#markItemContent#", GetDataFromDb(allCollectedInCard, instance,85 ));
            newTmpDict.Add("#markResponsible#", GetDataFromDb(allCollectedInCard, -1, 97));
            newTmpDict.Add("#markExecutor#", "???");
            newTmpDict.Add("#markUser#", user.Name);
            newTmpDict.Add("#markTodayDate#", DateTime.Now.Date.ToString());
            return newTmpDict;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = 0;
            int.TryParse(Session["userID"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            rtfExporter rtf = new rtfExporter();
            string cardIdStr = Request.QueryString["card"];
            string instIdStr = Request.QueryString["inst"];
            string registerIdStr = Request.QueryString["reg"];
            int cardId = 0;
            int instance = 0;
            int regId = 0;
            Int32.TryParse(cardIdStr, out cardId);
            Int32.TryParse(instIdStr, out instance);
            Int32.TryParse(registerIdStr, out regId);

            Dictionary<string, string> newTmpDict = GetDataEndExport(cardId, instance);
            string st;
            if (regId == 3)
            {
                st = rtf.Export(Server.MapPath("export1.rtf"), newTmpDict, Server.MapPath("tmp/"), 0, 0, 0);
            }
            else
            {
                st =  rtf.Export(Server.MapPath("export2.rtf"), newTmpDict, Server.MapPath("tmp/"), 0, 0, 0);
            }
            Label1.Text = st.ToString();
        }
    }
}
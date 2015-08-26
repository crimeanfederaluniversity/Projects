using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competition
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
           if (!IsPostBack)
           {
               List<Users> expert =
                   (from a in newCompetition.Users
                    where a.Role == 1 && a.Active == true
                    select a).OrderBy(mc => mc.Name).ToList();

               var dictionary = new Dictionary<int, string>();
               dictionary.Add(0, "Выберите эксперта");

               foreach (var a in expert)
                   dictionary.Add(a.ID_User, a.Name);

               DropDownList1.DataTextField = "Value";
               DropDownList1.DataValueField = "Key";
               DropDownList1.DataSource = dictionary;
               DropDownList1.DataBind();

           }
            
            //int iduser = (int)Session["ID_User"];
            List<Bids> userbid = (from a in newCompetition.Bids                                
                                  where  a.Active == true
                                  select a).ToList();

            DataTable dataTableBid = new DataTable();

            dataTableBid.Columns.Add(new DataColumn("ID_Bid", typeof(int)));
            dataTableBid.Columns.Add(new DataColumn("BidName", typeof(string)));
            dataTableBid.Columns.Add(new DataColumn("Konkurs", typeof(string)));
            dataTableBid.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTableBid.Columns.Add(new DataColumn("Status", typeof(string)));

            foreach (Bids n in userbid)
            {
                DataRow dataRow = dataTableBid.NewRow();

                dataRow["ID_Bid"] = n.ID_Bid;
                dataRow["BidName"] = n.BidName;
                dataRow["Konkurs"] = n.FK_Konkurs;
                dataRow["Date"] = n.Date;
                dataRow["Status"] = n.Status;
            }
            GridView1.DataSource = userbid;
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BidDelete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (CompetitionDBDataContext sessionid = new CompetitionDBDataContext())
                {
                    Bids id = (from a in sessionid.Bids
                               where a.ID_Bid == Convert.ToInt32(button.CommandArgument) &&
                               a.Active == true
                               select a).FirstOrDefault();
                    id.Active = false;
                    sessionid.Bids.InsertOnSubmit(id);
                    sessionid.SubmitChanges();
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Заявка удалена');", true);
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
   
    }
}
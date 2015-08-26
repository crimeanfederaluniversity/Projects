using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competition
{
    public partial class UserBid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();      
            int iduser = (int)Session["ID_User"];
            List<Bids> userbid = (from a in newCompetition.Bids
                                  join b in newCompetition.User_BidMapingTable
                                      on  iduser equals  b.FK_User
                                  where a.ID_Bid == b.FK_Bid && a.Active == true
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
        protected void BidFill_Click(object sender, EventArgs e)
        { 
            Button button = (Button)sender;
            {
                Session["ID_Bid"] = Convert.ToInt32(button.CommandArgument);
                using (CompetitionDBDataContext sessionid = new CompetitionDBDataContext())
                {
                    Bids id = (from a in sessionid.Bids
                               where a.ID_Bid == Convert.ToInt32(button.CommandArgument)
                               select a).FirstOrDefault();

                    Session["ID_Konkurs"] = Convert.ToInt32(id.FK_Konkurs);
                }
                Response.Redirect("~/ZapolnenieForm.aspx");
            }
        }
    }
} 
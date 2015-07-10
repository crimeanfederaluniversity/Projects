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
            int idbid = 1;//(int)Session["ID_Bid"];
            
            DataTable dataTableBid = new DataTable();

            dataTableBid.Columns.Add(new DataColumn("ID_Bid", typeof(int)));
            dataTableBid.Columns.Add(new DataColumn("BidName", typeof(string)));
            dataTableBid.Columns.Add(new DataColumn("Konkurs", typeof(string)));
            dataTableBid.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTableBid.Columns.Add(new DataColumn("Status", typeof(string)));

            CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
            Konkursy konname = (from c in newCompetition.Bids
                                join b in newCompetition.Konkursy
                                    on c.FK_Konkurs equals b.ID_Konkurs
                                where b.Active == true
                                select b).FirstOrDefault();
            List<Bids> mybid = (from a in newCompetition.Bids  where a.ID_Bid == idbid select a).ToList();
        
                foreach (Bids n in mybid)
                {
                   
                    DataRow dataRow = dataTableBid.NewRow();

                    dataRow["ID_Bid"] = n.ID_Bid;
                    dataRow["BidName"] = n.BidName;
                    dataRow["Konkurs"] = n.FK_Konkurs;
                    dataRow["Date"] = n.Date;
                    dataRow["Status"] = n.Status;
                }
                
            GridView1.DataSource = mybid;
            GridView1.DataBind();
        }
    }
}
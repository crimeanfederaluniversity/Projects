using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class FastStructure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }

            int allZeroLevel = 0;
            int insertZeroLevel = 0;
            int confirmZeroLevel = 0;
            #region get zero leve list
            List<ZeroLevelSubdivisionTable> zeroLevelList = (from a in kPiDataContext.ZeroLevelSubdivisionTable
                                                             where a.Active == true
                                                             select a).ToList();
            #endregion
            foreach (ZeroLevelSubdivisionTable zeroLevelItem in zeroLevelList)//по каждому университету
            {
                #region get first level list
                List<FirstLevelSubdivisionTable> firstLevelList = (from b in kPiDataContext.FirstLevelSubdivisionTable
                                                                  /* join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                                                       on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId*/
                                                                   where b.FK_ZeroLevelSubvisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                                                                         && b.Active == true
                                                                        // && c.Active == true
                                                                   select b).ToList();
                #endregion
                TextBox1.Text+="__" + zeroLevelItem.Name +"\n";
                foreach (FirstLevelSubdivisionTable firstLevelItem in firstLevelList)//по каждой академии
                {
                    #region get second level list
                    List<SecondLevelSubdivisionTable> secondLevelList =
                        (from d in kPiDataContext.SecondLevelSubdivisionTable
                         where d.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                         && d.Active == true
                         select d).ToList();
                    #endregion
                    TextBox1.Text +="____" + firstLevelItem.Name+"\n";
                    foreach (SecondLevelSubdivisionTable secondLevelItem in secondLevelList)//по каждому факультету
                    {

                        TextBox1.Text += "______" + secondLevelItem.Name + "\n";
                        #region get third level list
                        List<ThirdLevelSubdivisionTable> thirdLevelList =
                            (from f in kPiDataContext.ThirdLevelSubdivisionTable
                             where f.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                             && f.Active == true
                             select f).ToList();
                        #endregion
                        foreach (ThirdLevelSubdivisionTable thirdLevelItem in thirdLevelList)//по кафедре
                        {
                            TextBox1.Text += "________" + thirdLevelItem.Name + "\n";
                            #region get fourth level list

                            List<FourthLevelSubdivisionTable> fourthLevelList = (from g in kPiDataContext.FourthLevelSubdivisionTable
                                                   where
                                                   g.FK_ThirdLevelSubdivisionTable ==
                                                   thirdLevelItem.ThirdLevelSubdivisionTableID
                                                   && g.Active == true
                                                   select g).ToList();
                            
                            #endregion
                            foreach (FourthLevelSubdivisionTable fourthLevelItem in fourthLevelList)//по специальности
                            {
                                TextBox1.Text += "__________" + fourthLevelItem.Name + "\n";
                            }
                        }
                    }
                }
            }
        }
    }
}
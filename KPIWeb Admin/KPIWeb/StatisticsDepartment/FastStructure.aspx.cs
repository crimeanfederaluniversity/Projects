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
        public class MyObject
        {
            public int Id;
            public int ParentId;
            public string Name;
            //public string UrlAddr;
            //public int Active;
        }
        private void BindTree(IEnumerable<MyObject> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));
            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.Name, node.Id.ToString());
                
                /* if (node.Active == 1)
                {
                    //newNode.NavigateUrl = node.UrlAddr;
                }
                else*/

                newNode.SelectAction = TreeNodeSelectAction.None; 
          
                if (parentNode == null)
                {
                    TreeView1.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTree(list, newNode);
            }
        }

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

            List<MyObject> list = new List<MyObject>();    

            int allZeroLevel = 0;
            int insertZeroLevel = 0;
            int confirmZeroLevel = 0;
            int i = 1;

            int UsrCnt = 0;
            List<UsersTable> Users;
            string tmp;
            string tmp2;
            #region get zero leve list
            List<ZeroLevelSubdivisionTable> zeroLevelList = (from a in kPiDataContext.ZeroLevelSubdivisionTable
                                                             where a.Active == true
                                                             select a).ToList();
            #endregion
            foreach (ZeroLevelSubdivisionTable zeroLevelItem in zeroLevelList)//по каждому университету
            {
                #region get first level list
                List<FirstLevelSubdivisionTable> firstLevelList = (from b in kPiDataContext.FirstLevelSubdivisionTable
                                                                   where b.FK_ZeroLevelSubvisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                                                                         && b.Active == true
                                                                   select b).ToList();
                #endregion
                //TextBox1.Text+="__" + zeroLevelItem.Name +"\n";
                i++;
                int par0 = i;
                UsrCnt = 0;
                Users = (from a in kPiDataContext.UsersTable
                         where a.Active == true
                         && a.FK_ZeroLevelSubdivisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                         && a.FK_FirstLevelSubdivisionTable == null
                         select a).ToList();
                tmp = "";
                foreach (UsersTable curuser in Users )
                {
                    if (curuser.Confirmed==true)
                    {
                        tmp += "<font style=\"color:#00FF00;\">" + curuser.Email + "</font> ";
                    }
                    else
                    {
                        tmp += "<font style=\"color:#FF0000;\">" + curuser.Email + "</font> ";
                    }
                }
                UsrCnt = (from a in kPiDataContext.UsersTable
                          where a.Active == true
                          && a.FK_ZeroLevelSubdivisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                          select a).Count();
                tmp2 = "";
                if (UsrCnt>0)
                {
                    tmp2 = "<font style=\"color:#0000FF;\">" + zeroLevelItem.Name + "</font> ";
                }
                else
                {
                    tmp2 = zeroLevelItem.Name;
                }
                list.Add(new MyObject() { Id = i, ParentId = 0, Name = tmp2 + " ( " + UsrCnt.ToString() + ") " + tmp });
                
                foreach (FirstLevelSubdivisionTable firstLevelItem in firstLevelList)//по каждой академии
                {
                    #region get second level list
                    List<SecondLevelSubdivisionTable> secondLevelList =
                        (from d in kPiDataContext.SecondLevelSubdivisionTable
                         where d.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                         && d.Active == true
                         select d).ToList();
                    #endregion
                   // TextBox1.Text +="____" + firstLevelItem.Name+"\n";

                    i++;
                    int par1 = i;
                    UsrCnt = 0;
                    Users = (from a in kPiDataContext.UsersTable
                             where a.Active == true
                             && a.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                             && a.FK_SecondLevelSubdivisionTable == null
                             select a).ToList();
                    tmp = "";
                    foreach (UsersTable curuser in Users)
                    {
                        if (curuser.Confirmed == true)
                        {
                            tmp += "<font style=\"color:#00FF00;\">" + curuser.Email + "</font> ";
                        }
                        else
                        {
                            tmp += "<font style=\"color:#FF0000;\">" + curuser.Email + "</font> ";
                        }
                    }
                    UsrCnt = (from a in kPiDataContext.UsersTable
                              where a.Active == true
                              && a.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                              select a).Count();

                    tmp2 = "";
                    if (UsrCnt > 0)
                    {
                        tmp2 = "<font style=\"color:#0000FF;\">" + firstLevelItem.Name + "</font> ";
                    }
                    else
                    {
                        tmp2 = firstLevelItem.Name;
                    }
                    list.Add(new MyObject() { Id = i, ParentId = par0, Name = tmp2 + " (" + UsrCnt.ToString() + ") " + tmp });
                    foreach (SecondLevelSubdivisionTable secondLevelItem in secondLevelList)//по каждому факультету
                    {
                       // TextBox1.Text += "______" + secondLevelItem.Name + "\n";
                        #region get third level list
                        List<ThirdLevelSubdivisionTable> thirdLevelList =
                            (from f in kPiDataContext.ThirdLevelSubdivisionTable
                             where f.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                             && f.Active == true
                             select f).ToList();
                        #endregion
                        i++;
                        int par2 = i;
                        UsrCnt = 0;
                        Users = (from a in kPiDataContext.UsersTable
                                 where a.Active == true
                                 && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                 && a.FK_ThirdLevelSubdivisionTable == null
                                 select a).ToList();
                        tmp = "";
                        foreach (UsersTable curuser in Users)
                        {
                            if (curuser.Confirmed == true)
                            {
                                tmp += "<font style=\"color:#00FF00;\">" + curuser.Email + "</font> ";
                            }
                            else
                            {
                                tmp += "<font style=\"color:#FF0000;\">" + curuser.Email + "</font> ";
                            }
                        }
                        UsrCnt = (from a in kPiDataContext.UsersTable
                                  where a.Active == true
                                  && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                  select a).Count();

                        tmp2 = "";
                        if (UsrCnt > 0)
                        {
                            tmp2 = "<font style=\"color:#0000FF;\">" + secondLevelItem.Name + "</font> ";
                        }
                        else
                        {
                            tmp2 = secondLevelItem.Name;
                        }

                        list.Add(new MyObject() { Id = i, ParentId = par1, Name = tmp2 + " (" + UsrCnt.ToString() + ") " + tmp });
                        foreach (ThirdLevelSubdivisionTable thirdLevelItem in thirdLevelList)//по кафедре
                        {
                            //TextBox1.Text += "________" + thirdLevelItem.Name + "\n";
                            #region get fourth level list
                            /*
                            List<FourthLevelSubdivisionTable> fourthLevelList = (from g in kPiDataContext.FourthLevelSubdivisionTable
                                                   where
                                                   g.FK_ThirdLevelSubdivisionTable ==
                                                   thirdLevelItem.ThirdLevelSubdivisionTableID
                                                   && g.Active == true
                                                   select g).ToList();
                            */
                            #endregion
                            i++;
                            int par3 = i;
                            UsrCnt = 0;
                            Users = (from a in kPiDataContext.UsersTable
                                     where a.Active == true
                                     && a.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                     && a.FK_FourthLevelSubdivisionTable == null
                                     select a).ToList();
                            tmp = "";
                            foreach (UsersTable curuser in Users)
                            {
                                if (curuser.Confirmed == true)
                                {
                                    tmp += "<font style=\"color:#00FF00;\">" + curuser.Email + "</font> ";
                                }
                                else
                                {
                                    tmp += "<font style=\"color:#FF0000;\">" + curuser.Email + "</font> ";
                                }
                            }
                            UsrCnt = (from a in kPiDataContext.UsersTable
                                      where a.Active == true
                                      && a.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                      select a).Count();

                            tmp2 = "";
                            if (UsrCnt > 0)
                            {
                                tmp2 = "<font style=\"color:#0000FF;\">" + thirdLevelItem.Name + "</font> ";
                            }
                            else
                            {
                                tmp2 = thirdLevelItem.Name;
                            }

                            list.Add(new MyObject() { Id = i, ParentId = par2, Name = tmp2 + " (" + UsrCnt.ToString() + ") " + tmp });
                           /*
                            foreach (FourthLevelSubdivisionTable fourthLevelItem in fourthLevelList)//по специальности
                            {
                                TextBox1.Text += "__________" + fourthLevelItem.Name + "\n";
                            }
                            */
                        }
                    }
                }
            }
            BindTree(list, null);
            TreeView1.CollapseAll();
            //TreeView1.ExpandAll();
        }
    }
}
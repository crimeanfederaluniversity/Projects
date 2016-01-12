﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Info_Pages
{
    public partial class UserWithStructWithReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<ReportArchiveTable> RepList = (from a in kPiDataContext.ReportArchiveTable
                                                    where a.Active == true
                                                    select a).ToList();
                foreach (ReportArchiveTable rep in RepList)
                {
                    ListItem Item = new ListItem();
                    Item.Text = (string)rep.Name;
                    Item.Value = (rep.ReportArchiveTableID).ToString();
                    DropDownList1.Items.Add(Item);
                }
            }

        }
        public class MyObject
        {
            public int Id;
            public int ParentId;
            public string Name;
        }
        private void BindTree(IEnumerable<MyObject> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));
            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.Name, node.Id.ToString());
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "123_")
            {
                int reportID = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                TreeView1.DataSource = null;
                TreeView1.DataBind();
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
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
                                                                       join a in kPiDataContext.ReportArchiveAndLevelMappingTable
                                                                       on b.FirstLevelSubdivisionTableID equals a.FK_FirstLevelSubmisionTableId
                                                                       where a.Active == true
                                                                       && a.FK_ReportArchiveTableId == reportID
                                                                       select b).Distinct().ToList();
                    #endregion
                    i++;
                    int par0 = i;
                    UsrCnt = 0;
                    Users = (from a in kPiDataContext.UsersTable
                             where a.Active == true
                             && a.AccessLevel != 9
                             && a.AccessLevel != 10
                             && a.FK_ZeroLevelSubdivisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                             && a.FK_FirstLevelSubdivisionTable == null
                             select a).ToList();
                    tmp = "";
                    foreach (UsersTable curuser in Users)
                    {
                        if (curuser.Confirmed == true)
                        {
                            tmp += "<font style=\"color:#00a34f;font-weight: bold;\">" + curuser.Email + "</font> ";
                        }
                        else
                        {
                            tmp += "<font style=\"color:#990000;font-weight: bold;\">" + curuser.Email + "</font> ";
                        }
                    }
                    UsrCnt = (from a in kPiDataContext.UsersTable
                              where a.Active == true
                              && a.FK_ZeroLevelSubdivisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                              && a.AccessLevel != 9
                              && a.AccessLevel != 10
                              select a).Count();
                    tmp2 = "";
                    if (UsrCnt > 0)
                    {
                        tmp2 = "<font style=\"color:#0000FF;font-weight: bold;\">" + zeroLevelItem.Name + "</font> ";
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
                             join a in kPiDataContext.ReportArchiveAndLevelMappingTable
                             on d.SecondLevelSubdivisionTableID equals a.FK_SecondLevelSubdivisionTable
                             where a.Active == true
                             && a.FK_ReportArchiveTableId == reportID
                             select d).Distinct().ToList();
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
                                tmp += "<font style=\"color:#00a34f;font-weight: bold;\">" + curuser.Email + "</font> ";
                            }
                            else
                            {
                                tmp += "<font style=\"color:#990000;font-weight: bold;\">" + curuser.Email + "</font> ";
                            }
                        }
                        UsrCnt = (from a in kPiDataContext.UsersTable
                                  where a.Active == true
                                  && a.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                  select a).Count();

                        tmp2 = "";
                        if (UsrCnt > 0)
                        {
                            tmp2 = "<font style=\"color:#0000FF;font-weight: bold;\">" + firstLevelItem.Name + "</font> ";
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
                                 join a in kPiDataContext.ReportArchiveAndLevelMappingTable
                                 on f.ThirdLevelSubdivisionTableID equals a.FK_ThirdLevelSubdivisionTable
                                 where a.Active == true
                                 && a.FK_ReportArchiveTableId == reportID
                                 select f).Distinct().ToList();
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
                                    tmp += "<font style=\"color:#00a34f;font-weight: bold;\">" + curuser.Email + "</font> ";
                                }
                                else
                                {
                                    tmp += "<font style=\"color:#990000;font-weight: bold;\">" + curuser.Email + "</font> ";
                                }
                            }
                            UsrCnt = (from a in kPiDataContext.UsersTable
                                      where a.Active == true
                                      && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                      select a).Count();

                            tmp2 = "";
                            if (UsrCnt > 0)
                            {
                                tmp2 = "<font style=\"color:#0000FF;font-weight: bold;\">" + secondLevelItem.Name + "</font> ";
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
                                        tmp += "<font style=\"color:#00a34f;font-weight: bold;\">" + curuser.Email + "</font> ";
                                    }
                                    else
                                    {
                                        tmp += "<font style=\"color:#990000;font-weight: bold;\">" + curuser.Email + "</font> ";
                                    }
                                }
                                UsrCnt = (from a in kPiDataContext.UsersTable
                                          where a.Active == true
                                          && a.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                          select a).Count();

                                tmp2 = "";
                                if (UsrCnt > 0)
                                {
                                    tmp2 = "<font style=\"color:#0000FF;font-weight: bold;\">" + thirdLevelItem.Name + "</font> ";
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
}
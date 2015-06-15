using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class OwnersProcessInfo : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
           // Label5.Text = getColoredName(0, 0, 0, 0, 0, "");
            Label1.Text = getColoredName(1, 1, 0, 0, 0, "Утверждено");
            Label2.Text = getColoredName(1, 0, 1, 0, 0, "Ожидает утверждения");
            Label3.Text = getColoredName(1, 0, 0, 1, 0, "В процессе заполнения");
            Label4.Text = getColoredName(1, 0, 0, 0, 1, "Отчет не открывался");
            Label5.Text = getColoredName(1, 0, 0, 0, 0, "В процессе заполнения подразделениями");
        }

     public string getColoredName(int all,int conf,int toconf,int started, int notstarted,string name)
    {
        if (all == 0)
        {
            return name;
        }
        else if (all == conf)
        {
            return "<font style=\"color:#27a327;font-weight: bold;\">" + name + "</font> ";
        }
        else if(all == notstarted)
        {
            return "<font style=\"color:#92000a;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all == started)
        {
            return "<font style=\"color:#753313;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all == toconf)
        {
            return "<font style=\"color:#0c030d;font-weight: bold;\">" + name + "</font> ";
        }
        else
        {
            return "<font style=\"color:#044857;font-weight: bold;\">" + name + "</font> ";
        }
    }

        protected void Button1_Click(object sender, EventArgs e)
        {
            

            if (TextBox1.Text == "123_")
            {

                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();           
                TreeView1.DataSource = null;
                TreeView1.DataBind();
                //Label1.Visible = true;

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
                // колво утвердивших (% ) // % ожидающих утверждения // % начавших заполнение 
                foreach (ZeroLevelSubdivisionTable zeroLevelItem in zeroLevelList)//по каждому университету
                {
                    #region get first level list
                    List<FirstLevelSubdivisionTable> firstLevelList = (from b in kPiDataContext.FirstLevelSubdivisionTable

                                                                       where b.FK_ZeroLevelSubvisionTable == zeroLevelItem.ZeroLevelSubdivisionTableID
                                                                             && b.Active == true
                                                                       select b).ToList();
                    #endregion
                    #region
                    int all0 = (from a in kPiDataContext.ThirdLevelParametrs where a.Active == true select a).Count();
                    int toconf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 3
                                   select a).Distinct().Count();

                    int conf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 4                                  
                                   select a).Distinct().Count();

                    int started0 = (from a in kPiDataContext.ThirdLevelParametrs
                                    join b in kPiDataContext.CollectedBasicParametersTable
                                    on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                    where b.CollectedValue != null
                                    select a).Distinct().Count()  - toconf0 - conf0;

                    int notstarted0 =all0 - toconf0 - started0 - conf0;
                    #endregion
                    i++;
                    int par0 = i;
                    tmp2 = getColoredName(all0,conf0,toconf0,started0,notstarted0, zeroLevelItem.Name);
                    list.Add(new MyObject() { Id = i, ParentId = 0, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                    foreach (FirstLevelSubdivisionTable firstLevelItem in firstLevelList)//по каждой академии
                    {
                        #region get second level list
                        List<SecondLevelSubdivisionTable> secondLevelList =
                            (from d in kPiDataContext.SecondLevelSubdivisionTable
                             where d.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                             && d.Active == true
                             select d).ToList();
                        #endregion
                        #region

                        all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                join b in kPiDataContext.SecondLevelSubdivisionTable
                                on a.FK_SecondLevelSubdivisionTable equals b.SecondLevelSubdivisionTableID
                                where a.Active == true
                                && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                select a).Count();
                     
                        toconf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 3
                                   && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                   select a).Distinct().Count();

                        conf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 4
                                   && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                   select a).Distinct().Count();

                        started0 = (from a in kPiDataContext.ThirdLevelParametrs
                                    join b in kPiDataContext.CollectedBasicParametersTable
                                    on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                    where
                                    b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                    && b.CollectedValue != null
                                    select a).Distinct().Count() - toconf0 - conf0;
                        notstarted0 = all0 - toconf0 - started0 - conf0;
                        #endregion
                        i++;
                        int par1 = i;
                        //tmp2 = firstLevelItem.Name;
                        tmp2 = getColoredName(all0, conf0, toconf0, started0, notstarted0, firstLevelItem.Name);
                        list.Add(new MyObject() { Id = i, ParentId = par0, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                        foreach (SecondLevelSubdivisionTable secondLevelItem in secondLevelList)//по каждому факультету
                        {
                            #region get third level list
                            List<ThirdLevelSubdivisionTable> thirdLevelList =
                                (from f in kPiDataContext.ThirdLevelSubdivisionTable
                                 where f.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                 && f.Active == true
                                 select f).ToList();
                            #endregion
                            #region
                            all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                    where a.Active == true
                                    && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                    select a).Count();

                            conf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                     join b in kPiDataContext.CollectedBasicParametersTable
                                     on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                     where b.Status == 4
                                     && b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                     select a).Distinct().Count();

                            toconf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                       join b in kPiDataContext.CollectedBasicParametersTable
                                       on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                       where b.Status == 3
                                       && b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                       select a).Distinct().Count();

                            started0 = (from a in kPiDataContext.ThirdLevelParametrs
                                        join b in kPiDataContext.CollectedBasicParametersTable
                                        on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                        where
                                        b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                        && b.CollectedValue != null
                                        select a).Distinct().Count() - conf0 - toconf0;

                            notstarted0 = all0 - toconf0 - started0 - conf0;

                            #endregion
                            i++;
                            int par2 = i;
                            //tmp2 = secondLevelItem.Name;
                            tmp2 = getColoredName(all0, conf0, toconf0, started0, notstarted0, secondLevelItem.Name);
                            list.Add(new MyObject() { Id = i, ParentId = par1, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                            foreach (ThirdLevelSubdivisionTable thirdLevelItem in thirdLevelList)//по кафедре
                            {
                                #region
                                all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                        where a.Active == true
                                        && a.ThirdLevelSubdivisionTableID == thirdLevelItem.ThirdLevelSubdivisionTableID
                                        select a).Count();

                                conf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                         join b in kPiDataContext.CollectedBasicParametersTable
                                         on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                         where b.Status == 4
                                         && b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                         select a).Distinct().Count();

                                toconf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                           join b in kPiDataContext.CollectedBasicParametersTable
                                           on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                           where b.Status == 3
                                           && b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                           select a).Distinct().Count();

                                started0 = (from a in kPiDataContext.ThirdLevelParametrs
                                            join b in kPiDataContext.CollectedBasicParametersTable
                                            on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                            where
                                            b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                            && b.CollectedValue != null
                                            select a).Distinct().Count() - toconf0 - conf0;

                                notstarted0 = all0 - toconf0 - started0 - conf0;
                                #endregion
                                i++;
                                int par3 = i;
                               // tmp2 = thirdLevelItem.Name;
                                tmp2 = getColoredName(all0, conf0, toconf0, started0, notstarted0, thirdLevelItem.Name);
                                list.Add(new MyObject() { Id = i, ParentId = par2, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                            }
                        }
                    }
                }
                BindTree(list, null);
                TreeView1.CollapseAll();
            }
            
        }

    }
}
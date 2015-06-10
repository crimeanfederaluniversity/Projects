using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class FillingProcessInfo : System.Web.UI.Page
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
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             if (TextBox1.Text == "123_")
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                TextBox2.Text = "";
                TextBox2.Text += "Всего структурных подразделений вносящих данные: " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                      where a.Active == true
                                                                      select a).Count().ToString();
                TextBox2.Text += Environment.NewLine;

                TextBox2.Text += "Структурных подразделений вносящих данные, где есть хоть один пользователь: " + 
                                                                                    (from a in kPiDataContext.ThirdLevelParametrs
                                                                                     join b in kPiDataContext.UsersTable
                                                                                     on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                        where a.Active == true
                                                                                        && b.Active == true
                                                                                        && b.AccessLevel == 0
                                                                                        select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                 
                TextBox2.Text += "Всего пользователей: " + (from b in kPiDataContext.UsersTable
                                                             where 
                                                             b.AccessLevel == 0
                                                             && b.Active == true
                                                             select b).Count().ToString();
                TextBox2.Text += Environment.NewLine;

                TextBox2.Text += "Всего пользователей активировавших аккаунты: " + (from b in kPiDataContext.UsersTable
                                                           where
                                                           b.AccessLevel == 0
                                                           && b.Active == true
                                                           && b.Confirmed == true
                                                           select b).Count().ToString();

                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений утвердивших отчет " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                   join b in kPiDataContext.CollectedBasicParametersTable
                                                                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                   where b.Status == 4
                                                                                   select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений c отправленным на утверждение отчетом " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                   join b in kPiDataContext.CollectedBasicParametersTable
                                                                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                   where b.Status == 3
                                                                                   select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений отчетом возвращенным на доработку " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                                       join b in kPiDataContext.CollectedBasicParametersTable
                                                                                                       on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                                       where b.Status == 1
                                                                                                       select a).Distinct().Count().ToString();

                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений внесщих 1 и более показателей (не учитываются утвержденные и отправленные на утверждение) " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                                   join b in kPiDataContext.CollectedBasicParametersTable
                                                                                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                                   where b.Status == 0
                                                                                                   && b.CollectedValue != null
                                                                                                   select a).Distinct().Count().ToString();


                TreeView1.DataSource = null;
                TreeView1.DataBind();
                Label1.Visible = true;
                
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
                    int conf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                 join b in kPiDataContext.CollectedBasicParametersTable
                                         on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                 where b.Status == 4
                                 select a).Distinct().Count();
                    int toconf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 3
                                   select a).Distinct().Count();
                    int started0 = (from a in kPiDataContext.ThirdLevelParametrs
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 0
                                   && b.CollectedValue != null
                                   select a).Distinct().Count();
                    #endregion
                    i++;
                    int par0 = i;
                    tmp2 = zeroLevelItem.Name;
                    list.Add(new MyObject() { Id = i, ParentId = 0, Name = tmp2 +" : "+ all0.ToString() +"/"+ conf0.ToString() +"/"+ toconf0.ToString() +"/"+ started0.ToString() });
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

                        conf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                     join b in kPiDataContext.CollectedBasicParametersTable
                                             on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                     where b.Status == 4
                                     && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                     select a).Distinct().Count();

                        toconf0 = (from a in kPiDataContext.ThirdLevelParametrs
                                       join b in kPiDataContext.CollectedBasicParametersTable
                                       on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                       where b.Status == 3
                                       && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                       select a).Distinct().Count();

                        started0 = (from a in kPiDataContext.ThirdLevelParametrs
                                        join b in kPiDataContext.CollectedBasicParametersTable
                                        on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                        where b.Status == 0
                                        && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                        && b.CollectedValue != null
                                        select a).Distinct().Count();
                        #endregion
                        i++;
                        int par1 = i;                      
                        tmp2 = firstLevelItem.Name;
                        list.Add(new MyObject() { Id = i, ParentId = par0, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() });
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
                                        where b.Status == 0
                                        && b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                        && b.CollectedValue != null
                                        select a).Distinct().Count();
                            #endregion
                            i++;
                            int par2 = i;
                            tmp2 = secondLevelItem.Name;
                            list.Add(new MyObject() { Id = i, ParentId = par1, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() });
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
                                            where b.Status == 0
                                            && b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                            && b.CollectedValue != null
                                            select a).Distinct().Count();
                                #endregion
                                i++;
                                int par3 = i;
                                tmp2 = thirdLevelItem.Name;
                                list.Add(new MyObject() { Id = i, ParentId = par2, Name = tmp2 + " : " + all0.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() });                            
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
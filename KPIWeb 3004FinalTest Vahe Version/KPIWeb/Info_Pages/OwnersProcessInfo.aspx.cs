using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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

            Label1.Text = getColoredName(1, 1, 0, 0, 0, 0, "1)Согласовано директором");
            Label2.Text = getColoredName(1, 0, 1, 0, 0, 0, "2)Утверждено");
            Label3.Text = getColoredName(1, 0, 0, 1, 0, 0, "3)Ожидает утверждения");
            Label4.Text = getColoredName(1, 0, 0, 0, 1, 0, "4)Данные частично внесены");
            Label5.Text = getColoredName(1, 0, 0, 0, 0, 1, "5)Данные не внесены");
            Label7.Text = getColoredName(1, 0, 0, 0, 0, 2, "6)ОШИБКА");
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<ReportArchiveTable> RepList = (from a in kPiDataContext.ReportArchiveTable
                                                    where a.Active == true
                                                    select a).ToList();
                foreach (ReportArchiveTable rep in RepList)
                {
                    ListItem Item = new ListItem();
                    Item.Text = (string) rep.Name;
                    Item.Value = (rep.ReportArchiveTableID).ToString();
                    DropDownList1.Items.Add(Item);
                }
            }
        }

        public string getColoredName(int all,int conf1,int conf,int toconf,int started, int notstarted,string name)
        {
        if (all == 0)
        {
            return "<font style=\"color:#ff0000;font-weight: bold;\">" + name + "(Пользователь не открывал страницу заполнения)</font> ";
        }
        else if(all == notstarted)
        {
            return "<font style=\"color:#ff0000;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all == started)
        {
            return "<font style=\"color:#ffa500;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all == toconf)
        {
            return "<font style=\"color:#07d2b;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all == conf1)
        {
            return "<font style=\"color:#008000;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all == conf)
        {
            return "<font style=\"color:#0000ff;font-weight: bold;\">" + name + "</font> ";
        }
        else if (all != (notstarted + started + toconf + conf + conf1))
        {
            return "<font style=\"color:#808080;font-weight: bold;\">" + name + " (Ошибка! Обратитесь в техподдержку)</font> ";
        }
        else
        {
            return "<font style=\"color:#ffa500;font-weight: bold;\">" + name + "</font> ";
        }
    }

     protected void Button1_Click(object sender, EventArgs e)
        {
            int reportID = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
            if (TextBox1.Text == "123_")
            {
                Label1.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label7.Visible = true;
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();

                TreeView1.CollapseAll();
                TreeView1.DataSource = null;
                TreeView1.DataBind();
                List<MyObject> list = new List<MyObject>();
                int i = 0;
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
                                                                       join a in kPiDataContext.ReportArchiveAndLevelMappingTable
                                                                       on b.FirstLevelSubdivisionTableID equals a.FK_FirstLevelSubmisionTableId
                                                                       where a.Active == true
                                                                       && a.FK_ReportArchiveTableId == reportID
                                                                       select b).Distinct().ToList();
                    #endregion
                    #region
                    int all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable where a.Active == true 
                                join b in kPiDataContext.ReportArchiveAndLevelMappingTable 
                                on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                where b.Active == true
                                && a.Active == true
                                && b.FK_ReportArchiveTableId == reportID
                                select a).Distinct().Count();

                    int toconf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 3
                                   && a.Active == true
                                   && b.Active == true
                                   && b.FK_ReportArchiveTable == reportID
                                   join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                   on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                   where c.FK_ReportArchiveTableId == reportID
                                   && c.Active == true
                                   select a).Distinct().Count();

                    int conf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable                            
                                   where b.Status == 4
                                   && a.Active == true
                                   && b.FK_ReportArchiveTable == reportID
                                   && b.Active == true
                                   join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                   on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                   where
                                    c.FK_ReportArchiveTableId == reportID
                                    && c.Active == true
                                   select a).Distinct().Count();
                    int conf1 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                 join b in kPiDataContext.CollectedBasicParametersTable
                                 on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                 where b.Status == 5
                                 && a.Active == true
                                 && b.FK_ReportArchiveTable == reportID
                                 && b.Active == true
                                 join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                 on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                 where 
                                  c.FK_ReportArchiveTableId == reportID
                                  && c.Active == true
                                 select a).Distinct().Count();
                    int started0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                    join b in kPiDataContext.CollectedBasicParametersTable
                                    on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                    where b.CollectedValue != null
                                    && b.FK_ReportArchiveTable == reportID
                                    && b.Active == true
                                    && a.Active == true
                                    join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                    on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                    where
                                    c.FK_ReportArchiveTableId == reportID
                                    && c.Active == true
                                    select a).Distinct().Count() - toconf0 - conf0 - conf1;

                    int notstarted0 = all0 - toconf0 - started0 - conf0 - conf1;
                    #endregion
                    i++;
                    int par0 = i;
                    tmp2 = getColoredName(all0, conf1,conf0, toconf0, started0, notstarted0, zeroLevelItem.Name);
                    if (tmp2!=null)
                        list.Add(new MyObject() { Id = i, ParentId = 0, Name = tmp2 + " : " + all0.ToString() + "/" + conf1.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
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
                        #region

                        all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                join b in kPiDataContext.SecondLevelSubdivisionTable
                                on a.FK_SecondLevelSubdivisionTable equals b.SecondLevelSubdivisionTableID
                                where a.Active == true

                                && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                && b.Active == true
                                join c in kPiDataContext.ReportArchiveAndLevelMappingTable 
                                on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                
                                where c.Active == true
                                && c.FK_ReportArchiveTableId == reportID
                                select a).Count();

                        toconf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 3
                                   && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                   && b.Active == true
                                   && a.Active == true
                                   join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                   on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                   where c.Active == true
                                && b.FK_ReportArchiveTable == reportID
                                && c.FK_ReportArchiveTableId == reportID

                                   select a).Distinct().Count();

                        conf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                   join b in kPiDataContext.CollectedBasicParametersTable
                                   on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                   where b.Status == 4
                                   && b.FK_ReportArchiveTable == reportID
                                   && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                   && b.Active == true
                                   && a.Active == true
                                   join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                   on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                   where c.Active == true
                                   && c.FK_ReportArchiveTableId == reportID
                                   select a).Distinct().Count();

                        conf1 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                 join b in kPiDataContext.CollectedBasicParametersTable
                                 on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                 where b.Status == 5
                                 && b.FK_ReportArchiveTable == reportID
                                 && a.Active == true
                                 && b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                 && b.Active == true
                                 join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                 on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                 where c.Active == true
                                 && c.FK_ReportArchiveTableId == reportID
                                 select a).Distinct().Count();

                        started0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                    join b in kPiDataContext.CollectedBasicParametersTable
                                    on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                    where
                                    b.FK_FirstLevelSubdivisionTable == firstLevelItem.FirstLevelSubdivisionTableID
                                    && b.FK_ReportArchiveTable == reportID
                                    && b.CollectedValue != null
                                    && b.Active == true
                                    && a.Active == true
                                    join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                    on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                    where c.Active == true
                                    && c.FK_ReportArchiveTableId == reportID
                                    select a).Distinct().Count() - toconf0 - conf0 - conf1;
                        notstarted0 = all0 - toconf0 - started0 - conf0 - conf1;
                        #endregion
                        i++;
                        int par1 = i;
                        //tmp2 = firstLevelItem.Name;

                        tmp2 = getColoredName(all0, conf1, conf0, toconf0, started0, notstarted0, firstLevelItem.Name);
                        if (tmp2 != null)
                            list.Add(new MyObject() { Id = i, ParentId = par0, Name = tmp2 + " : " + all0.ToString() + "/" + conf1.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                        foreach (SecondLevelSubdivisionTable secondLevelItem in secondLevelList)//по каждому факультету
                        {
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
                            #region
                            all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                    where a.Active == true
                                    && a.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                    join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                                    on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                    where b.Active == true
                                    && a.Active == true
                                    && b.FK_ReportArchiveTableId == reportID
                                    select a).Distinct().Count();

                            conf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                     join b in kPiDataContext.CollectedBasicParametersTable
                                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                     where b.Status == 4
                                     && b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                     && b.FK_ReportArchiveTable == reportID
                                     && a.Active == true
                                     join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                     on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                     where c.Active == true
                                     && c.FK_ReportArchiveTableId == reportID
                                     select a).Distinct().Count();

                            conf1 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                     join b in kPiDataContext.CollectedBasicParametersTable
                                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                     where b.Status == 5
                                     && a.Active == true
                                     && b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                     && b.FK_ReportArchiveTable == reportID
                                     join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                     on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                     where c.Active == true
                                     && c.FK_ReportArchiveTableId == reportID
                                     select a).Distinct().Count();

                            toconf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                       join b in kPiDataContext.CollectedBasicParametersTable
                                       on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                       where b.Status == 3
                                       && a.Active == true
                                       && b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                       && b.FK_ReportArchiveTable == reportID
                                       join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                        on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                       where c.Active == true
                                       && c.FK_ReportArchiveTableId == reportID
                                       select a).Distinct().Count();

                            started0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                        join b in kPiDataContext.CollectedBasicParametersTable
                                        on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                        where
                                        b.FK_SecondLevelSubdivisionTable == secondLevelItem.SecondLevelSubdivisionTableID
                                        && b.CollectedValue != null
                                        && a.Active == true
                                        && b.FK_ReportArchiveTable == reportID
                                        join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                         on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                        where c.Active == true
                                        && c.FK_ReportArchiveTableId == reportID
                                        select a).Distinct().Count() - conf0 - toconf0 - conf1;

                            notstarted0 = all0 - toconf0 - started0 - conf0 - conf1;

                            #endregion
                            i++;
                            int par2 = i;
                            //tmp2 = secondLevelItem.Name;

                            tmp2 = getColoredName(all0, conf1, conf0, toconf0, started0, notstarted0, secondLevelItem.Name);
                            if (tmp2 != null)
                                list.Add(new MyObject() { Id = i, ParentId = par1, Name = tmp2 + " : " + all0.ToString() + "/" + conf1.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                            foreach (ThirdLevelSubdivisionTable thirdLevelItem in thirdLevelList)//по кафедре
                            {
                                #region
                                all0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                        where a.Active == true
                                        && a.ThirdLevelSubdivisionTableID == thirdLevelItem.ThirdLevelSubdivisionTableID
                                        join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                                        on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                        where b.Active == true
                                        && a.Active == true
                                        && b.FK_ReportArchiveTableId == reportID
                                        select a).Distinct().Count();

                                conf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                         join b in kPiDataContext.CollectedBasicParametersTable
                                         on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                         where b.Status == 4
                                         && a.Active == true
                                         && b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                         && b.FK_ReportArchiveTable == reportID
                                         join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                         on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                         where c.Active == true
                                         && c.FK_ReportArchiveTableId == reportID
                                         select a).Distinct().Count();

                                conf1 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                         join b in kPiDataContext.CollectedBasicParametersTable
                                         on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                         where b.Status == 5
                                         && a.Active == true
                                         && b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                         && b.FK_ReportArchiveTable == reportID
                                         join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                            on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                         where c.Active == true
                                         && c.FK_ReportArchiveTableId == reportID
                                         select a).Distinct().Count();

                                toconf0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                           join b in kPiDataContext.CollectedBasicParametersTable
                                           on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                           where b.Status == 3
                                           && a.Active == true
                                           && b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                           && b.FK_ReportArchiveTable == reportID
                                           join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                            on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                           where c.Active == true
                                           && c.FK_ReportArchiveTableId == reportID
                                           select a).Distinct().Count();

                                started0 = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                            join b in kPiDataContext.CollectedBasicParametersTable
                                            on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                            where
                                            b.FK_ThirdLevelSubdivisionTable == thirdLevelItem.ThirdLevelSubdivisionTableID
                                            && a.Active == true
                                            && b.FK_ReportArchiveTable == reportID
                                            && b.CollectedValue != null
                                            join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                            on a.ThirdLevelSubdivisionTableID equals c.FK_ThirdLevelSubdivisionTable
                                            where c.Active == true
                                            && c.FK_ReportArchiveTableId == reportID
                                            select a).Distinct().Count() - toconf0 - conf0 - conf1;

                                notstarted0 = all0 - toconf0 - started0 - conf0 - conf1;
                                #endregion
                                i++;
                                int par3 = i;
                               // tmp2 = thirdLevelItem.Name;

                                tmp2 = getColoredName(all0, conf1, conf0, toconf0, started0, notstarted0, thirdLevelItem.Name);
                                if (tmp2 != null)
                                    list.Add(new MyObject() { Id = i, ParentId = par2, Name = tmp2 + " : " + all0.ToString() + "/" + conf1.ToString() + "/" + conf0.ToString() + "/" + toconf0.ToString() + "/" + started0.ToString() + "/" + notstarted0.ToString() });
                            }
                        }
                    }
                }
                BindTree(list, null);
                TreeView1.CollapseAll();
            }
            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class FillOnlyCheckBoxes : System.Web.UI.Page
    {
       
        public class MyObject
        {
            public int Id;
            public int ParentId;
            public string Name;
            public bool Checked;
        }
        private void BindTree(IEnumerable<MyObject> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));

            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.Name, node.Id.ToString());
                newNode.Checked = node.Checked;
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
        public class FourthLevelStruct
        {
            public int firstLevelId;
            public string firstLevelName;

            public int secondLevelId;
            public string secondLevelName;

            public int thirdLevelId;
            public string thirdLevelName;

            public int fourthLevelId;
            public string fourthLevelName;

            public bool isChecked;
        }
        public List<FourthLevelStruct> GetFourthLevelStructs(int reportId)
        {
            #region
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<FourthLevelStruct> fourthLevelToFillInReport =
                (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                    where a.Active == true

                    join b in kpiWebDataContext.ThirdLevelSubdivisionTable
                        on a.FK_ThirdLevelSubdivisionTable equals b.ThirdLevelSubdivisionTableID
                    where b.Active == true

                    join c in kpiWebDataContext.SecondLevelSubdivisionTable
                        on b.FK_SecondLevelSubdivisionTable equals c.SecondLevelSubdivisionTableID
                    where c.Active == true

                    join d in kpiWebDataContext.FirstLevelSubdivisionTable
                        on c.FK_FirstLevelSubdivisionTable equals d.FirstLevelSubdivisionTableID
                    where d.Active == true

                    join f in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                        on d.FirstLevelSubdivisionTableID equals f.FK_FirstLevelSubmisionTableId
                    where f.Active == true
                          && f.FK_SecondLevelSubdivisionTable == c.SecondLevelSubdivisionTableID
                          && f.FK_ThirdLevelSubdivisionTable == b.ThirdLevelSubdivisionTableID
                          && f.FK_ReportArchiveTableId == reportId
                          
                    join g in kpiWebDataContext.FourthLevelParametrs
                        on a.FourthLevelSubdivisionTableID equals g.FourthLevelParametrsID

                        join h in kpiWebDataContext.SpecializationTable
                        on a.FK_Specialization equals  h.SpecializationTableID
                    select new FourthLevelStruct
                    {
                        firstLevelId = d.FirstLevelSubdivisionTableID,
                        firstLevelName = d.Name,
                        fourthLevelId = a.FourthLevelSubdivisionTableID,
                        fourthLevelName = a.Name+" "+h.SpecializationNumber,
                        isChecked = (bool) g.IsNetworkComunication,
                        secondLevelId = c.SecondLevelSubdivisionTableID,
                        secondLevelName = c.Name,
                        thirdLevelId = b.ThirdLevelSubdivisionTableID,
                        thirdLevelName = b.Name
                    }).Distinct().OrderBy(x => x.firstLevelName).ToList();
            return fourthLevelToFillInReport;

            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            MainFunctions mainFunctions = new MainFunctions();
            CheckBoxesToShow checkBoxesToShow = new CheckBoxesToShow();
            CollectedDataStatusProcess collectedDataStatusProcess = new CollectedDataStatusProcess();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userId = userSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userId);
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userId, 6, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            if (!checkBoxesToShow.CanUserEditCheckBoxNetwork(userId))
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            int reportId = Convert.ToInt32(mySession.ReportArchiveID);
            ReportArchiveTable report = mainFunctions.GetReportById(reportId);
            ReportNameLabel.Text = report.Name;
            //BasicParamName.Text = "123";
            if (!Page.IsPostBack)
            {
                List<MyObject> list = new List<MyObject>();
                List<FourthLevelStruct> fourthLevelToFillInReport = GetFourthLevelStructs(reportId); 
                List<int> firstLevelList = (from a in fourthLevelToFillInReport
                    select a.firstLevelId).Distinct().ToList();
                if ((collectedDataStatusProcess.DoesAllCollectedHaveNeededStatus(5, 0, 1, reportId, userId, true))&& (collectedDataStatusProcess.DoesAnyCollectedHaveNeededStatus(5, 0, 1, reportId, userId, true)))
                {
                    ClearAllCheckedButton.Enabled = false;
                    SaveChangesButton.Enabled = false;
                    TreeView1.Enabled = false;
                }
                List<int> fourthLevelToSave = new List<int>(); 

                int iD = 1;
                int parRoot = iD;
               // list.Add(new MyObject() { Id = iD, ParentId = 0, Name = "КФУ", Checked = false });
                iD++;
                foreach (int currentFirst in firstLevelList)
                {
                    int par0 = iD;              
                    string tmp0 = (from a in fourthLevelToFillInReport where a.firstLevelId == currentFirst select a.firstLevelName).FirstOrDefault();
                    list.Add(new MyObject() { Id = iD, ParentId = 0, Name = tmp0, Checked = false });
                    iD++;
                    List<int> secondLevelList = (from a in fourthLevelToFillInReport where a.firstLevelId == currentFirst select a.secondLevelId).Distinct().ToList();
                    foreach (int currentSecond in secondLevelList)
                    {
                        int par1 = iD;                     
                        string tmp1 = (from a in fourthLevelToFillInReport where a.secondLevelId == currentSecond select a.secondLevelName).FirstOrDefault();
                        list.Add(new MyObject() { Id = iD, ParentId = par0, Name = tmp1, Checked = false });
                        iD++;
                        List<int> fourthLevelList = (from a in fourthLevelToFillInReport where a.secondLevelId == currentSecond select a.fourthLevelId).Distinct().ToList();
                        foreach (int currentFourth in fourthLevelList)
                        {                          
                            string tmp2 =(from a in fourthLevelToFillInReport where a.fourthLevelId == currentFourth select a.fourthLevelName).FirstOrDefault();
                            bool tmp2Checked = (from a in fourthLevelToFillInReport  where a.fourthLevelId == currentFourth select a.isChecked).FirstOrDefault();
                            list.Add(new MyObject() { Id = currentFourth, ParentId = par1, Name = tmp2, Checked = tmp2Checked });

                            fourthLevelToSave.Add(currentFourth);
                            iD++;
                        }
                    }
                }
                ViewState["fourthLevelToSave"] = fourthLevelToSave;
                BindTree(list, null);
                TreeView1.CollapseAll();
            }
        }
        public TreeNode FindNodeByValue(string value,TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Value == value)
                    return node;
                if (node.ChildNodes.Count > 0)
                {
                    TreeNode newNode = FindNodeByValue(value, node.ChildNodes);
                    if (newNode!=null)
                    {
                        return newNode;
                    }
                }
            }
            return null;
        }
        public bool UncheckAllCheckboxes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = false;
                if (node.ChildNodes.Count > 0)
                {
                    UncheckAllCheckboxes(node.ChildNodes);
                }
            }
            return true;
        }
        public bool SaveAll()
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int reportId = Convert.ToInt32(mySession.ReportArchiveID);
           /* List<FourthLevelStruct> fourthLevelToFillInReport = GetFourthLevelStructs(reportId);
            fourthLevelToSave*/
            List<int> fourthLevelToSave = (List<int>) ViewState["fourthLevelToSave"];
            foreach (int currentFourthId in fourthLevelToSave)
            {
                TreeNode currentNode = FindNodeByValue(currentFourthId.ToString(), TreeView1.Nodes);
                FourthLevelParametrs currentFourthparam = (from a in kpiWebDataContext.FourthLevelParametrs
                    where a.FourthLevelParametrsID == currentFourthId
                    select a).FirstOrDefault();
                if (currentNode == null)
                    continue;
                if (currentNode.Checked != currentFourthparam.IsNetworkComunication)
                {
                    FourthLevelParametrs dbCurrentFourthParam = (from a in kpiWebDataContext.FourthLevelParametrs
                                                                 where a.FourthLevelParametrsID == currentFourthId
                                                                 select a).FirstOrDefault();
                    if (dbCurrentFourthParam != null)
                    {
                        dbCurrentFourthParam.IsNetworkComunication = currentNode.Checked;
                        kpiWebDataContext.SubmitChanges();
                    }
                }
            }
            MainFunctions mainFunctions = new MainFunctions();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userId = userSer.Id;
            AutoCalculateAfterSave autoCalculateAfterSave = new AutoCalculateAfterSave();
            autoCalculateAfterSave.AutoCalculate(reportId,userId,1,0,null,0);

            return true;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SaveAll();
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseReport.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void ClearAllCheckedButton_Click(object sender, EventArgs e)
        {
            UncheckAllCheckboxes(TreeView1.Nodes);
        }
        protected void ExpandAllButton_Click(object sender, EventArgs e)
        {
            TreeView1.ExpandAll();
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class ProcessHistory : System.Web.UI.Page
    {
        ProcessMainFucntions _main = new ProcessMainFucntions();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int processId = -1;
            int archiveVersion = -1;

            var procIdStr = HttpContext.Current.Session["processID"];
            if (procIdStr == null)
                Response.Redirect("Dashboard.aspx");
            Int32.TryParse(procIdStr.ToString(), out processId);
            int.TryParse(HttpContext.Current.Session["archiveVersion"].ToString(), out archiveVersion);
            if (processId < 0)
                Response.Redirect("Dashboard.aspx");

            Processes currentProcess = _main.GetProcessById(processId);

            ProcessNameLabel.Text = currentProcess.name;
            ProcessTypeLabel.Text = _main.GetProcessTypeNameByType(currentProcess.type);
            StartDateLebel.Text = currentProcess.startDate.ToString();
            EndDateLabel.Text = currentProcess.endDate.ToString();
            InitiatorLabel.Text = _main.GetUserById(currentProcess.fk_initiator).name;

            
            historyTableDiv.Controls.Add(_main.GetHistoryTable(archiveVersion,processId));
        }

        protected void goBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}
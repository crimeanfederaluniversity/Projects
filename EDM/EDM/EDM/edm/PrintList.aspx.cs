using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public partial class PrintList : System.Web.UI.Page
    {
        EDMdbDataContext _edmDb = new EDMdbDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {


            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int processId = (int) HttpContext.Current.Session["processID"];
            ProcessMainFucntions main = new ProcessMainFucntions();
            ProcessVersions LAstVersion = main.GetLastVersionInProcess(processId);
            List<Steps> lastVersionSteps = main.GetStepsInProcessVersion(LAstVersion.processVersionID);

            Table newTable = new Table();
            foreach(Steps step in lastVersionSteps)
            {

                TableRow newRow = new TableRow();
                TableCell newCell1 = new TableCell();
                TableCell newCell2 = new TableCell();
                TableCell newCell3 = new TableCell();

                Participants participant = (from a in _edmDb.Participants
                                            where a.participantID == step.fk_participent
                                            select a).FirstOrDefault();

                newCell1.Text = main.GetUserById(participant.fk_user).name;
                newCell2.Text = step.date.ToString().Split(' ')[0];
                newCell3.Text = step.comment;

                newRow.Cells.Add(newCell1);

                newRow.Cells.Add(newCell2);

                newRow.Cells.Add(newCell3);

                newTable.Rows.Add(newRow);

            }
            newTable.Width = 800;
            PrintMainDiv.Controls.Add(newTable);


        }
    }
}
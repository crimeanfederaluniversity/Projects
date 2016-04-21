using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public class OtherFuncs
    {
        EDMdbDataContext dc = new EDMdbDataContext();
        public List<string> Notification(int userId)
        {
            List<string> notifications = new List<string>()
            {
                string.Empty,
                string.Empty
            };
            
            #region Исходящие

            int userNotFinishedProcessesCount =
                (from a in dc.Processes
                 where  a.active == true == true && a.fk_initiator == userId && a.status == 1
                 select a).Count();
            if (userNotFinishedProcessesCount > 0)
                notifications[0] = " (" + userNotFinishedProcessesCount + ")";
               
            #endregion Исходящие
            
            #region Входящие

            List<int> userIsNewParticipant =
                (from a in dc.Participants
                 where  a.active == true && a.fk_user == userId && a.isNew == true
                 select a.fk_process)
                    .Distinct().ToList();

            if (userIsNewParticipant.Any())
            {
                List<Processes> newProcess = (from p in userIsNewParticipant
                                  join proc in dc.Processes on p equals proc.processID
                                  where proc.active && proc.status == 0
                                  select proc).ToList();
                int newProcessCount = 0;

                foreach (Processes proc in newProcess)
                {
                    int currentQueue = (from a in dc.Participants
                                        where  a.active == true && a.fk_process == proc.processID && a.fk_user == userId
                                        select a.queue).FirstOrDefault();

                    if ((currentQueue == 0))
                    {
                        newProcessCount++;
                    }
                    else
                    {
                        int procMaxVersion =
                            (from a in dc.ProcessVersions
                             where a.fk_process == proc.processID &&  a.active == true
                             select a)
                                .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                        int stepsCount =
                            (from a in dc.Steps
                             where a.fk_processVersion == procMaxVersion &&  a.active == true
                             select a).Count();

                        if (currentQueue == stepsCount)
                        {
                            newProcessCount++;
                        }
                    }
                }

                if (newProcessCount > 0)
                {
                    notifications[1] = " (" + newProcessCount + ")";
                }
            }
            #endregion Входящие
            
            return notifications;
        }

        public string NullableDateTimeToText(DateTime? date)
        {
            if (date == null)
                return "";
            DateTime newDate;
            DateTime.TryParse(date.ToString(), out newDate);
            return newDate.ToShortDateString();
        }

        public string GetTimeBeforeEnd(DateTime endDate)
        {
            double hours = ((endDate - DateTime.Now).TotalHours);
            if (hours < 1)
            {
                return "Менее часа";
            }
            int hoursRownd = (int)Math.Round(hours);
            if (hoursRownd < 24)
            {
                if (hoursRownd == 1 || hoursRownd == 21)
                    return hoursRownd + " час";
                if (hoursRownd == 2 || hoursRownd == 3 || hoursRownd == 4 || hoursRownd == 22 || hoursRownd == 23 || hoursRownd == 24)
                    return hoursRownd + " часа";

                return hoursRownd + " часов";
            }
            else {
                int days = (int)Math.Round((double)(hoursRownd / 24));
                if (days == 1 || days == 21)
                    return days + " день";
                if (days == 2 || days == 3 || days == 4 || days == 22 || days == 23 || days == 24)
                    return days + " дня";
                return days + " дней";
            }
            return "";
        }

        public List<int> GetSlaves(int bossStuctId, ref List<int> slavesStructId)
        {
            List<int> result = new List<int>();

            var slavesByStuct =
                (from a in dc.Struct where  a.active == true && a.fk_parent == bossStuctId select a).ToList();

                if (slavesByStuct.Any())
                {
                    slavesStructId.AddRange((slavesByStuct.Select(id => id.structID)));

                    foreach (var slave in slavesByStuct)
                        GetSlaves(slave.structID, ref slavesStructId);
                }

                    foreach (var itm in slavesStructId)
                        result.AddRange((from a in dc.Users where  a.active == true && a.fk_struct == itm select a.userID).ToList());

                    return result;
        }

        public void DocAddmd5AndUser(int docId, string path, int user)
        {
            Documents doc = (from a in dc.Documents where  a.active == true && a.documentID == docId select a).FirstOrDefault();

            if (doc != null)
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(path))
                    {
                    doc.md5= BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                    doc.userDownload = user;
                    }
                }
                dc.SubmitChanges();
            }
        }

        public string GetMd5FromFile(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        public int GetProcMaxVersion(int procId)
        {
            return (from a in dc.ProcessVersions where  a.active == true && a.fk_process == procId select a)
                .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();
        }

        public void Md5Check(Page pg, FileUpload fu)
        {
            OtherFuncs of = new OtherFuncs();
            string directoryToSave = HttpContext.Current.Server.MapPath("~/edm/documents/tmp/");
            if (!Directory.Exists(directoryToSave))
            {
                Directory.CreateDirectory(directoryToSave);
            }

            if (fu.PostedFile.FileName.Any() && fu.HasFile && fu.PostedFile.ContentLength < 35243512)
            {
                fu.SaveAs(directoryToSave + fu.FileName);
                var res = of.GetMd5FromFile(directoryToSave + fu.FileName);

                if (File.Exists(directoryToSave + fu.FileName))
                {
                    File.Delete(directoryToSave + fu.FileName);
                }

                //PORTCUT  ScriptManager.RegisterStartupScript(pg, pg.GetType(), "err_msg", "prompt('MD5: \\n\\r  ( нажмите CTRL+C чтобы скопировать )', '" + res + "');", true);
            }
            else
            {
                //PORTCUT ScriptManager.RegisterStartupScript(pg, pg.GetType(), "err_msg", "alert('Ошибка выполнения');",true);
                //throw new Exception(); // !!!!!!!!!!!!!!!!!!!!!!!!
            }
        }

        public List<DateTime> GetDatesBetween(DateTime startD, DateTime endD)
        {
            var dates = new List<DateTime>();

            for (var dt = startD; dt <= endD; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates;
        }

        public int GetSecToRefresh()
        {

            var now = DateTime.Now;
            var nownext = new DateTime(now.Year,now.Month,now.Day,17,1,0);
            var next = new DateTime(now.AddDays(1).Year, now.AddDays(1).Month,now.AddDays(1).Day,17,1,0);

            return now.Hour < 17 ? (int)(nownext - now).TotalSeconds : (int)(next - now).TotalSeconds;
        }
    }
}
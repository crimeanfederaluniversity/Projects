using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace EDM.edm
{
    public class OtherFuncs
    {
        public List<string> Notification(object userId)
        {
            List<string> notifications = new List<string>()
            {
                string.Empty,
                string.Empty
            };

            EDMdbDataContext dataContext = new EDMdbDataContext();

            #region Исходящие

            int userNotFinishedProcessesCount =
                (from a in dataContext.Processes
                 where a.active && a.fk_initiator == (int)userId && a.status == 1
                 select a).Count();
            if (userNotFinishedProcessesCount > 0)
                notifications[0] = " (" + userNotFinishedProcessesCount + ")";

            #endregion Исходящие

            #region Входящие

            var userIsNewParticipant =
                (from a in dataContext.Participants
                 where a.active && a.fk_user == (int)userId && a.isNew == true
                 select a.fk_process)
                    .Distinct().ToList();

            if (userIsNewParticipant.Any())
            {
                var newProcess = (from p in userIsNewParticipant
                                  join proc in dataContext.Processes on p equals proc.processID
                                  where proc.active && proc.status == 0
                                  select proc).ToList();
                int newProcessCount = 0;

                foreach (var proc in newProcess)
                {
                    var currentQueue = (from a in dataContext.Participants
                                        where a.active && a.fk_process == proc.processID && a.fk_user == (int)userId
                                        select a.queue).FirstOrDefault();

                    if ((currentQueue == 0))
                    {
                        newProcessCount++;
                    }
                    else
                    {
                        int procMaxVersion =
                            (from a in dataContext.ProcessVersions
                             where a.fk_process == proc.processID && a.active
                             select a)
                                .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                        var stepsCount =
                            (from a in dataContext.Steps
                             where a.fk_processVersion == procMaxVersion && a.active
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
            DateTime dateNow = DateTime.Now;
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

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EDM.edm
{
    public class Approvment
    {
        public void AddApprove(int user, int procVersion, Page pg)
        {
            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                Steps step = new Steps();
                var proc =
                    (from a in dataContext.ProcessVersions
                        where a.active && a.processVersionID == procVersion
                        select a.fk_process).FirstOrDefault();
                var fkParticipant =
                    (from a in dataContext.Participants
                        where a.active && a.fk_user == user && a.fk_process == proc
                        select a.participantID).FirstOrDefault();

        
                ProcessVersions procVer =
                    (from b in dataContext.ProcessVersions where b.active && b.processVersionID == procVersion select b)
                        .FirstOrDefault();

                step.active = true;
                step.fk_processVersion = procVersion;
                step.fk_participent = fkParticipant;
                step.stepResult = 1;
                if (procVer != null) { procVer.status = "Утвержден пользователем: "+ (from a in dataContext.Users where a.userID == user select a.name).FirstOrDefault()+" "+ DateTime.Now.ToShortDateString(); } else throw new Exception("Не возможно присвоить версии процесса в статус 1. Скорее всего он не существует");
                step.date = DateTime.Now;

                dataContext.Steps.InsertOnSubmit(step);
                dataContext.SubmitChanges();

            }

            CheckApprove(procVersion);

            pg.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Согласованно!');</script>");
        }

        public void RejectApprove(int user, int procVersion, string comment, Page pg)
        {
            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                int procId =
                        (from v in dataContext.ProcessVersions
                         where v.active && v.processVersionID == procVersion
                         select v.fk_process).FirstOrDefault();
                var fkParticipant =
                    (from a in dataContext.Participants
                     where a.active && a.fk_user == user && a.fk_process == procId
                     select a.participantID).FirstOrDefault();

                Steps step = new Steps();
                Processes process =
                    (from a in dataContext.Processes where a.active && a.processID == procId select a).FirstOrDefault();

                ProcessVersions procVer =
                    (from b in dataContext.ProcessVersions where b.active && b.processVersionID == procVersion select b)
                        .FirstOrDefault();

                step.active = true;
                step.fk_processVersion = procVersion;
                step.fk_participent = fkParticipant;
                step.comment = comment;
                step.stepResult = -2;
                if (process != null && procVer != null) { process.status = -2; procVer.status="Возвращен на доработку пользователем: " + (from a in dataContext.Users where a.userID == user select a.name).FirstOrDefault() + " " + DateTime.Now.ToShortTimeString();  } else throw new Exception("Не возможно вернуть процесс в статус -2. Скорее всего он не существует");
                step.date = DateTime.Now;


                dataContext.Steps.InsertOnSubmit(step);
                dataContext.SubmitChanges();
            }
            pg.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> alert('Отправленно на доработку!');</script>");
        }

        private void CheckApprove(int procVersion)
        {

            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                int procId =
                         (from v in dataContext.ProcessVersions
                          where v.active && v.processVersionID == procVersion
                          select v.fk_process).FirstOrDefault();

                ProcessVersions procVer = 
                            (from b in dataContext.ProcessVersions
                             where b.active && b.processVersionID == procVersion
                             select b).FirstOrDefault();

                var steps = (from s in dataContext.Steps where s.active && s.fk_processVersion == procVersion select s).ToList();

                var stepsCount = steps.Count();

                int participantsProcessCount = (from p in dataContext.Participants where p.active && p.fk_process == procId select p).Count();

                var stepsApproved = steps.Count(ap => ap.stepResult == 1); // ЗАМЕНИТЬ 1 на согласованный

                // если все steps status == 1 для всех пользователей, апрувим процесс.
                if (participantsProcessCount == stepsApproved)
                {
                    Processes proces =
                        (from p in dataContext.Processes where p.active && p.processID == procId select p)
                            .FirstOrDefault();

                    if (proces != null)
                    {
                        if (proces.status != 0)
                        {
                            throw new Exception("Статус процесса равен не 0");
                        }

                        if (procVer != null) procVer.status = "Процесс согласован всеми участниками"; else throw new Exception("Не возможно вернуть процесс в статус 1. Скорее всего он не существует");
                        proces.status = 1;
                        dataContext.SubmitChanges();

                        // Можно добавить рассылку Email-ов об успешном согласовании

                    }
                    else
                    {
                        throw new Exception("Процесс не найден");
                    }

                }



            }
        }



    }
}
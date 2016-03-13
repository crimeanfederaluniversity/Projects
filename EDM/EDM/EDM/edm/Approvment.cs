using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EDM.edm
{
    public class Approvment
    {
        public void AddApprove(int user, int procVersion, string comment, Page pg)
        {
            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                Steps step = new Steps();
                var procId =
                    (from a in dataContext.ProcessVersions
                        where a.active && a.processVersionID == procVersion
                        select a.fk_process).FirstOrDefault();
                var fkParticipant =
                    (from a in dataContext.Participants
                        where a.active && a.fk_user == user && a.fk_process == procId
                     select a.participantID).FirstOrDefault();
                var proc =
                    (from a in dataContext.Processes where a.active && a.processID == procId select a).FirstOrDefault();

                if (proc == null) throw new Exception("Процесс не найден");

                ProcessVersions procVer =
                    (from b in dataContext.ProcessVersions where b.active && b.processVersionID == procVersion select b)
                        .FirstOrDefault();
                step.active = true;
                step.fk_processVersion = procVersion;
                step.fk_participent = fkParticipant;
                step.comment = comment;
                step.stepResult = 1;
                step.date = DateTime.Now;

                dataContext.Steps.InsertOnSubmit(step);
                dataContext.SubmitChanges();

            }

            CheckApprove(procVersion,1,user);

            pg.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> confirm('Согласованно!');</script>");
            HttpContext.Current.Response.Redirect("Dashboard.aspx");
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
                step.date = DateTime.Now;


                dataContext.Steps.InsertOnSubmit(step);
                dataContext.SubmitChanges();
            }
            CheckApprove(procVersion, -2, user);

            pg.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "<script> confirm('Отправленно на доработку!');</script>");
            HttpContext.Current.Response.Redirect("Dashboard.aspx");

        }

        private void CheckApprove(int procVersion, int appType, int user)
        {

            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                int procId =
                         (from v in dataContext.ProcessVersions
                          where v.active && v.processVersionID == procVersion
                          select v.fk_process).FirstOrDefault();
                var proc =
                    (from a in dataContext.Processes where a.active && a.processID == procId select a).FirstOrDefault();

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

                        if (procVer != null) procVer.status = "Согласовано / "+ DateTime.Now.ToShortDateString(); else throw new Exception("Не возможно вернуть процесс в статус 1. Скорее всего он не существует");
                        proces.status = 1;
                        dataContext.SubmitChanges();

                        // Можно добавить рассылку Email-ов об успешном согласовании

                    }
                    else
                    {
                        throw new Exception("Процесс не найден");
                    }

                }
                else
                {
                    switch (appType)
                    {
                        case 1:
                        {
                            if (proc.type.Equals("review"))
                            {
                                int participantCount = (from a in dataContext.Participants
                                                        where a.active && a.fk_process == proc.processID
                                                        select a).Count();

                                if (procVer != null) procVer.status = "Обработано " + stepsCount + " рецензентами из " + participantCount + " / " + " " + DateTime.Now.ToShortDateString();
                                    else throw new Exception("Не возможно присвоить версии процесса в статус 1. Скорее всего он не существует");
                                }

                            else if (procVer != null) procVer.status = "Согласовано " + (from a in dataContext.Users where a.userID == user select a.name).FirstOrDefault() + " / " + DateTime.Now.ToShortDateString();
                                    else throw new Exception("Не возможно присвоить версии процесса в статус 1. Скорее всего он не существует");
                            }
                            break;



                        case -2:
                        {
                            if (proc != null && procVer != null)
                            {
                                proc.status = -2;
                                procVer.status = "Возвращено на доработку " + (from a in dataContext.Users where a.userID == user select a.name).FirstOrDefault() + " / " + DateTime.Now.ToShortDateString();
                                }
                            else throw new Exception("Не возможно вернуть процесс в статус -2. Скорее всего он не существует");
                        }
                            break;
                    }
                }
                dataContext.SubmitChanges();

            }
        }



    }
}
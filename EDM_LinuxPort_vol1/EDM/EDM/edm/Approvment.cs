﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EDM.edm
{
    public class Approvment
    {
        private LogHandler log = new LogHandler();
        public int AddApprove(int user, int procVersion, string comment, int commentType)
        {
            int stepId = 0;
            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                EmailFuncs ef = new EmailFuncs();
                Steps step = new Steps();
                var procId =
                    (from a in dataContext.ProcessVersions
                        where a.active == true && a.processVersionID == procVersion
                        select a.fk_process).FirstOrDefault();
                var fkParticipant =
                    (from a in dataContext.Participants
                        where a.active == true && a.fk_user == user && a.fk_process == procId
                     select a.participantID).FirstOrDefault();
                var proc =
                    (from a in dataContext.Processes where a.active == true && a.processID == procId select a).FirstOrDefault();

                if (proc == null) throw new Exception("Процесс не найден");

                step.active = true;
                step.fk_processVersion = procVersion;
                step.fk_participent = fkParticipant;
                step.comment = comment;
                step.commentType = commentType;
                step.stepResult = 1;
                step.date = DateTime.Now;

                dataContext.Steps.InsertOnSubmit(step);
                dataContext.SubmitChanges();
                stepId = step.stepID;
                // Отправка уведомления только для помледовательного
                if (proc.type == "serial")
                {
                    var currentParticipantQueue =
                        (from a in dataContext.Participants
                            where a.active == true && a.participantID == fkParticipant
                            select a.queue).FirstOrDefault();

                    var nextParticipant = (from a in dataContext.Participants
                        where a.active == true && a.fk_process == procId && a.queue == currentParticipantQueue + 1
                        select a).FirstOrDefault();

                    if (nextParticipant != null)
                    {
                        var usrId = nextParticipant.fk_user;
                        ef.StepApprove(procId, usrId);
                    }
                }

            }

            CheckApprove(procVersion,1,user);
            return stepId;
        }

        public int RejectApprove(int user, int procVersion, string comment)
        {
            int stepId = 0;
            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                int procId =
                        (from v in dataContext.ProcessVersions
                         where v.active == true && v.processVersionID == procVersion
                         select v.fk_process).FirstOrDefault();
                var fkParticipant =
                    (from a in dataContext.Participants
                     where a.active == true && a.fk_user == user && a.fk_process == procId
                     select a.participantID).FirstOrDefault();

                Steps step = new Steps();

                step.active = true;
                step.fk_processVersion = procVersion;
                step.fk_participent = fkParticipant;
                step.comment = comment;
                step.commentType = 2; // замечание
                step.stepResult = -2;
                step.date = DateTime.Now;
                
                dataContext.Steps.InsertOnSubmit(step);
                dataContext.SubmitChanges();
                stepId = step.stepID;
            }

            CheckApprove(procVersion, -2, user);
            return stepId;
        }

        private void CheckApprove(int procVersion, int appType, int user)
        {

            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                EmailFuncs ef = new EmailFuncs();

                int procId =
                         (from v in dataContext.ProcessVersions
                          where v.active == true && v.processVersionID == procVersion
                          select v.fk_process).FirstOrDefault();
                var proc =
                    (from a in dataContext.Processes where a.active == true && a.processID == procId select a).FirstOrDefault();

                ProcessVersions procVer = 
                            (from b in dataContext.ProcessVersions
                             where b.active == true && b.processVersionID == procVersion
                             select b).FirstOrDefault();

                var steps = (from s in dataContext.Steps where s.active == true && s.fk_processVersion == procVersion select s).ToList();

                var stepsCount = steps.Count();

                int participantsProcessCount = (from p in dataContext.Participants where p.active == true && p.fk_process == procId select p).Count();

                var stepsApproved = steps.Count(ap => ap.stepResult == 1); // ЗАМЕНИТЬ 1 на согласованный

                // если все steps status == 1 для всех пользователей, апрувим процесс.
                if (participantsProcessCount == stepsApproved)
                {
                    Processes proces =
                        (from p in dataContext.Processes where p.active == true && p.processID == procId select p)
                            .FirstOrDefault();

                    if (proces != null)
                    {
                        if (proces.status != 0)
                        {
                            log.AddError("Статус процесса "+ proces.processID+ " равен не 0");
                        }

                        if (procVer != null) procVer.status = "Согласовано / "+ DateTime.Now.ToShortDateString(); else throw new Exception("Не возможно вернуть процесс в статус 1. Скорее всего он не существует");
                        proces.status = 1;
                        dataContext.SubmitChanges();

                        ef.ApproveProcess(procId);// Можно добавить рассылку Email-ов об успешном согласовании

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
                                                        where a.active == true && a.fk_process == proc.processID
                                                        select a).Count();

                                if (procVer != null) procVer.status = "Обработано " + stepsCount + " рецензентами из " + participantCount + " / " + " " + DateTime.Now.ToShortDateString();
                                    else log.AddError("Не возможно присвоить версии процесса в статус 1. Скорее всего он не существует");
                                }

                            else if (procVer != null) procVer.status = "Согласовано " + (from a in dataContext.Users where a.userID == user select a.name).FirstOrDefault() + " / " + DateTime.Now.ToShortDateString();
                                    else log.AddError("Не возможно присвоить версии процесса в статус 1. Скорее всего он не существует");
                            }
                            break;



                        case -2:
                        {
                            if (proc != null && procVer != null)
                            {
                                proc.status = -2;
                                procVer.status = "Возвращено на доработку " + (from a in dataContext.Users where a.userID == user select a.name).FirstOrDefault() + " / " + DateTime.Now.ToShortDateString();
                                ef.RejectProcess(procId);// EMAIL
                             }
                            else log.AddError("Не возможно вернуть процесс в статус -2. Скорее всего он не существует");
                        }
                            break;
                    }
                }
                dataContext.SubmitChanges();

            }
        }

        public void ContinueApprove(int procId)
        {
            // Вызывать после создания новой версии ()
            using (EDMdbDataContext dc = new EDMdbDataContext())
            {
                List<Steps> stepsOld;

                var procVersions =
                    (from a in dc.ProcessVersions where a.active == true && a.fk_process == procId select a).OrderByDescending(
                        i => i.processVersionID).ToList();

                if (procVersions.Count > 1)
                {
                    var ss = procVersions[1].processVersionID;

                    stepsOld = (from a in dc.Steps
                        where a.active == true && a.fk_processVersion == procVersions[1].processVersionID
                        select a).ToList();

                    List<Participants> participantsNew =
                        (from a in dc.Participants where a.active == true && a.fk_process == procId select a).ToList();

                    foreach (Participants participant in participantsNew)
                    {
                        if ((from a in stepsOld where a.fk_participent == participant.participantID select a.stepResult).FirstOrDefault() != null)
                        {
                            if ((from a in stepsOld where a.fk_participent == participant.participantID select a.stepResult).FirstOrDefault() == 1)
                            {
                                Steps step = new Steps();

                                step.active = true;
                                step.fk_processVersion = procVersions[0].processVersionID;
                                step.fk_participent = participant.participantID;
                                step.comment = (from a in stepsOld where a.fk_participent == participant.participantID select a.comment).FirstOrDefault();
                                step.stepResult = 1;
                                step.date = (from a in stepsOld where a.fk_participent == participant.participantID select a.date).FirstOrDefault();

                                dc.Steps.InsertOnSubmit(step);
                                dc.SubmitChanges();
                            }
                            else
                            {
                                participant.isNew = true;
                                dc.SubmitChanges();
                            }
                        }
                        else
                        {
                            participant.isNew = true;
                            dc.SubmitChanges();
                        }
                    }
                }
            }
        }

        public void FinishApprove(int procId, string comment)
        {
            EmailFuncs ef = new EmailFuncs();

            using (EDMdbDataContext dataContext = new EDMdbDataContext())
            {
                Processes procToFinish = (from p in dataContext.Processes where p.active == true && p.processID == procId select p).FirstOrDefault();
                
                    ProcessMainFucntions main = new ProcessMainFucntions();
                    
                    if (procToFinish != null)
                    {
                        procToFinish.status = 10;
                        procToFinish.endComment = comment;
                        procToFinish.endDate = DateTime.Now;
                        dataContext.SubmitChanges();
                        if (procToFinish.fk_submitter != null)
                        {
                        Users submitter = main.GetUserById((int)procToFinish.fk_submitter);
                        EmailTemplates etmp =
                                (from a in dataContext.EmailTemplates
                                    where a.active == true && a.name == "youFinishedApprove"
                                    select a)
                                    .FirstOrDefault();

                            ef.SendEmail(submitter.email, etmp.emailTitle,
                                etmp.emailContent.Replace("#processName#",
                                    (from a in dataContext.Processes
                                        where a.active == true && a.processID == procId
                                        select a.name)
                                        .FirstOrDefault()), null); // Добавить attached 
                        }
                    }
                    else
                    {
                        log.AddError("Ошибка утверждения процесса № " + procId);
                        throw new Exception("Ошибка утверждения процесса № " + procId); //LOG
                    }
                }
            
        }
    }
}
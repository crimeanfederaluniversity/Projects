using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace EDM.edm
{
    public class EmailFuncs
    {
        EDMdbDataContext dc = new EDMdbDataContext();
        private int SendMailProcess(string smtpServer, int port, string from, string password,
            string mailto, string caption, string message, string attachFile)
        {
            EDMdbDataContext dataContext = new EDMdbDataContext();
            EmailCopies emailCopy = new EmailCopies();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, "Электронный документооборот КФУ");
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));

                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = port;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();

                EmailCopies emailCopy2 = new EmailCopies();
                emailCopy2.emailFrom = from;
                emailCopy2.emailTo = mailto;
                emailCopy2.emailTitle = caption;
                emailCopy2.emailContent = message;
                emailCopy2.emailAttachment = attachFile;
                emailCopy2.sendOk = true;
                emailCopy2.sendDateTime = DateTime.Now;
                dataContext.EmailCopies.InsertOnSubmit(emailCopy2);
                dataContext.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {              
                emailCopy.emailFrom = from;
                emailCopy.emailTo = mailto;
                emailCopy.emailTitle = caption;
                emailCopy.emailContent = message;
                emailCopy.emailAttachment = attachFile;
                emailCopy.sendOk = false;
                emailCopy.sendDateTime = DateTime.Now;
                emailCopy.errorMessage = ex.ToString();
                dataContext.EmailCopies.InsertOnSubmit(emailCopy);
                dataContext.SubmitChanges();
                return 0;
            }


            


            
        }
        public int SendEmail(string emailto, string caption, string message, string attachFile)
        {
            int errors = 0;

            EDMdbDataContext dataContext = new EDMdbDataContext();

            List<EmailBots> emails = (from a in dataContext.EmailBots select a).ToList();
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(message);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("Техническая поддержка: +7-978-117-53-98");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("E-mail адрес: it.cfu@ya.ru");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("С уважением администрация \"КФУ-Электронный документооборот\"");

            foreach (var ems in emails)
            {
                if ( SendMailProcess(ems.smtpName, 587, ems.email, ems.password, emailto, caption,
                        messageBuilder.ToString(), attachFile) == 1)
                {
                    ems.sendOk++;
                    break;
                }
                ems.sendError++;
                errors++;
            }

            dataContext.SubmitChanges();

            return errors;
        }
        public string EmailContentAutoReplace(string content, int?procId,int? userToSendId)
        {
            string tmpResult = content;
            ProcessMainFucntions main = new ProcessMainFucntions();
            string procName = "";
            string userName = "";
            string sexName = "ый(ая)";
            string initiatorName = "";
            if (procId != null)
            {
                Processes process = (from a in dc.Processes where a.processID == procId select a).FirstOrDefault();
                if (process != null)
                {
                    initiatorName = main.GetUserById(process.fk_initiator).name;
                    procName = process.name;
                }
            }
            if (userToSendId != null)
            {
                Users user = (from a in dc.Users where a.userID == userToSendId select a).FirstOrDefault();
                if (user != null)
                {
                    if (user.sex == false)
                    {
                        sexName = "ый";
                    }
                    else
                    {
                        sexName = "ая";
                    }
                    userName = main.GetOnlyIONameById((int)userToSendId);               
                }
            }

            tmpResult = tmpResult.Replace("#linkToWebSite#", " http://edm.cfu-portal.ru/ ");
            tmpResult = tmpResult.Replace("#userName#",  userName );
            tmpResult = tmpResult.Replace("#pol#", sexName);
            tmpResult = tmpResult.Replace("#initiator#",  "инициатор - "+initiatorName);
            tmpResult = tmpResult.Replace("#processName#", " " + procName + " ");
            return tmpResult;
        }
        public void StartProcess(int procId)
        {
            int currentQueue = 0;
            int procMaxVersion =
                (from a in dc.ProcessVersions where a.fk_process == procId && a.active select a)
                    .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();
            var steps = (from a in dc.Steps where a.active && a.fk_processVersion == procMaxVersion select a).ToList(); // для AutoAccept() ContinueAccept


            Users initiator = (from a in dc.Processes
                               where a.active && a.processID == procId
                               join b in dc.Users on a.fk_initiator equals b.userID
                               select b).FirstOrDefault();

                

           
            EmailTemplates etmpInit =
                (from i in dc.EmailTemplates where i.active && i.name == "youStartedProcess" select i).FirstOrDefault();
            SendEmail(initiator.email, etmpInit.emailTitle, EmailContentAutoReplace(etmpInit.emailContent,procId, initiator.userID), null);

            if (steps.Count > 0) // для AutoAccept() ContinueAccept
            {       
                    currentQueue = (from a in dc.Participants // ПРОТЕСТИТЬ
                    where a.active && a.fk_process == procId
                    join b in dc.Steps on a.participantID equals b.fk_participent
                    where b.active && b.fk_processVersion == procMaxVersion
                    select a).OrderByDescending(q => q.queue).Select(q => q.queue).FirstOrDefault();

                if (currentQueue > 0) currentQueue += 1; // для последовательного, в остальных всегда 0
            }

            
            var participantsZero =
                (from a in dc.Participants where a.active && a.fk_process == procId && a.queue == currentQueue && a.isNew == true select a.fk_user)
                    .ToList();

            if (participantsZero.Any())
            {
                foreach (var usrId in participantsZero)
                {
                    Users user =
                        (from a in dc.Users where a.active && a.userID == usrId select a).FirstOrDefault();
                    EmailTemplates etmp =
                        (from i in dc.EmailTemplates where i.active && i.name == "yourStep" select i).FirstOrDefault();
                    SendEmail(user.email, etmp.emailTitle, EmailContentAutoReplace(etmp.emailContent,procId, user.userID), null);
                }
            }
            else
            {
                // LOG
            }

        }
        public void ApproveProcess(int procId)
        {          
            string processName = "";
           /* Processes process = (from a in dc.Processes where a.processID == procId select a).FirstOrDefault();
            if (process != null)
            {
                processName = process.name;
            }
            */
            Users initiator =
                (from a in dc.Processes
                    where a.active && a.processID == procId
                    join b in dc.Users on a.fk_initiator equals b.userID
                    select b).FirstOrDefault();

            EmailTemplates etmpInit =
                (from i in dc.EmailTemplates where i.active && i.name == "yourProcessApproved" select i).FirstOrDefault();
            SendEmail(initiator.email, etmpInit.emailTitle, EmailContentAutoReplace(etmpInit.emailContent, procId,initiator.userID), null);
        }
        public void RejectProcess(int procId)
        { 
            Users initiator =
                (from a in dc.Processes
                    where a.active && a.processID == procId
                    join b in dc.Users on a.fk_initiator equals b.userID
                    select b).FirstOrDefault();

            EmailTemplates etmpInit =
                (from i in dc.EmailTemplates where i.active && i.name == "yourProcessCameBack" select i).FirstOrDefault();
            SendEmail(initiator.email, etmpInit.emailTitle, EmailContentAutoReplace(etmpInit.emailContent,procId, initiator.userID), null);
        }
        public void StepApprove(int procId, int userId)
        {
            // Отправка следующему для serial
            var userEmail =
                (from a in dc.Users where a.active && a.userID == userId select a.email).FirstOrDefault();
            EmailTemplates etmp =
                (from i in dc.EmailTemplates where i.active && i.name == "yourStep" select i).FirstOrDefault();
            SendEmail(userEmail, etmp.emailTitle, EmailContentAutoReplace(etmp.emailContent, procId, userId), null);
        }
        public void StepReject(int procId, int userId)
        {
            //// не вижу смысла так спамить :)
        }
        public void AutoAccept()
        {
            Approvment approve = new Approvment();
            LogHandler log = new LogHandler();

            var participantProcessDebt =
                (from a in dc.Participants where a.active && a.dateEnd < DateTime.Now
                    join b in dc.Processes on a.fk_process equals b.processID
                    where b.active && b.status == 0
                 select new {a.fk_user, a.fk_process, a.participantID}).ToList();

            foreach (var userProc in participantProcessDebt)
            {
                int procMaxVersion =
                    (from a in dc.ProcessVersions where a.fk_process == userProc.fk_process && a.active select a)
                     .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                // Проверить есть ли запись в Step для этого пользователя. Значит он утвердил/отказал уже если нет, то Автоапрувим.
                var stepExsist = (from a in dc.Steps
                                  where
                                      a.fk_participent == userProc.participantID && a.fk_processVersion == procMaxVersion && a.active
                                  select a).FirstOrDefault();

                if (stepExsist == null)
                {
                    string processName = "";
                    EmailTemplates etmp = (from i in dc.EmailTemplates where i.active && i.name == "yourStepAutoSubmitted" select i).FirstOrDefault();

                    Processes process =
                        (from a in dc.Processes where a.processID == userProc.fk_process select a).FirstOrDefault();
                    if (process != null)
                    {
                        processName = process.name;
                    }

                    var userEmail =
                        (from a in dc.Users where a.active && a.userID == userProc.fk_user select a.email)
                            .FirstOrDefault();

                    if (userEmail != null)
                        SendEmail(userEmail, etmp.emailTitle, EmailContentAutoReplace(etmp.emailContent, userProc.fk_process, userProc.fk_user),null);


                    approve.AddApprove(userProc.fk_user, procMaxVersion,
                        "Процесс согласован автоматически / " + DateTime.Now.ToShortDateString() + " " +
                        DateTime.Now.ToShortTimeString(),2);
                    log.AddInfo("Автоутверждение процесса: "+process?.processID+" в версии: "+procMaxVersion+", для участника с id = "+ userProc.fk_user);
                }

            }

        }
    }
}
﻿using System;
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
            }
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


            return 0;
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
                if (
                    SendMailProcess(ems.smtpName, 587, ems.email, ems.password, emailto, caption,
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

        public void StartProcess(int procId)
        {
            EDMdbDataContext dc = new EDMdbDataContext();
            int currentQueue = 0;
            int procMaxVersion =
                (from a in dc.ProcessVersions where a.fk_process == procId && a.active select a)
                    .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();
            var steps = (from a in dc.Steps where a.active && a.fk_processVersion == procMaxVersion select a).ToList(); // для AutoAccept()


            var initiatorEmail =
                (from a in dc.Processes
                    where a.active && a.processID == procId
                    join b in dc.Users on a.fk_initiator equals b.userID
                    select b.email).FirstOrDefault();

            EmailTemplates etmpInit =
                (from i in dc.EmailTemplates where i.active && i.name == "youStartedProcess" select i).FirstOrDefault();
            SendEmail(initiatorEmail, etmpInit.emailTitle, etmpInit.emailContent, null);

            if (steps.Count > 0) // для AutoAccept()
            {       
                    currentQueue = (from a in dc.Participants // ПРОТЕСТИТЬ
                    where a.active && a.fk_process == procId
                    join b in steps on a.participantID equals b.fk_participent
                    select a).OrderByDescending(q => q.queue).Select(q => q.queue).FirstOrDefault();
            }

            var participantsZero =
                (from a in dc.Participants where a.active && a.fk_process == procId && a.queue == currentQueue select a.fk_user)
                    .ToList();

            if (participantsZero.Any())
            {
                foreach (var usrId in participantsZero)
                {
                    var userEmail =
                        (from a in dc.Users where a.active && a.userID == usrId select a.email).FirstOrDefault();
                    EmailTemplates etmp =
                        (from i in dc.EmailTemplates where i.active && i.name == "yourStep" select i).FirstOrDefault();
                    SendEmail(userEmail, etmp.emailTitle, etmp.emailContent, null);
                }
            }
            else
            {
                // LOG
            }

        }

        public void ApproveProcess(int procId)
        {
            EDMdbDataContext dc = new EDMdbDataContext();

            var initiatorEmail =
                (from a in dc.Processes
                    where a.active && a.processID == procId
                    join b in dc.Users on a.fk_initiator equals b.userID
                    select b.email).FirstOrDefault();

            EmailTemplates etmpInit =
                (from i in dc.EmailTemplates where i.active && i.name == "yourProcessApproved" select i).FirstOrDefault();
            SendEmail(initiatorEmail, etmpInit.emailTitle, etmpInit.emailContent, null);
        }

        public void RejectProcess(int procId)
        {
            EDMdbDataContext dc = new EDMdbDataContext();

            var initiatorEmail =
                (from a in dc.Processes
                    where a.active && a.processID == procId
                    join b in dc.Users on a.fk_initiator equals b.userID
                    select b.email).FirstOrDefault();

            EmailTemplates etmpInit =
                (from i in dc.EmailTemplates where i.active && i.name == "yourProcessCameBack" select i).FirstOrDefault();
            SendEmail(initiatorEmail, etmpInit.emailTitle, etmpInit.emailContent, null);
        }

        public void StepApprove(int procId, int userId)
        {
            EDMdbDataContext dc = new EDMdbDataContext();

            // Отправка следующему для serial
            var userEmail =
                (from a in dc.Users where a.active && a.userID == userId select a.email).FirstOrDefault();

            EmailTemplates etmp =
                (from i in dc.EmailTemplates where i.active && i.name == "yourStep" select i).FirstOrDefault();

            SendEmail(userEmail, etmp.emailTitle, etmp.emailContent, null);
        }
    

        public void StepReject(int procId, int userId)
        {
            //// не вижу смысла так спамить :)
        }

        public void AutoAccept()
        {
            EDMdbDataContext dc = new EDMdbDataContext();
            Approvment approve = new Approvment();

            var participantProcessDebt =
                (from a in dc.Participants where a.active && a.dateEnd < DateTime.Now
                    join b in dc.Processes on a.fk_process equals b.processID
                    where b.active && b.status == 0
                 select new {a.fk_user, a.fk_process}).ToList();

            EmailTemplates etmp =
                (from i in dc.EmailTemplates where i.active && i.name == "yourStepAutoSubmitted" select i).FirstOrDefault();

            foreach (var userProc in participantProcessDebt)
            {
                var userEmail =
                    (from a in dc.Users where a.active && a.userID == userProc.fk_user select a.email).FirstOrDefault();

                if (userEmail != null)
                SendEmail(userEmail, etmp.emailTitle, etmp.emailContent, null);


                int procMaxVersion =
                    (from a in dc.ProcessVersions where a.fk_process == userProc.fk_process && a.active select a)
                        .OrderByDescending(v => v.version).Select(v => v.processVersionID).FirstOrDefault();

                approve.AddApprove(userProc.fk_user, procMaxVersion, "Процесс согласован автоматически / "+DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            }

        }
    }
}
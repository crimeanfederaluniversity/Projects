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
                throw new Exception(ex.Message);
            }
                emailCopy.emailFrom = from;
                emailCopy.emailTo = mailto;
                emailCopy.emailTitle = caption;
                emailCopy.emailContent = message;
                emailCopy.emailAttachment = attachFile;
                emailCopy.sendOk = true;
                emailCopy.sendDateTime = DateTime.Now;
                dataContext.EmailCopies.InsertOnSubmit(emailCopy);

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
                if (SendMailProcess(ems.smtpName, 587, ems.email, ems.password, emailto, caption, messageBuilder.ToString(), attachFile) == 1)
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
    }
}
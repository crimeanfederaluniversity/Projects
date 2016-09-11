using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Registration
{
    public static class Action
    {
        private static int SendMail(string smtpServer, int port, string from, string password,
                                                string mailto, string caption, string message, string attachFile)
        {
            UsersDBDataContext rating = new UsersDBDataContext();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, "ИАС. КФУ-Рейтинги");
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
            catch (Exception e)

            {
                EmailCopies emailCopy = new EmailCopies();
                emailCopy.EmailFrom = from;
                emailCopy.EmailTo = mailto;
                emailCopy.EmailTitle = caption;
                emailCopy.EmailContent = message;
                emailCopy.EmailAttachment = attachFile;
                emailCopy.SendOk = false;
                emailCopy.SendDateTime = DateTime.Now;
                emailCopy.ErrorMessage = e.ToString();
                rating.EmailCopies.InsertOnSubmit(emailCopy);
                rating.SubmitChanges();
                return 0;
            }
                EmailCopies emailCopy2 = new EmailCopies();
                emailCopy2.EmailFrom = from;
                emailCopy2.EmailTo = mailto;
                emailCopy2.EmailTitle = caption;
                emailCopy2.EmailContent = message;
                emailCopy2.EmailAttachment = attachFile;
                emailCopy2.SendOk = true;
                emailCopy2.SendDateTime = DateTime.Now;
            rating.EmailCopies.InsertOnSubmit(emailCopy2);
            rating.SubmitChanges();
            return 1;
        }


        public static int MassMailing(string emailto, string caption, string message, string attachFile)
        {
            int errors=0;

            UsersDBDataContext rating = new UsersDBDataContext();

            var emails = (from a in rating.EmailSendTable select a).ToList();
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(message);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);            
            messageBuilder.Append("По вопросам заполнения форм отчетности обращайтесь по телефону:  ");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("Техническая поддержка: ");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("E-mail адрес: ");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("С уважением администрация \"ИАС.КФУ-Рейтинги\"");
          
            foreach (var ems in emails)
            {
                if (SendMail(ems.SMTPName, 587, ems.Email, ems.Password, emailto, caption, messageBuilder.ToString(), attachFile) == 1)
                {
                    ems.SendOk++;
               //     LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0MS0: From mail " + ems.Email + " success send an email to " + emailto + " with caption:\" " + caption + " \"");
                    break;
                }
                ems.SendError++;
                errors++;
            //    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0MS1: From mail " + ems.Email + " failed send an email to " + emailto + " with caption:\" " + caption + " \"");
            }

            rating.SubmitChanges();

            return errors;

        }

    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace KPIWeb
{
    public static class Action
    {

        public static int Encode(string code) // расшифровка кода специальности
        {
            string pattern = @"\b(\.+\d+\.)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(code);
            char[] charsToTrim = { '.', ' ', '\'' };
            string result = match.Groups[1].Value.Trim(charsToTrim);


            switch (result)
            {
                case "03": return 1; // бакалавр 
                case "04": return 3; // магистр
                case "05": return 2; // специалист
                case "06": return 4; // аспирант
                case "08": return 4; // аспирант
            }

            return 5;
        }

        public static string EncodeToStr(string code) // расшифровка кода специальности
        {
            string pattern = @"\b(\.+\d+\.)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(code);
            char[] charsToTrim = { '.', ' ', '\'' };
            string result = match.Groups[1].Value.Trim(charsToTrim);


            switch (result)
            {
                case "03":
                    return "бакалавриат";
                case "04":
                    return "магистратура";
                case "05":
                    return "специалитет";
                case "06":
                    return "аспирантура";
                case "08":
                    return "аспирантура";
            }

            return "не опр.";
        }


        private static int SendMail(string smtpServer, int port, string from, string password,
                                                string mailto, string caption, string message, string attachFile)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, "ИАС. КФУ-Программа развития");
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
                return 0;
            }

            return 1;
        }


        public static int MassMailing(string emailto, string caption, string message, string attachFile)
        {
            int errors=0;

            KPIWebDataContext kpiWeb = new KPIWebDataContext();

            var emails = (from a in kpiWeb.EmailSendTable select a).ToList();
            StringBuilder messageBuilder = new StringBuilder();

            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(message);
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append(Environment.NewLine);            
            messageBuilder.Append("По вопросам заполнения форм отчетности обращайтесь по телефону: +7-978-823-14-32");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("Техническая поддержка: +7-978-117-53-98");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("E-mail адрес: otdel-avtomatizatsii-kfu@yandex.ru");
            messageBuilder.Append(Environment.NewLine);
            messageBuilder.Append("С уважением администрация \"ИАС.КФУ-Программа развития\"");


            




            foreach (var ems in emails)
            {
                if (SendMail(ems.SMTPName, 587, ems.Email, ems.Password, emailto, caption, messageBuilder.ToString(), attachFile) == 1)
                {
                    ems.SendOk++;
                    LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0MS0: From mail " + ems.Email + " success send an email to " + emailto + " with caption:\" " + caption + " \"");
                    break;
                }
                ems.SendError++;
                errors++;
                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0MS1: From mail " + ems.Email + " failed send an email to " + emailto + " with caption:\" " + caption + " \"");
            }
            kpiWeb.SubmitChanges();

            return errors;

        }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                mail.From = new MailAddress(from, "ИАС. КФУ-Программа Развития");
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


        public static void MassMailing(string emailto, string caption, string message, string attachFile)
        {
            int errors = 0;
            int ok = 0;

            KPIWebDataContext kpiWeb = new KPIWebDataContext();

            var emails = (from a in kpiWeb.EmailSendTables select a).ToList();


            foreach (var ems in emails)
            {
                if ((SendMail(ems.SMTPName, 587, ems.Email, ems.Password, emailto, caption, message, attachFile)) == 1)
                {
                    ok++;
                    ems.SendOk = ok;
                    break;
                }

                errors++;
                ems.SendError = errors;
            }

            kpiWeb.SubmitChanges();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace TempAccreditationPost
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private const string EMAIL_TEST = "pereskokow@gmail.com";
        private const string EMAIL_RECIEVER = "sivas111@ya.ru";
        private const string RECIEVER_EMAIL = EMAIL_RECIEVER;
        private const string SENDER_EMAIL = "cfuportal-bot@mail.ru";
        private const string SENDER_PASSWORD = "777qwe777";

        private string email_message_template = "Наименование СМИ: {smi}\n"
            + "Контактный телефон: {phone}\n"
            + "e-mail: {email}\n"
            + "Факс: {fax}\n"
            + "Фамилия, имя, отчество журналиста: {fio}\n"
            + "Название мероприятия на которое требуется аккредитация: {event_name}\n";

        private string[] post_keys = new string[] {
            "smi",
            "phone",
            "email",
            "fax",
            "fio",
            "event_name"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>();

            foreach ( string key in post_keys )
            {
                string val = Request.Form[key];
                if (val == null)
                    continue;

                replacements.Add("{" + key + "}", val);
            }

            if ( replacements.Count() == post_keys.Count() )
            {
                sendFormDataToReciver(replacements);
            }
            else
            {
                test_label.Text = "No values were set";
            }
        }

        private void sendFormDataToReciver( Dictionary<string, string> data )
        {
            MailDefinition md = new MailDefinition();
            md.From = SENDER_EMAIL;
            md.Subject = "";
            MailMessage email = md.CreateMailMessage(RECIEVER_EMAIL, data, email_message_template, new System.Web.UI.Control());

            sendMail(email);
        }

        private void sendMail(MailMessage mail)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.mail.ru",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SENDER_EMAIL, SENDER_PASSWORD)
            };

            smtp.Send(mail);
        }
    }
}
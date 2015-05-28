using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Account
{
    public partial class RestorePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            UsersTable user = (from a in kPiDataContext.UsersTable where a.Email == TextBox1.Text && a.Active select a).FirstOrDefault();

            if (user != null)
            {
                if (user.PassCode != null && user.PassCode.Any())
                {
                    DisplayAlert("На " + user.Email +
                                 " уже отправлена инструкция по восстановлению пароля." + Environment.NewLine + "Если в течении 20 минут письмо не пришло, обратитесь к администрации." +Environment.NewLine+ "Чтобы узнать как связаться с нами перейдите в раздел \"Контакты\" в верхней части экрана.");
                }
                else
                {
                    string passCode = RandomString(25);
                    user.PassCode = passCode;
                    user.Confirmed = false;
                    kPiDataContext.SubmitChanges();

                    EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                                                 where a.Name == "PasswordRecover"
                                                 && a.Active == true
                                                 select a).FirstOrDefault();
                    if (EmailParams != null)
                        Action.MassMailing(user.Email, EmailParams.EmailTitle,
                            EmailParams.EmailContent.Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")).Replace("#LINK#", ConfigurationManager.AppSettings.Get("SiteName") + "/Account/UserRegister?&id=" + passCode)
                            , null);

                    /*
                    Action.MassMailing(user.Email, "Восстановление пароля в системе ИАС 'КФУ-Программа развития'",
                        "Здравствуйте!" + Environment.NewLine +
                        "На ваш почтовый адрес был сформирован запрос на восстановление пароля в системе ИАС 'КФУ-Программа развития!'" +
                        Environment.NewLine +
                        "Для того, чтобы указать новый пароль перейдите по ссылке:" + Environment.NewLine +
                        ConfigurationManager.AppSettings.Get("SiteName") + "/Account/UserRegister?&id=" + passCode +
                        Environment.NewLine +
                        "Спасибо!"
                        , null);
                    */
                    DisplayAlert("На email " + user.Email +
                                 " было выслано письмо с дальнейшими указаниями по восстановлению пароля");
                }
            }
            else
            {
                DisplayAlert("Пользователь с таким email адресом в системе не зарегестрирован!");
            }
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        private void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              string.Format("alert('{0}');window.location.href = 'UserLogin.aspx'",
                message.Replace("'", @"\'").Replace("\n", "\\n").Replace("\r", "\\r")),
                true);
        }
    }
}
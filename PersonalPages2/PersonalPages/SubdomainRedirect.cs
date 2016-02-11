using System;
using System.Linq;
using System.Text;
using System.Threading;


namespace PersonalPages
{
    public class SubdomainRedirect
    {
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
        private string CreateNewLineWithRandomString(int randomLineLength, DateTime endDateTime, int userId)
        {
            string myPassCode = RandomString(randomLineLength);
            PersonalPagesDataContext personalPages = new PersonalPagesDataContext();
            SubdomainRedirectAutologinTable newRedirect = new SubdomainRedirectAutologinTable();
            newRedirect.Active = true;
            newRedirect.PassCode = myPassCode;
            newRedirect.UserId = userId;
            newRedirect.EndDate = endDateTime;
            personalPages.SubdomainRedirectAutologinTable.InsertOnSubmit(newRedirect);
            personalPages.SubmitChanges();

            SubdomainRedirectAutologinTable findredirect = (from a in personalPages.SubdomainRedirectAutologinTable
                where a.PassCode == myPassCode
                select a).FirstOrDefault();
            return myPassCode;
        }
        public string CreateLinkToSubdomain(string subdomainLink, int userId, int timeToLive)
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime endDateTime = dateTimeNow.AddSeconds(timeToLive);
            string tmpPassCode = CreateNewLineWithRandomString(51, endDateTime, userId);
            string link = subdomainLink + "?"+PassCodeKeyName + "=" + tmpPassCode;
            Thread.Sleep(4000);
            return link;
            //генерируем шифр здоровенный
        }

        public string PassCodeKeyName = "passcode";
        public int GetUserIdByPassCode(string passCode)
        {
            PersonalPagesDataContext personalPages = new PersonalPagesDataContext();
            SubdomainRedirectAutologinTable currentSubdomain = (from a in personalPages.SubdomainRedirectAutologinTable
                where a.Active == true
                      && a.PassCode == passCode
                      && a.EndDate > DateTime.Now
                select a).FirstOrDefault();
            if (currentSubdomain != null)
            {
                int userId = currentSubdomain.UserId;
                currentSubdomain.Active = false;
                personalPages.SubdomainRedirectAutologinTable.DeleteOnSubmit(currentSubdomain);
                personalPages.SubmitChanges();
                return userId;
            }
            // не нашли строку в базе
            // может вышел срок 
            // или по ней уже переходили
            return 0;
        }
    }
}
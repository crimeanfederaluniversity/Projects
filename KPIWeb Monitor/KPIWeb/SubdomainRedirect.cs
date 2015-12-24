﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using KPIWeb;

namespace PersonalPages
{
    public class SubdomainRedirect
    {
        /* как пользоваться
         * это сам переход
          Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = UserSer.Id;
         * УЗНАЛИ ID ПОЛЬЗОВАТЕЛЯ
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            Response.Redirect(subdomainRedirect.CreateLinkToSubdomain("http://monitor.cfu-portal.ru", userId, 10));
        */


        /*            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("http://cfu-portal.ru");
            }
            int userId = UserSer.Id;

            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            Response.Redirect(subdomainRedirect.CreateLinkToSubdomain("http://cfu-portal.ru", userId, 10));
         */



        /*
            как пользоваться
         * это в default
            SubdomainRedirect subdomainRedirect = new SubdomainRedirect();
            string passCode = Request.Params[subdomainRedirect.PassCodeKeyName];
            int userIdFromGet = subdomainRedirect.GetUserIdByPassCode(passCode);
            if (userIdFromGet != 0)
            {
                Serialization UserSerId = new Serialization(userIdFromGet);
                Session["UserID"] = UserSerId;
            }
         */
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
            KPIWebDataContext kpiWeb = new KPIWebDataContext();
            SubdomainRedirectAutologinTable newRedirect = new SubdomainRedirectAutologinTable();
            newRedirect.Active = true;
            newRedirect.PassCode = myPassCode;
            newRedirect.UserId = userId;
            newRedirect.EndDate = endDateTime;
            kpiWeb.SubdomainRedirectAutologinTable.InsertOnSubmit(newRedirect);
            kpiWeb.SubmitChanges();
            return myPassCode;
        }
        public string CreateLinkToSubdomain(string subdomainLink, int userId, int timeToLive)
        {
            DateTime dateTimeNow = DateTime.Now;
            DateTime endDateTime = dateTimeNow.AddSeconds(timeToLive);
            string tmpPassCode = CreateNewLineWithRandomString(51, endDateTime, userId);
            string link = subdomainLink + "?"+PassCodeKeyName + "=" + tmpPassCode;
            return link;
            //генерируем шифр здоровенный
        }

        public string PassCodeKeyName = "passcode";
        public int GetUserIdByPassCode(string passCode)
        {
            KPIWebDataContext kpiWeb = new KPIWebDataContext();
            SubdomainRedirectAutologinTable currentSubdomain = (from a in kpiWeb.SubdomainRedirectAutologinTable
                where a.Active == true
                      && a.PassCode == passCode
                      && a.EndDate > DateTime.Now
                select a).FirstOrDefault();
            if (currentSubdomain != null)
            {
                int userId = currentSubdomain.UserId;
                currentSubdomain.Active = false;
                kpiWeb.SubdomainRedirectAutologinTable.DeleteOnSubmit(currentSubdomain);
                kpiWeb.SubmitChanges();
                return userId;
            }
            // не нашли строку в базе
            // может вышел срок 
            // или по ней уже переходили
            return 0;
        }
    }
}
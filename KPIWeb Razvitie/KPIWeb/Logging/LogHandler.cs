using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    public class LogHandler
    {
        private static LogWriter _LogWriter;
        public static LogWriter LogWriter
        {
            get
            {
                if (_LogWriter == null)
                    _LogWriter = InitLogger();
                return _LogWriter;
            }
        }

        private static LogWriter InitLogger()
        {
            try
            {
                string loggingConfig = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                return new LogWriter(loggingConfig, false, null);
            }
            catch (Exception e)
            {
                throw new Exception("Error creating log4net handler.", e);
            }
        }
    }
}
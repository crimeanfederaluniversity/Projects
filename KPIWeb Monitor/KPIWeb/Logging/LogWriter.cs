using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using log4net.Config;
using log4net.Repository;

namespace KPIWeb
{
    public class LogWriter
    {
        private readonly ILog _log;

        public LogWriter()
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public LogWriter(string fileName, bool watchChanges, Type loggerType)
        {
            ILoggerRepository repository = LogManager.GetRepository();
            if (loggerType == null)
                loggerType = MethodBase.GetCurrentMethod().DeclaringType;
            var fileInfo = new FileInfo(fileName);
            if (watchChanges)
                XmlConfigurator.ConfigureAndWatch(repository, fileInfo);
            else
                XmlConfigurator.Configure(repository, fileInfo);
            _log = LogManager.GetLogger(repository.Name, loggerType);
        }

        public void WriteLog(LogCategory category, string message, params object[] args)
        {
            WriteLog(category, string.Format(message, args));
        }

        public void WriteError(Exception ex)
        {
            WriteLog(LogCategory.ERROR, PrepareException(ex));
        }
        public void WriteError(Exception ex, string message)
        {

            WriteLog(LogCategory.ERROR, string.Format("Exception:{0}  \n Message:{1}", PrepareException(ex), message));
        }

        public void WriteLog(LogCategory category, string message)
        {
            switch (category)
            {
                case LogCategory.DEBUG:
                case LogCategory.INFO:
                case LogCategory.WARN:
                case LogCategory.ERROR:
                case LogCategory.FATAL:
                    message = PrepareMessageString(message);
                    break;
                default:
                    break;
            }
            switch (category)
            {
                case LogCategory.DEBUG:
                    if (_log.IsDebugEnabled)
                        _log.Debug(message);
                    break;
                case LogCategory.INFO:
                    if (_log.IsInfoEnabled)
                        _log.Info(message);
                    break;
                case LogCategory.WARN:
                    if (_log.IsWarnEnabled)
                        _log.Warn(message);
                    break;
                case LogCategory.ERROR:
                    if (_log.IsErrorEnabled)
                        _log.Error(message);
                    break;
                case LogCategory.FATAL:
                    if (_log.IsFatalEnabled)
                        _log.Fatal(message);
                    break;
                default:
                    break;
            }
        }

        private static string PrepareException(Exception ex)
        {
            if (ex == null)
                return string.Empty;
            string res = ex.ToString();
            if (ex.Data != null && ex.Data.Count > 0)
            {
                res += "\n Additional Info: \n";
                foreach (var k in ex.Data.Keys)
                {
                    res += string.Format("{0} : {1}\n", k, ex.Data[k]);
                }
            }
            try
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1); // back to once step before
                MethodBase methodBase = stackFrame.GetMethod();
                int lineNumber = stackFrame.GetFileLineNumber();
                if (methodBase.Name.Equals("Write"))
                {
                    // back to 2 step before
                    stackFrame = stackTrace.GetFrame(2);
                    methodBase = stackFrame.GetMethod();
                    lineNumber = stackFrame.GetFileLineNumber();
                }
                res += string.Format("{0}  {1} ", methodBase.ReflectedType, methodBase);
                res += "Line Number: " + lineNumber;
            }
            catch
            { }
            return res;
        }

        private static string PrepareMessageString(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                message = message.Replace(Environment.NewLine, " ");
                var forbiddenSimbols = new[] { ';', '\n', '\r' };
                foreach (Char forbiddenChar in forbiddenSimbols)
                    message = message.Replace(forbiddenChar, ' ');
            }
            return message;
        }
    }

    public enum LogCategory
    {
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }
}
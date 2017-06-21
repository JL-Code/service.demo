using log4net.Config;
using System;
using System.IO;

namespace log4net.demo
{
    public class LogHelper
    {
        private static readonly Lazy<ILog> _logger = new Lazy<ILog>(() => LogManager.GetLogger("ErrorAppender"));
        static LogHelper()
        {
            var fileInfo = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\log4net.config");
            XmlConfigurator.Configure(fileInfo);
        }
        static ILog Logger { get => _logger.Value; }
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Info(object message)
        {
            Logger.Info(message);
        }


        public static void Error(object message, Exception ex)
        {
            Logger.Error(message, ex);
        }

        public static void Warn(object message, Exception ex)
        {
            Logger.Warn(message, ex);
        }

        public static void Debug(object message, Exception ex)
        {
            Logger.Debug(message, ex);
        }

        public static void Fatal(object message, Exception ex)
        {
            Logger.Fatal(message, ex);
        }
    }
}

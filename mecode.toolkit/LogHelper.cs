using log4net;
using log4net.Config;
using System;
using System.IO;

namespace mecode.toolkit
{
    public class Logger
    {
        private static readonly Lazy<ILog> _logger = new Lazy<ILog>(() => LogManager.GetLogger("ErrorAppender"));
        static Logger()
        {
            var fileInfo = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\log4net.config");
            XmlConfigurator.Configure(fileInfo);
        }
        static ILog Instance { get => _logger.Value; }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Info(object message)
        {
            Instance.Info(message);
        }
        
        public static void Error(object message, Exception ex)
        {
            Instance.Error(message, ex);
        }

        public static void Warn(object message, Exception ex)
        {
            Instance.Warn(message, ex);
        }

        public static void Debug(object message, Exception ex)
        {
            Instance.Debug(message, ex);
        }

        public static void Fatal(object message, Exception ex)
        {
            Instance.Fatal(message, ex);
        }
    }
}

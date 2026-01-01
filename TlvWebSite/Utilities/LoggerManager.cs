using log4net;
using log4net.Config;
using System.Reflection;

namespace TlvWebSite.Utilities
{
    public static class LoggerManager
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        static LoggerManager()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Error(string message, Exception? ex = null)
        {
            if (ex != null)
            {
                _logger.Error(message, ex);
            }
            else
            {
                _logger.Error(message);
            }
        }
    }
}

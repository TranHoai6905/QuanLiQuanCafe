using log4net;
using System;

namespace CKBCDT
{
    internal static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public static void Info(string message) => log.Info(message);
        public static void Warn(string message) => log.Warn(message);
        public static void Warn(string message, Exception ex) => log.Warn(message, ex);
        public static void Error(string message) => log.Error(message);
        public static void Error(string message, Exception ex) => log.Error(message, ex);
        public static void Debug(string message) => log.Debug(message);
    }
}

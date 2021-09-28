using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;

namespace R6T.WebApi.App_Start
{
    public static class Logger
    {
        private static readonly ILogger ErrorLogger;

        static Logger()
        {
            ErrorLogger = new LoggerConfiguration()
                .WriteTo.File(HttpContext.Current.Server.MapPath("~/logs/log-.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void LogError(string error)
        {
            ErrorLogger.Error(error);
        }

        public static void LogInfo(string info)
        {
            ErrorLogger.Information(info);
        }
    }
}
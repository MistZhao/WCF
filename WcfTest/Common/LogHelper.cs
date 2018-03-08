using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "PublicConf/log4net.config", Watch = true)]
namespace Common
{
    public class LogHelper
    {
        private static log4net.ILog log = LogManager.GetLogger("InfoLogger");

        public static void Info(string strMsg)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(strMsg);
            }
        }

        public static void Warn(string strMsg)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(strMsg);
            }
        }

        public static void Error(string strMsg)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(strMsg);
            }
        }

        public static void Fatal(string strMsg)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(strMsg);
            }
        }
    }
}

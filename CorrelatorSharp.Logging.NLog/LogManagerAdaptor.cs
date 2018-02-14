using NLogLogManager = NLog.LogManager;

namespace CorrelatorSharp.Logging.NLog
{
    public class LogManagerAdaptor : ILogManagerAdaptor
    {
        public ILogger GetLogger(string name)
        {
            return new LoggerAdaptor(NLogLogManager.GetLogger(name));
        }
    }
}

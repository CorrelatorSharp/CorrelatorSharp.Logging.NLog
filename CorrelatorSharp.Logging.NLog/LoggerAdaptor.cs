using System;
using NLog;
using NLogILogger = NLog.ILogger;

namespace CorrelatorSharp.Logging.NLog
{
    public class LoggerAdaptor : Logging.ILogger
    {
        public const string ActivityIdPropertyName = "cs-activity-id";
        public const string ParentIdPropertyName = "cs-activity-parentid";
        public const string NamePropertyName = "cs-activity-name";

        private readonly NLogILogger _logger;
        
        public LoggerAdaptor(NLogILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), $"{nameof(logger)} is null.");
        }

        public string Name => _logger.Name;

        public bool IsTraceEnabled => _logger.IsTraceEnabled;
        public bool IsWarnEnabled => _logger.IsWarnEnabled;
        public bool IsInfoEnabled => _logger.IsInfoEnabled;
        public bool IsErrorEnabled => _logger.IsErrorEnabled;
        public bool IsDebugEnabled => _logger.IsDebugEnabled;
        public bool IsFatalEnabled => _logger.IsFatalEnabled;

        private LogEventInfo CreateLogEntry(LogLevel level, string format, params object[] values)
        {
            var entry = new LogEventInfo(level, Name, null, format, values);

            var currentActivity = ActivityScope.Current;
            if (currentActivity != null)
            {
                entry.Properties[ActivityIdPropertyName] = currentActivity.Id;
                entry.Properties[ParentIdPropertyName] = currentActivity.ParentId;
                entry.Properties[NamePropertyName] = currentActivity.Name;
            }

            return entry;
        }

        public void LogTrace(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsTraceEnabled)
            {
                var entry = CreateLogEntry(LogLevel.Trace, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogDebug(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsDebugEnabled)
            {
                var entry = CreateLogEntry(LogLevel.Debug, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogError(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsErrorEnabled)
            {
                var entry = CreateLogEntry(LogLevel.Error, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogWarn(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsWarnEnabled)
            {
                var entry = CreateLogEntry(LogLevel.Warn, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogInfo(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsInfoEnabled)
            {
                var entry = CreateLogEntry(LogLevel.Info, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogFatal(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsFatalEnabled)
            {
                var entry = CreateLogEntry(LogLevel.Fatal, format, values);
                entry.Exception = exception;

                _logger.Log(entry);
            }
        }

        public void LogFatal(string format, params object[] values)
        {
            if (_logger.IsFatalEnabled)
            {
                _logger.Log(CreateLogEntry(LogLevel.Fatal, format, values));
            }
        }

        public void LogWarn(string format, params object[] values)
        {
            if (_logger.IsWarnEnabled)
            {
                _logger.Log(CreateLogEntry(LogLevel.Warn, format, values));
            }
        }

        public void LogInfo(string format, params object[] values)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Log(CreateLogEntry(LogLevel.Info, format, values));
            }
        }

        public void LogTrace(string format, params object[] values)
        {
            if (_logger.IsTraceEnabled)
            {
                _logger.Log(CreateLogEntry(LogLevel.Trace, format, values));
            }
        }

        public void LogDebug(string format, params object[] values)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Log(CreateLogEntry(LogLevel.Debug, format, values));
            }
        }

        public void LogError(string format, params object[] values)
        {
            if (_logger.IsErrorEnabled)
            {
                _logger.Log(CreateLogEntry(LogLevel.Error, format, values));
            }
        }
    }
}
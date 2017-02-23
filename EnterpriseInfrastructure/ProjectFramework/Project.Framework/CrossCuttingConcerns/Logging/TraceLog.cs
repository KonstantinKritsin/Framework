using System;
using System.Diagnostics;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.CrossCuttingConcerns.Logging
{
	public class TraceLog : ILogger {
		public bool IsInfoEnabled => true;
		public bool IsDebugEnabled => true;
		public bool IsWarnEnabled => true;
		public bool IsErrorEnabled => true;
		public bool IsFatalEnabled => true;
		public void Debug(object message)
		{
			Info(message);
		}

		public void Debug(object message, Exception exception)
		{
			Info(message, exception);
		}

		public void DebugFormat(string format, params object[] args)
		{
			InfoFormat(format, args);
		}

		public void DebugFormat(string format, Exception exception, params object[] args)
		{
			InfoFormat(format, exception, args);
		}

		public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			InfoFormat(formatProvider, format, args);
		}

		public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			InfoFormat(formatProvider, format, exception, args);
		}

		public void Info(object message)
		{
			Info(message, null);
		}

		public void Info(object message, Exception exception)
		{
			InfoFormat(message.ToString(), exception);
		}

		public void InfoFormat(string format, params object[] args)
		{
			InfoFormat(format, null, args);
		}

		public void InfoFormat(string format, Exception exception, params object[] args)
		{
			TraceInfo(null, format, exception, args);
		}

		public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			TraceInfo(formatProvider, format, null, args);
		}

		public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			TraceInfo(formatProvider, format, exception, args);
		}

		public void Warn(object message)
		{
			Warn(message, null);
		}

		public void Warn(object message, Exception exception)
		{
			WarnFormat(message.ToString(), exception);
		}

		public void WarnFormat(string format, params object[] args)
		{
			WarnFormat(format, null, args);
		}

		public void WarnFormat(string format, Exception exception, params object[] args)
		{
			TraceWarning(null, format, exception, args);
		}

		public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			TraceWarning(formatProvider, format, null, args);
		}

		public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			TraceWarning(formatProvider, format, exception, args);
		}

		public void Error(object message)
		{
			Fatal(message);
		}

		public void Error(object message, Exception exception)
		{
			Fatal(message, exception);
		}

		public void ErrorFormat(string format, params object[] args)
		{
			FatalFormat(format, args);
		}

		public void ErrorFormat(string format, Exception exception, params object[] args)
		{
			FatalFormat(format, exception, args);
		}

		public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			FatalFormat(formatProvider, format, args);
		}

		public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			FatalFormat(formatProvider, format, exception, args);
		}

		public void Fatal(object message)
		{
			Fatal(message, null);
		}

		public void Fatal(object message, Exception exception)
		{
			FatalFormat(message.ToString(), exception);
		}

		public void FatalFormat(string format, params object[] args)
		{
			TraceError(null, format, null, args);
		}

		public void FatalFormat(string format, Exception exception, params object[] args)
		{
			TraceError(null, format, exception, args);
		}

		public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			TraceError(formatProvider, format, null, args);
		}

		public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			TraceError(formatProvider, format, exception, args);
		}

		private static void TraceError(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			Trace.TraceError(GetMessage(formatProvider, format, exception, args));
		}
		private static void TraceInfo(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			Trace.TraceInformation(GetMessage(formatProvider, format, exception, args));
		}
		private static void TraceWarning(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			Trace.TraceWarning(GetMessage(formatProvider, format, exception, args));
		}

		private static string GetMessage(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
			var ex = exception?.ToString();
			var msg = "";
			if (!string.IsNullOrWhiteSpace(format))
			{
				if (args.Length == 0)
					msg = format;
				else
					msg = formatProvider != null ? string.Format(formatProvider, format, args) : string.Format(format, args);
			}
			return $"utc {DateTime.UtcNow:s}: {msg} {ex}";
		}
	}
}
using System;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.CrossCuttingConcerns.Logging
{
	public interface ILogger
	{
		bool IsInfoEnabled { get; }
		bool IsDebugEnabled { get; }
		bool IsWarnEnabled { get; }
		bool IsErrorEnabled { get; }
		bool IsFatalEnabled { get; }
		void Debug(object message);

		void Debug(object message, Exception exception);

		void DebugFormat(string format, params object[] args);

		void DebugFormat(string format, Exception exception, params object[] args);

		void DebugFormat(IFormatProvider formatProvider, string format, params object[] args);

		void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		void Info(object message);

		void Info(object message, Exception exception);

		void InfoFormat(string format, params object[] args);

		void InfoFormat(string format, Exception exception, params object[] args);

		void InfoFormat(IFormatProvider formatProvider, string format, params object[] args);

		void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		void Warn(object message);

		void Warn(object message, Exception exception);

		void WarnFormat(string format, params object[] args);

		void WarnFormat(string format, Exception exception, params object[] args);

		void WarnFormat(IFormatProvider formatProvider, string format, params object[] args);

		void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		void Error(object message);

		void Error(object message, Exception exception);

		void ErrorFormat(string format, params object[] args);

		void ErrorFormat(string format, Exception exception, params object[] args);

		void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args);

		void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

		void Fatal(object message);

		void Fatal(object message, Exception exception);

		void FatalFormat(string format, params object[] args);

		void FatalFormat(string format, Exception exception, params object[] args);

		void FatalFormat(IFormatProvider formatProvider, string format, params object[] args);

		void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);
	}
}
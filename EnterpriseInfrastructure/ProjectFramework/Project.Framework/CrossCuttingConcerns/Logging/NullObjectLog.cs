﻿using System;

namespace Project.Framework.CrossCuttingConcerns.Logging
{
	public class NullObjectLog : ILogger
	{
		public bool IsInfoEnabled => false;
		public bool IsDebugEnabled => false;

		public bool IsWarnEnabled => false;
		public bool IsErrorEnabled => false;

		public bool IsFatalEnabled => false;

		public void Debug(object message)
		{
		}

		public void Debug(object message, Exception exception)
		{
		}

		public void DebugFormat(string format, params object[] args)
		{
		}

		public void DebugFormat(string format, Exception exception, params object[] args)
		{
		}

		public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
		}

		public void Info(object message)
		{
		}

		public void Info(object message, Exception exception)
		{
		}

		public void InfoFormat(string format, params object[] args)
		{
		}

		public void InfoFormat(string format, Exception exception, params object[] args)
		{
		}

		public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
		}

		public void Warn(object message)
		{
		}

		public void Warn(object message, Exception exception)
		{
		}

		public void WarnFormat(string format, params object[] args)
		{
		}

		public void WarnFormat(string format, Exception exception, params object[] args)
		{
		}

		public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
		}

		public void Error(object message)
		{
		}

		public void Error(object message, Exception exception)
		{
		}

		public void ErrorFormat(string format, params object[] args)
		{
		}

		public void ErrorFormat(string format, Exception exception, params object[] args)
		{
		}

		public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
		}

		public void Fatal(object message)
		{
		}

		public void Fatal(object message, Exception exception)
		{
		}

		public void FatalFormat(string format, params object[] args)
		{
		}

		public void FatalFormat(string format, Exception exception, params object[] args)
		{
		}

		public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
		}

		public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
		{
		}
	}
}
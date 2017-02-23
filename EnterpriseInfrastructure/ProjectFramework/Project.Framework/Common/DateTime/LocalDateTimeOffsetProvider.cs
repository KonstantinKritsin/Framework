using System;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.Common.DateTime
{
	public class LocalDateTimeOffsetProvider : IDateTimeOffsetProvider
	{
		public DateTimeOffset Now => DateTimeOffset.Now;

		public DateTimeOffset MinValue => DateTimeOffset.MinValue;
	}
}
using System;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.Common.DateTime
{
	public class UtcDateTimeOffsetProvider : IDateTimeOffsetProvider
	{
		public DateTimeOffset Now => DateTimeOffset.UtcNow;

		public DateTimeOffset MinValue => DateTimeOffset.MinValue;
	}
}
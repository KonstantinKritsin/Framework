using System;

namespace Project.Framework.Common.DateTime
{
	public interface IDateTimeOffsetProvider
	{
		DateTimeOffset Now { get; }
	}
}
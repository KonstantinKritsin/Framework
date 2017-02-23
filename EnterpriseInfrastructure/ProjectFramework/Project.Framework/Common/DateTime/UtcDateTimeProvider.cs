// ReSharper disable UnusedMember.Global
namespace Project.Framework.Common.DateTime
{
	public class UtcDateTimeProvider : IDateTimeProvider
	{
		public System.DateTime Now => System.DateTime.UtcNow;

		public System.DateTime MinValue => System.DateTime.MinValue;
	}
}
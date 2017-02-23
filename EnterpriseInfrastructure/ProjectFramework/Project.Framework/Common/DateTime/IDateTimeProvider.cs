// ReSharper disable UnusedMember.Global
namespace Project.Framework.Common.DateTime
{
	public interface IDateTimeProvider
	{
		System.DateTime Now { get; }
		System.DateTime MinValue { get; }
	}
}
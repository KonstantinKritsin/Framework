namespace Project.Framework.Common.DateTime
{
	public class LocalDateTimeProvider : IDateTimeProvider
	{
		public System.DateTime Now => System.DateTime.Now;

		public System.DateTime MinValue => System.DateTime.MinValue;
	}
}
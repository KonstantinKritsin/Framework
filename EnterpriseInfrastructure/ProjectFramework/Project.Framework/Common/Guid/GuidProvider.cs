namespace Project.Framework.Common.Guid
{
	public class GuidProvider : IGuidProvider
	{
		public System.Guid NewGuid()
		{
			return System.Guid.NewGuid();
		}
	}
}
// ReSharper disable UnusedMember.Global
namespace Project.Framework.Common.Enums
{
	public enum IsolationLevel
	{
		Serializable,
		RepeatableRead,
		ReadCommitted,
		ReadUncommitted,
		Snapshot,
		Chaos,
		Unspecified,
	}
}
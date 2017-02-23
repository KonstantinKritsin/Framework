using System;

namespace Project.Framework.DataAccess.ModelContract
{
	public interface IModifiedBy<T> where T : IEquatable<T>
	{
		T ModifiedById { get; set; }
	}
}
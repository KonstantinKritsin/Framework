using System;

namespace Project.Framework.DataAccess.ModelContract
{
	public interface ICreatedBy<T> where T : IEquatable<T>
	{
		T CreatedById { get; set; }
	}
}
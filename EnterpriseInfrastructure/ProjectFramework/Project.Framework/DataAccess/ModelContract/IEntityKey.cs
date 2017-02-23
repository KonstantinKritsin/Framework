using System;

namespace Project.Framework.DataAccess.ModelContract
{
	public interface IEntityKey<T> where T : IEquatable<T>
	{
		T Id { get; set; }
	}
}

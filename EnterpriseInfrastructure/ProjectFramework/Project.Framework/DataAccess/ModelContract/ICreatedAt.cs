using System;

namespace Project.Framework.DataAccess.ModelContract
{
	public interface ICreatedAt
	{
		DateTimeOffset CreatedAt { get; set; }
	}
}
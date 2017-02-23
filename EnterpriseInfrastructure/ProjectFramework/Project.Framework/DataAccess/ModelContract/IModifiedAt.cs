using System;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.DataAccess.ModelContract
{
	public interface IModifiedAt
	{
		DateTimeOffset ModifiedAt { get; set; }
	}
}
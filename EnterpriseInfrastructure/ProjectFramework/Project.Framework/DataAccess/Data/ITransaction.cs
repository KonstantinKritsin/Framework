using System;

namespace Project.Framework.DataAccess.Data
{
	public interface ITransaction : IDisposable
	{
		void Commit();
	}
}
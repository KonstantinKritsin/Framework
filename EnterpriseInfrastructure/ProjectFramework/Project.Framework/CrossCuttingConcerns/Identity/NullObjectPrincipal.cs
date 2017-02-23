using System;
using System.Security.Principal;
using System.Threading;

namespace Project.Framework.CrossCuttingConcerns.Identity
{
	public class NullObjectPrincipal : IPrincipal<IMember>
	{
		public bool IsInRole(string role)
		{
			return false;
		}

		public IIdentity Identity { get; } = Thread.CurrentPrincipal.Identity;
		public IMember Member => new NullMember();

		private class NullMember : IMember
		{
			public int Id { get; } = 0;
			public string Login { get; } = null;
			public string Name { get; } = null;
			public string[] Roles { get; } = Array.Empty<string>();
		}
	}
}
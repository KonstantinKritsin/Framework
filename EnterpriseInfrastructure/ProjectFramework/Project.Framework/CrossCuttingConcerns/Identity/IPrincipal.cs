using System.Security.Principal;

namespace Project.Framework.CrossCuttingConcerns.Identity
{
	public interface IPrincipal<out TMember> : IPrincipal where TMember : IMember
	{
		TMember Member { get; }
	}
}

namespace Project.Framework.CrossCuttingConcerns.Identity
{
    public interface IMember
    {
        int Id { get; }
        string Login { get; }
        string Name { get; }
        string[] Roles { get; }
    }
}
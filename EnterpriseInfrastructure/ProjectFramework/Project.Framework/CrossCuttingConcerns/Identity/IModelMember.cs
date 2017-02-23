namespace Project.Framework.CrossCuttingConcerns.Identity
{
    public interface IModelMember<out TModel> : IMember
    {
        TModel Model { get; }
    }
}
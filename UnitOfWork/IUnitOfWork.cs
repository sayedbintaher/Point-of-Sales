namespace PosAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}

namespace CGEODP.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

using chatmobile.entites;

namespace chatmobile.repositories
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync(CancellationToken cancellationToken);
        IRepositories<User> User { get; }
    }
}
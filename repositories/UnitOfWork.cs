using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmobile.DB;
using chatmobile.entites;

namespace chatmobile.repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            User = new UserRepositories(_context);
        }
        public IRepositories<User> User { get; private set; }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
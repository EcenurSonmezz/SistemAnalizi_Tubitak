using KBYS.DataAcces.KBYSDbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KBYS.DataAcces.UnitOfWork
{
    public class UnitOfWork<C> : IUnitOfWork<C> where C : DbContext
    {
        private readonly ILogger<UnitOfWork<C>> _logger;
        public C Context { get; }

        public UnitOfWork(C context, ILogger<UnitOfWork<C>> logger)
        {
            Context = context;
            _logger = logger;
        }

        public async Task<int> Complete()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    var val = await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return val;
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, e.Message);
                    return 0;
                }
            }
        }
    }
}

using KBYS.DataAcces.GenericRepository;
using KBYS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.DataAcces.UnitOfWork
{
    public interface IUnitOfWork<C> : IDisposable where C : DbContext
    {
        C Context { get; }
        Task<int> Complete();
        Task<int> SaveAsync();

    }
}

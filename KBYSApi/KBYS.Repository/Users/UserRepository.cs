using KBYS.DataAcces.GenericRepository;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Repository.Users
{
    public class UserRepository : GenericRepository<User, KBYSDbContext>,
          IUserRepository
    {
        public UserRepository(IUnitOfWork<KBYSDbContext> uow) : base(uow)
        {
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await Context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}

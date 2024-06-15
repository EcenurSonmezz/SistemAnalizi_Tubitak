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

namespace KBYS.Repository.UserMealRecords
{
    public class UserMealRecordRepository : GenericRepository<UserMealRecord, KBYSDbContext>, IUserMealRecordRepository
    {
        public UserMealRecordRepository(IUnitOfWork<KBYSDbContext> uow) : base(uow)
        { 
        }

        public async Task<IEnumerable<UserMealRecord>> GetTodayUserMealsAsync(Guid userId)
        {
            var today = DateTime.UtcNow.Date;
            return await Context.UserMealRecords
                .Include(umr => umr.Food)
                .ThenInclude(f => f.NutritionalValues)
                .Where(umr => umr.UserId == userId && umr.DateConsumed.Date == today)
                .ToListAsync();
        }
    }
}

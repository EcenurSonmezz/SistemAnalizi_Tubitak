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

namespace KBYS.Repository.Foods
{
    public class FoodRepository : GenericRepository<Food, KBYSDbContext>, IFoodRepository
    {
        public FoodRepository(IUnitOfWork<KBYSDbContext> uow) : base(uow)
        {
        }
        public async Task<Food> GetFoodWithNutritionalValues(int id)
        {
             return await Context.Foods
                .Include(f => f.NutritionalValues)
                .SingleOrDefaultAsync(f => f.Id == id);
        }
    }
}

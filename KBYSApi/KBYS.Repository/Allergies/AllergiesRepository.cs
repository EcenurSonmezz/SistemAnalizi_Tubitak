using KBYS.DataAcces.GenericRepository;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Repository.Allergies
{
    public class AllergiesRepository : GenericRepository<Allergy,KBYSDbContext>,
        IAllergiesRepository
    {
        public AllergiesRepository(IUnitOfWork<KBYSDbContext> uow) : base(uow)
        {
        }
    }
}

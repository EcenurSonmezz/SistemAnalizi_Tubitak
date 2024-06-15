using KBYS.DataAcces.GenericRepository;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Repository.UserAllergies
{
    public class UserAllergiesRepository : GenericRepository<UserAllergy, KBYSDbContext>, IUserAllergiesRepository
    {
        public UserAllergiesRepository(IUnitOfWork<KBYSDbContext> uow) : base(uow)
        {
        }
    }
}

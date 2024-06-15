using KBYS.DataAcces.GenericRepository;
using KBYS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Repository.UserMealRecords
{
    public interface IUserMealRecordRepository : IGenericRepository<UserMealRecord>
    {
        Task<IEnumerable<UserMealRecord>> GetTodayUserMealsAsync(Guid userId);
    }
}

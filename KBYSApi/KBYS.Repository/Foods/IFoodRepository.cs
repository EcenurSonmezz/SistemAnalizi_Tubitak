﻿using KBYS.DataAcces.GenericRepository;
using KBYS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Repository.Foods
{
    public interface IFoodRepository : IGenericRepository<Food>
    {
        Task<Food> GetFoodWithNutritionalValues(int id);

    }
}

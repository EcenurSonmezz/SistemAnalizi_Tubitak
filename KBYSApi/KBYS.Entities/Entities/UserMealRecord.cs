﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Entities.Entities
{
    public class UserMealRecord
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public DateTime DateConsumed { get; set; }
    }
}

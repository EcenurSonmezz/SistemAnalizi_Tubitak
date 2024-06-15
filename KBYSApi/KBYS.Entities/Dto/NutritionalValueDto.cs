using KBYS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Entities.Dto
{
    public class NutritionalValueDto
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public string name { get; set; }
        public double foodValue { get; set; }
        public string Unit { get; set; }
    }
}

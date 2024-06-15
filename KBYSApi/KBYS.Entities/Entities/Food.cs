using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Entities.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string food_type { get; set; } 
        public ICollection<NutritionalValue> NutritionalValues { get; set; }

        public Food()
        {
            NutritionalValues = new List<NutritionalValue>();
        }
    }
}

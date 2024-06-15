using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Entities.Entities
{
    public class Allergy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserAllergy> UserAllergies { get; set; } = new List<UserAllergy>();

    }
}

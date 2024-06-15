using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Entities.Entities
{
    public class UserAllergy
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid AllergyId { get; set; }
        public Allergy Allergy { get; set; }
    }
}

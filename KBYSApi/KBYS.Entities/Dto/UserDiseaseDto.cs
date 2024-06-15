using KBYS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.Entities.Dto
{
    public class UserDiseaseDto
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}

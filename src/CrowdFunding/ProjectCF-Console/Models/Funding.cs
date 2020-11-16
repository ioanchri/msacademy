using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCF.Models
{
    public class Funding
    {
        public int Id { get; set; }
        public int Ammount { get; set; }
        public Project Project { get; set; }
        public User User { get; set; }

    }
}

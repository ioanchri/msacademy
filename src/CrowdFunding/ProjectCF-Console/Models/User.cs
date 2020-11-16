using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }   
        public List<Project> Projects { get; set; }
        public List<Funding> Fundings { get; set; }

        //ADDRESS??
    }
}

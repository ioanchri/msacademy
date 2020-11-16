using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectCF.Models;
using ProjectCF.Options;

namespace ProjectCF.Services
{
    public class UserService : IUserService
    {
        
            public User CreateUser(UserOption userOption)
            {
            //validation
            if (userOption == null) return null;
            if (userOption.FirstName == null) return null;

            User user = new User
    
                {
                    FirstName = userOption.FirstName,
                    LastName = userOption.LastName,
                    Email = userOption.Email
                };

            //dbContext.Customers.Add(customer);
            // dbContext.SaveChanges();
            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
            }

        public User DeleteUser(UserOption userOption, string email)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(UserOption userOption, string email)
        {
            throw new NotImplementedException();
        }
    }
}

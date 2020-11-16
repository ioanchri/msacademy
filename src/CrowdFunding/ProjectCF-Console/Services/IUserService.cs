using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectCF.Models;
using ProjectCF.Options;

namespace ProjectCF.Services
{
    interface IUserService
    {
        User CreateUser(UserOption userOption);
        User UpdateUser(UserOption userOption, string email);
        User DeleteUser(UserOption userOption, string email);

    }
}

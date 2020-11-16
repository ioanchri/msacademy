using System;
using ProjectCF.Models;
using ProjectCF.Options;
using ProjectCF.Services;

namespace ProjectCF_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the test area of the CrowdFunding website!");

            //var userOpt = new UserOption()
            //{
            //    FirstName = "Firstname",
            //    LastName = "Lastname",
            //    Email = "test@gmail.com",
            //    Password = "test",
            //    Dob = DateTime.Today


            //};


            //Console.WriteLine($"{userOpt.FirstName} {userOpt.LastName} {userOpt.Dob} {userOpt.Email}");


            UserOption userOption = new UserOption
            {
                FirstName = "Nick",
                LastName = "Giggs",
                Dob = new DateTime(1994, 5, 14),
                CreatedDate = DateTime.Now
            };

            UserService cs = new UserService();
            cs.CreateUser(userOption);

            Console.WriteLine("{0} {1},Date of Birth: {2},Created on {3}",userOption.FirstName,userOption.LastName,userOption.Dob,userOption.CreatedDate);

        }
    }
}
